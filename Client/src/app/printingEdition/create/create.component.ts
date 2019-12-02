import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {PrintingEditionModel} from '../../shared/models/printing-edition.model';
import {PrintingEditionService} from '../../services/printingEdition.service';
import {AuthorService} from '../../services/author.service';
import {AuthorModel} from '../../shared/models/author.model';
import {Router} from '@angular/router';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent implements OnInit {
  private defaultValue = '1';
  private form: FormGroup;
  private printingEdition: PrintingEditionModel = {};
  private currency = this.defaultValue;
  private type = this.defaultValue;
  private status = this.defaultValue;
  private addAuthorForm: boolean;
  private authors: AuthorModel[];
  private authorName: string;
  private authorDeathDay: string;
  private authorBirthDay: string;
  private selectedAuthor: string[];
  private fileData: File;
  private previewUrl: string;
  private AuthorInvalid = false;
  private checkMSG: string;

  constructor(private printingEditionService: PrintingEditionService, private authorService: AuthorService, private router: Router) {
    this.selectedAuthor = [];
  }

  async ngOnInit() {

    this.addAuthorForm = false;
    this.form = new FormGroup({
      publishingName: new FormControl(null, [Validators.required]),
      description: new FormControl(null, [Validators.required]),
      price: new FormControl(null, [Validators.required]),
      authorName: new FormControl(),
      authorBirthDay: new FormControl(),
      authorDeathDay: new FormControl()
    });
    this.authorName = '';
    this.authors = await this.authorService.GetAll();
    this.authors = this.authors.sort((a, b) => (a.name > b.name) ? 1 : ((b.name > a.name) ? -1 : 0));
  }

  public Create() {
    this.printingEdition.name = this.form.value.publishingName;
    this.printingEdition.description = this.form.value.description;
    this.printingEdition.price = this.form.value.price;
    this.printingEdition.currency = Number(this.currency);
    this.printingEdition.type = Number(this.type);
    this.printingEdition.status = Number(this.status);
    this.printingEdition.authorId = this.selectedAuthor;
    this.printingEdition.image = this.previewUrl;

    this.printingEditionService.Create(this.printingEdition).then(() => {
      this.router.navigate(['']).then(() => {
        window.location.reload();
      });
    });
  }

  public async AddAuthor() {
    this.authorName = this.authorName.trim();
    if (this.authorName === '' || !this.authorName) {
      this.checkMSG = 'Please enter a valid author name!';
      return;
    }

    const newAuthor: AuthorModel = {
      name: this.authorName
    };

    if ((new Date(this.authorDeathDay) <= new Date(this.authorBirthDay))) {
      this.checkMSG = 'Please check the date of birth and date of death of the author!';
      return false;
    }
    newAuthor.dateBirth = this.authorBirthDay;
    newAuthor.dateDeath = this.authorDeathDay;

    this.addAuthorForm = false;
    this.checkMSG = '';
    this.authorName = '';
    this.authorBirthDay = null;
    this.authorDeathDay = null;
    this.AuthorInvalid = false;
    const addedAuthor: AuthorModel = await this.authorService.AddAuthor(newAuthor);
    this.authors.unshift(addedAuthor);
  }

  private AddCover(event: Event) {
    const UploadImageInput: HTMLInputElement = event.target as HTMLInputElement;
    if (UploadImageInput && UploadImageInput.files.length) {
      this.fileData = UploadImageInput.files[0];
      const reader = new FileReader();
      reader.readAsDataURL(this.fileData);
      reader.onload = () => {
        this.previewUrl = reader.result.toString();
      };
    }
  }
}

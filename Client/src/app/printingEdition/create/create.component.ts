import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {PrintingEditionModel} from '../../shared/models/printing-edition.model';
import {PrintingEditionService} from '../../services/printingEdition.service';
import {AuthorService} from '../../services/author.service';
import {AuthorModel} from '../../shared/models/author.model';
import {Router} from '@angular/router';
import {environment} from '../../../environments/environment';


@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent implements OnInit {
  DefaultValue = '1';
  form: FormGroup;
  printingEdition: PrintingEditionModel = {};
  Currency = this.DefaultValue;
  Type = this.DefaultValue;
  Status = this.DefaultValue;
  AddAuthorForm: boolean;
  Authors: AuthorModel[];
  AuthorName: string;
  authorDeathDay: string;
  authorBirthDay: string;
  selectedAuthor: string[];
  private fileData: File;
  private previewUrl: string;
  private AuthorInvalid = false;
  private checkMSG: string;

  constructor(private printingEditionService: PrintingEditionService, private authorService: AuthorService, private router: Router) {
    this.selectedAuthor = [];
  }

  async ngOnInit() {

    this.AddAuthorForm = false;
    this.form = new FormGroup({
      publishingName: new FormControl(null, [Validators.required]),
      description: new FormControl(null, [Validators.required]),
      price: new FormControl(null, [Validators.required]),
      authorName: new FormControl(),
      authorBirthDay: new FormControl(),
      authorDeathDay: new FormControl()
    });
    this.AuthorName = '';
    this.Authors = await this.authorService.GetAll();
    this.Authors = this.Authors.sort((a, b) => (a.name > b.name) ? 1 : ((b.name > a.name) ? -1 : 0));
  }

  public Create() {
    this.printingEdition.name = this.form.value.publishingName;
    this.printingEdition.description = this.form.value.description;
    this.printingEdition.price = this.form.value.price;
    this.printingEdition.currency = Number(this.Currency);
    this.printingEdition.type = Number(this.Type);
    this.printingEdition.status = Number(this.Status);
    this.printingEdition.authorId = this.selectedAuthor;
    this.printingEdition.image = this.previewUrl;

    console.log(this.printingEdition);

    this.printingEditionService.Create(this.printingEdition).then(() => {this.router.navigate(['']).then(() => { window.location.reload(); } ); } );
  }

  public async AddAuthor() {

    this.AuthorName = this.AuthorName.trim();
    if (this.AuthorName === '' || !this.AuthorName) {
      this.checkMSG = 'Please enter a valid author name!';
      return;
    }

    const newAuthor: AuthorModel = {
      name: this.AuthorName
    };

    if ((new Date(this.authorDeathDay) <= new Date(this.authorBirthDay))) {
      this.checkMSG = 'Please check the date of birth and date of death of the author!';
      return false;
    }
    newAuthor.dateBirth = this.authorBirthDay;
    newAuthor.dateDeath = this.authorDeathDay;

    this.AddAuthorForm = false;
    this.checkMSG = '';
    this.AuthorName = '';
    this.authorBirthDay = null;
    this.authorDeathDay = null;
    this.AuthorInvalid = false;
    const addedAuthor: AuthorModel = await this.authorService.AddAuthor(newAuthor);
    this.Authors.unshift(addedAuthor);
  }

  AddCover(event: Event) {
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

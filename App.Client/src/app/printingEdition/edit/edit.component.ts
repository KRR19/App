import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {PrintingEditionModel} from '../../shared/models/printing-edition.model';
import {PrintingEditionService} from '../../services/printingEdition.service';
import {AuthorModel} from '../../shared/models/author.model';
import {AuthorService} from '../../services/author.service';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {
  private addAuthorForm: boolean;
  private authorName: string;
  private  authorBirthDay: string;
  private authorDeathDay: string;
  private currency: string;
  private type: string;
  private status: string;
  private printingEdition: PrintingEditionModel = {};
  private authors: AuthorModel[];
  private selectedAuthor: string[];
  private fileData: File;
  private authorInvalid = false;
  private checkMSG: string;
  private ErrorMSG: string;

  constructor(private router: Router, private route: ActivatedRoute, private printingEditionService: PrintingEditionService, private authorService: AuthorService, private  authService: AuthService) {
  }

  async ngOnInit() {
    if (!this.authService.isAdmin) {
      await this.router.navigate(['']);
    }
    this.addAuthorForm = false;
    let id: string;
    this.route.params.subscribe(params => id = params.id.slice(3));
    this.printingEdition = await this.printingEditionService.Get(id);
    this.currency = this.printingEdition.currency.toString();
    this.type = this.printingEdition.type.toString();
    this.status = this.printingEdition.status.toString();
    this.authors = await this.authorService.GetAll();
    this.authors = this.authors.sort((a, b) => (a.name < b.name) ? -1 : 1);
    this.selectedAuthor = this.printingEdition.authorId;

    if (this.printingEdition.image === 'no image') {
      this.printingEdition.image = 'assets/no-image-icon-10.png';
    }
    this.authorName = '';
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
    const dateBirth = new Date(this.authorBirthDay);
    const dateDeath = new Date(this.authorDeathDay);

    if (dateDeath < dateBirth) {
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
    this.authorInvalid = false;
    const addedAuthor: AuthorModel = await this.authorService.AddAuthor(newAuthor);
    this.authors.unshift(addedAuthor);
  }

  public async Edit() {
    this.printingEdition.name.trim();
    if (this.printingEdition.name === '') {
      this.ErrorMSG = 'Enter the title of the publication!';
      return false;
    }
    if (!this.printingEdition.price) {
      this.ErrorMSG = 'Enter the price of the publication!';
      return false;
    }
    this.printingEdition.currency = +this.currency;
    this.printingEdition.type = +this.type;
    this.printingEdition.status = +this.status;
    this.printingEdition.authorId = this.selectedAuthor;
    await this.printingEditionService.Update(this.printingEdition).then(() => {
       this.router.navigate(['']).then(() => {
         window.location.reload();
       });
     });
  }

  public async Delete() {
    await this.printingEditionService.Delete(this.printingEdition).then(() => {
      this.router.navigate(['']).then(() => {
        window.location.reload();
      });
    });
  }

  private AddCover(event: Event) {
    const UploadImageInput: HTMLInputElement = event.target as HTMLInputElement;
    if (UploadImageInput && UploadImageInput.files.length) {
      this.fileData = UploadImageInput.files[0];

      const reader = new FileReader();
      reader.readAsDataURL(this.fileData);
      reader.onload = () => {
        this.printingEdition.image = reader.result.toString();
      };
    }
  }
}

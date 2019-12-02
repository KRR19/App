import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {PrintingEditionModel} from '../../shared/models/printing-edition.model';
import {PrintingEditionService} from '../../services/printingEdition.service';
import {AuthorModel} from '../../shared/models/author.model';
import {AuthorService} from '../../services/author.service';
import {HeaderComponent} from '../../shared/header/header.component';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {
  AddAuthorForm: boolean;
  AuthorName: string;
  authorBirthDay: string;
  authorDeathDay: string;
  Currency: string;
  Type: string;
  Status: string;
  private printingEdition: PrintingEditionModel = {};
  Authors: AuthorModel[];
  selectedAuthor: string[];
  private fileData: File;
  private AuthorInvalid = false;
  private checkMSG: string;
  ErrorMSG: string;

  constructor(private router: Router, private route: ActivatedRoute, private printingEditionService: PrintingEditionService, private authorService: AuthorService, private  authService: AuthService) {
  }

  async ngOnInit() {
    if (!this.authService.isAdmin) {
      await this.router.navigate(['']);
    }
    this.AddAuthorForm = false;
    let id: string;
    this.route.params.subscribe(params => id = params.id.slice(3));
    this.printingEdition = await this.printingEditionService.Get(id);
    this.Currency = this.printingEdition.currency.toString();
    this.Type = this.printingEdition.type.toString();
    this.Status = this.printingEdition.status.toString();
    this.Authors = await this.authorService.GetAll();
    this.Authors = this.Authors.sort((a, b) => (a.name > b.name) ? 1 : ((b.name > a.name) ? -1 : 0));
    this.selectedAuthor = this.printingEdition.authorId;

    if(this.printingEdition.image === 'no image'){
      this.printingEdition.image = 'assets/no-image-icon-10.png'
    }
    this.AuthorName = '';
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
    this.printingEdition.currency = +this.Currency;
    this.printingEdition.type = +this.Type;
    this.printingEdition.status = +this.Status;
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

  AddCover(event: Event) {
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

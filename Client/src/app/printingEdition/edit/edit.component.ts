import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {PrintingEditionModel} from '../../shared/models/printing-edition.model';
import {PrintingEditionService} from '../../services/printingEdition.service';
import {AuthorModel} from '../../shared/models/author.model';
import {AuthorService} from '../../services/author.service';
import {FormGroup} from '@angular/forms';
import {HeaderComponent} from '../../shared/header/header.component';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {
  private isAdmin: boolean;
  AddAuthorForm: boolean;
  AuthorName: string;
  authorBirthDay: Date;
  authorDeathDay: Date;
  Currency: string;
  Type: string;
  Status: string;
  private printingEdition: PrintingEditionModel = {};
  Authors: AuthorModel[];
  selectedAuthor: string[];
  private fileData: File;
  private AuthorInvalid = false;
  private checkMSG: string;

  constructor(private router: Router, private route: ActivatedRoute, private printingEditionService: PrintingEditionService, private authorService: AuthorService, private  header: HeaderComponent) {
  }

  async ngOnInit() {
    if (!this.header.isAdmin) {
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
    const newAuthor: AuthorModel = {
      name: this.AuthorName,
      dateBirth: this.authorBirthDay.toString(),
      dateDeath: this.authorDeathDay.toString()
    };
    newAuthor.name = newAuthor.name.trim();
    if (newAuthor.name === '' || !newAuthor.name) {
      this.checkMSG = 'Please enter a valid author name!';
      return;
    }
    if (newAuthor.dateDeath !== '' && new Date(newAuthor.dateDeath) <= new Date(newAuthor.dateBirth)) {
      this.checkMSG = 'Please check the date of birth and date of death of the author!';
      return false;
    }

    if (newAuthor.dateDeath === '') {
      newAuthor.dateDeath = '0001-01-01';
    }

    if (newAuthor.dateBirth === '') {
      newAuthor.dateBirth = '0001-01-01';
    }

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

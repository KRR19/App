import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {PrintingEditionModel} from '../../shared/models/printing-edition.model';
import {PrintingEditionService} from '../../services/printingEdition.service';
import {AuthorModel} from '../../shared/models/author.model';
import {AuthorService} from '../../services/author.service';
import {FormGroup} from '@angular/forms';

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
  selectedAuthor: string;
  private fileData: File;

  constructor(private router: Router, private route: ActivatedRoute, private printingEditionService: PrintingEditionService, private authorService: AuthorService) {
  }

  async ngOnInit() {
    this.AdminCheck();
    if (!this.isAdmin) {
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
    console.log(this.printingEdition);
  }

  private AdminCheck() {
    this.isAdmin = localStorage.getItem('Role') === 'ADMIN';
    return this.isAdmin;
  }

  public async AddAuthor() {
    this.AddAuthorForm = false;
    const newAuthor = {name: this.AuthorName, dateBirth: this.authorBirthDay, dateDeath: this.authorDeathDay};
    this.AuthorName = '';
    this.authorBirthDay = null;
    this.authorDeathDay = null;
    const addedAuthor: AuthorModel = await this.authorService.AddAuthor(newAuthor);
    this.Authors.unshift(addedAuthor);
  }

  public async Edit() {
    this.printingEdition.currency = Number(this.Currency);
    this.printingEdition.type = Number(this.Type);
    this.printingEdition.status = Number(this.Status);
    this.printingEdition.authorId = this.selectedAuthor;
    await this.printingEditionService.Update(this.printingEdition).then(() => {this.router.navigate(['']).then(() => { window.location.reload(); } ); } );
  }

  public async Delete() {
    await this.printingEditionService.Delete(this.printingEdition).then(() => {this.router.navigate(['']).then(() => { window.location.reload(); } ); } );
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

import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {PrintingEditionModel} from '../../models/PrintingEditionModel';
import {PrintingEditionService} from '../shared/service/printingEdition.service';
import {AuthorService} from '../../author/shared/services/author.service';
import {AuthorModel} from '../../models/AuthorModel';
import {Router} from '@angular/router';


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
  newAuthorName: string;
  authorDeathDay: Date;
  authorBirthDay: Date;
  selectedAuthor: string;

  constructor(private printingEditionService: PrintingEditionService, private authorService: AuthorService, private router: Router) {
  }

  async ngOnInit() {

    this.AddAuthorForm = false;
    this.form = new FormGroup({
      publishingName: new FormControl(null, [Validators.required]),
      description: new FormControl(null, [Validators.required]),
      price: new FormControl(),
      authorName: new FormControl(),
      authorBirthDay: new FormControl(),
      authorDeathDay: new FormControl()
    });
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

    this.printingEditionService.Create(this.printingEdition).then(() => {this.router.navigate(['']).then(() => { window.location.reload(); } ); } );
  }

  public async AddAuthor() {
    this.AddAuthorForm = false;
    const newAuthor = {
      name: this.form.value.authorName,
      dateBirth: this.form.value.authorBirthDay,
      dateDeath: this.form.value.authorDeathDay
    };
    this.newAuthorName = '';
    this.authorBirthDay = null;
    this.authorDeathDay = null;
    const addedAuthor: AuthorModel = await this.authorService.AddAuthor(newAuthor);
    this.Authors.unshift(addedAuthor);
  }
}

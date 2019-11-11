import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {PrintingEditionModel} from '../../models/PrintingEditionModel';
import {PrintingEditionService} from '../shared/service/printingEdition.service';
import {AuthorModel} from '../../models/AuthorModel';
import {AuthorService} from '../shared/service/author.service';
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
    await this.printingEditionService.Update(this.printingEdition);
    await this.router.navigate(['']);
  }

  public async Delete() {
    await this.printingEditionService.Delete(this.printingEdition);
    await this.router.navigate(['']);
  }

}

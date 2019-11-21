import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {AuthorModel} from '../../shared/models/author.model';
import {AuthorService} from '../../services/author.service';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {HeaderComponent} from '../../shared/header/header.component';
import {log} from 'util';

@Component({
  selector: 'app-author-edit',
  templateUrl: './author-edit.component.html',
  styleUrls: ['./author-edit.component.scss']
})
export class AuthorEditComponent implements OnInit {
  private Author: AuthorModel = {};
  private isEdit = false;
  private DateBirth: string;
  private DateDeath: string;
  checkMSG: string;
  constructor(private route: ActivatedRoute, private router: Router, private authorService: AuthorService, private header: HeaderComponent) {
  }

  async ngOnInit() {
    let id: string;

    if (!this.header.isAdmin) {
      await this.router.navigate(['']);
    }

    this.route.params.subscribe(params => id = params.id.slice(3));
    if (id !== '') {
      this.isEdit = true;
      this.Author =  await this.authorService.GetById(id);
      this.DateBirth =  (this.Author.dateBirth.toString() === '0001-01-01T00:00:00') ? '' : this.Author.dateBirth.toString().slice(0, -9);
      this.DateDeath = (this.Author.dateDeath.toString() === '0001-01-01T00:00:00') ? '' : this.Author.dateDeath.toString().slice(0, -9);

    }



  }
  async AddAuthor() {
    this.Author.dateBirth = new Date(this.DateBirth);
    this.Author.dateDeath = new Date(this.DateDeath);
    this.Author.name = this.Author.name.trim();

    if (this.Author.dateDeath >= this.Author.dateBirth){
      this.checkMSG = 'Please check the date of birth and date of death of the author!';
      return;
    }
    if (this.Author.name === '') {
      this.checkMSG = 'Please enter a valid author name!';
      return;
    }

    await this.authorService.AddAuthor(this.Author).then(() => {this.router.navigate(['/author']).then(() => { window.location.reload(); } ); } );
  }

  async EditAuthor() {
    this.Author.dateBirth = new Date(this.DateBirth);
    this.Author.dateDeath = new Date(this.DateDeath);
    this.Author.name = this.Author.name.trim();

    if (this.Author.dateDeath >= this.Author.dateBirth) {
      this.checkMSG = 'Please check the date of birth and date of death of the author!';
      return;
    }
    if (this.Author.name === '') {
      this.checkMSG = 'Please enter a valid author name!';
      return;
    }
    await this.authorService.EditAuthor(this.Author).then(() => {this.router.navigate(['/author']).then(() => { this.ngOnInit(); } ); } );
  }

  DeleteAuthor() {
    this.authorService.Delete(this.Author).then(() => {this.router.navigate(['/author']).then(() => { window.location.reload(); } ); } );
  }
}

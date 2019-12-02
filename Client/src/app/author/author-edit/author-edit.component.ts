import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {AuthorModel} from '../../shared/models/author.model';
import {AuthorService} from '../../services/author.service';
import {HeaderComponent} from '../../shared/header/header.component';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'app-author-edit',
  templateUrl: './author-edit.component.html',
  styleUrls: ['./author-edit.component.scss']
})
export class AuthorEditComponent implements OnInit {
  private Author: AuthorModel = {};
  private isEdit = false;
  checkMSG: string;
  constructor(private route: ActivatedRoute, private router: Router, private authorService: AuthorService, private header: HeaderComponent, private authService: AuthService) {
  }

  async ngOnInit() {
    let id: string;

    if (!this.authService.isAdmin) {
      await this.router.navigate(['']);
    }
    this.Author.name = '';
    this.Author.dateBirth = '';
    this.Author.dateDeath = '';
    this.route.params.subscribe(params => id = params.id.slice(3));
    if (id) {
      this.isEdit = true;
      this.Author =  await this.authorService.GetById(id);
      this.Author.dateBirth = this.Author.dateBirth.slice(0, -9);
      this.Author.dateDeath = this.Author.dateDeath.slice(0, -9);
    }
  }
  async AddAuthor() {
    if (this.AuthorCheck()) {
       await this.authorService.AddAuthor(this.Author).then(() => {this.router.navigate(['/author']).then(() => { window.location.reload(); } ); } );
    }
  }

  async EditAuthor() {
    if (this.AuthorCheck()) {
      await this.authorService.EditAuthor(this.Author).then(() => {
        this.router.navigate(['/author']);
      });
    }
  }

  DeleteAuthor() {
    this.authorService.Delete(this.Author).then(() => {this.router.navigate(['/author']).then(() => { window.location.reload(); } ); } );
  }

  AuthorCheck(): boolean {
    this.Author.name = this.Author.name.trim();
    if (!this.Author.name) {
      this.checkMSG = 'Please enter a valid author name!';
      return false;
    }
    const dateBirth = new Date(this.Author.dateBirth)
    const dateDeath = new Date(this.Author.dateDeath);

    if (dateDeath < dateBirth) {
      this.checkMSG = 'Please check the date of birth and date of death of the author!';
      return false;
    }

    if (this.Author.dateDeath === '') {
    this.Author.dateDeath = '0001-01-01';
  }
    if (this.Author.dateBirth === '') {
      this.Author.dateBirth = '0001-01-01';
    }
    return  true;
  }
}

import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {AuthorModel} from '../../models/AuthorModel';
import {AuthorService} from '../shared/services/author.service';
import {MatDatepickerModule} from '@angular/material/datepicker';

@Component({
  selector: 'app-author-edit',
  templateUrl: './author-edit.component.html',
  styleUrls: ['./author-edit.component.scss']
})
export class AuthorEditComponent implements OnInit {
  Author: AuthorModel = {};
  isEdit = false;
  constructor(private route: ActivatedRoute, private router: Router, private authorService: AuthorService) {
  }

  async ngOnInit() {
    let id: string;

    if (!(localStorage.getItem('Role') === 'ADMIN')) {
      await this.router.navigate(['']);
    }

    this.route.params.subscribe(params => id = params.id.slice(3));
    if (id !== '') {
      this.isEdit = true;
      this.Author =  await this.authorService.GetById(id);
    }
  }

  async AddAuthor() {
    await this.authorService.AddAuthor(this.Author).then(() => {this.router.navigate(['/author']).then(() => { window.location.reload(); } ); } );
  }

  async EditAuthor() {
    await this.authorService.EditAuthor(this.Author).then(() => {this.router.navigate(['/author']).then(() => { this.ngOnInit(); } ); } );
  }

  DeleteAuthor() {
    this.authorService.Delete(this.Author).then(() => {this.router.navigate(['/author']).then(() => { window.location.reload(); } ); } );
  }
}

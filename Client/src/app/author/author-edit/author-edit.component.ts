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
  private author: AuthorModel = {};
  private isEdit = false;
  private checkMSG: string;

  constructor(private route: ActivatedRoute, private router: Router, private authorService: AuthorService, private header: HeaderComponent, private authService: AuthService) {
  }

  async ngOnInit() {
    let id: string;

    if (!this.authService.isAdmin) {
      await this.router.navigate(['']);
    }
    this.author.name = '';
    this.author.dateBirth = '';
    this.author.dateDeath = '';
    this.route.params.subscribe(params => id = params.id.slice(3));
    if (id) {
      this.isEdit = true;
      this.author = await this.authorService.GetById(id);
      this.author.dateBirth = this.author.dateBirth.slice(0, -9);
      this.author.dateDeath = this.author.dateDeath.slice(0, -9);
    }
  }

  private async AddAuthor() {
    if (this.AuthorCheck()) {
      await this.authorService.AddAuthor(this.author).then(() => {
        this.router.navigate(['/author']).then(() => {
          window.location.reload();
        });
      });
    }
  }

  private async EditAuthor() {
    if (this.AuthorCheck()) {
      await this.authorService.EditAuthor(this.author).then(() => {
        this.router.navigate(['/author']);
      });
    }
  }

  private DeleteAuthor() {
    this.authorService.Delete(this.author).then(() => {
      this.router.navigate(['/author']).then(() => {
        window.location.reload();
      });
    });
  }

  private AuthorCheck(): boolean {
    this.author.name = this.author.name.trim();
    if (!this.author.name) {
      this.checkMSG = 'Please enter a valid author name!';
      return false;
    }
    const dateBirth = new Date(this.author.dateBirth);
    const dateDeath = new Date(this.author.dateDeath);

    if (dateDeath < dateBirth) {
      this.checkMSG = 'Please check the date of birth and date of death of the author!';
      return false;
    }

    if (this.author.dateDeath === '') {
      this.author.dateDeath = '0001-01-01';
    }
    if (this.author.dateBirth === '') {
      this.author.dateBirth = '0001-01-01';
    }
    return true;
  }
}

import {Component, OnInit, ViewChild} from '@angular/core';
import {AuthorModel} from '../../shared/models/author.model';
import {AuthorService} from '../../services/author.service';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';

@Component({
  selector: 'app-author-dashboard',
  templateUrl: './author-dashboard.component.html',
  styleUrls: ['./author-dashboard.component.scss']
})
export class AuthorDashboardComponent implements OnInit {

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  Authors: AuthorModel[] = [];
  public displayedColumns: string[] = ['Name', 'DateBirth', 'DateDeath'];
  public dataSource = new MatTableDataSource<AuthorModel>();

  constructor(private authorService: AuthorService) {
  }

  async ngOnInit() {
    this.Authors = await this.authorService.GetAll();
    for (let i = 0; i < this.Authors.length; i++) {
      if (this.Authors[i].dateDeath === '0001-01-01T00:00:00') {
        this.Authors[i].dateDeath = '';
      }
      if (this.Authors[i].dateBirth === '0001-01-01T00:00:00') {
         this.Authors[i].dateBirth = '';
       }
    }
    this.Authors = this.Authors.sort((a, b) => (a.name > b.name) ? 1 : ((b.name > a.name) ? -1 : 0));
    this.dataSource = new MatTableDataSource<AuthorModel>(this.Authors);
    this.dataSource.paginator = this.paginator;
  }

  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  }
}

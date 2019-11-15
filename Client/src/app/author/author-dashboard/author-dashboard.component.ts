import {Component, OnInit, ViewChild} from '@angular/core';
import {AuthorModel} from '../../models/AuthorModel';
import {AuthorService} from '../shared/services/author.service';
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

  constructor(private authorservice: AuthorService) {
  }

  async ngOnInit() {

    this.Authors = await this.authorservice.GetAll();
    this.Authors = this.Authors.sort((a, b) => (a.name > b.name) ? 1 : ((b.name > a.name) ? -1 : 0));
    this.dataSource = new MatTableDataSource<AuthorModel>(this.Authors);
    this.dataSource.paginator = this.paginator;
    console.log(this.Authors);
  }

  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  }

}

import {Component, OnInit, ViewChild} from '@angular/core';
import {PrintingEditionService} from '../printingEdition/shared/service/printingEdition.service';
import {PrintingEditionModel} from '../models/PrintingEditionModel';
import {MatPaginator, MatTableDataSource} from '@angular/material';
import {Router} from '@angular/router';

@Component({
  selector: 'app-dash-board',
  templateUrl: './dash-board.component.html',
  styleUrls: ['./dash-board.component.scss']
})
export class DashBoardComponent implements OnInit {
  printingEdition: PrintingEditionModel[];
  displayedColumns: string[] = ['Name', 'Price'];
  dataSource = new MatTableDataSource<PrintingEditionModel>();

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  constructor(private printingEditionService: PrintingEditionService, private router: Router) { }

 async ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.printingEdition = await this.printingEditionService.GetAll();
    console.log(this.printingEdition);
    this.dataSource = new MatTableDataSource<PrintingEditionModel>(this.printingEdition);
  }

  OpenPrintingEdition(id: string) {
    const adress: string = 'printingEdition/:id' + id;
    this.router.navigate([adress]);
  }



}

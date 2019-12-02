import {Component, OnInit, ViewChild} from '@angular/core';
import {PrintingEditionService} from '../services/printingEdition.service';
import {PrintingEditionModel} from '../shared/models/printing-edition.model';
import {MatPaginator, MatTableDataSource} from '@angular/material';
import {Router} from '@angular/router';
import {CartService} from '../services/cart.service';

@Component({
  selector: 'app-dash-board',
  templateUrl: './dash-board.component.html',
  styleUrls: ['./dash-board.component.scss']
})
export class DashBoardComponent implements OnInit {
  private printingEdition: PrintingEditionModel[];
  public displayedColumns: string[] = ['Name', 'Price'];
  public dataSource = new MatTableDataSource<PrintingEditionModel>();

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  constructor(private printingEditionService: PrintingEditionService, private router: Router, private cartService: CartService) {
  }

  public async ngOnInit() {
    this.printingEdition = await this.printingEditionService.GetAll();
    this.dataSource = new MatTableDataSource<PrintingEditionModel>(this.printingEdition);
    this.dataSource.paginator = this.paginator;
  }

  public OpenPrintingEdition(id: string) {
    const adress: string = 'printing-edition/:id' + id;
    this.router.navigate([adress]);
  }

  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  };

}

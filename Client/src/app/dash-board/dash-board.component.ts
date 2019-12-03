import {Component, OnInit, ViewChild} from '@angular/core';
import {PrintingEditionService} from '../services/printingEdition.service';
import {PrintingEditionModel} from '../shared/models/printing-edition.model';
import {MatPaginator, MatTableDataSource} from '@angular/material';
import {Router} from '@angular/router';
import {CartService} from '../services/cart.service';
import {AuthorModel} from '../shared/models/author.model';
import {AuthorService} from '../services/author.service';
import {FilterModel} from '../shared/models/filter.model';

@Component({
  selector: 'app-dash-board',
  templateUrl: './dash-board.component.html',
  styleUrls: ['./dash-board.component.scss']
})
export class DashBoardComponent implements OnInit {
  private printingEdition: PrintingEditionModel[];
  private minFilterPrice: number;
  private maxFilterPrice: number;
  private selectedAuthor: string[];
  private allAuthors: AuthorModel[];
  private filtredModel: FilterModel = {authorId: []};
  private errorMsg: string;
  public displayedColumns: string[] = ['Name', 'Author', 'Price'];
  public dataSource = new MatTableDataSource<PrintingEditionModel>();

  private priceFilterMsg = 'minimum price cannot be higher than maximum';

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  constructor(private printingEditionService: PrintingEditionService, private router: Router, private cartService: CartService, private authorService: AuthorService) {
  }

  public async ngOnInit() {
    this.printingEdition = await this.printingEditionService.GetAll();
    this.allAuthors = await this.authorService.GetAll();
    this.dataSource = new MatTableDataSource<PrintingEditionModel>(this.printingEdition);
    this.dataSource.paginator = this.paginator;
  }

  public OpenPrintingEdition(id: string) {
    const adress: string = 'printing-edition/:id' + id;
    this.router.navigate([adress]);
  }

  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  }

  private filtred() {
    let min: number = this.minFilterPrice;
    let max: number = this.maxFilterPrice;

    if (!min) {
      min = 0;
    }
    if (!max) {
      max = Number.MAX_SAFE_INTEGER;
    }

    if (min > max) {
      this.errorMsg = this.priceFilterMsg;
      return;
    }

    if (!this.selectedAuthor) {

      this.filtredModel.authorId.push('all');
    } else {
      this.filtredModel.authorId = this.selectedAuthor;
    }

    this.filtredModel.minPrice = min;
    this.filtredModel.maxPrice = max;

    this.printingEditionService.Filter(this.filtredModel);

  }


  // private FiltredPrice() {
  //   let min: number = this.minFilterPrice;
  //   let max: number = this.maxFilterPrice;
  //   if (min > max) {
  //     this.errorMsg = this.priceFilterMsg;
  //     return;
  //   }
  //   if (!min) {
  //     min = 0;
  //   }
  //   if (!max) {
  //     max = Number.MAX_SAFE_INTEGER;
  //   }
  //   this.filtredData = this.filtredData.filter(x => min <= x.price && x.price <= max);
  // }
  //
  //
  // FiltredAuthor() {
  //   if (!this.selectedAuthor) {
  //     return;
  //   }
  //   const filtredByName: PrintingEditionModel[] = [];
  //   for (const selectd of this.selectedAuthor) {
  //     for (const printingEdition of this.filtredData) {
  //       for (const author of printingEdition.authorId) {
  //         if (selectd == author) {
  //           filtredByName.push(printingEdition);
  //           break;
  //         }
  //       }
  //     }
  //   }
  //   this.filtredData = filtredByName;
  // }
}

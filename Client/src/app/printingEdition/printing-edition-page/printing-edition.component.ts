import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {PrintingEditionService} from '../shared/service/printingEdition.service';
import {PrintingEditionModel} from '../../models/PrintingEditionModel';


@Component({
  selector: 'app-printing-edition-page',
  templateUrl: './printing-edition.component.html',
  styleUrls: ['./printing-edition.component.scss']
})
export class PrintingEditionComponent implements OnInit {
  printingEdition: PrintingEditionModel = {};
  isAdmin: boolean;

  constructor(private route: ActivatedRoute, private router: Router, private  printingEditionService: PrintingEditionService) {
  }

  async ngOnInit() {
    this.AdminCheck();
    let id: string;
    this.route.params.subscribe(params => id = params.id.slice(3));
    this.printingEdition = await this.printingEditionService.Get(id);
    console.log(this.printingEdition);
  }

  private AdminCheck() {
    this.isAdmin = localStorage.getItem('Role') === 'ADMIN';
  }

  public EditPage() {
    this.router.navigate(['/printingEdition/edit/:id' + this.printingEdition.id]);
  }

  AddCart() {
    localStorage.setItem('Cart', this.printingEdition.id);
    const s: string = JSON.stringify(this.printingEdition);
    console.log('JSON: ' + s);
  }
}

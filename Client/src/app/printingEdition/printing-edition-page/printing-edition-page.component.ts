import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {PrintingEditionService} from '../shared/service/printingEdition.service';
import {PrintingEditionModel} from '../../models/PrintingEditionModel';
import {AuthorService} from '../shared/service/author.service';

@Component({
  selector: 'app-printing-edition-page',
  templateUrl: './printing-edition-page.component.html',
  styleUrls: ['./printing-edition-page.component.scss']
})
export class PrintingEditionPageComponent implements OnInit {
  printingEdition: PrintingEditionModel = {};
  isAdmin: boolean;
  authors: string;
  constructor(private route: ActivatedRoute, private router: Router, private  printingEditionService: PrintingEditionService, private AuthorServices: AuthorService) { }

  async ngOnInit() {
    this.AdminCheck();
    let id: string;
    this.route.params.subscribe(params => id = params.id.slice(3));
    this.printingEdition = await this.printingEditionService.Get(id);
    console.log(this.printingEdition);

    console.log('Тут ошибка: ' + this.printingEdition.authorName);
    console.log('Тут нормально: ' + this.printingEdition.authorId);
  }

  private AdminCheck() {
    this.isAdmin = localStorage.getItem('Role') === 'admin';
  }

  EditPage() {
    this.router.navigate(['/printingEdition/edit/:id' + this.printingEdition.id]);
  }
}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {CreateComponent} from './create/create.component';
import {PrintingEditionComponent} from './printing-edition-page/printing-edition.component';
import {EditComponent} from './edit/edit.component';

const printingEditionRoutes: Routes = [
  {
    path: '', children: [
      {path: '', redirectTo: '/printing-edition/create', pathMatch: 'full'},
      {path: 'create', component: CreateComponent},
      {path: ':id', component: PrintingEditionComponent},
      {path: 'edit/:id', component: EditComponent}
    ]
  }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(printingEditionRoutes)
  ],
  exports: [RouterModule]
})
export class PrintingEditionRoutingModule { }

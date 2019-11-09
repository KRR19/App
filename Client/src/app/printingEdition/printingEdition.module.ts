import {NgModule} from '@angular/core';
import {CreateComponent} from './create/create.component';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {
  MatButtonModule,
  MatFormFieldModule,
  MatInputModule,
  MatListModule,
  MatOptionModule,
  MatSelectModule,
  MatTabsModule
} from '@angular/material';
import {PrintingEditionService} from './shared/service/printingEdition.service';
import {AuthorService} from './shared/service/author.service';
import { PrintingEditionPageComponent } from './printing-edition-page/printing-edition-page.component';
import { EditComponent } from './edit/edit.component';



@NgModule({
  declarations: [CreateComponent, PrintingEditionPageComponent, EditComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '', children: [
          {path: '', redirectTo: '/printingEdition/create', pathMatch: 'full'},
          {path: 'create', component: CreateComponent},
          {path: ':id', component: PrintingEditionPageComponent},
          {path: 'edit/:id', component: EditComponent}
        ]
      }
    ]),
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    MatTabsModule,
    MatOptionModule,
    MatSelectModule,
    MatListModule
  ],
    exports: [RouterModule],
    providers: [PrintingEditionService, AuthorService]
})

export class PrintingEditionModule {

}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthorDashboardComponent } from './author-dashboard/author-dashboard.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {SharedModule} from '../shared/shared.modules';
import {RouterModule} from '@angular/router';
import {AuthorService} from './shared/services/author.service';
import {MatDatepickerModule, MatFormFieldModule, MatNativeDateModule, MatPaginatorModule, MatTableModule} from '@angular/material';
import { AuthorEditComponent } from './author-edit/author-edit.component';
import { MatInputModule } from '@angular/material';




@NgModule({
  declarations: [AuthorDashboardComponent, AuthorEditComponent, ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    RouterModule.forChild([
      {path: '', redirectTo: '/author/author-dashboard', pathMatch: 'full'},
      {path: 'author-dashboard', component: AuthorDashboardComponent},
      {path: ':id', component: AuthorEditComponent}
    ]),
    MatTableModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  providers: [AuthorService],
  exports: [MatPaginatorModule, MatFormFieldModule,
    MatInputModule]
})
export class AuthorModule { }

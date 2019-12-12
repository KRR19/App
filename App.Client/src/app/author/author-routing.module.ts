import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AuthorDashboardComponent} from './author-dashboard/author-dashboard.component';
import {AuthorEditComponent} from './author-edit/author-edit.component';
import {RouterModule, Routes} from '@angular/router';

const authorRoutes: Routes = [
  {path: '', redirectTo: '/author/author-dashboard', pathMatch: 'full'},
  {path: 'author-dashboard', component: AuthorDashboardComponent},
  {path: ':id', component: AuthorEditComponent}
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(authorRoutes)
  ],
  exports: [RouterModule]
})
export class AuthorRoutingModule {
}

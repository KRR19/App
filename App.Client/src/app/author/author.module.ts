import {NgModule, Provider} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AuthorDashboardComponent} from './author-dashboard/author-dashboard.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {SharedModule} from '../shared/shared.modules';
import {AuthorService} from '../services/author.service';
import {MatDatepickerModule, MatFormFieldModule, MatNativeDateModule, MatPaginatorModule, MatTableModule} from '@angular/material';
import {AuthorEditComponent} from './author-edit/author-edit.component';
import {MatInputModule} from '@angular/material';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {Interceptor} from '../core/interceptor';
import {AuthorRoutingModule} from './author-routing.module';

const INTERCEPTOR_PROVIDER: Provider = {
  provide: HTTP_INTERCEPTORS,
  useClass: Interceptor,
  multi: true
};

@NgModule({
  declarations: [AuthorDashboardComponent, AuthorEditComponent,],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    AuthorRoutingModule,
    MatTableModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  providers: [INTERCEPTOR_PROVIDER, AuthorService],
  exports: [MatPaginatorModule, MatFormFieldModule,
    MatInputModule]
})
export class AuthorModule {
}

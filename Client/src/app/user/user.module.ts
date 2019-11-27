import {NgModule, Provider} from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersDashboardComponent } from './users-dashboard/users-dashboard.component';
import {RouterModule} from '@angular/router';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {
  MatButtonModule, MatButtonToggleModule,
  MatFormFieldModule,
  MatInputModule,
  MatListModule,
  MatOptionModule, MatPaginatorModule,
  MatSelectModule, MatTableModule,
  MatTabsModule
} from '@angular/material';
import {UsersService} from '../services/users.service';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {Interceptor} from '../core/interceptor';
import {ErrorRouteComponent} from '../error-route/error-route.component';

const INTERCEPTOR_PROVIDER: Provider = {
  provide: HTTP_INTERCEPTORS,
  useClass: Interceptor,
  multi: true
}

@NgModule({
  declarations: [UsersDashboardComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '', children: [
          {path: '', redirectTo: '/user/dashboard', pathMatch: 'full'},
          {path: 'dashboard', component: UsersDashboardComponent}
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
    MatListModule,
    MatButtonToggleModule,
    MatTableModule,
    MatPaginatorModule
  ],
  exports: [RouterModule],
  providers: [INTERCEPTOR_PROVIDER, UsersService]
})
export class UserModule { }

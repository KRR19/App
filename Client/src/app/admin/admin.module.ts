import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {LoginPageComponent} from './login-page/login-page.component';
import {DashboardPageComponent} from './dashboard-page/dashboard-page.component';
import {CreatePageComponent} from './create-page/create-page.component';
import {EditPageComponent} from './edit-page/edit-page.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {AuthService} from '../auth/shared/services/auth.service';
import {SharedModule} from '../shared/shared.modules';
import {UsersComponent} from './users/users.component';

@NgModule({
  declarations: [
    LoginPageComponent,
    DashboardPageComponent,
    CreatePageComponent,
    EditPageComponent,
    UsersComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    RouterModule.forChild([

      {path: '', redirectTo: '/admin/users', pathMatch: 'full'},
      {path: 'users', component: UsersComponent}

    ]),
    FormsModule
  ],
  exports: [RouterModule],
  providers: [AuthService]
})
export class AdminModule {

}

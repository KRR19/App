import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {SingupComponent} from './singup/singup.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatButtonModule, MatFormFieldModule, MatInputModule, MatTabsModule} from '@angular/material';
import { AuthLayoutComponent } from './shared/components/auth-layout/auth-layout.component';
import {LoginPageComponent} from '../admin/login-page/login-page.component';

@NgModule({
  declarations: [ SingupComponent, AuthLayoutComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path: '', component: AuthLayoutComponent, children: [
          {path: '', redirectTo: '/auth/SingUp', pathMatch: 'full'},
        //  {path: 'login', component: LoginPageComponent},
          {path: 'SingUp', component: SingupComponent},
        ]}
    ]),
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    MatTabsModule
  ],
  exports: [RouterModule]
})

export class AuthModule {

}

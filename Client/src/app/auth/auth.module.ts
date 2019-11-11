import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {SingupComponent} from './singup/singup.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatButtonModule, MatFormFieldModule, MatInputModule, MatTabsModule} from '@angular/material';
import {AuthService} from './shared/services/auth.service';
import {SinginComponent} from './singin/singin.component';
import {ForgotPasswordComponent} from './forgot-password/forgot-password.component';


@NgModule({
  declarations: [SingupComponent, SinginComponent, ForgotPasswordComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '', children: [
          {path: '', redirectTo: '/auth/SingIn', pathMatch: 'full'},
          {path: 'SingIn', component: SinginComponent},
          {path: 'SingUp', component: SingupComponent},
          {path: 'ForgotPassword', component: ForgotPasswordComponent},
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
  ],
  exports: [RouterModule],
  providers: [AuthService]
})

export class AuthModule {

}

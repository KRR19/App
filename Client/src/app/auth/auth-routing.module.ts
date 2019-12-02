import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {SinginComponent} from './singin/singin.component';
import {SingupComponent} from './singup/singup.component';
import {ForgotPasswordComponent} from './forgot-password/forgot-password.component';

const authRoutes: Routes =[
  {
    path: '', children: [
      {path: '', redirectTo: '/auth/SingIn', pathMatch: 'full'},
      {path: 'SingIn', component: SinginComponent},
      {path: 'SingUp', component: SingupComponent},
      {path: 'ForgotPassword', component: ForgotPasswordComponent}
    ]
  }
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(authRoutes)
  ],
  exports: [RouterModule]
})
export class AuthRoutingModule { }

import {NgModule, Provider} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {SingupComponent} from './singup/singup.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatButtonModule, MatFormFieldModule, MatInputModule, MatTabsModule} from '@angular/material';
import {AuthService} from '../services/auth.service';
import {SinginComponent} from './singin/singin.component';
import {ForgotPasswordComponent} from './forgot-password/forgot-password.component';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {Interceptor} from '../core/interceptor';
import {AuthRoutingModule} from './auth-routing.module';

const INTERCEPTOR_PROVIDER: Provider = {
  provide: HTTP_INTERCEPTORS,
  useClass: Interceptor,
  multi: true
};

@NgModule({
  declarations: [SingupComponent, SinginComponent, ForgotPasswordComponent],
  imports: [
    CommonModule,
    AuthRoutingModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    MatTabsModule,
  ],
  exports: [RouterModule],
  providers: [INTERCEPTOR_PROVIDER, AuthService]
})

export class AuthModule {

}

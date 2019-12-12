import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {AuthService} from '../../services/auth.service';
import {ResetPasswordModel} from '../../shared/models/reset-password.model';
import {ResponseModel} from '../../shared/models/response.model';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {
  private form: FormGroup;
  private passwordsMatch = true;
  private success: boolean;
  private message: string[];

  constructor(private auth: AuthService) {
  }

  ngOnInit() {
    this.success = false;
    this.passwordsMatch = true;
    this.form = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, [Validators.required, Validators.minLength(6)]),
      confirmPassword: new FormControl(null, [Validators.required, Validators.minLength(6)]),
    });
  }

  public async ForgotPassword() {
    if (this.form.value.password !== this.form.value.confirmPassword) {
      this.passwordsMatch = false;
      return;
    }
    const model: ResetPasswordModel = {
      email: this.form.value.email,
      password: this.form.value.password,
      confirmPassword: this.form.value.confirmPassword
    };
    const response: ResponseModel = await this.auth.forgotPassword(model);
    this.auth.logout();

    this.success = response.isValid;
    this.message = response.message;
  }
}

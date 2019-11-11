import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {AuthService} from '../shared/services/auth.service';
import {ResetPasswordModel} from '../../models/ResetPasswordModel';
import {Responsemodel} from '../../models/Responsemodel';

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
      ConfirmPassword: new FormControl(null, [Validators.required, Validators.minLength(6)]),
    });
  }

  public async ForgotPassword() {
    if (this.form.value.password !== this.form.value.ConfirmPassword) {
      this.passwordsMatch = false;
      return;
    }
    const model: ResetPasswordModel = {
      Email: this.form.value.email,
      Password: this.form.value.password,
      ConfirmPassword: this.form.value.ConfirmPassword
    };
    const response: Responsemodel = await this.auth.forgotPassword(model);
    this.auth.logout();
    this.success = response.isValid;
    this.message = response.message;
  }
}

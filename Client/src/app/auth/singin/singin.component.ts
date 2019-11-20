import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {UserModel} from '../../shared/models/user.model';
import {AuthService} from '../../services/auth.service';
import {LogInResponceModel} from '../../shared/models/logIn-responce.model';
import {Router} from '@angular/router';
import {HeaderComponent} from '../../shared/header/header.component';

@Component({
  selector: 'app-singin',
  templateUrl: './singin.component.html',
  styleUrls: ['./singin.component.scss']
})
export class SinginComponent implements OnInit {
  form: FormGroup;
  success: boolean;
  message: string[];

  constructor(private  auth: AuthService, private  router: Router, private header: HeaderComponent) {
  }

  ngOnInit() {
    this.form = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, [Validators.required, Validators.minLength(6)]),
    });
  }

  public async SingIn() {
    const user: UserModel = {email: this.form.value.email, password: this.form.value.password};
    const response: LogInResponceModel = await this.auth.SingIn(user);
    this.success = !response.isValid;
    this.message = response.message;

    if (response.isValid) {
      this.header.ngOnInit();
      this.router.navigate(['']);
    }
  }
}

import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {UserModel} from '../../models/UserModel';
import {AuthService} from '../shared/services/auth.service';
import {LogInResponceModel} from '../../models/LogInResponceModel';
import {Router} from '@angular/router';

@Component({
  selector: 'app-singin',
  templateUrl: './singin.component.html',
  styleUrls: ['./singin.component.scss']
})
export class SinginComponent implements OnInit {
  form: FormGroup;
  success: boolean;
  message: string[];


  constructor(private  auth: AuthService, private  router: Router) { }

  ngOnInit() {
    this.form = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, [Validators.required, Validators.minLength(6)]),
    });
  }

  async submit() {
    const  user: UserModel = { Email: this.form.value.email, Password: this.form.value.password};
    const response: LogInResponceModel = await this.auth.SingIn(user);
    this.success = !response.isValid;
    this.message = response.message;

    if (response.isValid) {
      this.router.navigate(['']);
    }
  }
}

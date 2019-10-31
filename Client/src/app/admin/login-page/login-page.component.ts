import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {validate} from 'codelyzer/walkerFactory/walkerFn';
import {AuthService} from '../shared/services/auth.service';
import {Router} from '@angular/router';
import {UserModel} from '../../models/UserModel';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {
  form: FormGroup

  constructor(private auth: AuthService, private router: Router) { }

  ngOnInit() {
    this.form = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, [Validators.required, Validators.minLength(6)]),
    });
  }

  submit() {
    if (this.form.invalid) {
      return;
    }

    const  user: UserModel = {Email: this.form.value.email, Password: this.form.value.password, FirstName: '', SecondName: ''};
    console.log(user);
    // this.auth.login(user).subscribe(() => {this.form.reset(), this.router.navigate(['admin', 'dashboard']); });
  }
}

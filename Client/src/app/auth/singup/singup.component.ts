import { Component, OnInit } from '@angular/core';
import {UserModel} from '../../models/UserModel';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {AuthService} from '../shared/services/auth.service';
import {Responsemodel} from '../../models/Responsemodel';

@Component({
  selector: 'app-singup',
  templateUrl: './singup.component.html',
  styleUrls: ['./singup.component.scss']
})
export class SingupComponent implements OnInit {
  form: FormGroup;
  FormVisible: boolean;
  successReg: boolean;
  unsuccessReg: boolean;
  constructor(private auth: AuthService, private router: Router) { }

  ngOnInit() {
    this.FormVisible = true;
    this.successReg = false;
    this.unsuccessReg = false;

    this.form = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, [Validators.required, Validators.minLength(6)]),
      firstName: new FormControl(null, [Validators.required]),
      secondName: new FormControl(null, [Validators.required])
    });
  }

  async submit() {
    this.FormVisible = true;
    this.successReg = false;
    this.unsuccessReg = false;

    if (this.form.invalid) {
      return;
    }

    const  user: UserModel = {Email: this.form.value.email, Password: this.form.value.password,
                              FirstName: this.form.value.firstName, SecondName: this.form.value.secondName};
    const result: Responsemodel =  await this.auth.SingUp(user);

    if (!result.isValid) {
     this.unsuccessReg = true;
     return;
    }
    this.FormVisible = false;
    this.successReg = true;
  }
}

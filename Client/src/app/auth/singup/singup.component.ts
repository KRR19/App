import {Component, OnInit} from '@angular/core';
import {UserModel} from '../../shared/models/user.model';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {AuthService} from '../../services/auth.service';
import {ResponseModel} from '../../shared/models/response.model';

@Component({
  selector: 'app-singup',
  templateUrl: './singup.component.html',
  styleUrls: ['./singup.component.scss']
})
export class SingupComponent implements OnInit {
  form: FormGroup;
  FormVisible: boolean;
  successReg: boolean;
  registrationError: boolean;

  constructor(private auth: AuthService, private router: Router) {
  }

  ngOnInit() {
    this.FormVisible = true;
    this.successReg = false;
    this.registrationError = false;

    this.form = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, [Validators.required, Validators.minLength(6)]),
      firstName: new FormControl(null, [Validators.required]),
      secondName: new FormControl(null, [Validators.required])
    });
  }

  public async SingUp() {
    this.FormVisible = true;
    this.successReg = false;
    this.registrationError = false;

    if (this.form.invalid) {
      return;
    }

    const user: UserModel = {
      email: this.form.value.email, password: this.form.value.password,
      firstName: this.form.value.firstName, lastName: this.form.value.secondName
    };

    const result: ResponseModel = await this.auth.SingUp(user);

    if (!result.isValid) {
      this.registrationError = true;
      return;
    }
    this.FormVisible = false;
    this.successReg = true;
  }
}

import { Component, OnInit } from '@angular/core';
import {UserModel} from '../../models/UserModel';

@Component({
  selector: 'app-singup',
  templateUrl: './singup.component.html',
  styleUrls: ['./singup.component.scss']
})
export class SingupComponent implements OnInit {
  user: UserModel

  EmailValue: string
  PassValue: string
  FirstName: string
  SecondName: string

  constructor() { }

  ngOnInit() {
  }

  SingUpEvnt() {
    this.user.Email = this.EmailValue
    this.user.Password = this.PassValue
    this.user.FirstName = this.FirstName
    this.user.SecondName = this.SecondName
    console.log(this.user);
  }
}

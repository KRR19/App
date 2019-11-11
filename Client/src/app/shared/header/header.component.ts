import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../auth/shared/services/auth.service';
import {Router} from '@angular/router';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  Auth: boolean;
  isAdmin: boolean;

  constructor(private  auth: AuthService, private  router: Router) { }

  ngOnInit() {
    this.isAdmin = localStorage.getItem('Role') === 'ADMIN';
    this.Auth = false;
    if (localStorage.getItem('accessToken')) {
      this.Auth = true;
    }
  }

  logout() {
    this.auth.logout();
    this.Auth = !true;
    this.router.navigate(['']);
  }
}

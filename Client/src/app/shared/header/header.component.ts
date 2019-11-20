import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';
import {CartModel} from '../models/cart.model';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  Auth: boolean;
  isAdmin: boolean;
  CartCount: number;
  User: string;

  constructor(private  auth: AuthService, private  router: Router) {
  }

  ngOnInit() {
    this.isAdmin = localStorage.getItem('Role') === 'ADMIN';
    this.User = localStorage.getItem('User');
    this.Auth = false;
    if (localStorage.getItem('accessToken')) {
      this.Auth = true;
    }

    this.GetCartCount();
  }

  GetCartCount() {
    this.CartCount = 0
    const cartJson: string = localStorage.getItem('Cart');
    if (cartJson === null) {
      return;
    }
    const cart: CartModel[] = JSON.parse(cartJson);
    for (const item of cart) {
      if (item.userName === this.User && item.printingEdition !== undefined) {
        for (const cartItem of item.printingEdition) {
          this.CartCount += cartItem.printingEditionCount;
        }
      }
    }
  }

  logout() {
    this.auth.logout();
    this.Auth = !true;
    this.ngOnInit();
    this.router.navigate(['']);
  }

  public reload() {
    window.location.reload();
  }
}

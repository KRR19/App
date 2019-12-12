import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';
import {CartService} from '../../services/cart.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  cartCount: number;
  user: string;
  isAuth = false;
  isAdmin = false;

  constructor(private  auth: AuthService, private  router: Router, private cartService: CartService) {
  }

  ngOnInit() {
    this.MenuLayout();
    this.cartCount = this.cartService.GetCartCount();
    CartService.cartCountChange.subscribe(() => {
      this.cartCount = this.cartService.cartCount;
    });
  }

  public MenuLayout() {
    this.isAuth = this.auth.isAuth;
    this.isAdmin = this.auth.isAdmin;
  }

  logout() {
    this.auth.logout();
    this.auth.isAuth = false;
    this.router.navigate(['']).then(() => window.location.reload());
  }

  public reload() {
    window.location.reload();
  }
}

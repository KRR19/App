import {Component, OnInit} from '@angular/core';
import {CartItem, CartModel} from '../../shared/models/cart.model';
import {MatTableDataSource} from '@angular/material';
import {CartService} from '../../services/cart.service';
import {ExchangeService} from '../../services/exchange.service';
import {OrderModel} from '../../shared/models/order.model';
import {PaymentModel} from '../../shared/models/payment.model';
import {Router} from '@angular/router';
import {HeaderComponent} from '../../shared/header/header.component';

@Component({
  selector: 'app-cart-board',
  templateUrl: './cart-board.component.html',
  styleUrls: ['./cart-board.component.scss']
})
export class CartBoardComponent implements OnInit {
  private cart: CartModel[] = [];
  private userIndex: number;
  private displayedColumns: string[] = ['Item', 'Count', 'Price', 'Amount', 'Controls'];
  public dataSource = new MatTableDataSource<CartItem>();
  public currencySelector: string;
  private totalPriceUSD: number;
  private clientPrice: number;
  public StripeCheckout: any;

  constructor(private cartService: CartService, private exchangeService: ExchangeService, private router: Router, private header: HeaderComponent) {
  }

  ngOnInit() {
    this.currencySelector = '1';
    const user = localStorage.getItem('User');
    const cartJson: string = localStorage.getItem('Cart');
    this.cart = JSON.parse(cartJson);

    for (let i = 0; i < this.cart.length; i++) {
      if (this.cart[i].userName === user) {
        this.userIndex = i;
      }
    }
    this.dataSource = new MatTableDataSource<CartItem>(this.cart[this.userIndex].printingEdition);
    this.TotalCalc();
  }

  CountIncrement(printingEditionId: string) {
    for (let i = 0; i < this.cart[this.userIndex].printingEdition.length; i++) {
      if (this.cart[this.userIndex].printingEdition[i].printingEditionId === printingEditionId) {
        this.cart[this.userIndex].printingEdition[i].printingEditionCount++;
        this.header.cartCount++;
      }
    }
    const cartJson: string = JSON.stringify(this.cart);
    localStorage.setItem('Cart', cartJson);
    this.TotalCalc();
    this.cartService.GetCartCount();
  }

  CountDecrement(printingEditionId: string) {
    for (let i = 0; i < this.cart[this.userIndex].printingEdition.length; i++) {
      if (this.cart[this.userIndex].printingEdition[i].printingEditionId === printingEditionId) {
        this.cart[this.userIndex].printingEdition[i].printingEditionCount--;

        if (this.cart[this.userIndex].printingEdition[i].printingEditionCount < 0) {
          this.cart[this.userIndex].printingEdition[i].printingEditionCount = 0;
        } else {
          this.header.cartCount--;
        }
      }
    }
    const cartJson: string = JSON.stringify(this.cart);
    localStorage.setItem('Cart', cartJson);
    this.TotalCalc();
    this.cartService.GetCartCount();
  }

  DeleteItem(printingEditionId: string) {
    this.cart[this.userIndex].printingEdition = this.cart[this.userIndex].printingEdition.filter(item => item.printingEditionId !== printingEditionId);
    const cartJson: string = JSON.stringify(this.cart);
    localStorage.setItem('Cart', cartJson);
    this.cartService.GetCartCount();
    this.dataSource = new MatTableDataSource<CartItem>(this.cart[this.userIndex].printingEdition);
    this.TotalCalc();
    this.cartService.GetCartCount();
  }

  TotalCalc() {
    this.totalPriceUSD = 0;
    for (const item of this.cart[this.userIndex].printingEdition) {

      if (item.printingEditionCurrency === 2) {
        this.totalPriceUSD += this.exchangeService.EurUsd(item.printingEditionPrice * item.printingEditionCount);
        continue;
      }

      if (item.printingEditionCurrency === 3) {
        this.totalPriceUSD += this.exchangeService.UsdUah(item.printingEditionPrice * item.printingEditionCount);
        continue;
      }
      this.totalPriceUSD += item.printingEditionPrice * item.printingEditionCount;
    }
    this.ClientCurrency(this.currencySelector);
  }

  ClientCurrency(CurrencySelector: string) {
    this.clientPrice = this.totalPriceUSD;
    if (CurrencySelector === '1') {
      this.clientPrice = this.totalPriceUSD;
      return this.clientPrice;
    }

    if (CurrencySelector === '2') {
      this.clientPrice = this.exchangeService.UsdEur(this.totalPriceUSD);
      return this.clientPrice;
    }
    if (CurrencySelector === '3') {
      this.clientPrice = this.exchangeService.UahUsd(this.totalPriceUSD);
      return this.clientPrice;
    }
  }

  private Order() {
    this.pay(this.clientPrice, +this.currencySelector);
  }

  private CreateOrder(payment: PaymentModel) {
    const order: OrderModel = {printingEdition: [{}]};
    order.userName = this.cart[this.userIndex].userName;
    order.printingEdition = this.cart[this.userIndex].printingEdition;
    order.currency = +this.currencySelector;
    order.amount = this.clientPrice;
    order.paymentSource = payment.id;
    order.paymentEmail = payment.email;
    this.cartService.CreateOrder(order);
  }

  private pay(amount: number, currencyNumber: number) {
    const currency = this.cartService.PriceSwitcher(currencyNumber);
    const handler = (<any> window).StripeCheckout.configure({
      key: 'pk_test_fZaEc3bZ6qQ0J0jiNHNe75Nh00I17tMxpI',
      locale: 'auto',
      token: (token: any) => {
        const payment: PaymentModel = {id: token.id, email: token.email};
        this.CreateOrder(payment);
        this.header.cartCount = 0;
        localStorage.removeItem('Cart');
        this.router.navigate(['']).then(() => this.header.reload());
      }
    });
    handler.open({
      name: 'App',
      description: '2 widgets',
      amount: amount * 100,
      currency
    });
  }
}


import {Injectable} from '@angular/core';
import {OrderModel} from '../shared/models/order.model';
import {environment} from '../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {CartModel} from '../shared/models/cart.model';
import {AuthService} from './auth.service';
import {Subject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor(private  http: HttpClient, private auth: AuthService) {
  }

  public static cartCountChange = new Subject();

  private api = 'api';
  private control = 'Order';
  private actionCreate = 'Create';
  private serverUrl = `${environment.protocol}://${environment.host}:${environment.port}/${this.api}/${this.control}`;

  public cartCount: number;

  public async CreateOrder(model: OrderModel) {
    const urlPath = `${this.serverUrl}/${this.actionCreate}`;
    await this.http.post<OrderModel>(urlPath, model).toPromise();
  }

  public PriceSwitcher(element: number) {
    switch (element) {
      case 1:
        return 'USD';
      case 2:
        return 'EUR';
      case 3:
        return 'UAH';
    }
  }

  public AmountCalc(price: number, count: number) {
    const amount: number = price * count;
    return amount;
  }

  public GetCartCount(): number {
    this.cartCount = 0;
    const cartJson: string = localStorage.getItem('Cart');
    if (cartJson === null) {
      return 0;
    }
    const cart: CartModel[] = JSON.parse(cartJson);
    for (const item of cart) {
      if (item.userName === this.auth.user && item.printingEdition !== undefined) {
        for (const cartItem of item.printingEdition) {
          this.cartCount += cartItem.printingEditionCount;
        }
      }
    }
    CartService.cartCountChange.next();

    return this.cartCount;
  }
}



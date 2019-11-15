import { Injectable } from '@angular/core';
import {OrderModel} from '../../../models/OrderModel';
import {environment} from '../../../../environments/environment';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private Api = 'api';
  private control = 'Order';
  private ActionGetAll = 'GetAll';
  private ActionCreate = 'Create';
  private ActionGet = 'Get';

  constructor(private  http: HttpClient) { }

  public async  CreateOrder(model: OrderModel) {
    console.log(model);
    const urlPath: string = environment.protocol + '://' + environment.host + ':' + environment.port + '/' + this.Api + '/' + this.control + '/' + this.ActionCreate;
    const result: OrderModel = await this.http.post<OrderModel>(urlPath, model).toPromise();

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
}



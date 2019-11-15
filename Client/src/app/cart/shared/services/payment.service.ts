import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  constructor() { }
  pay(amount: number, currencyNumber: number) {
    const currency = this.PriceSwitcher(currencyNumber);
    const handler = (<any>window).StripeCheckout.configure({
      key: 'pk_test_fZaEc3bZ6qQ0J0jiNHNe75Nh00I17tMxpI',
      locale: 'auto',
      token: function (token: any) {
        console.log(token)
      }
    });

    handler.open({
      name: 'App',
      description: '2 widgets',
      amount: amount * 100,
      currency
    });
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
}

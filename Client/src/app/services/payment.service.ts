import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  constructor() { }

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

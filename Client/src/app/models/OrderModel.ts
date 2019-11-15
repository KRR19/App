import {CartModel} from './CartModel';

export interface OrderModel extends CartModel{

  Currency?: number;
  Amount?: number;
  Description?: string;
  PaymentEmail?: string;
  PaymentSource?: string;
}

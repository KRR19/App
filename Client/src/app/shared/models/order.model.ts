import {CartModel} from './cart.model';

export interface OrderModel extends CartModel {
  amount?: number;
  currency?: number;
  description?: string;
  paymentEmail?: string;
  paymentSource?: string;
}

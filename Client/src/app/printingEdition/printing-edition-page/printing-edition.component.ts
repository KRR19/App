import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {PrintingEditionService} from '../../services/printingEdition.service';
import {PrintingEditionModel} from '../../shared/models/printing-edition.model';
import {CartItem, CartModel} from '../../shared/models/cart.model';
import {AuthService} from '../../services/auth.service';
import {CartService} from '../../services/cart.service';

@Component({
  selector: 'app-printing-edition-page',
  templateUrl: './printing-edition.component.html',
  styleUrls: ['./printing-edition.component.scss']
})
export class PrintingEditionComponent implements OnInit {

  private printingEdition: PrintingEditionModel = {};

  constructor(private route: ActivatedRoute, private router: Router, private  printingEditionService: PrintingEditionService, private authService: AuthService, private  cartService: CartService) {
  }

  public async ngOnInit() {
    let id: string;
    this.route.params.subscribe(params => id = params.id.slice(3));
    this.printingEdition = await this.printingEditionService.Get(id);
    if (this.printingEdition.image === null) {
      this.printingEdition.image = 'assets/no-image-icon-10.png';
    }
    if (this.printingEdition === null) {
      await this.router.navigate(['']);
    }
  }

  private EditPage() {
    this.router.navigate(['/printing-edition/edit/:id' + this.printingEdition.id]);
  }

  private AddCart() {

    if (!this.authService.isAuth) {
      this.router.navigate(['/auth/SingIn']);
      return;
    }
    let cart: CartModel[] = JSON.parse(localStorage.getItem('Cart'));
    let userIndex = -1;
    let cartJson: string;
    const userName: string = localStorage.getItem('User');

    if (cart === null) {
      cart = [{printingEdition: [{}]}];
      cart[0].userName = userName;
      cart[0].printingEdition[0].printingEditionId = this.printingEdition.id;
      cart[0].printingEdition[0].printingEditionCount = 1;
      cart[0].printingEdition[0].printingEditionName = this.printingEdition.name;
      cart[0].printingEdition[0].printingEditionPrice = this.printingEdition.price;
      cart[0].printingEdition[0].printingEditionCurrency = this.printingEdition.currency;
      cartJson = JSON.stringify(cart);
      localStorage.setItem('Cart', cartJson);
      this.cartService.GetCartCount();
      return;
    }

    for (let i = 0; i < cart.length; i++) {
      if (userName === cart[i].userName) {
        userIndex = i;
        break;
      }
    }

    if (userIndex === -1) {
      const cartModel: CartModel = {userName, printingEdition: [{}]};
      cart.push(cartModel);
      userIndex = cart.length - 1;
    }
    for (let i = 0; i < cart[userIndex].printingEdition.length; i++) {
      if (cart[userIndex].printingEdition[i].printingEditionId === this.printingEdition.id) {
        cart[userIndex].printingEdition[i].printingEditionCount++;
        cartJson = JSON.stringify(cart);
        localStorage.setItem('Cart', cartJson);
        this.cartService.GetCartCount();
        return;
      }
    }

    const newCartItem: CartItem = {
      printingEditionId: this.printingEdition.id,
      printingEditionCount: 1,
      printingEditionName: this.printingEdition.name,
      printingEditionCurrency: this.printingEdition.currency,
      printingEditionPrice: this.printingEdition.price
    };
    cart[userIndex].printingEdition.push(newCartItem);
    cartJson = JSON.stringify(cart);
    localStorage.setItem('Cart', cartJson);
    this.cartService.GetCartCount();
  }
}

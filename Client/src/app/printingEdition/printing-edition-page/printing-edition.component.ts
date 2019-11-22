import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {PrintingEditionService} from '../../services/printingEdition.service';
import {PrintingEditionModel} from '../../shared/models/printing-edition.model';
import {CartItem, CartModel} from '../../shared/models/cart.model';
import {HeaderComponent} from '../../shared/header/header.component';


@Component({
  selector: 'app-printing-edition-page',
  templateUrl: './printing-edition.component.html',
  styleUrls: ['./printing-edition.component.scss']
})
export class PrintingEditionComponent implements OnInit {
  printingEdition: PrintingEditionModel = {};
  isAdmin: boolean;


  constructor(private route: ActivatedRoute, private router: Router, private  printingEditionService: PrintingEditionService, private header: HeaderComponent) {
  }

  async ngOnInit() {
    this.AdminCheck();
    let id: string;
    this.route.params.subscribe(params => id = params.id.slice(3));
    this.printingEdition = await this.printingEditionService.Get(id);
    if (this.printingEdition.image === 'no image') {
      this.printingEdition.image = 'assets/no-image-icon-10.png';
    }
    if (this.printingEdition === null) {
      this.router.navigate(['']);
    }
    console.log(this.printingEdition);
  }
  private AdminCheck() {
    this.isAdmin = localStorage.getItem('Role') === 'ADMIN';
  }

  public EditPage() {
    this.router.navigate(['/printingEdition/edit/:id' + this.printingEdition.id]);
  }


  AddCart() {
    this.header.CartCount++;
    this.header.reload();
    if (!this.header.Auth) {
      this.router.navigate(['/auth/SingIn']);
      return;
    }
    let cart: CartModel[] = JSON.parse(localStorage.getItem('Cart'));
    let userIndex = -1;
    let cartJson: string;
    const userName: string = localStorage.getItem('User');

    if (cart === null) {
      cart = [{printingEdition: [{}]}]
      cart[0].userName = userName;
      cart[0].printingEdition[0].printingEditionId = this.printingEdition.id;
      cart[0].printingEdition[0].printingEditionCount = 1;
      cart[0].printingEdition[0].printingEditionName = this.printingEdition.name;
      cart[0].printingEdition[0].printingEditionPrice = this.printingEdition.price;
      cart[0].printingEdition[0].printingEditionCurrency = this.printingEdition.currency;
      cartJson = JSON.stringify(cart);
      localStorage.setItem('Cart', cartJson);

      return;
    }

    userIndex = cart.length + 1;

    for (let i = 0; i <= cart.length; i++) {
      if (userName === cart[i].userName) {
        userIndex = i;
        break;
      }
    }

    if (userIndex === -1) {
      const cartModel: CartModel = {userName, printingEdition: [{}]};
      cart.push(cartModel);
      userIndex = cart.length;
    }

    for (let i = 0; i < cart[userIndex].printingEdition.length; i++) {
      if (cart[userIndex].printingEdition[i].printingEditionId === this.printingEdition.id) {
        cart[userIndex].printingEdition[i].printingEditionCount++;
        cartJson = JSON.stringify(cart);
        localStorage.setItem('Cart', cartJson);
        this.header.CartCount++;
        return;
      }
    }

    const newCartItem: CartItem = {printingEditionId: this.printingEdition.id,
                                    printingEditionCount: 1,
                                    printingEditionName: this.printingEdition.name,
                                    printingEditionCurrency: this.printingEdition.currency,
                                    printingEditionPrice: this.printingEdition.price};
    cart[userIndex].printingEdition.push(newCartItem);
    cartJson = JSON.stringify(cart);
    localStorage.setItem('Cart', cartJson);


  }
}

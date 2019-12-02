import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {CartBoardComponent} from './cart-board/cart-board.component';

const cartRoutes: Routes = [
  {
    path: '', children: [
      {path: '', redirectTo: '/cart/cartBoard', pathMatch: 'full'},
      {path: 'cartBoard', component: CartBoardComponent}
    ]
  }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(cartRoutes)
  ],
  exports: [RouterModule]
})
export class CartRoutingModule {
}

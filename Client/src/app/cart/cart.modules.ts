import {NgModule, Provider} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {
  MatButtonModule, MatButtonToggleModule,
  MatFormFieldModule,
  MatInputModule,
  MatListModule,
  MatOptionModule,
  MatSelectModule, MatTableModule,
  MatTabsModule
} from '@angular/material';
import {CartBoardComponent} from './cart-board/cart-board.component';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {Interceptor} from '../core/interceptor';

const INTERCEPTOR_PROVIDER: Provider = {
  provide: HTTP_INTERCEPTORS,
  useClass: Interceptor,
  multi: true
}

@NgModule({
  declarations: [CartBoardComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '', children: [
          {path: '', redirectTo: '/cart/cartBoard', pathMatch: 'full'},
          {path: 'cartBoard', component: CartBoardComponent}
        ]
      }
    ]),
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatFormFieldModule,
    FormsModule,
    MatTabsModule,
    MatOptionModule,
    MatSelectModule,
    MatListModule,
    MatTableModule,
    MatButtonToggleModule
  ],
  exports: [RouterModule],
  providers: [INTERCEPTOR_PROVIDER]
})

export class CartModules {

}

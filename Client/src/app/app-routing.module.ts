import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {DashBoardComponent} from './dash-board/dash-board.component';
import {HeaderComponent} from './shared/header/header.component';


const routes: Routes = [
  {
    path: '', component: HeaderComponent, children: [
      {path: '', component: DashBoardComponent},
      {path: 'auth', loadChildren: './auth/auth.module#AuthModule'},
      {path: 'printingEdition', loadChildren: './printingEdition/printingEdition.module#PrintingEditionModule'},
      {path: 'cart', loadChildren: './cart/cart.modules#CartModules'},
      {path: 'author', loadChildren: './author/author.module#AuthorModule'},
      {path: 'user', loadChildren: './user/user.module#UserModule'},
      {path: '**', redirectTo: ''}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}


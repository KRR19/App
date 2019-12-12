import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {DashBoardComponent} from './dash-board/dash-board.component';
import {ErrorRouteComponent} from './error-route/error-route.component';
import {MainLayoutComponent} from './shared/main-layout/main-layout.component';

const routes: Routes = [
  {
    path: '', component: MainLayoutComponent, children: [
      {path: '', component: DashBoardComponent},
      {path: 'auth', loadChildren: './auth/auth.module#AuthModule'},
      {path: 'printing-edition', loadChildren: './printingEdition/printingEdition.module#PrintingEditionModule'},
      {path: 'cart', loadChildren: './cart/cart.modules#CartModules'},
      {path: 'author', loadChildren: './author/author.module#AuthorModule'},
      {path: 'user', loadChildren: './user/user.module#UserModule'},
      {path: '**', component: ErrorRouteComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}


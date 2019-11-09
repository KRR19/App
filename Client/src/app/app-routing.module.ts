import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {DashBoardComponent} from './dash-board/dash-board.component';



const routes: Routes = [
  {path: '', component: DashBoardComponent},
  {path: 'auth', loadChildren: './auth/auth.module#AuthModule'},
  {path: 'printingEdition', loadChildren: './printingEdition/printingEdition.module#PrintingEditionModule'},
  {path: 'admin', loadChildren: './admin/admin.module#AdminModule'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

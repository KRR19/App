import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {HeaderComponent} from './shared/header/header.component';
import {AdminModule} from './admin/admin.module';


const routes: Routes = [
  {path: '', component: HeaderComponent},
  {
    path: 'auth', loadChildren: './auth/auth.module#AuthModule'
  },
  {path: 'admin', loadChildren: './admin/admin.module#AdminModule'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

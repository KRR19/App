import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HttpClientModule} from '@angular/common/http';
import {AlertsModule} from 'angular-alert-module';
import {HeaderComponent} from './shared/header/header.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatFormFieldModule, MatInputModule, MatPaginatorModule, MatSliderModule, MatTableModule} from '@angular/material';
import {AuthService} from './auth/shared/services/auth.service';
import {DashBoardComponent} from './dash-board/dash-board.component';
import {PrintingEditionService} from './printingEdition/shared/service/printingEdition.service';



@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    DashBoardComponent

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AlertsModule,
    BrowserAnimationsModule,
    MatSliderModule,
    MatInputModule,
    MatFormFieldModule,
    MatPaginatorModule,
    MatTableModule
  ],
  providers: [AuthService, PrintingEditionService],
  bootstrap: [AppComponent]
})
export class AppModule {
}

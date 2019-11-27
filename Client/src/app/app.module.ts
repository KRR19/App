import {BrowserModule} from '@angular/platform-browser';
import {NgModule, Provider} from '@angular/core';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {AlertsModule} from 'angular-alert-module';
import {HeaderComponent} from './shared/header/header.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatFormFieldModule, MatInputModule, MatPaginatorModule, MatSliderModule, MatTableModule} from '@angular/material';
import {AuthService} from './services/auth.service';
import {DashBoardComponent} from './dash-board/dash-board.component';
import {PrintingEditionService} from './services/printingEdition.service';
import {Interceptor} from './core/interceptor';
import { ErrorRouteComponent } from './error-route/error-route.component';

const INTERCEPTOR_PROVIDER: Provider = {
  provide: HTTP_INTERCEPTORS,
  useClass: Interceptor,
  multi: true
}

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    DashBoardComponent,
    ErrorRouteComponent
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

  providers: [INTERCEPTOR_PROVIDER, AuthService, PrintingEditionService],
  bootstrap: [AppComponent]
})
export class AppModule {
}

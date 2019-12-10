import {BrowserModule} from '@angular/platform-browser';
import {NgModule, Provider} from '@angular/core';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {AlertsModule} from 'angular-alert-module';
import {HeaderComponent} from './shared/header/header.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {
  MatDialogModule,
  MatFormFieldModule,
  MatInputModule,
  MatOptionModule,
  MatPaginatorModule,
  MatSelectModule,
  MatSliderModule,
  MatTableModule
} from '@angular/material';
import {AuthService} from './services/auth.service';
import {DashBoardComponent} from './dash-board/dash-board.component';
import {PrintingEditionService} from './services/printingEdition.service';
import {Interceptor} from './core/interceptor';
import {ErrorRouteComponent} from './error-route/error-route.component';
import { MainLayoutComponent } from './shared/main-layout/main-layout.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {AuthorService} from './services/author.service';
import { AddDialogComponent } from './add-dialog/add-dialog.component';

const INTERCEPTOR_PROVIDER: Provider = {
  provide: HTTP_INTERCEPTORS,
  useClass: Interceptor,
  multi: true
};

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    DashBoardComponent,
    ErrorRouteComponent,
    MainLayoutComponent,
    AddDialogComponent
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
    MatTableModule,
    ReactiveFormsModule,
    FormsModule,
    MatOptionModule,
    MatSelectModule,
    MatDialogModule
  ],

  providers: [ AuthService, PrintingEditionService, HeaderComponent, AuthorService, INTERCEPTOR_PROVIDER],
  bootstrap: [AppComponent],
  entryComponents: [AddDialogComponent]
})
export class AppModule {
}

import {NgModule, Provider} from '@angular/core';
import {CreateComponent} from './create/create.component';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {
  MatButtonModule, MatButtonToggleModule,
  MatFormFieldModule,
  MatInputModule,
  MatListModule,
  MatOptionModule,
  MatSelectModule,
  MatTabsModule
} from '@angular/material';
import {PrintingEditionService} from '../services/printingEdition.service';
import {AuthorService} from '../services/author.service';
import {PrintingEditionComponent} from './printing-edition-page/printing-edition.component';
import {EditComponent} from './edit/edit.component';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {Interceptor} from '../core/interceptor';
import {PrintingEditionRoutingModule} from './printing-edition-routing.module';

const INTERCEPTOR_PROVIDER: Provider = {
  provide: HTTP_INTERCEPTORS,
  useClass: Interceptor,
  multi: true
};

@NgModule({
  declarations: [CreateComponent, PrintingEditionComponent, EditComponent],
  imports: [
    CommonModule,
    PrintingEditionRoutingModule,
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
    MatButtonToggleModule
  ],
  exports: [RouterModule],
  providers: [INTERCEPTOR_PROVIDER, PrintingEditionService, AuthorService]
})

export class PrintingEditionModule {
}

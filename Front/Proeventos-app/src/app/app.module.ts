import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {PalestrantesComponent} from './components/palestrantes/palestrantes.component';
import {HttpClientModule} from "@angular/common/http";
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {NavBarComponent} from './shared/nav-bar/nav-bar.component';
import {CollapseModule} from 'ngx-bootstrap/collapse';
import {FormsModule} from "@angular/forms";
import {EventService} from "./services/event.service";
import {NgOptimizedImage} from "@angular/common";
import {DateFormatPipe} from './helpers/dateFormat.pipe';
import {TooltipModule} from 'ngx-bootstrap/tooltip';
import {BsDropdownModule} from 'ngx-bootstrap/dropdown';
import {ModalModule} from 'ngx-bootstrap/modal';
import {ToastrModule} from 'ngx-toastr';
import {NgxSpinnerModule} from "ngx-spinner";
import { TituloComponent } from './shared/titulo/titulo.component';
import { ContatosComponent } from './components/contatos/contatos.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { PerfilComponent } from './components/perfil/perfil.component';
import { EventoDetalheComponent } from './components/eventos/evento-detalhe/evento-detalhe.component';
import { EventoListagemComponent } from './components/eventos/evento-listagem/evento-listagem.component';
import {EventosComponent} from "./components/eventos/eventos.component";

@NgModule({
  declarations: [
    AppComponent,
    PalestrantesComponent,
    NavBarComponent,
    DateFormatPipe,
    TituloComponent,
    ContatosComponent,
    DashboardComponent,
    PerfilComponent,
    EventoDetalheComponent,
    EventoListagemComponent,
    EventosComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    FormsModule,
    NgOptimizedImage,
    TooltipModule,
    BsDropdownModule,
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true,
    }),
    NgxSpinnerModule.forRoot({type: 'ball-scale-multiple'})
  ],
  providers: [EventService],
  bootstrap: [AppComponent], schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule {
}

import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {PalestrantesComponent} from './components/palestrantes/palestrantes.component';
import {HttpClientModule} from "@angular/common/http";
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {NavBarComponent} from './shared/nav-bar/nav-bar.component';
import {CollapseModule} from 'ngx-bootstrap/collapse';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {EventService} from "./services/event/event.service";
import {NgOptimizedImage} from "@angular/common";
import {DateFormatPipe} from './helpers/dateFormat.pipe';
import {TooltipModule} from 'ngx-bootstrap/tooltip';
import {BsDropdownModule} from 'ngx-bootstrap/dropdown';
import {ModalModule} from 'ngx-bootstrap/modal';
import {ToastrModule} from 'ngx-toastr';
import {NgxSpinnerModule} from "ngx-spinner";
import {TituloComponent} from './shared/titulo/titulo.component';
import {ContatosComponent} from './components/contatos/contatos.component';
import {DashboardComponent} from './components/dashboard/dashboard.component';
import {PerfilComponent} from './components/user/perfil/perfil.component';
import {EventoDetalheComponent} from './components/eventos/evento-detalhe/evento-detalhe.component';
import {EventoListagemComponent} from './components/eventos/evento-listagem/evento-listagem.component';
import {EventosComponent} from "./components/eventos/eventos.component";
import {UserComponent} from './components/user/user.component';
import {LoginComponent} from './components/user/login/login.component';
import {RegistrationComponent} from './components/user/registration/registration.component';
import {BsDatepickerModule} from "ngx-bootstrap/datepicker";
import {defineLocale} from 'ngx-bootstrap/chronos';
import {ptBrLocale} from 'ngx-bootstrap/locale';
import {NgxCurrencyDirective} from "ngx-currency";
import {TabsModule} from "ngx-bootstrap/tabs";
import { PerfilDetalheComponent } from './components/user/perfil/perfil-detalhe/perfil-detalhe/perfil-detalhe.component';
import { PalestranteListaComponent } from './components/palestrantes/palestrante-lista/palestrante-lista/palestrante-lista.component';
import { PalestranteDetalheComponent } from './components/palestrantes/palestrante-detalhe/palestrante-detalhe.component';
import { RedesSociaisComponent } from './components/redeSocial/redes-sociais/redes-sociais.component';

defineLocale('pt-br', ptBrLocale)

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
    EventosComponent,
    UserComponent,
    LoginComponent,
    RegistrationComponent,
    PerfilDetalheComponent,
    PalestranteListaComponent,
    PalestranteDetalheComponent,
    RedesSociaisComponent,
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
    TabsModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot({
        timeOut: 3000,
        positionClass: 'toast-bottom-right',
        preventDuplicates: true,
        progressBar: true,
      }
    ),
    BsDatepickerModule.forRoot(),
    NgxSpinnerModule.forRoot({type: 'ball-scale-multiple'}),
    ReactiveFormsModule, NgxCurrencyDirective
  ],
  providers: [EventService],
  bootstrap:
    [AppComponent], schemas:
    [CUSTOM_ELEMENTS_SCHEMA]
})

export class AppModule {
}

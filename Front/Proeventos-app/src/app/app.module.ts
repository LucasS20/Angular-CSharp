import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {EventosComponent} from './eventos/eventos.component';
import {PalestrantesComponent} from './palestrantes/palestrantes.component';
import {HttpClientModule} from "@angular/common/http";
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {NavBarComponent} from './nav-bar/nav-bar.component';
import {CollapseModule} from 'ngx-bootstrap/collapse';
import {FormsModule} from "@angular/forms";
import {EventService} from "./services/event.service";
import {NgOptimizedImage} from "@angular/common";
import { DateFormatPipe } from './helpers/dateFormat.pipe';


@NgModule({
  declarations: [
    AppComponent,
    EventosComponent,
    PalestrantesComponent,
    NavBarComponent,
    DateFormatPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    FormsModule,
    NgOptimizedImage
  ],
  providers: [EventService],
  bootstrap: [AppComponent]
})
export class AppModule {
}

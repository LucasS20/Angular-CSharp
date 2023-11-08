import {createComponent, NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {PalestrantesComponent} from "./components/palestrantes/palestrantes.component";
import {PerfilComponent} from "./components/user/perfil/perfil.component";
import {ContatosComponent} from "./components/contatos/contatos.component";
import {DashboardComponent} from "./components/dashboard/dashboard.component";
import {EventosComponent} from "./components/eventos/eventos.component";
import {EventoDetalheComponent} from "./components/eventos/evento-detalhe/evento-detalhe.component";
import {EventoListagemComponent} from "./components/eventos/evento-listagem/evento-listagem.component";
import {UserComponent} from "./components/user/user.component";
import {LoginComponent} from "./components/user/login/login.component";
import {RegistrationComponent} from "./components/user/registration/registration.component";
import {
  PalestranteListaComponent
} from "./components/palestrantes/palestrante-lista/palestrante-lista/palestrante-lista.component";

const routes: Routes = [
  {
    path: 'user', component: UserComponent, children: [{
      path: 'login', component: LoginComponent
    },
      {path: 'register', component: RegistrationComponent},
      {
        path: 'perfil', component: PerfilComponent
      }]
  },
  {path: 'eventos', redirectTo: 'eventos/listagem'},
  {
    path: 'eventos', component: EventosComponent,
    children: [
      {path: 'detalhe/:id', component: EventoDetalheComponent},
      {path: 'detalhe', component: EventoDetalheComponent},
      {path: 'listagem', component: EventoListagemComponent}
    ]
  },
  {path: 'dashboard', component: DashboardComponent},

  {path: 'palestrantes', component: PalestrantesComponent},
  {path: 'contatos', component: ContatosComponent},
  {path: '', redirectTo: 'dashboard', pathMatch: 'full'},
  {path: '**', redirectTo: 'dashboard', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}

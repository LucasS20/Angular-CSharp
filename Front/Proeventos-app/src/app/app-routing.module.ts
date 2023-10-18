import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {EventosComponent} from "./components/eventos/eventos.component";
import {PalestrantesComponent} from "./components/palestrantes/palestrantes.component";
import {PerfilComponent} from "./components/perfil/perfil.component";
import {ContatosComponent} from "./components/contatos/contatos.component";
import {DashboardComponent} from "./components/dashboard/dashboard.component";

const routes: Routes = [
  {path: 'Eventos', component: EventosComponent},
  {path: 'Dashboard', component: DashboardComponent},
  {path: 'Perfil', component: PerfilComponent},
  {path: 'Palestrante', component: PalestrantesComponent},
  {path: 'Contatos', component: ContatosComponent},
  {path: '', redirectTo: 'Dashboard', pathMatch: 'full'},
  {path: '**', redirectTo: 'Dashboard', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}

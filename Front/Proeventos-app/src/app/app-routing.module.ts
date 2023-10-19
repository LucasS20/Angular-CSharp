import {createComponent, NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {PalestrantesComponent} from "./components/palestrantes/palestrantes.component";
import {PerfilComponent} from "./components/perfil/perfil.component";
import {ContatosComponent} from "./components/contatos/contatos.component";
import {DashboardComponent} from "./components/dashboard/dashboard.component";
import {EventosComponent} from "./components/eventos/eventos.component";
import {EventoDetalheComponent} from "./components/eventos/evento-detalhe/evento-detalhe.component";
import {EventoListagemComponent} from "./components/eventos/evento-listagem/evento-listagem.component";

const routes: Routes = [
    {
        path: 'eventos', component: EventosComponent,
        children: [
            {path: 'detalhe/:id', component: EventoDetalheComponent},
            {path: 'detalhe', component: EventoDetalheComponent},
            {path: 'listagem', component: EventoListagemComponent}
        ]
    },
    {path: 'dashboard', component: DashboardComponent},
    {path: 'perfil', component: PerfilComponent},
    {path: 'palestrante', component: PalestrantesComponent},
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

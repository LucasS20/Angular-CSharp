import {Component, Input} from '@angular/core';
import {Router} from "@angular/router";

@Component({
    selector: 'app-titulo',
    templateUrl: './titulo.component.html',
    styleUrls: ['./titulo.component.scss']
})
export class TituloComponent {
    @Input() tittle: string = "";
    @Input() iconClass: string = "fa fa-user";
    @Input() subtitle: string = "Placeholder";
    @Input() showButton: boolean = false;
    private listarConteudo: boolean = false;

    ngOnInit() {
    }

    constructor(private router: Router) {
    }

    listar(): void {
        this.router.navigate([`/${(this.tittle.toLowerCase())}/listagem`])
    }
}

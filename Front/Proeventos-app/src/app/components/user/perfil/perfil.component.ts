import {Component, Input, OnInit} from '@angular/core';
import {NgxSpinnerService} from "ngx-spinner";
import {Palestrante} from "../../../models/Palestrante";
import {PalestranteService} from "../../../services/speaker/palestrante.service";


@Component({
    selector: 'app-perfil',
    templateUrl: './perfil.component.html',
    styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {
    public loaded = false;
    public formValue: any = {};
    @Input() palestranteId: number = 1;
    palestrante !: Palestrante

    constructor(private spinner: NgxSpinnerService,
                private palestranteService: PalestranteService) {
    }

    async ngOnInit() {
        await this.spinner.show();
        this.palestrante = await this.getPalestrante(1);
        await this.spinner.hide();
        this.loaded = true;
    }

    public setFormValue($event: any) {
        this.formValue = $event;
    }

    public isSpeaker(): boolean {
        return this.formValue.userFunction === 'Palestrante'
    }

    public getPalestrante(id: number): Promise<Palestrante> {
        return new Promise((resolve, reject) => {
            this.palestranteService.getById(id).subscribe({
                next: (palestrante: Palestrante) => {
                    resolve(palestrante);
                },
                error: (err: any) => {
                    console.error(err);
                    reject(err);
                }, complete: () => {

                }
            })
        });

    }
}

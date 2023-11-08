import {Component} from '@angular/core';
import {Form, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {SpeakerService} from "../../../../services/speaker/speaker.service";
import {ToastrService} from "ngx-toastr";
import {NgxSpinnerService} from "ngx-spinner";

@Component({
    selector: 'app-palestrante-detalhe',
    templateUrl: './palestrante-detalhe.component.html',
    styleUrls: ['./palestrante-detalhe.component.scss']
})
export class PalestranteDetalheComponent {


     _form: FormGroup;
    corDaDescricao: any;
    formSituation: any;
    formBuilder: FormBuilder = new FormBuilder;

    constructor(formBuilder: FormBuilder,
                palestranteService: SpeakerService,
                toastr: ToastrService,
                spinner: NgxSpinnerService) {
        this._form = this.formBuilder.group({miniCurriculo: [Validators.required]})
    }

    ngOnInit() {
        this.validation();
    }

    private validation() {
        this.formBuilder.group({
            miniCurriculo: []
        })
    }

    get f() {
        return this._form.controls;
    }
}

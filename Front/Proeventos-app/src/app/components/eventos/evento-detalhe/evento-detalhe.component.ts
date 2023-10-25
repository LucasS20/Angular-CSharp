import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {BsLocaleService} from "ngx-bootstrap/datepicker";
import {ActivatedRoute} from "@angular/router";
import {EventService} from "../../../services/event.service";
import {Evento} from "../../../models/Evento";
import {DateFormatPipe} from "../../../helpers/dateFormat.pipe";
import {NgxSpinnerService} from "ngx-spinner";
import {ToastrService} from "ngx-toastr";

@Component({
    selector: 'app-evento-detalhe',
    templateUrl: './evento-detalhe.component.html',
    styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

    form: FormGroup
    event = {} as Evento

    constructor(private fb: FormBuilder, private localeService: BsLocaleService, private router: ActivatedRoute, private eventService: EventService, private spinner: NgxSpinnerService,
                private toastr: ToastrService) {
        this.form = new FormGroup({})
        this.localeService.use('pt-br')
    }

    get getControls(): any {
        return this.form.controls;
    }

    get bsConfig() {
        return {
            adaptivePosition: true,
            dateInputFormat: 'DD/MM/YYYY mm:ss a',
            containerClass: 'theme-green',
            showWeekNumbers: false
        }
    }

    ngOnInit() {
        this.loadEvent();
        this.validation();
    }

    public validation() {
        this.form = this.fb.group({
            date: ['', [Validators.required, Validators.minLength(3)]],
            theme: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
            local: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(80)]],
            numberOfPeoples: ['', [
                Validators.required,
                Validators.min(2),
                Validators.max(120000)
            ]],
            imgUrl: ['', Validators.required],
            phone: ['', Validators.required],
            email: ['', [Validators.required, Validators.email]],
        })
    }

    public cssValidator(campo: FormControl) {
        return {'is-invalid': campo.errors && campo.touched}
    }

    public loadEvent() {

        let eventIdParam = this.router.snapshot.paramMap.get('id');
        if (eventIdParam !== null) {
            this.spinner.show()
            this.eventService.getById(+eventIdParam).subscribe(
                (event: Evento) => {
                    this.event = {...event}
                    this.form.patchValue(this.event)
                },
                (error: any) => {
                    console.error(error)
                    this.toastr.error("Error while trying to load the events","ERROR")
                    this.spinner.hide();
                },
                () => {this.spinner.hide()
                }
            );
        }
        else this.toastr.error("Erro ao carregar")
    }

    protected readonly DateFormatPipe = DateFormatPipe;
}

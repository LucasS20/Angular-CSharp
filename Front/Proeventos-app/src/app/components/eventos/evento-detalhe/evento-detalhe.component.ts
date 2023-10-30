import {Component, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {BsLocaleService} from "ngx-bootstrap/datepicker";
import {ActivatedRoute} from "@angular/router";
import {EventService} from "../../../services/event/event.service";
import {Evento} from "../../../models/Evento";
import {NgxSpinnerService} from "ngx-spinner";
import {ToastrService} from "ngx-toastr";
import {Batch} from "../../../models/Batch";
import {add} from "ngx-bootstrap/chronos";

@Component({
    selector: 'app-evento-detalhe',
    templateUrl: './evento-detalhe.component.html',
    styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

    form: FormGroup
    event = {} as Evento
    saveStatus: string = 'post';

    constructor(private fb: FormBuilder, private localeService: BsLocaleService, private router: ActivatedRoute, public eventService: EventService, private spinner: NgxSpinnerService,
                private toastr: ToastrService) {
        this.form = new FormGroup({})
        this.localeService.use('pt-br')
    }

    ngOnInit() {
        this.loadEvent();
        this.validation();
    }

    //#region Gets
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

    get lotsFormArray(): FormArray {
        return this.form.get('lots') as FormArray
    }

    //endregion
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
            lots: this.fb.array([])
        })
    }

    addLot(): void {
        this.lotsFormArray.push(this.createLot({id: 0} as Batch))
    }

    createLot(lot: Batch): FormGroup {
        return this.fb.group({
            id: [lot.id],
            name: [lot.name, Validators.required],
            price: [lot.price, Validators.required],
            startDate: [lot.startdate, Validators.required],
            endDate: [lot.enddate, Validators.required],
            ticketAmount: [lot.ticketsAmount, Validators.required],
        })
    }

    public cssValidator(campo: FormControl) {
        return {'is-invalid': campo.errors && campo.touched}
    }

    public loadEvent() {

        let eventIdParam = this.router.snapshot.paramMap.get('id');
        if (eventIdParam !== null) {
            this.saveStatus = 'put'
            this.spinner.show()
            this.eventService.getById(+eventIdParam).subscribe(
                (event: Evento) => {
                    this.event = {...event}
                    this.form.patchValue(this.event)
                },
                (error: any) => {
                    console.error(error)
                    this.toastr.error("Error while trying to load the events", "ERROR")
                    this.spinner.hide();
                },
                () => {
                    this.spinner.hide()
                }
            );
        }
    }

    saveChanges() {
        this.spinner.show();


        if (this.form.valid) {
            if (this.saveStatus === 'post') {
                this.event = {...this.form.value}
                this.initRequiredFields();
            } else {
                let lots = this.event.lots
                let se = this.event.speakersEvent;
                let sm = this.event.socialMedias;
                this.event = {id: this.event.id, ...this.form.value};
                this.event.lots = lots
                this.event.speakersEvent = se
                this.event.socialMedias = sm
            }


            // @ts-ignore
            this.eventService[this.saveStatus](this.event).subscribe(
                () => {
                    this.toastr.success('Event saved successfully', 'Saved')
                },
                (error: any) => {
                    console.log(error);
                    this.spinner.hide()
                    this.toastr.error("Error when trying to save changes,Error")
                },
                () => {
                    this.spinner.hide()
                }
            )

        }
    }

    initRequiredFields() {
        this.event.lots = []
        this.event.speakersEvent = []
        this.event.socialMedias = []
    }

    protected readonly add = add;
}

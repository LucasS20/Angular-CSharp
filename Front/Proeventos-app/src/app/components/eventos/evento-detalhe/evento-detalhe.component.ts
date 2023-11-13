import {Component, OnInit, TemplateRef} from '@angular/core';
import {AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {BsLocaleService} from "ngx-bootstrap/datepicker";
import {ActivatedRoute, Router} from "@angular/router";
import {EventService} from "../../../services/event/event.service";
import {Evento} from "../../../models/Evento";
import {NgxSpinnerService} from "ngx-spinner";
import {ToastrService} from "ngx-toastr";
import {Batch} from "../../../models/Batch";
import {BatchService} from "../../../services/batch/batch.service";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";

@Component({
    selector: 'app-evento-detalhe',
    templateUrl: './evento-detalhe.component.html',
    styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {
    private eventId: number;
    form: FormGroup
    event = {} as Evento
    saveStatus: string = 'post';
    currentBatch = {id: 0, name: '', index: 0};
    imagemURL = 'assets/uploadCloud.svg';
    file: File;

    constructor(private fb: FormBuilder,
                private localeService: BsLocaleService,
                private activatedRoute: ActivatedRoute,
                private eventService: EventService,
                private batchService: BatchService,
                private spinner: NgxSpinnerService,
                private toastr: ToastrService,
                private router: Router,
                private modalRef: BsModalRef,
                private modalService: BsModalService
    ) {
        this.file = {} as File;
        this.eventId = -1;
        this.form = new FormGroup({})
        this.localeService.use('pt-br')
    }

    ngOnInit() {
        this.loadEvent();
        this.validation();
    }

//#region Gets
    public get getControls(): any {
        return this.form?.controls;
    }

    public get bsConfig() {
        return {
            adaptivePosition: true,
            dateInputFormat: 'DD/MM/YYYY mm:ss a',
            containerClass: 'theme-green',
            showWeekNumbers: false
        }
    }

    public get editMode() {
        return this.saveStatus === 'put';
    }

    public get batches(): FormArray {
        return this.form.get('batches') as FormArray
    }

//endregion

    private validation() {
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
            batches: this.fb.array([])
        })
    }


//#region Batches
    public addBatch(): void {
        this.batches.push(this.createBatch({id: 0} as Batch))
    }

    public saveBatches() {

        if (this.getControls['batches'].valid) {
            this.spinner.show();
            this.batchService.saveBatch(this.eventId, this.form.value['batches']).subscribe(
                () => {
                    this.toastr.success("Lotes salvos com sucesso!", 'Sucesso!')
                    this.router.navigate([`eventos/detalhe/${this.eventId}`]);
                },
                (error: any) => {
                    this.toastr.error("Error ao tentar salvar lotes", 'Error');
                    console.error(error);
                },
                () => {
                    this.spinner.hide();
                }
            ).add(() => this.spinner.hide())
        } else {
            this.toastr.info("Insira o formulário valido", "Ops!!")
        }
    }

    public loadBatches() {
        this.batchService.getByEventId(this.eventId).subscribe(
            (batches: Batch[]) => {
                batches.forEach(b => this.batches.push(this.createBatch(b)))
            },
            (error) => {
                console.error(error)
                this.toastr.error("Erro ao carregar os lotes")
            },
            () => {
                this.spinner.hide()
            },)
    }

    public deleteBatch(template: TemplateRef<any>, index: number) {
        this.currentBatch.id = this.batches.get(index + '.id')?.value;
        this.modalRef = this.modalService.show(template, {class: 'modal-md'});
    }

//endregion
    public cssValidator(campo: FormControl | AbstractControl | null) {
        return campo ? {'is-invalid': campo.errors && campo.touched} : {};
    }

//#region EVENTS
    public loadEvent() {
        // @ts-ignore
        this.eventId = this.activatedRoute.snapshot.paramMap ? +this.activatedRoute.snapshot?.paramMap.get('id') : -1;
        if (this.eventId !== null && this.eventId !== 0) {
            this.saveStatus = 'put'
            this.spinner.show()
            this.eventService.getById(this.eventId).subscribe(
                (event: Evento) => {

                    this.event = {...event}

                    this.form.patchValue(this.event)

                    this.loadBatches();
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

    public saveEvent() {
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
                (evento: Evento) => {
                    this.toastr.success('Event saved successfully', 'Saved')
                    this.router.navigate([`eventos/detalhe/${evento.id}`]);
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

//endregion
//#region private methods
    private createBatch(batch: Batch): FormGroup {
        return this.fb.group({
            id: [batch.id],
            name: [batch.name, Validators.required],
            price: [batch.price, Validators.required],
            startDate: [batch.startDate, Validators.required],
            endDate: [batch.endDate, Validators.required],
            ticketAmount: [batch.ticketAmount, Validators.required],
        })
    }

    private initRequiredFields() {
        this.event.lots = []
        this.event.speakersEvent = []
        this.event.socialMedias = []
    }

//endregion

    decline() {
        this.modalRef.hide();
    }

    confirm() {

        this.modalRef.hide();
        this.spinner.show();

        this.batchService.delete(this.eventId, this.currentBatch.id).subscribe(
            () => {
                console.log(this.currentBatch.id)
                this.toastr.success("Lote deletado com sucesso", "Deletado")
                this.batches.removeAt(this.currentBatch.index);
            },
            (e) => {
                console.error(e)
                this.toastr.error("Erro ao deletar", "Erro")
            },
            () => {
            }
        ).add(() => this.spinner.hide());
        this.spinner.hide();
    }

    onFileChange(evento: any) {
        const reader = new FileReader();
        reader.onload = (ev: any) => this.imagemURL = ev.target.result;
        this.file = evento.target.files;
        // @ts-ignore
        reader.readAsDataURL(this.file[0])
        this.uploadImagem();
        this.loadEvent();
    }

    uploadImagem() {
        this.spinner.show();
        this.eventService.urlUpload(this.eventId, this.file).subscribe(
            () => {

                this.toastr.success("Imagem alterada com sucesso", "Sucesso")
            },
            (e) => {
                console.error(e);
                this.toastr.error(e);
            },
            () => {
            }
        ).add(() => this.spinner.hide());
    }


}

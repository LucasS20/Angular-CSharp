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
    eventId: number = Math.min();
    form!: FormGroup
    event: Evento = {base64: '',} as Evento;
    saveStatus: string = 'post';
    currentBatch = {id: 0, name: '', index: 0};
    imagemURL: string = 'assets/uploadCloud.svg';
    file: File = {} as File;

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
        this.localeService.use('pt-br');
    }

    ngOnInit() {
        this.loadEvent();
        this.initForm();
        this.editMode ? null : this.addBatch();

    }

//#region Gets
    public get getControls(): any {
        return this.form?.controls;
    }

    public get bsConfig() {
        return {
            adaptivePosition: true,
            dateInputFormat: 'YYYY/MM/DD',
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


//#region Batches
    public addBatch(): void {
        this.batches.push(this.createBatch({id: 0} as Batch))
    }


    public loadBatches() {
        this.batchService.getByEventId(this.eventId).subscribe({
            next: (batches: Batch[]) => {
                console.log(batches);
                batches.forEach(b => this.batches.push(this.createBatch(b)))
            },
            error: (error) => {
                console.error(error)
                this.toastr.error("Erro ao carregar os lotes")
            },
            complete: () => {
                this.spinner.hide().then()
            }
        })
    }

    public deleteBatch(template: TemplateRef<any>, index: number) {
        this.currentBatch.id = this.batches.at(index).get('id')?.value;
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
            this.spinner.show().then()
            this.eventService.getById(this.eventId).subscribe({
                next: (event: Evento) => {

                    this.event = {...event}

                    this.form.patchValue(this.event)

                    this.loadBatches();
                },
                error: (error: any) => {
                    console.error(error)
                    this.toastr.error("Error while trying to load the events", "ERROR")
                    this.spinner.hide().then();
                },
                complete: () => {
                    this.spinner.hide().then()
                }
            });
        }
    }

    public saveEvent() {
        if (this.form.valid) {
            const foto = this.event.base64;
            const socialMedias = this.event.socialMedias;
            if (this.saveStatus === 'post') {
                this.event = {...this.form.value}
                console.log(this.event);
                this.event.socialMedias = []
            } else {
                this.event = {id: this.event.id, ...this.form.value};
                this.event.socialMedias = socialMedias
            }
            this.event.batches = this.form.value['batches']
            this.event.speakerId = 1;
            this.event.base64 = foto;

            // @ts-ignore
            this.eventService[this.saveStatus](this.event).subscribe(
                (evento:Evento) => {
                    this.toastr.success('Event saved successfully', 'Saved')
                    this.router.navigate([`eventos/detalhe/${evento.id}`]).then();
                },
                (error:any) => {
                    this.spinner.hide().then()
                    console.error(error);
                    this.toastr.info(error.error, "Error")
                },
                () => {
                    this.spinner.hide().then()
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


    private initForm() {
        this.form = this.fb.group({
            date: ['', [Validators.required, Validators.minLength(3)]],
            theme: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
            local: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(80)]],
            numberOfPeoples: ['', [
                Validators.required,
                Validators.min(2),
                Validators.max(120000)
            ]],
            phone: ['', Validators.required],
            email: ['', [Validators.required, Validators.email]],
            batches: this.fb.array([])
        })
    }

    //endregion

    modalDecline() {
        this.modalRef.hide();
    }

    modalConfirm() {
        if (this.form.value['batches'].length > 1) {
            this.modalRef.hide();
            this.spinner.show().then();
            this.batchService.delete(this.eventId, this.currentBatch.id).subscribe({
                next: () => {
                    this.toastr.success("Lote deletado com sucesso", "Deletado")
                    this.batches.removeAt(this.currentBatch.index);
                },
                error: (e) => {
                    console.error(e)
                    this.toastr.error("Erro ao deletar", "Erro")
                },
                complete: () => {
                }
            }).add(() => this.spinner.hide());
            this.spinner.hide().then();
        } else {
            this.modalRef.hide();
            this.toastr.info("O evento deve possuir no menos UM lote", "Não foi possivel deletar")
        }
    }

    onFileChange(evento: any) {
        const reader = new FileReader();
        reader.onload = (evento: any) => {
            this.imagemURL = evento.target.result;
            this.event.base64 = evento.target.result;
        };
        const file = evento.target.files[0];
        if (file) {
            this.file = file;
            reader.readAsDataURL(this.file);
        }
    }


}

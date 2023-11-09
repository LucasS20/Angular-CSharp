import {Component, TemplateRef} from '@angular/core';
import {AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {RedesSociais} from "../../../models/RedesSociais";
import {RedesSociaisService} from "../../../services/redesSociais/redes-sociais.service";
import {NgxSpinnerService} from "ngx-spinner";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";

@Component({
    selector: 'app-redes-sociais',
    templateUrl: './redes-sociais.component.html',
    styleUrls: ['./redes-sociais.component.scss']
})
export class RedesSociaisComponent {
    public formRS: FormGroup = {} as FormGroup;
    public redeSocialAtual = {id: 0, name: '', index: ''}
    public redesSociais: RedesSociais[] = [];
    public eventoId = 0


    constructor(private formbuilder: FormBuilder,
                private modalRef: BsModalRef,
                private service: RedesSociaisService,
                private spinner: NgxSpinnerService,
                private toastr: ToastrService,
                private router: Router,
                private modalService: BsModalService) {
    }

    retornaTitulo(nome: string): string {
        return nome === null || nome === '' ? 'Nome do Lote' : nome;
    }

    public cssValidator(campo: FormControl | AbstractControl | null) {
        return campo ? {'is-invalid': campo.errors && campo.touched} : {};
    }

    adicionarRedeSocial() {
        this.socialMedias.push(this.createSocialMedia({id: 0} as RedesSociais))
    }

    salvarRedesSociais() {
        this.isEvento() ? this.saveOnEvento() : this.saveOnPalestrante()
    }

    confirmDeleteRedeSocial() {

    }

    declineDeleteRedeSocial() {
    }

    removerRedeSocial(template: TemplateRef<any>, index: number) {
        this.redeSocialAtual.id = this.socialMedias.get(index + '.id')?.value;
        this.modalRef = this.modalService.show(template, {class: 'modal-md'});
    }

    public validation() {
        this.formRS = this.formbuilder.group({
            redesSociais: this.formbuilder.array([])
        })
    }

    ngOnInit() {
        this.validation()
    }

    get getControls() {
        return this.formRS.controls
    }

    public get socialMedias(): FormArray {
        return this.formRS.get('redesSociais') as FormArray
    }


    private saveOnEvento() {
        if (this.getControls['batches'].valid) {
            this.spinner.show().then();
            this.service.saveOnEvento(this.eventoId, this.formRS.value['socialMedias']).subscribe(
                () => {
                    this.toastr.success("Lotes salvos com sucesso!", 'Sucesso!')
                    this.router.navigate([`eventos/detalhe/${this.eventoId}`]).then();
                },
                (error: any) => {
                    this.toastr.error("Error ao tentar salvar lotes", 'Error');
                    console.error(error);
                },
                () => {
                    this.spinner.hide().then();
                }
            ).add(() => this.spinner.hide())
        } else {
            this.toastr.info("Insira o formulário valido", "Ops!!")
        }
    }

    private saveOnPalestrante() {
        if (this.getControls['batches'].valid) {
            this.spinner.show().then();
            this.service.saveOnSpeaker(this.eventoId, this.formRS.value['socialMedias']).subscribe(
                () => {
                    this.toastr.success("Lotes salvos com sucesso!", 'Sucesso!')
                    this.router.navigate([`eventos/detalhe/${this.eventoId}`]);
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

    public loadSocialMedias() {
        this.isEvento() ? this.loadByEvent() : this.loadBySpeaker();
    }

    private isEvento() {
        return this.eventoId !== 0 || this.eventoId !== null
    }

    private loadBySpeaker() {
        this.service.getAllByPalestranteId(this.eventoId).subscribe(
            (socialMedias) => {
                socialMedias.forEach(b => this.socialMedias.push(this.createSocialMedia(b)))
            },
            (error) => {
                console.error(error)
                this.toastr.error("Erro ao carregar os lotes")
            },
            () => {
                this.spinner.hide()
            },)
    }

    private loadByEvent() {
        this.service.getAllByEventId(this.eventoId).subscribe(
            (batches: RedesSociais[]) => {
                batches.forEach(b => this.socialMedias.push(this.createSocialMedia(b)))
            },
            (error) => {
                console.error(error)
                this.toastr.error("Erro ao carregar os lotes")
            },
            () => {
                this.spinner.hide()
            },)
    }


    private createSocialMedia(batch: RedesSociais):
        FormGroup {
        return this.formbuilder.group({
            id: [batch.id],
            name: [batch.name, Validators.required],
            url: [batch.url, Validators.required],
        })
    }

}

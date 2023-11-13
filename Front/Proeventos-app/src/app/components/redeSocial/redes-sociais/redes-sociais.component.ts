import {Component, Input, TemplateRef} from '@angular/core';
import {AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {RedeSocial} from "../../../models/RedesSociais";
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
    @Input() speakerId: number = -1;
    @Input() eventoId = 0;
    public formRS: FormGroup = {} as FormGroup;
    public redeSocialAtual = {id: 0, name: '', index: 0}
    public redesSociais: RedeSocial[] = [];


    constructor(private formbuilder: FormBuilder,
                private modalRef: BsModalRef,
                private service: RedesSociaisService,
                private spinner: NgxSpinnerService,
                private toastr: ToastrService,
                private router: Router,
                private modalService: BsModalService) {
    }

    retornaTitulo(name: string): string {
        return name === null || name === '' ? 'Nome do Lote' : name;
    }

    public cssValidator(campo: FormControl | AbstractControl | null) {
        return campo ? {'is-invalid': campo.errors && campo.touched} : {};
    }

    adicionarRedeSocial() {
        this.socialMediaArray.push(this.createSocialMedia({id: 0} as RedeSocial))
    }

    salvarRedesSociais() {
        this.isEvento() ? this.saveOnEvento() : this.saveOnPalestrante()
    }

    confirmDeleteRedeSocial() {
        this.modalRef.hide();
        this.spinner.show();
        this.service.deleteOnSpeaker(this.speakerId, this.redeSocialAtual.id).subscribe(
            () => {

                this.toastr.success("Lote deletado com sucesso", "Deletado")
                this.socialMediaArray.removeAt(this.redeSocialAtual.index);
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

    declineDeleteRedeSocial() {
    }

    removerRedeSocial(template: TemplateRef<any>, index: number) {
        //@ts-ignore
        this.redeSocialAtual.id = this.socialMediaArray.get(index + '.id').value;
        this.modalRef = this.modalService.show(template, {class: 'modal-md'});
    }

    public validation() {
        this.formRS = this.formbuilder.group({
            redesSociais: this.formbuilder.array([])
        })
    }

    ngOnInit() {
        this.validation();
        this.loadSocialMedias();
    }

    get getControls() {
        return this.formRS.controls
    }

    public get socialMediaArray(): FormArray {
        return this.formRS.get('redesSociais') as FormArray
    }


    private saveOnEvento() {
        if (this.getControls['redesSociais'].valid) {
            this.spinner.show().then();
            this.service.saveOnEvento(this.eventoId, this.formRS.value['socialMediaArray']).subscribe(
                () => {
                    this.toastr.success("Lotes salvos com sucesso!", 'Success!')
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
        if (this.getControls['redesSociais'].valid) {
            this.spinner.show().then();

            this.service.saveOnSpeaker(this.speakerId, this.formRS.value['redesSociais']).subscribe(
                () => {
                    this.toastr.success("Lotes salvos com sucesso!", 'Sucesso!')
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
        return this.eventoId !== 0 && this.eventoId !== null
    }

    private loadBySpeaker() {
        this.service.getAllByPalestranteId(this.speakerId).subscribe(
            (socialMedias) => {
                socialMedias.forEach(b => this.socialMediaArray.push(this.createSocialMedia(b)))
            }
            ,
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
            (batches: RedeSocial[]) => {
                batches.forEach(b => this.socialMediaArray.push(this.createSocialMedia(b)))
            },
            (error) => {
                console.error(error)
                this.toastr.error("Erro ao carregar os lotes")
            },
            () => {
                this.spinner.hide()
            },)
    }


    private createSocialMedia(socialMedia: RedeSocial):
        FormGroup {
        return this.formbuilder.group({
            id: [socialMedia.id],
            name: [socialMedia.name, Validators.required],
            url: [socialMedia.url, Validators.required],
        })
    }

}

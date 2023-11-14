import {Component, EventEmitter, Input, Output} from '@angular/core';
import {AbstractControlOptions, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ValidatorPasswordField} from "../../../../../helpers/ValidatorPasswordField";
import {PalestranteService} from "../../../../../services/speaker/palestrante.service";
import {Palestrante} from "../../../../../models/Palestrante";
import {ToastrService} from "ngx-toastr";

@Component({
    selector: 'app-perfil-detalhe',
    templateUrl: './perfil-detalhe.component.html',
    styleUrls: ['./perfil-detalhe.component.scss']
})
export class PerfilDetalheComponent {
    @Output() changeFormValue: EventEmitter<any> = new EventEmitter;
    @Input() speakerId: number = 1;
    public form: FormGroup;
    palestrante: Palestrante;

    constructor(private formBuilder: FormBuilder,
                private palestranteService: PalestranteService,
                private toastr: ToastrService) {
        this.form = {} as FormGroup;
        this.palestrante = {} as Palestrante;
    }

    ngOnInit() {
        this.form = this.formBuilder.group({});
        this.validate();
        this.verifyForm();
    }

    private validate() {

        console.log(this.palestrante);
        const formOptions: AbstractControlOptions = {validators: ValidatorPasswordField.MustMatch('password', 'confirmPassword')}
        const specialCharacterPattern = /[!@#$%^&*()\-_=+\[\]{};:'",<>.?\/\\|]/;
        this.form = this.formBuilder.group({
            tittle: ['', [Validators.required]],
            firstName: ['', Validators.required],
            secondName: ['', Validators.required],
            email: ['', [Validators.required, Validators.email]],
            phone: ['', Validators.required],
            userFunction: ['', Validators.required],
            resume: ['', Validators.required],
            password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(50), Validators.pattern(specialCharacterPattern)]],
            confirmPassword: ['', [Validators.required]],
        }, formOptions)
    }

    get getControls(): any {
        return this.form.controls;
    }


    private verifyForm() {
        this.form.valueChanges.subscribe(
            () => {
                this.changeFormValue.emit({...this.form.value})
            },)
        this.palestranteService.getById(this.speakerId).subscribe({
            next:
                (speaker: Palestrante) => {
                    this.palestrante = speaker;

                    const names = speaker.name.split(' ');
                    const firstName = names[0];
                    const secondName = names.slice(1).join(' ');

                    this.form.patchValue({
                        ...speaker,
                        firstName: firstName,
                        secondName: secondName
                    });
                }, error:
                () => {
                },
            complete:
                () => {
                }
        })
    }

    saveSpeaker() {
        this.palestrante = this.form.value;
        this.palestrante.id = this.speakerId;
        this.palestrante.name = `${this.form.value.firstName} ${this.form.value.secondName}`;
        console.log(this.palestrante);
        this.palestranteService.update(this.palestrante).subscribe({
            next: () => {
                this.toastr.success("Deu bom")
            },
            error: (e) => {
                this.toastr.error(e)
                console.log(e)
            },
            complete: () => {
            },
        })
    }
}

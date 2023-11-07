import {Component, EventEmitter, Output} from '@angular/core';
import {AbstractControlOptions, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ValidatorPasswordField} from "../../../../../helpers/ValidatorPasswordField";

@Component({
    selector: 'app-perfil-detalhe',
    templateUrl: './perfil-detalhe.component.html',
    styleUrls: ['./perfil-detalhe.component.scss']
})
export class PerfilDetalheComponent {
    @Output() changeFormValue = new EventEmitter;
    public form: FormGroup;

    constructor(private formBuilder: FormBuilder) {
        this.form = new FormGroup<any>({});
    }

    ngOnInit() {
        this.validate();
        this.verifyForm();
    }

    private validate() {
        const formOptions: AbstractControlOptions = {validators: ValidatorPasswordField.MustMatch('password', 'confirmPassword')}
        const specialCharacterPattern = /[\!\@\#\$\%\^\&\*\(\)\-\_\=\+\[\]\{\};:\'",<>\.\?\/\\|]/;
        this.form = this.formBuilder.group({

            tittle: ['', [Validators.required]],
            firstName: ['', Validators.required],
            secondName: ['', Validators.required],
            email: ['', [Validators.required, Validators.email]],
            phone: ['', Validators.required],
            userFunction: ['', Validators.required],
            description: ['', Validators.required],
            password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(50), Validators.pattern(specialCharacterPattern)]],
            confirmPassword: ['', [Validators.required]],
        }, formOptions)
    }

    get getControls(): any {
        return this.form.controls;
    }

    protected readonly onsubmit = onsubmit;

    private verifyForm() {
        this.form.valueChanges.subscribe(
            () => {
                this.changeFormValue.emit({...this.form.value})
            },)
    }
}

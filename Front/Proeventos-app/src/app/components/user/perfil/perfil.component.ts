import {Component, OnInit} from '@angular/core';
import {AbstractControlOptions, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ValidatorPasswordField} from "../../../helpers/ValidatorPasswordField";

@Component({
    selector: 'app-perfil',
    templateUrl: './perfil.component.html',
    styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

    public form: FormGroup;

    constructor(private formBuilder: FormBuilder) {
        this.form = new FormGroup<any>({});
    }

    ngOnInit() {
        this.validate();
    }

    get getControls(): any {
        return this.form.controls;
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
        },formOptions)
    }
}

import {Component, OnInit} from '@angular/core';
import {AbstractControlOptions, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ValidatorPasswordField} from "../../../helpers/ValidatorPasswordField";

@Component({
    selector: 'app-registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
    form: FormGroup;


    constructor(private fb: FormBuilder) {
        this.form = new FormGroup({})
    }

    ngOnInit() {
        this.validate();
    }

    public validate(): void {
        const formOptions: AbstractControlOptions = {validators: ValidatorPasswordField.MustMatch('password', 'confirmPassword')}
        const specialCharacterPattern = /[\!\@\#\$\%\^\&\*\(\)\-\_\=\+\[\]\{\};:\'",<>\.\?\/\\|]/;
        this.form = this.fb.group({
            firstName: ['', [Validators.required, Validators.minLength(3)]],
            secondName: ['', [Validators.required, Validators.minLength(3)]],
            email: ['', [Validators.required, Validators.email]],
            user: ['', [Validators.required, Validators.minLength(3)]],
            password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(50), Validators.pattern(specialCharacterPattern)]],
            confirmPassword: ['', [Validators.required]],

        }, formOptions)

    }

    get getControls(): any {
        return this.form.controls;
    }

}

import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

  form: FormGroup

  constructor(private fb: FormBuilder) {
    this.form = new FormGroup({})
  }

  get getControls(): any {
    return this.form.controls;
  }

  ngOnInit() {
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
      imgURL: ['', Validators.required],
      phone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    })
  }
}

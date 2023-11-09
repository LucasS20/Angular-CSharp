import {Component, Input} from '@angular/core';
import {AbstractControl, Form, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {PalestranteService} from "../../../services/speaker/palestrante.service";
import {ToastrService} from "ngx-toastr";
import {NgxSpinnerService} from "ngx-spinner";
import {debounceTime, map} from "rxjs";
import {Palestrante} from "../../../models/Palestrante";

@Component({
  selector: 'app-palestrante-detalhe',
  templateUrl: './palestrante-detalhe.component.html',
  styleUrls: ['./palestrante-detalhe.component.scss']
})
export class PalestranteDetalheComponent {
  @Input() palestranteId: number = 1;
  speaker: Palestrante = {} as Palestrante;
  form: FormGroup;
  corDaDescricao: any;
  formSituation: any;
  formBuilder: FormBuilder = new FormBuilder;


  constructor(private _formBuilder: FormBuilder,
              private palestranteService: PalestranteService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService) {
    this.form = this.formBuilder.group({
      miniCurriculo: ['', Validators.required]
    })
  }

  ngOnInit() {
    this.verifyForm();
  }

  get f() {
    return this.form.controls;
  }

  private verifyForm() {
    this.form.valueChanges.pipe(
      map(() => {
        this.formSituation = 'Minicurriculo está sendo Atualizado!'
        this.corDaDescricao = 'text-warning'
      }), debounceTime(1000)
    ).subscribe(() => {

      this.formSituation = 'Minicurriculo foi Atualizado!'
      this.corDaDescricao = 'text-success'
    })
  }

  public cssValidator(campo: FormControl | AbstractControl | null) {
    return campo ? {'is-invalid': campo.errors && campo.touched} : {};
  }

  public get getControls(): any {
    return this.form?.controls;
  }


}

import {Component, OnInit} from '@angular/core';
import {NgxSpinnerService} from "ngx-spinner";
import {Palestrante} from "../../../models/Palestrante";


@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  public formValue: any = {};
  constructor(private spinner: NgxSpinnerService) {}

  ngOnInit() {
    this.spinner.show();
    setTimeout(() => {
      this.spinner.hide();
    }, 500);
  }

  public setFormValue($event: any) {
    this.formValue = $event;
  }

  public isSpeaker(): boolean {
    return this.formValue.userFunction === 'Palestrante'
  }
}

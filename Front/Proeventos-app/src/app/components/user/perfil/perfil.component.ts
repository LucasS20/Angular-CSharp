import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  public formValue: any = {};


  constructor() {
  }

  ngOnInit() {

  }

  public setFormValue($event: any) {
    this.formValue = $event;
    console.log($event);
  }

  public isSpeaker(): boolean {
    return this.formValue.userFunction === 'Palestrante'
  }


}

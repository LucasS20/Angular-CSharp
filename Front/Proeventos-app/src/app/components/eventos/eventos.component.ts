import {Component, OnInit, TemplateRef} from '@angular/core';
import {EventService} from "../../services/event.service";
import {Evento} from "../../models/Evento";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {ToastrService} from "ngx-toastr";
import {NgxSpinnerService} from "ngx-spinner";


@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {
  modalRef?: BsModalRef;
  events: Evento[] = []
  widthImage: number = 50;
  margin: number = 10;
  showImage: boolean = true;
  filteredEvents: Evento[] = [];
  _listFilter: string = '';

  constructor(private service: EventService,
              private modalService: BsModalService,
              private toastrService: ToastrService,
              private spinner: NgxSpinnerService) {
  }

  public get listFilter() {
    return this._listFilter;
  }

  public set listFilter(value: string) {
    this._listFilter = value;
    value = value.toLowerCase();
    this.filteredEvents = this._listFilter ? this.events.filter((e: Evento) => e.theme.toLowerCase().includes(value) || e.local.toLowerCase().includes(value)) : this.events;
  }

  public ngOnInit(): void {
    this.spinner.show();

    setTimeout(() => {
      this.spinner.hide();
    }, 5000);

    this.getEvents()
  }

  public getEvents(): void {
    const observer = {
      next: (_events: Evento[]) => {
        this.events = _events;
        this.filteredEvents = _events;
      },
      error: (error: any) => {
        console.log(error)
      },
      complete: () => {
      }
    };
    this.service.getEvents().subscribe(observer)
  }

  public changeState() {
    this.showImage = !this.showImage;
  }


  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.toastrService.success("Operação realizada com sucesso");
    this.modalRef?.hide();
  }

  decline(): void {
    this.modalRef?.hide();
  }


}

import {Component, OnInit, TemplateRef} from '@angular/core';
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {ToastrService} from "ngx-toastr";
import {NgxSpinnerService} from "ngx-spinner";
import {EventService} from "../../../services/event.service";
import {Evento} from "../../../models/Evento";
import {Router} from "@angular/router";


@Component({
    selector: 'app-evento-listagem',
    templateUrl: './evento-listagem.component.html',
    styleUrls: ['./evento-listagem.component.scss']
})
export class EventoListagemComponent implements OnInit {
    modalRef?: BsModalRef;
    events: Evento[] = []
    widthImage: number = 50;
    margin: number = 10;
    showImage: boolean = true;
    filteredEvents: Evento[] = [];
    _listFilter: string = '';
    eventoId: number = 0;

    constructor(private service: EventService,
                private modalService: BsModalService,
                private toastrService: ToastrService,
                private spinner: NgxSpinnerService,
                private router: Router) {
    }

    redirectDetalhes(id: number) {
        this.router.navigate([`eventos/detalhe/${id}`]);
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
        }, 500);

        this.loadEvents()
    }

    public loadEvents(): void {
        this.spinner.show();
        const observer = {
            next: (_events: Evento[]) => {
                this.events = _events;
                this.filteredEvents = _events;
            },
            error: (error: any) => {
                console.log(error)
                this.toastrService.error("Error while trying to load the events", "ERROR")
            },
            complete: () => {
                this.spinner.hide();
            }
        };
        this.service.getAll().subscribe(observer)
    }

    public changeState() {
        this.showImage = !this.showImage;
    }


    openModal(event: any, template: TemplateRef<any>, id: number) {
        event.stopPropagation();
        this.eventoId = id;
        this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
    }

    confirm(): void {
        this.spinner.show();
        this.service.delete(this.eventoId).subscribe(
            (result: any) => {
                console.log(result);
                    this.toastrService.success("Event deleted successfully");
                    this.spinner.hide();
                    this.loadEvents()
            },
            (error) => {
                console.error(error)
                this.spinner.hide()
                this.toastrService.error("Error when deleting event:" + this.eventoId, "Error");
            },
            () => {
                this.spinner.hide();
            },);
        this.modalRef?.hide();
    }

    decline(): void {
        this.modalRef?.hide();
    }


}

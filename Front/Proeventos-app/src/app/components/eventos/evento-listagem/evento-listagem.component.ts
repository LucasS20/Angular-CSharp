import {Component, OnInit, TemplateRef} from '@angular/core';
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {ToastrService} from "ngx-toastr";
import {NgxSpinnerService} from "ngx-spinner";
import {EventService} from "../../../services/event/event.service";
import {Evento} from "../../../models/Evento";
import {Router} from "@angular/router";
import {PageChangedEvent} from "ngx-bootstrap/pagination";
import {Pagination, PaginationResult} from "../../../models/Pagination";


@Component({
    selector: 'app-evento-listagem',
    templateUrl: './evento-listagem.component.html',
    styleUrls: ['./evento-listagem.component.scss']
})
export class EventoListagemComponent implements OnInit {
    modalRef?: BsModalRef;
    events: Evento[] = []
    showImage: boolean = true;
    filteredEvents: Evento[] = [];
    _listFilter: string = '';
    eventoId: number = 0;
    pagination: Pagination = {} as Pagination

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
        this.pagination = {currentPage: 1, itemsPerPage: 3, totalItems: 1} as Pagination
        this.spinner.show();

        setTimeout(() => {
            this.spinner.hide();
        }, 500);

        this.loadEvents()
        // console.log(this.pagination);
    }

    public loadEvents(): void {
        const observer = {
            next: (response: PaginationResult<Evento[]>) => {
                this.events = response.result;
                this.filteredEvents = response.result;

                this.pagination=response.pagination
                // console.log(this.pagination)
            },
            error: (error: any) => {
                console.log(error)
                this.toastrService.error("Error while trying to load the events", "ERROR")
            },
            complete: () => {
            }
        };

        this.service.getAll(this.pagination.currentPage, this.pagination.itemsPerPage).subscribe(observer).add(() => this.spinner.hide())
    }

    pageChanged(event: PageChangedEvent) {
        this.pagination.currentPage = event.page;
        this.loadEvents();
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
                this.toastrService.success("Event deleted successfully");
                this.loadEvents()
            },
            (error) => {
                console.error(error);
                this.toastrService.error("Error when deleting event:" + this.eventoId, "Error");
            }
            ,).add(() => this.spinner.hide()
        );
        this.modalRef?.hide();
    }


    decline(): void {
        this.modalRef?.hide();
    }

    loteVigente(evento: Evento): string {
        const dataAtual = new Date();

        const loteVigente = evento.batches.find(lote =>
            new Date(lote.startDate) <= dataAtual && new Date(lote.endDate) >= dataAtual
        );

        return loteVigente ? loteVigente.name : 'Nenhum lote aberto';
    }


}

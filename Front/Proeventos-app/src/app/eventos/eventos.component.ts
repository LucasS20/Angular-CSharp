import {Component} from '@angular/core';
import {HttpClient} from "@angular/common/http";


@Component({
    selector: 'app-eventos',
    templateUrl: './eventos.component.html',
    styleUrls: ['./eventos.component.scss']
})
export class EventosComponent {
    public events = this.getEvents();
    public widthImage: number = 50;
    public margin: number = 10;
    public showImage: boolean = true;
    public filteredEvents: any = [];
    private _listFilter: string = '';

    constructor(private http: HttpClient) {

    }

    get listFilter() {
        return this._listFilter;
    }

    set listFilter(value: string) {
        this._listFilter = value;
        value = value.toLowerCase();
        this.filteredEvents = this._listFilter ? this.events.filter((e: any) => e.theme.toLowerCase().includes(value) || e.local.toLowerCase().includes(value)) : this.events;
    }

    ngOnInit(): void {
        this.getEvents()
    }

    public getEvents(): any {
        this.http.get("https://localhost:7109/api/event").subscribe(
            (response: any) => {
                this.events = response;
                this.filteredEvents = response;
            },
            (error: any) => console.log(error),
        );
    }

    public changeState() {
        this.showImage = !this.showImage;
    }


}

import {Component, OnInit} from '@angular/core';
import {EventService} from "../services/event.service";
import {Evento} from "../models/Evento";


@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {
  events: Evento[] = []
  widthImage: number = 50;
  margin: number = 10;
  showImage: boolean = true;
  filteredEvents: Evento[] = [];
   _listFilter: string = '';

  constructor(private service: EventService) {

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

}

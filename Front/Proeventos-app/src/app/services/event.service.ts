import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, tap} from "rxjs";
import {Evento} from "../models/Evento";

@Injectable({
  providedIn: 'root'
})
export class EventService {
  baseURL = "https://localhost:7109/api/event";

  constructor(private http: HttpClient) {

  }

  getEvents(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.baseURL).pipe(
      tap(data => console.log('Eventos recebidos: ', data))
    );
  }

  getEventsByTheme(theme: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseURL}/theme/${theme}`);
  }

  getEventById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseURL}/${id}`);
  }
}

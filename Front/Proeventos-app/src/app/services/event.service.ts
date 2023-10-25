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

    getAll(): Observable<Evento[]> {
        return this.http.get<Evento[]>(this.baseURL).pipe(
            tap(data => console.log('Eventos recebidos: ', data))
        );
    }

    getByTheme(theme: string): Observable<Evento[]> {
        return this.http.get<Evento[]>(`${this.baseURL}/theme/${theme}`);
    }

    getById(id: number): Observable<Evento> {
        return this.http.get<Evento>(`${this.baseURL}/${id}`);
    }

    post(evento: Evento): Observable<Evento> {
        return this.http.post<Evento>(this.baseURL, evento);
    }

    put(id: number, evento: Evento): Observable<Evento> {
        return this.http.put<Evento>(`${this.baseURL}/${id}`, evento);
    }

    delete(id: number):Observable<any> {
        return this.http.delete(`${this.baseURL}/${id}`);
    }

}

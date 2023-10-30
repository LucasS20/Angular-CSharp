import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, tap} from "rxjs";
import {Evento} from "../../models/Evento";

@Injectable({
    providedIn: 'root'
})
export class EventService {
    baseURL = "https://localhost:7109/api/event";

    constructor(private http: HttpClient) {

    }

    public   getAll(): Observable<Evento[]> {
        return this.http.get<Evento[]>(this.baseURL).pipe(
            tap(data => console.log('Eventos recebidos: ', data))
        );
    }

    public  getByTheme(theme: string): Observable<Evento[]> {
        return this.http.get<Evento[]>(`${this.baseURL}/theme/${theme}`);
    }

    public  getById(id: number): Observable<Evento> {
        return this.http.get<Evento>(`${this.baseURL}/${id}`);
    }

  public  post(evento: Evento): Observable<Evento> {
        return this.http.post<Evento>(this.baseURL, evento);
    }

    public   put( evento: Evento): Observable<Evento> {
        return this.http.put<Evento>(`${this.baseURL}/${evento.id}`, evento);
    }

    public  delete(id: number) {
        return this.http.delete(`${this.baseURL}/${id}`);
    }

}

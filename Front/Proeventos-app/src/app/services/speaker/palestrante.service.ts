import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Palestrante} from "../../models/Palestrante";

@Injectable({
    providedIn: 'root'
})
export class PalestranteService {

    baseURL = "https://localhost:7109/api/speaker";

    constructor(private http: HttpClient) {

    }

    public getAll(): Observable<Palestrante[]> {
        return this.http.get<Palestrante[]>(this.baseURL);
    }

    public getById(id: number): Observable<Palestrante> {
        return this.http.get<Palestrante>(`${this.baseURL}/${id}`);
    }

    public create(Speaker: Palestrante): Observable<Palestrante> {
        return this.http.post<Palestrante>(this.baseURL, Speaker);
    }

    public update(Speaker: Palestrante): Observable<Palestrante> {
        return this.http.put<Palestrante>(`${this.baseURL}/${Speaker.id}`, Speaker);
    }

    public delete(id: number) {
        return this.http.delete(`${this.baseURL}/${id}`);
    }
}

import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Speaker} from "../../models/Speaker";

@Injectable({
    providedIn: 'root'
})
export class SpeakerService {

    baseURL = "https://localhost:7109/api/speaker";

    constructor(private http: HttpClient) {

    }

    public getAll(): Observable<Speaker[]> {
        return this.http.get<Speaker[]>(this.baseURL);
    }

    public getById(id: number): Observable<Speaker> {
        return this.http.get<Speaker>(`${this.baseURL}/${id}`);
    }



    public create(Speaker: Speaker): Observable<Speaker> {
        return this.http.post<Speaker>(this.baseURL, Speaker);
    }

    public update(Speaker: Speaker): Observable<Speaker> {
        return this.http.put<Speaker>(`${this.baseURL}/${Speaker.id}`, Speaker);
    }

    public delete(id: number) {
        return this.http.delete(`${this.baseURL}/${id}`);
    }
}

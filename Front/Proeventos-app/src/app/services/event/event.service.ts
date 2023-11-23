import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {map, Observable, take} from "rxjs";
import {Evento} from "../../models/Evento";
import {PaginationResult} from "../../models/Pagination";

@Injectable({
    providedIn: 'root'
})
export class EventService {
    baseURL = "https://localhost:7109/api/event";

    constructor(private http: HttpClient) {

    }

    public getAll(page: number, itemsPerPage: number): Observable<PaginationResult<Evento[]>> {
        const paginatedResult: PaginationResult<Evento[]> = {
            result: [],
            pagination: {
                currentPage: page,
                itemsPerPage: itemsPerPage,
                totalItems: 0,
                totalPages: 0
            }
        };
        let params = new HttpParams;

        if (page != null && itemsPerPage != null) {
            params = params.append('pageNumber', page.toString())
            params = params.append('pagesize', itemsPerPage.toString());
        }

        return this.http.get<Evento[]>(this.baseURL, {observe: 'response', params})
            .pipe(take(1), map((response) => {
                if (response.body !== null) {
                    paginatedResult.result = response.body;
                    console.log(response.headers);
                    if (response.headers.has('Pagination')) {
                        const paginationHeader = response.headers.get('Pagination');
                        if (paginationHeader !== null) {
                            console.log(paginatedResult.pagination);
                            paginatedResult.pagination = JSON.parse(paginationHeader);
                            console.log(paginatedResult.pagination);
                        }
                    }
                }
                return paginatedResult
            }));
    }


    public getById(id: number) {
        return this.http.get<Evento>(`${this.baseURL}/${id}`);
    }

    public post(evento: Evento): Observable<Evento> {
        return this.http.post<Evento>(this.baseURL, evento);
    }

    public put(evento: Evento): Observable<Evento> {
        return this.http.put<Evento>(`${this.baseURL}/${evento.id}`, evento);
    }

    public delete(id: number) {
        return this.http.delete(`${this.baseURL}/${id}`);
    }

    public urlUpload(eventId: number, file: File) {
        //@ts-ignore
        const fileToUpload = file[0] as File;
        const formData = new FormData()
        formData.append('file', fileToUpload);
        return this.http.post<Evento>(`${this.baseURL}/upload-image/${eventId}`, formData)
    }

}

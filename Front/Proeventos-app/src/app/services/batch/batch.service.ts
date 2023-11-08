import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Batch} from "../../models/Batch";

@Injectable({
  providedIn: 'root'
})
export class BatchService {
  private defaultUrl: string = "https://localhost:7109/api/lot"

  constructor(private http: HttpClient) {
  }


  public getByEventId(eventId: number): Observable<Batch[]> {
    return this.http.get<Batch[]>(`${this.defaultUrl}/${eventId}`);
  }

  public saveBatch(eventId: number, batches: Batch[]): Observable<Batch[]> {
    return this.http.put<Batch[]>(`${this.defaultUrl}/${eventId}`, batches)
  }

  public delete(eventId: number, batchId: number) {
    return this.http.delete(`${this.defaultUrl}/${eventId}/${batchId}`)
  }
}

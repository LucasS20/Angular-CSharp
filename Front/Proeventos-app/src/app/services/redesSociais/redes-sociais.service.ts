import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {RedeSocial} from "../../models/RedesSociais";

@Injectable({
  providedIn: 'root'
})
export class RedesSociaisService {
  baseURL = "https://localhost:7109/api/SocialMedia";

  constructor(private http: HttpClient) {
  }

  public getAllByEventId(eventId: number) {
    return this.http.get<RedeSocial[]>(`${this.baseURL}/GetAllByEventId/${eventId}`)
  }

  public getAllByPalestranteId(palestranteId: number) {
    return this.http.get<RedeSocial[]>(`${this.baseURL}/GetAllBySpeakerId/${palestranteId}`)
  }

  public getByPalestranteId(palestranteId: number, socialMediaId: number) {
    return this.http.get<RedeSocial>(`${this.baseURL}/GetBySpeakerId/${palestranteId}/${socialMediaId}`)
  }

  public getByEventId(eventId: number, socialMediaId: number) {
    return this.http.get<RedeSocial>(`${this.baseURL}/GetByEventId/${eventId}/${socialMediaId}`)
  }

  public deleteOnSpeaker(speakerId: number, socialMediaId: number) {
    return this.http.delete(`${this.baseURL}/DeleteOnSpeaker/${speakerId}/${socialMediaId}`)
  }

  public deleteOnEvent(eventId: number, socialMediaId: number) {
    return this.http.delete(`${this.baseURL}/deleteOnEvent/${eventId}`)
  }

  public saveOnSpeaker(speakerId: number, socialMedias: RedeSocial[]) {
    return this.http.put<RedeSocial>(`${this.baseURL}/SaveOnSpeaker/${speakerId}`, socialMedias)
  }

  public saveOnEvento(eventoId: number, socialMedias: RedeSocial[]) {
    return this.http.put<RedeSocial>(`${this.baseURL}/SaveOnEvent/${eventoId}`, socialMedias)
  }

}

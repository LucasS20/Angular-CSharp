import {Palestrante} from "./Palestrante";

export interface RedesSociais {
  Id: number;
  Name: string;
  Url: string;
  EventId: number;
  Event: Event;
  SpeakerId: number;
  Speaker: Palestrante;
}

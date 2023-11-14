import {Batch} from "./Batch";
import {RedeSocial} from "./RedesSociais";
import {EventSpeaker} from "./EventSpeaker";

export interface Evento {
  date?: Date;
  email: string;
  id: number;
  base64: string;
  local: string;
  numberOfPeoples: number;
  phone: string;
  socialMedias: RedeSocial[];
  speakersEvent: EventSpeaker[] ;
  lots: Batch[];
  theme: string;
}


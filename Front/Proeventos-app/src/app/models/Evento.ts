import {Batch} from "./Batch";
import {RedesSociais} from "./RedesSociais";
import {EventSpeaker} from "./EventSpeaker";

export interface Evento {
  date?: Date;
  email: string;
  id: number;
  imgUrl: string;
  local: string;
  numberOfPeoples: number;
  phone: string;
  socialMedias: RedesSociais[];
  speakersEvent: EventSpeaker[] ;
  lots: Batch[];
  theme: string;
}


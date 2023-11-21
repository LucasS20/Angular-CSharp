import {Batch} from "./Batch";
import {RedeSocial} from "./RedesSociais";
import {EventSpeaker} from "./EventSpeaker";
import {Palestrante} from "./Palestrante";

export interface Evento {
  date?: Date;
  email: string;
  id: number;
  base64: string;
  local: string;
  numberOfPeoples: number;
  phone: string;
  socialMedias: RedeSocial[];
  speakerId: number;
  batches: Batch[];
  theme: string;
}


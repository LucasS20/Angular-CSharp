import {RedesSociais} from "./RedesSociais";
import {EventSpeaker} from "./EventSpeaker";


export interface Palestrante {
  id: number;
  name: string;
  resume: string;
  imageURL: string;
  phone: string;
  email: string;
  socialMedias: RedesSociais[];
  eventSpeakers: EventSpeaker;
}

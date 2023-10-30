import {Batch} from "./Batch";
import {SocialMedia} from "./SocialMedia";
import {EventSpeaker} from "./EventSpeaker";

export interface Evento {
  date?: Date;
  email: string;
  id: number;
  imgUrl: string;
  local: string;
  numberOfPeoples: number;
  phone: string;
  socialMedias: SocialMedia[];
  speakersEvent: EventSpeaker[] ;
  lots: Batch[];
  theme: string;
}


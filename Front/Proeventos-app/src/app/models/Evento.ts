import {Lot} from "./Lot";
import {SocialMedia} from "./SocialMedia";
import {EventSpeaker} from "./EventSpeaker";

export interface Evento {
  id: number;
  local: string;
  date?: Date;
  theme: string;
  numberOfPeoples: number;
  imgURL: string;
  phone: string;
  email: string;
  lots: Lot[];
  socialMedias: SocialMedia[];
  speakersEvent: EventSpeaker ;
}


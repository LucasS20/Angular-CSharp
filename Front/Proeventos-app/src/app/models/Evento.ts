import {Lot} from "./Lot";
import {SocialMedia} from "./SocialMedia";
import {EventSpeaker} from "./EventSpeaker";

export interface Evento {
  date?: Date;
  email: string;
  id: number;
  imgUrl: string;
  local: string;
  lots: Lot[];
  numberOfPeoples: number;
  phone: string;
  socialMedias: SocialMedia[];
  speakersEvent: EventSpeaker[] ;
  theme: string;
}


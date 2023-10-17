import {Lot} from "./Lot";
import {SocialMedia} from "./SocialMedia";
import {EventSpeaker} from "./EventSpeaker";



export interface Event {
  Id: number;
  Local: string;
  Date?: Date;
  Theme: string;
  NumberOfPeoples: number;
  imgURL: string;
  phone: string;
  email: string;
  Lots: Lot[];
  SocialMedias: SocialMedia[];
  SpeakersEvent: EventSpeaker[];
}

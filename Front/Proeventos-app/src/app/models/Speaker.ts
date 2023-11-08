import {SocialMedia} from "./SocialMedia";
import {EventSpeaker} from "./EventSpeaker";


export interface Speaker {
  id: number;
  name: string;
  resume: string;
  imageURL: string;
  phone: string;
  email: string;
  socialMedias: SocialMedia[];
  eventSpeakers: EventSpeaker;
}

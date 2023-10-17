import {SocialMedia} from "./SocialMedia";
import {EventSpeaker} from "./EventSpeaker";


export interface Speaker {
  Id: number;
  Name: string;
  Resume: string;
  ImageURL: string;
  phone: string;
  email: string;
  SocialMedias: SocialMedia[];
  EventSpeakers: EventSpeaker;
}

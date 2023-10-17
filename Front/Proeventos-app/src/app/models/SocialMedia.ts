import {Speaker} from "./Speaker";

export interface SocialMedia {
  Id: number;
  Name: string;
  Url: string;
  EventId: number;
  Event: Event;
  SpeakerId: number;
  Speaker: Speaker;
}

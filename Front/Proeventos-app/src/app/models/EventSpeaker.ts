import {Speaker} from "./Speaker";

export interface EventSpeaker {
  EventId: number;
  Event: Event;
  SpeakerId: number;
  Speaker: Speaker;
}

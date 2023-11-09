import {Palestrante} from "./Palestrante";

export interface EventSpeaker {
  EventId: number;
  Event: Event;
  SpeakerId: number;
  Speaker: Palestrante;
}

import {Palestrante} from "./Palestrante";

export interface RedeSocial {
  id: number;
  name: string;
  url: string;
  eventId: number;
  event: Event;
  speakerId: number;
  speaker: Palestrante;
}

import {RedeSocial} from "./RedesSociais";


export interface Palestrante {
  id: number;
  name: string;
  resume: string;
  imageURL: string;
  phone: string;
  email: string;
  socialMedias: RedeSocial[];
  events: Event[];
}

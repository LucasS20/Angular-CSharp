import {Evento} from "./Evento";

export interface Batch {
    id: number;
    name: string;
    price: number;
    startdate?: Date;
    enddate?: Date;
    ticketsAmount: number;
    eventid: number;
    _event: Evento;
}

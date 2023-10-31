import {Evento} from "./Evento";

export interface Batch {
    id: number;
    name: string;
    price: number;
    startDate?: Date;
    endDate?: Date;
    ticketAmount: number;
    eventid: number;
    _event: Evento;
}

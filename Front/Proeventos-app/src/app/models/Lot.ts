export interface Lot {
  id: number;
  name: string;
  price: number;
  startdate?: Date;
  enddate?: Date;
  quantidade: number;
  eventid: number;
}

import { Pipe, PipeTransform } from '@angular/core';
import {DatePipe} from "@angular/common";

@Pipe({
  name: 'pipeFormatSemHora'
})
export class PipeFormatSemHoraPipe implements PipeTransform {

  transform(value: any, _args?: any): any {
    const datePipe: DatePipe = new DatePipe('en-US');
    return datePipe.transform(value, 'dd/MM/yyyy');
  }

}

import {Pipe, PipeTransform} from '@angular/core';
import {DatePipe} from "@angular/common";
import {Constants} from "../util/constants";

@Pipe({
  name: 'dateTimeFormat'
})
export class DateFormatPipe extends DatePipe implements PipeTransform {

  override transform(value: any, ...args: any[]): any {
    return super.transform(value, Constants.DATE_TIME_FMT);
  }

}

import {Component, EventEmitter, Input, OnInit, Output, SimpleChanges, ViewEncapsulation} from '@angular/core';
import {MatCalendarCellClassFunction, MatCalendarCellCssClasses} from "@angular/material/datepicker";
import {DELETE} from "@angular/cdk/keycodes";
import moment from "moment-timezone";

@Component({
  selector: 'app-availability-calendar',
  templateUrl: './availability-calendar.component.html',
  styleUrl: './availability-calendar.component.css',
  encapsulation: ViewEncapsulation.None
})
export class AvailabilityCalendarComponent  {
  @Input() availableDates !: Date[];
  @Input() isSelf !: boolean;
  @Output() toggleDateAvailability = new EventEmitter<Date>();

  selectedDate: any;

  ngOnChanges(changes: SimpleChanges) {
    if (changes.availableDates) {
      this.dateClass = this.createDateClassFunction();
    }
  }

  createDateClassFunction(): MatCalendarCellClassFunction<Date> {
    return (date: Date): MatCalendarCellCssClasses => {
      console.log(date);
      console.log(this.availableDates);
      const alternative = moment.tz(date, 'Europe/Bucharest').format().split('+')[0];
      console.log(alternative);
      if (this.availableDates.find(d => (d as any) == alternative)) {
        console.log("true");
        return 'available-date';
      } else {
        return '';
      }
    };
  }

  dateClass = this.createDateClassFunction();

  onDateSelected(date: Date | null) {
    if (!date) {
      return;
    }
    this.toggleDateAvailability.emit(date);
  }

  deleteDate(date: Date) {
    this.toggleDateAvailability.emit(date);
  }

}

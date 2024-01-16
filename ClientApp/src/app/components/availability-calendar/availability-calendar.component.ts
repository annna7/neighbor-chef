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
  @Input() availableDates !: string[];
  @Input() isSelf !: boolean;
  @Output() toggleDateAvailability = new EventEmitter<Date>();

  selectedDate: any;
  dateClass = (date: any):  MatCalendarCellCssClasses => {
    return this.availableDates.some(d => d.startsWith(date.toISOString().split('T')[0])) ? 'available-date' : '';
  }

  dateFilter = (date: Date | null): boolean => {
    return this.availableDates.some(d => d.startsWith(date?.toISOString().split('T')[0] || ''));
  }

  onDateSelected(date: any) {
    if (!date) {
      return;
    }
    this.toggleDateAvailability.emit(date);
    return;
  }

  deleteDate(date: any) {
    this.toggleDateAvailability.emit(date);
    return;
  }

}

import {Component, EventEmitter, Input, Output} from '@angular/core';
import {MatCalendarCellClassFunction} from "@angular/material/datepicker";
import {DELETE} from "@angular/cdk/keycodes";

@Component({
  selector: 'app-availability-calendar',
  templateUrl: './availability-calendar.component.html',
  styleUrl: './availability-calendar.component.css'
})
export class AvailabilityCalendarComponent {
  @Input() availableDates !: Date[];
  @Input() isSelf !: boolean;
  @Output() toggleDateAvailability = new EventEmitter<Date>();

  selectedDate: any;

  onDateSelected(date: Date | null) {
    if (!date) {
      return;
    }
    console.log('transmitting date', date);
    this.toggleDateAvailability.emit(date);
  }

  deleteDate(date: Date) {
    this.toggleDateAvailability.emit(date);
  }

  dateClass: MatCalendarCellClassFunction<Date> = (cellDate: Date, view: "month" | "year" | "multi-year") => {
    return '';
  }

  protected readonly DELETE = DELETE;
}

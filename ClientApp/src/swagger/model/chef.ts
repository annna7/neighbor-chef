import {Person} from "./person";
import {Review} from "./review";
import {Order} from "./order";

export interface Chef extends Person {
  advanceNoticeDays: number;
  description: string;
  maxOrdersPerDay: number;
  availableDates: Date[];
  reviewsReceived: Review[];
  ordersReceived: Order[];
}

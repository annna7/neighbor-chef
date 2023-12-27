import {Person} from "./person";
import {Review} from "./review";
import {Order} from "./order";
import {Meal} from "./meal";

export interface Chef extends Person {
  advanceNoticeDays: number;
  description: string;
  maxOrdersPerDay: number;
  availableDates: Date[];
  reviewsReceived: Review[];
  ordersReceived: Order[];
  meals: Meal[];
}

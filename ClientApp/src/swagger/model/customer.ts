import {Person} from './person';
import {Order} from "./order";
import {Review} from "./review";

export interface Customer extends Person {
  ordersPlaced: Order[];
  reviewsReceived: Review[];
}

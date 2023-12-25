import {OrderStatus} from "./orderStatus";
import {OrderMeal} from "./orderMeal";

export interface Order {
  id: string;
  chefId: string;
  customerId: string;
  observations: string;
  deliveryDate: string;
  status: OrderStatus;
  orderMeals: OrderMeal[];
}

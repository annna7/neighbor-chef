import {Meal} from "./meal";

export interface OrderMeal {
  id: string;
  mealId: string;
  quantity: number;
  meal: Meal;
}

import {Component, EventEmitter, Input, Output} from '@angular/core';
import { Meal } from '../../../swagger';
import {CartService} from "../../services/cart.service";

@Component({
  selector: 'app-meal-card',
  templateUrl: './meal-card.component.html',
  styleUrls: ['./meal-card.component.css']
})
export class MealCardComponent {
  @Input() meal!: Meal;
  @Input() isChef!: boolean;
  @Input() quantity!: number;
  @Output() updateQuantity = new EventEmitter<{ meal: Meal, quantity: number }>();
  @Output() isMealInCart = new EventEmitter<{ meal: Meal }>();
  @Output() deleteMeal = new EventEmitter<Meal>();
  @Output() editMeal = new EventEmitter<Meal>();
  @Output() addMealToCart = new EventEmitter<any>();
  showIngredients: boolean = false;

  constructor(protected cartService: CartService) {}

  toggleIngredients() {
    this.showIngredients = !this.showIngredients;
  }

  get ingredients(): string[] {
    return this.meal.ingredients;
  }
}

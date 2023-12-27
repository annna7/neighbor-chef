import { Component, Input } from '@angular/core';
import { Meal } from '../../../swagger';

@Component({
  selector: 'app-meal-card',
  templateUrl: './meal-card.component.html',
  styleUrls: ['./meal-card.component.css']
})
export class MealCardComponent {
  @Input() meal!: Meal;
  showIngredients: boolean = false;

  toggleIngredients() {
    this.showIngredients = !this.showIngredients;
  }

  get ingredients(): string[] {
    return this.meal.ingredients;
  }
}

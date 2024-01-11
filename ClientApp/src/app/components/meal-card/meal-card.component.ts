import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { Meal } from '../../../swagger';
import {CartService} from "../../services/cart.service";
import {UserService} from "../../services";

@Component({
  selector: 'app-meal-card',
  templateUrl: './meal-card.component.html',
  styleUrls: ['./meal-card.component.css']
})
export class MealCardComponent implements OnInit {
  @Input() searchView = false;
  @Input() meal!: Meal;
  @Input() isChef!: boolean;
  @Input() quantity!: number;
  @Output() updateQuantity = new EventEmitter<{ meal: Meal, quantity: number }>();
  @Output() isMealInCart = new EventEmitter<{ meal: Meal }>();
  @Output() deleteMeal = new EventEmitter<Meal>();
  @Output() editMeal = new EventEmitter<Meal>();
  @Output() addMealToCart = new EventEmitter<any>();

  chefDisplayName!: string;
  showIngredients: boolean = false;

  constructor(protected cartService: CartService, private userService: UserService) {}

  ngOnInit(): void {
    this.userService.getChefById(this.meal.chefId).subscribe(chef => {
      console.log(chef);
      this.chefDisplayName = chef.firstName + ' ' + chef.lastName;
    });
  }

  toggleIngredients() {
    this.showIngredients = !this.showIngredients;
  }

  get ingredients(): string[] {
    return this.meal.ingredients;
  }
}

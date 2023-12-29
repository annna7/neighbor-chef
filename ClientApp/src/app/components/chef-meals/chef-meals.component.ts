import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Meal } from '../../../swagger';
import { MealModalComponent } from '../meal-modal/meal-modal.component';
import {UserService} from "../../services/user.service";
import {User} from "oidc-client";
import {MealService} from "../../services";

@Component({
  selector: 'app-chef-meals',
  templateUrl: './chef-meals.component.html',
  styleUrls: ['./chef-meals.component.css']
})
export class ChefMealsComponent implements OnInit {
  meals: Meal[] = [];
  constructor(public dialog: MatDialog, private userService: UserService, private mealService: MealService) {}

  ngOnInit(): void {
    this.loadMeals();
  }

  loadMeals(): void {
    this.mealService.getMeals(this.userService.getCurrentUserId() as string).subscribe(
      meals => {
        this.meals = meals;
      },
      error => {
        console.error('Failed to fetch meals', error);
      }
    );
  }

  editMeal(meal: Meal): void {
    this.openMealModal(meal);
  }

  deleteMeal(meal: Meal): void {
    this.mealService.deleteMeal(meal.id).subscribe(
      () => {
        this.loadMeals();
      },
      error => {
        console.error("Failed to delete meal", error);
      }
    );
  }

  openMealModal(meal?: Meal): void {
    const dialogRef = this.dialog.open(MealModalComponent, {
      width: '700px',
      data: meal ? meal : { /* new meal data structure - empty object*/ },
    });

    dialogRef.afterClosed().subscribe(result => {
      this.loadMeals();
    });
  }
}

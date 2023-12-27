import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Meal } from '../../../swagger';
import { MealModalComponent } from '../meal-modal/meal-modal.component';
import {UserService} from "../../services/user.service";
import {User} from "oidc-client";

@Component({
  selector: 'app-chef-meals',
  templateUrl: './chef-meals.component.html',
  styleUrls: ['./chef-meals.component.css']
})
export class ChefMealsComponent implements OnInit {
  meals: Meal[] = [];
  constructor(public dialog: MatDialog, private userService: UserService) {}

  ngOnInit(): void {
    this.userService.getMeals().subscribe(
      meals => {
        this.meals = meals;
      },
      error => {
        console.error("Failed to fetch meals", error);
      }
    );
  }

  openMealModal(meal?: Meal): void {
    const dialogRef = this.dialog.open(MealModalComponent, {
      width: '250px',
      data: meal ? meal : { /* new meal data structure */ }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      // TODO: Handle the result (new or updated meal)
    });
  }
}

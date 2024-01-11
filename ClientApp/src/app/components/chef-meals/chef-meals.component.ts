import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {Chef, Meal} from '../../../swagger';
import { MealModalComponent } from '../meal-modal/meal-modal.component';
import {UserService} from "../../services/user.service";
import {User} from "oidc-client";
import {ChefService, MealService} from "../../services";
import {ActivatedRoute} from "@angular/router";
import {of, Subscription, switchMap} from "rxjs";
import {CartService} from "../../services/cart.service";
import {OrderModalComponent} from "../order-modal/order-modal.component";
import {ChefMealItem} from "../../models/chef-meal.dto";

@Component({
  selector: 'app-chef-meals',
  templateUrl: './chef-meals.component.html',
  styleUrls: ['./chef-meals.component.css']
})
export class ChefMealsComponent implements OnInit {
  meals: Meal[] = [];
  chefId: string | undefined;
  chef !: Chef;
  isSelf: boolean = false;
  cartItems: ChefMealItem[] = [];
  cartSubscription!: Subscription;

  constructor(public dialog: MatDialog, private userService: UserService, private mealService: MealService, private route: ActivatedRoute, protected cartService: CartService) {}

  ngOnInit(): void {
    this.route.params.pipe(
      switchMap(params => {
        if (params['id']) {
          this.isSelf = false;
          this.cartSubscription = this.cartService.items.subscribe(items => {
            this.cartItems = items[params['id']] || [];
          });
          return of(params['id']);
        } else {
          this.isSelf = true;
          return this.userService.currentUserId;
        }
      })
    ).subscribe(id => {
      this.chefId = id as string;
      this.userService.getChefById(this.chefId as string).subscribe(chef => {
        this.chef = chef;
      });
      this.loadMeals();
    });
  }

  loadMeals(): void {
    this.mealService.getMeals(this.chefId as string).subscribe(
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

  addMealToCart(meal: Meal): void {
    this.cartService.addMeal(meal, this.chefId as string, 1);
  }

  openMealModal(meal?: Meal): void {
    const dialogRef = this.dialog.open(MealModalComponent, {
      width: '700px',
      data: meal ? meal : { },
    });

    dialogRef.afterClosed().subscribe(result => {
      this.loadMeals();
    });
  }

  openOrderModal(): void {
    if (this.cartService.isCartEmpty(this.chefId as string)) {
      alert('You have to add at least one meal to the cart');
      return;
    }

    const dialogRef = this.dialog.open(OrderModalComponent, {
      width: '700px',
      data: { 'chefId': this.chefId, availableDates: this.chef.availableDates },
    });

    dialogRef.afterClosed().subscribe(result => {
      this.loadMeals();
    });
  }
}

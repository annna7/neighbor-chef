import {Injectable, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {catchError, forkJoin, map, Observable, switchMap, throwError} from "rxjs";
import {Meal} from "../../swagger";
import {environment} from "../../environments/environment";
import {UserService} from "./user.service";
import {CategoryService} from "./category.service";
@Injectable({
  providedIn: 'root'
})
export class MealService {
  private apiBaseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient, private userService: UserService, private categoryService: CategoryService) { }

  getAllMeals(): Observable<Meal[]> {
    return this.http.get<Meal[]>(`${this.apiBaseUrl}/Meal/all`).pipe(
      switchMap(meals => {
        return forkJoin(meals.map(meal => {
          return this.categoryService.getCategoryById(meal.categoryId).pipe(
            map(category => {
              return {
                ...meal,
                categoryName: category.name
              };
            })
          );
        }));
      }),
      catchError(error => {
        console.error("Failed to fetch meals", error);
        return throwError(error);
      })
    );
  }


  searchMeals(searchQuery: string): Observable<Meal[]> {
    return this.getAllMeals().pipe(
      map(meals => {
        return searchQuery ?
          meals.filter(meal => meal.name.toLowerCase().includes(searchQuery.toLowerCase())) : meals;
      }),
      catchError(error => {
        console.error("Failed to search meals", error);
        return throwError(error);
      })
    );
  }

  getMeals(chefId: string): Observable<Meal[]> {
    return this.userService.getChefById(chefId).pipe(
      switchMap(chef => {
        return forkJoin(chef.meals.map((meal: Omit<Meal, 'categoryName'>) => {
          return this.categoryService.getCategoryById(meal.categoryId).pipe(
            map(category => {
              return {
                ...meal,
                categoryName: category.name
              };
            })
          );
        }));
      }),
      catchError(error => {
        console.error("Failed to fetch meals", error);
        return throwError(error);
      })
    );
  }
  createMeal(meal: Omit<Meal, 'id'>): Observable<Meal> {
    return this.http.post<Meal>(`${this.apiBaseUrl}/Meal`, meal);
  }

  updateMeal(meal: Meal): Observable<Meal> {
    return this.http.put<Meal>(`${this.apiBaseUrl}/Meal/${meal.id}`, meal as Omit<Meal, 'id'>);
  }

  deleteMeal(mealId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiBaseUrl}/Meal/${mealId}`);
  }
}

import {Component, OnInit} from '@angular/core';
import {Chef, Meal} from "../../../swagger";
import {MealService, UserService, ChefService} from "../../services";

export enum SearchType {
  CHEFS = 'chefs',
  MEALS = 'meals'
}

@Component({
  selector: 'app-browse',
  templateUrl: './browse.component.html',
  styleUrl: './browse.component.css'
})
export class BrowseComponent implements OnInit {
  currentChefs !: Chef[];
  currentMeals !: Meal[];
  searchType: SearchType = SearchType.CHEFS;

  constructor(protected userService: UserService, protected mealService: MealService,
              private chefService: ChefService) {}
  ngOnInit() {
    this.loadMeals();
    this.loadChefs();
  }

  loadMeals() {
    this.mealService.getAllMeals().subscribe(meals => {
      this.currentMeals = meals;
    });
  }

  loadChefs() {
    this.chefService.loadAllChefs().subscribe(chefs => {
      this.currentChefs = chefs;
    });
  }

  onSearchPerformed(event: { searchQuery: string, searchType: string }): void {
    if (event.searchType === 'chefs') {
      this.searchType = SearchType.CHEFS;
      this.chefService.searchChefs(event.searchQuery).subscribe(chefs => {
        this.currentChefs = chefs;
      });
    } else if (event.searchType === 'meals') {
      this.searchType = SearchType.MEALS;
      this.mealService.searchMeals(event.searchQuery).subscribe(meals => {
        this.currentMeals = meals;
      });
    }
  }

}

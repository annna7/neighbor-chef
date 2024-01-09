import {Component, OnInit} from '@angular/core';
import {Category, Chef, Meal} from "../../../swagger";
import {MealService, UserService, ChefService, CategoryService} from "../../services";

export enum SearchType {
  CHEFS = 'chefs',
  MEALS = 'meals'
}

export type SortOptionsDictionary = {
  [type in SearchType]: string[];
};

export const SortOptions: SortOptionsDictionary = {
  [SearchType.CHEFS]: ['', 'name', 'rating'],
  [SearchType.MEALS]: ['', 'name', 'price']
};

@Component({
  selector: 'app-browse',
  templateUrl: './browse.component.html',
  styleUrl: './browse.component.css'
})
export class BrowseComponent implements OnInit {
  currentChefs !: Chef[];
  currentMeals !: Meal[];
  searchType: SearchType = SearchType.CHEFS;
  categories !: Category[];


  constructor(protected userService: UserService, protected mealService: MealService,
              private chefService: ChefService, private categoryService: CategoryService) {}
  ngOnInit() {
    this.loadMeals();
    this.loadChefs();
    this.loadCategories();
  }

  loadMeals() {
    this.mealService.getAllMeals().subscribe(meals => {
      this.currentMeals = meals;
      console.log(this.currentMeals);
    });
  }

  loadChefs() {
    this.chefService.loadAllChefs().subscribe(chefs => {
      this.currentChefs = chefs;
    });
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe(categories => {
      this.categories = categories;
    });
  }

  onSearchTypeChanged(event: SearchType): void {
    this.searchType = event;
    switch (event) {
      case SearchType.CHEFS:
        this.loadChefs();
        break;
      case SearchType.MEALS:
        this.loadMeals();
        break;
    }
  }

  onSortItems(event: string): void {
    switch (this.searchType) {
      case SearchType.CHEFS:
        this.chefService.sortChefs(event).subscribe(chefs => {
          this.currentChefs = [...chefs];
        });
        break;
      case SearchType.MEALS:
        this.mealService.sortMeals(event).subscribe(meals => {
          this.currentMeals = [...meals];
        });
        break;
    }
  }

  onFilterItems(event: string): void {
    if (this.searchType === SearchType.MEALS) {
      this.mealService.filterMeals(event).subscribe(meals => {
        this.currentMeals =[...meals];
      });
    } else {
      throw new Error('Filtering is only supported for meals');
    }
  }


  onSearchPerformed(event: { searchQuery: string, searchType: string }): void {
    if (event.searchType === 'chefs') {
      this.searchType = SearchType.CHEFS;
      this.chefService.searchChefs(event.searchQuery).subscribe(chefs => {
        this.currentChefs = [...chefs];
      });
    } else if (event.searchType === 'meals') {
      this.searchType = SearchType.MEALS;
      this.mealService.searchMeals(event.searchQuery).subscribe(meals => {
        this.currentMeals = [...meals];
      });
    }
  }

}

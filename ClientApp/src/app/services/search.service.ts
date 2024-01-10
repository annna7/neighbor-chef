import {Injectable, OnInit} from '@angular/core';
import {Category, Chef, Meal} from "../../swagger";
import {ChefService} from "./chef.service";
import {MealService} from "./meal.service";
import {CategoryService} from "./category.service";

export enum SearchType {
  CHEFS = 'chefs',
  MEALS = 'meals'
}

export interface SortOptionItem {
  value: SortOption;
  name: string;
}

export type SortOptionsDictionary = {
  [type in SearchType]: SortOptionItem[];
};

export enum SortOption {
  NONE = '',
  MEAL_NAME = 'meal_name',
  CHEF_NAME = 'chef_name',
  PRICE = 'price',
  RATING = 'rating'
}

function getSortOptionName(option: SortOption): string {
  switch (option) {
    case SortOption.NONE:
      return 'None';
    case SortOption.MEAL_NAME:
      return 'Name';
    case SortOption.CHEF_NAME:
      return 'Name';
    case SortOption.PRICE:
      return 'Price';
    case SortOption.RATING:
      return 'Rating';
    default:
      return '';
  }
}

export const SortOptions: SortOptionsDictionary = {
  [SearchType.CHEFS]: [
    { value: SortOption.NONE, name: getSortOptionName(SortOption.NONE) },
    { value: SortOption.CHEF_NAME, name: getSortOptionName(SortOption.CHEF_NAME) },
    { value: SortOption.RATING, name: getSortOptionName(SortOption.RATING) }
  ],
  [SearchType.MEALS]: [
    { value: SortOption.NONE, name: getSortOptionName(SortOption.NONE) },
    { value: SortOption.MEAL_NAME, name: getSortOptionName(SortOption.MEAL_NAME) },
    { value: SortOption.PRICE, name: getSortOptionName(SortOption.PRICE) }
  ]
};

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  allMeals: Meal[] = [];
  allChefs: Chef[] = [];
  allCategories: Category[] = [];
  sortedMeals: Meal[] = [];
  sortedChefs: Chef[] = [];
  toBeReturnedChefs: Chef[] = [];
  toBeReturnedMeals: Meal[] = [];
  currentSearchQuery: string = '';
  currentSearchType: SearchType = SearchType.CHEFS;

  constructor(private chefService: ChefService, private mealService: MealService, private categoryService: CategoryService) {
    this.loadMeals();
    this.loadChefs();
    this.loadCategories();
  }

  loadChefs() {
    this.chefService.loadAllChefs().subscribe(chefs => {
      this.allChefs = chefs;
      this.toBeReturnedChefs = this.allChefs;
    });
  }

  loadMeals() {
    this.mealService.getAllMeals().subscribe(meals => {
      this.allMeals = meals;
      this.toBeReturnedMeals = this.allMeals;
    });
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe(categories => {
      this.allCategories = categories;
    });
  }

  formatName(chef: Chef) {
    return `${chef.firstName} ${chef.lastName}`;
  }

  private sortChefs(sortBy: SortOption, chefList: Chef[]): Chef[] {
    switch (sortBy) {
      case SortOption.CHEF_NAME:
        return chefList.slice().sort((a, b) => this.formatName(a).localeCompare(this.formatName(b)));
      case SortOption.RATING:
        return chefList.slice().sort((a, b) => this.chefService.getRating(b) - this.chefService.getRating(a));
      default:
        return chefList;
    }
  }

  private sortMeals(sortBy: SortOption, mealList: Meal[]): Meal[] {
    switch (sortBy) {
      case SortOption.MEAL_NAME:
        return mealList.slice().sort((a, b) => a.name.localeCompare(b.name));
      case SortOption.PRICE:
        return mealList.slice().sort((a, b) => a.price - b.price);
      default:
        return mealList;
    }
  }

  public getSearchResults(): Chef[] | Meal[] {
    switch (this.currentSearchType) {
      case SearchType.CHEFS:
        return this.toBeReturnedChefs;
      case SearchType.MEALS:
        return this.toBeReturnedMeals;
    }
  }

  public sortItems(sortBy: SortOption): void {
    switch (this.currentSearchType) {
      case SearchType.CHEFS:
        this.allChefs = this.sortChefs(sortBy, this.allChefs);
        this.toBeReturnedChefs = this.sortChefs(sortBy, this.toBeReturnedChefs);
        break;
      case SearchType.MEALS:
        this.allMeals = this.sortMeals(sortBy, this.allMeals);
        this.toBeReturnedMeals = this.sortMeals(sortBy, this.toBeReturnedMeals);
        break;
    }
  }

  public filterItems(filterBy: string): void {
    if (this.currentSearchType === SearchType.CHEFS) {
      return;
    }

    this.toBeReturnedMeals = filterBy === 'all' ?
      this.allMeals : this.allMeals.filter(meal => meal.categoryName === filterBy);

    console.log('filtered meals', this.toBeReturnedMeals);
  }

  public searchItems(searchQuery: string): void {
    this.currentSearchQuery = searchQuery;
    switch (this.currentSearchType) {
      case SearchType.CHEFS:
        this.toBeReturnedChefs = searchQuery ?
          this.toBeReturnedChefs.filter(chef => this.formatName(chef).toLowerCase().includes(searchQuery.toLowerCase())) : this.sortedChefs;
        break;
      case SearchType.MEALS:
        this.toBeReturnedMeals = searchQuery ?
          this.toBeReturnedMeals.filter(meal => meal.name.toLowerCase().includes(searchQuery.toLowerCase())) : this.sortedMeals;
        break;
    }
  }

  public updateSearchType(searchType: SearchType): void {
    this.currentSearchType = searchType;
  }
}

// TODO: FIX CATEGORY IN CREATE MEAL

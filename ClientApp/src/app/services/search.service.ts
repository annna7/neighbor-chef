import {Injectable, OnInit} from '@angular/core';
import {Category, Chef, Meal} from "../../swagger";
import {ChefService} from "./chef.service";
import {MealService} from "./meal.service";
import {CategoryService} from "./category.service";
import {forkJoin, Observable, tap} from "rxjs";

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
  initialMeals: Meal[] = [];
  initialChefs: Chef[] = [];
  allMeals: Meal[] = [];
  allChefs: Chef[] = [];
  allCategories: Category[] = [];
  toBeReturnedChefs: Chef[] = [];
  toBeReturnedMeals: Meal[] = [];
  currentSearchQuery: string = '';
  currentSearchType: SearchType = SearchType.CHEFS;
  activeFilters: { [type in SearchType]: { [key: string]: (item: any) => boolean } } = {
    [SearchType.CHEFS]: {},
    [SearchType.MEALS]: {}
  };
  constructor(private chefService: ChefService, private mealService: MealService, private categoryService: CategoryService) {
    this.loadAllItems();
  }

  loadAllItems(): Observable<{chefs: Chef[], meals: Meal[], categories: Category[]}>  {
    return forkJoin({
      chefs: this.loadChefs(),
      meals: this.loadMeals(),
      categories: this.loadCategories()
    });
  }

  loadChefs(): Observable<Chef[]> {
    return this.chefService.loadAllChefs().pipe(
      tap(chefs => {
        this.allChefs = chefs;
        this.toBeReturnedChefs = this.initialChefs = this.allChefs;
      })
    );
  }

  loadMeals(): Observable<Meal[]> {
    return this.mealService.getAllMeals().pipe(
      tap(meals => {
        this.allMeals = meals;
        this.toBeReturnedMeals = this.initialMeals = this.allMeals;
      })
    );
  }

  loadCategories(): Observable<Category[]> {
    return this.categoryService.getCategories().pipe(
      tap(categories => {
        this.allCategories = categories;
      })
    );
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

  onFilterByCategory(categoryName: string) {
    if (categoryName === 'all') {
      delete this.activeFilters[SearchType.MEALS]['category'];
    } else {
      this.activeFilters[SearchType.MEALS]['category'] = (meal: Meal) => meal.categoryName === categoryName;
    }
    this.applyFilters();
  }

  onFilterItemsByQuery(query: string) {
    if (!query) {
      delete this.activeFilters[this.currentSearchType]['query'];
    } else {
      switch (this.currentSearchType) {
        case SearchType.CHEFS:
          this.activeFilters[this.currentSearchType]['query'] = (chef: Chef) => this.formatName(chef).toLowerCase().includes(query.toLowerCase());
          break;
        case SearchType.MEALS:
          this.activeFilters[this.currentSearchType]['query'] = (meal: Meal) => meal.name.toLowerCase().includes(query.toLowerCase());
          break;
      }
    }
    this.applyFilters();
  }

  private applyFilters() {
    let filteredMeals = [...this.allMeals];
    let filteredChefs = [...this.allChefs];

    switch (this.currentSearchType) {
      case SearchType.CHEFS:
        Object.values(this.activeFilters[this.currentSearchType]).forEach(filter => {
          filteredChefs = filteredChefs.filter(filter);
        });
        break;
      case SearchType.MEALS:
        Object.values(this.activeFilters[this.currentSearchType]).forEach(filter => {
          console.log('filter', filter);
          filteredMeals = filteredMeals.filter(filter);
          console.log('filteredMeals', filteredMeals)
        });
        break;
    }

    this.toBeReturnedMeals = filteredMeals;
    this.toBeReturnedChefs = filteredChefs;
  }

  // public filterByCategory(categoryName: string) {
  //   return this.filterByLambda((entity: Meal) => entity.categoryName === categoryName);
  // }
  //
  // private filterByLambda(lambda: (entity: any) => boolean) {
  //   switch (this.currentSearchType) {
  //     case SearchType.CHEFS:
  //       this.toBeReturnedChefs = this.allChefs.filter(lambda);
  //       break;
  //     case SearchType.MEALS:
  //       this.toBeReturnedMeals = this.allMeals.filter(lambda);
  //       break;
  //   }
  // }
  //
  // public onFilterItemsByQuery(query: string): void {
  //   let filterLambda: (entity: any) => boolean;
  //   switch (this.currentSearchType) {
  //     case SearchType.CHEFS:
  //       filterLambda = query ? (entity: Chef) => this.formatName(entity).toLowerCase().includes(query.toLowerCase()) || this.formatName(entity).toLowerCase().includes(query.toLowerCase()) : () => true;
  //       break;
  //     case SearchType.MEALS:
  //       filterLambda = query ? (entity: Meal) => entity.name.toLowerCase().includes(query.toLowerCase()) : () => true;
  //       break;
  //   }
  //   this.filterByLambda(filterLambda);
  // }

  public updateSearchType(searchType: SearchType): void {
    this.currentSearchType = searchType;
  }

  clearFilters() {
    this.toBeReturnedChefs = this.allChefs = this.initialChefs;
    this.toBeReturnedMeals = this.allMeals = this.initialMeals;
  }
}


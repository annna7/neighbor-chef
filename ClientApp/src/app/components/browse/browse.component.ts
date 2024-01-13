import {Component, OnInit} from '@angular/core';
import {Category, Chef, Meal} from "../../../swagger";
import {SearchService, SearchType, SortOption, SortOptionItem, SortOptions, MealService, UserService, ChefService, CategoryService} from "../../services";


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
              private chefService: ChefService, private categoryService: CategoryService, private searchService: SearchService) {
  }

  ngOnInit() {
    this.searchService.loadAllItems().subscribe((data) => {
      this.currentChefs = data.chefs;
      this.currentMeals = data.meals;
      this.categories = data.categories;
    });
  }

  onSearchTypeChanged(searchType: string | undefined) {
    if (searchType === undefined) {
      this.searchType = SearchType.CHEFS;
    }
    this.searchType = searchType as SearchType;
    this.searchService.updateSearchType(this.searchType);
    switch (this.searchType) {
      case SearchType.CHEFS:
        this.currentChefs = [...this.searchService.toBeReturnedChefs];
        this.currentMeals = [];
        break;
      case SearchType.MEALS:
        this.currentMeals = [...this.searchService.toBeReturnedMeals];
        this.currentChefs = [];
        break;
    }
  }

  onSortChanged(sortBy: SortOption) {
    this.searchService.sortItems(sortBy);
    switch (this.searchType) {
      case SearchType.CHEFS:
        this.currentChefs = [...this.searchService.toBeReturnedChefs];
        break;
      case SearchType.MEALS:
        this.currentMeals = [...this.searchService.toBeReturnedMeals];
        break;
      }
    }

    onFilterMealsByCategory(categoryName: string) {
      this.searchService.onFilterByCategory(categoryName);
      switch (this.searchType) {
        case SearchType.CHEFS:
          this.currentChefs = [...this.searchService.toBeReturnedChefs];
          break;
        case SearchType.MEALS:
          this.currentMeals = [...this.searchService.toBeReturnedMeals];
          break;
      }
    }


    onSearchPerformed(searchQuery: string) {
      this.searchService.onFilterItemsByQuery(searchQuery);
      switch (this.searchType) {
        case SearchType.CHEFS:
          this.currentChefs = [...this.searchService.toBeReturnedChefs];
          break;
        case SearchType.MEALS:
          this.currentMeals = [...this.searchService.toBeReturnedMeals];
          break;
      }
    }

    onClearFilters() {
      this.searchService.clearFilters();
      switch (this.searchType) {
        case SearchType.CHEFS:
          this.currentChefs = [...this.searchService.toBeReturnedChefs];
          break;
        case SearchType.MEALS:
          this.currentMeals = [...this.searchService.toBeReturnedMeals];
          break;
      }
    }

  protected readonly SearchType = SearchType;
  protected readonly SortOptions = SortOptions;
}

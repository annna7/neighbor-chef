import {Component, OnInit} from '@angular/core';
import {Category, Chef, Meal} from "../../../swagger";
import {MealService, UserService, ChefService, CategoryService} from "../../services";
import {SearchService, SearchType, SortOption, SortOptionItem, SortOptions} from "../../services/search.service";



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
    this.loadMeals();
    this.loadChefs();
    this.loadCategories();
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

  loadCategories() {
    this.categoryService.getCategories().subscribe(categories => {
      this.categories = categories;
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
    console.log('sort by', sortBy);
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

    onFilterItems(filterBy: string) {
      console.log('filter by', filterBy);
      this.searchService.filterItems(filterBy);
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
      this.searchService.searchItems(searchQuery);
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

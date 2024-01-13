import {Component, EventEmitter, Input, OnDestroy, Output} from '@angular/core';
import {Category, Chef, Meal} from "../../../swagger";
import {SearchType, SortOption, SortOptionItem} from "../../services";

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})

export class SearchBarComponent implements OnDestroy {
  @Input() categories: Category[] = [];
  @Input() sorts: SortOptionItem[] = [];
  @Output() search: EventEmitter<string> = new EventEmitter<string>();
  @Output() searchTypeChanged: EventEmitter<string | undefined> = new EventEmitter<string | undefined>();
  @Output() filterChanged: EventEmitter<string> = new EventEmitter<string>();
  @Output() sortChanged: EventEmitter<SortOption> = new EventEmitter<SortOption>();
  @Output() clearFiltersClicked: EventEmitter<void> = new EventEmitter<void>();

  searchQuery: string = '';
  searchType: SearchType = SearchType.CHEFS;
  currentSort: string = '';
  currentFilter: string = 'all';

  constructor() { }

  clearFilters() {
    this.searchQuery = '';
    this.currentSort = '';
    this.currentFilter = 'all';
    this.clearFiltersClicked.emit();
  }

  performSearch() {
    this.search.emit(this.searchQuery);
  }

  ngOnDestroy() {
    this.clearFilters();
  }

  protected readonly SearchType = SearchType;
}

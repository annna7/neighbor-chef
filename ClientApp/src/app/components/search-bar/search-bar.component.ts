import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Category, Chef, Meal} from "../../../swagger";
import {SearchType, SortOption, SortOptionItem} from "../../services/search.service";

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})

export class SearchBarComponent {
  @Input() categories: Category[] = [];
  @Input() sorts: SortOptionItem[] = [];
  @Output() search: EventEmitter<string> = new EventEmitter<string>();
  @Output() searchTypeChanged: EventEmitter<string | undefined> = new EventEmitter<string | undefined>();
  @Output() filterChanged: EventEmitter<string> = new EventEmitter<string>();
  @Output() sortChanged: EventEmitter<SortOption> = new EventEmitter<SortOption>();

  searchQuery: string = '';
  searchType: SearchType = SearchType.CHEFS;
  currentSort: string = '';
  currentFilter: string = 'all';

  constructor() { }

  performSearch() {
    console.log(`Searching for ${this.searchType}: ${this.searchQuery}`);
    this.search.emit(this.searchQuery);
  }

  protected readonly SearchType = SearchType;
}

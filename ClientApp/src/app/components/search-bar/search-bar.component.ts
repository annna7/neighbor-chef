import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Category, Chef, Meal} from "../../../swagger";
import {SearchType} from "../browse/browse.component";

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})

export class SearchBarComponent {
  @Input() categories: Category[] = [];
  @Output() search: EventEmitter<{ searchQuery: string, searchType: SearchType}> = new EventEmitter<{ searchQuery: string, searchType: SearchType}>();
  @Output() searchTypeChanged: EventEmitter<SearchType> = new EventEmitter<SearchType>();
  @Output() filterChanged: EventEmitter<string> = new EventEmitter<string>();
  @Output() sortChanged: EventEmitter<string> = new EventEmitter<string>();

  searchQuery: string = '';
  searchType: SearchType = SearchType.CHEFS;
  currentSort: string = '';
  currentFilter: string = 'all';

  constructor() { }

  performSearch() {
    console.log(`Searching for ${this.searchType}: ${this.searchQuery}`);
    this.search.emit({ searchQuery: this.searchQuery, searchType: this.searchType });
  }
}

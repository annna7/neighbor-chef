import {Component, EventEmitter, Output} from '@angular/core';
import {Chef, Meal} from "../../../swagger";
import {SearchType} from "../browse/browse.component";

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})

export class SearchBarComponent {
  @Output() search: EventEmitter<{ searchQuery: string, searchType: SearchType}> = new EventEmitter<{ searchQuery: string, searchType: SearchType}>();
  searchQuery: string = '';
  searchType: SearchType = SearchType.CHEFS;

  constructor() { }

  performSearch() {
    console.log(`Searching for ${this.searchType}: ${this.searchQuery}`);
    this.search.emit({ searchQuery: this.searchQuery, searchType: this.searchType });
  }
}

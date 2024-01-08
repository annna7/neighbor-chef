import { Component } from '@angular/core';
import {Chef, Meal} from "../../../swagger";

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})
export class SearchBarComponent {
  searchQuery: string = '';
  searchType: 'chefs' | 'meals' = 'chefs';

  constructor() { }

  performSearch() {
    console.log(`Searching for ${this.searchType}: ${this.searchQuery}`);
  }
}

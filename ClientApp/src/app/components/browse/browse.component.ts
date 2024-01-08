import {Component, OnInit} from '@angular/core';
import {Chef, Meal} from "../../../swagger";
import {MealService, UserService, ChefService} from "../../services";

@Component({
  selector: 'app-browse',
  templateUrl: './browse.component.html',
  styleUrl: './browse.component.css'
})
export class BrowseComponent implements OnInit {
  currentChefs !: Chef[];
  currentMeals !: Meal[];

  constructor(protected userService: UserService, protected mealService: MealService,
              private chefService: ChefService) {}
  ngOnInit() {
    this.loadMeals();
    this.loadChefs();
  }

  loadMeals() {

  }

  loadChefs() {
    this.chefService.loadAllChefs().subscribe(chefs => {
      // TODO: fix!! (add more seeds)
      this.currentChefs = [
        chefs[0], chefs[0], chefs[0], chefs[0]
      ]
      console.log(this.currentChefs);
    });
  }
}

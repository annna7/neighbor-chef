import {Component, OnInit} from '@angular/core';
import {ChefService, UserService} from "../../../services";
import {Chef} from "../../../../swagger";

@Component({
  selector: 'app-chef-dashboard',
  templateUrl: './chef-dashboard.component.html',
  styleUrl: './chef-dashboard.component.css'
})
export class ChefDashboardComponent implements OnInit {
  displayName !: string;
  rating !: number;
  numberOfUpcomingOrders !: number;
  constructor(protected userService: UserService, private chefService: ChefService) {}

  ngOnInit() {
    this.loadUser();
  }

  loadUser() {
    this.userService.getCurrentUser().subscribe(
      user => {
        this.displayName = user.firstName + " " + user.lastName;
        this.rating = this.chefService.getRating(user as Chef);
        this.numberOfUpcomingOrders = this.chefService.getNumberOfUpcomingOrders(user as Chef);

      },
      error => {
        throw new Error("failed to fetch user!");
      }
    )};
}

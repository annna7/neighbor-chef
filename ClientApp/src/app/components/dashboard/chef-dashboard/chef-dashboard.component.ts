import {Component, OnInit} from '@angular/core';
import {UserService} from "../../../services";
import {Chef} from "../../../../swagger";

@Component({
  selector: 'app-chef-dashboard',
  templateUrl: './chef-dashboard.component.html',
  styleUrl: './chef-dashboard.component.css'
})
export class ChefDashboardComponent implements OnInit {
  displayName !: string;
  rating !: number;
  constructor(protected userService: UserService) {}

  ngOnInit() {
    this.loadUser();
  }

  loadUser() {
    this.userService.getCurrentUser().subscribe(
      user => {
        this.displayName = user.firstName + " " + user.lastName;
        this.rating = (user as Chef).reviewsReceived.reduce((acc, review) => acc + review.rating, 0) / (user as Chef).reviewsReceived.length;
      },
      error => {
        throw new Error("failed to fetch user!");
      }
    )};
}

import {Component, OnInit} from '@angular/core';
import {UserService} from "../../../services";

@Component({
  selector: 'app-customer-dashboard',
  templateUrl: './customer-dashboard.component.html',
  styleUrl: './customer-dashboard.component.css'
})
export class CustomerDashboardComponent implements OnInit {
  displayName !: string;
  constructor(private userService: UserService) {}

  ngOnInit() {
    this.loadUser();
  }

  loadUser() {
    this.userService.getCurrentUser().subscribe(
      user => {
        this.displayName = user.firstName + " " + user.lastName;
      },
      error => {
        throw new Error("failed to fetch user!");
      }
    )};

}

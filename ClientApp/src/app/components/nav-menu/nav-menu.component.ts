import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";
import {UserService} from "../../services/user.service";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})

export class NavMenuComponent implements OnInit {
  isExpanded = false;
  currentUserId = this.userService.getCurrentUserId();

  constructor (protected authService: AuthService, private userService: UserService) {}

  ngOnInit(): void {
    this.currentUserId = this.userService.getCurrentUserId();
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}

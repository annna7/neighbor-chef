import { Component, OnInit } from '@angular/core';
import { AuthService, UserService } from "../../services";
import {BehaviorSubject, Observable} from "rxjs";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})

export class NavMenuComponent implements OnInit {
  isExpanded = false;
  currentUserId: string | undefined;

  constructor (protected authService: AuthService, protected userService: UserService) {}

  ngOnInit(): void {
    this.userService.currentUserId.subscribe(id => this.currentUserId = id ?? '');
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}

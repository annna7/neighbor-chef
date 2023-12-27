import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../services/auth.service";
import { UserService } from "../../services/user.service";
import { BehaviorSubject } from "rxjs";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})

export class NavMenuComponent implements OnInit {
  isExpanded = false;
  currentUserId: string | undefined;
  private roleSubject = new BehaviorSubject<string | undefined>(undefined);
  currentRole = this.roleSubject.asObservable(); // This will be used to reactively provide the role in the template

  constructor (protected authService: AuthService, private userService: UserService) {}

  ngOnInit(): void {
    this.currentUserId = this.userService.getCurrentUserId();
    if (this.currentUserId) {
      this.fetchUserRole(this.currentUserId);
    }
  }

  fetchUserRole(userId: string): void {
    this.userService.getRole(userId).subscribe(
      role => {
        this.roleSubject.next(role);
      },
      error => {
        console.error("Failed to fetch user role", error);
        this.roleSubject.next(undefined);
      }
    );
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}

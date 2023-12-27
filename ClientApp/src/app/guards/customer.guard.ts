import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import {UserService} from "../services/user.service";

@Injectable({
  providedIn: 'root'
})
export class CustomerGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean {
    if (localStorage.getItem('role') !== 'Customer') {
      this.router.navigate(['/']);
      return false;
    }
    return true;
  }
}

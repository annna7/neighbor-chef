import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean {
    const isLoggedIn = this.authService.isLoggedIn();
    console.log('AuthGuard#canActivate called', isLoggedIn)

    if (isLoggedIn && this.router.url === '/login') {
      this.router.navigate(['/dashboard']);
      return false;
    }

    if (!isLoggedIn && this.router.url !== '/login') {
      this.router.navigate(['/login']);
      return false;
    }

    return true;
  }
}

import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {LoginDto} from '../models/people/login.dto';
import {BehaviorSubject, Observable} from 'rxjs';
import {TokenDto} from "../models/token.dto";
import {Router} from "@angular/router";
import {UserService} from "./user.service";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiBaseUrl = environment.apiBaseUrl;
  private roleSubject = new BehaviorSubject<string | undefined>(undefined);
  role = this.roleSubject.asObservable();

  constructor(private http: HttpClient, private router: Router, private userService: UserService) {
  }

  login(credentials: LoginDto): Observable<TokenDto> {
    return this.http.post<TokenDto>(`${this.apiBaseUrl}/Account/Login`, credentials);
  }

  async logout(): Promise<void> {
    localStorage.removeItem('auth_token');
    localStorage.removeItem('user_data');
    localStorage.removeItem('role');
    await this.router.navigate(['/']);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('auth_token');
  }

  async register(userData: any): Promise<void> {
    await this.userService.register(userData).toPromise();
    await this.router.navigate(['/login']);
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
}

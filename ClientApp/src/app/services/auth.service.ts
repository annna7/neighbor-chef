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
  private roleSubject = new BehaviorSubject<string | null>(null);
  role = this.roleSubject.asObservable();

  constructor(private http: HttpClient, private router: Router, private userService: UserService) {
    const storedRole = localStorage.getItem('role');
    this.roleSubject = new BehaviorSubject<string | null>(storedRole);
    if (storedRole) {
      this.roleSubject.next(storedRole);
    }
    this.userService.userIdSubject = new BehaviorSubject<string | null>(localStorage.getItem('user_id'));
    if (localStorage.getItem('user_id')) {
      this.userService.userIdSubject.next(localStorage.getItem('user_id'));
    }
  }

  login(credentials: LoginDto): Observable<TokenDto> {
    return this.http.post<TokenDto>(`${this.apiBaseUrl}/Account/Login`, credentials);
  }

  async logout(): Promise<void> {
    localStorage.removeItem('auth_token');
    localStorage.removeItem('user_data');
    localStorage.removeItem('role');
    localStorage.removeItem('notificationsPermission');
    this.roleSubject.next(null);
    this.userService.userIdSubject.next(null);
    await this.router.navigate(['/']);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('auth_token');
  }

  async register(userData: any): Promise<void> {
    await this.userService.register(userData).toPromise();
    await this.router.navigate(['/login']);
  }

  updateRole(role: string): void {
    this.roleSubject.next(role);
  }

  get currentRole(): Observable<string | null> {
    return this.roleSubject.asObservable();
  }
}

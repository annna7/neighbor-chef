import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {LoginDto} from '../models/people/login.dto';
import {Observable} from 'rxjs';
import {TokenDto} from "../models/token.dto";
import {Router} from "@angular/router";
import {UserService} from "./user.service";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiBaseUrl = environment.apiBaseUrl;

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

  test() : Observable<string> {
    return this.http.get<string>(`${this.apiBaseUrl}/Chef`);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('auth_token');
  }

  async register(userData: any): Promise<void> {
    await this.userService.register(userData).toPromise();
    await this.router.navigate(['/login']);
  }
  // Method for user registration
  // register(data: RegisterDto): Observable<ServiceResponse<any>> {
  //   return this.http.post<ServiceResponse<any>>(`${this.apiBaseUrl}/Account/Register`, data);
  // }
}

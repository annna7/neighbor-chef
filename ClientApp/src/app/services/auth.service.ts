import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {LoginDto} from '../models/login.dto';
import {Observable} from 'rxjs';
import {TokenDto} from "../models/token.dto";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiBaseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {
  }

  // Method for user login
  login(credentials: LoginDto): Observable<TokenDto> {
    return this.http.post<TokenDto>(`${this.apiBaseUrl}/Account/Login`, credentials);
  }

  test() : Observable<string> {
    return this.http.get<string>(`${this.apiBaseUrl}/Chef`);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('auth_token');
  }

  // Method for user registration
  // register(data: RegisterDto): Observable<ServiceResponse<any>> {
  //   return this.http.post<ServiceResponse<any>>(`${this.apiBaseUrl}/Account/Register`, data);
  // }
}

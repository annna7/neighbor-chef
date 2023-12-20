import { Injectable } from '@angular/core';
import {PersonDto} from "../models/person.dto";

@Injectable({ providedIn: 'root' })
export class StorageService {

  private readonly tokenKey = 'auth_token';
  private readonly userKey = 'user_data';

  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  setUser(user: any): void {
    localStorage.setItem(this.userKey, JSON.stringify(user));
  }

  getUser(): PersonDto {
    const user = localStorage.getItem(this.userKey);
    return user ? JSON.parse(user) : null;
  }

  clearStorage(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.userKey);
  }
}

import { Injectable } from '@angular/core';
import {PersonDto} from "../models/people/person.dto";
import {Person} from "../../swagger";

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

  getUser(): Person {
    const user = localStorage.getItem(this.userKey);
    return user ? JSON.parse(user) : null;
  }

  clearStorage(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.userKey);
  }
}

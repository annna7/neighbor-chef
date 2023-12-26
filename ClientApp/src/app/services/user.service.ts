import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

import { PersonDto } from '../models/people/person.dto';
import {ChefRegisterDto} from "../models/people/chef-register.dto";
import {CustomerRegisterDto} from "../models/people/customer-register.dto";
import {Chef, Customer, Person} from "../../swagger";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiBaseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  getCurrentUser(): Observable<Chef | Customer> {
    switch (localStorage.getItem('role')) {
      case 'Chef':
        return this.getCurrentChef();
      case 'Customer':
        return this.getCurrentCustomer();
      default:
        throw new Error('Invalid role');
    }
  }

  getUserById(id: string): Observable<Chef | Customer> {
    return this.http.get<Chef | Customer>(`${this.apiBaseUrl}/Person/getById/${id}`);
  }

  getChefById(id: string): Observable<Chef> {
    return this.http.get<Chef>(`${this.apiBaseUrl}/Chef/${id}`);
  }

  getCustomerById(id: string): Observable<Customer> {
    return this.http.get<Customer>(`${this.apiBaseUrl}/Customer/${id}`);
  }
  getCurrentChef(): Observable<Chef> {
    return this.http.get<Chef>(`${this.apiBaseUrl}/Chef`);
  }

  getCurrentCustomer(): Observable<Customer> {
    return this.http.get<Customer>(`${this.apiBaseUrl}/Customer`);
  }

  updatePerson(personData: PersonDto): Observable<PersonDto> {
    return this.http.put<PersonDto>(`${this.apiBaseUrl}/Person/update`, personData);
  }

  register(userData: ChefRegisterDto | CustomerRegisterDto): Observable<any> {
    return this.http.post<any>(`${this.apiBaseUrl}/Account/register`, userData);
  }

  getCurrentUserId(): string | undefined {
    const userString = localStorage.getItem('user_data');
    if (userString) {
      return JSON.parse(userString)["id"];
    }
    return undefined;
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {BehaviorSubject, catchError, map, Observable, throwError} from 'rxjs';
import { environment } from '../../environments/environment';

import { PersonDto } from '../models/people/person.dto';
import {ChefRegisterDto} from "../models/people/chef-register.dto";
import {CustomerRegisterDto} from "../models/people/customer-register.dto";
import {Chef, Customer, Meal, Person} from "../../swagger";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiBaseUrl = environment.apiBaseUrl;

  userIdSubject = new BehaviorSubject<string | null>(null);

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

  getMeals(): Observable<Meal[]> {
    return this.getCurrentUser().pipe(
      map(user => {
        console.log("GET MEALS", user);
        return (user as Chef).meals;
      })
    );
  }

  getRole(id: string): Observable<string> {
    return this.getUserById(id).pipe(
      map((user: Chef | Customer) => {
        if  (user.hasOwnProperty('maxOrdersPerDay')) {
          return 'Chef';
        } else {
          return 'Customer';
        }
      },
      catchError(err => throwError(() => new Error('An error occurred: ' + err)))
      )
    );
  }

  getUserById(id: string): Observable<Chef | Customer> {
    return this.getChefById(id).pipe(
      catchError(err => {
        return this.getCustomerById(id);
      })
    );
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

  updatePerson(personData: Chef | Customer): Observable<Chef | Customer> {
    const updateData = personData as Omit<Chef, 'id'> | Omit<Customer, 'id'>;
    const unpackedApplicationUser = {
      ...updateData,
      phoneNumber: updateData?.applicationUser?.phoneNumber,
      email: updateData?.applicationUser?.email,
    }
    return this.http.put<Chef | Customer>(`${this.apiBaseUrl}/Person/update/${this.getCurrentUserId()}`, unpackedApplicationUser);
  }

  register(userData: ChefRegisterDto | CustomerRegisterDto): Observable<any> {
    return this.http.post<any>(`${this.apiBaseUrl}/Account/register`, userData);
  }

  addDate(date: string): Observable<Chef> {
    return this.http.post<Chef>(`${this.apiBaseUrl}/Chef/${this.getCurrentUserId()}/dates`, JSON.stringify(date));
  }

  deleteDate(date: string): Observable<Chef> {
    return this.http.delete<Chef>(`${this.apiBaseUrl}/Chef/${this.getCurrentUserId()}/dates/${date}`);
  }


  updateUserId(userId: string | undefined): void {
    console.log('new user id', userId);
    this.userIdSubject.next(userId !== undefined ? userId : null);
  }

  get currentUserId(): Observable<string | null> {
    return this.userIdSubject.asObservable();
  }

  getCurrentUserId(): string | undefined {
      const userString = localStorage.getItem('user_data');
      if (userString) {
          console.log("GET CURRENT USER ID", JSON.parse(userString)["id"]);
          return JSON.parse(userString)["id"];
      }
      return undefined;
  }
}

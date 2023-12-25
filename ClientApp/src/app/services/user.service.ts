import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

import { PersonDto } from '../models/people/person.dto';
import {ChefRegisterDto} from "../models/people/chef-register.dto";
import {CustomerRegisterDto} from "../models/people/customer-register.dto";
import {Chef, Customer} from "../../swagger";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiBaseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  getChef(): Observable<Chef> {
    return this.http.get<Chef>(`${this.apiBaseUrl}/Chef`);
  }

  getCustomer(): Observable<Customer> {
    return this.http.get<Customer>(`${this.apiBaseUrl}/Customer`);
  }

  updatePerson(personData: PersonDto): Observable<PersonDto> {
    return this.http.put<PersonDto>(`${this.apiBaseUrl}/Person/update`, personData);
  }

  register(userData: ChefRegisterDto | CustomerRegisterDto): Observable<any> {
    return this.http.post<any>(`${this.apiBaseUrl}/Account/register`, userData);
  }

}

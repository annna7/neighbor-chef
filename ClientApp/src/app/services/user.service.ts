import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ServiceResponse } from '../models/service-response.dto';

import { PersonDto } from '../models/person.dto';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiBaseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  // Get user details
  getUser(): Observable<any> {
    return this.http.get<any>(`${this.apiBaseUrl}/Person/getByEmail`);
  }

  // Update user profile
  updatePerson(personData: PersonDto): Observable<ServiceResponse<PersonDto>> {
    return this.http.put<ServiceResponse<PersonDto>>(`${this.apiBaseUrl}/person/update`, personData);
  }

  // Add other person-related methods as needed
}

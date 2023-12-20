import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

import { PersonDto } from '../models/person.dto';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiBaseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  // Get user details
  getUser(): Observable<PersonDto> {
    return this.http.get<PersonDto>(`${this.apiBaseUrl}/Person/getByEmail`);
  }

  // Update user profile
  updatePerson(personData: PersonDto): Observable<PersonDto> {
    return this.http.put<PersonDto>(`${this.apiBaseUrl}/Person/update`, personData);
  }

  // Add other person-related methods as needed
}

import {Injectable, OnInit} from '@angular/core';
import {map, Observable} from "rxjs";
import {Chef} from "../../swagger";
import {environment} from "../../environments/environment";
import {UserService} from "./user.service";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ChefService {
  private apiBaseUrl = environment.apiBaseUrl;
  currentUserId !: string;
  constructor(private http: HttpClient, private userService: UserService) {
    this.userService.currentUserId.subscribe(id => this.currentUserId = id ?? '');
  }

  addDate(date: string): Observable<Chef> {
    console.log(this.currentUserId);
    return this.http.post<Chef>(`${this.apiBaseUrl}/Chef/${this.currentUserId}/dates`, JSON.stringify(date));
  }

  deleteDate(date: string): Observable<Chef> {
    console.log(this.currentUserId);
    return this.http.delete<Chef>(`${this.apiBaseUrl}/Chef/${this.currentUserId}/dates/${date}`);
  }

  loadAllChefs() {
    return this.http.get<Chef[]>(`${this.apiBaseUrl}/Chef/all`);
  }

  searchChefs(searchQuery: string): Observable<Chef[]> {
    return this.loadAllChefs().pipe(
      map(chefs => {
        if (!searchQuery) {
          return chefs;
        }
        return chefs.filter(chef =>
          (chef.firstName && chef.firstName.toLowerCase().includes(searchQuery.toLowerCase())) ||
          (chef.lastName && chef.lastName.toLowerCase().includes(searchQuery.toLowerCase()))
        );
      })
    );
  }

  sortChefs(sortBy: string): Observable<Chef[]> {
return this.loadAllChefs().pipe(
      map(chefs => {
        switch (sortBy) {
          case 'rating':
            return chefs.sort((a, b) => this.getRating(b) - this.getRating(a));
          case 'name':
            return chefs.sort((a, b) => {
              if (a.firstName === b.firstName) {
                if (a.lastName === b.lastName) {
                  return 0;
                }
                if (!a.lastName) {
                  return -1;
                }
                if (!b.lastName) {
                  return 1;
                }
                return a.lastName.localeCompare(b.lastName);
              }
              if (!a.firstName) {
                return -1;
              }
              if (!b.firstName) {
                return 1;
              }
              return a.firstName.localeCompare(b.firstName);
            });
          default:
            return chefs;
        }
      })
    );
  }

  getRating(chef: Chef) {
    let sum = 0;
    const marks = chef.reviewsReceived.map(review => review.rating);
    for (const mark of marks) {
      sum += mark;
    }
    return sum / marks.length;
  }
}

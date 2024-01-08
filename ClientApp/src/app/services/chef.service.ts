import {Injectable, OnInit} from '@angular/core';
import {Observable} from "rxjs";
import {Chef} from "../../swagger";
import {environment} from "../../environments/environment";
import {UserService} from "./user.service";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ChefService implements OnInit {
  private apiBaseUrl = environment.apiBaseUrl;
  currentUserId !: string;
  constructor(private http: HttpClient, private userService: UserService) {}

  ngOnInit() {
    this.userService.currentUserId.subscribe(id => this.currentUserId = id ?? '');
  }

  addDate(date: string): Observable<Chef> {
    return this.http.post<Chef>(`${this.apiBaseUrl}/Chef/${this.currentUserId}/dates`, JSON.stringify(date));
  }

  deleteDate(date: string): Observable<Chef> {
    return this.http.delete<Chef>(`${this.apiBaseUrl}/Chef/${this.currentUserId}/dates/${date}`);
  }

  loadAllChefs() {
    return this.http.get<Chef[]>(`${this.apiBaseUrl}/Chef/all`);
  }

  getRating(chef: Chef) {
    let sum = 0;
    console.log(chef.reviewsReceived);
    const marks = chef.reviewsReceived.map(review => review.rating);
    for (const mark of marks) {
      sum += mark;
    }
    return sum / marks.length;
  }
}

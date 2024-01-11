import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Review} from "../../swagger";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  private readonly apiUrl = environment.apiBaseUrl + '/Review';
  constructor(private http: HttpClient) { }

  postReview(chefId: string, rating: number, comment: string): Observable<Review> {
    const review = {
      chefId: chefId,
      rating: rating,
      comment: comment
    };
    return this.http.post<Review>(this.apiUrl, review);
  }
}

import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Chef, Customer, Order} from "../../swagger";
import {catchError, map, Observable, throwError} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  private apiUrl = environment.apiBaseUrl;
  constructor(private http: HttpClient) { }

  getOrdersForChef(chefId: string) : Observable<Order[]> {
    return this.http.get<Chef>(`${this.apiUrl}/Chef/${chefId}`).pipe(
      map(chef => chef.ordersReceived),
      catchError(error => {
        console.error(error);
        return throwError(error);
      })
    );
  }

  getOrdersForCustomer(customerId: string): Observable<Order[]> {
    return this.http.get<Customer>(`${this.apiUrl}/Customer/${customerId}`).pipe(
      map(customer => customer.ordersPlaced),
      catchError(error => {
        console.error(error);
        return throwError(error);
      })
    );
  }


  createOrder(order: Order) {
    this.http.post(`${this.apiUrl}/Order/${order.chefId}`, order).subscribe(
      (order: any) => {
        return order;
      },
      error => {
        console.error(error);
      }
    )
  }

  updateOrder(order: Order) {
    this.http.put(`${this.apiUrl}/Order/${order.id}`, order).subscribe(
      (order: any) => {
        return order;
      },
      error => {
        console.error(error);
      }
    )
  }

}

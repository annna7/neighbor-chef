import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Chef, Customer, Order, OrderStatus} from "../../swagger";
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

  updateOrderStatusAsChef(status: OrderStatus, orderId: string) {
    const reqBody = {
      status: status
    };
    this.http.put<Order>(`${this.apiUrl}/Chef/orders/${orderId}`, reqBody).subscribe(
      (order: any) => {
        return order;
      },
      error => {
        console.error(error);
      }
    )
  }

  updateOrderStatusAsCustomer(status: OrderStatus, orderId: string) {
    const reqBody = {
      status: status
    };
    this.http.put(`${this.apiUrl}/Customer/orders/${orderId}`, reqBody).subscribe(
      (order: any) => {
        return order;
      },
      error => {
        console.error(error);
      }
    )
  }
  getStatusMessage(status: OrderStatus) {
    switch (status) {
      case OrderStatus.PLACED:
        return "Placed";
      case OrderStatus.READY:
        return "Ready for pickup";
      case OrderStatus.PREPARING:
        return "Preparing";
      case OrderStatus.DELIVERED:
        return "Delivered";
      default:
        return "Unknown";
    }
  }
}

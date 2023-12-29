import {Component, OnInit} from '@angular/core';
import {Order} from "../../../swagger";
import {OrdersService} from "../../services/orders.service";
import {UserService} from "../../services";

@Component({
  selector: 'app-chef-orders',
  templateUrl: './chef-orders.component.html',
  styleUrl: './chef-orders.component.css'
})
export class ChefOrdersComponent implements OnInit{
  pastOrders !: Order[];
  upcomingOrders !: Order[];

  constructor(private orderService: OrdersService, private userService: UserService) {}

  ngOnInit() {
    this.loadOrders();
  }

  loadOrders() {
    this.orderService.getOrdersForChef(this.userService.getCurrentUserId() as string).subscribe(
      orders => {
        this.pastOrders = orders.filter(order => new Date(order.deliveryDate) < new Date());
        this.upcomingOrders = orders.filter(order => new Date(order.deliveryDate) >= new Date());
      },
      error => {
        throw new Error("failed to fetch orders for chef!");
      }
    )};
}

import {Component, Input, OnInit} from '@angular/core';
import {Order} from "../../../swagger";
import {UserService, OrdersService} from "../../services";

@Component({
  selector: 'app-chef-orders',
  templateUrl: './chef-orders.component.html',
  styleUrl: './chef-orders.component.css'
})
export class ChefOrdersComponent implements OnInit{
  @Input() showPastOrders: boolean = true;
  @Input() showUpcomingOrders: boolean = true;
  pastOrders !: Order[];
  upcomingOrders !: Order[];
  userId !: string;

  constructor(private orderService: OrdersService, private userService: UserService) {}

  ngOnInit() {
    this.userService.currentUserId.subscribe(id => {
      this.userId = id as string;
      this.loadOrders();
    });
  }

  loadOrders() {
    this.orderService.getOrdersForChef(this.userId).subscribe(
      orders => {
        this.pastOrders = orders.filter(order => new Date(order.deliveryDate) < new Date());
        this.upcomingOrders = orders.filter(order => new Date(order.deliveryDate) >= new Date());
      },
      error => {
        throw new Error("failed to fetch orders for chef!");
      }
    )};

  changeOrderStatus(order: Order) {
    this.orderService.updateOrderStatusAsChef(order.status, order.id);
  }
}

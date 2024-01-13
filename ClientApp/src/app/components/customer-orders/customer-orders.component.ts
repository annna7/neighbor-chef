import {Component, Input} from '@angular/core';
import {Order} from "../../../swagger";
import {UserService, OrdersService} from "../../services";

@Component({
  selector: 'app-customer-orders',
  templateUrl: './customer-orders.component.html',
  styleUrl: './customer-orders.component.css'
})
export class CustomerOrdersComponent {
  @Input() showPastOrders: boolean = true;
  @Input() showUpcomingOrders: boolean = true;
  pastOrders !: Order[];
  upcomingOrders !: Order[];
  userId !: string;

  constructor(private orderService: OrdersService, private userService: UserService) {}

  ngOnInit() {
    this.userService.currentUserId.subscribe(id => this.userId = id as string);
    this.loadOrders();
  }

  loadOrders() {
    this.orderService.getOrdersForCustomer(this.userId).subscribe(
      orders => {
        this.pastOrders = orders.filter(order => new Date(order.deliveryDate) < new Date());
        this.upcomingOrders = orders.filter(order => new Date(order.deliveryDate) >= new Date());
      },
      error => {
        throw new Error("failed to fetch orders for customer!");
      }
    )}

  changeOrderStatus(order: Order) {
    this.orderService.updateOrderStatusAsCustomer(order.status, order.id);
  }
}

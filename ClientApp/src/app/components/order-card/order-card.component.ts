import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Order, OrderStatus} from "../../../swagger";
import {MatCardModule} from "@angular/material/card";
import {MatInputModule} from "@angular/material/input";
import {MatSelectModule} from "@angular/material/select";
import {MatChipsModule} from "@angular/material/chips";
import {MatListModule} from "@angular/material/list";
import {UserService, OrdersService, PushNotificationsService} from "../../services";

@Component({
  selector: 'app-order-card',
  templateUrl: './order-card.component.html',
  styleUrl: './order-card.component.css'
})
export class OrderCardComponent {
  @Input() title!: string;
  @Input() order!: Order;
  @Input() isChef!: boolean;
  @Output() changeOrderStatus = new EventEmitter<Order>();
  deliveryDate: Date = new Date();
  displayName!: string;
  total!: number;

  chefDisplayName !: string;
  customerDisplayName !: string;

  constructor(private userService: UserService, protected orderService: OrdersService, private pushService: PushNotificationsService) {}
  ngOnInit() {
    this.deliveryDate = new Date(this.order.deliveryDate);
    if (this.isChef) {
      this.userService.getChefById(this.order.chefId).subscribe(user => {
        this.chefDisplayName = user.firstName + " " + user.lastName;
        this.userService.getCustomerById(this.order.customerId).subscribe(user => {
            this.customerDisplayName = user.firstName + " " + user.lastName;
            });
      });
    } else {
      this.userService.getCustomerById(this.order.customerId).subscribe(user => {
        this.customerDisplayName = user.firstName + " " + user.lastName;
        this.userService.getChefById(this.order.chefId).subscribe(user => {
          this.chefDisplayName = user.firstName + " " + user.lastName;
        });
      });
    }
    this.total = this.order.orderMeals.reduce((acc, orderMeal) => acc + orderMeal.quantity * orderMeal.meal.price, 0);
  }

  updateOrderStatus(status: OrderStatus) {
    this.order.status = status;
    this.changeOrderStatus.emit(this.order);
  }

  protected readonly OrderStatus = OrderStatus;
}

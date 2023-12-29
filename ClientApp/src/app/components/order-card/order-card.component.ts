import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Order, OrderStatus} from "../../../swagger";
import {MatCardModule} from "@angular/material/card";
import {MatInputModule} from "@angular/material/input";
import {MatSelectModule} from "@angular/material/select";
import {MatChipsModule} from "@angular/material/chips";
import {MatListModule} from "@angular/material/list";


@Component({
  selector: 'app-order-card',
  templateUrl: './order-card.component.html',
  styleUrl: './order-card.component.css'
})
export class OrderCardComponent {
  @Input() order!: Order;
  @Output() changeOrderStatus = new EventEmitter<Order>();

  ngOnInit() {
    console.log(this.order);
  }

  updateOrderStatus(status: OrderStatus) {
    this.order.status = status;
    this.changeOrderStatus.emit(this.order);
  }
}

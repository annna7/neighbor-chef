import { Injectable } from '@angular/core';
import {CreateOrderDto, DateDto, Meal, TimeDto} from '../../swagger';
import {OrdersService} from "./orders.service";
import {ChefMealItem} from "../models/chef-meal.dto";
import {BehaviorSubject} from "rxjs";

interface ChefMealDict {
  [chefId: string]: ChefMealItem[];
}

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private itemsSubject = new BehaviorSubject<ChefMealDict>({});
  items = this.itemsSubject.asObservable();

  constructor(private orderService: OrdersService) { }

  getMealQuantity(meal: Meal, chefId: string): number {
    const itemsDict = this.itemsSubject.getValue();
    const currentItems = itemsDict[chefId] || [];
    const item = currentItems.find(item => item.meal.id === meal.id);
    return item?.quantity || 0;
  }

  addMeal(meal: Meal, chefId: string, quantity: number): void {
    const itemsDict = this.itemsSubject.getValue();
    const currentItems = itemsDict[chefId] || [];
    this.itemsSubject.next({
      ...itemsDict,
      [chefId]: [...currentItems, { meal, quantity }]
    });
  }

  isCartEmpty(chefId: string): boolean {
    for (const chefId in this.itemsSubject.getValue()) {
      if (this.itemsSubject.getValue()[chefId]?.length > 0) {
        return false;
      }
    }
    return true;
  }
  updateQuantity(meal: Meal, chefId: string, quantity: number): void {
    const itemsDict = this.itemsSubject.getValue();
    const currentItems = itemsDict[chefId] || [];
    const updatedItems = currentItems.map(item =>
      item.meal.id === meal.id ? { ...item, quantity } : item
    ).filter(item => item.quantity > 0);
    this.itemsSubject.next({ ...itemsDict, [chefId]: updatedItems });
  }

  updateQuantityFromString(meal: Meal, chefId: string, quantity: string): void {
    const quantityNumber = parseInt(quantity);
    if (isNaN(quantityNumber)) {
      return;
    }
    return this.updateQuantity(meal, chefId, quantityNumber);
  }

  clearCart(chefId: string): void {
    const itemsDict = this.itemsSubject.getValue();
    const updatedItems = { ...itemsDict };
    delete updatedItems[chefId];
    this.itemsSubject.next(updatedItems);
  }

  getTotalPrice(chefId: string): number {
    const itemsDict = this.itemsSubject.getValue();
    const currentItems = itemsDict[chefId] || [];
    return currentItems.reduce((acc, item) => acc + item.meal.price * item.quantity, 0);
  }

  orderMeals(chefId: string, observations: string, deliveryDate: Date): void {
    const orderItems = this.itemsSubject.getValue()[chefId]?.map(item => {
        return {
          mealId: item.meal.id,
          quantity: item.quantity
        };
      }) || [];

    if (orderItems.length === 0) {
      throw new Error('No meals selected');
    }

    const order: CreateOrderDto = {
      mealWithQuantities: orderItems,
      deliveryDate: {day: deliveryDate.getDate(), month: deliveryDate.getMonth() + 1, year: deliveryDate.getFullYear()},
      deliveryTime: {hour: deliveryDate.getHours(), minute: deliveryDate.getMinutes()},
      observations
    };

    this.orderService.createOrder(order, chefId)
      .subscribe({
        next: () => this.clearCart(chefId),
        error: (err) => console.error('Failed to create order', err)
      });
  }
}

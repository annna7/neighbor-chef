import {Component, Inject, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {CartService} from "../../services";
import {FormBuilder, FormGroup} from "@angular/forms";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {ChefMealItem} from "../../models/chef-meal.dto";
import {filter, Subscription} from "rxjs";
import {MatCalendarCellCssClasses} from "@angular/material/datepicker";

@Component({
  selector: 'app-order-modal',
  templateUrl: './order-modal.component.html',
  styleUrl: './order-modal.component.css',
  encapsulation: ViewEncapsulation.None
})
export class OrderModalComponent implements OnInit {
  chefId!: string;
  availableDates!: string[];
  orderForm !: FormGroup;
  cartItems !: ChefMealItem[];
  private cartSubscription !: Subscription;
  constructor(
    protected cartService: CartService,
    public dialogRef: MatDialogRef<OrderModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder
  ) {
    this.orderForm = this.fb.group({
      observations: [''],
      deliveryDate: [null],
      deliveryTime: ['']
    });
    this.availableDates = data.availableDates;
    this.chefId = data.chefId;
  }

  dateClass = (date: any):  MatCalendarCellCssClasses => {
    return this.availableDates.some(d => d.startsWith(date.toISOString().split('T')[0])) ? 'available-date' : '';
  }

  dateFilter = (date: Date | null): boolean => {
    return this.availableDates.some(d => d.startsWith(date?.toISOString().split('T')[0] || ''));
  }

  ngOnInit(): void {
    this.cartSubscription = this.cartService.items.subscribe(items => {
      this.cartItems = items[this.chefId] || [];
    });
  }


  onSave(): void {
    const observations = this.orderForm.get('observations')?.value;
    const deliveryDate = this.orderForm.get('deliveryDate')?.value;
    const deliveryTime = this.orderForm.get('deliveryTime')?.value;
    this.cartService.orderMeals(this.chefId, observations, deliveryDate, deliveryTime);
    this.dialogRef.close();
  }

  ngOnDestroy(): void {
    this.cartSubscription.unsubscribe();
  }

  protected readonly filter = filter;
}

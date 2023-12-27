import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Meal } from '../../../swagger';

@Component({
  selector: 'app-meal-modal',
  templateUrl: './meal-modal.component.html',
  styleUrls: ['./meal-modal.component.css']
})
export class MealModalComponent {

  mealForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<MealModalComponent>,
    @Inject(MAT_DIALOG_DATA) public meal: Meal,
    private fb: FormBuilder
  ) {
    this.mealForm = this.fb.group({
      name: [meal?.name || ''],
      description: [meal?.description || ''],
      pictureUrl: [meal?.pictureUrl || ''],
      price: [meal?.price || 0],
      ingredients: [meal?.ingredients || ''],
      category: [meal?.category || '']
    });
  }

  onSave(): void {
    this.dialogRef.close(this.mealForm.value);
    console.log('new meal: ', this.mealForm.value);
  }
}

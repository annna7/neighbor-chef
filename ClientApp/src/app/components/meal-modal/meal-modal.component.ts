import {Component, Inject, OnInit} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {FormArray, FormBuilder, FormGroup} from '@angular/forms';
import {Category, Meal} from '../../../swagger';
import {CategoryService, MealService, UserService, ImageService, ImageType} from '../../services';
import {MatChipInputEvent} from "@angular/material/chips";
import {COMMA, ENTER} from "@angular/cdk/keycodes";
import {auto} from "@popperjs/core";
import {EventEmitter} from "@angular/core";

@Component({
  selector: 'app-meal-modal',
  templateUrl: './meal-modal.component.html',
  styleUrls: ['./meal-modal.component.css']
})
export class MealModalComponent implements OnInit {
  categories: Category[] = [];
  mealForm: FormGroup;
  removable = true;
  selectable = true;
  mealCreated = new EventEmitter();
  separatorKeysCodes: number[] = [ENTER, COMMA];
  ingredients: string[] = [];
  currentUserId !: string;
  uploadedImage !: File;

  constructor(
    public dialogRef: MatDialogRef<MealModalComponent>,
    @Inject(MAT_DIALOG_DATA) public meal: Meal,
    private fb: FormBuilder,
    private categoryService: CategoryService,
    private mealService: MealService,
    private userService: UserService,
    private imageService: ImageService,
  ) {
    this.mealForm = this.fb.group({
      name: [meal?.name || ''],
      description: [meal?.description || ''],
      pictureUrl: [meal?.pictureUrl || ''],
      price: [meal?.price || 0],
      categoryName: [meal?.categoryName || ''],
      ingredients: this.fb.array(meal?.ingredients || [])
    });
    this.ingredients = meal?.ingredients || [];
  }

  ngOnInit(): void {
    this.userService.currentUserId.subscribe(id => this.currentUserId = id ?? '');
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getCategories().subscribe(
      categories => {
        this.categories = categories;
      },
      error => {
        console.error('Failed to fetch categories', error);
      }
    );
  }

  addIngredient(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    if ((value || '').trim()) {
      this.ingredients.push(value.trim());
    }

    if (input) {
      input.value = '';
    }
  }

  removeIngredient(ingredient: any): void {
    const index = this.ingredients.indexOf(ingredient);

    if (index >= 0) {
      this.ingredients.splice(index, 1);
    }
  }

  onFileSelected(event: any): void {
    this.uploadedImage = event.target.files[0];
  }

  onSave(): void {
    this.imageService.uploadImage(this.uploadedImage, ImageType.Meals).subscribe(
      imageUrl => {
        this.mealForm.patchValue({pictureUrl: imageUrl});
        if (this.meal && this.meal.id) {
          this.mealService.updateMeal({
            ...this.mealForm.value,
            ingredients: this.ingredients,
            id: this.meal.id,
            chefId: this.currentUserId
          }).subscribe(
            () => {
              this.mealCreated.emit();
              this.dialogRef.close();
            },
            error => {
              console.error('Failed to update meal', error);
            });
        } else {
          this.mealService.createMeal({
            ...this.mealForm.value,
            ingredients: this.ingredients,
            chefId: this.currentUserId
          }).subscribe(
            (createdMeal) => {
              this.mealCreated.emit();
              this.dialogRef.close();
            },
            error => {
              console.error('Failed to create meal', error);
            });
        }
      });
  }
}

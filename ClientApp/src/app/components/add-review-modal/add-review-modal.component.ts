import {Component, Inject, Input, OnInit} from '@angular/core';
import {Chef} from "../../../swagger";
import {ReviewService} from "../../services/review.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {FormBuilder, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-add-review-modal',
  templateUrl: './add-review-modal.component.html',
  styleUrl: './add-review-modal.component.css'
})
export class AddReviewModalComponent {
  reviewForm !: FormGroup;
  constructor(
    public dialogRef: MatDialogRef<AddReviewModalComponent>,
    @Inject(MAT_DIALOG_DATA) public chefId: string,
    private reviewService: ReviewService,
    private fb: FormBuilder
  ) {
    this.reviewForm = this.fb.group({
      rating: [1],
      comment: ['']
    });
  }

  setRating(rating: number) {
    this.reviewForm.get('rating')?.setValue(rating);
  }

  onSubmitReview() {
    if (this.reviewForm.valid) {
      const rating = this.reviewForm.get('rating')?.value;
      const comment = this.reviewForm.get('comment')?.value;
      this.reviewService.postReview(this.chefId, rating, comment).subscribe(() => {
        this.dialogRef.close();
      });
    }
  }
}

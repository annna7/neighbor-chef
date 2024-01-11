import {Component, Input} from '@angular/core';
import {Review} from "../../../swagger";
import {UserService} from "../../services/user.service";
import {AddReviewModalComponent} from "../add-review-modal/add-review-modal.component";
import {MatDialog} from "@angular/material/dialog";
import {DialogRef} from "@angular/cdk/dialog";


@Component({
  selector: 'app-chef-reviews',
  templateUrl: './chef-reviews.component.html',
  styleUrl: './chef-reviews.component.scss'
})

export class ChefReviewsComponent {
  @Input() chefId !: string;
  @Input() title !: string;
  @Input() limit : number | undefined;
  @Input() sessionUserId !: string;
  reviews: Review[] = [];

  constructor(private userService: UserService, public dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadReviews();
    this.userService.currentUserId.subscribe(userId => {
      this.sessionUserId = userId as string;
    });
  }

  private loadReviews() : void {
    this.userService.getChefById(this.chefId).subscribe(chef => {
      this.reviews = chef.reviewsReceived.filter(review => review.comment !== '');
      if (this.limit !== undefined) {
        this.reviews = this.reviews.slice(0, this.limit);
      }
    });
  }

  openAddReviewModal() {
    const dialogRef = this.dialog.open(AddReviewModalComponent, {
      width: '750px',
      data: this.chefId
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadReviews();
    });
  }
}

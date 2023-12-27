import {Component, Input} from '@angular/core';
import {Review} from "../../../swagger";
import {UserService} from "../../services/user.service";


@Component({
  selector: 'app-chef-reviews',
  templateUrl: './chef-reviews.component.html',
  styleUrl: './chef-reviews.component.scss'
})

export class ChefReviewsComponent {
  @Input() chefId !: string;
  reviews: Review[] = [];

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadReviews();
  }

  private loadReviews() : void {
    this.userService.getChefById(this.chefId).subscribe(chef => {
      this.reviews = chef.reviewsReceived.filter(review => review.comment !== '')
    });
  }

}

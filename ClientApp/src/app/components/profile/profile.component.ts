import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserService } from '../../services/user.service';
import {ActivatedRoute} from "@angular/router";
import {Chef, Customer} from "../../../swagger";
import moment from 'moment-timezone';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {
  profileForm!: FormGroup;
  isSelf: boolean = false;
  profileRole !: string;
  profileId !: string;

  showProfile: boolean = true;
  showReviews: boolean = true;
  showAvailability: boolean = true;
  availableDates: Date[] = [];
  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private activatedRoute: ActivatedRoute,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.profileId = params['id'];
      this.userService.getRole(this.profileId).subscribe(role => {
        this.profileRole = role;
        this.loadUserData();
      });
    });
  }

  private loadUserData(): void {
    const profileId = this.activatedRoute.snapshot.paramMap.get('id');
    if (!profileId) {
      return;
    }
    this.userService.getUserById(profileId).subscribe(user => {
      const currentUserId = this.userService.getCurrentUserId();
      this.isSelf = currentUserId === profileId;
      this.availableDates = (user as Chef).availableDates;
    });
  }

  updateDates(date: Date | string) {
    if (this.isSelf) {
      if (!(date instanceof Date)) {
        date = new Date(date);
      }

      const dateNum = date.getTime();
      const convertedAvailableDates = this.availableDates.map(d => d instanceof Date ? d : new Date(d));
      const index = convertedAvailableDates.findIndex(d => d.getTime() === dateNum);
      const dateString = moment.tz(date, 'Europe/Bucharest').format();
      if (index > -1) {
        this.availableDates.splice(index, 1);
        this.userService.deleteDate(dateString).subscribe();
      } else {
        this.availableDates.push(date);
        this.userService.addDate(dateString).subscribe();
      }
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserService } from '../../services/user.service';
import {ActivatedRoute} from "@angular/router";
import {Chef, Customer} from "../../../swagger";
import moment from 'moment-timezone';
import {ChefService} from "../../services";

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
  sessionUserId !: string;

  showProfile: boolean = true;
  showReviews: boolean = true;
  showAvailability: boolean = true;
  availableDates: string[] = [];
  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private activatedRoute: ActivatedRoute,
    private route: ActivatedRoute,
    private chefService: ChefService
  ) {}

  ngOnInit(): void {
    this.userService.currentUserId.subscribe(id => this.sessionUserId = id ?? '');
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
      this.isSelf = this.sessionUserId === profileId;
      this.availableDates = (user as Chef).availableDates;
    });
  }

  updateDates(date: Date | string) {
    if (this.isSelf) {
      if (!(date instanceof Date)) {
        date = new Date(date);
      }

      const dateNum = date.getTime();
      const index = this.availableDates.findIndex(d => (new Date(d)).getTime() === dateNum);
      const dateString = moment.tz(date, 'Europe/Bucharest').format();
      if (index > -1) {
        this.availableDates.splice(index, 1);
        this.chefService.deleteDate(dateString).subscribe();
      } else {
        this.availableDates.push(date.toString());
        this.chefService.addDate(dateString).subscribe();
      }
    }
  }
}

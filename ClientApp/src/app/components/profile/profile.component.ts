import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserService } from '../../services/user.service';
import {ActivatedRoute} from "@angular/router";
import {Chef, Customer} from "../../../swagger";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  profileForm!: FormGroup;
  isEditable: boolean = false;
  role !: string;
  profileId !: string;

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
        this.role = role;
        this.initializeForm();
        this.loadUserData();
      });
    });
  }

  private initializeForm(): void {
    this.profileForm = this.fb.group({
      firstName: '',
      lastName: '',
      applicationUser: this.fb.group({
        email: '',
        phoneNumber: ''
      }),
      address: this.fb.group({
        street: '',
        city: '',
        county: '',
        country: '',
        zipCode: '',
        streetNumber: '',
        apartment: '',
      }),
      profilePictureUrl: ''
    });

    if (this.role === 'Chef') {
      this.profileForm.addControl('description', this.fb.control(''));
      this.profileForm.addControl('rating', this.fb.control(0));
      this.profileForm.addControl('maxOrdersPerDay', this.fb.control(0));
      this.profileForm.addControl('advanceNoticeDays', this.fb.control(0));
    }
  }

  private loadUserData(): void {
    const profileId = this.activatedRoute.snapshot.paramMap.get('id');
    if (!profileId) {
      return;
    }
    this.userService.getUserById(profileId).subscribe(user => {
      console.log(user);
      this.profileForm.patchValue(user);
      this.checkEditPermission(profileId);
    });
  }

  private checkEditPermission(profileId: string): void {
    const currentUserId = this.userService.getCurrentUserId();
    this.isEditable = currentUserId === profileId;
  }

  saveProfile(): void {
    if (this.isEditable && this.profileForm.valid) {
      const profileData = this.profileForm.value;
      this.userService.updatePerson(profileData).subscribe(() => {
        this.profileForm.markAsPristine();
      });
    }
  }
}

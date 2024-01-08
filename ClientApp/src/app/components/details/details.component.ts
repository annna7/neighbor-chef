import {Component, Input, SimpleChanges} from '@angular/core';
import {MatButtonModule} from "@angular/material/button";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {NgIf} from "@angular/common";
import {FormBuilder, FormGroup, ReactiveFormsModule} from "@angular/forms";
import {UserService} from "../../services";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrl: './details.component.css'
})
export class DetailsComponent {
  @Input() profileId !: string;
  @Input() profileRole !: string;
  @Input() isSelf !: boolean;
  profileForm!: FormGroup;
  currentUserId !: string;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private activatedRoute: ActivatedRoute,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.userService.currentUserId.subscribe(id => this.currentUserId = id ?? '');
    this.loadUserData();
    this.initializeForm();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.profileRole) {
      this.initializeForm();
      this.loadUserData();
    }
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

    if (this.profileRole === 'Chef') {
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
      this.profileForm.patchValue(user);

      this.isSelf = this.currentUserId === profileId;
    });
  }

  saveProfile(): void {
    if (this.isSelf && this.profileForm.valid) {
      const profileData = this.profileForm.value;
      this.userService.updatePerson(profileData).subscribe(() => {
        this.profileForm.markAsPristine();
      });
    }
  }
}

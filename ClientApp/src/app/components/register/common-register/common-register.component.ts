import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-common-register',
  templateUrl: './common-register.component.html',
  styleUrls: ['./common-register.component.css']
})
export class CommonRegisterComponent implements OnInit {
  commonForm!: FormGroup;
  addressFields = ['street', 'city', 'county', 'country', 'zipCode', 'streetNumber', 'apartmentNumber'];

  constructor(private formBuilder: FormBuilder, private router: Router) {}

  ngOnInit(): void {
    this.commonForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      phoneNumber: ['', Validators.required],
      pictureUrl: [''],
      address: this.formBuilder.group({
        street: ['', Validators.required],
        city: ['', Validators.required],
        county: ['', Validators.required],
        country: ['', Validators.required],
        zipCode: ['', Validators.required],
        streetNumber: [''],
        apartmentNumber: ['']
      }),
      type: ['', Validators.required] // 'chef' or 'customer'
    });
  }

  confirmSelection(): void {
    if (this.commonForm.valid) {
      localStorage.setItem('form_common_data', JSON.stringify(this.commonForm.value));
      const role = this.commonForm.value.type.toLowerCase();
      this.router.navigate([`/register/${role}`]);
    }
  }
}

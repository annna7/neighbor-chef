import { Component } from '@angular/core';
import {FormBuilder, FormGroup} from "@angular/forms";
import {Router} from "@angular/router";

@Component({
  selector: 'app-register',
  templateUrl: './common-register.component.html',
  styleUrl: './common-register.component.css'
})

export class CommonRegisterComponent {
  commonForm: FormGroup;
  constructor(private formBuilder: FormBuilder, private router: Router) {
    this.commonForm = this.formBuilder.group({
      // ...common fields
    });
  }

  confirmSelection(role: string) {
    // Save common data to a service or local storage
    // Navigate to specific form based on role
    if (role === 'chef') {
      this.router.navigate(['/register/chef']);
    } else if (role === 'customer') {
      this.router.navigate(['/register/customer']);
    }
  }
}

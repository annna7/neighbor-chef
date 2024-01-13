import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from "@angular/forms";
import {UserService} from "../../../services";
import {Router} from "@angular/router";

@Component({
  selector: 'app-customer-register',
  templateUrl: './customer-register.component.html',
  styleUrl: './customer-register.component.css'
})
export class CustomerRegisterComponent implements OnInit {
  customerForm!: FormGroup;
  constructor (private formBuilder: FormBuilder, private userService: UserService, private router: Router) {}
  ngOnInit(): void {
    this.customerForm = this.formBuilder.group({});
  }

  registerCustomer(): void {
    if (this.customerForm.valid) {
      const customerData = {
        ...JSON.parse(localStorage.getItem('form_common_data') || '{}'),
        ...this.customerForm.value
      };

      this.userService.register(customerData).subscribe(
        {
          next: () => {
            localStorage.removeItem('form_common_data');
            this.router.navigate(['/login']);
          },
          error: (err: any) => {
            console.error(err);
          }
        }
      );
    }
  }
}

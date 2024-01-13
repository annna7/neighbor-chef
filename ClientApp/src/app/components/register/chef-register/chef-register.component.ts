import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {UserService} from "../../../services";
import {Router} from "@angular/router";

@Component({
  selector: 'app-chef-register',
  templateUrl: './chef-register.component.html',
  styleUrls: ['./chef-register.component.css']
})
export class ChefRegisterComponent implements OnInit {
  chefForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private userService: UserService, private router: Router) {}

  ngOnInit(): void {
    this.chefForm = this.formBuilder.group({
      description: ['', Validators.required],
      advanceNoticeDays: ['', [Validators.required, Validators.min(1)]],
      maxOrdersPerDay: ['', [Validators.required, Validators.min(1)]],
    });
  }

  registerChef(): void {
    if (this.chefForm.valid) {
      const chefData = {
        ...JSON.parse(localStorage.getItem('form_common_data') || '{}'),
        ...this.chefForm.value
      };

      this.userService.register(chefData).subscribe(
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

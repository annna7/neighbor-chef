import { Component } from '@angular/core';
import {LoginDto} from "../../models/people/login.dto";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";
import {StorageService} from "../../services/storage.service";
import {UserService} from "../../services/user.service";
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {PersonDto} from "../../models/people/person.dto";
import {error} from "@angular/compiler-cli/src/transformers/util";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {
  loginForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private storageService: StorageService,
    private userService: UserService,
    private router: Router,
    private formModule: FormsModule,
    private reactiveFormModule: ReactiveFormsModule
  ) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
      role: ['', [Validators.required]]
    });
  }

  login() {
    if (this.loginForm.invalid) {
      return;
    }

    const credentials: LoginDto = {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password
    };

    this.authService.login(credentials).subscribe({
      next: (response) => {
        this.storageService.setToken(response.Token);
        this.getUserDetails();
      },
      error: (err) => console.error('Login Error:', err)
    });
  }

  private getUserDetails() {
    const role = this.loginForm.value.role;
    switch (role) {
      case 'Chef':
        localStorage.setItem('role', 'Chef');
        this.userService.getChef().subscribe({
          next: (response) => {
            this.storageService.setUser(response);
          },
          error: (err) => console.error('Login Error:', err)
        });
        break;
      case 'Customer':
        localStorage.setItem('role', 'Customer');
        this.userService.getCustomer().subscribe({
          next: (response) => {
            this.storageService.setUser(response);
          },
          error: (err) => console.error('Login Error:', err)
        });
        break;
      default:
        console.error('Invalid Role');
    }
    this.router.navigate(['/dashboard']);
  }
}

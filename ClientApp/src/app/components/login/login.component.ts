import { Component } from '@angular/core';
import {LoginDto} from "../../models/login.dto";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";
import {StorageService} from "../../services/storage.service";
import {UserService} from "../../services/user.service";
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {PersonDto} from "../../models/person.dto";

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
      password: ['', [Validators.required]]
    });
  }

  test() {
    this.authService.test().subscribe(response => {
      console.log(response);
    });
  }
  login() {
    console.log(this.loginForm.value);
    if (this.loginForm.invalid) {
      return;
    }

    const credentials: LoginDto = this.loginForm.value;
    this.authService.login(credentials).subscribe(response => {
      if (response) {
        this.storageService.setToken((response as any).token);
        this.userService.getUser().subscribe(response => {
          if (response) {
            this.storageService.setUser(response);
            this.router.navigate(['/']);
          } else {
            // TODO: Handle
            console.error(response, "Error at parsing response");
          }
        });
      } else {
        // TODO: Handle
        console.error("Error at parsing response");
      }
    });

  }
}

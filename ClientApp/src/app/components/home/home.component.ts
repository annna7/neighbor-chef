import { Component } from '@angular/core';
import {AuthService} from "../../services";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  authenticated: boolean = this.authService.isLoggedIn();
  constructor(private authService : AuthService) { }

}

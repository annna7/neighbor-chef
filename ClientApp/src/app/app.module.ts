import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatButtonModule} from "@angular/material/button";
import {MatCardModule} from "@angular/material/card";
import {MatIconModule} from "@angular/material/icon";
import {MatMenuModule} from "@angular/material/menu";
import {HomeComponent} from "./components/home/home.component";
import {NavMenuComponent} from "./components/nav-menu/nav-menu.component";
import {FetchDataComponent} from "./garbage/fetch-data/fetch-data.component";
import {FooterComponent} from "./components/footer/footer.component";
import {LoginComponent} from "./components/login/login.component";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {AuthInterceptor} from "./interceptors/auth.interceptor";
import {AppRoutingModule} from "./app-routing.module";
import {ChefRegisterComponent} from "./components/register/chef-register/chef-register.component";
import {CommonRegisterComponent} from "./components/register/common-register/common-register.component";
import {CustomerRegisterComponent} from "./components/register/customer-register/customer-register.component";

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    FetchDataComponent,
    FooterComponent,
    LoginComponent,
    ChefRegisterComponent,
    CommonRegisterComponent,
    CustomerRegisterComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    AppRoutingModule,
    MatButtonModule,
    MatCardModule,
    MatToolbarModule,
    BrowserAnimationsModule,
    MatIconModule,
    MatMenuModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

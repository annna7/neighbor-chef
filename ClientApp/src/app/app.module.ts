import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
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
import { CamelCaseToFormattedTextPipe } from './pipes/camel-case-to-formatted-text.pipe';
import {JsonFormatInterceptor} from "./interceptors/json-format.interceptor";
import {ParentDashboardComponent} from "./components/dashboard/parent-dashboard/parent-dashboard.component";
import {ChefDashboardComponent} from "./components/dashboard/chef-dashboard/chef-dashboard.component";
import {CustomerDashboardComponent} from "./components/dashboard/customer-dashboard/customer-dashboard.component";
import {ProfileComponent} from "./components/profile/profile.component";
import {MatInputModule} from "@angular/material/input";
import {MatSelectModule} from "@angular/material/select";
import {MatOptionModule} from "@angular/material/core";
import {MatExpansionModule} from "@angular/material/expansion";
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
    CamelCaseToFormattedTextPipe,
    ParentDashboardComponent,
    CustomerDashboardComponent,
    ChefDashboardComponent,
    ProfileComponent
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
    MatInputModule,
    MatSelectModule,
    MatOptionModule,
    MatExpansionModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JsonFormatInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

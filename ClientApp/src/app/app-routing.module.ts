import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from "./components/login/login.component";
import {AuthGuard} from "./guards/auth.guard";
import {HomeComponent} from "./components/home/home.component";
import {LoggedInGuard} from "./guards/logged-in.guard";
import {CommonRegisterComponent} from "./components/register/common-register/common-register.component";
import {ChefRegisterComponent} from "./components/register/chef-register/chef-register.component";
import {CustomerRegisterComponent} from "./components/register/customer-register/customer-register.component";
import {ParentDashboardComponent} from "./components/dashboard/parent-dashboard/parent-dashboard.component";
import {ProfileComponent} from "./components/profile/profile.component";
import {ChefMealsComponent} from "./components/chef-meals/chef-meals.component";
import {ChefGuard} from "./guards/chef.guard";
import {ChefOrdersComponent} from "./components/chef-orders/chef-orders.component";

const routes: Routes = [
  { path: '', pathMatch: 'full', component: HomeComponent, canActivate: [LoggedInGuard] },
  { path: 'login', pathMatch: 'full', component: LoginComponent, canActivate: [LoggedInGuard] },
  { path: 'register', pathMatch: 'full', component: CommonRegisterComponent, canActivate: [LoggedInGuard] },
  { path: 'register/chef', pathMatch: 'full', component: ChefRegisterComponent, canActivate: [LoggedInGuard] },
  { path: 'register/customer', pathMatch: 'full', component: CustomerRegisterComponent, canActivate: [LoggedInGuard] },
  { path: 'dashboard', pathMatch: 'full', component: ParentDashboardComponent, canActivate: [AuthGuard] },
  { path: 'profile/:id', pathMatch: 'full', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'meals', pathMatch: 'full', component: ChefMealsComponent, canActivate: [AuthGuard, ChefGuard] },
  { path: 'orders', pathMatch: 'full', component: ChefOrdersComponent, canActivate: [AuthGuard, ChefGuard ] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

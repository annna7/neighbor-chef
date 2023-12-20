import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from "./components/login/login.component";
import {ChefDashboardComponent} from "./chef-dashboard/chef-dashboard.component";
import {AuthGuard} from "./guards/auth.guard";
import {HomeComponent} from "./components/home/home.component";
import {LoggedInGuard} from "./guards/logged-in.guard";

const routes: Routes = [
  { path: '', pathMatch: 'full', component: HomeComponent, canActivate: [LoggedInGuard] },
  { path: 'login', pathMatch: 'full', component: LoginComponent, canActivate: [LoggedInGuard] },
  { path: 'dashboard', pathMatch: 'full', component: ChefDashboardComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

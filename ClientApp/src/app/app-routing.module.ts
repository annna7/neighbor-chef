import { NgModule } from '@angular/core';
import {Routes, RouterModule, ROUTES, Router} from '@angular/router';
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
import {CustomerDashboardComponent} from "./components/dashboard/customer-dashboard/customer-dashboard.component";
import {CustomerGuard} from "./guards/customer.guard";
import {CustomerOrdersComponent} from "./components/customer-orders/customer-orders.component";
import {AuthService, UserService} from "./services";
import {BrowseComponent} from "./components/browse/browse.component";

const standardRoutes: Routes = [
  { path: '', pathMatch: 'full', component: HomeComponent, canActivate: [LoggedInGuard] },
  { path: 'login', pathMatch: 'full', component: LoginComponent, canActivate: [LoggedInGuard] },
  { path: 'register', pathMatch: 'full', component: CommonRegisterComponent, canActivate: [LoggedInGuard] },
  { path: 'register/chef', pathMatch: 'full', component: ChefRegisterComponent, canActivate: [LoggedInGuard] },
  { path: 'register/customer', pathMatch: 'full', component: CustomerRegisterComponent, canActivate: [LoggedInGuard] },
  { path: 'dashboard', pathMatch: 'full', component: ParentDashboardComponent, canActivate: [AuthGuard] },
  { path: 'profile/:id', pathMatch: 'full', component: ProfileComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(standardRoutes)],
  exports: [RouterModule],
  // providers: [
  //   {
  //     provide: ROUTES,
  //     useFactory: () => {
  //       let routes: Routes = [];
  //       let userRole = localStorage.getItem('role');
  //       console.log(userRole, 'ROUTING');
  //       if (userRole === 'Chef') {
  //         routes = [
  //           { path: 'meals', pathMatch: 'full', component: ChefMealsComponent, canActivate: [AuthGuard, ChefGuard] },
  //           { path: 'orders', pathMatch: 'full', component: ChefOrdersComponent, canActivate: [AuthGuard, ChefGuard ] },
  //         ];
  //       } else if (userRole === 'Customer') {
  //         routes = [
  //           { path: 'orders', pathMatch: 'full', component: CustomerOrdersComponent, canActivate: [AuthGuard, CustomerGuard ] },
  //           { path: 'browse', pathMatch: 'full', component: BrowseComponent, canActivate: [AuthGuard, CustomerGuard ] },
  //         ];
  //       }
  //
  //     return [
  //       ...standardRoutes,
  //       ...routes
  //     ];
  //     },
  //     multi: true
  //   }
  // ]
})

export class AppRoutingModule {
  constructor(private router: Router, private authService: AuthService) {
    authService.currentRole.subscribe(role => {
      this.updateRoutesBasedOnRole(role ? role : undefined);
    });
  }

  private updateRoutesBasedOnRole(role: string | undefined) {
    let routes: Routes = standardRoutes;
    if (role === 'Chef') {
      routes = [
        ...routes,
        { path: 'meals', pathMatch: 'full', component: ChefMealsComponent, canActivate: [AuthGuard, ChefGuard] },
        { path: 'orders', pathMatch: 'full', component: ChefOrdersComponent, canActivate: [AuthGuard, ChefGuard ] },
      ];
    } else if (role === 'Customer') {
      routes = [
        ...routes,
        { path: 'orders', pathMatch: 'full', component: CustomerOrdersComponent, canActivate: [AuthGuard, CustomerGuard ] },
        { path: 'browse', pathMatch: 'full', component: BrowseComponent, canActivate: [AuthGuard, CustomerGuard ] },
      ];
    }

    this.router.resetConfig(routes);
  }
}

import { BrowserModule } from '@angular/platform-browser';
import { NgModule, isDevMode } from '@angular/core';
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
import {MatNativeDateModule, MatOptionModule} from "@angular/material/core";
import {MatExpansionModule} from "@angular/material/expansion";
import {ChefReviewsComponent} from "./components/chef-reviews/chef-reviews.component";
import {ChefMealsComponent} from "./components/chef-meals/chef-meals.component";
import {MealCardComponent} from "./components/meal-card/meal-card.component";
import {MatListModule} from "@angular/material/list";
import {MealModalComponent} from "./components/meal-modal/meal-modal.component";
import {MatChipsModule} from "@angular/material/chips";
import {MatAutocompleteModule} from "@angular/material/autocomplete";
import {ChefOrdersComponent} from "./components/chef-orders/chef-orders.component";
import {OrderCardComponent} from "./components/order-card/order-card.component";
import {DetailsComponent} from "./components/details/details.component";
import {MatSlideToggleModule} from "@angular/material/slide-toggle";
import {AvailabilityCalendarComponent} from "./components/availability-calendar/availability-calendar.component";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MatHeaderRow} from "@angular/material/table";
import {CustomerOrdersComponent} from "./components/customer-orders/customer-orders.component";
import {SearchBarComponent} from "./components/search-bar/search-bar.component";
import {MatRadioModule} from "@angular/material/radio";
import {BrowseComponent} from "./components/browse/browse.component";
import {ChefCardComponent} from "./components/chef-card/chef-card.component";
import {OrderModalComponent} from "./components/order-modal/order-modal.component";
import {MatDialogActions, MatDialogContainer, MatDialogContent} from "@angular/material/dialog";
import {AddReviewModalComponent} from "./components/add-review-modal/add-review-modal.component";
import {TitleDirective} from "./directives/title.directive";
import {SubtitleDirective} from "./directives/subtitle.directive";
import {ImageLoaderDirective} from "./directives/image-loader.directive";
import {environment} from "../environments/environment";
import {AngularFireModule} from "@angular/fire/compat";
import {RatingPipe} from "./pipes/rating.pipe";
import {getMessaging} from "@angular/fire/messaging";
import firebase from "firebase/compat";
import messaging = firebase.messaging;
import {AngularFireMessagingModule} from "@angular/fire/compat/messaging";
import { ServiceWorkerModule } from '@angular/service-worker';
import {
  NotificationsPermissionsModalComponent
} from "./components/notification-permissions-modal/notifications-permissions-modal.component";
import {
  NotificationReceivedModalComponent
} from "./components/notification-received-modal/notification-received-modal.component";

@NgModule({
  bootstrap: [AppComponent],
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
    ProfileComponent,
    ChefReviewsComponent,
    ChefMealsComponent,
    MealCardComponent,
    MealModalComponent,
    ChefOrdersComponent,
    OrderCardComponent,
    DetailsComponent,
    AvailabilityCalendarComponent,
    CustomerOrdersComponent,
    SearchBarComponent,
    BrowseComponent,
    ChefCardComponent,
    OrderModalComponent,
    AddReviewModalComponent,
    TitleDirective,
    SubtitleDirective,
    ImageLoaderDirective,
    RatingPipe,
    NotificationsPermissionsModalComponent,
    NotificationReceivedModalComponent
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
    MatExpansionModule,
    MatListModule,
    MatChipsModule,
    MatAutocompleteModule,
    MatSlideToggleModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatRadioModule,
    MatDialogActions,
    MatDialogContent,
    AngularFireModule.initializeApp(environment.firebaseConfig),
    AngularFireMessagingModule,
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: !isDevMode(),
      // Register the ServiceWorker as soon as the application is stable
      // or after 30 seconds (whichever comes first).
      registrationStrategy: 'registerWhenStable:30000'
    }),
    MatDialogContainer
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JsonFormatInterceptor, multi: true},
  ]
})
export class AppModule {
}

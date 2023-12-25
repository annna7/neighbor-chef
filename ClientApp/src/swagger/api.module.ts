import { NgModule, ModuleWithProviders, SkipSelf, Optional } from '@angular/core';
import { Configuration } from './configuration';
import { HttpClient } from '@angular/common/http';
import { AccountService } from './api/account.service';
import { CategoryService } from './api/category.service';
import { ChefService } from './api/chef.service';
import { CustomerService } from './api/customer.service';
import { MealService } from './api/meal.service';
import { OidcConfigurationService } from './api/oidcConfiguration.service';
import { OrderService } from './api/order.service';
import { PersonService } from './api/person.service';
import { ReviewService } from './api/review.service';
import { WeatherForecastService } from './api/weatherForecast.service';
import { Observable } from 'rxjs';

@NgModule({
  imports:      [],
  declarations: [],
  exports:      [],
  providers: [
    AccountService,
    CategoryService,
    ChefService,
    CustomerService,
    MealService,
    OidcConfigurationService,
    OrderService,
    PersonService,
    ReviewService,
    WeatherForecastService ]
})
export class ApiModule {
    public static forRoot(configurationFactory: () => Configuration): ModuleWithProviders<ApiModule> {
        return {
            ngModule: ApiModule,
            providers: [ { provide: Configuration, useFactory: configurationFactory } ]
        };
    }

    constructor( @Optional() @SkipSelf() parentModule: ApiModule,
                 @Optional() http: HttpClient) {
        if (parentModule) {
            throw new Error('ApiModule is already loaded. Import in your base AppModule only.');
        }
        if (!http) {
            throw new Error('You need to import the HttpClientModule in your AppModule! \n' +
            'See also https://github.com/angular/angular/issues/20575');
        }
    }
}

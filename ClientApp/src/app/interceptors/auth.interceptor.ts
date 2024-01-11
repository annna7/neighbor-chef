import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { StorageService } from '../services/storage.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private storageService: StorageService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    request = request.clone({
      setHeaders: {
        'Content-Type': 'application/json',
      },
    });

    console.log(request);

    const token = this.storageService.getToken();

    if (request.body && request.headers.get('Content-Type') === 'application/json') {
      let newBody = JSON.parse(JSON.stringify(request.body).trim());
      request = request.clone({ body: newBody });
    }

    if (token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        },

      });
    }

    return next.handle(request);
  }
}

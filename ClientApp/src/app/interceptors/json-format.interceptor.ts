import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class JsonFormatInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.body && req.headers.get('Content-Type') === 'application/json') {
      const modifiedReq = req.clone({
        body: toPascalCase(req.body)
      });

      return next.handle(modifiedReq);
    }

    return next.handle(req);
  }
}

function toPascalCase(obj: any): any {
  if (Array.isArray(obj)) {
    return obj.map(toPascalCase);
  } else if (obj !== null && obj.constructor === Object) {
    return Object.keys(obj).reduce((result, key) => {
      const newKey = key.charAt(0).toUpperCase() + key.slice(1);
      result[newKey] = toPascalCase(obj[key]);
      return result;
    }, {} as any);
  }
  return obj;
}

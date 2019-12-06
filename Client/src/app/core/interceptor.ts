import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {AuthService} from '../services/auth.service';

export class Interceptor implements HttpInterceptor {
   constructor() {
   }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const accessToken: string = localStorage.getItem('accessToken');
    const refreshToken: string = localStorage.getItem('refreshToken');
    if (accessToken) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${accessToken}`
        }
      });
    }
    return next.handle(req).pipe(
       catchError(
         async  error => {
           const status = false;
           if (error.status === 401) {
             localStorage.removeItem('accessToken');
             localStorage.removeItem('refreshToken');
             localStorage.removeItem('User');
             window.location.href = '/auth/SingIn';
           }
           throw  {status};
         }
       )
     );
  }
}

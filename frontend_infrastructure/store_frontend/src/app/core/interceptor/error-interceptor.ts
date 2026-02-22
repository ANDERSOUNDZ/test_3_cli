import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Notification } from '../services/notification';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const notifyService = inject(Notification);

  return next(req).pipe(
    catchError((error) => {
      let errorMsg = error.error?.message || 'Ocurrió un error inesperado';
      notifyService.notify(errorMsg, true);
      return throwError(() => error);
    }),
  );
};

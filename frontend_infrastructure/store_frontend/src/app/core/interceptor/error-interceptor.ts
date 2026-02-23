import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let errorMessages: string[] = [];

      if (error.status === 400 && error.error && error.error.errors) {
        const serverErrors = error.error.errors;
        errorMessages = Object.values(serverErrors).flat() as string[];
      }

      return throwError(() => ({
        status: error.status,
        messages: errorMessages,
        originalError: error,
      }));
    }),
  );
};

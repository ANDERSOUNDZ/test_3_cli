import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      // Tipamos explícitamente como string[] para eliminar el error 7034/7005
      let errorMessages: string[] = [];
      
      if (error.status === 400 && error.error && error.error.errors) {
        // Forzamos el tipo del valor extraído para que TS sepa que son mensajes de texto
        const serverErrors = error.error.errors;
        errorMessages = Object.values(serverErrors).flat() as string[];
      }
      
      return throwError(() => ({
        status: error.status,
        messages: errorMessages,
        originalError: error
      }));
    })
  );
};

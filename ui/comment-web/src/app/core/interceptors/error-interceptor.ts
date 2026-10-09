import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError, throwError } from 'rxjs';
import { ApiResponse } from '../api/api-response.model';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const snackBar = inject(MatSnackBar);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      const body = error.error as ApiResponse<unknown> | null;
      const message =
        body?.message ?? (error.status === 0 ? 'Cannot reach the server' : 'Something went wrong');

      snackBar.open(message, 'Close', { duration: 4000 });
      return throwError(() => error);
    }),
  );
};

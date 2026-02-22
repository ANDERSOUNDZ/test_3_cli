import { inject, Injectable, signal } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
@Injectable({
  providedIn: 'root',
})
export class Notification {
  private _snackBar = inject(MatSnackBar);  
  message = signal<{text: string, type: 'success' | 'error'} | null>(null);
  notify(text: string, isError: boolean = false) {
    this.message.set({ text, type: isError ? 'error' : 'success' });
    this._snackBar.open(text, 'X', {
      duration: 4000,
      panelClass: isError ? ['error-snackbar'] : ['success-snackbar']
    });
  }
}
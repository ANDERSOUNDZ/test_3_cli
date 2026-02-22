import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Transaction } from '../models/transaction';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../environment/environment.ts';

@Injectable({
  providedIn: 'root',
})
export class TransactionService {
  private http = inject(HttpClient);
  private apiUrl = environment.transactionsUrl;

  private _transactions = signal<Transaction[]>([]);
  private _loading = signal<boolean>(false);

  get transactions() {
    return this._transactions.asReadonly();
  }

  get loading() {
    return this._loading.asReadonly();
  }

  getAll(filter?: any): void {
    this._loading.set(true);
    this.http
      .get<{ data: Transaction[] }>(`${this.apiUrl}/list_transactions`, { params: filter })
      .subscribe({
        next: (response) => {
          this._transactions.set(response.data);
        },
        error: () => this._loading.set(false),
        complete: () => this._loading.set(false),
      });
  }

  register(transaction: Partial<Transaction>): Observable<any> {
    this._loading.set(true);
    return this.http.post(`${this.apiUrl}/register_transaction`, transaction).pipe(
      tap(() => {
        this.getAll();
      }),
    );
  }
}

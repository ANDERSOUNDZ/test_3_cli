import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environment/environment.ts';
import { Category } from '../models/category.js';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private http = inject(HttpClient);
  private apiUrl = environment.productsUrl; 

  private _categories = signal<Category[]>([]);
  private _loading = signal<boolean>(false);

  get categories() {
    return this._categories.asReadonly();
  }

  get loading() {
    return this._loading.asReadonly();
  }

  getAll(): void {
    this._loading.set(true);
    this.http
      .get<{ data: Category[] }>(`${this.apiUrl}/list_categories`)
      .subscribe({
        next: (response) => {
          this._categories.set(response.data);
        },
        error: () => this._loading.set(false),
        complete: () => this._loading.set(false),
      });
  }

  create(category: Partial<Category>): Observable<any> {
    this._loading.set(true);
    return this.http.post(`${this.apiUrl}/create_category`, category).pipe(
      tap(() => {
        this.getAll();
      }),
    );
  }

  getById(id: number): Observable<{ data: Category }> {
    return this.http.get<{ data: Category }>(`${this.apiUrl}/get_category/${id}`);
  }
}

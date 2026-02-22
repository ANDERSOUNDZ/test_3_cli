import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environment/environment.ts';
import { Product } from '../models/product.js';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private http = inject(HttpClient);
  private apiUrl = environment.productsUrl;

  private _products = signal<Product[]>([]);
  private _loading = signal<boolean>(false);

  get products() {
    return this._products.asReadonly();
  }
  get loading() {
    return this._loading.asReadonly();
  }

  setProducts(val: Product[]) {
    this._products.set(val);
  }

  getAll(): void {
    this._loading.set(true);
    this.http.get<{ data: Product[] }>(`${this.apiUrl}/list_products`).subscribe({
      next: (response) => {
        this._products.set(response.data);
      },
      error: () => this._loading.set(false),
      complete: () => this._loading.set(false),
    });
  }

  getById(id: string): Observable<{ data: Product }> {
    return this.http.get<{ data: Product }>(`${this.apiUrl}/get_product/${id}`);
  }

  create(product: Partial<Product>): Observable<any> {
    return this.http.post(`${this.apiUrl}/create_product`, product).pipe(tap(() => this.getAll()));
  }

  update(id: string, product: Partial<Product>): Observable<any> {
    return this.http
      .put(`${this.apiUrl}/update_product/${id}`, product)
      .pipe(tap(() => this.getAll()));
  }

  delete(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/delete_product/${id}`).pipe(tap(() => this.getAll()));
  }
}

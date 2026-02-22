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
  products = signal<Product[]>([]);
  loading = signal<boolean>(false);

  getAll(): void {
    this.loading.set(true);
    this.http.get<{ data: Product[] }>(`${this.apiUrl}/list_products`).subscribe({
      next: (response) => this.products.set(response.data),
      complete: () => this.loading.set(false),
    });
  }

  create(product: Partial<Product>): Observable<any> {
    return this.http
      .post(`${this.apiUrl}/register_product`, product)
      .pipe(tap(() => this.getAll()));
  }
}

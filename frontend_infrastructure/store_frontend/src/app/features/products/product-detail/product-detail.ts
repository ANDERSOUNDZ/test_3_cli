import { CommonModule, CurrencyPipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ProductService } from '../../../core/services/product-service';
import { Product } from '../../../core/models/product';
import { MatSpinner } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-product-detail',
  imports: [CommonModule, CurrencyPipe, MatIconModule, RouterLink, MatSpinner],
  templateUrl: './product-detail.html',
  styleUrl: './product-detail.css',
})
export class ProductDetail {
  private route = inject(ActivatedRoute);
  private productService = inject(ProductService);

  product = signal<Product | null>(null);

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.productService.getById(id).subscribe({
        next: (response) => this.product.set(response.data),
        error: () => console.error('Producto no encontrado'),
      });
    }
  }

  handleImageError(event: Event) {
    const element = event.target as HTMLImageElement;
    element.onerror = null;
    element.src = 'img/placeholder-product.png';
  }
}

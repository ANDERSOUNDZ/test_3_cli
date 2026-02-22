import { CommonModule, CurrencyPipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ProductService } from '../../../core/services/product-service';
import { Product } from '../../../core/models/product';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CheckoutModal } from '../../../shared/components/checkout-modal/checkout-modal';
import { MatDialog } from '@angular/material/dialog';
import { CategoryNamePipe } from '../../../shared/pipes/category-name-pipe';
import { CategoryService } from '../../../core/services/category-service';

@Component({
  selector: 'app-product-detail',
  imports: [CommonModule, CurrencyPipe, MatIconModule, RouterLink, MatProgressSpinnerModule, CategoryNamePipe],
  templateUrl: './product-detail.html',
  styleUrl: './product-detail.css',
})
export class ProductDetail {
  product = signal<Product | null>(null);

  private route = inject(ActivatedRoute);
  private productService = inject(ProductService);
  private dialog = inject(MatDialog);
  private categoryService = inject(CategoryService);
  

  ngOnInit() {
    this.categoryService.getAll();
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

  openCheckout() {
    const currentProduct = this.product();
    if (currentProduct) {
      this.dialog.open(CheckoutModal, {
        data: currentProduct,
        panelClass: 'custom-bento-dialog',
      });
    }
  }
}

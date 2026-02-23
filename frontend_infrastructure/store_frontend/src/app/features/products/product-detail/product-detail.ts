import { CommonModule, CurrencyPipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ProductService } from '../../../core/services/product-service';
import { Product } from '../../../core/models/product';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CategoryNamePipe } from '../../../shared/pipes/category-name-pipe';
import { CategoryService } from '../../../core/services/category-service';
import { CartService } from '../../../core/services/cart-service';
import { Notification } from '../../../core/services/notification';

@Component({
  selector: 'app-product-detail',
  imports: [
    CommonModule,
    CurrencyPipe,
    MatIconModule,
    RouterLink,
    MatProgressSpinnerModule,
    CategoryNamePipe,
  ],
  templateUrl: './product-detail.html',
  styleUrl: './product-detail.css',
})
export class ProductDetail {
  product = signal<Product | null>(null);

  private route = inject(ActivatedRoute);
  private productService = inject(ProductService);
  private router = inject(Router);
  private cartService = inject(CartService);
  private notification = inject(Notification);
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

  addToCart() {
    const currentProduct = this.product();
    if (currentProduct) {
      try {
        this.cartService.addToCart(currentProduct);

        this.notification.notify('Producto añadido al carrito');

        this.router.navigate(['/checkout']);
      } catch (error: any) {
        this.notification.notify(error.message, true);
      }
    }
  }

  handleImageError(event: Event) {
    const element = event.target as HTMLImageElement;
    element.onerror = null;
    element.src = 'img/placeholder-product.png';
  }
}

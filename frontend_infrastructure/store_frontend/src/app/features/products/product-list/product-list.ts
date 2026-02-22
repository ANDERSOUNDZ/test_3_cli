import { CommonModule } from '@angular/common';
import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ProductCard } from '../product-card/product-card';
import { ProductService } from '../../../core/services/product-service';
import { MatDialog } from '@angular/material/dialog';
import { CheckoutModal } from '../../../shared/components/checkout-modal/checkout-modal';
import { Product } from '../../../core/models/product';

@Component({
  selector: 'app-product-list',
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    ProductCard,
  ],
  templateUrl: './product-list.html',
  styleUrl: './product-list.css',
})
export class ProductList implements OnInit {
  productService = inject(ProductService);
  searchQuery = signal('');

  // SOLUCIÓN 1: Permitir que el signal sea string O number
  selectedCategory = signal<string | number>('all');

  private dialog = inject(MatDialog);

  categories = computed(() => {
    const allCats = this.productService.products().map((p) => p.categoryId);
    return [...new Set(allCats)];
  });

  filteredProducts = computed(() => {
    let list = this.productService.products();
    const cat = this.selectedCategory();

    // SOLUCIÓN 2: Recuperar el valor del search query
    const query = this.searchQuery().toLowerCase();

    // Filtrado por categoría
    if (cat !== 'all') {
      list = list.filter((p) => p.categoryId === cat);
    }

    // Filtrado por búsqueda
    if (query) {
      list = list.filter((p) => p.name.toLowerCase().includes(query));
    }

    return list;
  });

  ngOnInit(): void {
    this.productService.getAll();
  }

  onSearch(event: Event) {
    const value = (event.target as HTMLInputElement).value;
    this.searchQuery.set(value);
  }

  openCheckoutFromList(product: Product) {
    this.dialog.open(CheckoutModal, {
      data: product,
      panelClass: 'custom-bento-dialog',
    });
  }
}

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
  selectedCategory = signal('all');
  private dialog = inject(MatDialog);
  categories = computed(() => {
    const allCats = this.productService.products().map((p) => p.category);
    return [...new Set(allCats)];
  });

  filteredProducts = computed(() => {
    let list = this.productService.products();
    const query = this.searchQuery().toLowerCase();
    const cat = this.selectedCategory();

    if (cat !== 'all') {
      list = list.filter((p) => p.category === cat);
    }

    if (query) {
      list = list.filter((p) => p.name.toLowerCase().includes(query));
    }

    return list;
  });

  ngOnInit(): void {
    this.productService.getAll();
  }

  onSearch(event: Event) {
    this.searchQuery.set((event.target as HTMLInputElement).value);
  }

  openCheckoutFromList(product: Product) {
    this.dialog.open(CheckoutModal, {
      data: product,
      panelClass: 'custom-bento-dialog',
    });
  }
}

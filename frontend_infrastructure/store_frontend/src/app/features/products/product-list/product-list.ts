import { CommonModule } from '@angular/common';
import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { RouterLink } from '@angular/router';
import { ProductCard } from '../product-card/product-card';
import { ProductService } from '../../../core/services/product-service';

@Component({
  selector: 'app-product-list',
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    ProductCard,
    RouterLink,
  ],
  templateUrl: './product-list.html',
  styleUrl: './product-list.css',
})
export class ProductList implements OnInit {
  productService = inject(ProductService);
  
  searchQuery = signal('');
  selectedCategory = signal('all');

  categories = computed(() => {
    const allCats = this.productService.products().map(p => p.category);
    return [...new Set(allCats)];
  });

  filteredProducts = computed(() => {
    let list = this.productService.products();
    const query = this.searchQuery().toLowerCase();
    const cat = this.selectedCategory();

    if (cat !== 'all') {
      list = list.filter(p => p.category === cat);
    }

    if (query) {
      list = list.filter(p => p.name.toLowerCase().includes(query));
    }

    return list;
  });

  ngOnInit(): void {
    this.productService.getAll();
  }

  onSearch(event: Event) {
    this.searchQuery.set((event.target as HTMLInputElement).value);
  }
}

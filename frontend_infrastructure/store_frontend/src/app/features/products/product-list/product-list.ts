import { CommonModule } from '@angular/common';
import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ProductCard } from '../product-card/product-card';
import { ProductService } from '../../../core/services/product-service';
import { MatDialog } from '@angular/material/dialog';
import { Product } from '../../../core/models/product';
import { CategoryNamePipe } from '../../../shared/pipes/category-name-pipe';
import { CategoryService } from '../../../core/services/category-service';

@Component({
  selector: 'app-product-list',
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    ProductCard,
    CategoryNamePipe
  ],
  templateUrl: './product-list.html',
  styleUrl: './product-list.css',
})
export class ProductList implements OnInit {
  productService = inject(ProductService);
  searchQuery = signal('');
  selectedCategory = signal<string | number>('all');

  private dialog = inject(MatDialog);
  private categoryService = inject(CategoryService);

  categories = computed(() => {
    const allCats = this.productService.products().map((p) => p.categoryId);
    return [...new Set(allCats)];
  });

  filteredProducts = computed(() => {
    let list = this.productService.products();
    const cat = this.selectedCategory();

    const query = this.searchQuery().toLowerCase();

    if (cat !== 'all') {
      list = list.filter((p) => p.categoryId === cat);
    }

    if (query) {
      list = list.filter((p) => p.name.toLowerCase().includes(query));
    }

    return list;
  });

  ngOnInit(): void {
    this.categoryService.getAll();
    this.productService.getAll();
  }

  onSearch(event: Event) {
    const value = (event.target as HTMLInputElement).value;
    this.searchQuery.set(value);
  }  
}

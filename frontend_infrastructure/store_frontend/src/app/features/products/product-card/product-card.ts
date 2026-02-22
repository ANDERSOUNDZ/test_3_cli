import { CommonModule, CurrencyPipe } from '@angular/common';
import { Component, inject, input, OnInit, output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { Product } from '../../../core/models/product';
import { CategoryNamePipe } from '../../../shared/pipes/category-name-pipe';
import { CategoryService } from '../../../core/services/category-service';

@Component({
  selector: 'app-product-card',
  imports: [CommonModule, RouterLink, MatIconModule, MatButtonModule, CurrencyPipe, RouterLink, CategoryNamePipe],
  templateUrl: './product-card.html',
  styleUrl: './product-card.css',
})
export class ProductCard implements OnInit {
  product = input.required<Product>();
  onBuy = output<Product>();
  private categoryService = inject(CategoryService);

  ngOnInit(): void {
    this.categoryService.getAll();
  }

  handleImageError(event: Event) {
    const element = event.target as HTMLImageElement;
    element.onerror = null;
    element.src = 'img/placeholder-product.png';
  }

  handleBuy() {
    this.onBuy.emit(this.product());
  }
}

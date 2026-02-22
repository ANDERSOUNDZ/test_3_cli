import { CommonModule, CurrencyPipe } from '@angular/common';
import { Component, input, output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { Product } from '../../../core/models/product';

@Component({
  selector: 'app-product-card',
  imports: [
    CommonModule,
    RouterLink,
    MatIconModule,
    MatButtonModule,
    CurrencyPipe,
    RouterLink
  ],
  templateUrl: './product-card.html',
  styleUrl: './product-card.css',
})
export class ProductCard {
  product = input.required<Product>();
  onBuy = output<Product>();

  handleImageError(event: Event) {
  const element = event.target as HTMLImageElement;
  element.onerror = null; 
  element.src = 'img/placeholder-product.png'; 
}

  handleBuy() {
    this.onBuy.emit(this.product());
  }
}

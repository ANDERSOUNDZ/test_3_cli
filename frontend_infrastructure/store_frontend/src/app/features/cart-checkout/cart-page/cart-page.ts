import { Component, inject } from '@angular/core';
import { CartService } from '../../../core/services/cart-service';
import { TransactionService } from '../../../core/services/transaction-service';
import { Router, RouterLink } from '@angular/router';
import { Transaction } from '../../../core/models/transaction';
import { Notification } from '../../../core/services/notification';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-cart-page',
  imports: [CommonModule, CurrencyPipe, MatIconModule, RouterLink],
  templateUrl: './cart-page.html',
  styleUrl: './cart-page.css',
})
export class CartPage {
  public cartService = inject(CartService);
  private transactionService = inject(TransactionService);
  private notification = inject(Notification);
  private router = inject(Router);

  confirmOrder() {
    const items = this.cartService.items();

    items.forEach((item) => {
      const request: Partial<Transaction> = {
        transactionType: 'Venta',
        productId: item.id,
        quantity: item.quantity,
        unitPrice: item.price,
        detail: `Compra Carrito: ${item.name}`,
      };

      this.transactionService.register(request).subscribe({
        next: () => {
          this.cartService.clear();
          this.notification.notify('¡Transacción completada con éxito!');
          this.router.navigate(['/products']);
        },
        error: () => this.notification.notify('Error al procesar la orden', true),
      });
    });
  }
}

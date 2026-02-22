import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table';
import { TransactionService } from '../../../core/services/transaction-service';
import { Transaction } from '../../../core/models/transaction';
import { Notification } from '../../../core/services/notification';

@Component({
  selector: 'app-checkout-modal',
  imports: [CommonModule, CurrencyPipe, MatTableModule, MatIconModule, MatProgressSpinnerModule],
  templateUrl: './checkout-modal.html',
  styleUrl: './checkout-modal.css',
})
export class CheckoutModal {
  private dialogRef = inject(MatDialogRef<CheckoutModal>);
  public product = inject(MAT_DIALOG_DATA);
  private transactionService = inject(TransactionService);
  private notification = inject(Notification);

  quantity = signal<number>(1);

  confirmPurchase() {
    const request: Partial<Transaction> = {
      transactionType: 'Venta',
      productId: this.product.id,
      quantity: this.quantity(),
      unitPrice: this.product.price,
      detail: `Compra online: ${this.product.name}`,
    };

    this.transactionService.register(request).subscribe({
      next: () => {
        this.notification.notify('¡Compra procesada y stock actualizado!');
        this.dialogRef.close(true);
      },
      error: (err) =>
        this.notification.notify(err.error?.message || 'Error en la transacción', true),
    });
  }
}

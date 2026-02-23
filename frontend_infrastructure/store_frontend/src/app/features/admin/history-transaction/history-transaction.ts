import { Component, inject } from '@angular/core';
import { TransactionService } from '../../../core/services/transaction-service';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIcon } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { TransactionDetail } from '../transaction-detail/transaction-detail';

@Component({
  selector: 'app-history-transaction',
  imports: [
    CommonModule,
    CurrencyPipe,
    MatProgressSpinnerModule,
    DatePipe,
    MatIcon,
    MatTableModule,
  ],
  templateUrl: './history-transaction.html',
  styleUrl: './history-transaction.css',
})
export class HistoryTransaction {
  private dialog = inject(MatDialog);
  transactionService = inject(TransactionService);

  ngOnInit() {
    this.transactionService.getAll();
  }

  loadTransactions() {
    this.transactionService.getAll();
  }

  viewDetail(id: string) {
    this.transactionService.getById(id).subscribe({
      next: (res) => {
        this.dialog.open(TransactionDetail, {
          data: res.data,
          width: '500px',
          panelClass: 'custom-modalbox'
        });
      }
    });
  }
}

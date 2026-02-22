import { Component, inject } from '@angular/core';
import { TransactionService } from '../../../core/services/transaction-service';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIcon } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';

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
  transactionService = inject(TransactionService);

  ngOnInit() {
    this.transactionService.getAll();
  }

  loadTransactions() {
    this.transactionService.getAll();
  }
}

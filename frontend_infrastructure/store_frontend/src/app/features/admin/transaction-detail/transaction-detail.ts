import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { Transaction } from '../../../core/models/transaction';
import jsPDF from 'jspdf';

@Component({
  selector: 'app-transaction-detail',
  imports: [CommonModule, MatDialogModule, MatIconModule, MatDividerModule, CurrencyPipe, DatePipe],
  templateUrl: './transaction-detail.html',
  styleUrl: './transaction-detail.css',
})
export class TransactionDetail {
  data = inject<Transaction>(MAT_DIALOG_DATA);

  downloadPDF() {
    const doc = new jsPDF();
    const t = this.data; //

    doc.setFontSize(18);
    doc.text('Comprobante de Movimiento', 20, 20);

    doc.setFontSize(12);
    doc.text(`ID Transacción: ${t.id}`, 20, 40);
    doc.text(`Fecha: ${new Date(t.date).toLocaleString()}`, 20, 50);
    doc.text(`Tipo: ${t.transactionType}`, 20, 60);

    doc.line(20, 65, 190, 65);

    doc.text(`Producto: ${t.productName}`, 20, 80);
    doc.text(`Cantidad: ${t.quantity}`, 20, 90);
    doc.text(`Precio Unitario: $${t.unitPrice}`, 20, 100);
    doc.text(`Total: $${t.totalPrice}`, 20, 110);

    doc.text('Detalle:', 20, 130);
    doc.text(`${t.detail || 'Sin detalles'}`, 20, 140);

    doc.save(`transaccion_${t.id.substring(0, 8)}.pdf`);
  }
}

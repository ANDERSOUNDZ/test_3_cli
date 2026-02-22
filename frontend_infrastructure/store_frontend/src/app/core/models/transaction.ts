export interface Transaction {
  id: string;
  date: Date;
  transactionType: 'Compra' | 'Venta';
  productId: string;
  productName?: string;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
  detail: string;
}

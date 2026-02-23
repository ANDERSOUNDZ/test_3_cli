import { computed, effect, Injectable, signal } from '@angular/core';
import { Product } from '../models/product';

export interface CartItem extends Product {
  quantity: number;
}

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private _items = signal<CartItem[]>([]);
  
  items = this._items.asReadonly();
  totalItems = computed(() => this._items().reduce((acc, item) => acc + item.quantity, 0));
  totalPrice = computed(() => this._items().reduce((acc, item) => acc + (item.price * item.quantity), 0));

  constructor() {
    const savedCart = localStorage.getItem('cart_storage');
    if (savedCart) this._items.set(JSON.parse(savedCart));

    effect(() => {
      localStorage.setItem('cart_storage', JSON.stringify(this._items()));
    });
  }

  addToCart(product: Product) {
    const currentItems = this._items();
    if (this.totalItems() >= 10) throw new Error('Máximo 10 productos permitidos');

    const index = currentItems.findIndex(i => i.id === product.id);
    if (index > -1) {
      const updated = [...currentItems];
      updated[index].quantity += 1;
      this._items.set(updated);
    } else {
      this._items.set([...currentItems, { ...product, quantity: 1 }]);
    }
  }

  removeItem(productId: string) {
    this._items.set(this._items().filter(i => i.id !== productId));
  }

  clear() {
    this._items.set([]);
    localStorage.removeItem('cart_storage');
  }
}

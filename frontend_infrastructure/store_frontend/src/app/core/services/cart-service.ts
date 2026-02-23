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
  totalPrice = computed(() =>
    this._items().reduce((acc, item) => acc + item.price * item.quantity, 0),
  );

  constructor() {
    const carritoGuardado = localStorage.getItem('cart_storage');
    if (carritoGuardado) {
      this._items.set(JSON.parse(carritoGuardado));
    }

    effect(() => {
      localStorage.setItem('cart_storage', JSON.stringify(this._items()));
    });
  }

  addToCart(product: Product) {
    if (this.totalItems() >= 10) {
      throw new Error('Máximo 10 productos permitidos');
    }

    const listaActual = this._items();
    const itemExistente = listaActual.find((i) => i.id === product.id);

    if (itemExistente) {
      itemExistente.quantity += 1;
      this._items.set(Array.from(listaActual));
    } else {
      const nuevoItem: CartItem = {
        id: product.id,
        name: product.name,
        price: product.price,
        quantity: 1,
        description: product.description,
        categoryId: product.categoryId,
        image: product.image,
        stock: product.stock,
      };

      this._items.set(listaActual.concat(nuevoItem));
    }
  }

  removeItem(productId: string) {
    const listaFiltrada = this._items().filter((i) => i.id !== productId);
    this._items.set(listaFiltrada);
  }

  clear() {
    this._items.set([]);
    localStorage.removeItem('cart_storage');
  }
}

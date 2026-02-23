import { Routes } from '@angular/router';
import { ProductList } from './features/products/product-list/product-list';
import { ProductForm } from './features/products/product-form/product-form';
import { ProductDetail } from './features/products/product-detail/product-detail';
import { ProductAdmin } from './features/products/product-admin/product-admin';
import { ProductEdit } from './features/products/product-edit/product-edit';
import { HistoryTransaction } from './features/admin/history-transaction/history-transaction';
import { CategoryAdmin } from './features/categories/category-admin/category-admin';
import { CategoryForm } from './features/categories/category-form/category-form';
import { CartPage } from './features/cart-checkout/cart-page/cart-page';

export const routes: Routes = [
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: 'products', component: ProductList },
  { path: 'products/new', component: ProductForm },
  { path: 'products/:id', component: ProductDetail },
  { path: 'admin', component: ProductAdmin },
  { path: 'admin/edit/:id', component: ProductEdit },
  { path: 'categories', component: CategoryAdmin }, 
  { path: 'categories/new', component: CategoryForm },
  { path: 'categories/edit/:id', component: CategoryForm },
  { path: 'transactions', component: HistoryTransaction },
  { path: 'checkout', component: CartPage },
];
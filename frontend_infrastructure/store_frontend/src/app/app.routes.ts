import { Routes } from '@angular/router';
import { ProductList } from './features/products/product-list/product-list';

export const routes: Routes = [
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: 'products', component: ProductList },
  //{ path: 'products/new', loadComponent: () => import('./features/products/product-form/product-form').then(m => m.ProductForm) },
  { path: 'transactions', loadComponent: () => import('./features/products/product-list/product-list').then(m => m.ProductList) },
];

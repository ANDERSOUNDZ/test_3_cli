import { Routes } from '@angular/router';
import { ProductList } from './features/products/product-list/product-list';
import { ProductForm } from './features/products/product-form/product-form';
import { ProductDetail } from './features/products/product-detail/product-detail';
import { ProductAdmin } from './features/products/product-admin/product-admin';
import { ProductEdit } from './features/products/product-edit/product-edit';

export const routes: Routes = [
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: 'products', component: ProductList },
  { path: 'products/new', component: ProductForm },
  { path: 'products/:id', component: ProductDetail },
  { path: 'admin', component: ProductAdmin },
  { path: 'admin/edit/:id', component: ProductEdit },
];

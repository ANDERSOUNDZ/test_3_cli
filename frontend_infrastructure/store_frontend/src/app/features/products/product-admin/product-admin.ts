import { CommonModule, CurrencyPipe } from '@angular/common';
import { Component, effect, inject, OnInit, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { RouterLink } from '@angular/router';
import { Product } from '../../../core/models/product';
import { ProductService } from '../../../core/services/product-service';
import { Notification } from '../../../core/services/notification';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { CategoryNamePipe } from '../../../shared/pipes/category-name-pipe';
import { CategoryService } from '../../../core/services/category-service';

@Component({
  selector: 'app-product-admin',
  imports: [
    CommonModule,
    MatPaginatorModule,
    MatTableModule,
    MatInputModule,
    MatFormFieldModule,
    MatIconModule,
    MatButtonModule,
    RouterLink,
    CurrencyPipe,
    CategoryNamePipe
  ],
  templateUrl: './product-admin.html',
  styleUrl: './product-admin.css',
})
export class ProductAdmin implements OnInit {
  private productService = inject(ProductService);
  private notification = inject(Notification);
  private categoryService = inject(CategoryService);
  
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  displayedColumns: string[] = ['image', 'name', 'category', 'price', 'stock', 'actions'];
  dataSource = new MatTableDataSource<Product>([]);

  constructor() {
    effect(() => {
      const products = this.productService.products();
      this.dataSource.data = products;
      if (this.paginator) {
        this.dataSource.paginator = this.paginator;
      }
    });
  }

  ngOnInit() {
    this.categoryService.getAll();
    this.productService.getAll();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  deleteProduct(id: string) {
    if (confirm('¿Estás seguro de que deseas eliminar este producto?')) {
      this.productService.delete(id).subscribe({
        next: () => {
          this.notification.notify('Producto eliminado correctamente');
        },
        error: () => this.notification.notify('Error al intentar eliminar', true),
      });
    }
  }
}

import { Component, effect, inject, ViewChild } from '@angular/core';
import { CategoryService } from '../../../core/services/category-service';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Category } from '../../../core/models/category';
import { Notification } from '../../../core/services/notification';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-category-admin',
  imports: [
    CommonModule,
    MatPaginatorModule,
    MatTableModule,
    MatInputModule,
    MatFormFieldModule,
    MatIconModule,
    MatButtonModule,
    MatTooltipModule,
    RouterLink,
  ],
  templateUrl: './category-admin.html',
  styleUrl: './category-admin.css',
})
export class CategoryAdmin {
  private categoryService = inject(CategoryService);
  private notification = inject(Notification);

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  displayedColumns: string[] = ['id', 'name', 'description', 'actions'];
  dataSource = new MatTableDataSource<Category>([]);

  constructor() {
    effect(() => {
      this.dataSource.data = this.categoryService.categories();
      if (this.paginator) {
        this.dataSource.paginator = this.paginator;
      }
    });
  }

  ngOnInit() {
    this.categoryService.getAll();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  deleteCategory(id: number) {
    if (confirm('¿Estás seguro? Esto podría afectar a los productos asociados.')) {
      this.notification.notify('Funcionalidad de borrado en desarrollo', true);
    }
  }
}

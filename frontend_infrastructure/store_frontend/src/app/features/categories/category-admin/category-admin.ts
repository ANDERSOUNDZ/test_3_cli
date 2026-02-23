import { Component, effect, inject, OnInit, ViewChild } from '@angular/core';
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
export class CategoryAdmin implements OnInit {
  private categoryService = inject(CategoryService);
  private notification = inject(Notification);

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  displayedColumns: string[] = ['id', 'name', 'description', 'actions'];
  dataSource = new MatTableDataSource<Category>([]);
  categories = this.categoryService.categories;
  isLoading = this.categoryService.loading;

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

  onDelete(id: number) {
    if (confirm('¿Estás seguro de eliminar esta categoría?')) {
      // Necesitas agregar el método delete en tu CategoryService
      this.categoryService.delete(id).subscribe({
        next: () => this.notification.notify('Categoría eliminada'),
        error: () => this.notification.notify('No se puede eliminar: tiene productos asociados', true)
      });
    }
  }
}

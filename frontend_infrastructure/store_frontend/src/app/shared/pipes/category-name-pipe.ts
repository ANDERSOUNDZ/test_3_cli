import { inject, Pipe, PipeTransform } from '@angular/core';
import { CategoryService } from '../../core/services/category-service';

@Pipe({
  name: 'categoryName',
  standalone: true
})
export class CategoryNamePipe implements PipeTransform {
  private categoryService = inject(CategoryService);

  transform(categoryId: number | undefined): string {
    if (!categoryId) return 'Sin categoría';

    const categories = this.categoryService.categories();
    const category = categories.find((category) => category.id === categoryId);

    return category ? category.name : `ID: ${categoryId}`;
  }
}
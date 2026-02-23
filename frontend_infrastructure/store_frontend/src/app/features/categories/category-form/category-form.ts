import { Component, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CategoryService } from '../../../core/services/category-service';
import { Notification } from '../../../core/services/notification';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-category-form',
  imports: [CommonModule, ReactiveFormsModule, MatIconModule, RouterLink],
  templateUrl: './category-form.html',
  styleUrl: './category-form.css',
})
export class CategoryForm {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private categoryService = inject(CategoryService);
  private notification = inject(Notification);

  categoryForm!: FormGroup;
  categoryId = signal<number | null>(null);
  isLoading = this.categoryService.loading;

  ngOnInit() {
    this.initForm();
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.categoryId.set(Number(id));
      this.loadCategory(Number(id));
    }
  }

  private initForm() {
    this.categoryForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(50)]],
      description: ['', [Validators.required, Validators.maxLength(200)]],
    });
  }

  loadCategory(id: number) {
    this.categoryService.getById(id).subscribe({
      next: (res) => this.categoryForm.patchValue(res.data),
      error: () => this.notification.notify('Error al cargar categoría', true),
    });
  }

  onSubmit() {
    if (this.categoryForm.invalid) return;

    const request = this.categoryId()
      ? this.categoryService.update(this.categoryId()!, this.categoryForm.value) // Necesitarás crear 'update' en el service
      : this.categoryService.create(this.categoryForm.value);

    request.subscribe({
      next: () => {
        this.notification.notify(
          `Categoría ${this.categoryId() ? 'actualizada' : 'creada'} con éxito`,
        );
        this.router.navigate(['/categories']);
      },
      error: () => this.notification.notify('Error en la operación', true),
    });
  }
}

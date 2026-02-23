import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../../core/services/product-service';
import { Router } from '@angular/router';
import { Notification } from '../../../core/services/notification';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIcon } from '@angular/material/icon';
import { Product } from '../../../core/models/product';
import { CategoryService } from '../../../core/services/category-service';

@Component({
  selector: 'app-product-form',
  imports: [CommonModule, ReactiveFormsModule, MatIcon],
  templateUrl: './product-form.html',
  styleUrl: './product-form.css',
})
export class ProductForm implements OnInit {
  private fb = inject(FormBuilder);
  private productService = inject(ProductService);
  private categoryService = inject(CategoryService);
  private notification = inject(Notification);
  private router = inject(Router);

  imagePreview: string | null = null;
  selectedFileName: string = '';
  productForm!: FormGroup;
  categories = this.categoryService.categories;
  isLoading = this.categoryService.loading;

  ngOnInit() {
    console.log("Holaaaa");
    
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required]],
      categoryId: [null, [Validators.required]],
      image: ['', [Validators.maxLength(500)]],
      price: [0.01, [Validators.required, Validators.min(0.01)]],
      stock: [1, [Validators.required, Validators.min(0)]],
    });    
    this.categoryService.getAll();
  }

  onSubmit() {
    if (this.productForm.valid) {
      const rawValue = this.productForm.value;

      const productData: Partial<Product> = {
        name: rawValue.name,
        description: rawValue.description,
        categoryId: Number(rawValue.categoryId), // Aseguramos que sea número
        price: rawValue.price,
        stock: rawValue.stock,
        image: 'img/' + (rawValue.image || '00.png'),
      };

      console.log('Enviando producto al servidor:', productData);

      this.productService.create(productData).subscribe({
        next: () => {
          this.notification.notify('¡Producto registrado con éxito!');
          this.router.navigate(['/products']);
        },
        error: (err) => {
          console.error('Error del servidor:', err);
          this.notification.notify('Error al guardar: Verifica que la categoría sea válida.', true);
          
          if (err.originalError?.error?.errors) {
            this.markServerErrors(err.originalError.error.errors);
          }
        },
      });
    } else {
      this.notification.notify('Por favor, completa el formulario correctamente.', true);
    }
  }

  onFileSelected(event: any) {
    const file: File = event.target.files[0];

    if (file) {
      const allowedTypes = ['image/jpeg', 'image/png', 'image/jpg'];
      if (!allowedTypes.includes(file.type)) {
        this.notification.notify('Solo se permiten imágenes JPG o PNG', true);
        return;
      }
      if (file.size > 2 * 1024 * 1024) {
        this.notification.notify('La imagen no debe superar los 2MB', true);
        return;
      }

      this.selectedFileName = file.name;
      this.productForm.patchValue({ image: file.name });

      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result as string;
      };
      reader.readAsDataURL(file);
    }
  }

  private markServerErrors(errors: any) {
    Object.keys(errors).forEach((key) => {
      const field = key.charAt(0).toLowerCase() + key.slice(1);
      const control = this.productForm.get(field);
      if (control) {
        control.setErrors({ serverError: errors[key][0] });
      }
    });
  }
}

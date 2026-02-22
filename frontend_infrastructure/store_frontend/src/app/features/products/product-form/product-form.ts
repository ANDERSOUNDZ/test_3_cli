import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../../core/services/product-service';
import { Router } from '@angular/router';
import { Notification } from '../../../core/services/notification';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIcon } from '@angular/material/icon';
import { Product } from '../../../core/models/product';

@Component({
  selector: 'app-product-form',
  imports: [CommonModule, ReactiveFormsModule, MatIcon],
  templateUrl: './product-form.html',
  styleUrl: './product-form.css',
})
export class ProductForm implements OnInit {
  private fb = inject(FormBuilder);
  private productService = inject(ProductService);
  private notification = inject(Notification);
  private router = inject(Router);
  imagePreview: string | null = null;
  selectedFileName: string = '';
  productForm!: FormGroup;

  ngOnInit() {
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required]],
      category: ['', [Validators.required, Validators.maxLength(50)]],
      image: ['', [Validators.maxLength(500)]],
      price: [0, [Validators.required, Validators.min(0.01)]],
      stock: [0, [Validators.required, Validators.min(0)]],
    });
  }

  onSubmit() {
    if (this.productForm.valid) {
      const productData: Partial<Product> = {
        name: this.productForm.value.name,
        description: this.productForm.value.description,
        category: this.productForm.value.category,
        price: this.productForm.value.price,
        stock: this.productForm.value.stock,
        image: 'img/' + this.productForm.value.image,
      };

      this.productService.create(productData).subscribe({
        next: () => {
          this.notification.notify('¡Producto registrado! Mueve la imagen a /public/img/');
          this.router.navigate(['/products']);
        },
        error: (err) => {
          if (err.messages && err.messages.length > 0) {
            this.notification.notify(err.messages[0], true);
            this.markServerErrors(err.originalError.error.errors);
          }
        },
      });
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

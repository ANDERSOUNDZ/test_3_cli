import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ProductService } from '../../../core/services/product-service';
import { Notification } from '../../../core/services/notification';

@Component({
  selector: 'app-product-edit',
  imports: [CommonModule, ReactiveFormsModule, MatIconModule, RouterLink],
  templateUrl: './product-edit.html',
  styleUrl: './product-edit.css',
})
export class ProductEdit implements OnInit {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private productService = inject(ProductService);
  private notification = inject(Notification);

  editForm!: FormGroup;
  productId = signal<string | null>(null);
  imagePreview = signal<string>('img/placeholder-product.png');

  ngOnInit() {
    this.initForm();
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.productId.set(id);
      this.loadProduct(id);
    }
  }

  private initForm() {
    this.editForm = this.fb.group({
      name: ['', [Validators.required]],
      description: ['', [Validators.required]],
      category: ['', [Validators.required]],
      image: [''],
      price: [0, [Validators.required, Validators.min(0.01)]],
      stock: [0, [Validators.required, Validators.min(0)]],
    });
  }

  loadProduct(id: string) {
    this.productService.getById(id).subscribe({
      next: (res) => {
        this.editForm.patchValue(res.data);
        this.imagePreview.set(res.data.image);
      },
      error: () => this.notification.notify('No se pudo cargar el producto', true),
    });
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.editForm.patchValue({ image: 'img/' + file.name });
      const reader = new FileReader();
      reader.onload = () => this.imagePreview.set(reader.result as string);
      reader.readAsDataURL(file);
    }
  }

  onSubmit() {
    if (this.editForm.valid && this.productId()) {
      this.productService.update(this.productId()!, this.editForm.value).subscribe({
        next: () => {
          this.notification.notify('Producto actualizado con éxito');
          this.router.navigate(['/admin']);
        },
        error: () => this.notification.notify('Error al actualizar', true),
      });
    }
  }
}

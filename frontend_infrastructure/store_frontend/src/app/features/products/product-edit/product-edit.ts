import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ProductService } from '../../../core/services/product-service';
import { Notification } from '../../../core/services/notification';
import { CategoryService } from '../../../core/services/category-service';
import { CategoryNamePipe } from '../../../shared/pipes/category-name-pipe';
import { TransactionService } from '../../../core/services/transaction-service';

@Component({
  selector: 'app-product-edit',
  imports: [CommonModule, ReactiveFormsModule, MatIconModule, RouterLink, CategoryNamePipe],
  templateUrl: './product-edit.html',
  styleUrl: './product-edit.css',
})
export class ProductEdit implements OnInit {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private productService = inject(ProductService);
  private notification = inject(Notification);
  private categoryService = inject(CategoryService);
  private transactionService = inject(TransactionService);

  editForm!: FormGroup;
  productId = signal<string | null>(null);
  imagePreview = signal<string>('img/00.png');
  isLoading = this.categoryService.loading;
  categories = this.categoryService.categories;
  
  // NUEVO: Para validar que no bajen el stock
  originalStock = signal<number>(0);

  ngOnInit() {
    this.categoryService.getAll();
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
      categoryId: [null, [Validators.required]],
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
        this.originalStock.set(res.data.stock); // Guardamos valor original
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
      const newStock = this.editForm.get('stock')?.value;
      const oldStock = this.originalStock();

      // VALIDACIÓN: No se puede retirar producto desde aquí
      if (newStock < oldStock) {
        this.notification.notify(`No puedes reducir el stock. El valor mínimo es ${oldStock}`, true);
        return;
      }

      this.productService.update(this.productId()!, this.editForm.value).subscribe({
        next: () => {
          // LÓGICA DE TRANSACCIÓN POR AUMENTO
          if (newStock > oldStock) {
            const diferencia = newStock - oldStock;
            this.transactionService.register({
              transactionType: 'Compra',
              productId: this.productId()!,
              quantity: diferencia,
              unitPrice: this.editForm.get('price')?.value,
              detail: `Ingreso manual de mercadería: +${diferencia} unidades`
            }).subscribe();
          }

          this.notification.notify('Producto actualizado y stock sincronizado');
          this.router.navigate(['/admin']);
        },
        error: () => this.notification.notify('Error al actualizar', true),
      });
    }
  }
}

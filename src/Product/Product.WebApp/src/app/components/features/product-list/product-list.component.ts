import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { ProductCardComponent } from './product-card/product-card.component';
import { Product } from '../../../models/product.model';
import { ProductService } from '../../../services/product.service';
import { CommonModule } from '@angular/common';
import { FloatingButtonComponent } from '../../shared/floating-button/floating-button.component';
import { FormsModule } from '@angular/forms';
import { ModalComponent } from '../../shared/modal/modal.component';
import { Subject, Subscription } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ModalComponent,
    ProductCardComponent,
    FloatingButtonComponent,
  ],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss',
})
export class ProductListComponent implements OnInit, OnDestroy {
  private subscriptions: Subscription = new Subscription();
  resetFormSubject: Subject<void> = new Subject<void>();
  products: Product[] = [];
  errorMessage: string | null = null;
  isModalOpen = false;
   
  constructor(private _productService: ProductService, private _toastService: ToastrService) {}

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    const productSubscription = this._productService.getProducts().subscribe({
      next: (data: Product[]) => {
        this.products = data;
      },
      error: () => {
        this._toastService.error('There was an error loading the products!');
      }
    });
    this.subscriptions.add(productSubscription); 
  }

  handleFormSubmit(formData: Product) {

    const addProductSubscription = this._productService.addProduct(formData).subscribe({
      next: () => {
        this.closeModal();
        this.loadProducts();  
        this._toastService.success('Product added successfully!');
      },
      error: (error) => {
        this._toastService.error(error.message);
      }
    });
    this.subscriptions.add(addProductSubscription); 
  }

  openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
    this.resetFormSubject.next();
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe(); 
  }
}

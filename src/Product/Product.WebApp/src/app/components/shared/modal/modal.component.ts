import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { Product } from '../../../models/product.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Subject, Subscription } from 'rxjs';

@Component({
  selector: 'app-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss'], 
})
export class ModalComponent implements OnInit, OnDestroy {
  private resetFormSubscription?: Subscription;
  @Output() closeModal = new EventEmitter<void>();
  @Output() submitForm = new EventEmitter<Product>();
  @Input() resetFormSubject: Subject<void> = new Subject<void>();
  @Input() isOpen: boolean = false;
  @Input() errorMessage: string | null = null;
  formData: Product = this.getInitialFormData(); 
  
  ngOnInit() {
    this.resetFormSubscription = this.resetFormSubject.subscribe(() => {
      this.resetFormData();
    });
  }

  onClose() {
    this.closeModal.emit();
  }

  onSubmit() {
    this.submitForm.emit(this.formData);
  }

  closeErrorMessage() {
    this.errorMessage = null; 
  }

  private getInitialFormData(): Product {
    return {
      name: '',
      code: '',
      price: 0,
    };
  }

  private resetFormData(): void {
    this.formData = this.getInitialFormData();
  }

  ngOnDestroy() {
    if (this.resetFormSubscription) {
      this.resetFormSubscription.unsubscribe();
    }
  }
}
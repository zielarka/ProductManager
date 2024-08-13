import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../models/product.model';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = 'http://localhost:8000'; 

  constructor(private http: HttpClient) {}

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/GetAllProducts`);
  }

  addProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(`${this.apiUrl}/CreateProduct`, product).pipe(
      catchError(this.handleErrorAddProduct)
    );
  }

  private handleErrorAddProduct(error: HttpErrorResponse) {
    if (error.status === 409) {
      return throwError(() => new Error('Product already exists'));
    }
    return throwError(() => new Error('Something went wrong, please try again later.'));
  }
}

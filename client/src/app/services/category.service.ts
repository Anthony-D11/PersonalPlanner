import { Injectable, Optional, inject } from '@angular/core';
import { NewCategory, Category } from '../models/interfaces';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable, throwError } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private http = inject(HttpClient);
  rootApiUrl = environment.apiUrl;
  categoryApiUrl = this.rootApiUrl + "/Category";

  getCategories(): Observable<any> {
    return this.http.get(this.categoryApiUrl);
  }
  addCategory(category: NewCategory): Observable<any> {
    return this.http.post(this.categoryApiUrl, category);
  }
  saveCategory(category: Category): Observable<any> {
    try {
      let categoryId = category.id;
      if(categoryId === null) {
        console.error("Null Category Id is not allowed");
      }
      return this.http.put(`${this.categoryApiUrl}/${categoryId}`, category);

    } catch(error) {
      return throwError(() => new Error('Category id not found'));
    }
  }
  deleteCategory(category: Category): Observable<any> {
    try {
      let categoryId = category.id;
      if(categoryId === null) {
        console.error("Null Todo Id is not allowed");
      }
      return this.http.delete(`${this.categoryApiUrl}/${categoryId}`);

    } catch(error) {
      return throwError(() => new Error('Category id not found'));
    }
  }
}

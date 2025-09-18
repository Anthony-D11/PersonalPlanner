import { Injectable, Optional, inject } from '@angular/core';
import { NewTodo, Todo } from '../models/interfaces';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable, throwError } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private http = inject(HttpClient);
  rootApiUrl = environment.apiUrl;

  getTodos(): Observable<any> {
    return this.http.get(`${this.rootApiUrl}/Todo`);
  }
  addTodo(todoItem: NewTodo): Observable<any> {
    return this.http.post(`${this.rootApiUrl}/Todo`, todoItem);
  }
  saveTodo(todoItem: Todo): Observable<any> {
    try {
      let todoItemId = todoItem.id;
      if(todoItemId === null) {
        console.error("Null Todo Id is not allowed");
      }
      return this.http.put(`${this.rootApiUrl}/Todo/${todoItemId}`, todoItem);

    } catch(error) {
      return throwError(() => new Error('Todo Item id not found'));
    }
  }
  deleteTodo(todoItem: Todo): Observable<any> {
    try {
      let todoItemId = todoItem.id;
      if(todoItemId === null) {
        console.error("Null Todo Id is not allowed");
      }
      return this.http.delete(`${this.rootApiUrl}/Todo/${todoItemId}`);

    } catch(error) {
      return throwError(() => new Error('Todo Item id not found'));
    }
  }
}

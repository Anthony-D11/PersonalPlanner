import { Injectable, Optional, inject } from '@angular/core';
import { NewTodoList, TodoList } from '../models/interfaces';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable, throwError } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class TodoListsService {
  private http = inject(HttpClient);
  rootApiUrl = environment.apiUrl;
  todoListApiUrl = this.rootApiUrl + "/TodoList";

  getTodoLists(): Observable<any> {
    return this.http.get(this.todoListApiUrl);
  }
  addTodoList(todoList: NewTodoList): Observable<any> {
    return this.http.post(this.todoListApiUrl, todoList);
  }
  saveTodoList(todoList: TodoList): Observable<any> {
    try {
      let todoListId = todoList.id;
      if(todoListId === null) {
        console.error("Null Todo List Id is not allowed");
      }
      return this.http.put(`${this.todoListApiUrl}/${todoListId}`, todoList);

    } catch(error) {
      return throwError(() => new Error('Todo Item id not found'));
    }
  }
  deleteTodoList(todoList: TodoList): Observable<any> {
    try {
      let todoListId = todoList.id;
      if(todoListId === null) {
        console.error("Null Todo Id is not allowed");
      }
      return this.http.delete(`${this.todoListApiUrl}/${todoListId}`);

    } catch(error) {
      return throwError(() => new Error('Todo Item id not found'));
    }
  }
}

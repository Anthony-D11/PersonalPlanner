import { Injectable, Optional, inject } from '@angular/core';
import { NewTodoTag, TodoTag } from '../models/interfaces';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable, throwError } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class TodoTagsService {
  private http = inject(HttpClient);
  rootApiUrl = environment.apiUrl;
  todoTagApiUrl = this.rootApiUrl + "/TodoTag";

  getTodoTags(): Observable<any> {
    return this.http.get(this.todoTagApiUrl);
  }
  addTodoTag(todoTag: NewTodoTag): Observable<any> {
    return this.http.post(this.todoTagApiUrl, todoTag);
  }
  saveTodoTag(todoTag: TodoTag): Observable<any> {
    try {
      let todoTagId = todoTag.id;
      if(todoTagId === null) {
        console.error("Null Todo Tag Id is not allowed");
      }
      return this.http.put(`${this.todoTagApiUrl}/${todoTagId}`, todoTag);

    } catch(error) {
      return throwError(() => new Error('Todo Item id not found'));
    }
  }
  deleteTodoTag(todoTag: TodoTag): Observable<any> {
    try {
      let todoTagId = todoTag.id;
      if(todoTagId === null) {
        console.error("Null Todo Id is not allowed");
      }
      return this.http.delete(`${this.todoTagApiUrl}/${todoTagId}`);

    } catch(error) {
      return throwError(() => new Error('Todo Item id not found'));
    }
  }
}

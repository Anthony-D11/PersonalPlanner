import { Injectable, Optional } from '@angular/core';
import { TodoType } from '../models/type';

@Injectable({
  providedIn: 'root'
})
export class TodosService {
  todoList: Array<TodoType> = [
    {
      id: 0,
      userId: 0,
      title: "Research content ideas",
      description: "Research content ideas",
      list: null,
      activeDate: new Date().toISOString().split('T')[0],
      dueDate: null,
      tag: null,
      subTodos: [],
      completed: false
    },
    {
      id: 1,
      userId: 0,
      title: "Renew driver's license",
      description: "Renew driver's license",
      list: null,
      activeDate: new Date().toISOString().split('T')[0],
      dueDate: new Date().toISOString().split('T')[0],
      tag: null,
      subTodos: [],
      completed: true
    },
  ]
}

import { Component, inject, OnInit, signal } from '@angular/core';
import { TodoItemComponent } from '../todo-item/todo-item';
import { TodosService } from '../../services/todos.service';
import { TodoType } from '../../models/type';

@Component({
  selector: 'app-today',
  imports: [TodoItemComponent],
  templateUrl: './today.html',
  styleUrl: './today.scss'
})
export class TodayComponent implements OnInit{
  todoService = inject(TodosService);
  todoList = signal<Array<TodoType>>([]);
  sideBar = signal<boolean>(false);
  selectedTodo = signal<TodoType | null>(null);
  ngOnInit(): void {
    this.todoList.set(this.todoService.todoList);
  }
  handleTodoItemClickEvent(event: MouseEvent, todoItem: TodoType) {
    event.preventDefault();
    this.selectedTodo.set(todoItem);
    this.sideBar.set(true);
  }
  handleCloseSideBar() {
    this.sideBar.set(false);
  }
  handleNewTodoItemEvent(event: MouseEvent) {
    event.preventDefault();
    this.selectedTodo.set(null);
    this.sideBar.set(true);
  }
}

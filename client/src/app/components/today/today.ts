import { Component, inject, OnInit, signal } from '@angular/core';
import { TodoItemComponent } from '../todo-item/todo-item';
import { TodoService } from '../../services/todo.service';
import { Todo } from '../../models/interfaces';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-today',
  imports: [TodoItemComponent, CommonModule, FormsModule],
  templateUrl: './today.html',
  styleUrl: './today.scss'
})
export class TodayComponent implements OnInit{
  todoService = inject(TodoService);
  todoList = signal<Array<Todo>>([]);
  sideBar = signal<boolean>(false);
  selectedTodo = signal<Todo | null>(null);
  ngOnInit(): void {
    this.refreshTodos();
  }
  refreshTodos() {
    this.todoService.getTodos().subscribe( response => {
      this.todoList.set(response);
    });
  }
  handleTodoItemClickEvent(event: MouseEvent, todoItem: Todo) {
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

  flipTodoCompletedValue(todo: Todo) {
    todo.completed = !todo.completed
    this.todoService.saveTodo(todo).subscribe(() => {
      this.refreshTodos()
    })
  }
}

import { Component, EventEmitter, input, Output, signal, inject } from '@angular/core';
import { NewTodo, Todo } from '../../models/interfaces';
import { TodosService } from '../../services/todos.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-todo-item',
  imports: [FormsModule],
  templateUrl: './todo-item.html',
  styleUrl: './todo-item.scss'
})
export class TodoItemComponent {
  todoService = inject(TodosService);
  currentTodoItem = input.required<Todo | null>();
  newTodoItem = signal<NewTodo>({
    userId: 1,
    title: "",
    description: "",
    list: null,
    activeDate: "",
    dueDate: "",
    tag: [],
    subTodos: [],
    completed: false
  });
  @Output() closeSideBar = new EventEmitter<boolean>();
  @Output() databaseChanged = new EventEmitter<boolean>();

  handleCloseSideBar() {
    this.closeSideBar.emit(true);
  }

  handleDeleteTaskEvent() {
    this.todoService.deleteTodo(this.currentTodoItem()!).subscribe(response => {
      this.closeSideBar.emit(true);
      this.databaseChanged.emit(true);
    })
  }
  handleSaveTaskEvent() {
    this.todoService.saveTodo(this.currentTodoItem()!).subscribe(response => {
      this.closeSideBar.emit(true);
      this.databaseChanged.emit(true);
    })
  }
  handleAddTaskEvent() {
    this.todoService.addTodo(this.newTodoItem()).subscribe(response => {
      this.closeSideBar.emit(true);
      this.databaseChanged.emit(true);
    });
  }

}

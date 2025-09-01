import { Component, EventEmitter, input, Output, signal } from '@angular/core';
import { TodoType } from '../../models/type';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-todo-item',
  imports: [FormsModule],
  templateUrl: './todo-item.html',
  styleUrl: './todo-item.scss'
})
export class TodoItemComponent {
  currentTodoItem = input.required<TodoType | null>();
  newTodoItem = signal<TodoType>({
    id: null,
    userId: null,
    title: null,
    description: null,
    list: null,
    activeDate: null,
    dueDate: null,
    tag: null,
    subTodos: [],
    completed: null
  });
  @Output() closeSideBar = new EventEmitter<boolean>();
  
  handleCloseSideBar() {
    this.closeSideBar.emit(true);
  }

  handleDeleteTaskEvent() {

  }
  handleSaveTaskEvent() {

  }
  handleAddTaskEvent() {
    console.log(this.newTodoItem())
  }

}

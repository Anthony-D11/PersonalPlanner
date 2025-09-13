import { Component, EventEmitter, input, Output, signal, inject, OnInit } from '@angular/core';
import { NewTodo, Todo, TodoList, TodoTag } from '../../models/interfaces';
import { TodosService } from '../../services/todos.service';
import { FormsModule } from '@angular/forms';
import { TodoListsService } from '../../services/todo-list.service';
import { TodoTagsService } from '../../services/todo-tag.service';

@Component({
  selector: 'app-todo-item',
  imports: [FormsModule],
  templateUrl: './todo-item.html',
  styleUrl: './todo-item.scss'
})
export class TodoItemComponent implements OnInit{
  todoService = inject(TodosService);
  todoListsService = inject(TodoListsService);
  todoTagsService = inject(TodoTagsService);
  currentTodoItem = input.required<Todo | null>();
  existingTodoLists = signal<Array<TodoList>>([]);
  existingTodoTags = signal<Array<TodoTag>>([]);

  newTodoItem = signal<NewTodo>({
    userId: 1,
    title: "",
    description: "",
    listId: null,
    activeDate: "",
    dueDate: "",
    tag: [],
    subTodos: [],
    completed: false
  });
  @Output() closeSideBar = new EventEmitter<boolean>();
  @Output() databaseChanged = new EventEmitter<boolean>();

  ngOnInit(): void {
    this.refreshTodoLists();
    this.refreshTodoTags();
  }

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
    console.log(this.newTodoItem())
    this.todoService.addTodo(this.newTodoItem()).subscribe(response => {
      this.closeSideBar.emit(true);
      this.databaseChanged.emit(true);
    });
  }

  refreshTodoLists() {
    this.todoListsService.getTodoLists().subscribe((response) => {
      this.existingTodoLists.set(response);
    });
  }
  refreshTodoTags() {
    this.todoTagsService.getTodoTags().subscribe((response) => {
      this.existingTodoTags.set(response);
    });
  }

}

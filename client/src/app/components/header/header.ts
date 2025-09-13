import { NewTodoList, NewTodoTag, TodoList, TodoTag } from '../../models/interfaces';
import { TodoListsService } from './../../services/todo-list.service';
import { Component, signal, NgModule, inject, model, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TodoTagsService } from '../../services/todo-tag.service';

@Component({
  selector: 'app-header',
  imports: [CommonModule, FormsModule],
  templateUrl: './header.html',
  styleUrl: './header.scss'
})
export class HeaderComponent implements OnInit{
  todoListsService = inject(TodoListsService);
  todoTagsService = inject(TodoTagsService);
  ngOnInit(): void {
    this.refreshTodoLists();
    this.refreshTodoTags();
  }

  colors = signal<Array<string>>(["#ff6b6b", "#d977f2", "#9775fa", "#5c7cfa", "#66d9e8", "#8ce99a", "#ffd43b", "#ff922b"])

  // List item variables & functions
  newTodoListName = model<string>("");
  existingTodoLists = signal<Array<TodoList>>([]);
  selectedTodoListColor = signal<string>("#ff6b6b");
  handleSelectTodoListColor(event: Event, color: string) {
    event.preventDefault();
    this.selectedTodoListColor.set(color);
  }
  newTodoListPanel = signal<boolean>(false);
  openNewTodoList(value: boolean) {
    this.newTodoListPanel.set(value);
  }
  saveNewTodoList() {
    let newTodoList: NewTodoList = {
      userId: 1,
      name: this.newTodoListName(),
      color: this.selectedTodoListColor(),
      numTodos: 0
    }
    this.todoListsService.addTodoList(newTodoList).subscribe((response) => {
      this.refreshTodoLists();
      this.openNewTodoList(false);
    })
  }
  refreshTodoLists() {
    this.todoListsService.getTodoLists().subscribe((response) => {
      this.existingTodoLists.set(response);
    });
  }

  // Tag item variables & functions
  newTodoTagName = model<string>("");
  existingTodoTags = signal<Array<TodoTag>>([]);
  selectedTodoTagColor = signal<string>("#ff6b6b");
  handleSelectTodoTagColor(event: Event, color: string) {
    event.preventDefault();
    this.selectedTodoTagColor.set(color);
  }
  newTodoTagPanel = signal<boolean>(false);
  openNewTodoTag(value: boolean) {
    this.newTodoTagPanel.set(value);
  }


  saveNewTodoTag() {
    let newTodoTag: NewTodoTag = {
      userId: 1,
      name: this.newTodoTagName(),
      color: this.selectedTodoTagColor(),
      numTodos: 0
    }
    this.todoTagsService.addTodoTag(newTodoTag).subscribe((response) => {
      this.refreshTodoTags();
      this.openNewTodoTag(false);
    })
  }
  refreshTodoTags() {
    this.todoTagsService.getTodoTags().subscribe((response) => {
      this.existingTodoTags.set(response);
    });
  }


}

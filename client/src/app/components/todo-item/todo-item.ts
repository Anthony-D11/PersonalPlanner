import { Component, EventEmitter, input, Output, signal, inject, OnInit, model, computed, InputSignal, Signal, WritableSignal } from '@angular/core';
import { NewTodo, Todo, Category, Tag } from '../../models/interfaces';
import { TodoService } from '../../services/todo.service';
import { FormsModule } from '@angular/forms';
import { CategoryService } from '../../services/category.service';
import { TagService } from '../../services/tag.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-todo-item',
  imports: [CommonModule, FormsModule],
  templateUrl: './todo-item.html',
  styleUrl: './todo-item.scss'
})
export class TodoItemComponent implements OnInit{
  todoService = inject(TodoService);
  categoryService = inject(CategoryService);
  tagService = inject(TagService);
  inputTodo = input.required<Todo | null>();
  currentTodoItem = signal<Todo | null>(null);
  existingCategories = signal<Array<Category>>([]);
  existingTags = signal<Array<Tag>>([]);
  selectedTagId = signal<number | null>(null);

  selectedTags = computed(() => {
    if (this.currentTodoItem() === null) {
      let tagIds = this.newTodoItem().tagIds;
      return this.existingTags().filter(tag => tagIds.some(tagId => tag.id === tagId));
    }
    else {
      let tagIds = this.currentTodoItem()!.tagIds;
      return this.existingTags().filter(tag => tagIds.some(tagId => tag.id === tagId));
    }
  });

  newTodoItem = signal<NewTodo>({
    title: "",
    description: "",
    activeDate: "",
    dueDate: "",
    tagIds: [],
    categoryId: null,
    subTodos: [],
    completed: false
  });
  @Output() closeSideBar = new EventEmitter<boolean>();
  @Output() databaseChanged = new EventEmitter<boolean>();

  ngOnInit(): void {
    this.refreshCategories();
    this.refreshTags();
    this.currentTodoItem.set(this.inputTodo());
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
    this.todoService.addTodo(this.newTodoItem()).subscribe(response => {
      this.closeSideBar.emit(true);
      this.databaseChanged.emit(true);
    });
  }

  refreshCategories() {
    this.categoryService.getCategories().subscribe((response) => {
      this.existingCategories.set(response);
    });
  }
  refreshTags() {
    this.tagService.getTags().subscribe((response) => {
      this.existingTags.set(response);
    });
  }

  updateSelectedTagId(value: number | number) {
    this.selectedTagId.set(value);
  }
  addSelectedTag(object: any) {
    if (this.selectedTagId() !== null && object() !== null && !object().tagIds.some((tagId: number) => tagId === this.selectedTagId())) {
      object.update((currentObject: any) => ({
        ...currentObject,
        tagIds: [...currentObject!.tagIds!, this.selectedTagId()!]
      }));
    }
  }
}

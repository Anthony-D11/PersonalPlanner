import { NewCategory, NewTag, Category, Tag } from '../../models/interfaces';
import { CategoryService } from '../../services/category.service';
import { Component, signal, NgModule, inject, model, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TagService } from '../../services/tag.service';

@Component({
  selector: 'app-header',
  imports: [CommonModule, FormsModule],
  templateUrl: './header.html',
  styleUrl: './header.scss'
})
export class HeaderComponent implements OnInit{
  todoListsService = inject(CategoryService);
  todoTagService = inject(TagService);
  ngOnInit(): void {
    this.refreshCategorys();
    this.refreshTags();
  }

  colors = signal<Array<string>>(["#ff6b6b", "#d977f2", "#9775fa", "#5c7cfa", "#66d9e8", "#8ce99a", "#ffd43b", "#ff922b"])

  // List item variables & functions
  newCategoryName = model<string>("");
  existingCategories = signal<Array<Category>>([]);
  selectedCategoryColor = signal<string>("#ff6b6b");
  handleSelectCategoryColor(event: Event, color: string) {
    event.preventDefault();
    this.selectedCategoryColor.set(color);
  }
  newCategoryPanel = signal<boolean>(false);
  openNewCategory(value: boolean) {
    this.newCategoryPanel.set(value);
  }
  saveNewCategory() {
    let newCategory: NewCategory = {
      name: this.newCategoryName(),
      color: this.selectedCategoryColor()
    }
    this.todoListsService.addCategory(newCategory).subscribe((response) => {
      this.refreshCategorys();
      this.openNewCategory(false);
    })
  }
  refreshCategorys() {
    this.todoListsService.getCategories().subscribe((response) => {
      this.existingCategories.set(response);
    });
  }

  // Tag item variables & functions
  newTagName = model<string>("");
  existingTags = signal<Array<Tag>>([]);
  selectedTagColor = signal<string>("#ff6b6b");
  handleSelectTagColor(event: Event, color: string) {
    event.preventDefault();
    this.selectedTagColor.set(color);
  }
  newTagPanel = signal<boolean>(false);
  openNewTag(value: boolean) {
    this.newTagPanel.set(value);
  }


  saveNewTag() {
    let newTag: NewTag = {
      name: this.newTagName(),
      color: this.selectedTagColor()
    }
    this.todoTagService.addTag(newTag).subscribe((response) => {
      this.refreshTags();
      this.openNewTag(false);
    })
  }
  refreshTags() {
    this.todoTagService.getTags().subscribe((response) => {
      this.existingTags.set(response);
    });
  }


}

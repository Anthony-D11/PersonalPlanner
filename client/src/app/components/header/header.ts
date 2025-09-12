import { Component, signal, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  imports: [CommonModule],
  templateUrl: './header.html',
  styleUrl: './header.scss'
})
export class HeaderComponent {
  colors = signal<Array<string>>(["#ff6b6b", "#da77f2", "#9775fa", "#5c7cfa", "#66d9e8", "#8ce99a", "#ffd43b", "#ff922b"])

  // List item variables & functions
  selectedListItemColor = signal<string>("#ff6b6b");
  handleSelectListItemColor(event: Event, color: string) {
    event.preventDefault();
    this.selectedListItemColor.set(color);
  }
  newListItem = signal<boolean>(false);
  openNewListItem(value: boolean) {
    this.newListItem.set(value);
  }
  saveNewListItem() {

  }

  // Tag item variables & functions
  selectedTagItemColor = signal<string>("#ff6b6b");
  handleSelectTagItemColor(event: Event, color: string) {
    event.preventDefault();
    this.selectedTagItemColor.set(color);
  }
  newTagItem = signal<boolean>(false);
  openNewTagItem(value: boolean) {
    this.newTagItem.set(value);
  }


  saveNewTagItem() {

  }


}

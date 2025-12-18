import { Component, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Shelf } from '../../models/shelf.model';

@Component({
  selector: 'app-shelf-list',
  imports: [CommonModule],
  templateUrl: './shelf-list.html',
  styleUrl: './shelf-list.css',
})
export class ShelfList {
  title = input<string>('');
  shelves = input<Shelf[]>([]);
  maxVisible = input<number>(25);
  showMoreLink = input<boolean>(true);

  formatCount(count: number): string {
    return count.toLocaleString();
  }
}

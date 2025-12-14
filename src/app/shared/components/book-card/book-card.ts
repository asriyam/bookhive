import { Component, input } from '@angular/core';
import { Book } from '../../models/book.model';

@Component({
  selector: 'app-book-card',
  imports: [],
  templateUrl: './book-card.html',
  styleUrl: './book-card.css',
})
export class BookCard {
  book = input.required<Book>();
  showImage = input<boolean>(true);
  showDescription = input<boolean>(false);
}


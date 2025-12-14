import { Component, input } from '@angular/core';
import { Book } from '../../../../shared/models/book.model';
import { BookCard } from '../../../../shared/components/book-card/book-card';

@Component({
  selector: 'app-book-shelf',
  imports: [BookCard],
  templateUrl: './book-shelf.html',
  styleUrl: './book-shelf.css',
})
export class BookShelf {
  books = input<Book[]>([], { alias: 'books' });
}

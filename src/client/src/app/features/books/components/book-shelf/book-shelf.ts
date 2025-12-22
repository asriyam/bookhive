import { Component, input, inject, computed } from '@angular/core';
import { BookCard } from '../../../../shared/components/book-card/book-card';
import { BookFacade } from '../../../../core/services/book.facade';

@Component({
  selector: 'app-book-shelf',
  imports: [BookCard],
  templateUrl: './book-shelf.html',
  styleUrl: './book-shelf.css',
})
export class BookShelf {
  private bookFacade = inject(BookFacade);

  shelfType = input.required<'currentlyReading' | 'recommendations'>();

  books = computed(() => {
    const type = this.shelfType();
    return type === 'currentlyReading'
      ? this.bookFacade.currentlyReading()
      : this.bookFacade.recommendations();
  });
}

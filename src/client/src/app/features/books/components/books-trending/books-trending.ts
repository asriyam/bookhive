import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Book } from '../../../../shared/models/book.model';
import { BookFacade } from '../../../../core/services/book.facade';
import { CarouselComponent } from '../../../../shared/components/carousel/carousel';

@Component({
  selector: 'app-books-trending',
  standalone: true,
  imports: [CommonModule, CarouselComponent],
  templateUrl: './books-trending.html',
  styleUrl: './books-trending.css',
})
export class BooksTrending {
  private bookFacade = inject(BookFacade);

  trendingBooks = signal<Book[]>([]);

  constructor() {
    this.loadTrendingBooks();
  }

  /**
   * Load trending books from facade
   */
  private loadTrendingBooks(): void {
    const books = this.bookFacade.getTrendingBooks();
    this.trendingBooks.set(books);
  }
}

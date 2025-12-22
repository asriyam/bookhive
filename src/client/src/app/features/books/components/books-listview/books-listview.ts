import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookFacade } from '../../../../core/services/book.facade';
import { UserBook } from '../../../../shared/models/user-book.model';

@Component({
  selector: 'app-books-listview',
  imports: [CommonModule],
  templateUrl: './books-listview.html',
  styleUrl: './books-listview.css',
})
export class BooksListview implements OnInit {
  private bookFacade = inject(BookFacade);

  books = signal<UserBook[]>([]);
  isLoading = signal(false);

  ngOnInit() {
    this.loadUserBooks();
  }

  loadUserBooks() {
    this.isLoading.set(true);
    // TODO: Get actual user ID from auth service when implemented
    const userId = 'mock-user-id';
    const userBooks = this.bookFacade.getBooksByUser(userId);
    this.books.set(userBooks);
    this.isLoading.set(false);
  }

  getRatingStars(rating: number): string {
    return '★'.repeat(rating) + '☆'.repeat(5 - rating);
  }

  formatDate(dateString: string | null): string {
    if (!dateString) return 'Not set';
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric'
    });
  }
}

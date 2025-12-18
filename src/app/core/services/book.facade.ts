import { Injectable, signal } from '@angular/core';
import { Book } from '../../shared/models/book.model';
import { UserBook, UserBooksData } from '../../shared/models/user-book.model';

import bookListData from '../../shared/data/books-list.json';
import userBooksData from '../../shared/data/user-books.json';

@Injectable({
  providedIn: 'root'
})
export class BookFacade {
  private allBooks = signal<Book[]>([]);
  currentlyReading = signal<Book[]>([]);
  recommendations = signal<Book[]>([]);
  userBooks = signal<UserBook[]>([]);

  private originalBooks: Book[] = bookListData.library_catalog;

  /**
   * Load and initialize all book data
   * Slices books into different categories
   */
  loadBooks(): void {
    const displayedBooks = [...this.originalBooks];
    this.allBooks.set(displayedBooks);
    this.currentlyReading.set(displayedBooks.slice(0, 3));
    this.recommendations.set(displayedBooks.slice(3, 5));
  }

  /**
   * Get all available books
   */
  getAllBooks(): Book[] {
    return this.allBooks();
  }

  /**
   * Get currently reading books
   */
  getCurrentlyReading(): Book[] {
    return this.currentlyReading();
  }

  /**
   * Get recommended books
   */
  getRecommendations(): Book[] {
    return this.recommendations();
  }

  /**
   * Future: Search books by title, author, ISBN
   */
  // searchBooks(query: string): Observable<Book[]> { ... }

  /**
   * Future: Get books by genre
   */
  // getBooksByGenre(genre: string): Observable<Book[]> { ... }

  /**
   * Future: Get book by ID
   */
  // getBookById(id: string): Observable<Book> { ... }

  /**
   * Get books for a specific user
   * @param userId - User identifier (currently unused as we have mock data for single user)
   * @returns Array of user books with ratings, shelves, and dates
   */
  getBooksByUser(userId: string): UserBook[] {
    const data = userBooksData as UserBooksData;
    this.userBooks.set(data.books);
    return data.books;
  }
}

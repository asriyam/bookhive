import { Component, input, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Book } from '../../models/book.model';

@Component({
  selector: 'app-carousel',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './carousel.html',
  styleUrl: './carousel.css',
})
export class CarouselComponent {
  books = input.required<Book[]>();
  currentIndex = signal(0);

  /**
   * Go to the next item in carousel
   */
  nextSlide(): void {
    const booksArray = this.books();
    if (booksArray.length > 0) {
      this.currentIndex.update(
        (index) => (index + 1) % booksArray.length
      );
    }
  }

  /**
   * Go to the previous item in carousel
   */
  previousSlide(): void {
    const booksArray = this.books();
    if (booksArray.length > 0) {
      this.currentIndex.update(
        (index) => (index - 1 + booksArray.length) % booksArray.length
      );
    }
  }

  /**
   * Get visible books (current and 4 adjacent books on the right for carousel effect)
   */
  getVisibleBooks(): Book[] {
    const booksArray = this.books();
    if (booksArray.length === 0) return [];

    const visible = [];
    for (let i = 0; i < 5; i++) {
      const index = (this.currentIndex() + i) % booksArray.length;
      visible.push(booksArray[index]);
    }
    return visible;
  }

  /**
   * Get offset percentage for carousel animation
   */
  getCarouselOffset(): string {
    return `${-this.currentIndex() * 20}%`;
  }

  /**
   * Check if previous button should be visible
   */
  isPreviousVisible(): boolean {
    return this.currentIndex() > 0;
  }

  /**
   * Check if next button should be visible
   */
  isNextVisible(): boolean {
    const booksArray = this.books();
    // Assuming 5 items visible (20% each = 100%)
    return this.currentIndex() < booksArray.length - 5;
  }
}

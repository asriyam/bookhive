import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SearchFacade } from '../../core/services/search.facade';
import { BookCard } from '../../shared/components/book-card/book-card';
import { Book } from '../../shared/models/book.model';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [CommonModule, BookCard],
  templateUrl: './search.html',
  styleUrl: './search.css',
})
export class Search implements OnInit {
  private searchFacade = inject(SearchFacade);

  searchQuery = signal('');
  searchResults = signal<Book[]>([]);
  isLoading = signal(false);

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const query = params['q'] || '';
      this.searchQuery.set(query);
      this.performSearch(query);
    });
  }

  performSearch(query: string) {
    if (!query.trim()) {
      this.searchResults.set([]);
      return;
    }

    this.isLoading.set(true);

    // Simulate API delay
    setTimeout(() => {
      const results = this.searchFacade.performSearch(query);
      this.searchResults.set(results);
      this.isLoading.set(false);
    }, 300);
  }
}

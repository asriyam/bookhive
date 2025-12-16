import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SearchFacade } from '../../core/services/search.facade';
import { BookCard } from '../../shared/components/book-card/book-card';
import { ShelfList } from '../../shared/components/shelf-list/shelf-list';
import { Book } from '../../shared/models/book.model';
import { Shelf } from '../../shared/models/shelf.model';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [CommonModule, BookCard, ShelfList],
  templateUrl: './search.html',
  styleUrl: './search.css',
})
export class Search implements OnInit {
  private searchFacade = inject(SearchFacade);

  searchQuery = signal('');
  searchResults = signal<Book[]>([]);
  isLoading = signal(false);
  relatedShelves = signal<Shelf[]>([]);

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
      this.relatedShelves.set([]);
      return;
    }

    this.isLoading.set(true);

    // Simulate API delay
    setTimeout(() => {
      this.searchFacade.performSearch(query);
      this.searchResults.set(this.searchFacade.searchResults());
      this.relatedShelves.set(this.searchFacade.relatedShelves());
      this.isLoading.set(false);
    }, 300);
  }
}

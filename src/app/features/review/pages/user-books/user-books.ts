import { Component, inject, signal, OnInit } from '@angular/core';
import { BooksListview } from '../../../books/components/books-listview/books-listview';
import { ActivatedRoute } from '@angular/router';

import { ShelfList } from '../../../../shared/components/shelf-list/shelf-list';
import { Footer } from '../../../../shared/components/footer/footer';
import { SearchFacade } from '../../../../core/services/search.facade';
import { Shelf } from '../../../../shared/models/shelf.model';

@Component({
  selector: 'app-user-books',
  imports: [BooksListview, ShelfList, Footer],
  templateUrl: './user-books.html',
  styleUrl: './user-books.css',
})
export class UserBooks implements OnInit {
  relatedShelves = signal<Shelf[]>([]);
  private searchFacade = inject(SearchFacade);

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.relatedShelves.set(this.searchFacade.relatedShelves());
  }
}

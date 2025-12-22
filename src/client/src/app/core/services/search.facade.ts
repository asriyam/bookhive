import { Injectable, signal } from "@angular/core";
import { BookFacade } from "./book.facade";
import { Shelf } from '../../shared/models/shelf.model';
import { Book } from "../../shared/models/book.model";

import searchShelvesData from '../../shared/data/search-shelves.json';

@Injectable({
    providedIn: 'root'
})
export class SearchFacade {
    searchResults = signal<Book[]>([]);
    relatedShelves = signal<Shelf[]>([]);

    constructor(private bookFacade: BookFacade) {
        this.relatedShelves.set(searchShelvesData.relatedShelves);
    }



    performSearch(query: string) {

        const lowerQuery = query.toLowerCase();
        const allBooks = this.bookFacade.getAllBooks();
        const results = allBooks.filter(book =>
            book.title.toLowerCase().includes(lowerQuery) ||
            book.author.toLowerCase().includes(lowerQuery) ||
            book.description.toLowerCase().includes(lowerQuery)
        );

        this.searchResults.set(results);
        this.relatedShelves.set(searchShelvesData.relatedShelves);

        return results;
    }
}

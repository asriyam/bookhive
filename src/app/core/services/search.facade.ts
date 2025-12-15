import { Book } from "../../shared/models/book.model";
import bookListData from '../../shared/data/books-list.json';
import { Injectable, signal } from "@angular/core";
import { BookFacade } from "./book.facade";

@Injectable({
    providedIn: 'root'
})
export class SearchFacade {
    searchResults = signal<Book[]>([]);

    constructor(private bookFacade: BookFacade) { }



    performSearch(query: string) {

        const lowerQuery = query.toLowerCase();
        const allBooks = this.bookFacade.getAllBooks();
        const results = allBooks.filter(book =>
            book.title.toLowerCase().includes(lowerQuery) ||
            book.author.toLowerCase().includes(lowerQuery) ||
            book.description.toLowerCase().includes(lowerQuery)
        );

        this.searchResults.set(results);
        return results;
    }
}

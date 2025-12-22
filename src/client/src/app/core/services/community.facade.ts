import { Injectable, signal, Signal } from "@angular/core";
import { Quote } from "../../shared/models/quote.model";

import quotesListData from '../../shared/data/quotes-list.json';
import { Book } from "../../shared/models/book.model";

@Injectable({
    providedIn: 'root'
})
export class CommunityFacade {
    private originalQuotes: Quote[] = quotesListData.quotes;

    allQuotes = signal<Quote[]>([]);

    constructor() {
        this.loadQuotes();
    }

    loadQuotes(): void {
        this.allQuotes.set(this.originalQuotes);
    }

    getAllQuotes(): Quote[] {
        return this.allQuotes();
    }
} 
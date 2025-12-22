import { Component, OnInit, inject } from '@angular/core';
import { CommunityFacade } from '../../../../core/services/community.facade';
import { QuotesList } from '../../../../shared/components/quotes-list/quotes-list';

@Component({
  selector: 'app-quotes',
  imports: [QuotesList],
  templateUrl: './quotes.html',
  styleUrl: './quotes.css',
})
export class Quotes implements OnInit {
  private contentFacade = inject(CommunityFacade);
  quotesData = this.contentFacade.allQuotes;

  ngOnInit(): void {
    this.contentFacade.loadQuotes();
  }
}

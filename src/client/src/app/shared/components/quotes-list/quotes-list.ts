import { Component, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Quote } from '../../models/quote.model';

@Component({
  selector: 'app-quotes-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './quotes-list.html',
  styleUrl: './quotes-list.css',
})
export class QuotesList {
  quotes = input.required<Quote[]>();
}

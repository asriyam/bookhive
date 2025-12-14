import { Component, input } from '@angular/core';
import { UserUpdate } from '../../models/userupdates.model';
import { BookCard } from '../book-card/book-card';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-feed-item',
  imports: [BookCard, CommonModule],
  templateUrl: './feed-item.html',
  styleUrl: './feed-item.css',
})
export class FeedItem {
  update = input.required<UserUpdate>();
  parseInt = parseInt;
}

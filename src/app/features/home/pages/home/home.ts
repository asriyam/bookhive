import { Component, OnInit, inject } from '@angular/core';
import { BookShelf } from '../../../books/components/book-shelf/book-shelf';
import { ActivityFeed } from '../../../activity/components/activity-feed/activity-feed';
import { BookFacade } from '../../../../core/services/book.facade';
import { ActivityFacade } from '../../../../core/services/activity.facade';

@Component({
  selector: 'app-home',
  imports: [BookShelf, ActivityFeed],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {
  private bookFacade = inject(BookFacade);
  private activityFacade = inject(ActivityFacade);

  ngOnInit(): void {
    this.bookFacade.loadBooks();
    this.activityFacade.loadUserUpdates();
  }
}

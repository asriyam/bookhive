import { Component, input } from '@angular/core';
import { UserUpdate } from '../../../../shared/models/userupdates.model';
import { FeedItem } from '../../../../shared/components/feed-item/feed-item';

@Component({
  selector: 'app-user-feed',
  imports: [FeedItem],
  templateUrl: './user-feed.html',
  styleUrl: './user-feed.css',
})
export class UserFeed {
  userUpdates = input<UserUpdate[]>([], { alias: 'updates' });
}

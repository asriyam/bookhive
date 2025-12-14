import { Component, inject } from '@angular/core';
import { FeedItem } from '../../../../shared/components/feed-item/feed-item';
import { ActivityFacade } from '../../../../core/services/activity.facade';

@Component({
  selector: 'app-activity-feed',
  imports: [FeedItem],
  templateUrl: './activity-feed.html',
  styleUrl: './activity-feed.css',
})
export class ActivityFeed {
  private activityFacade = inject(ActivityFacade);

  userUpdates = this.activityFacade.userUpdates;
}

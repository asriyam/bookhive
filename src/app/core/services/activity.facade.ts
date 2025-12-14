import { Injectable, signal } from '@angular/core';
import { UserUpdate } from '../../shared/models/userupdates.model';

import userUpdateData from '../../shared/data/userupdates.json';

@Injectable({
  providedIn: 'root'
})
export class ActivityFacade {
  userUpdates = signal<UserUpdate[]>([]);

  /**
   * Load all user activity/updates data
   */
  loadUserUpdates(): void {
    this.userUpdates.set(userUpdateData);
  }

  /**
   * Get user updates
   */
  getUserUpdates(): UserUpdate[] {
    return this.userUpdates();
  }

  /**
   * Future: Get updates by user
   */
  // getUserUpdates(userId: string): Observable<UserUpdate[]> { ... }

  /**
   * Future: Get updates by book
   */
  // getUpdatesByBook(bookId: string): Observable<UserUpdate[]> { ... }

  /**
   * Future: Post a new user update
   */
  // postUpdate(update: UserUpdate): Observable<UserUpdate> { ... }

  /**
   * Future: Delete a user update
   */
  // deleteUpdate(updateId: string): Observable<void> { ... }
}

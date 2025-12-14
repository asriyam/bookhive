import { Component, signal, OnInit } from '@angular/core';
import { BookShelf } from '../books/components/book-shelf/book-shelf';
import { UserFeed } from '../activity/components/user-feed/user-feed';


import { Book } from '../../shared/models/book.model';
import { UserUpdate } from '../../shared/models/userupdates.model';

import bookListData from '../../shared/data/books-list.json';
import userUpdateData from '../../shared/data/userupdates.json';

@Component({
  selector: 'app-home',
  imports: [BookShelf, UserFeed],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {
  private originalBooks: Book[] = bookListData.library_catalog;
  displayedBooks: Book[] = [];
  currentlyReadingShelfData: Book[] = [];
  recommendedBooks: Book[] = [];
  userUpdates: UserUpdate[] = userUpdateData;

  ngOnInit(): void {
    this.displayedBooks = [...this.originalBooks];
    this.currentlyReadingShelfData = [this.displayedBooks[0], this.displayedBooks[1], this.displayedBooks[2]];
    this.recommendedBooks = [this.displayedBooks[3], this.displayedBooks[4]];
  }
}

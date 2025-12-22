export interface UserBook {
  id: string;
  title: string;
  book_image: string;
  author: string;
  avgRating: number;
  userRating: number;
  shelves: string[];
  dateRead: string | null;
  dateAdded: string;
}

export interface UserBooksData {
  shelf: string;
  books: UserBook[];
  meta: {
    sortBy: string;
    sortOrder: string;
    totalBooks: number;
  };
}

import { Book } from "./book.model";
import { User } from "./user.model";

export interface UserUpdate {
    updateid: string;
    updatetype: string;
    timestamp: string;
    rating: string;
    update_content: string;
    book: Book;
    user: User;
}

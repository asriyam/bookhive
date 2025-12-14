

export interface Book {
    id: string;
    title: string;
    description: string;
    book_image: string;
    author: string;
    publisher: string;
    list_name: string;
    list_name_encoded: string;
}

export enum SortDirection {
    Asc,
    Desc,
    None
}

export interface SortState {
    column: keyof Book | '';
    direction: SortDirection;
}

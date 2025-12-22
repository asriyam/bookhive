
export interface Author {
    id: number;
    name: string;
    avatarUrl: string;
}

export interface Quote {
    id: number;
    text: string;
    author: Author;
    tags: string[];
    likes: number;
    createdAt: string;
}


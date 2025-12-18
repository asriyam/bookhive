import { Injectable, signal } from '@angular/core';
import newsData from '../../shared/data/news-list.json';

export interface Author {
  name: string;
  avatarUrl: string;
}

export interface FeaturedPost {
  id: number;
  title: string;
  slug: string;
  excerpt: string;
  imageUrl: string;
  publishedAt: string;
  likes: number;
  author: Author;
  category: string;
}

export interface SecondaryPost {
  id: number;
  title: string;
  slug: string;
  publishedAt: string;
  category: string;
  url: string;
}

export interface NewsData {
  featuredPost: FeaturedPost;
  secondaryPosts: SecondaryPost[];
  meta: {
    sectionTitle: string;
    totalPosts: number;
  };
}

@Injectable({
  providedIn: 'root',
})
export class ContentFacade {
  featuredPost = signal<FeaturedPost | null>(null);
  secondaryPosts = signal<SecondaryPost[]>([]);
  meta = signal<{ sectionTitle: string; totalPosts: number } | null>(null);

  constructor() {
    this.loadNewsData();
  }

  loadNewsData() {
    const data = newsData as NewsData;
    this.featuredPost.set(data.featuredPost);
    this.secondaryPosts.set(data.secondaryPosts);
    this.meta.set(data.meta);
  }

  getFeaturedPost() {
    return this.featuredPost();
  }

  getSecondaryPosts() {
    return this.secondaryPosts();
  }

  getMeta() {
    return this.meta();
  }
}

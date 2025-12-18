import { Component, input } from '@angular/core';
import { CommonModule } from '@angular/common';

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

@Component({
  selector: 'app-featured-post',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './featured-post.html',
  styleUrl: './featured-post.css',
})
export class FeaturedPostComponent {
  post = input.required<FeaturedPost>();
}

import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContentFacade } from '../../../../core/services/content.facade';
import { FeaturedPostComponent } from '../../../../shared/components/featured-post/featured-post';

@Component({
  selector: 'app-news-heroview',
  standalone: true,
  imports: [CommonModule, FeaturedPostComponent],
  templateUrl: './news-heroview.html',
  styleUrl: './news-heroview.css',
})
export class NewsHeroview implements OnInit {
  private contentFacade = inject(ContentFacade);

  featuredPost = this.contentFacade.featuredPost;
  secondaryPosts = this.contentFacade.secondaryPosts;
  meta = this.contentFacade.meta;

  ngOnInit() {
    // Data is already loaded by the facade
  }

  formatDate(dateString: string): string {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
    });
  }

  navigateToPost(url: string) {
    window.location.href = url;
  }
}

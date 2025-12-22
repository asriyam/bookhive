import { Component } from '@angular/core';
import { NewsHeroview } from '../../components/news-heroview/news-heroview';
import { Footer } from '../../../../shared/components/footer/footer';
import { BooksTrending } from '../../../books/components/books-trending/books-trending';

@Component({
  selector: 'app-content-explorer',
  standalone: true,
  imports: [NewsHeroview, BooksTrending, Footer],
  templateUrl: './content-explorer.html',
  styleUrl: './content-explorer.css',
})
export class ContentExplorer { }

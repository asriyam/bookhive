import { Component, signal } from '@angular/core';
import { Router } from '@angular/router';
import { NgIcon, provideIcons } from '@ng-icons/core';
import { heroUserCircle, heroBars3, heroMagnifyingGlass } from '@ng-icons/heroicons/outline';

import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-header',
  imports: [NgIcon, FormsModule],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {
  isMenuOpen = signal(false);
  searchQuery = signal('');

  constructor(private router: Router) { }

  toggleMenu() {
    this.isMenuOpen.update(value => !value);
  }

  closeMenu() {
    this.isMenuOpen.set(false);
  }

  onSearch() {
    const query = this.searchQuery().trim();
    if (query) {
      this.router.navigate(['/search'], { queryParams: { q: query } });
      this.searchQuery.set('');
    }
  }

  onSearchKeyPress(event: KeyboardEvent) {
    if (event.key === 'Enter') {
      this.onSearch();
    }
  }

  navigateToHome() {
    this.router.navigate(['/']);
  }
}

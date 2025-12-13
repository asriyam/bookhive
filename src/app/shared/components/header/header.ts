import { Component, signal } from '@angular/core';
import { Router } from '@angular/router';
import { NgIcon, provideIcons } from '@ng-icons/core';
import { heroUserCircle, heroBars3, heroMagnifyingGlass } from '@ng-icons/heroicons/outline';

import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-header',
  imports: [NgIcon, FormsModule],
  providers: [provideIcons({ heroUserCircle, heroBars3, heroMagnifyingGlass })],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {
  isMenuOpen = signal(false);
  searchQuery = signal('');
  browseSubmenuOpen = signal(false);
  communitySubmenuOpen = signal(false);

  constructor(private router: Router) { }

  toggleMenu() {
    this.isMenuOpen.update(value => !value);
  }

  closeMenu() {
    this.isMenuOpen.set(false);
  }

  toggleSubmenu(menu: 'browse' | 'community') {
    if (menu === 'browse') {
      this.browseSubmenuOpen.update(value => !value);
    } else if (menu === 'community') {
      this.communitySubmenuOpen.update(value => !value);
    }
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

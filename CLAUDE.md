# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

BookHive is a GoodReads-style social reading platform built with Angular 20 and Tailwind CSS. Currently in Phase 1 with a book catalog browsing interface. The project is structured as a standalone Angular application with plans for a .NET 8 backend in Phase 2.

## Development Commands

```bash
npm start              # Start dev server at http://localhost:4200
npm run build          # Production build (outputs to dist/)
npm test               # Run unit tests with Karma and Jasmine
npm run watch          # Watch mode - rebuilds on file changes
ng serve               # Angular CLI dev server (same as npm start)
ng test                # Run tests in watch mode
ng build --configuration production  # Production build with optimizations
```

**Running a single test:** Tests use Karma with Jasmine. Test files follow the pattern `*.spec.ts`. Run all tests and filter in the browser UI, or modify karma.conf.js to focus on specific test suites.

## Code Architecture

### Standalone Components (Angular 20 Zoneless)
This project uses **standalone components** exclusively (no NgModules). Angular 20's zoneless change detection is enabled via `provideBrowserGlobalErrorListeners()` and `provideZonelessChangeDetection()` in [app.config.ts](src/app/app.config.ts). This means:
- Components import their own dependencies via the `imports` array
- No modules needed; bootstrap directly with `bootstrapApplication()`
- Zoneless change detection improves performance by removing Zone.js overhead

### Directory Structure
- **src/app/** - Application root
  - **app.ts** - Root component bootstrapping the router outlet
  - **app.routes.ts** - Application routing (currently empty)
  - **app.config.ts** - Providers configuration (router, error handling, zoneless detection)
  - **shared/components/** - Reusable components (e.g., Header with search and navigation)
  - **features/** - Feature modules for books, authors, genres (structure to be expanded)
  - **core/** - Singleton services, guards, interceptors (planned)
  - **layout/** - Layout wrapper components (planned)
- **src/styles.css** - Global styles with Tailwind directives and custom theme variables
- **src/assets/** - Static assets and mock data
- **src/environments/** - Environment-specific configurations

### Styling Approach
- **Tailwind CSS 4 with PostCSS**: Uses `@import "tailwindcss"` in [src/styles.css](src/styles.css)
- **Custom theme variables**: Book-themed colors defined in `@theme` (cream backgrounds, masthead styling, text colors)
- **Reusable component classes**: Common book shelf and book item styles in `@layer components` (e.g., `.shelf-container`, `.book-item`, `.book-image`, `.book-title`)
- **Global element styles**: Heading hierarchy (h1-h6), links, typography in `@layer base`

### Component Communication
- Components use Angular signals for reactive state (e.g., `signal()`, `signal.update()`)
- Router integration for navigation and query parameters (see Header component handling search)
- Dependency injection via constructor (e.g., `Router` injected in Header)

## Code Style & Formatting

- **Prettier formatting** configured in [package.json](package.json):
  - Print width: 100 characters
  - Single quotes for JS/TS
  - Angular parser for HTML templates
- **TypeScript strict mode enabled**: `noImplicitAny`, `noImplicitReturns`, `noFallthlowCasesInSwitch`
- **Template strict mode**: `strictTemplates: true` in tsconfig

## Key Dependencies

- **Angular 20**: Core framework with zoneless change detection
- **Tailwind CSS 4**: Styling with PostCSS v8
- **RxJS 7.8**: Reactive programming (used by Angular services)
- **ng-icons** (via Header component): Icon library integration (Heroicons)

## Phase 1 Status

Currently implementing the book catalog browsing interface with:
- Book catalog display
- Search by title, author, ISBN
- Genre filtering
- Book detail pages
- Responsive design (Tailwind-based)

Future phases will add authentication, bookshelves, progress tracking, ratings/reviews, and social features (Phase 2+).

## Important Notes

- **No NgModules**: Everything is standalone. Import components, directives, and pipes directly where needed.
- **Zoneless Performance**: Avoid relying on Zone.js behavior; ensure change detection works with manual notification when needed.
- **Tailwind 4 Syntax**: Uses the new `@import "tailwindcss"` approach, not the legacy JIT compiler.
- **Git Configuration**: Repository uses conventional commits. Recent work focuses on initial project setup.

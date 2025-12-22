# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

BookHive is a GoodReads-style social reading platform built with Angular 21 and Tailwind CSS 4. Currently in Phase 1 with a fully functional book catalog browsing, search, user books, explore/featured content, and community quotes features. The project uses standalone components with zoneless change detection, Signal-based state management via facade services, and mock JSON data. Plans for a .NET 8 backend in Phase 2.

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

### Standalone Components (Angular 21 Zoneless)
This project uses **standalone components** exclusively (no NgModules). Angular 21's zoneless change detection is enabled via `provideBrowserGlobalErrorListeners()` and `provideZonelessChangeDetection()` in [app.config.ts](src/app/app.config.ts). This means:
- Components import their own dependencies via the `imports` array
- No modules needed; bootstrap directly with `bootstrapApplication()`
- Zoneless change detection improves performance by removing Zone.js overhead
- Vite builder configured in `angular.json` for optimal build performance

### Directory Structure
- **src/app/** - Application root
  - **app.ts** - Root component bootstrapping the router outlet
  - **app.routes.ts** - Application routing with lazy-loaded features
  - **app.config.ts** - Providers configuration (router, error handling, zoneless detection)
  - **core/services/** - Facade services for state management
    - `book.facade.ts` - Book catalog and user books management
    - `search.facade.ts` - Search functionality with query filtering
    - `activity.facade.ts` - User activity/updates feed
    - `community.facade.ts` - Community quotes data
    - `content.facade.ts` - Featured content and news
  - **features/** - Feature modules organized by business domain
    - `home/` - Home page with shelves and activity feed
    - `search/` - Search results page with query parameters
    - `review/` - User's personal books (My Books page)
    - `explorer/` - Featured content and trending books
    - `community/` - Community features (quotes)
    - `books/` - Shared book components (shelves, list views, trending)
    - `activity/` - Activity feed components
  - **shared/components/** - Reusable UI components
    - `header/` - Navigation, search, responsive menu
    - `book-card/` - Individual book display
    - `carousel/` - Horizontal scrolling book carousel
    - `featured-post/` - Featured article card
    - `feed-item/` - Activity feed item
    - `shelf-list/` - Shelf collection display
    - `quotes-list/` - Community quotes list
    - `footer/` - Footer component
  - **shared/models/** - TypeScript interfaces and data models
  - **shared/data/** - Mock JSON data files
  - **layout/** - Layout wrapper components
    - `main-layout/` - Used for home page
    - `list-layout/` - Used for search, review, explore, quotes pages
- **src/styles.css** - Global styles with Tailwind directives and custom theme variables
- **src/assets/** - Static assets and mock data
- **src/environments/** - Environment-specific configurations

### Styling Approach
- **Tailwind CSS 4 with PostCSS**: Uses `@import "tailwindcss"` in [src/styles.css](src/styles.css)
- **Custom theme variables**: Book-themed colors defined in `@theme` (cream backgrounds, masthead styling, text colors)
- **Reusable component classes**: Common book shelf and book item styles in `@layer components` (e.g., `.shelf-container`, `.book-item`, `.book-image`, `.book-title`)
- **Global element styles**: Heading hierarchy (h1-h6), links, typography in `@layer base`

### State Management with Signals
- **Pattern**: Angular Signals (no RxJS Observables or Redux)
- **Facade Services**: All data flow through facade services (book, search, activity, community, content)
- **Signals Usage**: `signal()`, `computed()`, `input()` for reactive state (zoneless compatible)
- **Data Sources**: Mock JSON files in `src/app/shared/data/`
- **Component Communication**:
  - Components use Router for navigation and query parameters
  - Facades exposed via dependency injection
  - Dependency injection via constructor (e.g., `Router` injected in Header, `BookFacade` in Home)

## Code Style & Formatting

- **Prettier formatting** configured in [package.json](package.json):
  - Print width: 100 characters
  - Single quotes for JS/TS
  - Angular parser for HTML templates
- **TypeScript strict mode enabled**: `noImplicitAny`, `noImplicitReturns`, `noFallthlowCasesInSwitch`
- **Template strict mode**: `strictTemplates: true` in tsconfig

## Key Dependencies

- **Angular 21.0.5**: Core framework with zoneless change detection and Vite builder
- **Tailwind CSS 4**: Styling with PostCSS v8 and new `@import` syntax
- **RxJS 7.8**: Reactive programming (available but primary state via Signals)
- **ng-icons**: Icon library integration (Heroicons for header and components)
- **TypeScript 5.6+**: Strict mode enabled for type safety

## Phase 1 Status - COMPLETE

BookHive Phase 1 is feature-complete with the following implemented:

### ✓ Implemented Features
- **Home Page**: Book shelves (Currently Reading, Recommendations) + activity feed with three-column layout
- **Search**: Full-text search by title, author, or description with query parameters (`/search?q=`)
- **My Books Page** (`/review`): User's personal book collection with ratings, shelves, and dates read
- **Explore Page** (`/explore`): Featured content section with featured posts and trending books carousel
- **Community/Quotes** (`/quotes`): Community quotes display and management
- **Header Navigation**: Full navigation with responsive design (desktop nav + mobile hamburger menu)
- **Book Components**:
  - Book cards with optional images and descriptions
  - Horizontal carousel for browsing (used on home and explore pages)
  - Book shelves with filtering (Currently Reading, Recommendations, etc.)
  - Book list view with detailed metadata
- **UI/UX**: Responsive Tailwind-based design, custom theme colors, reusable components, footer

### ✓ Architecture
- Standalone components with zoneless change detection
- Facade-based state management using Angular Signals
- Mock JSON data for books, users, activity, quotes, and news
- Lazy loading of feature routes
- Responsive layout wrappers (MainLayout for home, ListLayout for other pages)

### Upcoming - Phase 2+
- **.NET 8 Backend**: Replace mock JSON with REST API calls
- **User Authentication**: Login/registration and session management
- **Advanced Bookshelves**: Create, manage, and customize personal bookshelves
- **Progress Tracking**: Book reading progress with page markers
- **Ratings & Reviews**: User-generated book reviews and ratings
- **Social Features**: Follow users, messaging, recommendations
- **Data Persistence**: Server-side storage via .NET 8 API

## Important Notes

- **No NgModules**: Everything is standalone. Import components, directives, and pipes directly where needed.
- **Zoneless Performance**: Avoid relying on Zone.js behavior; ensure change detection works with manual notification when needed.
- **Tailwind 4 Syntax**: Uses the new `@import "tailwindcss"` approach, not the legacy JIT compiler.
- **Facade Pattern**: Always route data requests through facade services (BookFacade, SearchFacade, etc.), not directly to JSON files
- **Signals Over RxJS**: Use Angular Signals for state management within facades and components; avoid Observables unless necessary
- **Git Configuration**: Repository uses conventional commits. Current branch is `dev`, main branch for PRs is `main`
- **Routing**: All feature pages use lazy loading via routes. Query parameters used for search (`/search?q=`)
- **Responsive Design**: Mobile-first approach with Tailwind breakpoints; test on mobile, tablet, and desktop sizes

## Component Architecture Patterns

### Page Components (Standalone)
- Located in `features/[feature]/pages/`
- Handle routing parameters via `ActivatedRoute`
- Inject facades to load data
- Use `OnInit` to initialize data or `computed()` for dependent signals

### Feature Components
- Located in `features/[feature]/components/`
- Receive data via `@Input()` decorator
- Emit events via `@Output()` if needed
- Generally presentational (render data, handle user interaction)

### Shared Components
- Located in `shared/components/`
- Highly reusable across multiple features
- Take `@Input()` for data and configuration
- Emit `@Output()` for user interactions
- Examples: BookCard, Carousel, ShelfList, Header

### Facade Services
- Located in `core/services/`
- Encapsulate all data operations for a domain
- Use Signals for state (not private, to be accessible to components)
- Methods to load data and provide getters
- Single responsibility per facade (BookFacade for books, SearchFacade for search, etc.)

## Recent Changes (Last 5 Commits)

1. **e985406** - "Explore and my books pages updated": Updated content-explorer and user-books components
2. **bf4dbff** - "added shelves in search page": Enhanced search with ShelfList display
3. **64b17b9** - "added search page": Complete search feature implementation
4. **9f481b5** - "facade added": Introduced facade pattern services
5. **00139a1** - "refactored and added components": Major component restructuring

## Next Steps for Phase 2

When starting Phase 2 backend integration:
1. Create HTTP interceptor in `core/interceptors/` to add authentication headers
2. Create `.NET 8` API service layer to replace mock JSON imports
3. Update facades to call HTTP endpoints instead of importing JSON data
4. Add error handling and loading states to components
5. Implement authentication service and guards
6. Add offline caching strategy for better UX

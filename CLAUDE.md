# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

BookHive is a GoodReads-style social reading platform built as a **monorepo** with an Angular 21 frontend and .NET 10 backend. Phase 1 (frontend) is complete with full book catalog browsing, search, user books, explore/featured content, and community quotes features. The Angular client uses standalone components with zoneless change detection and Signal-based state management via facade services with mock JSON data. The .NET 10 API backend is in early Phase 2 development with domain entities, DTOs, mock data service, and initial REST API endpoints implemented.

## Development Commands

### Angular Client (src/client)
```bash
cd src/client
npm start              # Start dev server at http://localhost:4200
npm run build          # Production build (outputs to dist/)
npm test               # Run unit tests with Karma and Jasmine
npm run watch          # Watch mode - rebuilds on file changes
ng serve               # Angular CLI dev server (same as npm start)
ng test                # Run tests in watch mode
ng build --configuration production  # Production build with optimizations
```

### .NET 8 API (src/api)
```bash
cd src/api
dotnet build           # Compile all projects (Api, Core, Infrastructure)
dotnet run             # Run API dev server (HTTP: 5076, HTTPS: 7272)
dotnet test            # Run unit tests (when configured)
```

**Running a single test (Angular):** Tests use Karma with Jasmine. Test files follow the pattern `*.spec.ts`. Run all tests and filter in the browser UI, or modify karma.conf.js to focus on specific test suites.

## Code Architecture

### Standalone Components (Angular 21 Zoneless)
This project uses **standalone components** exclusively (no NgModules). Angular 21's zoneless change detection is enabled via `provideBrowserGlobalErrorListeners()` and `provideZonelessChangeDetection()` in [app.config.ts](src/app/app.config.ts). This means:
- Components import their own dependencies via the `imports` array
- No modules needed; bootstrap directly with `bootstrapApplication()`
- Zoneless change detection improves performance by removing Zone.js overhead
- Vite builder configured in `angular.json` for optimal build performance

### Directory Structure

#### Root Level (Monorepo)
```
bookhive/
├── src/
│   ├── client/                 # Angular 21 Frontend (Phase 1 - Complete)
│   └── api/                    # .NET 8 Backend (Phase 2 - Scaffolding)
├── .github/workflows/          # CI/CD workflows (GitHub Actions)
├── docs/                       # Documentation
├── CLAUDE.md                   # This file - development guide
├── README.md                   # Project overview
└── .gitignore                  # Git ignore rules
```

#### Angular Client Structure (src/client/)
- **angular.json** - CLI configuration with Vite builder
- **package.json** - npm dependencies (26 packages)
- **tsconfig.json** - TypeScript strict mode config
- **tsconfig.app.json** - App-specific TypeScript config
- **tsconfig.spec.json** - Test TypeScript config
- **.postcssrc.json** - PostCSS config for Tailwind CSS 4
- **.editorconfig** - Editor formatting rules
- **src/**
  - **app/** - Application root
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
    - **layout/** - Layout wrapper components
      - `main-layout/` - Used for home page
      - `list-layout/` - Used for search, review, explore, quotes pages
    - **shared/components/** - Reusable UI components
      - `header/` - Navigation, search, responsive menu
      - `book-card/` - Individual book display
      - `carousel/` - Horizontal scrolling book carousel
      - `featured-post/` - Featured article card
      - `feed-item/` - Activity feed item
      - `shelf-list/` - Shelf collection display
      - `quotes-list/` - Community quotes list
      - `footer/` - Footer component
    - **shared/models/** - TypeScript interfaces and data models (6 files)
    - **shared/data/** - Mock JSON data files (7 files)
  - **styles.css** - Global styles with Tailwind directives and custom theme variables
  - **index.html** - HTML entry point
  - **main.ts** - Application bootstrap
- **public/** - Static assets (images, favicons)
- **dist/** - Production build output (Vite-compiled)
- **node_modules/** - npm dependencies (586 packages)

#### .NET 10 API Structure (src/api/)
- **BookHive.Api.slnx** - Solution file (.NET 10 Target Framework, C# 14)
- **BookHive.Api/** - Main Web API Project
  - **Program.cs** - ASP.NET Core application setup with CORS and DI configuration
  - **Controllers/** - REST API endpoints
    - `BooksController.cs` - GET /api/books, GET /api/books/{id}, GET /api/books/search
    - `UsersController.cs` - GET /api/users/{id}, GET /api/users, GET /api/users/{userId}/activities
    - `ShelvesController.cs` - GET /api/shelves, GET /api/shelves/user/{userId}
  - **Properties/launchSettings.json** - Development server ports (HTTP: 5076, HTTPS: 7272)
  - **appsettings.json** - Configuration
  - **appsettings.Development.json** - Development settings
  - **BookHive.Api.csproj** - Project file
  - **bin/** - Build output
  - **obj/** - Intermediate files
- **BookHive.Core/** - Business Logic Layer (Class Library)
  - **BookHive.Core.csproj** - Project file (.NET 10, C# 14)
  - **Entities/** - Domain models
    - `Book.cs` - Book entity with catalog information
    - `User.cs` - User entity with profile details
    - `Shelf.cs` - Shelf (category) entity
    - `Activity.cs` - User activity/updates entity
  - **DTOs/** - Data Transfer Objects for API contracts
    - `BookDto.cs` - Book transfer object
    - `UserDto.cs` - User transfer object
    - `ShelfDto.cs` - Shelf transfer object
    - `ActivityDto.cs` - Activity transfer object
- **BookHive.Infrastructure/** - Data Access Layer (Class Library)
  - **BookHive.Infrastructure.csproj** - Project file (.NET 10, C# 14)
  - **Services/** - Business services
    - `MockDataService.cs` - Mock data provider (mirrors Angular JSON data)
  - References: BookHive.Core
  - Planned: EF Core DbContext, repositories, database configuration
- **.vs/** - Visual Studio cache files

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

### Angular Client
- **Angular 21.0.5**: Core framework with zoneless change detection and Vite builder
- **Tailwind CSS 4.1.18**: Styling with PostCSS v8 and new `@import` syntax
- **RxJS 7.8**: Reactive programming (available but primary state via Signals)
- **ng-icons 33.0.0**: Icon library integration (Heroicons for header and components)
- **TypeScript 5.9.2**: Strict mode enabled for type safety
- **Karma + Jasmine**: Unit testing framework

### .NET 10 API
- **.NET 10.0** (Target Framework)
- **C# 14**: Latest language features (implicit usings, nullable reference types, default parameter values)
- **ASP.NET Core 10.0**: Web framework with built-in dependency injection and routing
- **Microsoft.AspNetCore.OpenApi 10.0.0**: OpenAPI/Swagger support
- **Entity Framework Core**: (planned for Phase 2 - database configuration and migrations)
- **CORS**: Configured to allow Angular client on localhost:4200

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

### Phase 2 Status - IN PROGRESS

Phase 2 backend development has begun with initial API implementation:

#### ✓ Completed
- **Domain Entities**: Book, User, Shelf, Activity entities in BookHive.Core
- **DTOs**: BookDto, UserDto, ShelfDto, ActivityDto for API contracts
- **Mock Data Service**: MockDataService in BookHive.Infrastructure mirrors Angular JSON data
- **REST API Controllers**:
  - BooksController: GET /api/books, GET /api/books/{id}, GET /api/books/search
  - UsersController: GET /api/users/{id}, GET /api/users, GET /api/users/{userId}/activities
  - ShelvesController: GET /api/shelves, GET /api/shelves/user/{userId}
- **CORS Configuration**: Enabled for Angular client (localhost:4200)
- **Dependency Injection**: MockDataService registered in Program.cs

#### Upcoming - Phase 2 Continued

#### Backend Implementation
- **Database Layer**: Set up Entity Framework Core with Azure SQL/Cosmos DB in BookHive.Infrastructure
- **Additional Entities**: Review, Rating, UserBook entities
- **Additional Endpoints**: POST/PUT/DELETE operations for CRUD functionality
- **Authentication**: JWT/OAuth integration with Identity Server
- **Data Seeding**: Migrate mock JSON data to database
- **Error Handling**: Global exception middleware, validation filters
- **Logging**: Structured logging with Serilog

#### Frontend Integration
- **HTTP Client Service**: Create HTTP service layer in Angular to replace mock JSON
- **API Integration**: Update facades to call REST API endpoints
- **Error Handling**: Add loading states, error messages, retry logic
- **Authentication**: Implement login/registration pages, auth guards, auth interceptor
- **Token Management**: JWT storage, refresh token handling

#### Features
- **Advanced Bookshelves**: Create, manage, and customize personal bookshelves
- **Progress Tracking**: Book reading progress with page markers
- **Ratings & Reviews**: User-generated book reviews and ratings
- **Social Features**: Follow users, messaging, recommendations
- **Admin Panel**: Book management, user moderation, analytics

#### Deployment
- **CI/CD**: GitHub Actions workflows for build and test
- **Azure Static Web Apps**: Host Angular frontend
- **Azure App Service**: Host .NET API
- **Database**: Azure SQL Server or Cosmos DB

## Important Notes

### Angular Client
- **No NgModules**: Everything is standalone. Import components, directives, and pipes directly where needed.
- **Zoneless Performance**: Avoid relying on Zone.js behavior; ensure change detection works with manual notification when needed.
- **Tailwind 4 Syntax**: Uses the new `@import "tailwindcss"` approach, not the legacy JIT compiler.
- **Facade Pattern**: Always route data requests through facade services (BookFacade, SearchFacade, etc.), not directly to JSON files
- **Signals Over RxJS**: Use Angular Signals for state management within facades and components; avoid Observables unless necessary
- **Routing**: All feature pages use lazy loading via routes. Query parameters used for search (`/search?q=`)
- **Responsive Design**: Mobile-first approach with Tailwind breakpoints; test on mobile, tablet, and desktop sizes
- **Build Location**: Production build outputs to `src/client/dist/bookhive/` with Vite hash-based asset names

### .NET 10 API
- **Layered Architecture**:
  - `BookHive.Api` - Controllers, routing, and dependency injection setup
  - `BookHive.Core` - Domain entities (Book, User, Shelf, Activity) and DTOs
  - `BookHive.Infrastructure` - MockDataService and planned data access patterns
- **Development Ports**: HTTP on 5076, HTTPS on 7272
- **CORS**: Configured to accept requests from Angular client (http://localhost:4200, https://localhost:4200)
- **Dependency Injection**: Built-in ASP.NET Core DI using constructor injection with C# 14 primary constructors
- **Target Framework**: .NET 10.0 with C# 14 features (implicit usings, nullable reference types, primary constructors)
- **Entity Framework**: Planned for next phase; currently using MockDataService for development data

### Repository Configuration
- **Structure**: Monorepo with separate client and API projects under `src/`
- **Git**: Uses conventional commits. Current branch is `dev`, main branch for PRs is `main`
- **node_modules**: Properly ignored in `.gitignore` at `src/client/node_modules/`
- **CI/CD**: `.github/workflows/` directory exists for GitHub Actions (not yet configured)
- **Documentation**: See `.gitignore` for ignored patterns, `README.md` for project overview

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

## API Endpoints Reference

### Books API
- **GET /api/books** - Retrieve all books
- **GET /api/books/{id}** - Get a specific book by ISBN
- **GET /api/books/search?query={term}** - Search books by title, author, or description

### Users API
- **GET /api/users** - Get all users (admin use)
- **GET /api/users/{id}** - Get a specific user by ID
- **GET /api/users/{userId}/activities** - Get user's activity history

### Shelves API
- **GET /api/shelves** - Get all available shelves
- **GET /api/shelves/user/{userId}** - Get shelves for a specific user

### Planned Endpoints (Phase 2)
- **POST /api/books** - Create new book (admin)
- **PUT /api/books/{id}** - Update book (admin)
- **DELETE /api/books/{id}** - Delete book (admin)
- **POST /api/users/{id}/ratings** - Rate a book
- **POST /api/users/{id}/reviews** - Write a review
- **POST /api/auth/login** - User login
- **POST /api/auth/register** - User registration
- **POST /api/users/{id}/shelves** - Create custom shelf
- **GET /api/users/{id}/books** - Get user's books with ratings/reviews

## Phase 2 Implementation Roadmap

### Step 1: Backend Foundation (COMPLETED)
✓ **Domain Models in BookHive.Core**
   - ✓ Book, User, Shelf, Activity entities
   - ✓ DTOs for API contracts (BookDto, UserDto, ShelfDto, ActivityDto)
   - Planned: Review, Rating, UserBook entities

✓ **Mock Data Service in BookHive.Infrastructure**
   - ✓ MockDataService with in-memory data
   - ✓ Methods: GetAllBooks(), GetBookById(), SearchBooks(), etc.

✓ **Initial API Controllers in BookHive.Api**
   - ✓ BooksController with GET endpoints
   - ✓ UsersController with GET endpoints and activities
   - ✓ ShelvesController with GET endpoints
   - ✓ CORS configuration for Angular client

### Step 1b: Database Setup (NEXT)
1. **EF Core Configuration**
   - Create DbContext in BookHive.Infrastructure
   - Configure entity relationships and constraints
   - Add migrations infrastructure

2. **Database Seeding**
   - Migrate mock JSON data to initial database
   - Create seed data script

3. **Repository Pattern**
   - Implement repositories for each entity
   - Create generic base repository

### Step 2: Authentication & Authorization (Weeks 2-3)
1. **Identity Server / JWT Setup**
   - User registration and login endpoints
   - JWT token generation and validation
   - Refresh token mechanism

2. **Authentication in Angular**
   - Login/registration pages
   - Auth guard for protected routes
   - Auth interceptor for adding JWT to requests
   - Session/token storage

### Step 3: Frontend Integration (Weeks 3-4)
1. **HTTP Service Layer**
   - Create `src/client/src/app/core/services/api/` directory
   - Implement: BooksHttpService, UsersHttpService, ReviewsHttpService, etc.
   - Error handling and retry logic

2. **Facade Updates**
   - Update facades to call HTTP services instead of importing JSON
   - Add loading and error signals
   - Implement caching where appropriate

3. **Component Updates**
   - Add loading states (spinners, skeletons)
   - Display error messages
   - Handle offline scenarios

### Step 4: Advanced Features (Weeks 4+)
1. **Bookshelves**: Implement CRUD operations
2. **Reviews & Ratings**: Create, read, update, delete reviews
3. **Progress Tracking**: Page/chapter tracking
4. **Social Features**: Following, recommendations
5. **Admin Features**: Book/user management, moderation

### Step 5: DevOps & Deployment (Ongoing)
1. **GitHub Actions Workflows**
   - Client build and test pipeline
   - API build and test pipeline
   - Automated versioning

2. **Azure Deployment**
   - Static Web Apps for Angular frontend
   - App Service for .NET API
   - SQL Database or Cosmos DB
   - Application Insights for monitoring

## Development Workflow for Phase 2

```bash
# Terminal 1: Start Angular client
cd src/client
npm start              # Runs on http://localhost:4200

# Terminal 2: Start .NET API
cd src/api
dotnet run             # Runs on http://localhost:5076

# Both services run independently and communicate via HTTP
# Angular client calls API at http://localhost:5076/api/...
```

## Testing Strategy

### Angular Client
- Unit tests: `npm test` in `src/client/`
- E2E tests: Cypress or Playwright (to be added in Phase 2)
- Test coverage: Aim for 70%+ on core services and facades

### .NET API
- Unit tests: `dotnet test` (to be added in Phase 2)
- Integration tests: Using WebApplicationFactory
- API tests: Postman or similar for endpoint validation

## Monitoring & Logging

### Phase 2 Additions
- **Application Insights**: For Azure monitoring
- **Logging**: Serilog for .NET, NgxLogger for Angular
- **Error Tracking**: Azure DevOps or similar

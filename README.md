# 📚 BookHive

A GoodReads-style social reading platform built with modern technologies.

## 🚀 Tech Stack

- **Frontend:** Angular 17+, Tailwind CSS
- **Backend:** .NET 8 Web API (Phase 2+)
- **Database:** Azure SQL, Cosmos DB (Phase 3+)
- **Cloud:** Microsoft Azure
- **CI/CD:** GitHub Actions → Azure Static Web Apps

## 📋 Features

### Phase 1 (Current)
- [x] Book catalog browsing
- [x] Search by title, author, ISBN
- [x] Genre filtering
- [x] Book detail pages
- [x] Responsive design

### Phase 2+ (Planned)
- [ ] User authentication
- [ ] Personal bookshelves
- [ ] Reading progress tracker
- [ ] Ratings and reviews
- [ ] Social features

## 🛠️ Getting Started

### Prerequisites
- Node.js 18+
- Angular CLI 17+
- Git

### Installation
```bash
# Clone the repository
git clone https://github.com/yourusername/bookhive.git

# Navigate to project
cd bookhive

# Install dependencies
npm install

# Start development server
ng serve

# Open browser at http://localhost:4200
```

## 📁 Project Structure
bookhive/
├── src/app/
│   ├── core/           # Singleton services, guards, interceptors
│   ├── shared/         # Reusable components, pipes, directives
│   ├── features/       # Feature modules (books, authors, genres)
│   ├── layout/         # Layout components
│   └── app.routes.ts   # Application routing
├── src/assets/         # Static assets and mock data
├── src/environments/   # Environment configurations
└── tailwind.config.js  # Tailwind CSS configuration

## 🔧 Available Scripts
```bash
npm start        # Start dev server
npm run build    # Production build
npm test         # Run unit tests
npm run lint     # Lint code
```

## 🌐 Deployment

This project is configured for deployment to Azure Static Web Apps via GitHub Actions.

## 📄 License

MIT License - see LICENSE file for details.

## 👤 Author

Srikanth - [GitHub](https://github.com/asriyam)

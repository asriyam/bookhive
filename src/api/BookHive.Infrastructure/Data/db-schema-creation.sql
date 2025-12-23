/*
================================================================================
BookHive Database Schema - Azure SQL Database DDL
================================================================================
This script creates the complete database schema for BookHive, a GoodReads-style 
social reading platform. Execute this script against Azure SQL Database to 
create all tables with proper constraints, indexes, and relationships.

Created: December 2025
Version: 1.0
Target: Azure SQL Database (SQL Server 2019 compatible)
================================================================================
*/

-- Set error handling to continue on error so we can see all issues
-- Remove this in production if you want strict error handling
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

/*
================================================================================
TABLE 1: USERS
Description: Stores user account information for the BookHive platform
Relationships: Referenced by multiple tables (shelves, reviews, blogs, etc.)
Notes: Core user table - all other user-related data references this
================================================================================
*/
CREATE TABLE [dbo].[Users] (
    [UserId] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [FirstName] VARCHAR(300) NOT NULL,
    [LastName] VARCHAR(300) NOT NULL,
    [DisplayName] VARCHAR(300) NOT NULL,
    [Avatar] VARCHAR(300) NULL,
    [Email] VARCHAR(300) NOT NULL UNIQUE,
    [Birth] DATE NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
);

-- Create index on Email for faster lookups during authentication
CREATE INDEX IX_Users_Email ON [dbo].[Users]([Email]);

/*
================================================================================
TABLE 2: AUTHORS
Description: Stores author information for books in the catalog
Relationships: Referenced by AuthorBooks (junction) and Quotes tables
Notes: Separate from Users - books have authors, users are readers/reviewers
================================================================================
*/
CREATE TABLE [dbo].[Authors] (
    [AuthorId] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [FirstName] VARCHAR(300) NOT NULL,
    [LastName] VARCHAR(300) NOT NULL,
    [Description] VARCHAR(3000) NULL,
    [Author_Image] VARCHAR(512) NULL,
    [Birth] DATE NULL,
    [Death] DATE NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
);

-- Create index on author name for search and discovery
CREATE INDEX IX_Authors_Name ON [dbo].[Authors]([LastName], [FirstName]);

/*
================================================================================
TABLE 3: BOOKS
Description: Stores book catalog information
Relationships: Referenced by AuthorBooks, UserBookShelves, UserReviews, Quotes
Notes: Core book catalog table - the central entity in BookHive
    book_type: tinyint enum values (1=Hardcover, 2=Paperback, 3=Ebook, etc.)
    language: stored as language code (e.g., 'en', 'es', 'fr')
================================================================================
*/
CREATE TABLE [dbo].[Books] (
    [BookId] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [Title] VARCHAR(300) NOT NULL,
    [Description] VARCHAR(3000) NULL,
    [Book_Image] VARCHAR(512) NULL,
    [Publisher] VARCHAR(300) NULL,
    [Book_Type] TINYINT NOT NULL DEFAULT 1,
    [PageCount] INT NOT NULL,
    [Published_Date] DATE NULL,
    [ISBN] VARCHAR(15) NULL UNIQUE,
    [ASIN] VARCHAR(15) NULL,
    [Language] VARCHAR(30) NOT NULL DEFAULT 'en',
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
);

-- Create indexes for common search patterns
CREATE INDEX IX_Books_Title ON [dbo].[Books]([Title]);
CREATE INDEX IX_Books_ISBN ON [dbo].[Books]([ISBN]);
CREATE INDEX IX_Books_PublishedDate ON [dbo].[Books]([Published_Date]);

/*
================================================================================
TABLE 4: TAGS
Description: Stores global tag definitions for quotes
Relationships: Referenced by QuoteTags (junction table)
Notes: Normalized tag management - supports multiple tags per quote
    Each tag is unique across the system - shared vocabulary for categorization
================================================================================
*/
CREATE TABLE [dbo].[Tags] (
    [TagId] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [Tag] VARCHAR(300) NOT NULL UNIQUE,
    [Description] VARCHAR(3000) NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
);

-- Create index on Tag for quick lookups and aggregation
CREATE INDEX IX_Tags_Tag ON [dbo].[Tags]([Tag]);

/*
================================================================================
TABLE 5: SHELVES
Description: Stores shelf definitions - both system and user-created
Relationships: Referenced by UserBookShelves and UserShelfConfig
Notes: Core shelving system - unified storage for genres and shelves
    shelftype: tinyint enum (1=System, 2=UserCreated)
    isdefault: bit flag for the three mandatory shelves (Want to Read, Currently Reading, Read)
    shelfname: UNIQUE constraint ensures no duplicate shelf names across system
    createdbyuserid: FK to Users, but NULL for system/default shelves
================================================================================
*/
CREATE TABLE [dbo].[Shelves] (
    [ShelfId] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [ShelfName] VARCHAR(300) NOT NULL UNIQUE,
    [Description] VARCHAR(3000) NULL,
    [ShelfType] TINYINT NOT NULL, -- 1=System, 2=UserCreated
    [IsDefault] BIT NOT NULL DEFAULT 0, -- Flag for the three mandatory shelves
    [CreatedByUserId] INT NULL REFERENCES [dbo].[Users]([UserId]) ON DELETE SET NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
);

-- Create index on ShelfName for quick lookups and discovery
CREATE INDEX IX_Shelves_ShelfName ON [dbo].[Shelves]([ShelfName]);
CREATE INDEX IX_Shelves_ShelfType ON [dbo].[Shelves]([ShelfType]);

/*
================================================================================
TABLE 6: AUTHORBOOKS
Description: Junction table linking authors to books (many-to-many relationship)
Relationships: References Authors and Books tables
Notes: Supports multiple authors per book and multiple books per author
    This normalized structure is more scalable than storing author_id directly in Books
================================================================================
*/
CREATE TABLE [dbo].[AuthorBooks] (
    [AuthorBooksId] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [AuthorId] INT NOT NULL REFERENCES [dbo].[Authors]([AuthorId]) ON DELETE CASCADE,
    [BookId] INT NOT NULL REFERENCES [dbo].[Books]([BookId]) ON DELETE CASCADE,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
);

-- Create unique constraint to prevent duplicate author-book relationships
ALTER TABLE [dbo].[AuthorBooks] ADD CONSTRAINT UC_AuthorBooks_AuthorBook UNIQUE ([AuthorId], [BookId]);

-- Create index for efficient queries of "all books by author" and "all authors of book"
CREATE INDEX IX_AuthorBooks_BookId ON [dbo].[AuthorBooks]([BookId]);

/*
================================================================================
TABLE 7: USERBOOKSHELVES
Description: Core relationship table linking users, books, and shelves
Relationships: References Users, Books, and Shelves tables
Notes: This is the heart of the tagging system
    Supports both default shelves (Want to Read, Currently Reading, Read) and custom shelves
    isfavorite: marks whether the user has marked this shelf as a favorite genre
    When combined with Shelves.isdefault and Shelves.isexclusive, enforces business rules
================================================================================
*/
CREATE TABLE [dbo].[UserBookShelves] (
    [UserBookShelfId] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [UserId] INT NOT NULL REFERENCES [dbo].[Users]([UserId]) ON DELETE CASCADE,
    [BookId] INT NOT NULL REFERENCES [dbo].[Books]([BookId]) ON DELETE CASCADE,
    [ShelfId] INT NOT NULL REFERENCES [dbo].[Shelves]([ShelfId]) ON DELETE CASCADE,
    [IsFavorite] BIT NOT NULL DEFAULT 0, -- Marks favorite genres/shelves
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
);

-- Create unique constraint to prevent duplicate book-shelf assignments for a user
ALTER TABLE [dbo].[UserBookShelves] ADD CONSTRAINT UC_UserBookShelves_UserBookShelf UNIQUE ([UserId], [BookId], [ShelfId]);

-- Create indexes for efficient queries
-- Used when showing "all books on a user's shelf"
CREATE INDEX IX_UserBookShelves_UserShelf ON [dbo].[UserBookShelves]([UserId], [ShelfId]);
-- Used when aggregating shelves for a book (Top Shelves feature)
CREATE INDEX IX_UserBookShelves_BookShelf ON [dbo].[UserBookShelves]([BookId], [ShelfId]);
-- Used for finding favorite shelves
CREATE INDEX IX_UserBookShelves_IsFavorite ON [dbo].[UserBookShelves]([UserId], [IsFavorite]);

/*
================================================================================
TABLE 8: USERSHELFCONFIG
Description: Stores user-specific configuration for each shelf they use
Relationships: References Users and Shelves tables
Notes: Captures how individual users configure their shelves
    One row per user-shelf combination (enforced by unique constraint)
    Flags control shelf behavior: exclusive (only one book per shelf), sticky (shown first),
    sortable (user can order books), feature (show on profile), recsenabled (recommendations)
    Based on Goodreads shelf configuration model
================================================================================
*/
CREATE TABLE [dbo].[UserShelfConfig] (
    [UserShelfConfigId] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [ShelfId] INT NOT NULL REFERENCES [dbo].[Shelves]([ShelfId]) ON DELETE CASCADE,
    [UserId] INT NOT NULL REFERENCES [dbo].[Users]([UserId]) ON DELETE CASCADE,
    [IsExclusive] BIT NOT NULL DEFAULT 0, -- Book can only be on one exclusive shelf
    [IsSticky] BIT NOT NULL DEFAULT 0,     -- Shelf sorted first on user's profile
    [IsSortable] BIT NOT NULL DEFAULT 0,   -- User can manually order books
    [IsFeature] BIT NOT NULL DEFAULT 0,    -- Show this shelf on user's profile
    [RecsEnabled] BIT NOT NULL DEFAULT 0,  -- Generate recommendations for this shelf
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
);

-- Create unique constraint to ensure each user configures each shelf exactly once
ALTER TABLE [dbo].[UserShelfConfig] ADD CONSTRAINT UC_UserShelfConfig_UserShelf UNIQUE ([UserId], [ShelfId]);

-- Create index for efficient lookups of shelf configuration for a user
CREATE INDEX IX_UserShelfConfig_User ON [dbo].[UserShelfConfig]([UserId]);

/*
================================================================================
TABLE 9: USERREVIEWS
Description: Stores user book reviews, ratings, and reading progress
Relationships: References Users and Books tables
Notes: Tracks user reviews, ratings (1-5), and reading dates
    datestarted: when user started reading (nullable - may not track this initially)
    datefinished: when user finished reading (nullable - may not track this initially)
    rating: numeric rating (tinyint can store 0-255, but typically 0-5)
    reviewtext: full text review (optional - user can rate without writing a review)
    This table supports Phase 3 (database integration) and Phase 4 (social features)
================================================================================
*/
CREATE TABLE [dbo].[UserReviews] (
    [ReviewId] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [UserId] INT NOT NULL REFERENCES [dbo].[Users]([UserId]) ON DELETE CASCADE,
    [BookId] INT NOT NULL REFERENCES [dbo].[Books]([BookId]) ON DELETE CASCADE,
    [Rating] TINYINT NOT NULL DEFAULT 0,
    [ReviewText] VARCHAR(5000) NULL,
    [DateStarted] DATETIME NULL,
    [DateFinished] DATETIME NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
);

-- Create indexes for common queries
-- Used when displaying all reviews for a book
CREATE INDEX IX_UserReviews_BookId ON [dbo].[UserReviews]([BookId]);
-- Used when displaying all reviews by a user
CREATE INDEX IX_UserReviews_UserId ON [dbo].[UserReviews]([UserId]);
-- Used for rating aggregation
CREATE INDEX IX_UserReviews_Rating ON [dbo].[UserReviews]([BookId], [Rating]);

/*
================================================================================
TABLE 10: QUOTES
Description: Stores memorable quotes from books
Relationships: References Authors and Books tables (both optional)
Notes: Flexible quote storage - quotes can be attributed to:
    - A specific book (bookid not null, authorid null)
    - A specific author directly (authorid not null, bookid null)
    - Just the text itself (both nullable, though this should be rare)
    This supports community quotes feature from Phase 1 and future community contributions
================================================================================
*/
CREATE TABLE [dbo].[Quotes] (
    [QuoteId] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [QuoteText] VARCHAR(3000) NOT NULL,
    [AuthorId] INT NULL REFERENCES [dbo].[Authors]([AuthorId]) ON DELETE SET NULL,
    [BookId] INT NULL REFERENCES [dbo].[Books]([BookId]) ON DELETE SET NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
);

-- Create indexes for efficient quote discovery
-- Used when showing quotes from a specific book
CREATE INDEX IX_Quotes_BookId ON [dbo].[Quotes]([BookId]);
-- Used when showing quotes by a specific author
CREATE INDEX IX_Quotes_AuthorId ON [dbo].[Quotes]([AuthorId]);

/*
================================================================================
TABLE 11: QUOTETAGS
Description: Junction table linking quotes to tags (many-to-many relationship)
Relationships: References Quotes and Tags tables
Notes: Normalized tag assignment for quotes
    One quote can have multiple tags (e.g., "inspirational", "courage", "philosophy")
    One tag can be applied to many quotes
    Supports aggregation queries: "show all quotes tagged with X" or "most popular tags"
================================================================================
*/
CREATE TABLE [dbo].[QuoteTags] (
    [QuoteTagId] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [QuoteId] INT NOT NULL REFERENCES [dbo].[Quotes]([QuoteId]) ON DELETE CASCADE,
    [TagId] INT NOT NULL REFERENCES [dbo].[Tags]([TagId]) ON DELETE CASCADE,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
);

-- Create unique constraint to prevent duplicate tag assignments to a quote
ALTER TABLE [dbo].[QuoteTags] ADD CONSTRAINT UC_QuoteTags_QuoteTag UNIQUE ([QuoteId], [TagId]);

-- Create index for efficient queries of "all quotes with a specific tag"
CREATE INDEX IX_QuoteTags_TagId ON [dbo].[QuoteTags]([TagId]);

/*
================================================================================
TABLE 12: BLOGS
Description: Stores blog posts (admin/staff authored only)
Relationships: No foreign keys - createdby is stored as plain text
Notes: Simple blog for BookHive staff and admin posts
    blogtitle: required headline for discoverability
    excerpt: required summary text shown in blog lists
    blogtext: full content (varchar(5000) - adjust to varchar(MAX) if needed for longer posts)
    status: tinyint enum (1=Draft, 2=Published, 3=Archived)
    viewcount: tracks engagement on the blog post
    createdby: plain text (not FK) allows flexibility for guest authors or staff names
    This supports the Explore/Featured content from Phase 1
================================================================================
*/
CREATE TABLE [dbo].[Blogs] (
    [BlogId] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [BlogTitle] VARCHAR(300) NOT NULL,
    [BlogText] VARCHAR(5000) NOT NULL,
    [Excerpt] VARCHAR(500) NOT NULL,
    [Status] INT NOT NULL DEFAULT 1, -- 1=Draft, 2=Published, 3=Archived
    [ViewCount] TINYINT NOT NULL DEFAULT 0,
    [CreatedBy] VARCHAR(100) NOT NULL,
    [FeaturedImage] VARCHAR(300) NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
);

-- Create index for published blogs (used for public display)
CREATE INDEX IX_Blogs_Status ON [dbo].[Blogs]([Status]);
-- Create index for finding recent blogs
CREATE INDEX IX_Blogs_CreatedDate ON [dbo].[Blogs]([CreatedDate] DESC);

/*
================================================================================
ADDITIONAL NOTES AND GUIDELINES
================================================================================

DEFAULT VALUES AND TIMESTAMPS:
All tables include CreatedDate and ModifiedDate fields that default to GETUTCDATE().
This is a best practice that allows you to track when records were created and last modified.
In your .NET application, ensure you:
1. Never manually set CreatedDate - let the database default it
2. Update ModifiedDate on every UPDATE operation (or add a trigger to do this automatically)

FOREIGN KEY CASCADING:
Most foreign keys use ON DELETE CASCADE to maintain referential integrity while allowing
clean deletion of related records. For example, when a user is deleted, all their reviews,
shelves, and configurations are automatically deleted.

Exception: Some fields use ON DELETE SET NULL (e.g., Shelves.CreatedByUserId) to preserve
the shelf even if the user who created it is deleted.

INDEXES:
Indexes are created for:
1. Foreign key columns (for efficient joins)
2. Fields used in WHERE clauses (Email, ShelfName, etc.)
3. Fields used in GROUP BY aggregations
4. Fields used for sorting (CreatedDate)

Consider adding additional indexes based on your query patterns once you start monitoring
performance in Phase 2 and beyond.

UNIQUE CONSTRAINTS:
Unique constraints are used to prevent duplicates where logically appropriate:
- Email in Users (no two users can have the same email)
- ISBN in Books (unique book identifier)
- ShelfName in Shelves (ensures unified shelf vocabulary)
- Junction table composites (prevent duplicate relationships)

NEXT STEPS:
1. Execute this script against your Azure SQL Database
2. Create corresponding Entity Framework Core models in your .NET project
3. Set up your DbContext to use these tables
4. In Phase 3, populate system shelves and initial data
5. Implement application logic for enforcing exclusivity and shelf configuration rules

For questions about schema, refer to your updated books-schema.xlsx documentation.

================================================================================
*/
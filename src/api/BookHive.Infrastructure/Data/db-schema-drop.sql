/*
================================================================================
BookHive Database Schema - DROP TABLE Script
================================================================================
This script safely removes all BookHive tables from the Azure SQL Database.

IMPORTANT: This script will DELETE ALL DATA in the database. Use with caution 
and only in development environments or after backing up your database.

Execution Order: Tables are dropped in reverse dependency order to respect 
foreign key constraints. Child tables (those with foreign keys) are dropped 
before parent tables (those being referenced).

Created: December 2025
Target: Azure SQL Database (SQL Server 2019 compatible)
================================================================================
*/

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

/*
================================================================================
DROP ORDER EXPLANATION
================================================================================

The following order respects foreign key dependencies:

TIER 1 - Junction/Child Tables (most dependent):
  - QuoteTags (references Quotes, Tags)
  - UserReviews (references Users, Books)
  - UserShelfConfig (references Users, Shelves)
  - UserBookShelves (references Users, Books, Shelves)
  - AuthorBooks (references Authors, Books)

TIER 2 - Data Tables (some dependencies):
  - Blogs (no FK dependencies, but dropped early for clarity)
  - Quotes (references Authors, Books)

TIER 3 - Lookup/Reference Tables (fewer dependencies):
  - Shelves (referenced by UserBookShelves, UserShelfConfig - dropped in Tier 1)
  - Books (referenced by multiple tables - all dropped in Tier 1)
  - Tags (referenced by QuoteTags - dropped in Tier 1)

TIER 4 - Core Tables (most referenced):
  - Authors (referenced by AuthorBooks, Quotes - both dropped in Tier 1)
  - Users (referenced by multiple tables - all dropped in Tier 1)

================================================================================
*/

PRINT '==================== DROPPING TABLES ===================='
PRINT 'WARNING: This will delete all data from the BookHive database!'
PRINT 'Ensure you have a backup before proceeding.'
PRINT '';

-- ============================================================================
-- TIER 1: DROP JUNCTION AND CHILD TABLES (highest dependency tier)
-- ============================================================================

PRINT 'Tier 1: Dropping junction and child tables...'

-- QuoteTags: Junction table linking Quotes to Tags
-- This table must be dropped before Quotes and Tags
IF OBJECT_ID('[dbo].[QuoteTags]', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping [QuoteTags]...'
    DROP TABLE [dbo].[QuoteTags];
    PRINT 'Success: [QuoteTags] dropped'
END
ELSE
BEGIN
    PRINT 'Note: [QuoteTags] does not exist (already dropped or never created)'
END

-- UserReviews: User reviews of books
-- Must be dropped before Users and Books
IF OBJECT_ID('[dbo].[UserReviews]', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping [UserReviews]...'
    DROP TABLE [dbo].[UserReviews];
    PRINT 'Success: [UserReviews] dropped'
END
ELSE
BEGIN
    PRINT 'Note: [UserReviews] does not exist (already dropped or never created)'
END

-- UserShelfConfig: User-specific shelf configurations
-- Must be dropped before Users and Shelves
IF OBJECT_ID('[dbo].[UserShelfConfig]', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping [UserShelfConfig]...'
    DROP TABLE [dbo].[UserShelfConfig];
    PRINT 'Success: [UserShelfConfig] dropped'
END
ELSE
BEGIN
    PRINT 'Note: [UserShelfConfig] does not exist (already dropped or never created)'
END

-- UserBookShelves: Core relationship table linking users, books, and shelves
-- This is the heart of the shelving system - must be dropped first
IF OBJECT_ID('[dbo].[UserBookShelves]', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping [UserBookShelves]...'
    DROP TABLE [dbo].[UserBookShelves];
    PRINT 'Success: [UserBookShelves] dropped'
END
ELSE
BEGIN
    PRINT 'Note: [UserBookShelves] does not exist (already dropped or never created)'
END

-- AuthorBooks: Junction table linking Authors to Books
-- Must be dropped before Authors and Books
IF OBJECT_ID('[dbo].[AuthorBooks]', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping [AuthorBooks]...'
    DROP TABLE [dbo].[AuthorBooks];
    PRINT 'Success: [AuthorBooks] dropped'
END
ELSE
BEGIN
    PRINT 'Note: [AuthorBooks] does not exist (already dropped or never created)'
END

PRINT '';

-- ============================================================================
-- TIER 2: DROP DATA TABLES
-- ============================================================================

PRINT 'Tier 2: Dropping data tables...'

-- Blogs: Blog posts from admin/staff
-- No external dependencies (createdby is plain text)
IF OBJECT_ID('[dbo].[Blogs]', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping [Blogs]...'
    DROP TABLE [dbo].[Blogs];
    PRINT 'Success: [Blogs] dropped'
END
ELSE
BEGIN
    PRINT 'Note: [Blogs] does not exist (already dropped or never created)'
END

-- Quotes: Memorable quotes from books and authors
-- Can be dropped after QuoteTags
IF OBJECT_ID('[dbo].[Quotes]', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping [Quotes]...'
    DROP TABLE [dbo].[Quotes];
    PRINT 'Success: [Quotes] dropped'
END
ELSE
BEGIN
    PRINT 'Note: [Quotes] does not exist (already dropped or never created)'
END

PRINT '';

-- ============================================================================
-- TIER 3: DROP LOOKUP AND REFERENCE TABLES
-- ============================================================================

PRINT 'Tier 3: Dropping lookup and reference tables...'

-- Shelves: Book shelves (both system and user-created)
-- Safe to drop after UserBookShelves and UserShelfConfig are gone
IF OBJECT_ID('[dbo].[Shelves]', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping [Shelves]...'
    DROP TABLE [dbo].[Shelves];
    PRINT 'Success: [Shelves] dropped'
END
ELSE
BEGIN
    PRINT 'Note: [Shelves] does not exist (already dropped or never created)'
END

-- Books: Book catalog
-- Safe to drop after AuthorBooks, UserBookShelves, UserReviews, and Quotes
IF OBJECT_ID('[dbo].[Books]', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping [Books]...'
    DROP TABLE [dbo].[Books];
    PRINT 'Success: [Books] dropped'
END
ELSE
BEGIN
    PRINT 'Note: [Books] does not exist (already dropped or never created)'
END

-- Tags: Quote tags/categories
-- Safe to drop after QuoteTags
IF OBJECT_ID('[dbo].[Tags]', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping [Tags]...'
    DROP TABLE [dbo].[Tags];
    PRINT 'Success: [Tags] dropped'
END
ELSE
BEGIN
    PRINT 'Note: [Tags] does not exist (already dropped or never created)'
END

PRINT '';

-- ============================================================================
-- TIER 4: DROP CORE TABLES
-- ============================================================================

PRINT 'Tier 4: Dropping core tables...'

-- Authors: Book authors
-- Safe to drop after AuthorBooks and Quotes
IF OBJECT_ID('[dbo].[Authors]', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping [Authors]...'
    DROP TABLE [dbo].[Authors];
    PRINT 'Success: [Authors] dropped'
END
ELSE
BEGIN
    PRINT 'Note: [Authors] does not exist (already dropped or never created)'
END

-- Users: Platform users (readers and reviewers)
-- Safe to drop after all tables referencing it
-- This is typically the last to be dropped
IF OBJECT_ID('[dbo].[Users]', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping [Users]...'
    DROP TABLE [dbo].[Users];
    PRINT 'Success: [Users] dropped'
END
ELSE
BEGIN
    PRINT 'Note: [Users] does not exist (already dropped or never created)'
END

PRINT '';
PRINT '==================== CLEANUP COMPLETE ===================='
PRINT 'All BookHive tables have been dropped successfully.'
PRINT 'The database is now empty and ready for a fresh schema creation.'
PRINT '';
PRINT 'Next step: Execute BookHive_Schema_DDL.sql to recreate the tables.'
PRINT '===========================================================';

/*
================================================================================
RECOVERY INFORMATION
================================================================================

If you accidentally ran this script, here are your recovery options:

1. RESTORE FROM BACKUP (Recommended)
   - Contact your Azure SQL administrator
   - Restore from the latest backup of your database
   - This is the safest recovery method

2. RECREATE SCHEMA
   - If no backup exists and you only lost the schema structure (not data)
   - Execute BookHive_Schema_DDL.sql to recreate all tables
   - Any data in the original tables is permanently lost unless backed up elsewhere

3. PREVENT ACCIDENTAL DROPS
   - Use ALTER DATABASE ... SET READ_ONLY to prevent modifications
   - Use Azure SQL database backup settings to ensure regular backups
   - Implement database roles and permissions to restrict who can drop tables
   - Add transaction controls to your deployment scripts

================================================================================
*/
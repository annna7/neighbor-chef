# Neighbor Chef

### Table of Contents
  - [Description](#description)
  - [Features](#features)
  - [Technologies](#tech-stack)
  - [Project Requirements](#project-requirements)

## Description

Neighbor Chef is a full-stack web application that allows users to order home-cooked meals from local cooking enthusiasts. 
The app serves as a platform for local chefs to share their passion for cooking with their community and make some extra money on the side, while also providing customers with a convenient and affordable way to come home to delicious, healthy meals while juggling their busy lives.

## Features
- Authentication and Authorization as either a Customer or a Chef
- Customers can browse through the list of chefs and their meals, and filter and sort them by criteria such as price, rating, cuisine, etc.
- Customers can see chef profiles, menus and reviews, and leave reviews of their own
- Customers can place orders and see their order history
- Chefs can create, edit, and delete their meals
- Chefs can modify their availability
- Chefs can see their order history and the details of each order
- Chefs can see their reviews and the details of each review
- Both parties can see their profile and edit their information
- Both parties get notified via in-app notifications and email when the status of their order changes

## Tech Stack
### Backend
- .NET 7.0 (C#)
- Entity Framework Core
- ASP.NET Core Identity
- Microsoft SQL Server (Database)
### Frontend
- Angular (v. 17) with Typescript
- Material UI
### Misc
- Firebase (image storage + notifications)
- Brevo (email services)

## Project Requirements
### Back End
- [x] 10 Controllers, CRUD methods, REST
- [x] One to one relationship (User and Address)
- [x] One to many relationship (Chef and Meals, Customer and Reviews, Customer and Orders, Chef and Orders etc.)
- [x] Many to many relationship (Meals and Orders)
- [x] Linq queries
- [x] Authentication and Authorization (two roles: Customer and Chef)
- [x] Repository pattern
- [x] Services

#### Nice to have:
- [x] Unit of Work (1p)
- [x] Specification pattern (1p)
- [x] Identity Authentication (1p)
- [x] SMTP with Brevo (former SendinBlue) (1p)
- [x] Upload files with Firebase Storage (1p)
- [x] Firebase notifications (1p)

### Front End
- [x] At least 3 components
- [x] Routing
- [x] At least one directive (ImageLoader, Subtitle, Title)
- [x] At least one pipe (Rating pipe, Camel case formatting pipe)]
- [x] Register + Login (with reactive forms)
- [x] Guard implementation

#### Nice to have:
- [x] At least 3 extra RXJS methods (`switchMap`, `forkJoin`, `of`, `finalize`, `last`, etc.)




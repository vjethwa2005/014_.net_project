# Blush Event Portal

## Overview
A full-featured ASP.NET Core MVC web application for event bookings. Users can browse events, RSVP, and manage their registrations. Features a soft pink aesthetic design with Bootstrap 5.

## Features
- **Home Page**: Displays upcoming events with images, dates, venues, and RSVP buttons
- **User Authentication**: Registration and login using ASP.NET Core Identity
- **RSVP System**: Logged-in users can RSVP for events and view their registrations
- **Admin Panel**: Add, edit, and delete events (admin@blush.com / Admin123!)
- **Contact Form**: Users can send messages to the admin
- **Email Notifications**: RSVP confirmations are logged (email integration ready)
- **Responsive Design**: Mobile-friendly soft pink theme with gradients and modern cards

## Tech Stack
- **Framework**: ASP.NET Core 8.0 MVC
- **Authentication**: ASP.NET Core Identity
- **Database**: SQLite with Entity Framework Core
- **Email**: MailKit (logging mode for development)
- **UI**: Bootstrap 5 with custom pink gradient theme
- **Icons**: Font Awesome 6

## Project Structure
```
BlushEventPortal/
├── Controllers/          # MVC Controllers
│   ├── HomeController.cs
│   ├── EventController.cs
│   ├── RSVPController.cs
│   ├── ContactController.cs
│   └── AdminController.cs
├── Models/              # Data Models
│   ├── Event.cs
│   ├── RSVP.cs
│   ├── ApplicationUser.cs
│   ├── ContactViewModel.cs
│   └── ErrorViewModel.cs
├── Views/               # Razor Views
│   ├── Home/
│   ├── Event/
│   ├── RSVP/
│   ├── Contact/
│   ├── Admin/
│   └── Shared/
├── Areas/Identity/      # Identity Pages
│   └── Pages/Account/
├── Data/                # Database Context
│   ├── ApplicationDbContext.cs
│   └── DbInitializer.cs
├── Services/            # Services
│   ├── IEmailSender.cs
│   └── EmailSender.cs
├── wwwroot/            # Static Files
│   ├── css/
│   └── js/
└── Program.cs          # Application Entry Point
```

## Database Schema
- **ApplicationUser**: Extended Identity user with FirstName, LastName
- **Event**: Id, Name, Description, EventDate, Venue, ImageUrl, Capacity, CreatedAt
- **RSVP**: Id, EventId, UserId, RSVPDate, Notes (unique constraint on EventId+UserId)

## Seed Data
- **Admin User**: admin@blush.com / Admin123!
- **Sample Events**: 
  - Summer Music Festival (500 capacity)
  - Tech Meetup & Networking (100 capacity)
  - Rooftop Sunset Party (150 capacity)

## Running the Application
```bash
dotnet restore
dotnet build
dotnet run --urls http://0.0.0.0:5000
```

## Key Routes
- `/` - Home page with upcoming events
- `/Event` - All events
- `/Event/Details/{id}` - Event details
- `/RSVP/MyRSVPs` - User's RSVP list
- `/Contact` - Contact form
- `/Admin` - Admin panel (requires Admin role)
- `/Identity/Account/Login` - Login
- `/Identity/Account/Register` - Register

## Design Theme
The application uses a custom soft pink aesthetic:
- **Primary Colors**: Pink gradients (#fce4ec, #f8bbd0, #ff9eb9, #d81b60)
- **Typography**: Segoe UI font family
- **Components**: Rounded corners (20px), soft shadows, gradient backgrounds
- **Cards**: Hover effects with elevation
- **Buttons**: Gradient backgrounds with hover animations
- **Navbar**: Pink gradient with white text

## Recent Changes
- Initial project setup (October 29, 2025)
- Implemented all core features
- Added soft pink theme styling
- Created seed data with sample events
- Configured SQLite database
- Set up ASP.NET Core Identity authentication

## User Preferences
- N/A (First session)

## Next Steps
- Integrate real email service (SendGrid/SMTP)
- Add event search and filtering
- Implement event capacity limits enforcement
- Add user profile management
- Create event calendar view
- Add social sharing features

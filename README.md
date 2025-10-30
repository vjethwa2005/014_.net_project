# 🌸 Blush Event Portal

## 📖 Overview
**Blush Event Portal** is a web-based event booking and RSVP management system built using **ASP.NET Core MVC**.  
It allows users to browse upcoming events, RSVP to their favorite ones, and manage their bookings through a simple and elegant interface.

The project features a **modern pink-themed UI** designed for smooth user experience and easy navigation.

---

## 🎯 Objectives
- To create a user-friendly platform for event discovery and participation.  
- To provide login-based event booking and RSVP functionality.  
- To demonstrate key **.NET MVC** concepts such as models, controllers, and Razor views.  
- To apply CRUD operations in a real-world scenario.  

---

## ✨ Features
- 🏠 **Home Page** with a hero section and event highlights.  
- 📅 **Event Management** – view all events, details, and RSVP.  
- 💌 **RSVP System** – users can confirm their participation and see their booked events.  
- 📧 **Contact Form** – allows users to reach out via email.  
- 🔐 **User Authentication** – secure login and registration using ASP.NET Identity.  
- 🎨 **Custom UI Theme** – soft pink gradient aesthetic for a visually pleasing design.  

---

## ⚙️ Technologies Used

| Category | Tools / Frameworks |
|-----------|--------------------|
| Frontend | HTML5, CSS3, Bootstrap 5 |
| Backend | ASP.NET Core MVC |
| Database | Entity Framework Core with SQLite |
| IDE | Visual Studio / Visual Studio Code |
| Language | C# |
| Version Control | Git & GitHub |

---

## 🏗️ Project Structure
```
BlushEventPortal/
│
├── Controllers/ # MVC Controllers (Event, RSVP, Home, Contact)
├── Models/ # Entity classes for Event and RSVP
├── Views/ # Razor Views (UI pages)
├── wwwroot/ # Static files (CSS, JS, Images)
├── Data/ # Database context and migrations
├── Services/ # Utility or helper services
├── appsettings.json # Configuration settings
└── Program.cs # Entry point of the application
```
---

## 🚀 How to Run Locally

### 1. Clone the repository
```bash
git clone https://github.com/<your-username>/BlushEventPortal.git
cd BlushEventPortal
```

### 2. Restore dependencies
```
dotnet restore
```
### 3. Build the project
dotnet build

### 4. Run the application
```
dotnet run
```

Then open your browser and visit 👉 https://localhost:5001
 (or as shown in terminal).
---
## 🧠 Learning Outcomes

Gained practical understanding of the MVC architecture.

Learned how to integrate Entity Framework for database operations.

Implemented user authentication and data validation in ASP.NET Core.

Applied UI design concepts for a professional web interface.

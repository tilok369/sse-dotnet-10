# Native SSE Implementation in .NET 10
## This is a dummy ride sharing app with realtime route status notification system.

## Backend Implementation
- A .NET 10 Web API
- Minimal API to expose SSE events
- Background Service Worker to update status on realtime.

## Frontend Implementation
- A .NET 10 ASP.NET Core MVC App
- Vanilla JavaScript to connect and register to SSE events to get the update in the page in realtime.

This is a RESTFUL TODO API built using ASP.NET Core (.NET8). It allows the users to create, read, update and delete TODO items with proper validation, error handling and correct HTTP status codes.
HOW TO RUN: a. Clone the repository, b. Open the project in visual studio, c. Run the application: Click RUN(IIS Express), d. Open Swagger: http://localhost:xxxx/swagger.
API Endpoints: 1. GET/api/todos; Get all TODOs(Response: [], Status Codes: 200 OK, 2. GET/api/todos/{id}; Get a single TODO (Response: {"id": 1, "title": "My task", "description": "Test", "isCompleted": false, "createdAt": "20206-05-08T10:00:00", "dueDate": null, "priority": "Medium"} Status Codes: . 200 OK .404 Not Found
POST/api/todos (Create a new TODO) Request Body: {"title": "Complete assignment", "description": "Finish Week 11", "priority": "High:} Response: {"id": 1, "title": "Complete assignment", "description": "Finish Week11", "isComplete": false, "createdAt": "2026-05-08T10:00:00", "dueDate": null, "priority": "High"} Error Response: {"statusCode": 400, "message": "Validation failed", "errors": {"Title": ["Title is required"]}, "timestamp": "2026-05-08T10:00:00"} Status Codes: . 200 Created . 400 Bad Request
PUT/api/todos/{id} Update a TODO (Resquest Body: {"title": "Update tast", "description": "Updated description", "isCompleted": true, "priority": "Low"} Status Codes: . 200 OK . 400 Bad Request . 400 Not Found
DELETE/api/todos/{id} Status Codes: . 204 No Content . 404 Not Found 
Validation Rules .Title is required (3-100 characters) .Title cannot be empty or whitespace  .Description max length: 500  .Priority must be: Low, Medium, High  .DueDate cannot be in the past
Testing: Tested using Swagger and Postman
Project Structure: Controllers/ Models/ DTOs/ Response/ Program.cs  README.md
Author: Ozoani Chiamaka Christiana
Final Checklist: API runs without errors, Swagger works, All endpoint tested, README added, .gitignore added, At least 8 commits, Postman collection exported
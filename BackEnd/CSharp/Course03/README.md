This contains the AI-Guided user management API Project using Copilot.
# User Management API with Custom Middleware

## Project Overview
This project is a **User Management API** built with **ASP.NET Core**.  
It demonstrates how to implement and configure **custom middleware** for:

- **Logging** → Records all incoming requests and outgoing responses.
- **Error Handling** → Catches unhandled exceptions and returns consistent JSON error responses.
- **Authentication** → Secures endpoints using token-based authentication.

This project was developed as part of a back-end API learning activity.

## Features
- **Logging Middleware**
  - Logs HTTP method, request path, and response status code.
- **Error Handling Middleware**
  - Returns JSON error responses like:
    ```json
    { "error": "Internal server error.", "details": "Exception message" }
    ```
- **Authentication Middleware**
  - Validates tokens from the `Authorization` header.
  - Allows only requests with `Bearer valid-token`.
  - Returns `401 Unauthorized` for invalid/missing tokens.
  - Skips Swagger UI paths so documentation is accessible.
  - 
---
## Getting Started

### Prerequisites
- [.NET 6+ SDK](https://dotnet.microsoft.com/download)
- Visual Studio / VS Code

### Run the API
dotnet watch run

The API will start at: http://localhost:5016
Swagger UI will be available at: http://localhost:5016/swagger

**Authentication**

Endpoints require an Authorization header:

Authorization: Bearer valid-token
Without this header, requests return 401 Unauthorized.

**Swagger UI**

Click Authorize (padlock icon).

Enter:
Bearer valid-token
Now all requests from Swagger will include the token.

**Testing**

You can test with:
- **Swagger UI** → interactive documentation.
- **VS Code** requests.http file → send sample requests.
- **Postman** → manually test endpoints with headers.

**Example request:**
http
GET http://localhost:5016/api/users
Authorization: Bearer valid-token

**Next Steps**
Replace the demo valid-token with JWT authentication for real-world security.
Add database integration for persistent user storage.
Expand middleware with request/response timing or custom logging providers.

**Author**
Developed by Kingsley as part of a back-end API learning project.

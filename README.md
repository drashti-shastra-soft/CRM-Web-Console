
# 🚀 CRM Web Console – Customer Relationship Management

**CRM Web Console** is a responsive ASP.NET web application designed to manage customer relationships and streamline sales activities through a centralized platform.

The application connects with the CRM Web API and provides teams with tools to manage leads, contacts, deals, tasks, follow-ups, sales pipelines, and customer interactions.

It provides secure login, role-based access, and a user-friendly interface for managing CRM operations.

## ✨ Key Features

- **Secure Authentication:** Provides secure user login.
- **Role-Based Access:** Controls application access based on user roles.
- **Dashboard:** Displays important CRM information and sales insights.
- **Lead Management:** Manage and track customer leads.
- **Contact Management:** Maintain customer and business contact information.
- **Deal Management:** Manage sales deals and opportunities.
- **Task Management:** Create and manage business-related tasks.
- **Follow-Up Management:** Track customer follow-ups and activities.
- **Sales Pipeline:** Manage and monitor sales pipeline progress.
- **Interaction Tracking:** Maintain customer interaction records.
- **Sales Reports:** View sales-related reports and information.
- **Real-Time Updates:** Supports timely updates for CRM information.
- **API Integration:** Communicates with the CRM Web API.
- **Responsive Design:** Provides a convenient experience across desktop, tablet, and mobile devices.

## 📦 Main Modules

### 1. Dashboard
- View important CRM information.
- Access sales-related summaries.
- Monitor business activities.

### 2. Lead Management
- Create and manage leads.
- View lead information.
- Track lead progress.
- Manage lead-related activities.

### 3. Contact Management
- Manage customer contacts.
- View contact details.
- Maintain customer information.

### 4. Deal Management
- Create and manage deals.
- Track sales opportunities.
- Monitor deal progress.

### 5. Task Management
- Create tasks.
- View and update tasks.
- Manage assigned activities.

### 6. Follow-Up Management
- Schedule customer follow-ups.
- Track follow-up activities.
- Maintain customer communication history.

### 7. Sales Pipeline
- Manage pipeline stages.
- Monitor sales progress.
- Track opportunities.

### 8. Reports & Analytics
- View sales reports.
- Access CRM-related information.
- Monitor business performance.

## 🔐 Security

- Secure login functionality.
- Role-based access control.
- API-based communication.
- Protected application functionality.

## 🛠️ Technology Stack

- **Framework:** ASP.NET
- **Language:** C#
- **Architecture:** Layered Architecture
- **Frontend:** As implemented in the project
- **API Integration:** CRM Web API
- **Database:** As configured in the application
- **Development Environment:** Visual Studio

## 🏗️ Project Structure

The repository follows a layered architecture with the following projects:

```text
CRM-Web-Console/
│
├── CRMWebConsole.Domain/
│
├── CRMWebConsole.Infrastructure/
│
├── CRMWebConsole.Service/
│
├── CRMWebConsole/
│
├── CRMWebConsole.sln
│
├── .gitattributes
│
└── .gitignore
```

### Folder Responsibilities

| Project | Description |
|---|---|
| CRMWebConsole.Domain | Contains domain models and application-related definitions. |
| CRMWebConsole.Infrastructure | Handles infrastructure-related implementations and integrations. |
| CRMWebConsole.Service | Contains service-layer functionality and business logic. |
| CRMWebConsole | Contains the main ASP.NET web application and user interface. |

## 🔗 Backend API Integration

CRM Web Console communicates with the CRM Web API to perform backend operations and manage CRM data.

**Related Backend Repository:**

[CRM Web Service – GitHub](https://github.com/drashti-shastra-soft/CRM-Web-Service)

Configure the correct CRM Web API base URL in the application settings before running the console.

## ⚙️ Getting Started

### Prerequisites

- Visual Studio
- Compatible .NET SDK / Targeting Pack
- C# development environment
- NuGet Package Manager
- CRM Web API configuration
- Database configuration (if required)

### Installation

**1. Clone the repository**

```bash
git clone https://github.com/drashti-shastra-soft/CRM-Web-Console.git
```

**2. Navigate to the project directory**

```bash
cd CRM-Web-Console
```

**3. Open the solution**

Open the following solution file in Visual Studio:

```text
CRMWebConsole.sln
```

**4. Restore NuGet Packages**

```bash
dotnet restore
```

**5. Configure Application Settings**

Configure the required settings, including:

- CRM Web API base URL
- Database connection (if required)
- Authentication settings
- Other environment-specific configurations

**Security Note:** Never commit production credentials, passwords, API keys, or sensitive configuration values to GitHub.

**6. Build the Solution**

```bash
dotnet build
```

**7. Run the Application**

Set `CRMWebConsole` as the startup project in Visual Studio and run the application.

Make sure the CRM Web API is configured and accessible.

## 🔗 Related Links

- **GitHub Repository:** [CRM Web Console](https://github.com/drashti-shastra-soft/CRM-Web-Console)
- **Backend API:** [CRM Web Service](https://github.com/drashti-shastra-soft/CRM-Web-Service)

## 🎯 Project Objective

The main objective of CRM Web Console is to provide a centralized platform for managing customer relationships and sales operations.

It simplifies lead management, contact management, deal tracking, task management, follow-ups, and sales pipeline monitoring through an integrated web application.

The application is designed to work with the CRM Web API, providing a structured and maintainable solution for CRM operations.

# Job Application Tracker

A C# Console Application for managing companies, recruiters, job vacancies, candidates, and job applications.

This project is developed incrementally across multiple sprints, focusing on C# fundamentals, object-oriented programming, collections, generics, delegates, events, and clean code organization.

## Project Overview

Job Application Tracker simulates a simple job portal workflow:

- Companies register and wait for admin approval.
- Recruiters manage job vacancies.
- Candidates browse and apply for available jobs.
- The system validates business rules and prevents invalid or duplicate operations.
- Event-driven notifications respond to important business operations, such as company approval and application status updates.

All data is stored in memory and is reset when the application restarts.

## Features

### Companies
- Add, update, list, and remove companies.
- Manage company statuses: Pending, Approved, and Rejected.
- List companies by status.
- Undo the most recent status change using a Stack.
- Allow job creation only for approved companies.
- Trigger an event when a company is approved.

### Recruiters
- Add, update, list, and remove recruiters.
- Associate recruiters with existing companies.

### Jobs
- Add, update, list, search, and remove jobs.
- Find jobs by ID using a Dictionary.
- Search jobs by title keyword and minimum salary.
- Combine search filters.
- Sort jobs by salary (ascending or descending) and title.
- Keep the Jobs DataStore and Dictionary synchronized.
- Prevent removing jobs that have existing applications.

### Candidates
- Add, update, list, and remove candidates.
- Prevent duplicate email addresses.
- Compare emails case-insensitively using a HashSet.

### Applications
- Apply for open jobs.
- Validate candidate and job existence.
- Allow applications only when the job's company is approved.
- Prevent duplicate applications by the same candidate for the same job.
- Manage application statuses.
- Keep the applied-jobs HashSet synchronized when applications are removed.
- Trigger events when applications are submitted or their statuses change.

### Event-Driven Notifications
- Use `EventHandler<TEventArgs>` to define strongly typed events.
- Create custom EventArgs classes to carry event data.
- Subscribe to events using named handler methods and lambda expressions.
- Unsubscribe from events when needed.
- Display notifications for company approval, application submission, and application status changes.

### Console User Interface
- Main menu and separate menus for each entity.
- Colored titles, prompts, success messages, warnings, and errors.
- Centralized console formatting through `ConsolePrinter`.
- Safe input handling for integers, decimals, text, Boolean values, and enum values.

## Technologies & Concepts

- C# and .NET Console Application
- Object-Oriented Programming (OOP)
- Interfaces and Generic Constraints
- Generic DataStore
- Collections: List, Dictionary, HashSet, and Stack
- IEnumerable and yield return
- Delegates: Func, Action, Predicate, and Comparison
- Lambda Expressions
- Events and `EventHandler<TEventArgs>`
- Custom EventArgs
- Event Subscription and Unsubscription
- In-memory data storage
- Service-based organization
- Search and sorting without LINQ
- Console input validation and exception handling

## Project Structure

```text
JobApplicationTracker/
├── Data/
│   ├── AppData.cs
│   ├── DataSeeder.cs
│   └── DataStore.cs
├── Enums/
│   ├── ApplicationStatus.cs
│   └── CompanyStatus.cs
├── Events/
│   ├── ApplicationStatusChangedEventArgs.cs
│   ├── ApplicationSubmittedEventArgs.cs
│   └── CompanyApprovedEventArgs.cs
├── Helpers/
│   └── ConsolePrinter.cs
├── Interfaces/
│   └── IEntity.cs
├── Models/
│   ├── Application.cs
│   ├── Candidate.cs
│   ├── Company.cs
│   ├── Job.cs
│   └── Recruiter.cs
├── Practice/
│   ├── CollectionsPractice.cs
│   ├── GenericMethods.cs
│   ├── Delegates.cs
│   └── Events.cs
├── Services/
│   ├── ApplicationService.cs
│   ├── CandidateService.cs
│   ├── CompanyService.cs
│   ├── JobService.cs
│   ├── RecruiterService.cs
│   └── NotificationService.cs
├── UI/
│   └── ConsoleUI.cs
└── Program.cs
```

## Getting Started

### Prerequisites
- Visual Studio or another C# IDE.
- A .NET SDK compatible with the project's target framework.

### Run the Application

1. Clone the repository:

```bash
git clone YOUR_REPOSITORY_URL
```

2. Open the solution in Visual Studio.
3. Build the solution.
4. Run the project.

The application seeds initial data automatically when it starts. No database configuration is required.

## Sprint 1 — Core Console Application

- Models and enums.
- Generic DataStore implementation.
- Seed data.
- Basic entity operations.
- Dictionary-based job lookup.
- Case-insensitive candidate email uniqueness.
- Duplicate application prevention.
- Undo company status changes.
- Console menus and colored output.
- Safe input handling.

## Sprint 2 — Advanced C# and Application Workflows

- Delegates: Func, Action, Predicate, and Comparison.
- Lambda Expressions.
- Search and sorting using delegates and collections without LINQ.
- Company approval workflow and status management.
- Application workflow and business-rule validation.
- Duplicate application protection.
- Event-driven notifications using EventHandler and custom EventArgs.
- Named event handlers, lambda subscriptions, and unsubscription.

## Git Workflow

Check the current working tree:

```bash
git status
```

Stage and commit completed work:

```bash
git add .
git commit -m "Describe the completed requirement"
```

Push commits:

```bash
git push
```

After completing a sprint, create and push its tag:

```bash
git tag sprint-1
git push origin sprint-1
```

For Sprint 2:

```bash
git tag sprint-2
git push origin sprint-2
```

## Important Notes

- No database or Entity Framework is used in the current console application.
- LINQ is intentionally not used in the project implementation.
- All data is stored in memory and is reset when the application restarts.

---

**Job Application Tracker — Sprint 1 & Sprint 2**

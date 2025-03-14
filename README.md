# Junior Developer Aptitude Test
## Overview
This aptitude test is designed to evaluate your ability to work with .NET and Angular in a practical scenario related to the insurance industry. You will be working with a pre-configured project that contains some initial setup, and your task is to implement specific features.
## Provided Setup
The project includes:
### Backend (ASP.NET Core Web API)
- A pre-configured database using Entity Framework Core In-Memory Database, seeded with some initial quotes, as well as states and their rates.
- A Quote model with the following properties:
  - Id (int, auto-generated)
  - Name (string)
  - TIV (decimal, Total Insurable Value)
  - Premium (decimal)
  - StateId (int, fk to State)
  - State (State, Navigation prop)
- A State model with the following properties:
  - Id (int, auto-generated)
  - Abbreviation (string)
  - Rate (decimal, used to calculate the premium)
- An existing GET /api/quotes endpoint that returns all quotes.
### Frontend (Angular Application)
- A data client service to call the API.
- A table component displaying quotes.
- A button to create a new quote.
- A clickable row that navigates to an edit page.
- TailwindCSS has been added for your convenience, if you choose to use it, please uncomment `@import 'tailwindcss;` in the angular global stylesheet `styles.css`.

## Getting Started
Commands are shown below, however you are free to use your favorite IDE.
### 1. Fork this repository
Clone your forked repository to your local machine:
```
git clone https://github.com/YOUR_USERNAME/REPO_NAME.git
cd REPO_NAME
```
### 2. Setting up the server
  1. Navigate to the `AptitudeTestServer` directory: `cd AptitudeTestServer`
  2. Restore dependencies: `dotnet restore`
  3. Run the backend API: `dotnet run`
### 3. Setting up the client
  1. Navigate to the `AptitudeTestClient` directory `cd AptitudeTestClient`
  2. Install dependencies: `npm install`
  3. Run the angular dev server: `ng serve`

## Your Tasks

You need to complete the following:

### 1. Backend Tasks
- Implement the POST /api/quotes endpoint to create a new quote.
- Implement the PUT /api/quotes/{id} endpoint to update an existing quote.
- Calculate the Premium when a quote is created or updated based on the state rate using the formula:
$$Premium = (TIV * Rate) / 100$$
Example: If the state is FL and TIV is 100,000, and the rate is 0.25, then the premium should be $$100,000 * 0.25 / 100 = 250$$.

### 2. Frontend Tasks

- Implement a New Quote Form to save a new quote
- Implement an Edit Quote Form that allows users to modify an existing quote.
- Ensure the form submits data to the API and updates the table upon success.
- The Premium should be read-only and should update automatically when the State or TIV is changed.

## Expectations
- You are free to reference any documentation.
- Please aim to complete the task in about 1-2 hours.
- Implementation details are left vague so that you may express your creativity and how you approach a problem.
- Please do not worry about styling and/or testing, unless you choose to do so.

## Submission
When you are done, please share your github repository with me.

Good luck and please feel free to reach out to wgibbons@gridironins.com for any questions, clarifications, or issues.

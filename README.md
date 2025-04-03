# Project Waterloo - Where does InfoTrack stand in the market?

## Introduction
Simple .NET 8 React SPA that was put together as a framework into which a web scraping service could be added.
Unfortunately, I didn't have time to implement the scraping, but I have implemented the structure of the application, including
strategies for building url, and parsing response from the returned HTML. There are also config entries to set Google and Bing url, headers etc.
It quickly became evident that Google Search doesn't like scraping and I would need to use a headless browser, with plugins or other means, to get around this.
As the spec required **no use of 3rd party libraries or the Google API** this proved to me unmanageable within the timescale, although I would be very happy if someone were to provide me with a solution!
The returned payload is now extremely JavaScript heavy and obfuscated (look at search result source - breakpoint `GenericScrapeService.ScrapeSearchEngine` - line 40 and inspect 'content').
You can't simply query the HTML or load into DOM, even using the likes of Pupeteer or Selenium WebDriver will not work as expected and attempts at supplying HTTP headers,
e.g. requesting results for a text-only browser, will leave you blocked by indirection and invariably result in failure.
I suspect this is a deliberate attempt to thwart users who try to scrape Google Search results, and instead force them to use paid services such as is provided as part of GCP subscriptions and Google API. 
A quick (Yahoo and Bing Ha!) search confirmed this theory to me so I didn't pursue it further.

The provided application simply returns a list of fixed positions.

## Getting Started
- Clone the repository
- If running with VS Code
  1. Select 'RUN AND DEBUG' > 'Launch Debug'
  1. Wait for SPA to run
  1. Try search Target URL:'Google' and Keywords: 'Google' to prove it is reaching out
- If running without VS Code
  1. Go to folder where `Waterloo.Server.csproj` is located (i.e. `./Waterloo.Server`)
  1. Open PowerShell
  1. Execute `dotnet build`
  1. Execute `dotnet run`
  1. Should start the Web API on `http://localhost:5166`
  1. Should start the SPA Development Server on `https://localhost:51048/`
  1. Browse to `https://localhost:51048` to see the application running.

## Project Structure
- `Waterloo.Web` - This is the Presentation Layer - Contains
  1. Contracts - requests/responses
  1. Endpoints - API Mapping (using Minimal API's - could be controller endpoints)
- `Waterloo.Application` - Currently only contains Command and Handler and Validator, but could contain
  1. Commands - Commands to perform actions
  1. Queries - Queries to get data
  1. Handlers - Command and Query Handlers
  1. Validators - Validation for Commands and Queries
  1. Pipeline Behaviours - Cross cutting concerns
- `Waterloo.Domain` - Currently anaemic, but could contain
  1. Entities - Business Objects
  1. Value Objects - Objects that have no identity
  1. Services - Business Logic`
- `Waterloo.Infrastructure` - Currently only contains infra to access Google, but could be used to genericise all HTTP requests (scrapes). Other things here would include..
  1. Data - Data Access Layer
  1. Services - External Services
  1. Logging - Logging
  1. Messaging - Messaging
  1. Caching - Caching`
- `Waterloo.SharedKernel` - Contains shared code across the application
  1. Constants - Constants
  1. Exceptions - Exceptions
  1. Extensions - Extensions
  1. Interfaces - Interfaces
  1. Utilities - Utilities
	
Each project is responsible for it's own dependencies/registrations and has a clear separation of concerns.

## Comments
- In my view this is over-engineered for what it does, but it is more to demonstrate project structures and how you can use commands, queries, pipeline behaviours etc to keep your responsibilities segregated.
- I guess it makes a reasonable foundation if it were to be extended.

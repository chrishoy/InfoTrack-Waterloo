# Project Waterloo - Where does InfoTrack stand in the market?

## Introduction
Simple .NET 8 React SPA that was put together as a framework into which a web scraping service could be added.
Unfortunately, I didn't have time to implement the scraping, but I have implemented the structure of the application.
It very quickly became evident to me that Google Search doesn't like scraping and I would need to use a headless browser, or other means, to get around this.
Google Search is now extremely JavaScript heavy and very obfuscated, even using attempts at supplying HTTP headers, e.g. requesting results for a text-only browser, resulting in failure.
I suspect this is a deliberate attempt to thwart users who scrape Google Search results, and instead force them to use paid services such as is provided as part of GCP subscriptions. 
A quick (Yahoo and Bing Ha!) search confirmed this theory to me so I didn't pursue it further.

The provided application simply returns a list of fixed positions.

## Project Structure
- `Waterloo.Web` - This is the Presentation Layer - Contains
	a) Contracts - requests/responses
	a) Endpoints - API Mapping (using Minimal API's - could be controller endpoints)
- `Waterloo.Application` - Currently only contains Command and Handler and Validator, but could contain
	a) Commands - Commands to perform actions
	b) Queries - Queries to get data
	c) Handlers - Command and Query Handlers
	d) Validators - Validation for Commands and Queries
	e) Pipeline Behaviours - Cross cutting concerns
- `Waterloo.Domain` - Currently anaemic, but could contain
	a) Entities - Business Objects
	b) Value Objects - Objects that have no identity
	c) Services - Business Logic`
- `Waterloo.Infrastructure` - Currently only contains infra to access Google, but could be used to genericise all HTTP requests (scrapes). Other things here would include..
	a) Data - Data Access Layer
	b) Services - External Services
	c) Logging - Logging
	d) Messaging - Messaging
	e) Caching - Caching`
- `Waterloo.SharedKernel` - Contains shared code across the application
	a) Constants - Constants
	b) Exceptions - Exceptions
	c) Extensions - Extensions
	d) Interfaces - Interfaces
	e) Utilities - Utilities
	
Each project is responsible for it's own dependencies/registrations and has a clear separation of concerns.

## Comments
- In my view this is over-engineered for what it does, but it is more to demonstrate project structures and how you can use commands, queries, pipeline behaviours etc to keep your responsibilities segregated.
- I guess it makes a reasonable foundation if it were to be extended.

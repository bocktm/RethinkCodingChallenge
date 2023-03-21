# Rethink Coding Challenge

Please create a responsive web application that will have an upload view that accepts CSV files with 
patient records as input (see CSV sample file attached), and a view that displays the patients already 
uploaded to the application with basic ordering and filtering by patient name. Once uploaded, each 
patient record should be editable.
The app must have the following:
- Backend Web API written in .NET 6 C#.
- Frontend written in Angular 12 or higher.
- Data uploaded can be stored in SQL and should not be volatile (survive a restart).

Your app will be graded on best practices in coding, testing, validation techniques, architecture, UX and UI

## Personal Comments

First off, thanks for the opportunity to take this challenge!

I took a little gamble with trying to learn Angular Material. While I was able to get most of what I wanted done, I found myself spending too much time trying to make their table sort and paging work with the Web API backend. So while there is a Web API controller action that takes paging and sorting as parameters, I wasn't able to wire it successfully to the Angular app. I'm personally interested in figuring that out, but in the interst of time, I'm submitting this challenge with that concession.

To run the app, there are only two changes that are needed.
- In the Web API appsettings.json, change the PatientsDBContext connection string to point to the MDF location (included in this repo) locally.
- In the Angular Web App proxy.config.ts, change the port to that of your WebAPI when you run locally.
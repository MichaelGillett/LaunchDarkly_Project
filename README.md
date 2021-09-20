# LaunchDarkly Project

## Code

This project is using .NET 5. It's a web app that can be run from Visual Studio or from the command line using "_dotnet run_" in _\LaunchDarkly_Project\LaunchDarkly Project\LaunchDarkly Project_.

There are 3 files where code was added to the default web app template:

- Startup.cs
- Pages/Index.cshtml
- Pages/Index.cshtml.cs

The _Startup.cs_ file contains a singleton implementation of the LdClient that is available through out the app. This is on line 29. The "_YOUR_SDK_KEY_" value needs to be changed to an SDK key for a project/environment within your LaunchDarkly account.

The _Pages/Index.cshtml_ file contains the HTML that is rendered. An _if_ statement was added to encapsulate the default template implementation and new HTML.

The _Pages/Index.cshtml.cs_ files contains the logic to create the User object and identify which Boolean Variation to serve to a user for the _my-first-feature-flag_ feature flag. There are two mechanisms to create the User object within this file, the simpler one is commented out.

## App

The app is designed to show a different message when the feature flag is enabled or disabled. It is a simple example of a feature flag. However, when it comes the User there is more logic added to ensure that a user can be consistently identified. As there is no account management system within this simple application a cookie is set with a GUID value. This GUID value is used as the Key for the User object. If a request to the Index page is made and a cookie already exists then the GUID value of that cookie is used for the creation of the User object, otherwise a new GUID is generated and saved as a cookie for the next time that page is requested.

Clearing the cookie called "__ldUserCookie_" will allow the application to treat the request as a new one and possible change the served value if a percentage is being used. If this you want to run this application so that each request is treated as a new user then line 27 of _Pages/Index.cshtml.cs_ can be commented out. This created a new GUID for every request and does not check for any cookies.

The if statement on the _Index.cshtml_ file could be added to the _Index.cshtml.cs_ file in a future version of this application, but for now I put it on the presentation layer. This was because the variation from the feature flag changes both the HTML and the title of the page which are controlled in the presentation file.

## Feature Flag

The feature flag used in this application has the key "_my-first-feature-flag_". It can be used to turn the new implementation on or off for all requests, or targeting can be used to conditionally turn on the feature for select users.
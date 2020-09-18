# UC Davis Aid Estimator Example

This example application was built by UC Davis to demonstrate how the aid estimator
could be used within an ASP.NET Core MVC application.

This application also features a number of usability enhancements, including inline, client-side
validation and dynamic toggling of inputs based on user's responses.

## Running the Application

To run the example application, ensure that you have **ASP.NET Core 3.1.8** installed.

Open up the **UCD.AidEstimator.sln** solution **"As Administrator"** and choose the
**UCD.AidEstimator** project as the "Startup Project". Use **IIS Express** to run and
debug the application.

## Customizing the Application

To customize this application for use by your institution, take the following steps:

1. Find and replace "PLACEHOLDER" text
2. Find and replace "OURSCHOOL" text
3. Add your school's logo in `\wwwroot\images` and update the img tag in `\Views\Shared\_BasicLayout.cshtml` to match
4. Update `GetGrantAmount()` with your school's grant formula
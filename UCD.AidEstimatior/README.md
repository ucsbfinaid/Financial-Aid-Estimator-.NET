# UCD MVC Version

MVC configuration notes.

1. Be sure you have Asp.Net Core 2.1.6 install. If you want to use a later version, update the NuGet packages to match

2. After opening the UCD.AidEstimator.sln solution mark the UCD.AidEstimator project as "Startup Project"

3. Search for and replace "PLACEHOLDER" text

4. the project is currently set up to use https://localhost for testing and debugging. To use localhost with IIS Express you may need to open Visual Studio "As Administrator". To switch to a different local domain adjust your \Properties\launchSettings.json file.

5. Add your icon in \wwwroot\images and update the img tag in \Views\Shared\_BasicLayout.cshtml to match

6. Search for and update the GetGrantAmount() method with your grant calculation process

7. Run locally via Debug --> Start Without Debugging

8. Example MSBuild arguments (for IIS Web App Deployment - using a zip file):  /p:DeployOnBuild=true /p:WebPublishMethod=Package /p:PackageAsSingleFile=true /p:SkipInvalidConfigurations=true /p:DesktopBuildPackageLocation="SOMELOCATION\WebApp.zip" /p:DeployIisAppPath="MY.DOMAIN.EDU/MYPATH"
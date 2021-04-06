# TopAct Readme

## Repository
[Description of the link](https://github.com/imgen/TopAct "TopAct")

## How to run
First for `HTTPS` to work, might need to trust the root certificate. Run below command

	dotnet dev-certs https -v

Secondly this project requires the latest editon of VS 2019 and .NET 5 SDK

Open `TopAct.sln` in VS and press `F5` or `Ctrl + F5`. It will launch `Swagger UI` page. Click `Authorize` button, in the popup dialog, don't forget to tick the `api` scope checkbox. Then just click the `Authorize` button at the bottom left, it should succeed. Then click `Close` button to close the popup. 
After authorization, just play with the UI using `Swagger UI`. 

## Configuration
Not much, just one. In `TopAct.WebApi/appsettings.json`, there is a configuration entry `DbFilePath`

## Sequence diagram
See `Deliverables/Sequence diagram.pdf`

## Schema
See `Deliverables/Schema.json`

## Architecture

	Domain Drive Design
	
## How long?
 Research DDD and other libraries to use: about 1 hour
 Authentication/Authorization: 3 hours. Hit two brick walls with `IdentityServer4` and `Swagger UI` integration with `IdentityServer4`. Each took me about 1 hour to fix. 
 Learning DDD: 1 ~ 2 hours
 Learning Blazor + Syncfusion controls suite: 2 hours
 Coding: around 10 hours
 Total: 17 ~ 18 hours
 
 ## Unit tests?
 Almost none. I have a test called `AuthenticationTests` which does test the authentication part. But that requires the project running. It just showcases I know how to write unit tests. 
 
 But the whole project is done in way that I think is quite testable. 
 
 I am simply out of time. 
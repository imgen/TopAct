# TopAct Readme

## Repository
[TopAct on Github](https://github.com/imgen/TopAct)

## How to run
First, for `HTTPS` to work, might need to trust the root certificate. Run below command

	dotnet dev-certs https -v

Secondly this project requires the latest editon of `VS 2019` and `.NET 5 SDK`

Open `TopAct.sln` in VS and press <kbd>F5</kbd> or <kbd>Ctrl</kbd> + <kbd>F5</kbd>. 

It will launch `Swagger UI` page. Click <kbd>Authorize</kbd> button, in the popup dialog, don't forget to tick the `api` scope checkbox. Then just click the  <kbd>Authorize</kbd> button at the bottom left, it should retrieve the authorization token from `IdentityServer4`. Then click <kbd>Close</kbd> button to close the popup. 
After authorization, just play with the UI using `Swagger UI`. 

## Configuration
Not much, just one. In [TopAct.WebApi/appsettings.json](https://github.com/imgen/TopAct/blob/master/TopAct.WebApi/appsettings.json), there is a configuration entry `DbFilePath`

## Sequence diagram
See [Deliverables/Sequence diagram.pdf](https://github.com/imgen/TopAct/blob/master/Deliverables/Sequence%20diagram.pdf)

## Schema
See [Deliverables/Schema.json](https://github.com/imgen/TopAct/blob/master/Deliverables/schema.json) which is the JSON Schema of the `Contact` models

To view it visually, one can use this awesome [Online Schema Viewer](https://navneethg.github.io/jsonschemaviewer/). *Remember*: need to click the big blue dots to expand the model trees

## Architecture

	Domain Drive Design
	
## How long it took me?
 Research `DDD` and other libraries to use: about 1 hour
 
 Authentication/Authorization: 3 hours. Hit two brick wall: one with `IdentityServer4` and another with `Swagger UI` integration with `IdentityServer4`. Each took me about 1 hour to fix.
 
 Learning the `DDD` way: 1 ~ 2 hours
 
 Learning Blazor + Syncfusion controls suite: 2 hours
 
 Coding: around 10 hours
 
 Total: 17 ~ 18 hours
 
 ## Unit tests?
 Almost none. I have a test called `AuthenticationTests` which does test the authentication part. But that requires the project running. It just showcases I know how to write unit tests. 
 
 But the whole project is done in way that I think is quite testable. :(. I ran out of time. 
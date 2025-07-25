# Image Processor WebApi

Process an image with .NET core WebApi utilizing Opencv through C++/Cli module

## Project Architecture

This is a Multi layered application which consist of:
  - WebApi in .NET core -> Listens on a port for User interaction, receives image through FormFile sends it to the Application layer
  - Application project library in .NET 8 -> Validates file, according to FlunetValidation rules, sends the validated file as byte array to C++/Cli module
  - C++/Cli module -> Applies parallel Gaussian Blur on the received byte array using omp's parallel for which utilizez the all the avalaible threads in the CPU, returns it to Application layer, which in turn returns it to the client

## Prerequisites

### Visual Studio 2022 Community version download
### .NET 8 SDK download
### In the Visual Studio Installer:

![alt text](https://github.com/sabotamas0/ImageProcessor_WebApi/raw/main/ImageProcessor_WebApi/assets/projectWorkloads.PNG "Workloads")

### These project types, should by ticked, along with C++/Cli support: 

![alt text](https://github.com/sabotamas0/ImageProcessor_WebApi/raw/main/ImageProcessor_WebApi/assets/cliSupport.PNG "Cli support")

Note: Due of the nature of the C++/Cli module, this solution only works in windows currently.

## Run the application

The project supports both Release, and Debug mode out of the box on platform x64, loads the neccessary OpenCv dll-s, according to current configuration. These dll-s, and lib-s come from nuget package manager, and included throughout the project.


### Modes

On Debug mode, according to the configuration condition:

![alt text](https://github.com/sabotamas0/ImageProcessor_WebApi/raw/main/ImageProcessor_WebApi/assets/DebugConditional.PNG "Debug Condition")

The following dll-s will be transferred for use, alongside of the project various dll-s.

![alt text](https://github.com/sabotamas0/ImageProcessor_WebApi/raw/main/ImageProcessor_WebApi/assets/TransferredDlls.PNG "Transferred Dlls")

These apply to the Release mode aswell.

### Starting the application

Upon clicking the Start button:

![alt text](https://github.com/sabotamas0/ImageProcessor_WebApi/raw/main/ImageProcessor_WebApi/assets/StartApp.PNG "Start App")

We get the following Swagger User Interface:

![alt text](https://github.com/sabotamas0/ImageProcessor_WebApi/raw/main/ImageProcessor_WebApi/assets/swaggerUi.PNG "Swagger")

Here we can use the process_image endpoint, by uploading a file. This file will be processed, as described in the Project Architecture section.

![alt text](https://github.com/sabotamas0/ImageProcessor_WebApi/raw/main/ImageProcessor_WebApi/assets/UploadPhoto.PNG "Upload Photo")


## Behaviours

The Api currently supports image files, .png and .jpg to be exact. Here's a valid Request, where the user sends a .png file:

![alt text](https://github.com/sabotamas0/ImageProcessor_WebApi/raw/main/ImageProcessor_WebApi/assets/validRequestYellowBird.PNG "Valid request yellow bird after")

And this is the before image:

![alt text](https://github.com/sabotamas0/ImageProcessor_WebApi/raw/main/ImageProcessor_WebApi/SamplePictures/yellowishBird.PNG "Valid request yellow bird before")

Here's a valid .jpg Request:

![alt text](https://github.com/sabotamas0/ImageProcessor_WebApi/raw/main/ImageProcessor_WebApi/assets/validRequestRedBird.PNG "Valid request red bird after")

Before the Request:

![alt text](https://github.com/sabotamas0/ImageProcessor_WebApi/raw/main/ImageProcessor_WebApi/SamplePictures/redBird.jpg "Valid request red bird before")

The program validation denies unsupported file extensions and too large files like so:

![alt text](https://github.com/sabotamas0/ImageProcessor_WebApi/raw/main/ImageProcessor_WebApi/assets/invalidRequest.PNG "Invalid request large and usupported file")

Kestrel and FluentValidation ensures this, by defining validation rules:

![alt text](https://github.com/sabotamas0/ImageProcessor_WebApi/raw/main/ImageProcessor_WebApi/assets/fluentValidationRuleSet.PNG "Fluent validation ruleset")

And in the program files, the Kestrel configuration, protects the api from too large files, which otherwise would cause a false CORS exception. The real reason is, that the server can't receive a file with over certain amount of size, instead it return a CORS error.

With Kestrel configured we get the following response:

![alt text](https://github.com/sabotamas0/ImageProcessor_WebApi/raw/main/ImageProcessor_WebApi/assets/invalidRequest.PNG "Invalid request large and usupported file")

Here, the file size is 178MB, which without the configuration, would cause the CORS error:

![alt text](https://github.com/sabotamas0/ImageProcessor_WebApi/raw/main/ImageProcessor_WebApi/assets/corsError.PNG "Cors error")

## Future development
	
	- Add linux dockerization support, this would include replacing the C++/Cli module, and call the methods through pinvoke, add CI job, to automatically deploy the new image to my docker hub account
	- Add file upload with Base64 string, this would include converting the Base64 string to byte array, determine the Base64 string size, extend the validation according to these changes

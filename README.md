# Image Processor WebApi

Process an image with .NET core WebApi utilizing Opencv through C++/Cli module

## Project Architecture

This is a Multi layered application which consist of:
  - WebApi in .NET core -> Listens on a port for User interaction, receives image through FormFile sends it to the Application layer
  - Application project library in .NET 8 -> Validates file, according to FlunetValidation rules, sends the validated file as byte array to C++/Cli module
  - C++/Cli module -> Applies parallel Gaussian Blur on the received byte array, returns it to Application layer, which in turn returns it to the client

## Prerequisites

### .NET 8 SDK
Note: Due of the nature of the C++/Cli module, this solution only works in windows currently.

## Run the application

The project supports both release, and Debug mode out of the box on platform x64, loads the neccessary OpenCv dll-s, according to current configuration. These dll-s, and lib-s come from nuget package manager, and included throughout the project.

### Debug Mode

On Debug mode, according to the configuration condition:
![alt text](https://github.com/sabotamas0/ImageProcessor_WebApi/raw/main/ImageProcessor_WebApi/assets/DebugConditional.PNG "Debug Condition")


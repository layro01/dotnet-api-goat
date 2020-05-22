# dotnet-api-goat
A simple ASP.NET WebAPI 2.0 REST application that exposes endpoints with code that contains vulnerabilities.

## Introduction
Welcome to .NET API Goat a sample .NET-based REST API application that shows a number of OWASP Top 10 vulnerabilities. 

It is intended to be a sample that shows how Veracode Interactive Analysis can be used to detect those vulnerabilities as the code executes, usually as the tests execute as part of a CI/CD pipeline.

This sample API uses the ASP.NET Web API 2 framework to implement the API and the VS Test for the unit testing framework. For more information on these technologies, please refer to the following Microsoft tutorials:

- [Get Started with ASP.NET Web API 2 (C#)](https://docs.microsoft.com/en-us/aspnet/web-api/overview/getting-started-with-aspnet-web-api/tutorial-your-first-web-api)
- [Unit Testing ASP.NET Web API 2](https://docs.microsoft.com/en-us/aspnet/web-api/overview/testing-and-debugging/unit-testing-with-aspnet-web-api).

## Prerequisites
You will need to have the following in order to use this sample:

- Windows 10
- Visual Studio 2019 (with ASP.NET and Web Tools installed). 

Note that you can use other versions of Visual Studio, but you will need to update the sourcing of the Visual Studio Developer command prompt environment in the files `test.bat` and `test_with_iast.bat` to the correct batch file for your version.

## Setup
To use this sample, do the following:

1. Clone the `dotnet-api-goat` repo onto your machine and make sure that the tests pass without IAST first:

```dos
git clone https://github.com/layro01/dotnet-api-goat.git
cd dotnet-api-goat
test.bat
```
You should see that 4 tests ran successfully.

2. To run the tests with IAST enabled, do the following:
   * Edit the batch file and check the value of `IASTAGENT_PATH`.
   * Run `test_with_iast.bat`.
   
Once this completes, you should see a file called `dotnet-api-goat-info.log` created in the root directory of the project containing the IAST results associated with the test run.

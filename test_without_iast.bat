@ECHO OFF

REM Set a PROJECT_NAME variable based on the solution name.
For %%a IN (*.sln) DO Set PROJECT_NAME=%%~na

REM Source the Visual Studio developer command line environment, if not already present.
IF NOT DEFINED DevEnvDir (
    CALL "C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\Common7\Tools\VsDevCmd.bat"
)

REM Build the solution
MSBuild %PROJECT_NAME%.sln

REM Run the tests associated with the solution.
vstest.console.exe .\%PROJECT_NAME%.Tests\bin\Debug\%PROJECT_NAME%.Tests.dll
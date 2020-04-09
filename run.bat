@ECHO OFF
SETLOCAL

REM Set the defaults for IIS.
SET IIS_PATH=C:\Program Files (x86)\IIS Express\iisexpress.exe

REM Find the current directory.
for /f %%i in ('git rev-parse --show-toplevel') do set ROOT_DIR=%%i

REM Set a PROJECT_NAME variable based on the solution name.
FOR %%a IN (*.sln) DO SET PROJECT_NAME=%%~na

REM Source the Visual Studio developer command line environment, if not already present.
IF NOT DEFINED DevEnvDir (
    CALL "C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\Common7\Tools\VsDevCmd.bat"
)

REM Build the solution
MSBuild %PROJECT_NAME%.sln

REM Host the ASP.NET application in the IIS web server.
START "IIS Server" "%IIS_PATH%" /config:"%ROOT_DIR%\.vs\dotnet-api-goat\config\applicationhost.config" /site:dotnet-api-goat /apppool:Clr4IntegratedAppPool

REM Launch a browser on one of the endpoints.
start "" https://localhost:44381/api/goats/command?command=dir
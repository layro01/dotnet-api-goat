@ECHO OFF
SETLOCAL

REM Set the path to the IAST Agent.
SET IASTAGENT_PATH=c:\iast\iast-dev\out\agent\Debug\dotnet

REM Set the defaults for IIS.
SET IIS_PATH=C:\Program Files (x86)\IIS Express\iisexpress.exe

REM Set a PROJECT_NAME variable based on the solution name.
For %%a IN (*.sln) DO Set PROJECT_NAME=%%~na

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

REM Set additional IAST Agent settings.
SET IASTAGENT_LOGGING_STDERR_LEVEL=info
SET IASTAGENT_LOGGING_FILE_ENABLED=true
SET IASTAGENT_LOGGING_FILE_LEVEL=info
SET IASTAGENT_LOGGING_FILE_PATHNAME=.\%PROJECT_NAME%-%IASTAGENT_LOGGING_STDERR_LEVEL%.log

REM Enable IAST profiling.
SET COR_ENABLE_PROFILING=1
SET COR_PROFILER={90747D54-A553-4353-8E39-CA9ADE930151}
SET COR_PROFILER_PATH=%IASTAGENT_PATH%\agent_dotnet_win32.dll

REM Host the ASP.NET application in the IIS web server.
START "IIS Server" "%IIS_PATH%" /config:"%ROOT_DIR%\.vs\dotnet-api-goat\config\applicationhost.config" /site:dotnet-api-goat /apppool:Clr4IntegratedAppPool

SET COR_ENABLE_PROFILING=0

REM Launch a browser on one of the endpoints.
start "" https://localhost:44381/api/goats/command?command=dir
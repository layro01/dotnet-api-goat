@ECHO OFF

REM Set a PROJECT_NAME variable based on the solution name.
For %%a IN (*.sln) DO Set PROJECT_NAME=%%~na

REM Set the path to the IAST Agent.
SET IASTAGENT_PATH=c:\iast\iast-dev\out\agent\Debug\dotnet

REM Source the Visual Studio command line environment, if not already present.
IF NOT DEFINED DevEnvDir (
    CALL "C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\Common7\Tools\VsDevCmd.bat"
)

REM Build the solution
MSBuild %PROJECT_NAME%.sln

REM Enable IAST profiling.
SET COR_ENABLE_PROFILING=1
SET COR_PROFILER={90747D54-A553-4353-8E39-CA9ADE930151}
SET COR_PROFILER_PATH=%IASTAGENT_PATH%\agent_dotnet_win32.dll

REM Set additional IAST Agent settings.
SET IASTAGENT_LOGGING_STDERR_LEVEL=info
SET IASTAGENT_LOGGING_FILE_ENABLED=true
SET IASTAGENT_LOGGING_FILE_LEVEL=info
SET IASTAGENT_LOGGING_FILE_PATHNAME=.\%PROJECT_NAME%-%IASTAGENT_LOGGING_STDERR_LEVEL%.log

REM Run the tests associated with the solution.
vstest.console.exe .\%PROJECT_NAME%.Tests\bin\Debug\%PROJECT_NAME%.Tests.dll

REM Disable IAST profiling.
SET COR_ENABLE_PROFILING=0
@echo off

REM Navigate to the front-end folder
cd hair-saloon-fe

REM Install npm dependencies if node_modules is missing
if not exist "node_modules" (
    echo Installing npm dependencies...
    npm install
)

REM Start the front-end
echo Starting the front-end...
start cmd /k "npm start"

REM Navigate to the back-end folder
cd ..\HairSaloonApp\HairSaloonAPI

REM Start the back-end
echo Starting the back-end...
start cmd /k "dotnet run"

REM Return to the root folder
cd ..
pause

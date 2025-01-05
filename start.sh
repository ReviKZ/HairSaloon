#!/bin/bash

# Navigate to the front-end folder
cd hair-saloon-fe

# Install npm dependencies if node_modules is missing
if [ ! -d "node_modules" ]; then
    echo "Installing npm dependencies..."
    npm install
fi

# Start the front-end
echo "Starting the front-end..."
npm start &
FE_PID=$!

# Navigate to the back-end folder
cd ../HairSaloonApp/HairSaloonAPI

# Start the back-end
echo "Starting the back-end..."
dotnet run &
BE_PID=$!

# Wait for both processes to finish
wait $FE_PID $BE_PID

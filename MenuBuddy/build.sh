#!/bin/bash

# Get library name from use the directory name
LIBRARY_NAME=$(basename "$(pwd)")

echo "Building package: $LIBRARY_NAME"

# Clean up old packages
echo "Cleaning up old packages..."
rm -f *.nupkg .snupkg

# Use dotnet clean to properly clean build artifacts
echo "Cleaning project..."
dotnet clean --configuration Release

# Force delete all contents in bin/Release
echo "Cleaning bin/Release folder..."
rm -rf bin/Release/*

# Build and pack
echo "Building and packing..."
dotnet build --configuration Release

dotnet pack --configuration Release --no-build

# Copy to local NuGet folder
echo "Copying to local NuGet repository..."
cp ./bin/Release/*.nupkg ~/Documents/Source/Nugets
cp ./bin/Release/*.snupkg ~/Documents/Source/Nugets

# Upload to NuGet.org
echo "Triggering publish workflow..."
gh workflow run publish.yml

echo "Package published successfully!"
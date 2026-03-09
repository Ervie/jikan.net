#!/usr/bin/env bash
set -e

# Resolve repo root (directory where this script lives)
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

PROJECT_PATH="JikanDotNet/JikanDotNet.csproj"
NUGET_SOURCE="https://api.nuget.org/v3/index.json"

if [[ ! -f "$PROJECT_PATH" ]]; then
  echo "Error: Project file not found at $PROJECT_PATH"
  exit 1
fi

# Extract package version from .csproj (PackageVersion for NuGet, fallback to Version)
VERSION=$(grep '<PackageVersion>' "$PROJECT_PATH" | sed -n 's/.*<PackageVersion>\([^<]*\)<\/PackageVersion>.*/\1/p' | tr -d ' \t')
if [[ -z "$VERSION" ]]; then
  VERSION=$(grep '<Version>' "$PROJECT_PATH" | head -1 | sed -n 's/.*<Version>\([^<]*\)<\/Version>.*/\1/p' | tr -d ' \t')
fi
if [[ -z "$VERSION" ]]; then
  echo "Error: Could not determine package version from $PROJECT_PATH"
  exit 1
fi

PACKAGE_ID="JikanDotNet"
NUPKG_NAME="${PACKAGE_ID}.${VERSION}.nupkg"
# Pack outputs to bin/Release by default
NUPKG_PATH="JikanDotNet/bin/Release/${NUPKG_NAME}"

if [[ -z "${NUGET_API_KEY:-}" ]]; then
  echo "Error: NUGET_API_KEY environment variable is not set."
  echo "Set it with: export NUGET_API_KEY=your_api_key"
  echo "Get your API key from https://www.nuget.org/account/apikeys"
  exit 1
fi

echo "-------------------------------------------"
echo "NuGet publish: $PACKAGE_ID"
echo "Version:       $VERSION"
echo "Package:       $NUPKG_NAME"
echo "Source:        $NUGET_SOURCE"
echo "-------------------------------------------"
echo ""
read -r -p "Confirm deployment of the above version? [y/N] " response
if [[ ! "$response" =~ ^[yY]$ ]]; then
  echo "Aborted."
  exit 0
fi

echo "Packing..."
dotnet pack "$PROJECT_PATH" -c Release

if [[ ! -f "$NUPKG_PATH" ]]; then
  echo "Error: Pack succeeded but $NUPKG_PATH was not found."
  exit 1
fi

echo "Pushing to NuGet..."
dotnet nuget push "$NUPKG_PATH" --source "$NUGET_SOURCE" --api-key "$NUGET_API_KEY"

echo "Done. $PACKAGE_ID $VERSION has been pushed to NuGet."

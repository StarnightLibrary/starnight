@echo off

echo Preparing native tooling build: extracting dotnet runtime identifier...

if %PROCESSOR_ARCHITECTURE%==AMD64 (
	set rid=win-x64
	goto :ridknown
) else (
	if %PROCESSOR_ARCHITECTURE%==ARM64 (
		set rid=win-arm64
		goto :ridknown
	) else (
		echo Unknown/unsupported CPU architecture. Aborting.
		exit 1
	)
)

echo Extracted dotnet runtime identifier: %rid%

:ridknown

:: dotnet publish tools\generators\ToolName\ToolName.csproj -r %rid% -c Release
:: copy tools\generators\ToolName\bin\Release\net7.0\%rid%\native\ToolName.exe tool-name.exe
:: echo Successfully built and prepared tool tool-name for use.

dotnet publish tools\generators\Starnight.Generators.GenerateInternalEvents\Starnight.Generators.GenerateInternalEvents.csproj -r %rid% -c Release || exit 1
copy tools\generators\Starnight.Generators.GenerateInternalEvents\bin\Release\net7.0\%rid%\native\Starnight.Generators.GenerateInternalEvents.exe generate-internal-events.exe || exit 1
echo Successfully built and prepared tool generate-internal-events for use.

echo Successfully built and prepared native tooling for Starnight development.

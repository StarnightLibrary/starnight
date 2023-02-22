@echo off

for %%x in (%*) do (

	if "%%x"=="shared" (
		dotnet pack -o build -c Release src/Starnight.Shared --include-source --include-symbols -p:TreatWarningsAsErrors=false
	)

	if "%%x"=="caching-providers" (
		dotnet pack -o build -c Release src/Starnight.Caching.Providers --include-source --include-symbols -p:TreatWarningsAsErrors=false
	)

	if "%%x"=="internal" (
		dotnet pack -o build -c Release src/Starnight.Internal --include-source --include-symbols -p:TreatWarningsAsErrors=false
	)

	if "%%x"=="caching" (
		dotnet pack -o build -c Release src/Starnight.Caching --include-source --include-symbols -p:TreatWarningsAsErrors=false
	)

	if "%%x"=="main" (
		dotnet pack -o build -c Release src/Starnight --include-source --include-symbols -p:TreatWarningsAsErrors=false
	)
)

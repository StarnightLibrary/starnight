echo Preparing native tooling build: extracting dotnet runtime identifier...

uname_output=$(uname -m)

musl=$(ldd /bin/ls | grep 'musl' | head -1 | cut -d ' ' -f1)

case $uname_output in

	x86_64)
		if [ -z $musl ]; then
			rid="linux-x64"
		else
			rid="linux-musl-x64"
		fi
		;;

	aarch64 | armv8l)
		if [ -z $musl ]; then
			rid="linux-arm64"
		else
			rid="linux-musl-arm64"
		fi
		;;

	*)
		echo Unknown/unsupported CPU architecture. Aborting.
		exit 1
		;;
esac

echo Extracted dotnet runtime identifier: $rid


# dotnet publish ./tools/generators/ToolName/ToolName.csproj -r $rid -c Release
# cp ./tools/generators/ToolName/bin/Release/net7.0/$rid/ToolName ./tool-name
# echo Successfully built and prepared tool tool-name for use.

dotnet publish ./tools/generators/Starnight.Generators.GenerateInternalEvents/Starnight.Generators.GenerateInternalEvents.csproj \
	-r $rid -c Release

cp ./tools/generators/Starnight.Generators.GenerateInternalEvents/bin/Release/net7.0/$rid/Starnight.Generators.GenerateInternalEvents \
	./generate-internal-events

echo Successfully built and prepared tool generate-internal-events for use.

echo Successfully built and prepared native tooling for Starnight development.

# Contributing to the Starnight Library

It's awesome to know you wish to contribute to the Starnight project! Before you do so though, please give these guidelines a quick read.

### Licensing

Starnight is licensed under the MIT license, and by contributing you agree to license your contributions under the [license](./LICENSE).

### Code Style

Try to follow the guidelines set out in the [styleguide](./styleguide.md). Also, don't explicitly try to make your code unreadable.

### Internal Tooling

Starnight employs internal code generators for certain tasks. They are located in `./tools/generators`, written in C# using the AOT compiler. To use them, run the `build-tools` script for your OS (the .cmd script assumes windows, the .sh script assumes linux); which will place the generators in your project root.

Since they use the C# AOT compiler, using them follows the same prerequisites as any other AOT project, and you will have to have the necessary dependencies installed.

Adding new such tools is encouraged if they are
- intended to run rarely
- operable with relatively limited information and do not need to parse type information.

AOT generator information should be placed in `./data`.

Any other use-case should be covered by roslyn source generators, located in `./gen`.

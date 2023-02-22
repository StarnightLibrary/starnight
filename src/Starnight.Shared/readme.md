# The Starnight Shared Type Library

This contains types used by Starnight.Internal that need to be exposed in Starnight.

This needs to exist because type-forwards don't actually expose the type if the dependency is included as a private asset. Don't include
this directly, ever.

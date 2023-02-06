# The Starnight Shim Source Generator

The shim source generator is located in `/gen/Shims` and exists to automatically generate redirects for shims that don't cover every
single method. In its current state, it is very rudimentary and performs no validity checking, thus, it is advised to avoid using it
outside of `/src/Starnight.Caching/Shims` for now.

It currently operates by taking all partial methods on a type and implementing them through a field of the name `underlying`. Since
the emitted `ShimAttribute<TInterface>` holds the interface the shim is built over, in the future, it should be updated to utilize this
type information, in order to only shim methods fulfilling the following criteria:

- The method is present on the specified interface type.
- The method is not yet implemented in another partial file (partiality is handled rather interestingly by source generation).
- There is an object of the specified interface type that the shim can be implemented through.

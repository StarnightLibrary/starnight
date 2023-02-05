# The Starnight Caching Source Generator

The caching source generator is located in /gen/Caching and tasked with generating cache updating methods. Because Discord doesn't always
send a full object, we can't just blanket overwrite cache with the new object (this especially goes for rest calls in `src/Starnight.Caching/Shims`,
gateway events tend to be more or less complete), instead, we generate methods to copy everything we got and keeping data we didn't get but
had already cached locally.

Because our entities are `record`s, plainly copying everything over would be rather painful - records are editable with `with` expressions,
and every `with` expression allocates a new object; which we want to avoid. Therefore, the generator emits a `file record struct` mirroring
all properties on the entity type and does the copying work there, without incurring allocations.

The generator currently is only utilized for updating an object with an object of the same type, but it already mostly supports use for
partial entities, should they ever be implemented in the library (which is fairly likely, but not a given). It only remains to correctly
extract partial entity metadata and correctly emit two updating methods, one partial, one full, into the same output file.

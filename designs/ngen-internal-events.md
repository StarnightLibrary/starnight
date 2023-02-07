# The Internal Event Deserialization generator

The internal event deserialization generator is tasked with generating the deserializer for dispatch events. It is, more specifically,
used to generate `/src/Starnight.Internal/Gateway/Events/Inbound/Dispatch/EventDeserializer.cs` from `/data/events-internal.json`,
according to the steps laid out in [the rudimentary native generator documentation](../CONTRIBUTING.md#internal-tooling).

This scenario is considered a good use-case for the generator because events change very rarely, and thus, not regenerating the
deserializer on every build is perfectly warranted - in fact, doing so is a waste of time and resources.

# Title

Use .Net ASpire to streamline cloud native production ready app development.

# Status
Accepted

# Context

To build cloud native production ready applications, we need
* Observability
* Databases
* Caching
* Messaging
etc.
We will use the containerization approach to build and host our application. To this effect there are two ways we can achieve
1. Use docker compose and wire everything by bespoke configuration
2. Use .Net  Aspire to get the application wired up with state of the art abstractions through .Nuget packages

# Decision

We will use the .Net Aspire to speed up the development as the configuration for the application is maintained as C# code and will prove easier to grasp for newly onboarded developers.

# Consequences

* There will be initial learning curve for the .Net Aspire
* Extra area for auditing and security analysis as .Net Aspire will bring in its own set of nuget packages.

# Compliance
* We will perform regular audits on the compliance of the solution

# Notes

**Author**: Mandar Dharmadhikari
**Date**: 16 March 2025

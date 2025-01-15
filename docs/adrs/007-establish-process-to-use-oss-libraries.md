# Title
0007-Establish a process to allow the use of third party and open source libraries in the code.

# Status
Accepted

# Context
It is necessary to establish a process to allow the use of Nuget packages in the project. Some of the overarching considerations are

* Security Vulnerabilities
* Maintenance history of the package/libraries
* Licensing model


# Decision
We will review each Nuget package based on identified concerns. We will review each package with a principle of `The lesser the number of external dependencies, the better it is.` Each Nuget package request will require a ADR to be added to the architecture decision log.

# Consequences

* We will have to spend time in scrutiny of the packages

# Compliance

* We will use dependabot to allow scanning of our packages for vulnerabilities
* We will periodically review the versions of the packages and consider updates as required.

# Notes
**Author**: Mandar Dharmadhikari

**Date**: 15 Jan 2025
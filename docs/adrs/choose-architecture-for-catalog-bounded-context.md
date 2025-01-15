# Title
- Use Simple CRUD Architecture for Catalog Management

# Status
Proposed

# Context

As per the identified use cases of the catalog management subdomain, we only need to perform addition of the books, determination of physical rack. There are various architecture approaches available
* Implement Clean Architecture with full controller based Web APIs
* Implement Clean Architecture with Minimal APIs
* Implement a simple CRUD API with Minimal API and Vertical Slice Architecture

# Decision

We will build a simple CRUD API with Vertical slice Architecture and use the Minimal APIs to expose a set of Web APIs. These APIs will be RESTful

# Consequences


# Compliance

* We will use the Architecture Test to enforce the architecture style for the module


# Notes

**Author**: Mandar Dharmadhikari

**Date**: 15 Jan 2025
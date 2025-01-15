# Title
0002- Use of Modular Monolith to build the Library Management System

# Status
Accepted

# Context

As we have identified that there are different bounded contexts that interact with each other. We have multiple options to structure the code for the Library Management System.

* Monolith : We can build a monolith with all the submodules being built and packaged as one. We can deploy the entire software as one

* Microservices: We can build separate services for each bounded context and then implement the context maps as integrations. We have to make the services individually deployable

* Modular Monolith: We can build different modules for the identified sub contexts and implement the context maps as integration. We can deploy the entire software as one package.

# Decision

We will use modular monolith specifically due to following reasons

* We can separate the sub contexts as modules. This provides a high cohesion between the module components and low coupling between the modules
* We can package the entire software as one deployable. This reduces the cost of the infrastructure at the beginning. This also requires us to set up simpler observability patterns as compared to full on microservices

# Consequences

* Each team will work on one sub context and its associated module
* Since we are deploying the entire solution as one package, we will not be able to scale each module separately.
* Each team will have to document the development style for their module using ADRs

# Compliance

* We will write architecture tests to ensure that we have low coupling between the modules and high cohesion inside the module boundary.
* We will periodically monitor the ADRs to ensure that each team documents their choice of architecture of their module.

# Notes

**Author**: Mandar Dharmadhikari

**Date**: 15 Jan 2025
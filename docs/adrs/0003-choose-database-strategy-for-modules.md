# Title
0003- Use one database with separate schema per module and restrict cross module table access

# Status
Accepted

# Context

There are different patterns that are available to us for database setup for the modular monolith

* **Single Databae and Single Schema for the application** : In this approach every module has access to the data of the other module. All the database entities are deployed to a single database and schema. The entire concept of modularity is lost here. Infrastructure wise this is cheaper as compared to the database per module approach.

* **Single Database  and Separate Schema per module**: In this approach the database remains the same for the entire application but there will be different schemas for the different modules. No module can query data from other module's schema entities directly. Infrastructure wise this is cheaper as compared to the database per module approach.

* **Different Databases** : In this approach each module will have a different database. No module can connect to the database of another module.

# Decision

To save infrastructure costs and at the same time maintain modularity and choesion at the database level, we will implement the `Single Database and Separate Schema per module` pattern

# Consequence

* We acknowledge that scaling for data needs of high load modules will not be possible.

# Compliance

* We will write architecture tests to ensure that the constraints are enforced in code
* We will also do periodic code reviews

# Notes
**Author**: Mandar Dharmadhikari

**Date**: 15 Jan 2025
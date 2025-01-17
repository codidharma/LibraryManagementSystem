# Title
Choose Keycloak as Identity and Access Management Solution for the Library Management System

# Status
Accepted

# Context
We need to identify the proper identity and access management solution for the Library Management System. We will evalute the available solutions in the market based on following checklist.

* Is it scalable?
* What is the pricing?
* What is the support available in case of issues?
* Supports single sign on?
* Supports multi factor authentication?
* Does it lock us to a cloud platform?

For the purposes of this project, we have following assumption of users
* Number of users: 10000
* Number of Librarians: 1
* Number of concurrent users: 1000

We will evaluate following solutions available on the market

* Auth0 by Okta
* Identity Server by Duende Software
* KeyCloak an Open Source Alternative

|Parameter|Auth0|Identity Server| KeyCloak|
|---------|-----|---------------|---------|
|Is it scalable?||||
|What is the pricing?||||
|Supports single sign on?||||
|Supports multi factor authentication?||||
|Does it lock us to a cloud platform?||||
|What is the support available in case of issues?||||


# Decision
We will use Keycloak due to pricing and scalability and good OSS support.

# Consequences
We will have to setup and run a Keycloak server which will require us to 
* Add extra observability
* Add extra efforts for operations
* Extra cost of running the server
* Extra area for auditing and security analysis

# Compliance

* We will perform regular audits on the compliance of the solution to industry best practices for the IAM solutions
* We will write automated fitness functions to monitor the health of the server

# Notes
**Author**: Mandar Dharmadhikari

**Date**: 17 Jan 2025
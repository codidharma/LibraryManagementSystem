# Title
0005-Use Sonar Analyzer for Static Code analyzer

# Status
Accepted

# Context

Static code analysis is very important to help maintain the quality of code that we produce.
Starting .Net 6, Microsoft provides the Rosalyn analyzers in built with support in Visual Studio. In addition to this there are other open source static analyzers available in the market.

# Decision

We will use SonarAnalyzer for CSharp

# Consequences

* We will have a dependency on the third party nuget package named `SonarAnalyzer.CSharp`. We agree that we will have to manage this dependency
* This dependency will be managed in the Build Props file for the project

# Compliance

* We will use dependabot alerts to manage the security vulnerabilities for this analyzer

# Notes
**Author**: Mandar Dharmadhikari

**Date**: 15 Jan 2025
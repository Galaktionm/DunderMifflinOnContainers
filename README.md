# DunderMifflinOnContainers

Microservices that communicate directly to front end use HTTP.

Microservice-to-microservice communication is done solely on gRPC.

The idea is from microsoft's sample microservice application - https://github.com/dotnet-architecture/eShopOnContainers. Trying to include as many functionality from the sample as possible

Cloning the repository and running "docker-compose up" should start the application. Only the back-end is dockerized. It can be tested with Postman, but Angular application "DunderMifflinFrontEnd" is also included in the repository. In order to connect to the localhost ports exposed by docker urls in URLGlobal.ts should be changed accordingly.

This is a sample application. Many features are still unimplemented. For example, there is no "logout" page in angular, and on registration we are giving users enough "free money" on their balance to test the functionality.

## General Outline

![DmDiagram](https://user-images.githubusercontent.com/106910530/208336796-bd72ce18-5d9d-4f64-8358-6bf2a76450e1.png)

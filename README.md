# Address Book

  Address book where users can view list of contacts, add and update a contact, built using ASP.NET Core and Angular. Address Book API exposes HTTP endpoints 
  through /contacts that internally use MediatR to handle requests and responses.

## Install & Run
   
 Clone master. Run the following commands, inside the application directory:
      
    \AddressBook.WebAPI:
      
          dotnet restore 
          
          run Update-Database from VS or dotnet ef database update from dotnet CLI (runs db under localhost\\SQLEXPRESS)          
          
          dotnet run
      
    \Angular 11\AddressBook:
     
          run npm install
          
          ng serve     
          
Navigate to https://localhost:44357/index.html to check the API Swagger documentation.

## Frameworks and Libraries
   
- Aps.Net core 3.1 - WEB API

- MediatR
  
- Entityframework (for Data access)
  
- SQL Server
 
- Swashbuckle (API documentation).
  
- Angular 12
  
- Angular material
  
## Architecture

Address Book WEB API is developed following CQRS pattern with MediatR. CQRS Command and Query Responsibility Segregation is a pattern used to separate the logic between commands and queries.
  
Followed CQRS, so you have the Single Responsibility Principle by design and you get the ability to design a loosely coupled architecture.

Used FluentValidation with pipeline behaviours for data validations. It helps you to keep validations seperate from domain model, easy to plug in/re-use validations 
in any models.

Address book front end is developed following MVVM patterns.

![architecture_diagram](https://user-images.githubusercontent.com/8025466/132505127-6fb31c3f-8d0c-4d89-9cd1-6cd3f425377b.png)



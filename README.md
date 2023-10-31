# LondonStockExchange.API

All in all a fun couple of hours spent on this...this just serves as a small demo and in no way reflects a production ready app...plenty of enchancements to be made! 

Is this system scalable? Not as it stands 

How can it cope with high traffic? Currently, not very well 

Can you identify bottlenecks and suggest an improved design and architecture? Yes and excited to go into more detail :D 

#To run this project:

create a local sql server database called 'LondonStockExchange' and update the connectionstring in the appsettings.json file in the main LondonStockExchange.API project 

e.g 

  "ConnectionStrings": {
    "LondonStockExchangeConnection": "Server=localhost;Database=LondonStockExchange;Trusted_Connection=True;MultipleActiveResultSets=true; TrustServerCertificate=True"
  },

  one connectioned, run:

  dotnet ef database update 

  once the relevant tables have been created, run the project and navigate to https://localhost:44326/swagger/index.html and excecute the /api/seedData endpoint to populate the stock table with some dummy data

  in order to use the Stock API endpoints you must be registered to the system:

  sign up via the  /api/Authentication/register endpoint, being sure to uses an alphanumeric password with upperand and lowercase letters and special characters....or just use Password91!

  once the account has been created, login using the /api/Authentication/login endpoint

  once successfully logged in, in the response body grab the 'token' value and authorize in swagger e.g "Bearer {token}"

  you are now authorised to use the Stock endpoints and monitor transactions

  
  

  

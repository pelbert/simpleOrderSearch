--Faraz start of comments
This has 2 programs
1. Webapi application OrderSearch
2. Console application TestOrderSearch

Instructions:
Please host the service OrderSearch on local web server
In TestOrderSearch, update the below parameter in App.config ot reflect the right port number
  <appSettings>
    <add key ="SearchURL" value="http://localhost:64628/api/OrderSearch?"/>
  </appSettings>

--Faraz End of comments---------
# simpleOrderSearch
simpleOrderSearch


I want to assess your ability to create an application and REST API service. It truly is the bare minimum of knowledge necessary to be successful in this position. I don't want you to spend a lot of time on this. You should be able to do this in an hour or so if the job is right for you.

Order Search

This programming task consists of building a simple console application to search for orders. Fork this repository and create your application. It should take this input from the user:

(Order Number || (MSA && Status)) && CompletionDte

The console application will call a service that you create using C#. I have provided some sample data for the application in the JSON file in the data folder.



The file contains an array whose elements represent orders. The data should be defined as a model in your service.

The application calling the service can be a console app. You have total freedom to do what you want but make sure it can do these three things:

• Validate that the user has provided the right criteria to make a search

• Provide an offset and page value for pagination.

• Write the outputs of the service call to a console window. 

Create a pull request once you have it working. I will clone your repository, verify that it works, and evaluate it. Please ensure you include any instructions for running that may be required. 

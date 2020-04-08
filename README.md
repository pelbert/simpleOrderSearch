This is my SimpleOrderSearch REST API service (and console client) attempt after ~4 hours of work. This was my first time creating a REST API service from scratch and my first ever ASP.net project, so the ~4 hours are both learning and doing.

The concepts seemed somewhat familiar/easy, like how routes work.  Fair amount of fumbling about for exact class/nuget-package to do what I want.

Also, the specification requiring an OrderID AND a CompletionDte seemed so
wrong that I implemented things as if valid search criteria are:
    1: OrderID, or...  
    2: MSA and Status and CompletionDte

I would change things the moment I become convinced that always requiring the
CompletionDte is a good/necessary thing.

To run my stuff, open each solution in Visual Studio 2019 and start debugging.

See `example_client_session.txt` for an example of a client session with both
searching by OrderID and searching by {MSA, Status, CompletionDte} with
pagination.

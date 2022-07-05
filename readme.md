# Build Instructions

Because the application uses LiteDB with an in-memory database, no special build instructions are required.

# Design Decisions

The architecture of this solution is based on Jimmy Bogard's sample implementation of Bob Martin's' "Clean Architecture".
The specifics of implementing that architecture vary, but this is a simple implementation that is applicable to a project of this size. 

Normally, on a project of this type, I like to use Ardalis Mediatr. In the interest of time, I skipped that.

I also prefer using a mapping solution, like AutoMapper, to map between objects. Due to time constraints, and the simplicity of the application, I also chose not to implement a mapper.

Other, more class-specific design decisions are noted in comments in the associated code.

In the interest of ease of packaging the solution, I chose to use LiteDB.

Rather than a generic implementation of the repository, there is instead only a Contact-specific, LiteDB-specific
implementation. In a more complex application, or when I was not using something like LiteDB, I would not have chosen this approach.

I would have preferred to use ValueObject types for some of the properties on contact, such as the phone number and email address, but time constraints prevented
addressing the serialization issues that causes when trying to match the required JSON output. In any non-trivial system, I think it is important to consider
first class treatment of properties that have known restrictions on them, and value types is one of the ways to do so.


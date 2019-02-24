# Intent
The project purpose is to define a set of reusable patterns to allow to mock calls to external services in order to be able to :
- launch end to end test in a mastered environment
- mock a service when it is broken and could take time to be up again (in all environment but production)
- mock a service on all environment but production (for long call time or money reason)

# Strategies
Mocks are use one by one in the order they are defined

The idea is to behave like NSubstitute but with a database between the client (which define the mock) and the service (which apply the defined strategy through a proxy)

We have 3 strategies:
- Method mock
- Object mock
- Force no mock (available if you want to not mock between two mocks)

All can have a context which can be used to :
  * filter on specific context (sessionId or anything else) to know when apply the strategy
  * help to do a calculation in a specific strategy

# Todo list
- Create a "mock always with" strategy and create a way to stop it
- Use a real database to save mock strategies
- Give example of Factory/Builder/... to build object before send it
- Give an example with parameter base strategy
- Give an example with multiple methods for the main class
- Not have to use method id. Look for NSubstitute like behavior with DynamicProxy like "myService.Get(Arg.Any()).Return(...)""
- Create a "mock only on error" strategy
- Example with async
- Add some analyse method to be sure that all call has been used and not more or less for end to end test purpose
- Create a view to allow mock on demand
- Generate proxy dynamicly
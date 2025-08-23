# EGO.Gladius
Your Simple Sharp tool to Unify Result/Error handling scenarios with maximum Effort for your poor Programmers 💀.

## The philosophy is:
> Any call is not Perfectly Valid Until You Validate it
> 
> So The Main Return Name is SPR (Super Posed Result)


## Install From Nuget

[Base Library](https://www.nuget.org/packages/EGO.Gladius/) <br>
[Runtime Weaver](https://www.nuget.org/packages/EGO.Gladius.Weaver/)


## Get Started
```csharp
// Begin to fit in pattern
SPR<string> result = SPR.FromResult("Your Result");

// or Use Extension
SPR<string> result = "Your Result".AsSPR();
```

Usage on Methods are:
```csharp
SPR<string> result = GetSomeResult();

SPR<string> GetSomeResult()
{
	// Do your stuff

	// Convert to SPR<> is implicit to simplify usage and integration with Codebases
	return "Processed Result";
}
```

You can also mix it with Tasks like:
```csharp
SPR<string> result = await GetSomeResultAsync();

async Task<SPR<string>> GetSomeResultAsync()
{
	// Do your async stuff

	return "Processed Result";
}

async ValueTask<SPR<string>> GetSomeResultAsync()
{
	// Do your async stuff

	return "Processed Result";
}
```

Fault can Be Returned Like Normal objects, Generated, Or Thrown Normally <br>
Any Method With SPR, VSP, Return type Will Automatically Convert Fault or Thrown Exceptions into Result and Return
```csharp
SPR<string> GetSomeResult()
{
    if (hasError)
        throw new Exception("Faulted Here");

    if (hasError)
        return SPF.Gen("Faulted Here");

    return "Processed Result";
}
```

no need to wrap method body or calling site into Try/Catch block to handle errors <br>
Just install [EGO.Gladius.Weaver](https://www.nuget.org/packages/EGO.Gladius/) package so any Throwable Error will be Converted into Fault Result and returned Gracefully
```csharp
if (result.Faulted())
{
    // handle error here using
    var fault = result.Fault;

	fault.Message ... // custom message
	fault.Exception ... // captured exception
	fault.CapturedContext ... // chain of method callings led to error
	fault.Parameters ... // last method parameters where fault happened
}
```

This library is built to terminate excess Try/Catch blocks within code <br>
and enforce implementing fault handling pipeline along with main flow

the only important part of this library is You Cannot Get Result Data Without Qualifying
So 
```csharp
if(result.Succeed(out var val))
{
    // if result succeeded you can have the returned value
}
else
{
    // also you have to implement error handling right here
    // to have a more robust method with least edge cases and unhandled errors/branches
}
```

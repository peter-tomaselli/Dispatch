# Dispatch

## `Callable`
An instance of `Callable` wraps a function with a specific number of arguments. There are several `Callable`s for different function 'arities':
* ```class Callable<R> : ICallable<R>```
* ```class Callable<T1, R> : ICallable<R>```
* ```class Callable<T1, T2, R> : ICallable<R>```
* ```...```

## `ICallable`
All arities of `Callable` implement the same 'arity' of `ICallable`, which is an `interface` with one method:
* ```R InvokeWithArgs(params object[] args)```
* This provides one level of indirection: the programmer can attempt to call a function of an arbitrary number of arguments using an arbitrary number of arguments
  * If you get it wrong this will fail tho

## `CallableTable`
`CallableTable` provides a second level of indirection by mapping `ICallable`s to two levels of `string` keys (a '`domain`' and a '`name`')
* In sum, this provides a 'dispatch table' where functions are 'namespaced' (called here '`domain`'), can be 'resolved', and subsequently invoked with an arbitrary number of parameters at runtime

### `CallableTable` Example
```csharp
public void RuntimeWingIt()
{
  ICallableTable<MyResult> myTable = /* configure the table */
  
  MyResult result = myTable.GetCallable(
    "someRuntimeDomainKey", 
    "someRuntimeFunctionName")
  .InvokeWithArgs("some", "number", "of", "runtime", "args");
}
```

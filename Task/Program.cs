#region using
using System.Diagnostics;
var stopWatch = new Stopwatch();
#endregion

#region without task
/*stopWatch.Start();
for (int i = 0; i < 10; i++)
{
    myFunction1(i);
}
stopWatch.Stop();
Console.WriteLine(stopWatch.ElapsedMilliseconds);

void myFunction1(int x)
{
    Console.WriteLine($"execution begin:{x}");
    Task.Delay(1000).Wait();
    Console.WriteLine($"execution end:{x}");
}
// execution 10131 ms
// each one finish its execution, then the thread goes to next one*/
#endregion

#region with task
/*stopWatch.Restart();
for (int i = 0; i < 10; i++)
{
    myFunction2(i);
}
stopWatch.Stop();
Console.WriteLine(stopWatch.ElapsedMilliseconds);

Task myFunction2(int x)
{
    return Task.Run(() =>
    {
        Console.WriteLine($"execution begin:{x}");
        Task.Delay(1000);
        Console.WriteLine($"execution end:{x}");
    });
}
// execution 36 ms
// each task runs seperately, non of them finish theid code because its not awaited
// and before finish their code, the watch stops (prints are not done)*/
#endregion

#region with task ( call the method using await )
/*stopWatch.Restart();
for (int i = 0; i < 10; i++)
{
    await myFunction3(i);
}
stopWatch.Stop();
Console.WriteLine(stopWatch.ElapsedMilliseconds);

Task myFunction3(int x)
{
    return Task.Run(() =>
    {
        Console.WriteLine($"execution begin:{x}");
        Task.Delay(1000);
        Console.WriteLine($"execution end:{x}");
    });
}
// execution 86 ms
// each task runs seperately, all of them finish theid code and prints
// but the delay does not work because its not called using await*/
#endregion

#region with task ( call the method using await ) ( call the delay using await )
/*stopWatch.Restart();
for (int i = 0; i < 10; i++)
{
    await myFunction4(i);
}
stopWatch.Stop();
Console.WriteLine(stopWatch.ElapsedMilliseconds);

Task myFunction4(int x)
{
    return Task.Run(async () =>
    {
        Console.WriteLine($"execution begin:{x}");
        await Task.Delay(1000);
        Console.WriteLine($"execution end:{x}");
    });
}
// execution 10243 ms
// each task runs seperately and before its execution is done,
// the other ones do not start*/
#endregion

#region with task ( call the method using await ) ( call the delay using await ) (WhenAll)
/*var tasks5 = new List<Task>();
stopWatch.Restart();
for (int i = 0; i < 10; i++)
{
    var myTask5 = myFunction5(i);
    tasks5.Add(myTask5);
}
await Task.WhenAll(tasks5);
stopWatch.Stop();
Console.WriteLine(stopWatch.ElapsedMilliseconds);

Task myFunction5(int x)
{
    return Task.Run(async () =>
    {
        Console.WriteLine($"execution begin:{x}");
        await Task.Delay(1000);
        Console.WriteLine($"execution end:{x}");
    });
}
// execution 1116 ms
// all of them in 10 thread begin together
// and end together (the order is not fixed)*/
#endregion

#region with task ( call the method using await ) ( call the delay using await ) (WhenAny)
/*var tasks6 = new List<Task>();
stopWatch.Restart();
for (int i = 0; i < 10; i++)
{
    var myTask6 = myFunction6(i);
    tasks6.Add(myTask6);
}
await Task.WhenAny(tasks6);
stopWatch.Stop();
Console.WriteLine(stopWatch.ElapsedMilliseconds);

Task myFunction6(int x)
{
    return Task.Run(async () =>
    {
        Console.WriteLine($"execution begin:{x}");
        await Task.Delay(1000);
        Console.WriteLine($"execution end:{x}");
    });
}
// execution 1166 ms
// all of them in 10 thread begin together
// after finishing the first one, timer stops*/
#endregion

#region with task ( call the method using await ) ( call the delay using await ) (WhenAny) (delay not fixed)
/*var tasks7 = new List<Task>();
stopWatch.Restart();
for (int i = 0; i < 10; i++)
{
    var myTask7 = myFunction7(i);
    tasks7.Add(myTask7);
}
await Task.WhenAny(tasks7);
//await Task.WhenAll(tasks7);
stopWatch.Stop();
Console.WriteLine(stopWatch.ElapsedMilliseconds);

Task myFunction7(int x)
{
    return Task.Run(async () =>
    {
        Console.WriteLine($"execution begin:{x}");
        await Task.Delay((x+1)*1000);
        Console.WriteLine($"execution end:{x}");
    });
}
// execution 1074 ms
// all of them in 10 thread begin together
// after finishing thread0, the times stops
// if it was WhenAll the time was order of large one which is thread9 ( 10890 ms )*/
#endregion
using ConsoleLoadingApp;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("Hello, World!");
using (var loading = new ConsoleLoading("Download nuget package") { Interval = 20 })
{
    int errCount = 0;
    while (errCount < 500)
    {
        loading.Value += 0.2;
        Thread.Sleep(5);
        errCount++;
    }
    loading.OK("Download complete");
}

using (var loading = new ConsoleLoading("Building for production.."))
{
    Thread.Sleep(6000);
    loading.End("Build production complete", ConsoleColor.DarkBlue);
}

using (var loading = new ConsoleLoading("Building for production.."))
{
    while (true)
    {
        Thread.Sleep(10000);
    }
}
using ConsoleLoadingApp;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("Hello, World!");
var loading = new ConsoleLoading("Download nuget package", "Download complete") { Interval = 20 };
loading.Start();
int errCount = 0;
while (errCount < 500)
{
    loading.Value += 0.2;
    Thread.Sleep(5);
    errCount++;
}
loading.Dispose();

loading = new ConsoleLoading("Building for production..", "Build production complete");
loading.Start();

while (true)
{
    Thread.Sleep(10000);
}
// See https://aka.ms/new-console-template for more information
using ConsoleApp1;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("Hello, World!");
var loading = new ConsoleLoading("Building for production..", "BuildOK");
loading.Start();
int errCount = 0;
while (errCount < 30)
{
    Thread.Sleep(100);
    errCount++;
}
loading.Dispose();

loading = new ConsoleLoading("正在最后调整..", "最后调整完成") { Interval = 50 };
loading.Start();

while (true)
{
    Thread.Sleep(10000);
}
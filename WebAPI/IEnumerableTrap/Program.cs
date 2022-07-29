// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


IEnumerable<int> GetItems()
{
    yield return 1;
    yield return 2;
    yield return 3;
    Console.WriteLine("666");
    yield return 4;
    yield return 6;
}

var items = GetItems();
Console.WriteLine("OK");

foreach (var item in items)
{
    Console.WriteLine(item);
}
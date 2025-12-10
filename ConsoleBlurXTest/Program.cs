// See https://aka.ms/new-console-template for more information
using ConsoleBlurXTest;
using Mask.BlurX;
using System.Text.Json;

Console.WriteLine("Hello, World!");

var rootModel = BlurEngineeDataGenerator.GetBlurEngineeData(2);

var maskedModel = BlurXHelper.Mask(rootModel);

//var options = new JsonSerializerOptions
//{
//    WriteIndented = true,           // Pretty print
//    ReferenceHandler = ReferenceHandler.Preserve, // Handle circular references if any
//    MaxDepth = 100                  // Increase depth for deep objects
//};

string json = JsonSerializer.Serialize(maskedModel);
Console.WriteLine(json);
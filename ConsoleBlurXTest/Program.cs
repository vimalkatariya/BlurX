// See https://aka.ms/new-console-template for more information
using ConsoleBlurXTest;
using Mask.BlurX;
using System.Text.Json;

Console.WriteLine("Hello, World!");

#region LargeModel

//var rootModel = BlurEngineeDataGenerator.GetBlurEngineeData(2);
//var maskedModel = BlurXHelper.Mask(rootModel); 

#endregion LargeModel

var user = new UserInfo
{
    Email = "john.doe@example.com",
    UserName = "john007.doe",
    Phone = "9876543210",
    WorkPhone = "9876543210",
    Phone2 = "9876543210",
    Phone3 = "9876543210",
    Password = "MyStrongPassword123",
    CardNumber = "4111 1111 1111 1111",
};

var maskedModel = BlurXHelper.Mask(user);

string json = JsonSerializer.Serialize(maskedModel);
Console.WriteLine(json);
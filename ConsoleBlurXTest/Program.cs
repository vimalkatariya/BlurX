// See https://aka.ms/new-console-template for more information
using Mask.BlurX;

Console.WriteLine("Hello, World!");

//var rootModel = BlurEngineeDataGenerator.GetBlurEngineeData(2);

//var maskedModel = BlurXHelper.Mask(rootModel);

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

public class MyClass { public List<UserDto> UsersList() { var users = new List<UserDto> { new UserDto { Name = "Alice Johnson", Phone = "5551239876", Email = "alice.johnson@example.com", CardNo = "4539123498761234" }, new UserDto { Name = "Brian Smith", Phone = "5559871122", Email = "brian.smith@example.com", CardNo = "4726123476549821" }, new UserDto { Name = "Carla Mendoza", Phone = "5554412299", Email = "carla.mendoza@example.com", CardNo = "4021345698764321" }, new UserDto { Name = "David Lee", Phone = "5557734455", Email = "david.lee@example.com", CardNo = "4916123487659999" }, new UserDto { Name = "Emily Clark", Phone = "5553345678", Email = "emily.clark@example.com", CardNo = "4485123456781234" }, new UserDto { Name = "Frank Turner", Phone = "5555529988", Email = "frank.turner@example.com", CardNo = "4532123412345678" }, new UserDto { Name = "Grace Howard", Phone = "5556621100", Email = "grace.howard@example.com", CardNo = "4029123499991111" }, new UserDto { Name = "Henry Cooper", Phone = "5557843321", Email = "henry.cooper@example.com", CardNo = "4556123455556666" }, new UserDto { Name = "Ivy Martinez", Phone = "5559912345", Email = "ivy.martinez@example.com", CardNo = "4716123467894321" }, new UserDto { Name = "Jack Thompson", Phone = "5552203344", Email = "jack.thompson@example.com", CardNo = "4489123400012345" } }; return users; } }


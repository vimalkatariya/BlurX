// See https://aka.ms/new-console-template for more information
using BlurX;

Console.WriteLine("Hello, World!");

var users = new MyClass().UsersList();

BlurXHelper.Mask(users);

foreach (var item in users)
{
    Console.WriteLine("====================================");
    Console.WriteLine($"Name    : {item.Name}");
    Console.WriteLine($"Phone   : {item.Phone}");
    Console.WriteLine($"Email   : {item.Email}");
    Console.WriteLine($"Card No : {item.CardNo}");
    Console.WriteLine("====================================\n");
}

public class UserDto
{
    [BlurXField(BlurStyle.Default)]
    public string Name { get; set; }

    [BlurXField(BlurStyle.Prefix, maskLength: 4)]
    public string Phone { get; set; }

    [BlurXField(BlurStyle.Email)]
    public string Email { get; set; }

    [BlurXField(BlurStyle.Regex, regexPattern: @"\d(?=\d{4})")]
    public string CardNo { get; set; }
}


public class MyClass { public List<UserDto> UsersList() { var users = new List<UserDto> { new UserDto { Name = "Alice Johnson", Phone = "5551239876", Email = "alice.johnson@example.com", CardNo = "4539123498761234" }, new UserDto { Name = "Brian Smith", Phone = "5559871122", Email = "brian.smith@example.com", CardNo = "4726123476549821" }, new UserDto { Name = "Carla Mendoza", Phone = "5554412299", Email = "carla.mendoza@example.com", CardNo = "4021345698764321" }, new UserDto { Name = "David Lee", Phone = "5557734455", Email = "david.lee@example.com", CardNo = "4916123487659999" }, new UserDto { Name = "Emily Clark", Phone = "5553345678", Email = "emily.clark@example.com", CardNo = "4485123456781234" }, new UserDto { Name = "Frank Turner", Phone = "5555529988", Email = "frank.turner@example.com", CardNo = "4532123412345678" }, new UserDto { Name = "Grace Howard", Phone = "5556621100", Email = "grace.howard@example.com", CardNo = "4029123499991111" }, new UserDto { Name = "Henry Cooper", Phone = "5557843321", Email = "henry.cooper@example.com", CardNo = "4556123455556666" }, new UserDto { Name = "Ivy Martinez", Phone = "5559912345", Email = "ivy.martinez@example.com", CardNo = "4716123467894321" }, new UserDto { Name = "Jack Thompson", Phone = "5552203344", Email = "jack.thompson@example.com", CardNo = "4489123400012345" } }; return users; } }


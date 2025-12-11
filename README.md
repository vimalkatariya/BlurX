# BlurX

**BlurX** is a lightweight, enterprise-grade **data masking library for
.NET** that helps you protect sensitive information (PII, credentials,
emails, phone numbers, card numbers, etc.) directly at the **model/DTO
property level** using simple attributes.

Built for:

-   ✅ Zero-effort field masking\
-   ✅ Fine-grained, per-property control\
-   ✅ Multiple ready-to-use masking styles\
-   ✅ High-performance reflection engine\
-   ✅ Enterprise-ready customizability

------------------------------------------------------------------------

## ✨ Features

-   Apply masking using simple **attributes**\
-   Control **visible character count**, **mask length**, and **mask
    character**\
-   Supports multiple masking strategies:
    -   **Prefix masking**
    -   **Suffix masking**
    -   **Middle masking**
    -   **Full masking**
    -   **Email masking**
    -   **Regex-based masking**
-   Handles:
    -   Single objects\
    -   Lists & collections\
-   Easy extensibility for custom masking rules

------------------------------------------------------------------------

## 📦 Installation

Install via NuGet:

``` bash
dotnet add package Mask.BlurX
```

Or using the NuGet Package Manager:

    Install-Package Mask.BlurX

------------------------------------------------------------------------

## 🚀 Quick Example

### **Model Example**

``` csharp
public class UserInfo
{
    [BlurXField(BlurStyle.Email)]
    public string Email { get; set; }

    [BlurXField(BlurStyle.Regex, regexPattern: @"\d")]
    public string UserName { get; set; }

    [BlurXField(BlurStyle.Prefix)]
    public string WorkPhone { get; set; }

    [BlurXField(BlurStyle.Prefix, visibleCharCount: 1)]
    public string Phone { get; set; }
        
    [BlurXField(BlurStyle.Suffix, visibleCharCount: 1)]
    public string Phone2 { get; set; }

    [BlurXField(BlurStyle.Default)]
    public string Phone3 { get; set; }

    [BlurXField(BlurStyle.Full)]
    public string Password { get; set; }

    [BlurXField(BlurStyle.Middle, maskChar: '#', maskLength: 6)]
    public string CardNumber { get; set; }
}
```

### **Masking Usage**

``` csharp
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

var masked = BlurX.Mask(user);
```

### **Output**
``` json
    {
      "Email": "********@example.com",
      "UserName": "john***.doe",
      "WorkPhone": "******3210",
      "Phone": "9******210",
      "Phone2": "987******0",
      "Phone3": "98******10",
      "Password": "*******************",
      "CardNumber": "4111 1######11 1111"
    }
```
------------------------------------------------------------------------

## 🛠️ Configuration Options

Each property allows full control using the attribute:

``` csharp
[BlurX(
    Style = BlurStyle.Middle,
    MaskChar = '*',
    MaskLength = 4,
    VisibleCharCount = 2,
    RegexPattern = null
)]
public string CardNumber { get; set; }
```

  -------------------------------------------------------------------------
  Parameter                       Description
  ------------------------------- -----------------------------------------
  **Style**                       Defines how masking is applied
                                  (Prefix/Suffix/Middle/Full/Email/Regex)

  **MaskLength**                  Number of characters to replace with mask
                                  character

  **VisibleCharCount**            Number of characters to keep unmasked

  **MaskChar**                    Character used for masking (default: `*`)

  **RegexPattern**                Custom masking via regular expression
  -------------------------------------------------------------------------

------------------------------------------------------------------------

## 📂 Supports Complex Scenarios

-   Mask entire objects\
-   Mask deeply nested properties\
-   Mask dynamic collections\
-   Use custom masking engines

------------------------------------------------------------------------

## 📞 Contact & Support

If you find a bug, need an enhancement, or want to suggest a new
feature:

-   **Open an issue on GitHub:**\
    👉 *https://github.com/vimalkatariya/BlurX/issues*\
-   Or contact me by email:\
    👉 **vimalkatariya5@gmail.com**

------------------------------------------------------------------------

## 🤝 Contributing

Contributions are welcome!\
Feel free to submit:

-   Pull Requests
-   Feature Requests
-   Bug Reports

------------------------------------------------------------------------

## ❤️ Support the Project

If this package helps you, please give it a ⭐ on GitHub and leave a
review on NuGet.

using Mask.BlurX;

namespace ConsoleBlurXTest
{
    // ClassE - Sales Representative
    public class SalesRep
    {
        public int Id { get; set; }

        [BlurXField(BlurStyle.Default, visibleCharCount: 2, maskLength: 4)]
        public string Name { get; set; }

        [BlurXField(BlurStyle.Email, visibleCharCount: 2, maskLength: 4)]
        public string Email { get; set; }

        [BlurXField(BlurStyle.Middle)]
        public string PhoneNumber { get; set; }

        [BlurXField(BlurStyle.Prefix)]
        public DateTime DateOfJoining { get; set; }
        public decimal TotalSales { get; set; }

        [BlurXField(BlurStyle.Default)]
        public byte[] Invoice { get; set; }

    }

    // ClassD - Store Employee / Supervisor
    public class StoreEmployee
    {
        public int Id { get; set; }

        [BlurXField(BlurStyle.Middle)]
        public string Name { get; set; }

        [BlurXField(BlurStyle.Email)]
        public string Email { get; set; }

        [BlurXField(BlurStyle.Regex, regexPattern: @"(?<!\w)(?:\+?\d{1,3}[\s.-]?)?(?:\(?\d{3,5}\)?[\s.-]?)?\d{3,4}[\s.-]?\d{4}(?!\w)")]
        public string PhoneNumber { get; set; }

        [BlurXField(BlurStyle.Default)]
        public DateTime DateOfJoining { get; set; }
        public string Position { get; set; }

        public byte[] Image { get; set; }

        public List<SalesRep> SalesReps { get; set; } = new List<SalesRep>();
    }

    // ClassC - Local Store
    public class LocalStore
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }

        [BlurXField(BlurStyle.Full)]
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [BlurXField(BlurStyle.Default)]
        public string ZipCode { get; set; }

        public List<StoreEmployee> Employees { get; set; } = new List<StoreEmployee>();
    }

    // ClassB - Regional Franchise
    public class RegionalFranchise
    {
        public int RegionId { get; set; }
        [BlurXField(BlurStyle.Default, visibleCharCount: 2, maskChar: '-')]
        public string RegionName { get; set; }
        public string ManagerName { get; set; }

        [BlurXField(BlurStyle.Email)]
        public string ManagerEmail { get; set; }

        [BlurXField(BlurStyle.Regex, regexPattern: @"(?<!\w)(?:\+91[\s-]?)?[6-9]\d{9}(?!\w)")]
        public string ManagerPhone { get; set; }

        public List<LocalStore> Stores { get; set; } = new List<LocalStore>();
    }

    // ClassA - Master Franchise
    public class MasterFranchise
    {
        public int FranchiseId { get; set; }

        [BlurXField(BlurStyle.Prefix, maskLength: 10, visibleCharCount: 3)]
        public string FranchiseName { get; set; }
        [BlurXField(BlurStyle.Suffix, maskLength: 3, visibleCharCount: 3)]
        public string CEOName { get; set; }
        [BlurXField(BlurStyle.Email)]
        public string CEOEmail { get; set; }

        [BlurXField(BlurStyle.Regex, regexPattern: @"(?<!\w)(?:\+91[\s-]?)?[6-9]\d{9}(?!\w)")]
        public string CEOPhone { get; set; }

        public List<RegionalFranchise> Regions { get; set; } = new List<RegionalFranchise>();
    }

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


}
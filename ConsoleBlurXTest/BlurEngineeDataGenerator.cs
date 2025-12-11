using Bogus;
using System.Reflection.Metadata;

namespace ConsoleBlurXTest
{
    internal static class BlurEngineeDataGenerator
    {
        public static List<MasterFranchise> GetBlurEngineeData(int count)
        {
            var list = GetMasterFranchise(count);

            return list;
        }

        public static List<MasterFranchise> GetMasterFranchise(int count)
        {
            var faker = new Faker();
            var list = new List<MasterFranchise>();

            for (int i = 0; i < count; i++)
            {
                list.Add(new MasterFranchise
                {
                    FranchiseId = faker.UniqueIndex,
                    FranchiseName = faker.Company.CompanyName(),
                    CEOName = faker.Name.FullName(),
                    CEOEmail = faker.Internet.Email(),
                    CEOPhone = faker.Phone.Locale,
                    Regions = GetRegionalFranchise(count)
                });
            }

            return list;
        }

        public static List<RegionalFranchise> GetRegionalFranchise(int count)
        {
            var list = new List<RegionalFranchise>();
            var faker = new Faker();

            for (int i = 0; i < count; i++)
            {
                list.Add(new RegionalFranchise
                {
                    RegionId = faker.UniqueIndex,
                    RegionName = faker.Address.State(),
                    ManagerName = faker.Name.FullName(),
                    ManagerEmail = faker.Internet.Email(),
                    ManagerPhone = faker.Phone.PhoneNumber(),
                    Stores = GetLocalStore(5)
                });

            }

            return list;
        }

        public static List<LocalStore> GetLocalStore(int count)
        {
            var list = new List<LocalStore>();
            var faker = new Faker();
            for (int i = 0; i < count; i++)
            {
                list.Add(new LocalStore
                {
                    StoreId = faker.UniqueIndex,
                    StoreName = faker.Company.CompanyName(),
                    Address = faker.Address.StreetAddress(),
                    City = faker.Address.City(),
                    State = faker.Address.State(),
                    ZipCode = faker.Address.ZipCode(),
                    Employees = GetStoreEmployee(10)
                });
            }

            return list;
        }

        public static List<StoreEmployee> GetStoreEmployee(int count)
        {
            var list = new List<StoreEmployee>();
            var faker = new Faker();
            for (int i = 0; i < count; i++)
            {
                list.Add(new StoreEmployee
                {
                    Id = faker.UniqueIndex,
                    Name = faker.Name.FullName(),
                    Email = faker.Internet.Email(),
                    PhoneNumber = faker.Phone.PhoneNumber(),
                    DateOfJoining = faker.Date.Past(5),
                    Position = faker.Name.JobTitle(),
                    SalesReps = GetSalesRep(5),
                    Image = faker.Random.Bytes(300)
                });
            }

            return list;
        }

        public static List<SalesRep> GetSalesRep(int count)
        {
            var list = new List<SalesRep>();
            var faker = new Faker();
            for (int i = 0; i < count; i++)
            {
                list.Add(new SalesRep
                {
                    Id = faker.UniqueIndex,
                    Name = faker.Name.FullName(),
                    Email = faker.Internet.Email(),
                    PhoneNumber = faker.Phone.PhoneNumber(),
                    DateOfJoining = faker.Date.Past(3),
                    TotalSales = Math.Round(faker.Finance.Amount(5000, 100000), 2),
                    Invoice = faker.Random.Bytes(200)
                });
            }

            return list;
        }
    }
}

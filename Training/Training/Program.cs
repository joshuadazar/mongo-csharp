using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Core;
using Training;
using System.Security.Principal;

internal class Program
{
    static async Task Main(string[] args)
    {
        Task send = AddCollections();
        await send;

    }

    static async Task AddCollections()
    {
        /*
        
        #region insert multiple
        var newAccount2 = new Account
        {
            AccountId = "002",
            AccountHolder = " Maylin Daza",
            AccountType = "checking",
            Balance = 3000000

        };

        var newAccount3 = new Account
        {
            AccountId = "003",
            AccountHolder = " Sophia Daza",
            AccountType = "checking",
            Balance = 100000

        };

        var accounts = new[]
            {
                newAccount2,
                newAccount3
            };


       await accountCollection.InsertManyAsync(accounts);
        #endregion
        */

      

        initializeStep: Console.WriteLine("Please input full name: ");

        var name =  Console.ReadLine();
        var accountTypeOptions = Helper.AccountType;
        Console.WriteLine($"{name} Please set your Account Type \n {string.Join(" ", Helper.AccountType)}");
        var accountType = Console.ReadLine();

        foreach (var item in accountTypeOptions)
        {
            if (item.Contains(accountType))
            {
                accountType = item.Substring(item.IndexOf(' '));
            }
        }

        Console.WriteLine($"{name} Please input your Initial Consignation: ( Only use numeric values ) ");
        string balance = Console.ReadLine();


    #region confirmation
    confirmationDataStep:
        Console.WriteLine($"\n {name} Please confirm if the following information is correct: \n {accountType}| {balance} | \n Type: 'Y' to continue. \n type: 'R' to re-start the process.");
        #endregion

        var confirmation = Console.ReadLine();
        confirmation = confirmation?.ToLower();

        if (confirmation is not null)
        {
            switch (confirmation)
            {
                case "y":
                    confirmation = confirmation ?? confirmation;
                    goto sendDataStep;
                    break;

                case "r":
                    goto initializeStep;
                    

                default:
                    break;
            }
        } else
        {
            goto confirmationDataStep;
            
        }

        sendDataStep: Console.WriteLine($"{name} If you want to create an account please type 'y' else type 'r'");

        Account newAccount = CreateAccountStructure( name, accountType, Convert.ToDecimal(balance)) ;
        Task save = SaveAccount(newAccount);
        await save;
        if (save.IsCompleted)
            Console.WriteLine($"User: {name} was created succesfully");
    }


    static Account CreateAccountStructure(string nameAccount, string accountType, decimal balanceAccount )
    {
        var uid = Guid.NewGuid();
        string uide =  uid.ToString();
        var newAccount = new Account
        {
            AccountId = uide,
            AccountHolder = nameAccount,
            AccountType = accountType,
            Balance = balanceAccount

        };
        return newAccount;
    }

    static async Task SaveAccount(Account account_)
    {
        var client = new MongoClient(Helper.AtlasUri);
        //var dbList = client.ListDatabases().ToList();
        var database = client.GetDatabase("bank");
        var accountCollection = database.GetCollection<Account>("account");
        accountCollection.InsertOne(account_);

    }
}




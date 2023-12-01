using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
//generic class
class InventoryItem
{
    public string Name { get; set; } = string.Empty;
    public string PurchaseDate { get; set; } = string.Empty;
    public string ExpirationDate { get; set; } = string.Empty;
    public int Quantity { get; set; }
}

class Program
{
    static void SaveInventoryToFile()
    {
// SRP: Saves the current inventory to a file in JSON format.
//log of item inventory saved to a text file
        string json = System.Text.Json.JsonSerializer.Serialize(inventory);
        File.WriteAllText("data.json", json);
    }
//regex for formatting date 
    static string FormatDate(string input)
    {
// SRP: Formats the date to mm/dd/yyyy and ensures valid date format using regex.
        var digitsOnly = string.Concat(input.Where(char.IsDigit));
        if (digitsOnly.Length == 8)
        {
            return digitsOnly.Substring(0, 2) + "/" + digitsOnly.Substring(2, 2) + "/" + digitsOnly.Substring(4, 4);
        }
        return input;
    }


    static void LoadInventoryFromFile()
    {
// SRP: Loads the inventory from a file in JSON format.
        if (File.Exists("data.json"))
        {
            string json = File.ReadAllText("data.json");
            inventory = System.Text.Json.JsonSerializer.Deserialize<List<InventoryItem>>(json) ?? new List<InventoryItem>();
        }
    }
//list
    static List<InventoryItem> inventory = new List<InventoryItem>();

    static void DisplayMenu()
    {
 // SRP: Displays a simple menu for interacting with the inventory.
        Console.WriteLine("                     ▐▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▌");
        Console.WriteLine("                     ▐       ██╗    ██╗██╗  ██╗ █████╗ ████████╗    ██╗███████╗    ██╗███╗   ██╗          ▌");
        Console.WriteLine("                     ▐       ██║    ██║██║  ██║██╔══██╗╚══██╔══╝    ██║██╔════╝    ██║████╗  ██║          ▌");
        Console.WriteLine("                     ▐       ██║ █╗ ██║███████║███████║   ██║       ██║███████╗    ██║██╔██╗ ██║          ▌");
        Console.WriteLine("                     ▐       ██║███╗██║██╔══██║██╔══██║   ██║       ██║╚════██║    ██║██║╚██╗██║          ▌");
        Console.WriteLine("                     ▐       ╚███╔███╔╝██║  ██║██║  ██║   ██║       ██║███████║    ██║██║ ╚████║          ▌");
        Console.WriteLine("                     ▐        ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝       ╚═╝╚══════╝    ╚═╝╚═╝  ╚═══╝          ▌");
        Console.WriteLine("                     ▐    ███╗   ███╗██╗   ██╗    ███████╗██████╗ ██╗██████╗  ██████╗ ███████╗██████╗     ▌");
        Console.WriteLine("                     ▐    ████╗ ████║╚██╗ ██╔╝    ██╔════╝██╔══██╗██║██╔══██╗██╔════╝ ██╔════╝╚════██╗    ▌");
        Console.WriteLine("                     ▐    ██╔████╔██║ ╚████╔╝     █████╗  ██████╔╝██║██║  ██║██║  ███╗█████╗    ▄███╔╝    ▌");
        Console.WriteLine("                     ▐    ██║╚██╔╝██║  ╚██╔╝      ██╔══╝  ██╔══██╗██║██║  ██║██║   ██║██╔══╝    ▀▀══╝     ▌");
        Console.WriteLine("                     ▐    ██║ ╚═╝ ██║   ██║       ██║     ██║  ██║██║██████╔╝╚██████╔╝███████╗  ██╗       ▌");
        Console.WriteLine("                     ▐    ╚═╝     ╚═╝   ╚═╝       ╚═╝     ╚═╝  ╚═╝╚═╝╚═════╝  ╚═════╝ ╚══════╝  ╚═╝       ▌");
        Console.WriteLine("                     ▐▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▌");

        Console.WriteLine("\n                                                       Choose an option:" +
            "");
        Console.WriteLine("                                         [A]dd Item | [R]emove Item | [V]iew Inventory | E[x]it");
    }

    static void AddItem()
    {
// SRP: Adds an item to the inventory with user input validation.
        Console.Write(" Enter name of item: ");
        string name = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter date of purchase: ");
        string purchaseDate;

        while (true)
        {
            Console.Write("Enter date of purchase (mm/dd/yyyy): ");
            string input = Console.ReadLine() ?? string.Empty;
            purchaseDate = FormatDate(input);
            if (DateTime.TryParseExact(purchaseDate, "dd/mm/yyyy.", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                break;
            }

            Console.WriteLine("Invalid date format. Please use mm/dd/yyyy.");
        }

        Console.Write("Enter expiration date: ");
        string expirationDate; 

        while (true)
        {
            
            
            string input = Console.ReadLine() ?? string.Empty;
            expirationDate = FormatDate(input);
            if (DateTime.TryParseExact(expirationDate, "dd/mm/yyyy.", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                break;
            }


            Console.WriteLine("Invalid date format. Please use mm/dd/yyyy.");
        }

        Console.Write("Enter the quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            Console.WriteLine("Invalid input for quantity.");
            return;
        }

        InventoryItem item = new InventoryItem
        {
            Name = name,
            PurchaseDate = purchaseDate,
            ExpirationDate = expirationDate,
            Quantity = quantity
        };

        inventory.Add(item);
        Console.WriteLine("Item added successfully!");
    }

    static void RemoveItem()
    {
// SRP: Removes an item from the inventory with user input validation.
        if (inventory.Count == 0)
        {
            Console.WriteLine("Inventory is empty. Cannot remove items.");
            return;
        }

        Console.WriteLine("Current Inventory: ");
        for (int i = 0; i < inventory.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {inventory[i].Name}");
        }

        Console.Write("Enter the number of the item to remove: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= inventory.Count)
        {
            InventoryItem removedItem = inventory[choice - 1];
            inventory.RemoveAt(choice - 1);
            Console.WriteLine($"{removedItem.Name} removed from inventory.");
        }
        else
        {
            Console.WriteLine("Invalid choice. Please enter a valid item number.");
        }
    }

    static void ViewInventory()
    {
// SRP: Displays the current inventory in a user-friendly format.
        if (inventory.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }

        Console.WriteLine("\nInventory:");
        for (int i = 0; i < inventory.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Name: {inventory[i].Name}, Purchase Date: {inventory[i].PurchaseDate}, Expiration Date: {inventory[i].ExpirationDate}, Quantity: {inventory[i].Quantity}");
        }
    }

    static void Main()
    {
// SRP: Manages the main control flow of the inventory application.
        LoadInventoryFromFile();

        while (true)
        {
            DisplayMenu();
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (char.ToUpper(key.KeyChar))
            {
                case 'A':
                    AddItem();
                    break;
                case 'R':
                    RemoveItem();
                    break;
                case 'V':
                    ViewInventory();
                    break;
                case 'X':
                    SaveInventoryToFile();
                    Console.WriteLine("\nExiting the menu. Goodbye!");
                    return;
                default:
                    Console.WriteLine("\nInvalid choice. Please enter a valid option.");
                    break;
            }
        }
    }
}

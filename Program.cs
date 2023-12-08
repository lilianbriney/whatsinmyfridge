using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

// Generic class
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
        string json = System.Text.Json.JsonSerializer.Serialize(inventory);
        File.WriteAllText("data.json", json);
    }

    static string FormatDate(string input)
    {
        var digitsOnly = string.Concat(input.Where(char.IsDigit));
        if (digitsOnly.Length == 8)
        {
            return digitsOnly.Substring(0, 2) + "/" + digitsOnly.Substring(2, 2) + "/" + digitsOnly.Substring(4, 4);
        }
        return input;
    }

    static void LoadInventoryFromFile()
    {
        if (File.Exists("data.json"))
        {
            string json = File.ReadAllText("data.json");
            inventory = System.Text.Json.JsonSerializer.Deserialize<List<InventoryItem>>(json) ?? new List<InventoryItem>();
        }
    }

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

    }

    static void AddItem()
    {
        Console.Write("Enter name of item: ");
        string name = Console.ReadLine() ?? string.Empty;

        string purchaseDate = GetValidDate("Enter date of purchase (mm/dd/yyyy or mmddyyyy): ");
        string expirationDate = GetValidDate("Enter expiration date (mm/dd/yyyy or mmddyyyy): ");

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

    static string GetValidDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? string.Empty;
            string formattedDate = FormatDate(input);
            if (DateTime.TryParseExact(formattedDate, new[] { "MM/dd/yyyy", "MMddyyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return formattedDate;
            }

            Console.WriteLine("Invalid date format. Please use mm/dd/yyyy or mmddyyyy.");
        }
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
        DisplayMenu();
        while (true)
        {
            Console.WriteLine("                                         [A]dd Item | [R]emove Item | [V]iew Inventory | E[x]it");
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
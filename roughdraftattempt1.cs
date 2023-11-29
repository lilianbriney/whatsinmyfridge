using System;
using System.Collections.Generic;

class InventoryItem
{
  public string Name { get; set; }
  public string PurchaseDate { get; set;}
  public string ExpirationDate { get; set;}
  public int Quantity { get; set; 
  }
  }
class Program
//look at this area
{
  static List<InventoryItem> inventory = new List<InventoryItem>();

  static void DisplayMenu()
  {
    Console.WriteLine("\nMenu:");
    Console.WriteLine("Add Item");
    Console.WriteLine("Remove Item");
    Console.WriteLine
    Console.WriteLine("View Inventory");
    Console.WriteLine("Exit");

    Console.WriteLine("\nChoose an option:");

    //Display buttons
    Console.WriteLine("[A]dd Item [R]emove Item [V]iew Inventory E[x]it");
    
  }

  static void AddItem()
  {
    Console.Write("Enter name of item: ");
    string name = Console.ReadLine();
    Console.Write("Enter date of purchase: ");
    string purchaseDate = Console.ReadLine();
    Console.Write("Enter expiration date: ");
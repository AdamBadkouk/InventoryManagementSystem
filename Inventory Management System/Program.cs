using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagementSystem
{
    class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int Id { get; set; }
    }

    class Program
    {
        private static List<Product> products = new List<Product>();
        private static int nextProductId = 1;

        static void Main(string[] args)
        {
            Console.WriteLine("=== Inventory Management System ===");

            bool running = true;
            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        UpdateProduct();  // Changed from UpdateStock to UpdateProduct
                        break;
                    case "3":
                        RemoveProduct();
                        break;
                    case "4":
                        ViewProducts();
                        break;
                    case "5":
                        running = false;
                        Console.WriteLine("Thank you for using the Inventory Management System!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n--- Main Menu ---");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Update Product");  // Updated menu text
            Console.WriteLine("3. Remove Product");
            Console.WriteLine("4. View Products");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice (1-5): ");
        }

        // METHOD: Update Product (Name, Price, or Quantity)
        static void UpdateProduct()
        {
            Console.WriteLine("\n--- Update Product ---");

            if (products.Count == 0)
            {
                Console.WriteLine("No products available to update.");
                return;
            }

            Console.Write("Enter product ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid product ID!");
                return;
            }

            // Find product
            Product productToUpdate = null;
            foreach (var product in products)
            {
                if (product.Id == productId)
                {
                    productToUpdate = product;
                    break;
                }
            }

            if (productToUpdate == null)
            {
                Console.WriteLine($"Product with ID {productId} not found!");
                return;
            }

            // Display current product details
            Console.WriteLine($"\nCurrent Product Details:");
            Console.WriteLine($"Name: {productToUpdate.Name}");
            Console.WriteLine($"Price: ${productToUpdate.Price:F2}");
            Console.WriteLine($"Stock Quantity: {productToUpdate.StockQuantity}");

            // Ask what to update
            Console.WriteLine("\nWhat would you like to update?");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Price");
            Console.WriteLine("3. Stock Quantity");
            Console.WriteLine("4. Update All");
            Console.WriteLine("5. Cancel");
            Console.Write("Enter your choice (1-5): ");

            string updateChoice = Console.ReadLine();

            switch (updateChoice)
            {
                case "1": // Update Name
                    Console.Write("Enter new product name: ");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newName))
                    {
                        string oldName = productToUpdate.Name;
                        productToUpdate.Name = newName;
                        Console.WriteLine($"Product name updated from '{oldName}' to '{newName}'");
                    }
                    else
                    {
                        Console.WriteLine("Product name cannot be empty!");
                    }
                    break;

                case "2": // Update Price
                    Console.Write("Enter new price (you can use $ symbol): ");
                    string priceInput = Console.ReadLine();
                    priceInput = priceInput.Replace("$", "").Replace(" ", "");

                    if (decimal.TryParse(priceInput, out decimal newPrice) && newPrice >= 0)
                    {
                        decimal oldPrice = productToUpdate.Price;
                        productToUpdate.Price = newPrice;
                        Console.WriteLine($"Price updated from ${oldPrice:F2} to ${newPrice:F2}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid price! Please enter a positive number.");
                    }
                    break;

                case "3": // Update Stock Quantity
                    Console.Write("Enter new stock quantity: ");
                    if (int.TryParse(Console.ReadLine(), out int newQuantity) && newQuantity >= 0)
                    {
                        int oldQuantity = productToUpdate.StockQuantity;
                        productToUpdate.StockQuantity = newQuantity;
                        Console.WriteLine($"Stock quantity updated from {oldQuantity} to {newQuantity}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity! Please enter a positive whole number.");
                    }
                    break;

                case "4": // Update All fields
                    UpdateAllProductFields(productToUpdate);
                    break;

                case "5": // Cancel
                    Console.WriteLine("Update cancelled.");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Update cancelled.");
                    break;
            }
        }

        // METHOD: Update all product fields at once
        static void UpdateAllProductFields(Product product)
        {
            Console.WriteLine("\n--- Update All Product Fields ---");

            // Update Name
            Console.Write("Enter new product name (press Enter to keep current): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                product.Name = newName;
            }

            // Update Price
            Console.Write("Enter new price (press Enter to keep current): ");
            string priceInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(priceInput))
            {
                priceInput = priceInput.Replace("$", "").Replace(" ", "");
                if (decimal.TryParse(priceInput, out decimal newPrice) && newPrice >= 0)
                {
                    product.Price = newPrice;
                }
                else
                {
                    Console.WriteLine("Invalid price! Keeping current price.");
                }
            }

            // Update Quantity
            Console.Write("Enter new stock quantity (press Enter to keep current): ");
            string quantityInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(quantityInput))
            {
                if (int.TryParse(quantityInput, out int newQuantity) && newQuantity >= 0)
                {
                    product.StockQuantity = newQuantity;
                }
                else
                {
                    Console.WriteLine("Invalid quantity! Keeping current quantity.");
                }
            }

            Console.WriteLine("Product updated successfully!");
        }

        // Rest of the methods remain the same (AddProduct, RemoveProduct, ViewProducts, etc.)
        static void AddProduct()
        {
            Console.WriteLine("\n--- Add New Product ---");

            try
            {
                Console.Write("Enter product name: ");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Product name cannot be empty!");
                    return;
                }

                Console.Write("Enter product price (you can use $ symbol): ");
                string priceInput = Console.ReadLine();
                priceInput = priceInput.Replace("$", "").Replace(" ", "");

                if (!decimal.TryParse(priceInput, out decimal price) || price < 0)
                {
                    Console.WriteLine("Invalid price! Please enter a positive number.");
                    return;
                }

                Console.Write("Enter stock quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 0)
                {
                    Console.WriteLine("Invalid quantity! Please enter a positive whole number.");
                    return;
                }

                Product newProduct = new Product
                {
                    Id = nextProductId++,
                    Name = name,
                    Price = price,
                    StockQuantity = quantity
                };

                products.Add(newProduct);
                Console.WriteLine($"Product '{name}' added successfully with ID: {newProduct.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product: {ex.Message}");
            }
        }

        static void RemoveProduct()
        {
            Console.WriteLine("\n--- Remove Product ---");

            if (products.Count == 0)
            {
                Console.WriteLine("No products available to remove.");
                return;
            }

            ViewProducts();
            Console.Write("Enter product ID to remove: ");

            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("Invalid product ID!");
                return;
            }

            bool productRemoved = false;
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Id == productId)
                {
                    string productName = products[i].Name;
                    products.RemoveAt(i);
                    Console.WriteLine($"Product '{productName}' removed successfully!");
                    productRemoved = true;
                    break;
                }
            }

            if (!productRemoved)
            {
                Console.WriteLine($"Product with ID {productId} not found!");
            }
        }

        static void ViewProducts()
        {
            Console.WriteLine("\n--- Product List ---");

            if (products.Count == 0)
            {
                Console.WriteLine("No products in inventory.");
                return;
            }

            Console.WriteLine("ID\tName\t\tPrice\t\tStock");
            Console.WriteLine("--------------------------------------------");

            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id}\t{product.Name}\t\t${product.Price:F2}\t\t{product.StockQuantity}");
            }

            DisplayInventoryStatistics();
        }

        static void DisplayInventoryStatistics()
        {
            if (products.Count > 0)
            {
                decimal totalValue = products.Sum(p => p.Price * p.StockQuantity);
                int totalItems = products.Sum(p => p.StockQuantity);

                Console.WriteLine($"\nInventory Statistics:");
                Console.WriteLine($"Total Products: {products.Count}");
                Console.WriteLine($"Total Items in Stock: {totalItems}");
                Console.WriteLine($"Total Inventory Value: ${totalValue:F2}");
            }
        }
    }
}
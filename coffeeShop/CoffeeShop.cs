
using System;

namespace coffeeShop
{
    internal class CoffeeShop
    {
        static List<Item> items = new List<Item>();
        static void Main(string[] args)
        {
            InitialDrinks();
            Login();
        }

        static void Login()
        {
            string username = "user1", userPassword = "123", adminName = "admin1", adminPassword = "admin123";
            bool isNotDoneOrdering = true;
            Console.WriteLine(" ------------------------------------------");
            Console.WriteLine("\tWelcome To Caffeine++");
            do
            {
                Console.WriteLine(" ------------------------------------------");
                Console.WriteLine(" Enter Username: ");
                string userNameInput = Console.ReadLine();
                Console.WriteLine(" Enter Password: ");
                string userPasswordInput = Console.ReadLine();
                Console.WriteLine(" ------------------------------------------");

                if (userNameInput == username && userPasswordInput == userPassword)
                {
                    Order();
                }
                else if (userNameInput == adminName && adminPassword == userPasswordInput)
                {
                    AdminAccess();
                }
                else
                {
                    Console.WriteLine(" ------------------------------------------");
                    Console.WriteLine("Wrong Username Or Password!");
                    Console.WriteLine(" ------------------------------------------");
                }

                if (!IsDone("Program"))
                {
                    Login();
                }

                Environment.Exit(0);

            } while (isNotDoneOrdering);

        }

        static void Order()
        {
            string receipt = "";
            List<Object> orderListBevarage = OrderTemplate("Bevarage");
            List<Object> orderListSnack = OrderTemplate("Snack");
            receipt += orderListBevarage[1];
            receipt += orderListSnack[1];
            receipt += " Total: " + (Convert.ToDouble(orderListBevarage[0]) + Convert.ToDouble(orderListSnack[0]));
            Console.WriteLine(" ------------------------------------------");
            Console.WriteLine("Order Success! \n Here is Your Receipt: ");
            Console.WriteLine(" ------------------------------------------");
            Console.WriteLine(receipt);
            Console.WriteLine(" ------------------------------------------");

        }

        static List<object> OrderTemplate(String typeItem)
        {
            Boolean isOrdering = true;
            List<Item> orderList = new List<Item>();
            String orderReceipt = typeItem + "\n";
            double total = 0;
            do
            {
                foreach (var i in items)
                {
                    if (i.type == typeItem)
                    {
                        orderList.Add(i);
                    }
                }
                Console.WriteLine(typeItem);
                Console.WriteLine(" ------------------------------------------");
                for (int i = 0; i < orderList.Count; i++)
                {
                    Console.WriteLine("[" + i + "] " + orderList[i].name + ":  " + orderList[i].cost);
                }
                Console.WriteLine(" ------------------------------------------");
                Console.WriteLine("Enter Order: ");
                int order = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("Enter Quantity: ");
                int orderQuantity = Convert.ToInt16(Console.ReadLine());
                if (orderList.Count() > order)
                {
                    orderReceipt += orderQuantity + " " + orderList[order].name + " :" + orderQuantity * orderList[order].cost + "\n";

                    total += orderQuantity * orderList[order].cost;
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].name == orderList[order].name)
                        {
                            items[i].soldCount += orderQuantity;
                        }
                    }
                }
                else
                {
                    Console.WriteLine(" ------------------------------------------");
                    Console.WriteLine("Invalid Input");
                }

                if (IsDone(typeItem))
                {
                    isOrdering = false;
                }
                orderList.Clear();

            } while (isOrdering);
            List<Object> orderSummary = new List<Object>();
            orderSummary.Add(total);
            orderSummary.Add(orderReceipt);
            return (orderSummary);
        }

        static void AdminAccess()
        {
            Console.WriteLine(" ------------------------------------------");
            Console.WriteLine("""
                [0] View Sold Summary
                [1] Add Item
                [2] Delete Bevarage
                """);
            int choice = Convert.ToInt16(Console.ReadLine());
            if (choice == 0)
            {
                double totalBevarages = SummaryPerItemTypeAdmin("Bevarage");
                double totalSnacks = SummaryPerItemTypeAdmin("Snack");

                Console.WriteLine("Total: " + (totalBevarages + totalSnacks));

            }
            else if (choice == 1)
            {
                Console.WriteLine(" ------------------------------------------");
                Console.WriteLine("Enter Item Type: ");
                string itemType = Console.ReadLine();
                Console.WriteLine("Enter Item Name: ");
                string itemName = Console.ReadLine();
                Console.WriteLine("Enter Bevarage Cost: ");
                double itemCost = Convert.ToDouble(Console.ReadLine());

                items.Add(new Item(itemName, itemCost, itemType));
                Console.WriteLine(" ------------------------------------------");
                Console.WriteLine(itemName + " is ADDED successfully");
                Console.WriteLine(" ------------------------------------------");

            }
            else if (choice == 2)
            {
                Console.WriteLine(" ------------------------------------------");
                Console.WriteLine("Enter Bevarage Name: ");
                string itemName = Console.ReadLine();

                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].name == itemName)
                    {
                        items.RemoveAt(i);
                        break;
                    }
                }
                Console.WriteLine(" ------------------------------------------");
                Console.WriteLine(itemName + " is DELETED successfully");
                Console.WriteLine(" ------------------------------------------");
            }
            else
            {
                AdminAccess();
            }

            if (!IsDone("Admin Access"))
            {
                AdminAccess();
            }

            return;

        }

        static double SummaryPerItemTypeAdmin(string itemType)
        {
            List<Item> _items = new List<Item>();
            double total = 0.00;
            Console.WriteLine(" ------------------------------------------");
            Console.WriteLine(itemType + " \t\t" + "COST" + "\t" + "Sold  Count" + "\t" + "Total");
            Console.WriteLine(" ------------------------------------------\n");
            foreach (Item i in items)
            {
                if (i.type == itemType)
                {
                    _items.Add(i);
                }
            }
            foreach (Item j in _items)
            {
                double totalSoldPerDrink = j.soldCount * j.cost;
                Console.WriteLine(j.name + " \t\t" + j.cost + "\t" + j.soldCount + "\t" + totalSoldPerDrink);
                total += totalSoldPerDrink;
            }
            Console.WriteLine(" ------------------------------------------");
            Console.WriteLine("Total: " + total);
            Console.WriteLine(" ------------------------------------------\n\n");
            return total;
        }

        static void InitialDrinks()
        {
            items.Add(new Item("Milktea", 67.00, "Bevarage"));
            items.Add(new Item("Taco", 100.50, "Snack"));
            items.Add(new Item("Coffee", 69.00, "Bevarage"));
            items.Add(new Item("Pizza", 120.99, "Snack"));
            items.Add(new Item("Iced Coffee", 80.00, "Bevarage"));
            items.Add(new Item("Waffle", 50.25, "Snack"));

        }
        static Boolean IsDone(string ActionType)
        {
            Console.WriteLine(" ------------------------------------------");
            Console.WriteLine("Do You Want To Continue With " + ActionType + " ?");
            Console.WriteLine("[0] Yes  \t [1] No");
            Console.WriteLine("");
            int choice = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine(" ------------------------------------------");

            if (choice == 0)
            {
                return false;
            }
            else if (choice == 1)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Invalid Input!");
                IsDone(ActionType);
            }
            return false;

        }
    }
    public class Item
    {
        public string name;
        public double cost;
        public int soldCount = 0;
        public string type;

        public Item(string name, double cost, string type)
        {
            this.name = name;
            this.cost = cost;
            this.type = type;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ShoppingListItems
{
    class Program
    {
        static void Main(string[] args)
        {
            string ShoppingList;
            string[] ShoppingItems;
            int numberOfItems = 0;

            if (!File.Exists("ShoppingList.txt"))
            {
                Console.WriteLine("Shopping list does not exist");
            }
            
            ShoppingList = File.ReadAllText("ShoppingList.txt");
            ShoppingItems = ShoppingList.Split('\n');
            string[][] jaggedShoppingItems = new string[ShoppingItems.Length - 1][];
            
            for (int i = 0; i < ShoppingItems.Length-1; i++)
                {
                string[] a = ShoppingItems[i].Split('|');
                jaggedShoppingItems[i] = new string[a.Length]; 
                Array.Copy(a, jaggedShoppingItems[i], a.Length);

                numberOfItems = numberOfItems + int.Parse(jaggedShoppingItems[i][a.Length-1]);

                //Displaying the jagged array:

                for(int j=0;j<a.Length;j++)
                {
                    Console.WriteLine("Jagged[" + i + "][" + j + "]=" + jaggedShoppingItems[i][j]);
                }
                     
            }
            Console.WriteLine("Total number of items: " + numberOfItems);

            //Searching for a certain item:

            Console.WriteLine("Please write the name of the product, to find out the quantity:");
            string nameOfProduct = Console.ReadLine();
            
            for ( int i = 0; i < jaggedShoppingItems.Length ; i++)
            {
                Console.WriteLine(jaggedShoppingItems[i][0]);

                if(jaggedShoppingItems[i][0] == nameOfProduct)
                {
                    Console.WriteLine("You have to buy: " + jaggedShoppingItems[i][1] + " items of" + nameOfProduct);               
                }
            }
           
            Console.Read();
        }
    }
}

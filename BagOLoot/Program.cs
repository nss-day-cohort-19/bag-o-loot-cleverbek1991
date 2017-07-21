using System;
using System.Collections.Generic;
using System.Linq;

namespace BagOLoot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var db = new DatabaseInterface();
            db.Check();

            Console.WriteLine ("WELCOME TO THE BAG O' LOOT SYSTEM");
            Console.WriteLine ("*********************************");
            Console.WriteLine ("1. Add a child");
            Console.WriteLine ("2. Assign toy to a child");
            Console.WriteLine ("3. Revoke toy from child");
            Console.WriteLine ("4. Review child's toy list");
            Console.WriteLine ("5. Child toy delivery complete");
            Console.WriteLine ("6. Yuletime Delivery Report");
			Console.Write ("> ");

			// Read in the user's choice
			int choice;
			Int32.TryParse (Console.ReadLine(), out choice);

            if (choice == 1)
            {
                Console.WriteLine ("Enter child name");
                Console.Write ("> ");
                string childName = Console.ReadLine();
                ChildRegister registry = new ChildRegister();
                bool childId = registry.AddChild(childName);
                Console.WriteLine(childId);
            }
            else if (choice == 2)
            {
                ChildRegister registry = new ChildRegister();
                Dictionary<int, string> _children = registry.GetChildren();
                Dictionary<int, string> _kids = new Dictionary<int, string>();
                Console.WriteLine ("Assign toy to which child?");
                int i = 1;
                foreach (var child in _children)
                {
                    Console.WriteLine($"{i}. {child.Value}");
                    _kids.Add(i, child.Value);
                    i++;
                }
                Console.Write ("> ");
                int chosenChild = int.Parse(Console.ReadLine());
                int childID = _children.FirstOrDefault(y => y.Value == _kids[chosenChild]).Key;
                Console.WriteLine($"Enter toy to add to {_children[childID]}'s Bag o' Loot");
                string toyName = Console.ReadLine();
                SantaHelper addToy = new SantaHelper();
                addToy.AddToyToBag(toyName, childID);
            }
            else if (choice == 3)
            {
                ChildRegister registry = new ChildRegister();
                Dictionary<int, string> _children = registry.GetChildren();
                Console.WriteLine("Remove toy from which child?");
                int i = 1;
                foreach (var child in _children)
                {
                    Console.WriteLine($"{i++}. {child.Value}");
                }
                Console.Write ("> ");
                int chosenNumber = int.Parse(Console.ReadLine());
                SantaHelper helper = new SantaHelper();
                Dictionary<int, string> childToys = helper.GetAllToysForChild(chosenNumber);
                Console.WriteLine($"Choose toy to revoke from {_children[chosenNumber]}'s Bag o' Loot");
                if (childToys.Count > 0)
                {
                    Dictionary<int, string> toyList = new Dictionary<int, string>();
                    int j = 1;
                    foreach (var toy in childToys)
                    {
                        Console.WriteLine($"{j}. {toy.Value}");
                        toyList.Add(j, toy.Value);
                        j++;
                    }
                    Console.Write("> ");
                    int chosenToy = int.Parse(Console.ReadLine());
                    int toyID = childToys.FirstOrDefault(x => x.Value == toyList[chosenToy]).Key;
                    helper.RemoveToyFromBag(toyID);
                }
                else 
                {
                    Console.WriteLine("\n This child doesn't have any toys. \n");
                }
            }
            else if (choice == 4)
            {
                ChildRegister registry = new ChildRegister();
                SantaHelper helper = new SantaHelper();
                Dictionary<int, string> _children = registry.GetChildren();
                
                Dictionary<int, string> _childrenReference = new Dictionary<int, string>();
                Console.WriteLine("View Bag o' Loot for which child?");
                int i = 1;
                foreach (var child in _children)
                {
                    Console.WriteLine($"{i}. {child.Value}");
                    _childrenReference.Add(i, child.Value);
                    i++;
                }
                Console.WriteLine("> ");
                int chosenChild = int.Parse(Console.ReadLine());
                int childID = _children.FirstOrDefault(x => x.Value == _childrenReference[chosenChild]).Key;
                Dictionary<int, string> childToys = helper.GetAllToysForChild(chosenChild);
                if (_children.Count > 0)
                {
                    int j = 1;
                    foreach (var toy in childToys)
                    {
                        Console.WriteLine($"{j}. {toy.Value}");
                    }
                }
                else
                {
                    Console.WriteLine("\n This child doesn't have any toys. \n");
                }
            }
            else if (choice == 5)
            {
                ChildRegister registry = new ChildRegister();
                SantaHelper helper = new SantaHelper();
                DeliveryReport isDelivered = new DeliveryReport();
                Dictionary<int, string> _children = registry.GetChildren();
                Dictionary<int, string> _childrenReference = new Dictionary<int, string>();
                Console.WriteLine("Which child had all of their toys delivered?");
                int i = 1;
                foreach (var child in _children)
                {
                    Console.WriteLine($"{i}. {child.Value}");
                    _childrenReference.Add(i, child.Value);
                    i++;
                }
                Console.WriteLine("> ");
                int chosenChild = int.Parse(Console.ReadLine());
                isDelivered.Delivered(chosenChild);
            }
            else if (choice == 6)
            {
                Console.WriteLine("\n Yuletime Delivery Report \n %%%%%%%%%%%%%%%%%%%%%%%% \n");
                ChildRegister registry = new ChildRegister();
                SantaHelper helper = new SantaHelper();
                DeliveryReport isDelivered = new DeliveryReport();
                Dictionary<int, string> _children = isDelivered.GetAllToysForChild();
                foreach (var child in _children)
                {
                    Console.WriteLine($"{child.Value}");
                    Dictionary<int, string> childToys = helper.GetAllToysForChild(child.Key);
                    int i = 1;
                    foreach (var toy in childToys)
                    {
                        Console.WriteLine($"  {i++}. {toy.Value}");
                    }
                    Console.WriteLine("\n *********************** \n");
                }
            }
        }
    }
}

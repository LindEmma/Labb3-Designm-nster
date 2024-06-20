namespace Labb3_Designmönster
{
    using System;
    using System.Collections.Generic;

    // Designmönster - labb 3 implementera
    // Emma Lind .NET23

    // The design pattern is Factory method
    // I added coffee, hot chocolate and cappuccino, without changing the pattern or making changes to the previous code

    namespace VarmDrinkStation
    {
        // Interface for warm drinks
        public interface IWarmDrink
        {
            void Consume(); // Method to consume the drink
        }

        // concrete class water, implements the Consume method
        internal class Water : IWarmDrink
        {
            public void Consume()
            {
                Console.WriteLine("Warm water is served."); // console output that is unique to the drink
            }
        }

        // additional concrete classes coffee, cappuccino and hot chocolate
        internal class Coffee : IWarmDrink
        {
            public void Consume()
            {
                Console.WriteLine("Coffee is served.");
            }
        }
        internal class Cappuccino : IWarmDrink
        {
            public void Consume()
            {
                Console.WriteLine("Cappucino is served.");
            }
        }
        internal class HotChocolate : IWarmDrink
        {
            public void Consume()
            {
                Console.WriteLine("Hot chocolate is served.");
            }
        }

        // Factory interface that creates the drinks
        public interface IWarmDrinkFactory
        {
            IWarmDrink Prepare(int total);
        }

        // factories for each of the concrete classes
        internal class HotWaterFactory : IWarmDrinkFactory
        {
            public IWarmDrink Prepare(int total)
            {
                Console.WriteLine($"Pour {total} ml hot water in your cup");
                return new Water(); // Returns new instance of the class
            }
        }
        internal class CoffeeFactory : IWarmDrinkFactory
        {
            public IWarmDrink Prepare(int total)
            {
                Console.WriteLine($"Pour {total} ml coffee in your cup");
                return new Coffee();
            }
        }
        internal class CappuccinoFactory : IWarmDrinkFactory
        {
            public IWarmDrink Prepare(int total)
            {
                Console.WriteLine($"Pour {total} ml cappuccino in your cup");
                return new Cappuccino();
            }
        }
        internal class HotChocolateFactory : IWarmDrinkFactory
        {
            public IWarmDrink Prepare(int total)
            {
                Console.WriteLine($"Pour {total} ml hot chocolate in your cup");
                return new HotChocolate();
            }
        }

        // handles the preparation of warm drinks
        public class WarmDrinkMachine
        {
            private readonly List<Tuple<string, IWarmDrinkFactory>> namedFactories; // list of all factories

            public WarmDrinkMachine()
            {
                namedFactories = new List<Tuple<string, IWarmDrinkFactory>>();

                RegisterFactory<HotWaterFactory>("Hot Water");
                RegisterFactory<CoffeeFactory>("Coffee");
                RegisterFactory<CappuccinoFactory>("Cappuccino");
                RegisterFactory<HotChocolateFactory>("Hot chocolate");
            }

            // Method for registering a factory
            private void RegisterFactory<T>(string drinkName) where T : IWarmDrinkFactory, new()
            {
                namedFactories.Add(Tuple.Create(drinkName, (IWarmDrinkFactory)Activator.CreateInstance(typeof(T)))); // adds factories to list namedFactories
            }

            // method to make a drink
            public IWarmDrink MakeDrink()
            {
                Console.WriteLine("This is what we serve today:");
                for (var index = 0; index < namedFactories.Count; index++)
                {
                    var tuple = namedFactories[index];
                    Console.WriteLine($"{index}: {tuple.Item1}"); // prints all types of warm beverages from the list
                }
                Console.WriteLine("Select a number to continue:");
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out int i) && i >= 0 && i < namedFactories.Count) // lets user choose a drink
                    {
                        Console.Write("How much: ");
                        if (int.TryParse(Console.ReadLine(), out int total) && total > 0) // lets user choose how many ml
                        {
                            return namedFactories[i].Item2.Prepare(total); // creates chosen drink with the Prepare method
                        }
                    }
                    Console.WriteLine("Something went wrong with your input, try again."); // Error message
                }
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                var machine = new WarmDrinkMachine(); // instantiates WarmDrinkMachine
                IWarmDrink drink = machine.MakeDrink(); // makes drink
                drink.Consume(); // consumes drink
            }
        }
    }

}

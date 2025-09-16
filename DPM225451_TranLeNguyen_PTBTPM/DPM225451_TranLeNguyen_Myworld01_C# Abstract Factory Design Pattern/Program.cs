using System;

namespace MyWorld.AbstractFactory
{
    // Abstract products
    public abstract class Herbivore { }
    public abstract class Carnivore
    {
        public abstract void Eat(Herbivore h);
    }

    // Concrete products for one “world”
    public class ForestHerbivore : Herbivore { }
    public class ForestCarnivore : Carnivore
    {
        public override void Eat(Herbivore h)
        {
            Console.WriteLine($"{GetType().Name} eats {h.GetType().Name}");
        }
    }

    // Another set of products for a different “world”
    public class DesertHerbivore : Herbivore { }
    public class DesertCarnivore : Carnivore
    {
        public override void Eat(Herbivore h)
        {
            Console.WriteLine($"{GetType().Name} eats {h.GetType().Name}");
        }
    }

    // Abstract factory
    public abstract class WorldFactory
    {
        public abstract Herbivore CreateHerbivore();
        public abstract Carnivore CreateCarnivore();
    }

    // Concrete factories
    public class ForestWorldFactory : WorldFactory
    {
        public override Herbivore CreateHerbivore()
        {
            return new ForestHerbivore();
        }

        public override Carnivore CreateCarnivore()
        {
            return new ForestCarnivore();
        }
    }

    public class DesertWorldFactory : WorldFactory
    {
        public override Herbivore CreateHerbivore()
        {
            return new DesertHerbivore();
        }

        public override Carnivore CreateCarnivore()
        {
            return new DesertCarnivore();
        }
    }

    // Client
    public class MyWorld
    {
        private Herbivore _herbivore;
        private Carnivore _carnivore;

        public MyWorld(WorldFactory factory)
        {
            _herbivore = factory.CreateHerbivore();
            _carnivore = factory.CreateCarnivore();
        }

        public void RunFoodChain()
        {
            _carnivore.Eat(_herbivore);
        }
    }

    // Test / Main
    class Program
    {
        static void Main(string[] args)
        {
            // Use Forest world
            WorldFactory forest = new ForestWorldFactory();
            MyWorld world1 = new MyWorld(forest);
            world1.RunFoodChain();  // expect: ForestCarnivore eats ForestHerbivore

            // Use Desert world
            WorldFactory desert = new DesertWorldFactory();
            MyWorld world2 = new MyWorld(desert);
            world2.RunFoodChain();  // expect: DesertCarnivore eats DesertHerbivore

            Console.ReadKey();
        }
    }
}

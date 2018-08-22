using Autofac;
using System;

namespace consoleAppCoreAutofacSandbox03
{
    public interface ILog
    {
        void Write(string message);
    }

    public class ConsoleLog : ILog
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class Engine
    {
        private ILog log;
        private int id;

        public Engine(ILog log)
        {
            this.log = log;
            id = new Random().Next();
        }

        public Engine(ILog log, int id)
        {
            this.log = log;
            this.id = id;
        }

        public void Ahead(int power)
        {
            log.Write($"Engine [{id}] ahead {power}");
        }
    }

    public class Car
    {
        private Engine engine;
        private ILog log;

        public Car(Engine engine, ILog log)
        {
            this.engine = engine;
            this.log = log;
        }

        public void Go()
        {
            engine.Ahead(100);
            log.Write("Car going forward...");
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>().As<ILog>();
            //builder.RegisterType<Engine>();


            //var log = new ConsoleLog();
            //var engine = new Engine(log);
            //var car = new Car(engine, log);
            //car.Go();

            //var motor = new Engine(new ConsoleLog(), 456);
            //builder.RegisterInstance(motor);
            builder.Register((IComponentContext c) => new Engine(c.Resolve<ILog>(), 456));
            builder.RegisterType<Car>();

            var container = builder.Build();
            var carro = container.Resolve<Car>();
            carro.Go();

            Console.ReadKey();
        }
    }
}

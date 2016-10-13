using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Messages;
using ClassLibrary1;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace ConsoleApp1
{
    class MessageHandlerBuilder
    {
        private IContainer _container;

        public MessageHandlerBuilder(IContainer container)
        {
            _container = container;
        }
        public void HandleMessages<T>(T message)
        {
            var handlers = _container.Resolve<IEnumerable<IHandleMessage<T>>>();

            foreach (var handler in handlers)
            {
                handler.Handle(message);
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            var dataAccess = Assembly.GetEntryAssembly();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                   .Where(t => t.Name.EndsWith("Handler"))
                   .AsImplementedInterfaces();

            var container = builder.Build();

            var messageHandlers = new MessageHandlerBuilder(container);
            var message = new MessageA();

            typeof(MessageHandlerBuilder).GetMethod("HandleMessages").MakeGenericMethod(message.GetType()).Invoke(messageHandlers, new object[] { message });

            //messageHandlers.HandleMessages(new MessageA());
        }
    }

    //http://www.michael-whelan.net/replacing-appdomain-in-dotnet-core/
    public class AppDomain
    {
        public static AppDomain CurrentDomain { get; private set; }

        static AppDomain()
        {
            CurrentDomain = new AppDomain();
        }

        public Assembly[] GetAssemblies()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;
            foreach (var library in dependencies)
            {
                if (IsCandidateCompilationLibrary(library))
                {
                    try
                    {
                        var assembly = Assembly.Load(new AssemblyName(library.Name));
                        assemblies.Add(assembly);
                    }
                    catch (Exception)
                    {   
                    }
                }
            }
            return assemblies.ToArray();
        }

        private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary)
        {
            return true;
        }
    }
}

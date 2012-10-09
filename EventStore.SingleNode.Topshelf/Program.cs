using System;
using Topshelf;

namespace EventStore.SingleNode.Topshelf
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<SingleNodeWithProjections>(s =>
                {
                    s.ConstructUsing(name => new SingleNodeWithProjections());
                    s.WhenStarted(tc => tc.Run(new [] { "--db", "c:\\temp\\db", "-t", "3000", "--ip", "127.0.0.1", "-h", "3001", }));
                    s.WhenStopped(tc => tc.Stop());
                });

                x.UseNLog();

                x.RunAsNetworkService();
                x.SetDescription("EventStore Topshelf");
                x.SetDisplayName("EventStore");
                x.SetServiceName("EventStore");
            });
        }
    }
}

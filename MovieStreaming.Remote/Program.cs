using System;
using System.Threading.Tasks;
using Akka.Actor;
using MovieStreaming.Common;

namespace MovieStreaming.Remote
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static async Task Main(string[] args)
        {
            ;
            ColorConsole.WriteLineGray("Creating MovieStreamingActorSystem in remote process.");
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            
            await MovieStreamingActorSystem.WhenTerminated;
        }
    }
}

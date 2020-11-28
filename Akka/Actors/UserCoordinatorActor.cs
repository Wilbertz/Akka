using System;
using System.Collections.Generic;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;

        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(m =>
            {
                CreateChildUserIfNotExists(m.UserId);

                IActorRef childActorRef = _users[m.UserId];
                childActorRef.Tell(m);

            });

            Receive<StopMovieMessage>(m =>
            {
                CreateChildUserIfNotExists(m.UserId);

                IActorRef childActorRef = _users[m.UserId];
                childActorRef.Tell(m);

            });
        }

        private void CreateChildUserIfNotExists(int userId)
        {
            if (!_users.ContainsKey(userId))
            {
                IActorRef newChildActorRef =
                    Context.ActorOf(Props.Create(() => new UserActor(userId)), "User" + userId);

                _users.Add(userId, newChildActorRef);

                ColorConsole.WriteLineCyan(string.Format("UserCoordinatorActor created new child UserActor for {0} (Total Users: {1})", userId, _users.Count));
            }
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineCyan("UserCoordinatorActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineCyan("UserCoordinatorActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineCyan("UserCoordinatorActor PreRestart because: " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineCyan("UserCoordinatorActor PostRestart because: " + reason);

            base.PostRestart(reason);
        }
    }
}

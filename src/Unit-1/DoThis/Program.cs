using Akka.Actor;

namespace WinTail
{
    #region Program

    class Program
    {
        public static ActorSystem MyActorSystem;

        static void Main(string[] args)
        {
            // initialize MyActorSystem
            MyActorSystem = ActorSystem.Create(nameof(MyActorSystem));

            var consoleWriterProps = Props.Create<ConsoleWriterActor>();
            var consoleWriterActor = MyActorSystem.ActorOf(consoleWriterProps, nameof(ConsoleWriterActor));

            var tailCoordinatorProps = Props.Create<TailCoordinatorActor>();
            var tailCoordinatorActor = MyActorSystem.ActorOf(tailCoordinatorProps, nameof(TailCoordinatorActor));

            var fileValidatorProps = Props.Create(
                () => new FileValidatorActor(consoleWriterActor, tailCoordinatorActor));
            var validationActor = MyActorSystem.ActorOf(fileValidatorProps, nameof(FileValidatorActor));

            var consoleReaderProps = Props.Create<ConsoleReaderActor>(validationActor);
            var consoleReaderActor = MyActorSystem.ActorOf(consoleReaderProps, nameof(ConsoleReaderActor));

            // tell console reader to begin
            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);

            // blocks the main thread from exiting until the actor system is shut down
            MyActorSystem.WhenTerminated.Wait();
        }
    }

    #endregion
}
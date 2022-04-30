using System;
using System.Collections.Generic;

namespace DesignPatterns_CSharp.behavioural
{
    public interface ICommand // it could be an abstract class as well depending on the contexy
    {
        void Execute();
    }
    public class SimpleCommand : ICommand
    {
        private string payload = String.Empty;
        public SimpleCommand(string payload)
        {
            this.payload = payload;
        }
        public void Execute()
        {
            Console.WriteLine($"SimpleCommand: See, I can do simple things also like printing {payload}.");
        }
    }
    public class NormalWorkerCommand : ICommand
    {
        private Receiver receiver;
        private string mainWork;
        private string parallelWork;
        public NormalWorkerCommand(Receiver receiver, string mainWork, string parallelWork)
        {
            this.receiver = receiver;
            this.mainWork = mainWork;
            this.parallelWork = parallelWork;
        }
        public void Execute()
        {
            Console.WriteLine("NormalWorkerCommand: calling the normal worker to perform it's operations...");
            receiver.DoSomething(mainWork);
            receiver.DoSomethingElse(parallelWork);
        }
    }
    public class ChineseWorkerCommand : ICommand
    {
        private Receiver_china chineseReceiver;
        private string mainWork;
        private string parallelWork;
        public ChineseWorkerCommand(Receiver_china chineseReceiver, string mainWork, string parallelWork)
        {
            this.chineseReceiver = chineseReceiver;
            this.mainWork = mainWork;
            this.parallelWork = parallelWork;
        }
        public void Execute()
        {
            Console.WriteLine("ChineseWorkerCommand: calling the chinese worker to perform it's operations...");
            chineseReceiver.Work(mainWork);
            chineseReceiver.WorkOnSomethingElse(parallelWork);
        }
    }
    public class CombinationCommand : ICommand
    {
        private List<ICommand> commands;
        public CombinationCommand(List<ICommand> commands)
        {
            this.commands = commands;
        }
        public void Execute()
        {
            foreach(ICommand c in commands)
            {
                c.Execute();
            }
        }
    }
    public class Receiver
    {
        public void DoSomething(string a)
        {
            Console.WriteLine($"Receiver: working on {a}");
        }
        public void DoSomethingElse(string b)
        {
            Console.WriteLine($"Receiver: Also working on {b}");
        }
    }
    public class Receiver_china
    {
        public void Work(string a)
        {
            Console.WriteLine($"Receiver_china: Working on {a} in chinese");
        }
        public void WorkOnSomethingElse(string b)
        {
            Console.WriteLine($"Receiver_china: Working on {b} in chinese");
        }
    }
    public class Invoker
    {
        private ICommand onStart;
        private ICommand onFinish;
        public Invoker(ICommand onStart, ICommand onFinish)
        {
            this.onStart = onStart;
            this.onFinish = onFinish;
        }
        public void DoSomethingImportant()
        {
            Console.WriteLine("Invoker: Does anybody want something done before I start?");
            this.onStart.Execute();
            //Console.WriteLine("Hi!");
            Console.WriteLine("Invoker: ...doing something really important");
            Console.WriteLine("Invoker: Does anybody wants to do something after I finish?");
            this.onFinish.Execute();
            // var worker = new Receiver();
            // worker.DoSomething("OOPS");
            // worker.DoSomethingElse("Design Patterns");
            // var worker_china = new Receiver_china();
            // worker_china.Work("OOPS");
            // worker_china.WorkOnSomethingElse("Design Patterns");
        }
    }
    public class Invoker_advance
    {
        private ICommand onStart;
        private ICommand onFinish;
        public Invoker_advance(ICommand onStart, ICommand onFinish)
        {
            this.onStart = onStart;
            this.onFinish = onFinish;
        }
        public void DoSomethingAdvanced()
        {
            Console.WriteLine("Invoker_advance: Hey, Does anybody want something done before I start?");
            this.onStart.Execute();
            // Console.WriteLine("Hi Advance Invoker!");
            Console.WriteLine("Invoker_advance: ... doing something really import and advanced");
            Console.WriteLine("Invoker_advance: Does anybody want to do something after I finish?");
            this.onFinish.Execute();
            // var worker = new Receiver();
            // worker.DoSomething("OOPS");
            // worker.DoSomethingElse("Design Patterns");
            // var worker_china = new Receiver_china();
            // worker_china.Work("OOPS");
            // worker_china.WorkOnSomethingElse("Design Patterns");
        }
    }
    public class Client_command_conc
    {
        public void ClientCode()
        {
            ICommand onStartInvoker = new SimpleCommand("Hi!");
            Receiver regularWorker = new Receiver();
            Receiver_china chineseWorker = new Receiver_china();
            string mainWork = "OOPS";
            string parallelWork = "Design Patterns";
            ICommand normalWorkerOperations = new NormalWorkerCommand(regularWorker, mainWork, parallelWork);
            ICommand chineseWorkerOperations = new ChineseWorkerCommand(chineseWorker, mainWork, parallelWork);
            ICommand onFinishInvoker = new CombinationCommand(new List<ICommand>(){normalWorkerOperations, chineseWorkerOperations});
            Invoker invoker = new Invoker(onStartInvoker, onFinishInvoker);
            invoker.DoSomethingImportant();
            ICommand onStartInvoker_advance = new SimpleCommand("Hi Advance Invoker!");
            Invoker_advance advanceInvoker = new Invoker_advance(onStartInvoker_advance, onFinishInvoker);
            advanceInvoker.DoSomethingAdvanced();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> testList = new List<int>();

            TestClass tc = new TestClass();

            tc.Action1();
            tc.Action2();
            tc.CancelLastOperation();
            tc.RepeatLastOperation();
            tc.Action3();
            tc.CancelLastOperation();
            tc.CancelLastOperation();
            tc.CancelLastOperation();
            tc.RepeatLastOperation();
            tc.RepeatLastOperation();
            tc.RepeatLastOperation();
            tc.Action1();
            tc.Action1();
            tc.Action1();
            tc.CancelLastOperation();

            Console.ReadLine();
        }
    }

    internal class TestClass
    {
        private readonly CancelableCommandsManager commandsManager
            = new CancelableCommandsManager();

        internal void CancelLastOperation()
        {
            if (this.commandsManager.CanCancel)
            {
                this.commandsManager.CancelLastAction();
            }
            else
            {
                Console.WriteLine("Операция не может быть отменена.");
            }
        }

        internal void RepeatLastOperation()
        {
            if (this.commandsManager.CanRepeat)
            {
                this.commandsManager.RepeatLastCanceledAction();
            }
            else
            {
                Console.WriteLine("Операция не может быть повторена.");
            }
        }

        internal void Action1()
        {
            this.commandsManager.Perform(
                this.Action1Perform,
                this.Action1Cancel,
                this.Action1Repeat);
        }

        private void Action1Perform()
        {
            Console.WriteLine("Выполнение операции 1...");
        }

        private void Action1Cancel()
        {
            Console.WriteLine("Отмена операции 1");
        }

        private void Action1Repeat()
        {
            Console.WriteLine("Выполнение операции 1 (после отмены)... ");
        }

        internal void Action2()
        {
            this.commandsManager.Perform(
                () => Console.WriteLine("Выполнение операции 2..."),
                () => Console.WriteLine("Отмена операции 2"),
                () => Console.WriteLine("Выполнение операции 2 (после отмены)..."));
        }

        internal void Action3()
        {
            this.commandsManager.Perform(
                () => Console.WriteLine("Выполнение операции 3..."),
                () => Console.WriteLine("Отмена операции 3"),
                () => Console.WriteLine("Выполнение операции 3 (после отмены)..."));
        }
    }

    /// <summary>
    /// Отменяемое действие пользователя.
    /// </summary>
    internal class UserCommand
    {
        /// <summary>
        /// Действие, необходимое для отмены операции.
        /// </summary>
        private readonly Action cancelAction;

        /// <summary>
        /// Действие, необходимое для повтора операции.
        /// </summary>
        private readonly Action repeatAction;

        /// <summary>
        /// Создать отменяемое действие пользователя.
        /// </summary>
        /// <param name="cancelAction">Действие для отмены операции.</param>
        /// <param name="repeatAction">Действие для повтора операции.</param>
        internal UserCommand(
            Action cancelAction,
            Action repeatAction)
        {
            this.cancelAction = cancelAction;
            this.repeatAction = repeatAction;
        }

        /// <summary>
        /// Отменить операцию пользователя.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Если действие для отмены операции не было назначено.
        /// </exception>
        internal void Cancel()
        {
            if (this.cancelAction == null)
            {
                throw new InvalidOperationException(
                    "Невозможно отменить операцию пользователя, "
                    + "т.к. действие для отмены не было назначено.");
            }

            this.cancelAction();
        }

        /// <summary>
        /// Повторить операцию пользователя.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Если действие для повтора операции не было назначено.
        /// </exception>
        internal void Repeat()
        {
            if (this.cancelAction == null)
            {
                throw new InvalidOperationException(
                    "Невозможно повторить операцию пользователя, "
                    + "т.к. действие для повтора операции не было назначено.");
            }

            this.repeatAction();
        }
    }

    /// <summary>
    /// Механизм выполнения отменяемых и повторяемых действий.
    /// </summary>
    internal class CancelableCommandsManager
    {
        /// <summary>
        /// Упорядоченный список команд, которые могут быть отменены.
        /// </summary>
        private List<UserCommand> cancelableCommands
            = new List<UserCommand>();

        /// <summary>
        /// Упорядоченный список отмененных команд, выполнение 
        /// которых возможно повторить.
        /// </summary>
        private List<UserCommand> repeatableCommands
            = new List<UserCommand>();

        /// <summary>
        /// Возможна отмена последнего действия.
        /// </summary>
        internal bool CanCancel
        {
            get
            {
                return this.cancelableCommands.Any();
            }
        }

        /// <summary>
        /// Возможен повтор последнего отмененного действия.
        /// </summary>
        internal bool CanRepeat
        {
            get
            {
                return this.repeatableCommands.Any();
            }
        }

        /// <summary>
        /// Выполнить действие с возможностью дальнейшей отмены и повтора.
        /// </summary>
        /// <param name="action">Выполняемое действие.</param>
        /// <param name="cancelAction">
        /// Действие, необходимое для отмены.</param>
        /// <param name="repeatAction">
        /// Действие, необходимое для повтора после отмены.</param>
        internal void Perform(
            Action action,
            Action cancelAction,
            Action repeatAction)
        {
            action();
            this.cancelableCommands.Insert(
                this.cancelableCommands.Count,
                new UserCommand(cancelAction, repeatAction));
            this.repeatableCommands.Clear();
        }

        /// <summary>
        /// Отменить последнее действие.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Если не было выполнено действий с возможностью отмены,
        /// или все действия уже были отменены.
        /// </exception>
        internal void CancelLastAction()
        {
            if (!this.CanCancel)
            {
                throw new InvalidOperationException(
                    "Невозможно отменить последнее действие, "
                    + "т.к. еще не было выполнено действий с "
                    + "возможностью отмены, либо все действия"
                    + " уже были отменены.");
            }

            this.cancelableCommands.Last().Cancel();
            this.repeatableCommands.Insert(
                this.repeatableCommands.Count,
                this.cancelableCommands.Last());
            this.cancelableCommands.Remove(
                this.cancelableCommands.Last());
        }

        /// <summary>
        /// Повторить последнее отмененное действие.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Если действия не были отменены.
        /// </exception>
        internal void RepeatLastCanceledAction()
        {
            if (!this.CanRepeat)
            {
                throw new InvalidOperationException(
                    "Невозможно повторить последнее отмененное действие, "
                    + "так как ни одно действие не было отменено.");
            }

            this.repeatableCommands.Last().Repeat();
            this.cancelableCommands.Insert(
                this.cancelableCommands.Count,
                this.repeatableCommands.Last());
            this.repeatableCommands.Remove(
                this.repeatableCommands.Last());
        }
    }
}

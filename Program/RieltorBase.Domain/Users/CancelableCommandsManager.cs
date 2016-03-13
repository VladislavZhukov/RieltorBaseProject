namespace RieltorBase.Domain.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
                this.cancelableCommands.Count - 1,
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
                this.repeatableCommands.Count - 1,
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
                this.cancelableCommands.Count - 1,
                this.repeatableCommands.Last());
            this.repeatableCommands.Remove(
                this.repeatableCommands.Last());
        }
    }
}

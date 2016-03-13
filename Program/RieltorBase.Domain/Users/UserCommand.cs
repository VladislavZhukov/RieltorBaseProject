namespace RieltorBase.Domain.Users
{
    using System;

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
}

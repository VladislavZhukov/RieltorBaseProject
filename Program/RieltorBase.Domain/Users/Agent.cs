namespace RieltorBase.Domain.Users
{
    public class Agent
    {
        private object[] currentAppartments;

        /// <summary>
        /// Создать агента.
        /// </summary>
        internal Agent()
        {
            // получить собственные квартиры агента.
            this.currentAppartments = null; 
        }

        /// <summary>
        /// Отменить последнее действие.
        /// </summary>
        public void CancelLastOperation()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Повторить отмененное действие.
        /// </summary>
        public void RepeatLastOperation()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Получить 
        /// </summary>
        /// <returns></returns>
        public object[] GetOwnAppartments()
        {
            // получить свои квартиры
            throw new System.NotImplementedException();
        }

        private void UpdateAppartment(object appartment)
        {
            throw new System.NotImplementedException();
        }

        private void AddAppartment(object appartment)
        {
            throw new System.NotImplementedException();
        }

        private void RemoveAppartment(object appartment)
        {
            throw new System.NotImplementedException();
        }
    }
}

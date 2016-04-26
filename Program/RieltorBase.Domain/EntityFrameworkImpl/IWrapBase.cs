namespace RieltorBase.Domain.EntityFrameworkImpl
{
    public interface IWrapBase<TRealObject, TInterface> 
        where TRealObject : new()
    {
        /// <summary>
        /// Получить обертываемый объект.
        /// </summary>
        /// <returns>Реальный объект (EF Entity).</returns>
        TRealObject GetRealObject();

        /// <summary>
        /// Обновить реальный объект EF в соответствии со свойствами обертки.
        /// </summary>
        /// <param name="realObject">Реальный объект.</param>
        void UpdateRealObject(TRealObject realObject);
    }
}

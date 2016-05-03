namespace RieltorBase.Domain.EntityFrameworkImpl
{
    public interface IWrapBase<TRealObject, TInterface> 
        where TRealObject : new()
    {
        /// <summary>
        /// Получить обертываемый объект.
        /// </summary>
        /// <returns>Реальный объект (EF Entity).</returns>
        /// <remarks>Может быть не связан с контекстом БД.</remarks>
        TRealObject GetRealObject();
    }
}

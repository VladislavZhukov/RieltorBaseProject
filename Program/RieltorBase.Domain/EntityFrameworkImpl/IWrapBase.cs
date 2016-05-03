namespace RieltorBase.Domain.EntityFrameworkImpl
{
    /// <summary>
    /// Общий интерфейс класса-обертки другого класса.
    /// </summary>
    /// <typeparam name="TRealObject">Тип "оборачиваемого" класса.</typeparam>
    public interface IWrapBase<out TRealObject>
        where TRealObject : new()
    {
        /// <summary>
        /// Получить обертываемый объект.
        /// </summary>
        /// <returns>Реальный ("оборачиваемый") объект.</returns>
        TRealObject GetRealObject();
    }
}

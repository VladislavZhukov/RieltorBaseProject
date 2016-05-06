namespace RieltorBase.WebSite
{
    using Ninject;

    using RieltorBase.Domain.EntityFrameworkImpl;
    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// Класс, определяющий, какие конкретные реализации 
    /// интерфейсов использовать (на основе Ninject).
    /// </summary>
    internal class RBDependencyResolver
    {
        /// <summary>
        /// Экземпляр класса.
        /// </summary>
        private static readonly RBDependencyResolver instance
            = new RBDependencyResolver();

        /// <summary>
        /// Текущий используемый "определитель" реализаций.
        /// </summary>
        internal static RBDependencyResolver Current
        {
            get
            {
                return RBDependencyResolver.instance;
            }
        }

        /// <summary>
        /// Ninject-ядро.
        /// </summary>
        private readonly StandardKernel kernel 
            = new StandardKernel();

        /// <summary>
        /// Закрытый конструктор.
        /// </summary>
        private RBDependencyResolver()
        {
            this.SetEFRealization();
        }

        /// <summary>
        /// Получить реализацию, назначенную для определенного интерфейса.
        /// </summary>
        /// <typeparam name="T">Интерфейс, для которого требуется получить реализацию.</typeparam>
        /// <returns>Назначенная (установленная) реализация требуемого интерфейса.</returns>
        public T Resolve<T>()
        {
            return this.kernel.Get<T>();
        }

        /// <summary>
        /// Назначить реализацию на основе Entity Framework в качестве используемой.
        /// </summary>
        private void SetEFRealization()
        {
            this.kernel.Bind<IRealtyBaseContext>().To<RealtyBaseContext>();
            this.kernel.Bind<IFirmsRepository>().To<FirmsRepository>();
            this.kernel.Bind<IRealtyObjectsRepository>().To<RealtyObjectsRepository>();
            this.kernel.Bind<IRepository<IPhoto>>().To<PhotosRepository>();

            this.kernel.Bind<IAuthenticationMechanism>().To<EFAuthorization>();
            this.kernel.Bind<IAuthorizationMechanism>().To<EFAuthorization>();
        }
    }
}
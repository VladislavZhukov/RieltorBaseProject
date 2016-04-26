namespace RieltorBase.WebSite
{
    using Ninject;

    using RieltorBase.Domain.EntityFrameworkImpl;
    using RieltorBase.Domain.Interfaces;

    internal class RBDependencyResolver
    {
        private static readonly RBDependencyResolver instance
            = new RBDependencyResolver();

        internal static RBDependencyResolver Current
        {
            get
            {
                return RBDependencyResolver.instance;
            }
        }

        private readonly StandardKernel kernel 
            = new StandardKernel();

        private RBDependencyResolver()
        {
            this.kernel.Bind<IRealtyBaseContext>().To<RealtyBaseContext>();
            this.kernel.Bind<IFirmsRepository>().To<FirmsRepository>();
            this.kernel.Bind<IRealtyObjectsRepository>().To<RealtyObjectsRepository>();
            this.kernel.Bind<IRepository<IPhoto>>().To<PhotosRepository>();
        }

        public T Resolve<T>()
        {
            return this.kernel.Get<T>();
        }
    }
}
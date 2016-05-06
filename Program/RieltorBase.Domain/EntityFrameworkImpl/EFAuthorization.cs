namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using RieltorBase.Domain.Interfaces;

    public class EFAuthorization : IAuthenticationMechanism, IAuthorizationMechanism
    {
        public UserInfo Authenticate(string login, string password)
        {
            throw new System.NotImplementedException();
        }

        public bool CanUserAddRealtyObject(UserInfo user, IRealtyObject @object)
        {
            throw new System.NotImplementedException();
        }

        public bool CanUserUpdateRealtyObject(UserInfo user, IRealtyObject @object)
        {
            throw new System.NotImplementedException();
        }

        public bool CanUserDeleteRealtyObject(UserInfo user, IRealtyObject @object)
        {
            throw new System.NotImplementedException();
        }

        public bool IsUserFirmAdmin(UserInfo user, IFirm firm)
        {
            throw new System.NotImplementedException();
        }
    }
}

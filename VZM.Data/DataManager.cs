using VZM.Interfaces;
using VZM;

namespace VZM.Data
{
    public class DataManager
    {
        public IProductRepository Products { get; set; }
        public IUserRepository Users { get; set; }
        public IRoleRepository Roles { get; set; }
        public AuthorizedUser AuthorizedUser { get; set; }

        public DataManager(IProductRepository productRepository, IUserRepository userRepository , IRoleRepository roleRepository, AuthorizedUser authorizedUser)
        {
            Products = productRepository;
            Users = userRepository;
            Roles = roleRepository;
            AuthorizedUser = authorizedUser;
        }
    }
}

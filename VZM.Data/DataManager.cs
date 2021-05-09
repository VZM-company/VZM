using VZM.Interfaces;

namespace VZM.Data
{
    public class DataManager
    {
        public IProductRepository Products { get; set; }
        public IUserRepository Users { get; set; }
        public IRoleRepository Roles { get; set; }

        public DataManager(IProductRepository productRepository, IUserRepository userRepository , IRoleRepository roleRepository)
        {
            Products = productRepository;
            Users = userRepository;
            Roles = roleRepository;
        }
    }
}

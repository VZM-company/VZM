using VZM.Interfaces;

namespace VZM.Data
{
    public class DataManager
    {
        public IProductRepository Products { get; set; }
        public IUserRepository Users { get; set; }

        public DataManager(IProductRepository productRepository, IUserRepository userRepository)
        {
            Products = productRepository;
            Users = userRepository;
        }
    }
}

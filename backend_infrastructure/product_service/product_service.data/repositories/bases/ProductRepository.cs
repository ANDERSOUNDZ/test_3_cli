using product_service.data.context;

namespace product_service
{
    public partial class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;
        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }
    }
}

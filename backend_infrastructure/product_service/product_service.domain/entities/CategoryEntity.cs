namespace product_service.domain.entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    }
}

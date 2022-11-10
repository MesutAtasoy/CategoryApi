namespace CategoryApi.Domain.Entities;

public class Category
{
    public Category()
    {
        Products = new List<Product>();
    }
    
    public long Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}
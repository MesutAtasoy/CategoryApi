namespace CategoryApi.Domain.Entities;
public class Product
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
}

namespace CategoryApi.Application.Products.Dto.Request;

public class CreateProductDto
{
    public long CategoryId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
}
using CategoryApi.Application.Products.Dto;
using CategoryApi.Application.Products.Dto.Request;
using CategoryApi.Application.Products.Services;
using CategoryApi.Application.Shared.Models;
using Framework.Models;
using Microsoft.AspNetCore.Mvc;

namespace CategoryApi.Api.Controllers;

/// <summary>
/// product
/// </summary>
[Route("v1/products")]
[ApiController]
[Produces("application/json", "application/xml")]
[Consumes("application/json", "application/xml")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="productService"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public ProductController(IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }
    
    /// <summary>
    /// Get a single product by product id
    /// </summary>
    /// <response code="200">The product was found</response>
    /// <response code="404">The product was not found</response>
    /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
    /// <response code="415">When a response is specified in an unsupported content type</response>
    /// <response code="422">If query params structure is valid, but the values fail validation</response>
    /// <response code="500">A server fault occurred</response>
    [HttpGet("{productId}", Name = nameof(GetProductById))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ProductDto>> GetProductById([FromRoute] long productId)
    {
        var product = await _productService.GetByIdAsync(productId).ConfigureAwait(true);

        if (product == null)
            return NotFound();

        return Ok(product);
    }


    /// <summary>
    /// Create a new product
    /// </summary>
    /// <response code="201">The product was created successfully. Also includes 'location' header to newly created product</response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
    /// <response code="415">When a response is specified in an unsupported content type</response>
    /// <response code="422">If query params structure is valid, but the values fail validation</response>
    /// <response code="500">A server fault occurred</response>
    [HttpPost(Name = nameof(CreateProduct))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto product)
    {
        var createdProduct = await _productService.CreateAsync(product).ConfigureAwait(true);

        return CreatedAtAction(actionName: nameof(GetProductById),
            routeValues: new { productId = createdProduct.Id }, value: createdProduct);
    }

    /// <summary>
    /// Update an existing product
    /// </summary>
    /// <response code="204">The product was updated successfully</response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="404">The user was not found for specified user id</response>
    /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
    /// <response code="415">When a response is specified in an unsupported content type</response>
    /// <response code="422">If query params structure is valid, but the values fail validation</response>
    /// <response code="500">A server fault occurred</response>
    [HttpPut("{productId}", Name = nameof(UpdateProduct))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateProduct([FromRoute] long productId, [FromBody] UpdateProductDto product)
    {
        await _productService.UpdateAsync(productId, product).ConfigureAwait(true);
        return NoContent();
    }


    /// <summary>
    /// Delete product
    /// </summary>
    /// <response code="204">The product was deleted successfully.</response>
    /// <response code="404">A product having specified user id was not found</response>
    /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
    /// <response code="415">When a response is specified in an unsupported content type</response>
    /// <response code="422">If query params structure is valid, but the values fail validation</response>
    /// <response code="500">A server fault occurred</response>
    [HttpDelete("{productId}", Name = nameof(DeleteProduct))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteProduct([FromRoute] long productId)
    {
        await _productService.DeleteAsync(productId).ConfigureAwait(true);
        return NoContent();
    }
}
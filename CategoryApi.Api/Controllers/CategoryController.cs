using CategoryApi.Application.Categories.Dto;
using CategoryApi.Application.Categories.Dto.Request;
using CategoryApi.Application.Categories.Services;
using CategoryApi.Application.Products.Dto;
using CategoryApi.Application.Products.Services;
using CategoryApi.Application.Shared.Models;
using Framework.Models;
using Microsoft.AspNetCore.Mvc;

namespace CategoryApi.Api.Controllers;

/// <summary>
/// category
/// </summary>
[Route("v1/categories")]
[ApiController]
[Produces("application/json", "application/xml")]
[Consumes("application/json", "application/xml")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="categoryService"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public CategoryController(ICategoryService categoryService, IProductService productService)
    {
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }
    
    /// <summary>
    /// Get categories
    /// </summary>
    /// <response code="200">The categories were found</response>
    /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
    /// <response code="415">When a response is specified in an unsupported content type</response>
    /// <response code="422">If query params structure is valid, but the values fail validation</response>
    /// <response code="500">A server fault occurred</response>
    [HttpGet(Name = nameof(GetCategories))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<CategoryDto>>> GetCategories()
    {
        var result = await _categoryService.GetAsync().ConfigureAwait(true);
        return Ok(result);
    }

    /// <summary>
    /// Get products by category id
    /// </summary>
    /// <response code="200">The category was found</response>
    /// <response code="404">The category was not found</response>
    /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
    /// <response code="415">When a response is specified in an unsupported content type</response>
    /// <response code="422">If query params structure is valid, but the values fail validation</response>
    /// <response code="500">A server fault occurred</response>
    [HttpGet("{categoryId}/products", Name = nameof(GetCategoryProducts))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PagedList<ProductDto>>> GetCategoryProducts([FromRoute] long categoryId,
        [FromQuery] PaginationFilter filter)
    {
        var result = await _productService.GetAsync(categoryId, filter).ConfigureAwait(true);
        return Ok(result);
    }

    /// <summary>
    /// Get a single category by category id
    /// </summary>
    /// <response code="200">The category was found</response>
    /// <response code="404">The category was not found</response>
    /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
    /// <response code="415">When a response is specified in an unsupported content type</response>
    /// <response code="422">If query params structure is valid, but the values fail validation</response>
    /// <response code="500">A server fault occurred</response>
    [HttpGet("{categoryId}", Name = nameof(GetCategoryById))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CategoryDto>> GetCategoryById([FromRoute] long categoryId)
    {
        var category = await _categoryService.GetByIdAsync(categoryId).ConfigureAwait(true);

        if (category == null)
            return NotFound();

        return Ok(category);
    }


    /// <summary>
    /// Create a new category
    /// </summary>
    /// <response code="201">The category was created successfully. Also includes 'location' header to newly created category</response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
    /// <response code="415">When a response is specified in an unsupported content type</response>
    /// <response code="422">If query params structure is valid, but the values fail validation</response>
    /// <response code="500">A server fault occurred</response>
    [HttpPost(Name = nameof(CreateCategory))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto category)
    {
        var createdCategory = await _categoryService.CreateAsync(category).ConfigureAwait(true);

        return CreatedAtAction(actionName: nameof(GetCategoryById),
            routeValues: new { categoryId = createdCategory.Id }, value: createdCategory);
    }

    /// <summary>
    /// Update an existing category
    /// </summary>
    /// <response code="204">The category was updated successfully</response>
    /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
    /// <response code="404">The user was not found for specified user id</response>
    /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
    /// <response code="415">When a response is specified in an unsupported content type</response>
    /// <response code="422">If query params structure is valid, but the values fail validation</response>
    /// <response code="500">A server fault occurred</response>
    [HttpPut("{categoryId}", Name = nameof(UpdateCategory))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCategory([FromRoute] long categoryId, [FromBody] UpdateCategoryDto category)
    {
        await _categoryService.UpdateAsync(categoryId, category).ConfigureAwait(true);
        return NoContent();
    }


    /// <summary>
    /// Delete category
    /// </summary>
    /// <response code="204">The category was deleted successfully.</response>
    /// <response code="404">A category having specified user id was not found</response>
    /// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
    /// <response code="415">When a response is specified in an unsupported content type</response>
    /// <response code="422">If query params structure is valid, but the values fail validation</response>
    /// <response code="500">A server fault occurred</response>
    [HttpDelete("{categoryId}", Name = nameof(DeleteCategory))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCategory([FromRoute] long categoryId)
    {
        await _categoryService.DeleteAsync(categoryId).ConfigureAwait(true);
        return NoContent();
    }
}
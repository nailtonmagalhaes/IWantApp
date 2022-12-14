namespace IWantApp.Endpoints.Products;

public class ProductPut
{
    public static string Template => "/products/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;
    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action([FromRoute] Guid id, ProductRequest productRequest, HttpContext http, ApplicationDbContext context)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var product = context.Products.Where(c => c.Id == id).FirstOrDefault();
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == productRequest.CategoryId);
        if (product == null)
            return Results.NotFound(new { message = "Produto não encontrado!", success = false });

        product.EditInfo(productRequest.Name, category, productRequest.Description, productRequest.HasStock, productRequest.Active, productRequest.Price, userId);

        if (!product.IsValid)
            return Results.ValidationProblem(product.Notifications.ToProblemDetails());
        await context.SaveChangesAsync();
        return Results.Ok(product);
    }
}

namespace IWantApp.Endpoints.Products;

public class ProductGetShowcases
{
    public static string Template => "/products/showcase";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static IResult Action(ApplicationDbContext context, int page = 1, int rows = 10, string orderBy = "name")
    {
        if (rows > 10) return Results.Problem(title: "Row with max 10", statusCode: 400);

        var queryBase = context.Products.AsNoTracking().Include(p => p.Category).Where(p => p.HasStock && p.Category.Active);

        if (orderBy == "name") queryBase.OrderBy(p => p.Name);
        else if (orderBy == "price") queryBase.OrderBy(p => p.Price);
        else Results.Problem(title: "Order by only by price or name", statusCode: 400);

        var queryFiltered = queryBase.Skip((page - 1) * rows).Take(rows);

        var products = queryFiltered.ToList();
        var results = products.Select(p => new ProductResponse(p.Name, p.Category.Name, p.Description, p.HasStock, p.Price, p.Active)).ToList();
        return Results.Ok(results);
    }
}

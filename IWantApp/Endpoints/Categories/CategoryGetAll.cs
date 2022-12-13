namespace IWantApp.Endpoints.Categories;

public class CategoryGetAll
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static IResult Action(ApplicationDbContext context)
    {
        var categories = context.Categories.Select(c => new CategoryResponse(c.Id, c.Name, c.Active)).ToList();

        return Results.Ok(categories);
    }
}

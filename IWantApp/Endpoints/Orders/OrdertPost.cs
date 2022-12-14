namespace IWantApp.Endpoints.Orders;

public class OrderPost
{
    public static string Template => "/orders";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "CpfPolicy")]
    public static async Task<IResult> Action(OrderRequest request, HttpContext http, ApplicationDbContext context)
    {
        var clientId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var clientName = http.User.Claims.First(c => c.Type == ClientClaimTypes.NAME).Value;

        List<Product> productsFound = null;
        if(request.ProductIds.Any())
            productsFound = context.Products.Where(p => request.ProductIds.Any(i => i == p.Id)).ToList();

        var order = new Order(clientId, clientName, productsFound, request.DeliveryAddress);

        if (!order.IsValid)
            return Results.ValidationProblem(order.Notifications.ToProblemDetails());

        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();

        return Results.Created($"{Template}/{order.Id}", order.Id);
    }
}

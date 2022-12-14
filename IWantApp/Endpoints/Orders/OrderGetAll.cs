namespace IWantApp.Endpoints.Orders;

public class OrderGetAll
{
    public static string Template => "/orders";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize]
    public static async Task<IResult> Action(HttpContext http, ApplicationDbContext context)
    {
        var clientId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var claimCpf = http.User.Claims.FirstOrDefault(c => c.Type == ClientClaimTypes.CPF)?.Value;
        var claimCode = http.User.Claims.FirstOrDefault(c => c.Type == EmployeeClaimTypes.EMPLOYEE_CODE)?.Value;

        var orders = context.Orders.Where(o => claimCode != null || o.ClientId == clientId).ToList();

        return Results.Ok(orders);
    }
}

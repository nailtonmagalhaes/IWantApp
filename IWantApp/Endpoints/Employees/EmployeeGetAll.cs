namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;
    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        page = page ?? 1;
        rows = rows ?? 10;
        return Results.Ok(await query.Execute(page.Value, rows.Value));

        //var users = userManager.Users.Skip((page - 1) * rows).Take(rows).ToList();
        //var employees = new List<EmployeeResponse>();

        //foreach (var user in users)
        //{
        //    var claims = userManager.GetClaimsAsync(user).Result;
        //    var claimName = claims.FirstOrDefault(c => c.Type == "Name");
        //    employees.Add(new EmployeeResponse(user.Email, claimName?.Value ?? string.Empty));
        //}
        //return Results.Ok(employees);
    }
}

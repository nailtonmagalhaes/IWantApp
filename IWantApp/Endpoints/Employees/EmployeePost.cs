namespace IWantApp.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(EmployeeRequest employeeRequest, HttpContext http, UserCreator userCreator)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var userClaims = new List<Claim>()
        {
            new Claim(EmployeeClaimTypes.EMPLOYEE_CODE, employeeRequest.EmployeeCode),
            new Claim(EmployeeClaimTypes.NAME, employeeRequest.Name),
            new Claim(EmployeeClaimTypes.CREATED_BY, userId),
        };
        (IdentityResult identy, string userId) result = await userCreator.Create(employeeRequest.Email, employeeRequest.Password, userClaims);

        if (!result.identy.Succeeded)
            return Results.BadRequest(result.identy.Errors.ToProblemDetails());

        return Results.Created($"{Template}/{result.userId}", result.userId);
    }
}

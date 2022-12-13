namespace IWantApp.Infra.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration _configuration;

    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<EmployeeResponse>> Execute(int page, int rows)
    {
        var db = new SqlConnection(_configuration["ConnectionString:IWantDb"]);
        return await db.QueryAsync<EmployeeResponse>($@"
SELECT    u.Email, c.ClaimValue AS Name
FROM      AspNetUsers u
LEFT JOIN AspNetUserClaims c ON u.Id = c.UserId AND c.ClaimType = 'Name'
ORDER BY Name
OFFSET (@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY", new { page, rows });
    }
}

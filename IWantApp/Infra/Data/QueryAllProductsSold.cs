namespace IWantApp.Infra.Data;

public class QueryAllProductsSold
{
    private readonly IConfiguration _configuration;

    public QueryAllProductsSold(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<ProductsSoldResponse>> Execute()
    {
        var db = new SqlConnection(_configuration["ConnectionString:IWantDb"]);
        return await db.QueryAsync<ProductsSoldResponse>(@"
SELECT   p.Id, p.Name, COUNT(*) Amount 
FROM     Orders o 
JOIN     OrderProducts op ON op.OrdersId = o.Id
JOIN     Products p ON p.Id = op.ProductsId
GROUP BY p.Id, p.Name
ORDER BY Amount DESC");
    }
}

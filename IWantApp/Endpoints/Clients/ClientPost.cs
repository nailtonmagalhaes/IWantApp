namespace IWantApp.Endpoints.Clients;

public class ClientPost
{
    public static string Template => "/clients";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    [AllowAnonymous]
    public static async Task<IResult> Action(ClientRequest clientRequest, UserCreator userCreator)
    {
        var userClaims = new List<Claim>()
        {
            new Claim(ClientClaimTypes.CPF, clientRequest.Cpf),
            new Claim(ClientClaimTypes.NAME, clientRequest.Name),
        };
        (IdentityResult identy, string userId) result = await userCreator.Create(clientRequest.Email, clientRequest.Password, userClaims);

        if (!result.identy.Succeeded)
            return Results.BadRequest(result.identy.Errors.ToProblemDetails());

        return Results.Created($"{Template}/{result.userId}", result.userId);
    }
}

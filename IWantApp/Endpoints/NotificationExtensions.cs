namespace IWantApp.Endpoints;

public static class NotificationExtensions
{
    public static Dictionary<string, string[]> ToProblemDetails(this IReadOnlyCollection<Notification> notifications) =>
        notifications.GroupBy(g => g.Key).ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());

    public static Dictionary<string, string[]> ToProblemDetails(this IEnumerable<IdentityError> error) =>
        new Dictionary<string, string[]>(new List<KeyValuePair<string, string[]>>() { new KeyValuePair<string, string[]>("Error", error.Select(e => e.Description).ToArray()) });
    //error.GroupBy(g => g.Code).ToDictionary(g => g.Key, g => g.Select(x => x.Description).ToArray());
}

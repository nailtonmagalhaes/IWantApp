namespace IWantApp.Domain.Products;

public class Category : Entity
{
    public Category(string name, string createdBy, string editedBy)
    {
        Name = name;
        Active = true;
        CreatedBy = createdBy;
        EditedBy = editedBy;
        EditedOn = DateTime.Now;
        CreatedOn = DateTime.Now;
        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Category>()
             .IsNotNullOrEmpty(Name, nameof(Name))
             .IsNotNullOrEmpty(CreatedBy, nameof(CreatedBy))
             .IsNotNullOrEmpty(EditedBy, nameof(EditedBy));
        AddNotifications(contract);
    }

    public string Name { get; private set; }
    public bool Active { get; private set; }

    public void EditInfo(string name, bool active, string editedBy)
    {
        Active = active;
        Name = name;
        EditedBy = editedBy;
        EditedOn = DateTime.Now;
        Validate();
    }
}

namespace InventoryManagement.Models.Interface
{
    public interface IModel<TKey>
    {
        TKey Id { get; set; }
    }
}

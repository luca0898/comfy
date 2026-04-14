namespace Comfy.Product.Contracts.Shared
{
    public interface IEntity
    {
        int Id { get; set; }
        bool Deleted { get; set; }
    }
}

namespace App.Public.DTO.v1;

public class OrderCont
{
    public List<String> Products { get; set; } = default!;
    public Order Order { get; set; } = default!;

}
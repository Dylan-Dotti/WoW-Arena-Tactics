
public class AStarProperties<T>
{
    public T ParentNode { get; set; }
    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost => GCost + HCost;

    public void Reset()
    {
        ParentNode = default;
        GCost = 0;
        HCost = 0;
    }
}

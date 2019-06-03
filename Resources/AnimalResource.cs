namespace MMM_Bracket.API.Resources
{
  public class AnimalResource
  {
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public int InitialSeed { get; set; }

    // public virtual CategoryResource Category { get; set; }
  }
}
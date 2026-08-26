namespace DemoCrudWithEF.Domain.Entities;

public class Groupe
{
    public int Id { get; set; }
    public string Nom { get; set; } = default!;
    public virtual IList<Album> Albums { get; set; } = new List<Album>();
}

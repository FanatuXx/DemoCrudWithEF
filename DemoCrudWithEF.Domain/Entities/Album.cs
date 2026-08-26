namespace DemoCrudWithEF.Domain.Entities;

public class Album
{
    public int Id { get; set; }
    public string Titre { get; set; } = default!;
    public int Annee { get; set; }
    public int GroupeId { get; set; }
    public virtual Groupe Groupe { get; set; } = default!;
}

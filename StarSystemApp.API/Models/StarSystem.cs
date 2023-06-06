using System.ComponentModel.DataAnnotations.Schema;

namespace StarSystemApp.API.Models
{
    public class StarSystem
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public required double Age { get; set; }

        public int? MassCenterId { get; set; }

        [ForeignKey("MassCenterId")]
        public SpaceObject? MassCenter { get; set; }

        public List<SpaceObject>? SpaceObjects { get; set; }

    }

    public class SpaceObject
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public required string Type { get; set; }
        public required double Age { get; set; }
        public required double Diameter { get; set; }
        public required double Mass { get; set; }

        public int? StarSystemId { get; set; }

        [ForeignKey("StarSystemId")]
        public StarSystem? StarSystem { get; set; }
    }
}

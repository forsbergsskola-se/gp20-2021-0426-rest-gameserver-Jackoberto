using System.ComponentModel.DataAnnotations;

namespace MMO_RPG.Model
{
    public class NewPlayer
    {
        [StringLength(15)]
        public string Name { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace MMO_RPG.Model
{
    public class NewItem
    {
        public string Name { get; set; }
        
        [Range(1, 99)]
        public int Level { get; set; }
        
        [EnumDataType(typeof(ItemType))]
        public ItemType Type { get; set; }
        
        [DateValidation]
        public DateTime CreationTime { get; set; }
    }
}
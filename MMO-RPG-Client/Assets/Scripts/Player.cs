using System;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public int Level { get; set; }
    public DateTime CreationTime { get; set; }

    public override string ToString() => $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Score)}: {Score}, {nameof(Level)}: {Level}, {nameof(CreationTime)}: {CreationTime}";
}
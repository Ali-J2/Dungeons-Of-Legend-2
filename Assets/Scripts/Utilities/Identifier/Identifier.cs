using System;

namespace DungeonBrickStudios
{
    [AttributeUsage(AttributeTargets.Field)]
    public class Identifier : Attribute
    {
        public readonly string enumIdentifier;

        public Identifier(string enumIdentifier)
        {
            this.enumIdentifier = enumIdentifier;
        }
    }
}

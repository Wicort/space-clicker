namespace Assets.SpaceArena.Scripts.Infrastructure.Localization
{
    public partial class EnLocalizationService
    {
        public class ItemLocalization
        {
            public string Name { get; set; }
            public string Description { get; set; }

            public ItemLocalization(string name, string description)
            {
                Name = name;
                Description = description;
            }
        }
    }
}

using BindOpen.Data.Items;
using System.Linq;

namespace BindOpen.Application.Modules
{
    /// <summary>
    /// This class represents a Sphere application module.
    /// </summary>
    public static class AppModuleFactory
    {
        /// <summary>
        /// Initializes a new instance of the AppModule class.
        /// </summary>
        /// <param name="name">The name of the instance.</param>
        /// <param name="sections">The sections of the instance.</param>
        public static AppModule Create(string name, params IAppSection[] sections)
        {
            return new AppModule()
            {
                Name = name,
                Sections = DataItemFactory.CreateItemSet<AppSection>(sections.Cast<AppSection>().ToArray())
            };
        }
    }
}
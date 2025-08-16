using AutoMapper;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// This class represents a ClassReference mapping profile.
/// </summary>
public class ClassReferenceProfile : Profile
{
    /// <summary>
    /// Instantiates a new instance of the ClassReferenceProfile class.
    /// </summary>
    public ClassReferenceProfile()
    {
        CreateMap<BdoClassReference, ClassReferenceDb>();
    }
}

using System;

namespace BindOpen.Data.Objects;

/// <summary>
/// Represents the base object within the BindOpen ecosystem.
/// This interface serves as the root contract, ensuring all components
/// support life-cycle management and cloning.
/// </summary>
public interface IBdoObject : IDisposable, IClonable
{
}
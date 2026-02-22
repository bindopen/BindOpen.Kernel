using System;

namespace BindOpen.Data;

/// <summary>
/// Represents the base object within the BindOpen ecosystem.
/// This interface serves as the root contract, ensuring all components
/// support life-cycle management and cloning.
/// </summary>
public interface IBdoObject : IDisposable, IClonable
{
}
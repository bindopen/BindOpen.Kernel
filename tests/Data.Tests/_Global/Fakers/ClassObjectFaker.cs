using Bogus;
using System;

namespace BindOpen.Tests;

/// <summary>
/// Provides factory methods for generating randomized <see cref="ClassFake"/> instances.
/// </summary>
public static class ClassObjectFaker
{
    /// <summary>
    /// Static instance of the Bogus Faker to ensure consistent random seed management.
    /// </summary>
    private static readonly Faker _faker = new();

    /// <summary>
    /// Creates a new instance of <see cref="ClassFake"/> populated with randomized data.
    /// </summary>
    /// <returns>Returns a new <see cref="ClassFake"/> object with initialized properties.</returns>
    /// <remarks>
    /// This method is used to test POCO (Plain Old CLR Object) mapping and property resolution 
    /// within the BindOpen Kernel.
    /// </remarks>
    public static object Fake()
    {
        return new ClassFake()
        {
            DateTimeProp = DateTime.Now,
            DoubleProp = _faker.Random.Double(),
            FloatProp = _faker.Random.Float(),
            IntProp = _faker.Random.Int(),
            StringProp = _faker.Random.Word(),
            TimeSpanProp = _faker.Date.Timespan()
        };
    }
}
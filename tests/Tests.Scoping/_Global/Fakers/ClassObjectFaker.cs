using Bogus;
using System;

namespace BindOpen.System.Tests.Scoping
{
    public static class ClassObjectFaker
    {
        public static object Fake()
        {
            var f = new Faker();
            return new ClassFake()
            {
                DateTimeProp = DateTime.Now,
                DoubleProp = f.Random.Double(),
                FloatProp = f.Random.Float(),
                IntProp = f.Random.Int(),
                StringProp = f.Random.Word(),
                TimeSpanProp = f.Date.Timespan()
            };
        }
    }
}

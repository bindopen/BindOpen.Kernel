﻿namespace BindOpen.Framework.Core.Application.Options
{
    public interface IOptionSpec : IScalarElementSpec
    {
        bool IsArgumentMarching(string argumentstring);
        bool IsArgumentMarching(string argumentstring, out int aliasIndex);
    }
}
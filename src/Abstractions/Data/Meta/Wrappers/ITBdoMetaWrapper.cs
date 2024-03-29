﻿namespace BindOpen.Data.Meta
{
    public interface ITBdoMetaWrapper<TDetail> : IBdoMetaWrapper
        where TDetail : IBdoMetaSet
    {
        new TDetail Detail { get; set; }
    }
}
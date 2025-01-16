using BindOpen.Data.Meta;
using BindOpen.Scoping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents an application 
/// </summary>
public static class BdoTaskExtensions_Definition
{
    // Get

    public static ITBdoSet<IBdoSchema> Properties(this ITBdoSet<IBdoSchema> set)
        => BdoData.NewItemSet(set?.Where(q => q.IsProperty())?.ToArray());

    public static ITBdoSet<IBdoSchema> Inputs(this ITBdoSet<IBdoSchema> set)
        => BdoData.NewItemSet(set?.Where(q => q.IsInput())?.ToArray());

    public static ITBdoSet<IBdoSchema> Outputs(this ITBdoSet<IBdoSchema> set)
        => BdoData.NewItemSet(set?.Where(q => q.IsOutput())?.ToArray());

    // Is

    public static bool IsProperty(this IBdoSchema schema)
        => schema.OfGroup(null);

    public static bool IsInput(this IBdoSchema schema)
        => schema.OfGroup(IBdoTaskExtensions.__Token_Input);

    public static bool IsOutput(this IBdoSchema schema)
        => schema.OfGroup(IBdoTaskExtensions.__Token_Output);

    // As

    public static IBdoSchema AsProperty(
        this IBdoSchema schema)
    {
        schema?.WithGroupId(null);

        return schema;
    }

    public static IBdoSchema AsInput(
        this IBdoSchema schema)
    {
        schema?.WithGroupId(IBdoTaskExtensions.__Token_Input);

        return schema;
    }

    public static IBdoSchema AsOutput(
        this IBdoSchema schema)
    {
        schema?.WithGroupId(IBdoTaskExtensions.__Token_Output);

        return schema;
    }

    // Properties

    public static T WithProperties<T>(
        this T set,
        params IBdoSchema[] props)
        where T : IBdoSchema
    {
        if (set != null)
        {
            set.RemoveChildren(q => q.OfGroup(null));
            set.AddProperties(props);
        }

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance with the specified key and text.
    /// </summary>
    /// <param key="text">The text to consider.</param>
    /// <returns>Returns the added data key value.</returns>
    public static T WithProperties<T>(
        this T set,
        params (string Name, DataValueTypes ValueType)[] pairs)
        where T : IBdoSchema
    {
        set?.WithProperties(pairs?.Select(q => BdoData.NewSchema(q.Name, q.ValueType)).ToArray());

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance.
    /// </summary>
    /// <param key="pairs">The value to add.</param>
    public static T WithProperties<T>(
        this T set,
        params KeyValuePair<string, DataValueTypes>[] pairs)
        where T : IBdoSchema
    {
        set?.WithProperties(pairs?.Select(q => BdoData.NewSchema(q.Key, q.Value)).ToArray());

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance with the specified key and text.
    /// </summary>
    /// <param key="text">The text to consider.</param>
    /// <returns>Returns the added data key value.</returns>
    public static T WithProperties<T>(
        this T set,
        params (string Name, DataValueTypes ValueType, object DefaultData)[] pairs)
        where T : IBdoSchema
    {
        set?.WithProperties(pairs?.Select(q => BdoData.NewSchema(q.Name, q.ValueType, q.DefaultData)).ToArray());

        return set;
    }

    public static T AddProperties<T>(
        this T set,
        params IBdoSchema[] props)
        where T : IBdoSchema
    {
        if (set != null)
        {
            Array.ForEach(props, q => { q.AsProperty(); });
            set.AddChildren(props);
        }

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance with the specified key and text.
    /// </summary>
    /// <param key="text">The text to consider.</param>
    /// <returns>Returns the added data key value.</returns>
    public static T AddProperties<T>(
        this T set,
        params (string Name, DataValueTypes ValueType)[] pairs)
        where T : IBdoSchema
    {
        set?.AddProperties(pairs?.Select(q => BdoData.NewSchema(q.Name, q.ValueType)).ToArray());

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance.
    /// </summary>
    /// <param key="pairs">The value to add.</param>
    public static T AddProperties<T>(
        this T set,
        params KeyValuePair<string, DataValueTypes>[] pairs)
        where T : IBdoSchema
    {
        set?.AddProperties(pairs?.Select(q => BdoData.NewSchema(q.Key, q.Value)).ToArray());

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance with the specified key and text.
    /// </summary>
    /// <param key="text">The text to consider.</param>
    /// <returns>Returns the added data key value.</returns>
    public static T AddProperties<T>(
        this T set,
        params (string Name, DataValueTypes ValueType, object DefaultData)[] pairs)
        where T : IBdoSchema
    {
        set?.AddProperties(pairs?.Select(q => BdoData.NewSchema(q.Name, q.ValueType, q.DefaultData)).ToArray());

        return set;
    }

    // Inputs

    public static T WithInputs<T>(
        this T set,
        params IBdoSchema[] inputs)
        where T : IBdoSchema
    {
        if (set != null)
        {
            set.RemoveChildren(q => q.OfGroup(IBdoTaskExtensions.__Token_Input));
            set.AddInputs(inputs);
        }

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance with the specified key and text.
    /// </summary>
    /// <param key="text">The text to consider.</param>
    /// <returns>Returns the added data key value.</returns>
    public static T WithInputs<T>(
        this T set,
        params (string Name, DataValueTypes ValueType)[] pairs)
        where T : IBdoSchema
    {
        set?.WithInputs(pairs?.Select(q => BdoData.NewSchema(q.Name, q.ValueType)).ToArray());

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance.
    /// </summary>
    /// <param key="pairs">The value to add.</param>
    public static T WithInputs<T>(
        this T set,
        params KeyValuePair<string, DataValueTypes>[] pairs)
        where T : IBdoSchema
    {
        set?.WithInputs(pairs?.Select(q => BdoData.NewSchema(q.Key, q.Value)).ToArray());

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance with the specified key and text.
    /// </summary>
    /// <param key="text">The text to consider.</param>
    /// <returns>Returns the added data key value.</returns>
    public static T WithInputs<T>(
        this T set,
        params (string Name, DataValueTypes ValueType, object DefaultData)[] pairs)
        where T : IBdoSchema
    {
        set?.WithInputs(pairs?.Select(q => BdoData.NewSchema(q.Name, q.ValueType, q.DefaultData)).ToArray());

        return set;
    }

    public static T AddInputs<T>(
        this T set,
        params IBdoSchema[] inputs)
        where T : IBdoSchema
    {
        if (set != null)
        {

            Array.ForEach(inputs, q => { q.AsInput(); });
            set.AddChildren(inputs);
        }

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance with the specified key and text.
    /// </summary>
    /// <param key="text">The text to consider.</param>
    /// <returns>Returns the added data key value.</returns>
    public static T AddInputs<T>(
        this T set,
        params (string Name, DataValueTypes ValueType)[] pairs)
        where T : IBdoSchema
    {
        set?.AddInputs(pairs?.Select(q => BdoData.NewSchema(q.Name, q.ValueType)).ToArray());

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance.
    /// </summary>
    /// <param key="pairs">The value to add.</param>
    public static T AddInputs<T>(
        this T set,
        params KeyValuePair<string, DataValueTypes>[] pairs)
        where T : IBdoSchema
    {
        set?.AddInputs(pairs?.Select(q => BdoData.NewSchema(q.Key, q.Value)).ToArray());

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance with the specified key and text.
    /// </summary>
    /// <param key="text">The text to consider.</param>
    /// <returns>Returns the added data key value.</returns>
    public static T AddInputs<T>(
        this T set,
        params (string Name, DataValueTypes ValueType, object DefaultData)[] pairs)
        where T : IBdoSchema
    {
        set?.AddInputs(pairs?.Select(q => BdoData.NewSchema(q.Name, q.ValueType, q.DefaultData)).ToArray());

        return set;
    }

    // Outputs

    public static T WithOutputs<T>(
        this T set,
        params IBdoSchema[] outputs)
        where T : IBdoSchema
    {
        if (set != null)
        {
            set.RemoveChildren(q => q.OfGroup(IBdoTaskExtensions.__Token_Output));
            set.AddOutputs(outputs);
        }

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance with the specified key and text.
    /// </summary>
    /// <param key="text">The text to consider.</param>
    /// <returns>Returns the added data key value.</returns>
    public static T WithOutputs<T>(
        this T set,
        params (string Name, DataValueTypes ValueType)[] pairs)
        where T : IBdoSchema
    {
        set?.WithOutputs(pairs?.Select(q => BdoData.NewSchema(q.Name, q.ValueType)).ToArray());

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance.
    /// </summary>
    /// <param key="pairs">The value to add.</param>
    public static T WithOutputs<T>(
        this T set,
        params KeyValuePair<string, DataValueTypes>[] pairs)
        where T : IBdoSchema
    {
        set?.WithOutputs(pairs?.Select(q => BdoData.NewSchema(q.Key, q.Value)).ToArray());

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance with the specified key and text.
    /// </summary>
    /// <param key="text">The text to consider.</param>
    /// <returns>Returns the added data key value.</returns>
    public static T WithOutputs<T>(
        this T set,
        params (string Name, DataValueTypes ValueType, object DefaultData)[] pairs)
        where T : IBdoSchema
    {
        set?.WithOutputs(pairs?.Select(q => BdoData.NewSchema(q.Name, q.ValueType, q.DefaultData)).ToArray());

        return set;
    }

    public static T AddOutputs<T>(
        this T set,
        params IBdoSchema[] outputs)
        where T : IBdoSchema
    {
        if (set != null)
        {

            Array.ForEach(outputs, q => { q.AsOutput(); });
            set.AddChildren(outputs);
        }

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance with the specified key and text.
    /// </summary>
    /// <param key="text">The text to consider.</param>
    /// <returns>Returns the added data key value.</returns>
    public static T AddOutputs<T>(
        this T set,
        params (string Name, DataValueTypes ValueType)[] pairs)
        where T : IBdoSchema
    {
        set?.AddOutputs(pairs?.Select(q => BdoData.NewSchema(q.Name, q.ValueType)).ToArray());

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance.
    /// </summary>
    /// <param key="pairs">The value to add.</param>
    public static T AddOutputs<T>(
        this T set,
        params KeyValuePair<string, DataValueTypes>[] pairs)
        where T : IBdoSchema
    {
        set?.AddOutputs(pairs?.Select(q => BdoData.NewSchema(q.Key, q.Value)).ToArray());

        return set;
    }

    /// <summary>
    /// Adds a new value to this instance with the specified key and text.
    /// </summary>
    /// <param key="text">The text to consider.</param>
    /// <returns>Returns the added data key value.</returns>
    public static T AddOutputs<T>(
        this T set,
        params (string Name, DataValueTypes ValueType, object DefaultData)[] pairs)
        where T : IBdoSchema
    {
        set?.AddOutputs(pairs?.Select(q => BdoData.NewSchema(q.Name, q.ValueType, q.DefaultData)).ToArray());

        return set;
    }
}

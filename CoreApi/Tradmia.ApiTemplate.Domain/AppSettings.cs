using System.Diagnostics.CodeAnalysis;

namespace Tradmia.Ciutada.Domain;

/// <summary>
/// Application settings
/// </summary>
[ExcludeFromCodeCoverage]
public class AppSettings
{
}

/// <summary>
/// Options for CosmosDb
/// </summary>
[ExcludeFromCodeCoverage]
public class CosmosDbOptions
{
    /// <summary>
    /// Account endpoint
    /// </summary>
    public string Endpoint { get; set; } = null!;

    /// <summary>
    /// Account key
    /// </summary>
    public string AuthKey { get; set; } = null!;

    /// <summary>
    /// Database id
    /// </summary>
    public string DatabaseId { get; set; } = null!;
}

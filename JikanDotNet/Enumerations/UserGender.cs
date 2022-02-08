using System.ComponentModel;

namespace JikanDotNet;

public enum UserGender
{
    /// <summary>
    /// Allow all types to be displayed in results.
    /// </summary>
    [Description("any")]
    Any,

    /// <summary>
    /// Male gender.
    /// </summary>
    [Description("male")]
    Male,

    /// <summary>
    /// Female gender.
    /// </summary>
    [Description("female")]
    Female,

    /// <summary>
    /// Non-binary gender.
    /// </summary>
    [Description("nonbinary")]
    NonBinary
}
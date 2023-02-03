namespace Rnd.Constants;

public enum UnitRole
{
    /// <summary>
    /// Variable stored in the database
    /// Implicit by default
    /// var
    /// </summary>
    Variable,
    /// <summary>
    /// Immutable constant
    /// const
    /// </summary>
    Constant,
    /// <summary>
    /// Computed value using module units
    /// exp
    /// </summary>
    Expression,
    /// <summary>
    /// Computed value with external parameters
    /// func
    /// </summary>
    Function,
    /// <summary>
    /// Declares named object type
    /// type
    /// </summary>
    Type,
    /// <summary>
    /// module
    /// </summary>
    Module,
}
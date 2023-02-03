namespace Rnd.Constants;

public enum UnitType
{
    /// <summary>
    /// key-value collection
    /// 
    /// literal:
    /// { Key: value, Key2: value }
    /// 
    /// declaration:
    /// obj Attributes:
    ///   "Title"
    ///   """Description"""
    ///     type Key: value
    ///     type Key2: value
    /// </summary>
    Object,
    /// <summary>
    /// literal:
    /// "value"
    /// 'value'
    /// """multi-line value"""
    /// `interpolation @Value @{expression}`
    /// 
    /// declaration:
    /// str Var: "value"
    /// </summary>
    String,
    /// <summary>
    /// literal:
    /// 0
    /// 
    /// declaration:
    /// int Var: 0
    /// </summary>
    Integer,
    /// <summary>
    /// literal:
    /// 0.
    /// 
    /// declaration:
    /// float Var: 0.
    /// </summary>
    Float,
    /// <summary>
    /// literal:
    /// 2d6
    /// 
    /// declaration:
    /// dice Var: 2d6
    /// </summary>
    Dice,
    /// <summary>
    /// literal:
    /// false
    /// 
    /// declaration:
    /// bool Var: false
    /// </summary>
    Boolean,
    /// <summary>
    /// literal:
    /// [value1, value2]
    /// 
    /// declaration:
    /// list Var:
    ///     value1
    ///     value2
    /// </summary>
    List,
    /// <summary>
    /// literal: Variable
    /// 
    /// declaration:
    /// ref Var: Variable
    /// </summary>
    Reference,
    /// <summary>
    /// No data
    /// 
    /// literal:
    /// none
    /// </summary>
    None,
}
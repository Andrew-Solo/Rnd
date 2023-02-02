namespace Rnd.Compiler.Lexer;

public enum LexemeType
{
    Unknown,
    Newline,
    Operator,
    Integer,
    Float,
    Dice,
    ListBracket,
    FunctionBracket,
    ObjectBracket,
    String,
    Title,
    Multistring,
    Identifier,
    Attribute,
    TypePicker,
    None,
    Boolean,
    Role,
    Type,
    Accessor,
    Tabulation,
}
using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Localization;

namespace Rnd.Api.Modules.Basic.Fields;

public class FieldFactory : IStorableFactory<Field>
{
    public static IField Create(Field entity)
    {
        var factory = new FieldFactory();
        return (IField) factory.CreateStorable(entity);
    }

    public IStorable<Field> CreateStorable(Field entity)
    {
        return CreateSimilar(entity).LoadNotNull(entity);
    }
    
    protected virtual IField CreateSimilar(Field entity)
    {
        return entity.Type switch
        {
            FieldType.Tiny => CreateTiny(entity),
            FieldType.Short => CreateShort(entity),
            FieldType.Medium => CreateMedium(entity),
            FieldType.Paragraph => CreateParagraph(entity),
            FieldType.List => CreateList(entity),
            FieldType.Number => CreateNumber(entity),
            _ => throw new ArgumentOutOfRangeException(nameof(entity.Type), entity.Type, 
                Lang.Exceptions.IStorableFactory.UnknownType)
        };
    }
    
    private static TinyField CreateTiny(IEntity entity)
    {
        return new TinyField(entity);
    }
    
    private static ShortField CreateShort(IEntity entity)
    {
        return new ShortField(entity);
    }
    
    private static MediumField CreateMedium(IEntity entity)
    {
        return new MediumField(entity);
    }
    
    private static ParagraphField CreateParagraph(IEntity entity)
    {
        return new ParagraphField(entity);
    }
    
    private static ListField CreateList(IEntity entity)
    {
        return new ListField(entity);
    }
    
    private static NumberField CreateNumber(IEntity entity)
    {
        return new NumberField(entity);
    }
}
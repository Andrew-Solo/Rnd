using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Helpers;
using Rnd.Api.Localization;
using Rnd.Api.Modules.Basic.Characters;

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
    
    private static TinyField CreateTiny(Field entity)
    {
        return new TinyField(CharacterFactory.Create(entity.Character), PathHelper.GetName(entity.Fullname));
    }
    
    private static ShortField CreateShort(Field entity)
    {
        return new ShortField(CharacterFactory.Create(entity.Character), PathHelper.GetName(entity.Fullname));
    }
    
    private static MediumField CreateMedium(Field entity)
    {
        return new MediumField(CharacterFactory.Create(entity.Character), PathHelper.GetName(entity.Fullname));
    }
    
    private static ParagraphField CreateParagraph(Field entity)
    {
        return new ParagraphField(CharacterFactory.Create(entity.Character), PathHelper.GetName(entity.Fullname));
    }
    
    private static ListField CreateList(Field entity)
    {
        return new ListField(CharacterFactory.Create(entity.Character), PathHelper.GetName(entity.Fullname));
    }
    
    private static NumberField CreateNumber(Field entity)
    {
        return new NumberField(CharacterFactory.Create(entity.Character), PathHelper.GetName(entity.Fullname));
    }
}
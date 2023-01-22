using Rnd.Core;
using Rnd.Results;

namespace Rnd.Data;

public readonly record struct ModelResult<TModel>(TModel Model, bool IsSuccess, Message Message) 
    where TModel : Model;
namespace Rnd.Primitives;

public record struct Permission(
    string Path,
    bool Create = true,
    bool Read = true,
    bool ReadAsCreator = true,
    bool Update = true,
    bool UpdateAsCreator = true,
    bool Delete = true,
    bool DeleteAsCreator = true
);
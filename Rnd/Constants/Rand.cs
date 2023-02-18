namespace Rnd.Constants;

public static class Rand
{
    public static Random Get { get; } = new(Guid.NewGuid().GetHashCode());
    
    public static List<int> Range(int count, int value = 0) => new int[count].Select(_ => value).ToList();
    
    public static int Roll(int dice) => Get.Next(1, dice);
    
    public static List<int> Roll(int count, int dice) => Range(count).Select(_ => Roll(dice)).ToList();
    
    public static List<int> Roll(int count, int dice, int advantage)
    {
        var allDices = Roll(count + Math.Abs(advantage), dice);

        return advantage > 0
            ? allDices.Best(count)
            : allDices.Worst(count);
    }
    
    public static List<int> Roll(int count, int dice, int advantage, int drama)
    {
        var allDices = Roll(count, dice, advantage);
        
        allDices.AddRange(drama > 0 ? Range(drama, 20) : Range(-1 * drama, 1));
        
        return drama > 0
            ? allDices.Best(count)
            : allDices.Worst(count);
    }
    
    public static List<T> Shuffle<T>(this IEnumerable<T> enumerable)
    {
        var list = enumerable.ToList();
        var n = list.Count;  
        
        while (n > 1) 
        {  
            n--;  
            var k = Get.Next(n + 1);  
            (list[k], list[n]) = (list[n], list[k]);
        }  
        
        return list;
    }
    
    public static List<int> Best(this List<int> list, int count)
    {
        var newList = new List<int>(list);
        
        newList.Sort();
        
        return newList.TakeLast(count).Shuffle();
    }
    
    public static List<int> Worst(this List<int> list, int count)
    {
        var newList = new List<int>(list);
        
        newList.Sort();
        
        return newList.Take(count).Shuffle();
    }
}
using RnDBot.Views;

namespace RnDBot.Models.Common;

public class CommonPanel : IPanel
{
    public CommonPanel(string title, params IField[] fields)
    {
        Title = title;
        Fields = new List<IField>(fields);
    }

    public string Title { get; set; }
    public List<IField>? Fields { get; }
}
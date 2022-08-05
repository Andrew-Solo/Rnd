using Discord;
using RnDBot.Views;
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace RnDBot.Models.Common;

public class CommonPanel : IPanel
{
    public CommonPanel(string title, params IField[] fields)
    {
        Title = title;
        Fields = new List<IField>(fields);
    }

    public string Title { get; }
    public List<IField>? Fields { get; }
    public string? Description { get; set; }
    public Color? Color { get; set; }
    public string? Footer { get; set; }
}
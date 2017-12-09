namespace TagsCloud
{
    public class TagCloudSettings
    {
        public int TopNWords { get; set; } = 250;
        public int MinWordLength { get; set; } = 3;
        public int MinFontSize { get; set; } = 20;
        public int MaxFontSize { get; set; } = 72;
        public string FontFamily { get; set; } = "Calibri";
        public string InputPath { get; set; } = null;
        public string OutputPath { get; set; } = null;
    }
}

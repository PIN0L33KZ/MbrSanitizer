using Newtonsoft.Json;

namespace MbrSanitizer.Data;

public class Template
{
    [JsonProperty("ProjPath")]
    public required string ProjectPath { get; set; }

    public required string ValueShort { get; set; }

    public required string ValueLong { get; set; }

    [JsonProperty("DeleteProjectFile")]
    public bool DeleteMbrFile { get; set; } = false;

    [JsonProperty("AntiDragImages")]
    public bool AntiDragImgs { get; set; } = false;

    public string CustomComment { get; set; } = string.Empty;
}
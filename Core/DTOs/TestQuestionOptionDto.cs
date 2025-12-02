using System.Text.Json.Serialization;

public class TestQuestionOptionDto
{
    [JsonPropertyName("optionid")]
    public int OptionId { get; set; }

    [JsonPropertyName("optiontext")]
    public string OptionText { get; set; }
}
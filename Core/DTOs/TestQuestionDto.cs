using System.Text.Json.Serialization;

public class TestQuestionDto
{
    [JsonPropertyName("questionid")]
    public int QuestionId { get; set; }

    [JsonPropertyName("questiontext")]
    public string QuestionText { get; set; }

    [JsonPropertyName("options")]
    public List<TestQuestionOptionDto> Options { get; set; } = new();
}
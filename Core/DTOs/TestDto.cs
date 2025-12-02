using System.Text.Json.Serialization;

public class TestDto
{
    [JsonPropertyName("testid")]
    public int TestId { get; set; }

    [JsonPropertyName("subjectid")]
    public int SubjectId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("descriptiontext")]
    public string DescriptionText { get; set; }

    [JsonPropertyName("questions")]
    public List<TestQuestionDto> Questions { get; set; } = new();
}
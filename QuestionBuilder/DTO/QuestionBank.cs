namespace QuestionBuilder.DTO
{
    public class QuestionBank
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Question>? Questions { get; set; }
        public string? Curriculum { get; set; }
        public int UserScore { get; set; }
        public bool Attempted { get; set; }
    }
}

public class Answer
{
	private string name;
	private bool correct;
	public Answer(string name, bool correct)
	{
		this.name = name;
		this.correct = correct;
	}
	public string GetName() { return name; }
	public bool GetCorrect() { return correct; }
}
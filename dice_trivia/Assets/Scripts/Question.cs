using System;
using System.Collections;
using System.Collections.Generic;

public class Question
{
	private string name;
	private List<Answer> answers;
	public Question (string name)
	{
		this.name = name;
		this.answers = new List<Answer> ();
	}
	public string GetName() { return name; }
	public Answer GetAnswer(int index)
	{
		return answers[index];	
	}
	public int GetNumberOfAnswers()
	{
		return answers.Count;
	}
	public void AddAnswer(string name, bool correct)
	{
		Answer answer = new Answer (name, correct);
		answers.Add (answer);
	}
}

using System;
using System.Collections;
using System.Collections.Generic;

public class Question
{
	private string name;
	private List<Answer> answers;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
	public Question (string name)
	{
		this.name = name;
		this.answers = new List<Answer> ();
	}
	public string GetName() { return name; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
	public Answer GetAnswer(int index)
	{
		return answers[index];	
	}

    public List<Answer> GetPossibleAnswers()
    {
        return this.answers;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
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

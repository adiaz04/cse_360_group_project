using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizController1
{
	private List<Question> questions;
	/// <summary>
	/// Gets a random question
	/// </summary>
	/// <returns>Random Question</returns>
	public Question GetQuestion()
	{
		int index = Random.Range(0, questions.Count);
		Question question = GetQuestion(index);
		return question;
	}
	/// <summary>
	/// Get a question by index
	/// </summary>
	/// <param name="index">The question index to get</param>
	/// <returns>The question at the index</returns>
	public Question GetQuestion(int index)
	{
		Question result = null;
		if(index >= 0 && index < questions.Count)
		{
			result = questions[index];	
		}
		return result;
	}
	/// <summary>
	/// Generates questions from a question file
	/// </summary>
	/// <param name="text">Contents of the question file</param>
	public void GenerateQuestions(string text)
	{
		//Initialize the questions list
		questions = new List<Question>();
		//The current question, which subsequent answers will be added to
		Question currentQuestion = null;

		//Split the file by line
		string[] lines = text.Split('\n');
		//Get the number of lines
		int length = lines.GetLength(0);
		for (int lineIndex = 0; lineIndex < length; lineIndex++)
		{
			//The current line
			string line = lines[lineIndex];
			//Format for a line is <Designator><Space><Name>
			char firstCharacter = line[0]; //Gets the first character "Designator:
			string name = line.Substring(2); //Gets the rest of the line (Which starts after the designator and space)
			switch (firstCharacter)
			{
				//This line is a question
				case 'Q':
					{
						//Set it as the current question
						currentQuestion = new Question(name);
						//Add it to the list
						questions.Add(currentQuestion);
					}
					break;
				//This is a (correct) answer
				case 'C':
					{
						//add it to the current question
						if (currentQuestion != null)
							currentQuestion.AddAnswer(name, true);
					}
					break;
				//This is an (incorrect) answer
				case 'A':
					{
						//add it to the current question
						if (currentQuestion != null)
							currentQuestion.AddAnswer(name, false);
					}
					break;
				default: break; //Do nothing for any other designator
			}
		}
		/*
		//Logs all questions and answers for testing purposes
		foreach (Question q in questions)
		{
			Debug.Log("Question: " + q.GetName());
			for (int i = 0; i < q.GetNumberOfAnswers(); i++)
			{
				Answer a = q.GetAnswer(i);
				Debug.Log("Answer: " + a.GetName() + " Correct ? " + a.GetCorrect());
			}
		}
		*/
	}
}

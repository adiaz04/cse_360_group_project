using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuizController : MonoBehaviour
{
	//This resource must be set to load the questions
	public TextAsset questionFile;

	private List<Question> questions;
	// Use this for initialization
	void Start ()
	{
		//Load quiz questions
		if (questionFile) {
			string[] lines = questionFile.text.Split ('\n');

			int length = lines.GetLength(0);
			questions = new List<Question>();

			Question currentQuestion = null;
			for(int lineIndex = 0; lineIndex < length; lineIndex++)
			{
				string line = lines[lineIndex];
				//Format for a line is <Designator><Space><Name>
				char firstCharacter = line[0]; //Gets the first character "Designator:
				string name = line.Substring(2); //Gets the rest of the line (Which starts after the designator and space)
				switch(firstCharacter)
				{
				case 'Q':
				{
					currentQuestion = new Question(name);
					questions.Add(currentQuestion);
				} break;
				case 'C':
				{
					if(currentQuestion != null)
						currentQuestion.AddAnswer(name, true);
				} break;
				case 'A':
				{
					if(currentQuestion  != null)
						currentQuestion.AddAnswer(name, false);
				} break;
				default: break; //Do nothing for any other designator
				}
			}
			foreach(Question q in questions)
			{
				Debug.Log("Question: " + q.GetName());
				for(int i = 0; i < q.GetNumberOfAnswers(); i++)
				{
					Answer a = q.GetAnswer(i);
					Debug.Log("Answer: " + a.GetName() + " Correct ? " + a.GetCorrect());
				}
			}
		} else {
			throw new UnityException ("Couldn't load the Questions.txt file");
		}
		//Parse quiz questions
	}
	public Question GetQuestion()
	{
		int index = Random.Range(0, questions.Count);
		Question question = questions [index];
		return question;
	}
}

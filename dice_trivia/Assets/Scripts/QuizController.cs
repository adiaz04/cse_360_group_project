using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuizController : MonoBehaviour
{
	public List<Question> questions;
	// Use this for initialization
	void Start ()
	{
		//Load quiz questions
		TextAsset asset = Resources.Load ("Text/questions.txt") as TextAsset;
		if (asset) {
			string[] lines = asset.text.Split ('\n');

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

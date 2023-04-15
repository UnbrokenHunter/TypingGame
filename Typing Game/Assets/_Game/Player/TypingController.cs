using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TypingController : MonoBehaviour
{

	[SerializeField] private string[] _texts;

	[SerializeField] private TMP_Text _textMesh;
	[SerializeField] private Color _color;

	[SerializeField] private UnityEvent OnTextCompleted;

	private float _timeElapsed = 0;

	// The current place the player is in the text
	private int _currentIndex = 0; 
	// The current text element array item
	private int _currentArrayIndex = 0; 
	private string _input;

	private bool _isCompleted = false;
	private bool _isTypingStarted = false;

	private string _startColorHTML => $"<color=#{_color.ToHexString()}>";
	private readonly string _endColorHTML = "</color>";

	public void ResetValues()
	{
		_timeElapsed = 0;
		_currentIndex = 0;

		_isCompleted = false;
		_isTypingStarted = false;

		_currentArrayIndex++;

		UpdateText();
	}

	private void Awake() => UpdateText();

	private void UpdateText() => _textMesh.text = _texts[_currentArrayIndex];

	private void Update()
	{
		if (_isCompleted) return;

		if (_isTypingStarted) _timeElapsed += Time.deltaTime;

		// Get input
		string keyboardInput = GetKeyboardInput();

		// Make sure there was actually an input
		if (string.IsNullOrEmpty(keyboardInput)) 
			return;

		// Set input variable to the non empty variable
		_input = keyboardInput;

		// Check if the input was correct
		bool isCorrect = CompareInput(_input);

		// Replace the letter
		if (isCorrect) ReplaceLetter();

		CalculateWordsPerMinute();

	}

	/// <summary>
	/// Calculates the words per minute
	/// </summary>
	/// <returns></returns>
	public string CalculateWordsPerMinute()
	{
		float wordLength = 5;
		float keysPressed = _currentIndex;
		float minutesElapsed = (_timeElapsed / 60);
		
		float wpm = (keysPressed / wordLength) / minutesElapsed;

		return wpm.ToString("0");
	}

	/// <summary>
	/// Returns true if it is the correct input 
	/// False if not
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	private bool CompareInput(string input)
	{
		char inputAsChar = input.ToCharArray()[0];
		bool correctComparison = _texts[_currentArrayIndex][_currentIndex].Equals(inputAsChar);

		if (!correctComparison) 
			return false;

		// Increment the place in the text that the player is at
		_currentIndex++;

		return true;

	}

	/// <summary>
	/// Replace the correctly typed current letter with a different color
	/// </summary>
	private void ReplaceLetter()
	{

		bool isLastLetter = _currentIndex == _texts[_currentArrayIndex].Length;


		string text = _texts[_currentArrayIndex];

		text = isLastLetter ?
			text += _endColorHTML : // Is last letter
			text.Insert(_currentIndex, _endColorHTML); // Is not last letter

		text = _startColorHTML + text;

		_textMesh.text = text;



		// It is still a correct comparison, it is just not the last letter
		if (!isLastLetter) return;
		
		_isCompleted = true;
		OnTextCompleted?.Invoke();

	}

	/// <summary>
	/// Gets the key that was pressed and returns it as a string
	/// </summary>
	/// <returns></returns>
	private string GetKeyboardInput()
	{
		// Start Timer if this is the first input
		_isTypingStarted = true;

		bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

		for (KeyCode key = KeyCode.A; key <= KeyCode.Z; key++)
		{
			if (Input.GetKeyDown(key))
			{
				string letter = key.ToString();
				string shiftedLetter = isShiftPressed ? letter : letter.ToLower();
				return shiftedLetter;
			}
		}

		for (KeyCode key = KeyCode.Alpha0; key <= KeyCode.Alpha9; key++)
		{
			if (Input.GetKeyDown(key))
			{
				return key.ToString().Remove(0, 5);
			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
			return " ";

		return "";
	}

}

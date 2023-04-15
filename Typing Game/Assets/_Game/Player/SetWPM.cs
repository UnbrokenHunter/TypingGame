using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetWPM : MonoBehaviour
{
	[SerializeField] private TMP_Text _text;
	[SerializeField] private TypingController _player;

	[Space]

	[SerializeField] private string _beforeText;
	[SerializeField] private string _afterText;

	private void Update()
	{
		_text.text = _beforeText + _player.CalculateWordsPerMinute() + _afterText;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private float characterDelay = 0.08f;
    [SerializeField] private float sentenceDelay = 2;
    
    [Header("Refs")]
    [SerializeField] private Collider2D trigger;
    [SerializeField] private List<String> sentences = new List<string>();
    [SerializeField] private GameObject textObj;
    
    private Text uiText;
    private bool triggered = false; 
    
    private int sentenceIndex = 0;
    private int charIndex = 0;
    private bool sentenceComplete = false;
    
    private void Start()
    {
        uiText = textObj.GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !triggered)
            StartDialogue();
    }

    private void StartDialogue()
    {
        triggered = true;
        sentenceIndex = 0;
        uiText.text = "";
        StartCoroutine(PrintSentence(sentences[sentenceIndex]));
    }

    IEnumerator PrintSentence(string sentence) {
        foreach (char letter in sentence.ToCharArray()) {
                uiText.text += letter;
            yield return new WaitForSeconds(characterDelay);
        }

        print("done");
        print("Yes");
    }}

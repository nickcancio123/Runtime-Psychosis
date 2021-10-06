using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private float characterDelay = 0.08f;
    [SerializeField] private float sentenceDelay = 2;
    [SerializeField] private float textOffset = 1;
    [SerializeField] private int fontSize = 75;
    [SerializeField] private int maxCharactersPerLine = 20;
    [SerializeField] private List<String> sentences = new List<string>();
    [SerializeField] private GameObject textBox;
    [SerializeField] private bool destroyOnDeath = true;
    [SerializeField] private float destructionAfterDeathDelay = 8;
    [SerializeField] private GameObject jojoLights;
    private GuardController guardController;
    private GameObject textObj;
    private TextMesh tm;
    private bool triggered = false;
    private int sentenceIndex = 0;
    private int lineCharCount = 0;
    private Vector3 initialScale;

    private void Start()
    {
        initialScale = gameObject.transform.localScale;
        guardController = gameObject.transform.parent.gameObject.GetComponent<GuardController>();
        CreateTextMesh();
    }

    private void Update()
    {
        //Always make face right direction
        if (guardController.gameObject.transform.localScale.x < 0)
            textObj.transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
        else
            textObj.transform.localScale = initialScale;
        
        if (guardController.isDead)
        {
            if (destroyOnDeath)
            {
                SelfDestruct();
                return;   
            }
        }
        
        SetTextPosition();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Dialogue after death is can only be triggered after death
        if (!destroyOnDeath && !guardController.isDead)
            return;
        
        if (other.gameObject.CompareTag("Player") && !triggered)
            StartDialogue();
    }

    private void StartDialogue()
    {
        triggered = true;
        sentenceIndex = 0;
        tm.text = "";
        textBox.SetActive(true);
        
        if (jojoLights)
            jojoLights.SetActive(true);

        StartCoroutine(PrintCharacters());
    }

    IEnumerator PrintCharacters()
    {
        tm.text = "";
        
        foreach (char letter in sentences[sentenceIndex].ToCharArray()) {
            tm.text += letter;
            lineCharCount++;
            if (lineCharCount > maxCharactersPerLine && tm.text.ElementAt(tm.text.Length - 1) == ' ')
            {
                tm.text += "\n";
                lineCharCount = 0;
            }
            
            yield return new WaitForSeconds(characterDelay);
        }

        sentenceIndex++;
        if (sentenceIndex < sentences.Count)
            StartCoroutine(PrintNextSentence());
        else
        {
            StartCoroutine(DelayDestructionAfterDeath());
        }
    }

    IEnumerator PrintNextSentence()
    {
        lineCharCount = 0;
        yield return new WaitForSeconds(sentenceDelay);
        StartCoroutine(PrintCharacters());
    }

    
    private void SetTextPosition()
    {
        tm.transform.position = transform.position + Vector3.up * textOffset;
        textBox.transform.position = transform.position + Vector3.up * textOffset;
    }

    private void CreateTextMesh()
    {
        textObj = new GameObject("Speaker_Text");
        textObj.transform.parent = transform.parent;
        textObj.transform.position = Vector3.zero;
        MeshRenderer meshRenderer = textObj.AddComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = "Player";
        meshRenderer.sortingOrder = 10;
        tm = textObj.AddComponent<TextMesh>();
        tm.color = Color.white;
        tm.fontStyle = FontStyle.Italic;
        tm.alignment = TextAlignment.Center;
        tm.anchor = TextAnchor.MiddleCenter;
        tm.characterSize = 0.06f;
        tm.fontSize = fontSize;
    }

    IEnumerator DelayDestructionAfterDeath()
    {
        yield return new WaitForSeconds(destructionAfterDeathDelay);
        SelfDestruct();
    }

    private void SelfDestruct()
    {
        Destroy(textObj);
        Destroy(this.gameObject);
    }
}

using UnityEngine;
using TMPro;
using HuggingFace.API;
using Random = System.Random;

public class ConsoleManager : MonoBehaviour
{
    public TMP_InputField console;
    public OrdisV2 ordis;
    private string[] tagCandidates = {
        "bottle",
        "book",
        "rock"
    };

    void Start()
    {
        console.onSubmit.AddListener(SSTask);
    }

    void SSTask(string inputText)
    {
        if (inputText.Trim().Length > 0)
        {
            HuggingFaceAPI.SentenceSimilarity(inputText, OnSuccess, OnError, tagCandidates);
        }
        else{
            Debug.Log("Empty String!");
        }
    }

    void OnSuccess(float[] result)
    {
        console.text = string.Empty;
        int argmax = 0;
        float max = -10;

        for (int i = 0; i < result.Length; i++)
        {
            if (result[i] > max)
            {
                argmax = i;
                max = result[i];
            }
        }

        ordis.Search(tagCandidates[argmax]);
    }

    void OnError(string error)
    {
        Debug.LogError(error);
    }

    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            console.ActivateInputField();
        }
    }
}

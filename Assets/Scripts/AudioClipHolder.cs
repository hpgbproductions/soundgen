using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipHolder : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] AudioList;

    [SerializeField]
    private string[] AliasList;

    private void Start()
    {
        if (AudioList.Length != AliasList.Length | AudioList.Length == 0 | AliasList.Length == 0)
        {
            Debug.LogError("The lengths of AudioList and AliasList are not equal, or either length is zero.");
            return;
        }
    }

    public AudioClip SelectAudio(string alias)
    {
        for (int i = 0; i < AudioList.Length; i++)
        {
            if (alias == AliasList[i])
            {
                return AudioList[i];
            }
        }

        Debug.LogError("The selected AudioClip \"" + alias + "\" does not exist.");
        return AudioList[0];
    }
}

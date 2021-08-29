using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary : MonoBehaviour
{

    // collection of single specific sounds
    [System.Serializable]
    public class SoundSingle
    {
        public string soundId;
        public AudioClip sound;
    }

    public SoundSingle[] soundSingles;

    private Dictionary<string, AudioClip> singleDictionary = new Dictionary<string, AudioClip>();

    // collection of group random sounds
    [System.Serializable]
    public class SoundGroup
    {
        public string groupId;
        public AudioClip[] group;
    }

    public SoundGroup[] soundGroups;

    private Dictionary<string, AudioClip[]> groupDictionary = new Dictionary<string, AudioClip[]>();

    private void Awake()
    {
        foreach (SoundGroup group in soundGroups)
            groupDictionary.Add(group.groupId, group.group);

        foreach (SoundSingle single in soundSingles)
            singleDictionary.Add(single.soundId, single.sound);
    }


    public AudioClip GetRandomClipFromGroup(string name)
    {
        if (groupDictionary.ContainsKey(name))
        {
            AudioClip[] sounds = groupDictionary[name];
            return sounds[Random.Range(0, sounds.Length)];
        }

        return null;
    }

    public AudioClip GetSpecificClipFromName(string name)
    {
        if (singleDictionary.ContainsKey(name)) 
            return singleDictionary[name];

        return null;
    }
}

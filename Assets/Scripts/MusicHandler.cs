
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler
{
    public List<AudioReverbFilter> audioReverbFilterList;

    // Create a hashmap liking string to AudioSource
    private Dictionary<string, AudioSource> soundDictionary;
    public MusicHandler()
    {
        audioReverbFilterList = new List<AudioReverbFilter>();

        CreateSoundDictionary();
    }

    private void CreateSoundDictionary()
    {
        soundDictionary = new Dictionary<string, AudioSource>();

        AudioClip guitarA3 = Resources.Load<AudioClip>("Audios/Music-1/Git220Hz");
        GameObject guitarA3Object = new GameObject("GuitarAudioSource");
        AudioSource guitarA3Source = guitarA3Object.AddComponent<AudioSource>();
        guitarA3Source.clip = guitarA3;
        guitarA3Source.volume = 0.5f;
        soundDictionary.Add("Guitar_A3", guitarA3Source);
        AudioReverbFilter guitarReverbFilter = guitarA3Object.AddComponent<AudioReverbFilter>();
        guitarReverbFilter.reverbPreset = AudioReverbPreset.Cave;

        AudioClip guitarC4 = Resources.Load<AudioClip>("Audios/Music-1/Git220Hz");
        GameObject guitarC4Object = new GameObject("GuitarC4AudioSource");
        AudioSource guitarC4Source = guitarC4Object.AddComponent<AudioSource>();
        guitarC4Source.clip = guitarC4;
        guitarC4Source.volume = 0.5f;
        // Transform from 220Hz (A3) to 261.6256Hz (C4)
        guitarC4Source.pitch = 261.6256f / 220f; // Adjust pitch to match the desired frequency
        soundDictionary.Add("Guitar_C4", guitarC4Source);
        AudioReverbFilter guitarC4ReverbFilter = guitarC4Object.AddComponent<AudioReverbFilter>();
        guitarC4ReverbFilter.reverbPreset = AudioReverbPreset.Cave;

        AudioClip guitarAH4 = Resources.Load<AudioClip>("Audios/Music-1/Git440Hz");
        GameObject guitarAH4Object = new GameObject("GuitarAudioSource");
        AudioSource guitarAH4Source = guitarAH4Object.AddComponent<AudioSource>();
        guitarAH4Source.clip = guitarAH4;
        guitarAH4Source.volume = 0.5f;
        // Transform from 440Hz (A4) to 466.1638Hz (A#4)
        guitarAH4Source.pitch = 466.1638f / 440f; // Adjust pitch to match the desired frequency
        soundDictionary.Add("Guitar_AH4", guitarAH4Source);
        AudioReverbFilter guitarAH4ReverbFilter = guitarAH4Object.AddComponent<AudioReverbFilter>();
        guitarAH4ReverbFilter.reverbPreset = AudioReverbPreset.Cave;

        AudioClip guitarF1 = Resources.Load<AudioClip>("Audios/Music-1/Git110Hz");
        GameObject guitarF1Object = new GameObject("GuitarF1AudioSource");
        AudioSource guitarF1Source = guitarF1Object.AddComponent<AudioSource>();
        guitarF1Source.clip = guitarF1;
        guitarF1Source.volume = 0.5f;
        // Transform from 110Hz (A2) to 43.65 Hz (C3)
        guitarF1Source.pitch = 43.65f / 110f; // Adjust pitch to match the desired frequency
        soundDictionary.Add("Guitar_F1", guitarF1Source);
        AudioReverbFilter guitarF1ReverbFilter = guitarF1Object.AddComponent<AudioReverbFilter>();
        guitarF1ReverbFilter.reverbPreset = AudioReverbPreset.Cave;

        AudioClip guitarFH3 = Resources.Load<AudioClip>("Audios/Music-1/Git220Hz");
        GameObject guitarFH3Object = new GameObject("GuitarFH3AudioSource");
        AudioSource guitarFH3Source = guitarFH3Object.AddComponent<AudioSource>();
        guitarFH3Source.clip = guitarFH3;
        guitarFH3Source.volume = 0.5f;
        // Transform from 220Hz (A3) to 185Hz (F#3)
        guitarFH3Source.pitch = 185f / 220f; // Adjust pitch to match the desired frequency
        soundDictionary.Add("Guitar_FH3", guitarFH3Source);
        AudioReverbFilter guitarFH3ReverbFilter = guitarFH3Object.AddComponent<AudioReverbFilter>();
        guitarFH3ReverbFilter.reverbPreset = AudioReverbPreset.Cave;

        AudioClip pianoAH4 = Resources.Load<AudioClip>("Audios/Music-1/Piano_69_96");
        GameObject pianoAH4Object = new GameObject("PianoAudioSource");
        AudioSource pianoAH4Source = pianoAH4Object.AddComponent<AudioSource>();
        pianoAH4Source.clip = pianoAH4;
        pianoAH4Source.volume = 0.5f;
        // Transform from 440Hz (C4) to 466.1638Hz (A4)
        pianoAH4Source.pitch = 466.1638f / 440f; // Adjust pitch to match the desired frequency
        soundDictionary.Add("Piano_AH4", pianoAH4Source);
        AudioReverbFilter pianoReverbFilter = pianoAH4Object.AddComponent<AudioReverbFilter>();
        pianoReverbFilter.reverbPreset = AudioReverbPreset.Cave;

        AudioClip HHatClosed = Resources.Load<AudioClip>("Audios/Music-1/Yamaha-HHatClosed");
        GameObject hatClosedObject = new GameObject("HatClosedAudioSource");
        AudioSource hatClosedSource = hatClosedObject.AddComponent<AudioSource>();
        hatClosedSource.clip = HHatClosed;
        soundDictionary.Add("Hat_Closed", hatClosedSource);
        AudioReverbFilter hatClosedReverbFilter = hatClosedObject.AddComponent<AudioReverbFilter>();
        hatClosedReverbFilter.reverbPreset = AudioReverbPreset.Cave;

        AudioClip HiTomFon = Resources.Load<AudioClip>("Audios/Music-1/HiTomFon_57_96");
        GameObject hiTomFonObject = new GameObject("HiTomFonAudioSource");
        AudioSource hiTomFonSource = hiTomFonObject.AddComponent<AudioSource>();
        hiTomFonSource.clip = HiTomFon;
        soundDictionary.Add("HiTom_Fon", hiTomFonSource);
        AudioReverbFilter hiTomFonReverbFilter = hiTomFonObject.AddComponent<AudioReverbFilter>();
        hiTomFonReverbFilter.reverbPreset = AudioReverbPreset.Cave;

        AudioClip HiTomFonF5 = Resources.Load<AudioClip>("Audios/Music-1/HiTomFon_81_96");
        GameObject hiTomFonF5Object = new GameObject("HiTomFonF5AudioSource");
        AudioSource hiTomFonF5Source = hiTomFonF5Object.AddComponent<AudioSource>();
        hiTomFonF5Source.clip = HiTomFonF5;
        // Transform from 880.0000 (A5) to 698.4565 Hz (F5)
        hiTomFonF5Source.pitch = 698.4565f / 880f; // Adjust pitch to match the desired frequency
        soundDictionary.Add("HiTom_Fon_F5", hiTomFonF5Source);
        AudioReverbFilter hiTomFonF5ReverbFilter = hiTomFonF5Object.AddComponent<AudioReverbFilter>();
        hiTomFonF5ReverbFilter.reverbPreset = AudioReverbPreset.Cave;

        AudioClip Kick = Resources.Load<AudioClip>("Audios/Music-1/YamahaKick");
        GameObject kickObject = new GameObject("KickAudioSource");
        AudioSource kickSource = kickObject.AddComponent<AudioSource>();
        kickSource.clip = Kick;
        soundDictionary.Add("Kick", kickSource);
        AudioReverbFilter kickReverbFilter = kickObject.AddComponent<AudioReverbFilter>();
        kickReverbFilter.reverbPreset = AudioReverbPreset.Cave;

        // Add fx sounds
        AudioClip correctCollision = Resources.Load<AudioClip>("Audios/Effects/correct-collision");
        GameObject correctCollisionObject = new GameObject("CorrectCollisionAudioSource");
        AudioSource correctCollisionSource = correctCollisionObject.AddComponent<AudioSource>();
        correctCollisionSource.clip = correctCollision;
        correctCollisionSource.volume = 1f;
        soundDictionary.Add("Correct_Collision", correctCollisionSource);

        AudioClip wrongCollision = Resources.Load<AudioClip>("Audios/Effects/wrong-collision");
        GameObject wrongCollisionObject = new GameObject("WrongCollisionAudioSource");
        AudioSource wrongCollisionSource = wrongCollisionObject.AddComponent<AudioSource>();
        wrongCollisionSource.clip = wrongCollision;
        wrongCollisionSource.volume = 1f;
        soundDictionary.Add("Wrong_Collision", wrongCollisionSource);

        AudioClip winSound = Resources.Load<AudioClip>("Audios/Effects/win-sound");
        GameObject winSoundObject = new GameObject("WinSoundAudioSource");
        AudioSource winSoundSource = winSoundObject.AddComponent<AudioSource>();
        winSoundSource.clip = winSound;
        winSoundSource.volume = 1f;
        soundDictionary.Add("Win_Sound", winSoundSource);

        AudioClip loseSound = Resources.Load<AudioClip>("Audios/Effects/lose-sound");
        GameObject loseSoundObject = new GameObject("LoseSoundAudioSource");
        AudioSource loseSoundSource = loseSoundObject.AddComponent<AudioSource>();
        loseSoundSource.clip = loseSound;
        loseSoundSource.volume = 1f;
        soundDictionary.Add("Lose_Sound", loseSoundSource);

        

    }

    public void LoadFirstRingMusic1(List<CirclesRing> circlesRingList)
    {
        List<int> firstRingPolygon = new List<int> { 4, 22 };
        List<string> firstRingTracks = new List<string> { "Hat_Closed", null, null };
        List<float> firstRingDuration = new List<float> { 0.120f, -1, -1 };
        List<float> firstRingVolume = new List<float> { 0.5f, -1, -1 };

        List<int> secondRing = new List<int> { 14, 32 };
        List<string> secondRingTracks = new List<string> { "Piano_AH4", "Guitar_AH4", "HiTom_Fon" };
        List<float> secondRingDuration = new List<float> { 0.120f, 0.120f, 0.120f };
        List<float> secondRingVolume = new List<float> { 0.5f, 0.5f, 1f };

        List<int> thirdRing = new List<int> { 10, 22, 34 };
        List<string> thirdRingTracks = new List<string> { "HiTom_Fon_F5", null, null };
        List<float> thirdRingDuration = new List<float> { 0.120f, -1, -1 };
        List<float> thirdRingVolume = new List<float> { 1f, -1, -1 };

        List<int> fourthRing = new List<int> { 6, 18, 30 };
        List<string> fourthRingTracks = new List<string> { "Guitar_C4", "HiTom_Fon", null };
        List<float> fourthRingDuration = new List<float> { 0.220f, 0.120f, -1 };
        List<float> fourthRingVolume = new List<float> { 0.5f, 1f, -1 };

        List<int> fifthRing = new List<int> { 0, 6, 12, 18, 24, 30 };
        List<string> fifthRingTracks = new List<string> { "Guitar_F1", "Kick", null };
        List<float> fifthRingDuration = new List<float> { 0.120f, 0.120f, -1 };
        List<float> fifthRingVolume = new List<float> { 0.5f, 1f, -1 };

        List<int> sixthRing = new List<int> { 0, 4, 8, 12, 16, 20, 24, 28, 32 };
        List<string> sixthRingTracks = new List<string> { "Guitar_FH3", "HiTom_Fon", null };
        List<float> sixthRingDuration = new List<float> { 0.120f, 0.120f, -1 };
        List<float> sixthRingVolume = new List<float> { 0.5f, 1f, -1 };

        List<List<int>> firstRingPolygonList = new List<List<int>> { firstRingPolygon, secondRing, thirdRing, fourthRing, fifthRing, sixthRing };
        List<List<string>> firstRingTracksList = new List<List<string>> { firstRingTracks, secondRingTracks, thirdRingTracks, fourthRingTracks, fifthRingTracks, sixthRingTracks };
        List<List<float>> firstRingDurationList = new List<List<float>> { firstRingDuration, secondRingDuration, thirdRingDuration, fourthRingDuration, fifthRingDuration, sixthRingDuration };
        List<List<float>> firstRingVolumeList = new List<List<float>> { firstRingVolume, secondRingVolume, thirdRingVolume, fourthRingVolume, fifthRingVolume, sixthRingVolume };

        for (int i = 0; i < circlesRingList.Count; i++)
        {
            CirclesRing circlesRing = circlesRingList[i];
            List<int> currentRingPolygon = firstRingPolygonList[i];
            List<string> currentRingTracks = firstRingTracksList[i];
            List<float> currentRingDuration = firstRingDurationList[i];
            List<float> currentRingVolume = firstRingVolumeList[i];

            foreach (int currentPolygonPosition in currentRingPolygon)
            {
                // Transform into clockwise order
                int currentPolygonPositionClockWise = currentPolygonPosition;

                circlesRing.circles[currentPolygonPositionClockWise].isSpecial = true; // Mark the circles in the current polygon as special
                circlesRing.circles[currentPolygonPositionClockWise].SetColor(Globals.ColorList[i]); // Set the color of the circles in the current polygon
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack1 = currentRingTracks[0]; // Assign the first audio track to the circle
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack2 = currentRingTracks[1]; // Assign the second audio track to the circle
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack3 = currentRingTracks[2]; // Assign the third audio track to the circle

                circlesRing.circles[currentPolygonPositionClockWise].audioTrack1Duration = currentRingDuration[0]; // Assign the first audio track duration to the circle
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack2Duration = currentRingDuration[1]; // Assign the second audio track duration to the circle
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack3Duration = currentRingDuration[2]; // Assign the third audio track duration to the circle

                circlesRing.circles[currentPolygonPositionClockWise].audioTrack1Volume = currentRingVolume[0]; // Assign the first audio track volume to the circle
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack2Volume = currentRingVolume[1]; // Assign the second audio track volume to the circle
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack3Volume = currentRingVolume[2]; // Assign the third audio track volume to the circle
            }
        }

        circlesRingList[0].Activate();
        //circlesRingList[1].Activate();
        //circlesRingList[2].Activate();
        //circlesRingList[3].Activate();
        //circlesRingList[4].Activate();
        //circlesRingList[5].Activate(); // Activate the first and last circles ring
    }

    public void PlayTrack(string track, float volume, float durationSeconds)
    {

        if (track == null)
        {
            return;
        }

        AudioSource selectedAudioSource = soundDictionary[track];

        if (selectedAudioSource == null)
        {
            Debug.LogWarning($"Audio source for track '{track}' not found.");
            return;
        }

        selectedAudioSource.Play();

        // Create a coroutine to stop the audio after the specified duration
        GlobalHandler.Instance.StartCoroutine(StopAudioAfterDuration(selectedAudioSource, volume, durationSeconds));
    }
    
    private System.Collections.IEnumerator StopAudioAfterDuration(AudioSource audioSource, float volume, float duration)
    {
        if (duration <= 0)
        {
            yield break; // If duration is not specified or invalid, do nothing
        }
        float startVolume = volume;
        float elapsedTime = 0f;
        float fadeOutTime = duration * 0.8f; 

        yield return new WaitForSeconds(duration - fadeOutTime);
        
        // Fade out
        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = startVolume * (1 - (elapsedTime / fadeOutTime));
            yield return null;
        }
    }

}
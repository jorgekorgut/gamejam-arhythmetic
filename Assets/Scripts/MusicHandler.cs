
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler
{
    public class AudioParameters
    {
        public AudioSource audioSource;

        public AudioReverbFilter reverbFilter; // Optional reverb filter for the audio source
    }
    public List<AudioReverbFilter> audioReverbFilterList;

    // Create a hashmap liking string to AudioSource
    private Dictionary<string, AudioParameters> soundDictionary;

    private AudioParameters gameLoopAudioSource;
    private int gameLoopInitialVolume = 1;

    private float volumeIntensity = 0.5f;

    public MusicHandler()
    {
        audioReverbFilterList = new List<AudioReverbFilter>();

        gameLoopAudioSource = new AudioParameters();
        gameLoopAudioSource.audioSource = new GameObject("GameLoopAudioSource").AddComponent<AudioSource>();
        AudioClip gameLoopClip = Resources.Load<AudioClip>("Audios/Effects/game-loop");
        gameLoopAudioSource.audioSource.clip = gameLoopClip; // Assign the game loop audio clip
        gameLoopAudioSource.audioSource.loop = true; // Set the game loop audio source to loop
        gameLoopAudioSource.audioSource.volume = 0.5f; // Set the volume for the game loop audio source
        gameLoopAudioSource.audioSource.playOnAwake = false; // Prevent it from playing immediately

        CreateSoundDictionary();
    }

    public void SetVolume(float volumeIntensity)
    {
        if (gameLoopAudioSource.audioSource != null)
        {
            gameLoopAudioSource.audioSource.volume = gameLoopInitialVolume * volumeIntensity; // Set the initial volume for the game loop audio source
        }

        this.volumeIntensity = volumeIntensity; // Store the volume intensity for later use
        Debug.Log("Volume intensity set to: " + this.volumeIntensity);
    }

    public void PlayGameLoopMusic()
    {
        if (gameLoopAudioSource.audioSource != null && !gameLoopAudioSource.audioSource.isPlaying)
        {
            gameLoopAudioSource.audioSource.Play(); // Play the game loop audio source
        }
    }

    public void StopGameLoopMusic()
    {
        if (gameLoopAudioSource.audioSource != null && gameLoopAudioSource.audioSource.isPlaying)
        {
            gameLoopAudioSource.audioSource.Stop(); // Stop the game loop audio source
        }
    }

    private void CreateSoundDictionary()
    {
        soundDictionary = new Dictionary<string, AudioParameters>();

        AudioClip guitar220 = Resources.Load<AudioClip>("Audios/Music-1/Git220Hz");
        AudioClip guitar440 = Resources.Load<AudioClip>("Audios/Music-1/Git440Hz");
        AudioClip guitar110 = Resources.Load<AudioClip>("Audios/Music-1/Git110Hz");

        AudioClip nylon220 = Resources.Load<AudioClip>("Audios/Music-1/Nylon_57_96");
        AudioClip nylon55 = Resources.Load<AudioClip>("Audios/Music-1/Nylon_33_96");

        AudioClip piano440 = Resources.Load<AudioClip>("Audios/Music-1/Piano_69_96");


        GameObject guitarA3Object = new GameObject("GuitarAudioSource");
        AudioSource guitarA3Source = guitarA3Object.AddComponent<AudioSource>();
        guitarA3Source.clip = guitar220;
        guitarA3Source.volume = 0f;
        AudioReverbFilter guitarReverbFilter = guitarA3Object.AddComponent<AudioReverbFilter>();
        guitarReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Guitar_A3", new AudioParameters { audioSource = guitarA3Source, reverbFilter = guitarReverbFilter });

        GameObject guitarC3Object = new GameObject("GuitarC3AudioSource");
        AudioSource guitarC3Source = guitarC3Object.AddComponent<AudioSource>();
        guitarC3Source.clip = guitar110;
        guitarC3Source.volume = 0f;
        // Transform from 110Hz (A2) to 130.8128Hz (C3)
        guitarC3Source.pitch = 130.8128f / 110f; // Adjust pitch to match the desired frequency
        AudioReverbFilter guitarC3ReverbFilter = guitarC3Object.AddComponent<AudioReverbFilter>();
        guitarC3ReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Guitar_C3", new AudioParameters { audioSource = guitarC3Source, reverbFilter = guitarC3ReverbFilter });

        GameObject guitarB3Object = new GameObject("GuitarB3AudioSource");
        AudioSource guitarB3Source = guitarB3Object.AddComponent<AudioSource>();
        guitarB3Source.clip = guitar220;
        guitarB3Source.volume = 0f;
        // Transform from 220Hz (A3) to 246.9417Hz (B3)
        guitarB3Source.pitch = 246.9417f / 220f; // Adjust pitch to match the desired frequency
        AudioReverbFilter guitarB3ReverbFilter = guitarB3Object.AddComponent<AudioReverbFilter>();
        guitarB3ReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Guitar_B3", new AudioParameters { audioSource = guitarB3Source, reverbFilter = guitarB3ReverbFilter });

        GameObject guitarA2Object = new GameObject("GuitarA2AudioSource");
        AudioSource guitarA2Source = guitarA2Object.AddComponent<AudioSource>();
        guitarA2Source.clip = guitar110;
        guitarA2Source.volume = 0f;
        AudioReverbFilter guitarA2ReverbFilter = guitarA2Object.AddComponent<AudioReverbFilter>();
        guitarA2ReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Guitar_A2", new AudioParameters { audioSource = guitarA2Source, reverbFilter = guitarA2ReverbFilter });

        GameObject guitarB2Object = new GameObject("GuitarB2AudioSource");
        AudioSource guitarB2Source = guitarB2Object.AddComponent<AudioSource>();
        guitarB2Source.clip = guitar110;
        guitarB2Source.volume = 0f;
        // Transform from 110Hz (A2) to 123.4708Hz (B2)
        guitarB2Source.pitch = 123.4708f / 110f; // Adjust pitch to match the desired frequency
        AudioReverbFilter guitarB2ReverbFilter = guitarB2Object.AddComponent<AudioReverbFilter>();
        guitarB2ReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Guitar_B2", new AudioParameters { audioSource = guitarB2Source, reverbFilter = guitarB2ReverbFilter });

        GameObject guitarDH3Object = new GameObject("GuitarDH3AudioSource");
        AudioSource guitarDH3Source = guitarDH3Object.AddComponent<AudioSource>();
        guitarDH3Source.clip = guitar110;
        guitarDH3Source.volume = 0f;
        // Transform from 110Hz (A2) to 155.5635 Hz (D#3)
        guitarDH3Source.pitch = 155.5635f / 110f; // Adjust pitch to match the desired frequency
        AudioReverbFilter guitarDH3ReverbFilter = guitarDH3Object.AddComponent<AudioReverbFilter>();
        guitarDH3ReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Guitar_DH3", new AudioParameters { audioSource = guitarDH3Source, reverbFilter = guitarDH3ReverbFilter });

        GameObject guitarG3Object = new GameObject("GuitarG3AudioSource");
        AudioSource guitarG3Source = guitarG3Object.AddComponent<AudioSource>();
        guitarG3Source.clip = guitar220;
        guitarG3Source.volume = 0f;
        // Transform from 220Hz (A3) to 196.0000 Hz (G3)
        guitarG3Source.pitch = 196f / 220f; // Adjust pitch to match the desired frequency
        AudioReverbFilter guitarG3ReverbFilter = guitarG3Object.AddComponent<AudioReverbFilter>();
        guitarG3ReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Guitar_G3", new AudioParameters { audioSource = guitarG3Source, reverbFilter = guitarG3ReverbFilter });

        GameObject guitarC4Object = new GameObject("GuitarC4AudioSource");
        AudioSource guitarC4Source = guitarC4Object.AddComponent<AudioSource>();
        guitarC4Source.clip = guitar220;
        guitarC4Source.volume = 0f;
        // Transform from 220Hz (A3) to 261.6256Hz (C4)
        guitarC4Source.pitch = 261.6256f / 220f; // Adjust pitch to match the desired frequency
        AudioReverbFilter guitarC4ReverbFilter = guitarC4Object.AddComponent<AudioReverbFilter>();
        guitarC4ReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Guitar_C4", new AudioParameters { audioSource = guitarC4Source, reverbFilter = guitarC4ReverbFilter });


        GameObject guitarAH4Object = new GameObject("GuitarAudioSource");
        AudioSource guitarAH4Source = guitarAH4Object.AddComponent<AudioSource>();
        guitarAH4Source.clip = guitar440;
        guitarAH4Source.volume = 0f;
        // Transform from 440Hz (A4) to 466.1638Hz (A#4)
        guitarAH4Source.pitch = 466.1638f / 440f; // Adjust pitch to match the desired frequency
        AudioReverbFilter guitarAH4ReverbFilter = guitarAH4Object.AddComponent<AudioReverbFilter>();
        guitarAH4ReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Guitar_AH4", new AudioParameters { audioSource = guitarAH4Source, reverbFilter = guitarAH4ReverbFilter });

        GameObject guitarF1Object = new GameObject("GuitarF1AudioSource");
        AudioSource guitarF1Source = guitarF1Object.AddComponent<AudioSource>();
        guitarF1Source.clip = guitar110;
        guitarF1Source.volume = 0f;
        // Transform from 110Hz (A2) to 43.65 Hz (C3)
        guitarF1Source.pitch = 43.65f / 110f; // Adjust pitch to match the desired frequency
        AudioReverbFilter guitarF1ReverbFilter = guitarF1Object.AddComponent<AudioReverbFilter>();
        guitarF1ReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Guitar_F1", new AudioParameters { audioSource = guitarF1Source, reverbFilter = guitarF1ReverbFilter });

        GameObject guitarFH3Object = new GameObject("GuitarFH3AudioSource");
        AudioSource guitarFH3Source = guitarFH3Object.AddComponent<AudioSource>();
        guitarFH3Source.clip = guitar220;
        guitarFH3Source.volume = 0f;
        // Transform from 220Hz (A3) to 185Hz (F#3)
        guitarFH3Source.pitch = 185f / 220f; // Adjust pitch to match the desired frequency
        AudioReverbFilter guitarFH3ReverbFilter = guitarFH3Object.AddComponent<AudioReverbFilter>();
        guitarFH3ReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Guitar_FH3", new AudioParameters { audioSource = guitarFH3Source, reverbFilter = guitarFH3ReverbFilter });

        GameObject nylonGH3Object = new GameObject("NylonGH3AudioSource");
        AudioSource nylonGH3Source = nylonGH3Object.AddComponent<AudioSource>();
        nylonGH3Source.clip = nylon220;
        nylonGH3Source.volume = 0f;
        // Transform from 220Hz (A3) to 196.0000 Hz (G3)    
        nylonGH3Source.pitch = 196f / 220f; // Adjust pitch to match the desired frequency
        AudioReverbFilter nylonGH3ReverbFilter = nylonGH3Object.AddComponent<AudioReverbFilter>();
        nylonGH3ReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Nylon_GH3", new AudioParameters { audioSource = nylonGH3Source, reverbFilter = nylonGH3ReverbFilter });

        GameObject nylonE3Object = new GameObject("NylonE3AudioSource");
        AudioSource nylonE3Source = nylonE3Object.AddComponent<AudioSource>();
        nylonE3Source.clip = nylon220;
        nylonE3Source.volume = 0f;
        // Transform from 220Hz (A3) to 164.8138 Hz (E3)
        nylonE3Source.pitch = 164.8138f / 220f; // Adjust pitch to match the desired frequency
        AudioReverbFilter nylonE3ReverbFilter = nylonE3Object.AddComponent<AudioReverbFilter>();
        nylonE3ReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Nylon_E3", new AudioParameters { audioSource = nylonE3Source, reverbFilter = nylonE3ReverbFilter });

        GameObject nylonA1Object = new GameObject("NylonA1AudioSource");
        AudioSource nylonA1Source = nylonA1Object.AddComponent<AudioSource>();
        nylonA1Source.clip = nylon55;
        nylonA1Source.volume = 0f;
        AudioReverbFilter nylonA1ReverbFilter = nylonA1Object.AddComponent<AudioReverbFilter>();
        nylonA1ReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Nylon_A1", new AudioParameters { audioSource = nylonA1Source, reverbFilter = nylonA1ReverbFilter });

        GameObject pianoAH4Object = new GameObject("PianoAudioSource");
        AudioSource pianoAH4Source = pianoAH4Object.AddComponent<AudioSource>();
        pianoAH4Source.clip = piano440;
        pianoAH4Source.volume = 0f;
        // Transform from 440Hz (C4) to 466.1638Hz (A4)
        pianoAH4Source.pitch = 466.1638f / 440f; // Adjust pitch to match the desired frequency
        AudioReverbFilter pianoReverbFilter = pianoAH4Object.AddComponent<AudioReverbFilter>();
        pianoReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Piano_AH4", new AudioParameters { audioSource = pianoAH4Source, reverbFilter = pianoReverbFilter });

        GameObject pianoCH5Object = new GameObject("PianoCH5AudioSource");
        AudioSource pianoCH5Source = pianoCH5Object.AddComponent<AudioSource>();
        pianoCH5Source.clip = piano440;
        pianoCH5Source.volume = 0f;
        // Transform from 440Hz (C4) to 554.36Hz (CH5)
        pianoCH5Source.pitch = 554.36f / 440f; // Adjust pitch to match the desired frequency
        AudioReverbFilter pianoCH5ReverbFilter = pianoCH5Object.AddComponent<AudioReverbFilter>();
        pianoCH5ReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Piano_CH5", new AudioParameters { audioSource = pianoCH5Source, reverbFilter = pianoCH5ReverbFilter });

        GameObject pianoGH4Object = new GameObject("PianoGH4AudioSource");
        AudioSource pianoGH4Source = pianoGH4Object.AddComponent<AudioSource>();
        pianoGH4Source.clip = piano440;
        pianoGH4Source.volume = 0f;
        // Transform from 440Hz (C4) to 415.3047Hz (GH4)
        pianoGH4Source.pitch = 415.3047f / 440f; // Adjust pitch to match the desired frequency
        AudioReverbFilter pianoGH4ReverbFilter = pianoGH4Object.AddComponent<AudioReverbFilter>();
        pianoGH4ReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Piano_GH4", new AudioParameters { audioSource = pianoGH4Source, reverbFilter = pianoGH4ReverbFilter });

        GameObject pianoE5Object = new GameObject("PianoE5AudioSource");
        AudioSource pianoE5Source = pianoE5Object.AddComponent<AudioSource>();
        pianoE5Source.clip = piano440;
        pianoE5Source.volume = 0f;
        // Transform from 440Hz (C4) to 659.2551Hz (E5)
        pianoE5Source.pitch = 659.2551f / 440f; // Adjust pitch to match the desired frequency
        AudioReverbFilter pianoE5ReverbFilter = pianoE5Object.AddComponent<AudioReverbFilter>();
        pianoE5ReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Piano_E5", new AudioParameters { audioSource = pianoE5Source, reverbFilter = pianoE5ReverbFilter });

        AudioClip HHatClosed = Resources.Load<AudioClip>("Audios/Music-1/Yamaha-HHatClosed");
        GameObject hatClosedObject = new GameObject("HatClosedAudioSource");
        AudioSource hatClosedSource = hatClosedObject.AddComponent<AudioSource>();
        hatClosedSource.clip = HHatClosed;
        AudioReverbFilter hatClosedReverbFilter = hatClosedObject.AddComponent<AudioReverbFilter>();
        hatClosedReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Hat_Closed", new AudioParameters { audioSource = hatClosedSource, reverbFilter = hatClosedReverbFilter });


        AudioClip HiTomFon = Resources.Load<AudioClip>("Audios/Music-1/HiTomFon_57_96");
        GameObject hiTomFonObject = new GameObject("HiTomFonAudioSource");
        AudioSource hiTomFonSource = hiTomFonObject.AddComponent<AudioSource>();
        hiTomFonSource.clip = HiTomFon;
        AudioReverbFilter hiTomFonReverbFilter = hiTomFonObject.AddComponent<AudioReverbFilter>();
        hiTomFonReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("HiTom_Fon", new AudioParameters { audioSource = hiTomFonSource, reverbFilter = hiTomFonReverbFilter });

        AudioClip HiTomFonF5 = Resources.Load<AudioClip>("Audios/Music-1/HiTomFon_81_96");
        GameObject hiTomFonF5Object = new GameObject("HiTomFonF5AudioSource");
        AudioSource hiTomFonF5Source = hiTomFonF5Object.AddComponent<AudioSource>();
        hiTomFonF5Source.clip = HiTomFonF5;
        // Transform from 880.0000 (A5) to 698.4565 Hz (F5)
        hiTomFonF5Source.pitch = 698.4565f / 880f; // Adjust pitch to match the desired frequency
        AudioReverbFilter hiTomFonF5ReverbFilter = hiTomFonF5Object.AddComponent<AudioReverbFilter>();
        hiTomFonF5ReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("HiTom_Fon_F5", new AudioParameters { audioSource = hiTomFonF5Source, reverbFilter = hiTomFonF5ReverbFilter });


        AudioClip Kick = Resources.Load<AudioClip>("Audios/Music-1/YamahaKick");
        GameObject kickObject = new GameObject("KickAudioSource");
        AudioSource kickSource = kickObject.AddComponent<AudioSource>();
        kickSource.clip = Kick;
        AudioReverbFilter kickReverbFilter = kickObject.AddComponent<AudioReverbFilter>();
        kickReverbFilter.reverbPreset = AudioReverbPreset.Cave;
        soundDictionary.Add("Kick", new AudioParameters { audioSource = kickSource, reverbFilter = kickReverbFilter });

        // Add fx sounds
        AudioClip correctCollision = Resources.Load<AudioClip>("Audios/Effects/correct-collision");
        GameObject correctCollisionObject = new GameObject("CorrectCollisionAudioSource");
        AudioSource correctCollisionSource = correctCollisionObject.AddComponent<AudioSource>();
        correctCollisionSource.clip = correctCollision;
        correctCollisionSource.volume = 0f;
        soundDictionary.Add("Correct_Collision", new AudioParameters { audioSource = correctCollisionSource});

        AudioClip wrongCollision = Resources.Load<AudioClip>("Audios/Effects/wrong-collision");
        GameObject wrongCollisionObject = new GameObject("WrongCollisionAudioSource");
        AudioSource wrongCollisionSource = wrongCollisionObject.AddComponent<AudioSource>();
        wrongCollisionSource.clip = wrongCollision;
        wrongCollisionSource.volume = 0f;
        soundDictionary.Add("Wrong_Collision", new AudioParameters { audioSource = wrongCollisionSource});

        AudioClip winSound = Resources.Load<AudioClip>("Audios/Effects/win-sound");
        GameObject winSoundObject = new GameObject("WinSoundAudioSource");
        AudioSource winSoundSource = winSoundObject.AddComponent<AudioSource>();
        winSoundSource.clip = winSound;
        winSoundSource.volume = 0f;
        soundDictionary.Add("Win_Sound", new AudioParameters { audioSource = winSoundSource});

        AudioClip loseSound = Resources.Load<AudioClip>("Audios/Effects/lose-sound");
        GameObject loseSoundObject = new GameObject("LoseSoundAudioSource");
        AudioSource loseSoundSource = loseSoundObject.AddComponent<AudioSource>();
        loseSoundSource.clip = loseSound;
        loseSoundSource.volume = 0f;
        soundDictionary.Add("Lose_Sound", new AudioParameters { audioSource = loseSoundSource});
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
        List<float> thirdRingVolume = new List<float> { 0.6f, -1, -1 };

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

                circlesRing.circles[currentPolygonPositionClockWise].SetSpecial();
                circlesRing.circles[currentPolygonPositionClockWise].SetColor(Globals.ColorList[i]);
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack1 = currentRingTracks[0];
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack2 = currentRingTracks[1];
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack3 = currentRingTracks[2];

                circlesRing.circles[currentPolygonPositionClockWise].audioTrack1Duration = currentRingDuration[0];
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack2Duration = currentRingDuration[1];
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack3Duration = currentRingDuration[2];

                circlesRing.circles[currentPolygonPositionClockWise].audioTrack1Volume = currentRingVolume[0];
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack2Volume = currentRingVolume[1];
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack3Volume = currentRingVolume[2];
            }
        }

        circlesRingList[0].Activate();
    }

    public void LoadFirstRingMusic2(List<CirclesRing> circlesRingList)
    {
        List<int> firstRingPolygon = new List<int> { 11, 25, 39 };
        List<string> firstRingTracks = new List<string> { "Nylon_E3", null, null };
        List<float> firstRingDuration = new List<float> { 0.180f, -1, -1 };
        List<float> firstRingVolume = new List<float> { 0.5f, -1, -1 };

        List<int> secondRing = new List<int> { 8, 29 };
        List<string> secondRingTracks = new List<string> { "Piano_CH5", null, null };
        List<float> secondRingDuration = new List<float> { 1.6f, -1, -1 };
        List<float> secondRingVolume = new List<float> { 0.5f, -1, -1 };

        List<int> thirdRing = new List<int> { 0, 6, 12, 18, 24, 30, 36 };
        List<string> thirdRingTracks = new List<string> { "Nylon_A1", null, null };
        List<float> thirdRingDuration = new List<float> { 0.180f, -1, -1 };
        List<float> thirdRingVolume = new List<float> { 0.5f, -1, -1 };

        List<int> fourthRing = new List<int> { 8, 29 };
        List<string> fourthRingTracks = new List<string> { "Piano_GH4", null, null };
        List<float> fourthRingDuration = new List<float> { 1.6f, -1, -1 };
        List<float> fourthRingVolume = new List<float> { 0.5f, -1, -1 };

        List<int> fifthRing = new List<int> { 3, 17, 31 };
        List<string> fifthRingTracks = new List<string> { "Nylon_GH3", null, null };
        List<float> fifthRingDuration = new List<float> { 0.180f, -1, -1 };
        List<float> fifthRingVolume = new List<float> { 0.5f, -1, -1 };

        List<int> sixthRing = new List<int> { 8, 29 };
        List<string> sixthRingTracks = new List<string> { "Piano_E5", null, null };
        List<float> sixthRingDuration = new List<float> { 1.6f, -1, -1 };
        List<float> sixthRingVolume = new List<float> { 0.5f, -1, -1 };

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

                circlesRing.circles[currentPolygonPositionClockWise].SetSpecial();
                circlesRing.circles[currentPolygonPositionClockWise].SetColor(Globals.ColorList[i]);
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack1 = currentRingTracks[0];
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack2 = currentRingTracks[1];
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack3 = currentRingTracks[2];

                circlesRing.circles[currentPolygonPositionClockWise].audioTrack1Duration = currentRingDuration[0];
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack2Duration = currentRingDuration[1];
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack3Duration = currentRingDuration[2];

                circlesRing.circles[currentPolygonPositionClockWise].audioTrack1Volume = currentRingVolume[0];
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack2Volume = currentRingVolume[1];
                circlesRing.circles[currentPolygonPositionClockWise].audioTrack3Volume = currentRingVolume[2];
            }
        }

        circlesRingList[0].Activate();
    }

    public void PlayTrack(string track, float volume, float durationSeconds)
    {
        if (track == null)
        {
            return;
        }

        AudioSource selectedAudioSource = soundDictionary[track].audioSource;

        if (selectedAudioSource == null)
        {
            Debug.LogWarning($"Audio source for track '{track}' not found.");
            return;
        }

        selectedAudioSource.volume = volume * volumeIntensity; // Set the volume based on the intensity

        Debug.Log($"Playing track: {track} with volume: {selectedAudioSource.volume} for duration: {durationSeconds} seconds");

        selectedAudioSource.Play();

        // Create a coroutine to stop the audio after the specified duration
        GlobalHandler.Instance.StartCoroutine(StopAudioAfterDuration(selectedAudioSource, volume * volumeIntensity, durationSeconds));
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
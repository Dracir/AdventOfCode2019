using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
	public AudioClips blip;

	private ObjectPool<PooledAudioGameObject> _audioPool;
	private List<PooledAudioGameObject> _audioActive = new List<PooledAudioGameObject>();

	protected override void Awake()
	{
		base.Awake();
		_audioPool = new ObjectPool<PooledAudioGameObject>(20, MakeNewAudioGameObject);
	}

	private PooledAudioGameObject MakeNewAudioGameObject()
	{
		GameObject newGo = new GameObject("Audio");
		newGo.transform.parent = this.transform;
		var source = newGo.AddComponent<AudioSource>();

		return new PooledAudioGameObject
		{
			AudioGameObject = newGo,
			AudioSource = source
		};
	}

	public void Play(AudioClip clip, Vector3 position, float volume = 1f, float pitch = 1f, float delay = 0f)
	{
		if (clip == null)
		{
			Debug.LogWarning("Tu veux jouer un son null :/");
			return;
		}

		var pooledAudio = _audioPool.Pop();

		var gameObject = pooledAudio.AudioGameObject;

		var source = pooledAudio.AudioSource;
		source.playOnAwake = false;
		source.loop = false;
		source.pitch = pitch;
		source.clip = clip;
		source.volume = volume;

		if (delay > 0)
			source.PlayDelayed(delay);
		else
			source.Play();

		var unpooler = gameObject.AddComponent<UnPoolAudioOnDone>();
		unpooler.Source = source;
	}

	[System.Serializable]
	protected struct PooledAudioGameObject
	{
		public GameObject AudioGameObject;
		public AudioSource AudioSource;
	}

}

public class AudioLoop
{
	public AudioClip Clip;
}


[System.Serializable]
public class AudioClips
{
	public AudioClip[] Clips;
	public Vector2 RandomPitchRange = new Vector2(0.9f, 1.1f);
	private AudioClip _LastPlayed;


	public enum PlayMode { Random, Sequence };
	public PlayMode Mode;
	public bool DontRepeatSound = true;

	private int _sequenceIndex;

	public void Play(float delay = 0f) => Play(Vector3.zero, 1, UnityEngine.Random.Range(RandomPitchRange.x, RandomPitchRange.y), delay);
	public void Play(float volume = 1f, float pitch = 1f, float delay = 0f) => Play(Vector3.zero, volume, pitch, delay);

	public void Play(Vector3 position, float volume = 1f, float pitch = 1f, float delay = 0f)
	{
		if (Clips.Length == 0)
			return;

		AudioClip clip = null;
		switch (Mode)
		{
			case PlayMode.Random:
				if (Clips.Length == 1)
					clip = Clips[0];
				else if (_LastPlayed == null)
					clip = Clips[UnityEngine.Random.Range(0, Clips.Length)];
				else
				{
					var sfx = _LastPlayed;
					while (sfx.name == _LastPlayed.name)
					{
						sfx = Clips[UnityEngine.Random.Range(0, Clips.Length)];
					}
					clip = sfx;
				}
				break;
			case PlayMode.Sequence:
				_sequenceIndex++;
				if (_sequenceIndex >= Clips.Length) _sequenceIndex = 0;
				clip = Clips[_sequenceIndex];
				break;
		}
		_LastPlayed = clip;
		AudioManager.Instance.Play(clip, position, volume, pitch, delay);

	}
}

public class UnPoolAudioOnDone : MonoBehaviour
{
	bool _audioStarted;
	public AudioSource Source;

	void Update()
	{
		if (!_audioStarted && Source.isPlaying)
			_audioStarted = true;

		if (_audioStarted && !Source.isPlaying)
			Destroy(gameObject);
	}
}

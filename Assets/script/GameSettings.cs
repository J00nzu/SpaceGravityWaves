using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;


public static class GameSettings
{

	public static float DEFAULT_MUSIC_VOL = 0.4f;
	public static float DEFAULT_SFX_VOL = 0.8f;

	private static readonly string filePath = Application.persistentDataPath + "/settings.dat";
	private static SettingsObject settingsInstance;
	private static bool changed = false;
	private static bool loaded = false;

	public static void Save(){
		if (!changed) {
			return;
		}

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(filePath);

		bf.Serialize(file, settingsInstance);
		file.Close();

		Debug.Log ("Settings saved to : " + filePath);

		changed = false;
	}

	public static void Load(){
		Debug.Log("Loading settings..");
		if (File.Exists (filePath)) {
			try{
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(filePath, FileMode.Open);

				try{
					settingsInstance = (SettingsObject)bf.Deserialize(file);
					Debug.Log ("Settings loaded from : " + filePath);
					Debug.Log(settingsInstance);

					Analytics.CustomEvent ("settingsLoad", new Dictionary<string, object> {
						{"musicVol", settingsInstance.music},
						{"sfxVol", settingsInstance.sfx},
						{"yearCounter", settingsInstance.yearEnabled}
					});
				}catch(Exception ex){
					Debug.LogWarning(ex);
				}

				file.Close ();
			}catch(Exception ex){
				Debug.LogWarning (ex);
				CreateDefaults ();
				Save ();
			}finally{
			}

		} else {
			Debug.LogWarning ("No settings file found : " + filePath);
		}
		loaded = true;

	}

	private static SettingsObject CreateDefaults(){
		settingsInstance = new SettingsObject ();
		settingsInstance.music = DEFAULT_MUSIC_VOL;
		settingsInstance.sfx = DEFAULT_SFX_VOL;
		settingsInstance.progress = 0;
		settingsInstance.yearEnabled = true;
		settingsInstance.lastLevelBuildIndex = 0;

		return settingsInstance;
	}

	public static SettingsObject Get(){
		if (!loaded) {
			Load ();
		}
		if (hasInstance ()) {
			return settingsInstance;
		} else {
			return CreateDefaults ();
		}
			
	}

	public static void NotifyChange(){
		changed = true;
	}

	private static bool hasInstance(){
		return (settingsInstance != null);
	}
}

[Serializable]
public class SettingsObject{
	private float _music;
	public float music{
		get{
			return  _music;
		}
		set{
			_music = value;
			GameSettings.NotifyChange ();
		}
	}

	private float _sfx;
	public float sfx{
		get{
			return  _sfx;
		}
		set{
			_sfx = value;
			GameSettings.NotifyChange ();
		}
	}

	private int _progress;
	public int progress{
		get{
			return  _progress;
		}
		set{
			_progress = value;
			GameSettings.NotifyChange ();
		}
	}

	private int _deadPoints;
	public int deadPoints{
		get{
			return  _deadPoints;
		}
		set{
			_deadPoints = value;
			GameSettings.NotifyChange ();
		}
	}

	private int _earthPoints;
	public int earthPoints{
		get{
			return  _earthPoints;
		}
		set{
			_earthPoints = value;
			GameSettings.NotifyChange ();
		}
	}

	private bool _yearEnabled;
	public bool yearEnabled{
		get{
			return  _yearEnabled;
		}
		set{
			_yearEnabled = value;
			GameSettings.NotifyChange ();
		}
	}

	private int _lastLevel;
	public int lastLevelBuildIndex{
		get{
			return  _lastLevel;
		}
		set{
			_lastLevel = value;
			GameSettings.NotifyChange ();
		}
	}

	public override string ToString ()
	{
		return string.Format ("[SettingsObject: music={0}, sfx={1}, progress={2}, deadPoints={3}, earthPoints={4}, yearEnabled={5}, lastLevel={6}]", music, sfx, progress, deadPoints, earthPoints, yearEnabled, lastLevelBuildIndex);
	}
	
	
	
}



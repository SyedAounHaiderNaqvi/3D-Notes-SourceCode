using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadManager
{
    public static void SaveNoteData(DragMessages data, string nameOfNote, string folderChoice, string projectName)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream($"{Application.persistentDataPath}/{projectName}/{folderChoice}/{nameOfNote}.data", FileMode.Create);

		NoteData theData = new NoteData(data);

		formatter.Serialize(stream, theData);
		stream.Close();
	}

	public static NoteData LoadNoteData(string nameOfNote)
	{
		if (File.Exists(nameOfNote))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(nameOfNote, FileMode.Open);

			NoteData data = formatter.Deserialize(stream) as NoteData;
			stream.Close();
			return data;

		} else
		{
			Debug.LogWarning("Note data of " + nameOfNote + " not found.");
			return null;
		}
	}
}

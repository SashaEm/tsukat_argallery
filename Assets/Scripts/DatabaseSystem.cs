using System.Collections.Generic;
using Firebase.Storage;
using FullSerializer;
using Proyecto26;
using UnityEngine;

public static class DatabaseSystem
{
    private static readonly string databaseURL = "https://tsukatartgalery-default-rtdb.firebaseio.com/";

    private static fsSerializer serializer = new fsSerializer();

    public delegate void PostUserCallback();
    public delegate void GetUserCallback(ImageDescSerializable user);
    public delegate void GetUsersCallback(Dictionary<string, ImageDescSerializable> users);


    /// <summary>
    /// Adds a user to the Firebase Database
    /// </summary>
    /// <param name="user"> Image object that will be uploaded </param>
    /// <param name="userId"> Id of the image that will be uploaded </param>
    /// <param name="callback"> What to do after the image is uploaded successfully </param>
    public static void PostImageDesc(ImageDescSerializable user, string userId, PostUserCallback callback)
    {
        RestClient.Put<ImageDescSerializable>($"{databaseURL}images/{userId}.json", user).Then(response => { callback(); });
    }

    
    /// <summary>
    /// Gets all images from the Firebase Database
    /// </summary>
    /// <param name="callback"> What to do after all images are downloaded successfully </param>
    public static void GetImageDescsFromDB(GetUsersCallback callback)
    {
        Debug.Log("Called GetImageDescFromDb");
        RestClient.Get($"{databaseURL}images.json").Then(response =>
        {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(List<ImageDescSerializable>), ref deserialized);

            var users = deserialized as List<ImageDescSerializable>;
            var userDictionary = new Dictionary<string, ImageDescSerializable>();
            for(int i = 0; i < users.Count; i++)
            {
                userDictionary.Add(i.ToString(), users[i]);
            }
            callback(userDictionary);
        });
    }

    public static void SaveImage(string filename)
    {
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        // Points to the root reference
        StorageReference storage_ref =
          storage.GetReferenceFromUrl("gs://tsukatartgalery.appspot.com/");

        // Points to "images"
        StorageReference images_ref = storage_ref.Child("Images");

        // Note that you can use variables to create child values
        StorageReference space_ref = images_ref.Child(filename);

        // File name is "space.jpeg"
        string name = space_ref.Name;

        string local_url = LocalDataSystem.IMAGE_FOLDER + name;

        space_ref.GetFileAsync(local_url);
    }
}
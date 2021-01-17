using Firebase.Storage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadImages : MonoBehaviour
{
    private void Awake()
    {
        DownloadImage();
    }

    public static void DownloadImage()
    {
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        // Points to the root reference
        StorageReference storage_ref =
          storage.GetReferenceFromUrl("gs://tsukatartgalery.appspot.com/");

        // Points to "images"
        StorageReference images_ref = storage_ref.Child("Images");

        // Note that you can use variables to create child values
        string filename = "dixit.jpg";
        StorageReference space_ref = images_ref.Child(filename);

        // File path is "images/space.jpg"
        var path = space_ref.Path;

        // File name is "space.jpg"
        string name = space_ref.Name;

        string local_url = $"G:/Github/TSUKAT_TEST/tsukat_test/Assets/Resources/Images/{filename}";

        space_ref.GetFileAsync(local_url).ContinueWith(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Debug.Log("File downloaded.");
            }
            else
            {
                Debug.Log("Error");
            }
        });
    }

    
}

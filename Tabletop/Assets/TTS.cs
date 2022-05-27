using System.Diagnostics;
using TMPro;
using UnityEngine;

public class TTS : MonoBehaviour
{
    private string dtPath = "C:/Users/colli/Desktop/DECtalk/say.exe";
    private TMP_InputField feild;
    // Start is called before the first frame update
    void Start()
    {
        feild = GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Speak(string input)
    {
        if(feild.text != null) input = feild.text;
        print("SAYING " + input);
        var info = new ProcessStartInfo();
        info.FileName = "C:/Users/colli/Desktop/DECtalk/say.exe";
        info.WorkingDirectory = "C:/Users/colli/Desktop/DECtalk";
        info.Arguments = input;
        info.CreateNoWindow = true;
        info.UseShellExecute = false;
        var process = Process.Start(info);
        //process.WaitForExit();
        process.Close();
    }
}

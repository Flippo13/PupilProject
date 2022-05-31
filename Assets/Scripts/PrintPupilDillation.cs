using System.Collections;
using System.Collections.Generic;
using Viveport;
using UnityEngine;
using System.IO;

public class PrintPupilDillation : MonoBehaviour
{
    string filename = "";
    public ImageSlideshow imageSlideShow; 
    private ViveSR.anipal.Eye.SingleEyeData singleEyeData;

    private float pupilDiameterLeft;
    private float pupilDiameterRight; 

    private string imageName; 

    public float timer = 0; 
    private float timeLeft; 

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timer; 
        pupilDiameterLeft = singleEyeData.pupil_diameter_mm;

        filename = Application.dataPath + "/pupilDiameter.csv";

        TextWriter tw = new StreamWriter(filename,false); 
        tw.WriteLine("Pupil diameter (mm), Image, Time"); 
        tw.Close();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime; 
//        Debug.Log(timeLeft);
        if ((timeLeft / timer) < 0){
            WriteToCSV();
            timeLeft = timer;
        }
    }

    public void NewImage(string currentImageName){
        imageName = currentImageName;
        Debug.Log("new Image: " + imageName);
    }

    public void WriteToCSV(){
        TextWriter tw = new StreamWriter(filename,true); 
        tw.WriteLine(pupilDiameterLeft.ToString() + "/," + imageName + "/," + Time.timeSinceLevelLoad); 
        tw.Close();
    }
}

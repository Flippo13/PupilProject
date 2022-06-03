using System.Collections;
using System.Collections.Generic;
using Viveport;
using UnityEngine;
using ViveSR.anipal.Eye;
using System.IO;

public class PrintPupilDillation : MonoBehaviour
{   
    public string filename = "";
    public ImageSlideshow imageSlideShow; 

    private float pupilDiameterLeft;
    private float pupilDiameterRight; 
    VerboseData verboseData; 
    private string imageName; 

    public float timer = 0; 
    private float timeLeft; 

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timer; 
        
        if (filename == "")
            filename = Application.dataPath + "/pupilDiameter.csv";
        else
            filename = Application.dataPath + "/"+filename+".csv";
        TextWriter tw = new StreamWriter(filename,false); 
        tw.WriteLine("Pupil diameter Left (mm), Pupil diameter Right (mm), Image, Time"); 
        tw.Close();
    }

    // Update is called once per frame
    void Update()
    {
        SRanipal_Eye.GetVerboseData(out verboseData); 

        pupilDiameterLeft = verboseData.left.pupil_diameter_mm;
        pupilDiameterRight = verboseData.right.pupil_diameter_mm;

        timeLeft -= Time.deltaTime; 
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
        tw.WriteLine(pupilDiameterLeft + ","  + pupilDiameterRight + "," + imageName + "," + Time.timeSinceLevelLoad); 
        tw.Close();
    }
}

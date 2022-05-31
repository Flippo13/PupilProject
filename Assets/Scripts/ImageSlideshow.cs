using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum SlideShowSate{
    FadeOut, 
    FadeIn,
    NextImage, 
    ShowNeutral, 
    ShowImage
}


[System.Serializable]
public class ShownImageEvent : UnityEvent<string>
{

}

public class ImageSlideshow : MonoBehaviour
{
    public Sprite[] imageArray;
    public float showImageDuration;
    public float fadeOutInDuration;
    public bool manual;

    private Image currImage; 
    private Sprite currSprite; 
    private float fadeTimeLeft;
    private float fadeSpeed; 
    [SerializeField]
    private float showImageTimeLeft;

    private float imageAlpha = 1;
    private bool fadingOut = false; 
    private bool fadingIn = false; 
    private int index = -1; 
    private int neutralIndex = 0;
    private bool showNeutralImage = true;

    private Sprite neutralImage;
    private SlideShowSate currState = SlideShowSate.ShowImage;

    public ShownImageEvent imageChangeEvent;

    public PrintPupilDillation printPupilDillation;

    // Start is called before the first frame update
    async void Start()
    {
        fadeTimeLeft = fadeOutInDuration; 
        showImageTimeLeft = showImageDuration;
        fadeSpeed = 1/fadeOutInDuration; 
        currImage = GetComponent<Image>();
        SetNewNeutralImage(); 

        currImage.sprite = neutralImage;

        imageChangeEvent.AddListener(printPupilDillation.NewImage);
    }

    // Update is called once per frame
     void Update()
    {
        if (!manual) {
            
            if (currState == SlideShowSate.ShowImage) {
                ShowImage();
            }

            else if (currState == SlideShowSate.NextImage) {
                NextImage();
            }

            else if (currState == SlideShowSate.FadeOut) {
                FadeOutIn(true);
            }

            else if (currState == SlideShowSate.FadeIn) {
                FadeOutIn(false);
            }


            /*
            showImageTimeLeft -= Time.deltaTime; 
                if (showImageTimeLeft <= 0){
                    fadingOut = true; 
                }
            if (fadingOut) {
                if(FadeOutIn(true))
                    NextImage();
            }
            if (fadingIn) {
                if(FadeOutIn(false)) {
                    Debug.Log("Hey");
                    showImageTimeLeft = showImageDuration;
                    fadingIn = false;
                }
            } */
        } 
    }

    public void NextImage() {
        //Debug.Log("Current Index " + index);
      if (showNeutralImage) {
                if (imageArray[index].name == "Neutral"+neutralIndex){
                    SetNewNeutralImage();
                }
                currImage.sprite = neutralImage;
                showImageTimeLeft = showImageDuration;
                imageChangeEvent.Invoke(currImage.sprite.name);
                currState = SlideShowSate.FadeIn;
            }
            else {
                index++; 
                showImageTimeLeft = showImageDuration;
                if (index < imageArray.Length-1) {
                        currImage.sprite = imageArray[index]; 
                        imageChangeEvent.Invoke(currImage.sprite.name);
                        currState = SlideShowSate.FadeIn;
                }
            }
    }

    public void ShowImage(){
        showImageTimeLeft -= Time.deltaTime; 
            if (showImageTimeLeft <= 0){
                currState = SlideShowSate.FadeOut;
            }
    }


    private void SetNewNeutralImage(){
        Debug.Log("New Neutral image");
        neutralIndex++;
        for(int i = 0; i < imageArray.Length;i++){
            if (imageArray[i].name == "Neutral"+neutralIndex)
            {
                neutralImage = imageArray[i];
            }
        }
    }
 /*
    public void NextImage(){
        index++; 
        if (index < imageArray.Length-1) {
            currImage.sprite = imageArray[index]; 
            imageAlpha = 0;
            fadingIn = true;
            fadingOut = false;
        }
    }
*/
    public bool FadeOutIn(bool fadeOut){
        if (fadeOut) {
            imageAlpha -= fadeSpeed * Time.deltaTime;
            currImage.color = new Color(1,1,1,imageAlpha);
            
            if (imageAlpha <= 0) {
                showNeutralImage = !showNeutralImage;
                currState = SlideShowSate.NextImage;
                return true; 
            }
            else 
                return false; 
        } else {
            imageAlpha += fadeSpeed * Time.deltaTime;
            currImage.color = new Color(1,1,1,imageAlpha);
            
            if (imageAlpha >= 1) {
                currState = SlideShowSate.ShowImage;
                return true; 
            }
            else 
                return false; 
        }
    }
}

using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class LifeViewer : MonoBehaviour
{
    private Image image;
    private Canvas canvas;
    private List<Image> images = new();

    private Vector2 firtImagePos;
    private Vector2 imageOffset;

    public void Initialise(Image image, int initialLife, Vector2 firstImagePos, Vector2 imageOffset)
    {
        this.image = image;
        this.firtImagePos = firstImagePos;
        this.imageOffset = imageOffset;
        canvas = GetComponent<Canvas>();
        UpdateImages(initialLife);

    }


    public void UpdateImages(int life)
    {
        Debug.Log(images.Count);
        if (images.Count < life)
        {
            for (int index = 0; index < images.Count; index++)
            {
                images[index].gameObject.SetActive(true);
            }
            for (int index = images.Count; index < life; index++)
            {
                Image newImage = Instantiate(image, canvas.transform); ;
                images.Add(newImage);
                Vector2 position = firtImagePos + index * imageOffset;
                newImage.rectTransform.anchoredPosition = position;

            }
        }
    }
}

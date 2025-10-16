using UnityEngine;

public class RandomFace : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer shownFace;
    [SerializeField]
    Sprite[] possibleFaces;
    void Awake()
    {
        int faceIndex = Random.Range(0, possibleFaces.Length);
        shownFace.sprite = possibleFaces[faceIndex];
    }

}

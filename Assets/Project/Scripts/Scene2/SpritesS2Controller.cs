using UnityEngine;

public class SpritesS2Controller : MonoBehaviour
{
    [SerializeField] private Sprite greenGroundSprite, happyAntaSprite,
        happyCapibaraSprite, happyFrogSprite;
    [SerializeField] private SpriteRenderer groundRenderer, antaRenderer,
        capibaraRenderer, frogRenderer;
    [SerializeField] private GameObject dryBush1Object, dryBush2Object,
        greenBush1Object, greenBush2Object, treesRootsObject;

    public void showTreesRoots()
    {
        treesRootsObject.SetActive(true);
    }

    public void turnSceneGreenAndAnimalsHappy()
    {
        groundRenderer.sprite = greenGroundSprite;

        Destroy(dryBush1Object);
        Destroy(dryBush2Object);

        greenBush1Object.SetActive(true);
        greenBush2Object.SetActive(true);

        antaRenderer.sprite = happyAntaSprite;
        capibaraRenderer.sprite = happyCapibaraSprite;
        frogRenderer.sprite = happyFrogSprite;
    }
}

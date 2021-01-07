using System.Linq;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public static BackgroundController instance;

    [SerializeField]
    private BackgroundHolder[] backgrounds;

    [SerializeField]
    private int currentBG;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        PrepareBackgrounds();
    }
    private void PrepareBackgrounds()
    {
        if (!GameController.instance.IsNightBGUnlocked())
        {
            backgrounds[(int) Backgrounds.Night] = null;
        }
        if (!GameController.instance.IsCityBGUnlocked())
        {
            backgrounds[(int)Backgrounds.City] = null;
        }
        if (!GameController.instance.IsGymBGUnlocked())
        {
            backgrounds[(int)Backgrounds.Gym] = null;
        }
    }

    public int CycleBG()
    {
        var possibilities = backgrounds.Where(bg => bg != null);
        if (possibilities.Count() > 0)
        {
            currentBG++;
            if (currentBG < backgrounds.Length)
            {
                if (backgrounds[currentBG] != null)
                {
                    SelectBackground(currentBG);
                }
                else
                {
                    currentBG = CycleBG();
                }
            }
            else
            {
                currentBG = 0;
                SelectBackground(currentBG);
            }
        }
        return currentBG;
    }

    public void SelectBackground(int selectedBG)
    {
        //TODO: Refactor. If selecting background from elsewhere make sure currentBG is updated.
        if (currentBG != selectedBG)
        {
            currentBG = selectedBG;
        }
        if (selectedBG < backgrounds.Length && backgrounds[selectedBG] != null)
        {
            var bgRenderers = Helper.FindComponentsInChildrenWithTag<SpriteRenderer>(gameObject, "Background");
            foreach (var render in bgRenderers)
            {
                render.sprite = backgrounds[selectedBG].bg;
            }

            var groundRenderers = Helper.FindComponentsInChildrenWithTag<SpriteRenderer>(gameObject, "Ground");
            foreach (var render in groundRenderers)
            {
                render.sprite = backgrounds[selectedBG].ground;
            }

            var obstacleHolders = Helper.FindComponentsInChildrenWithTag<Transform>(gameObject, "PipeHolder");
            foreach (var holder in obstacleHolders)
            {
                var pipeRenderers = Helper.FindComponentsInChildrenWithTag<SpriteRenderer>(gameObject, "Pipe");
                foreach (var render in pipeRenderers)
                {
                    render.sprite = backgrounds[selectedBG].obstacle;
                }

                var decoControllers = Helper.FindComponentsInChildrenWithTag<DecorationController>(gameObject, "Decoration");
                foreach (var deco in decoControllers)
                {
                    deco.UpdateDecorations(backgrounds[selectedBG].decorations);
                }

            }
        }
    }

    public void ExternalChooseBackground(int selectedBG)
    {
        if (selectedBG == backgrounds.Length - 1)
        {
            PrepareBackgrounds();
            var availableBackgrounds = backgrounds.Where(bg => bg != null).Select((bg,index)=> index);
            var randChoice = Random.Range(0, availableBackgrounds.Count() - 1);
            selectedBG = availableBackgrounds.ElementAt(randChoice);
        }
        SelectBackground(selectedBG);
    }

    public void GenerateParticle(Vector3 location, bool big = false)
    {
        if (backgrounds[currentBG]?.particle != null)
        {
            if (big)
            {
                Instantiate(backgrounds[currentBG].bigParticle ?? backgrounds[currentBG].particle, location, Quaternion.identity);
            }
            else
            {
                Instantiate(backgrounds[currentBG].particle, location, Quaternion.identity);
            }
        }
    }
}

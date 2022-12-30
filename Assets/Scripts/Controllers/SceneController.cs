using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject infoObj;
    [SerializeField] private bool infoVisibleAtStart = false;

    [Header("Buttons")]
    [SerializeField] private Button infoBtn;

    private PanelAnimator infoObjAnimator;
    
    // Start is called before the first frame update
    private void Start()
    {
        if (infoObj)
        {
            infoObj.TryGetComponent(out infoObjAnimator);
            ChangeInfoObjState(!infoVisibleAtStart);
        }
        if(infoBtn) infoBtn.onClick.AddListener(ShowInfoButton);
    }

    public void CloseInfoPanel()
    {
        AnimateInfoObj(false);
    }

    private void ShowInfoButton()
    {
        ChangeInfoObjState(infoObj.activeSelf);
    }

    private void ChangeInfoObjState(bool objState)
    {
        if (infoObjAnimator == null) return;
        if (objState)
        {
            if (infoObjAnimator.ActiveState is PanelState.AnimatingDown)
            {
                AnimateInfoObj(true);
            }
            else AnimateInfoObj(false);
            return;
        }
        
        infoObj.SetActive(true);
        AnimateInfoObj(true);
    }

    private void AnimateInfoObj(bool isActive)
    {
        if (isActive) infoObjAnimator.Appear();
        else infoObjAnimator.Disappear();
    }
}

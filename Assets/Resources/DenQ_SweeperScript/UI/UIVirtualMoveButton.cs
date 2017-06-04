using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DenQ.BaseStruct;
public class UIVirtualMoveButton : MonoBehaviour
{

    public float plateMaxAlpha;
    public float plateMinAlpha;
    public float plateRecAlpha;
    public float plateAlphaChangeSpeed;
    private enum ALPHA_STATS
    {
        min = 0,
        max,
        increase,
        decrease,
    };
    ALPHA_STATS alphaState = ALPHA_STATS.min;
    [SerializeField] public UIButtonStatsController buttonStateCtrl = null;
    [SerializeField] public RectTransform canvasRectT;
    [SerializeField] public RectTransform panelRectT;
    [SerializeField] public RectTransform buttonRectT;
    [SerializeField] public Image backGroundImage;
	public GameObject cameraTargetObj = null;

    public Vector2 panelPosition;
    public Vector2 panelMousePosition;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if(cameraTargetObj == null)
		{
			cameraTargetObj = GameObject.FindWithTag("CameraTarget");
		}
        panelPosition = panelRectT.anchoredPosition;

        if (buttonStateCtrl.GetButtonPress())
        {
            panelMousePosition = DenQUIhelper.GetPanelPosition(canvasRectT, panelRectT);
            alphaState = ALPHA_STATS.increase;
            float range = panelMousePosition.magnitude;
            if (range > panelRectT.rect.width / 2.0f)
            {
				//ここで、パーネルサイズで計算しているが、背景画像でも良い
                Vector2 newPos = new Vector2(panelRectT.rect.width / 2.0f * panelMousePosition.x / range,
                                            panelRectT.rect.height / 2.0f * panelMousePosition.y / range);
                buttonRectT.anchoredPosition = newPos;
            }
            else
            {
                buttonRectT.anchoredPosition = panelMousePosition;
            }
        }
        else
        {
            alphaState = ALPHA_STATS.decrease;
            buttonRectT.anchoredPosition = backGroundImage.rectTransform.anchoredPosition;
        }
        switch (alphaState)
        {
            case ALPHA_STATS.increase:
                if (plateRecAlpha < plateMaxAlpha)
                {
                    plateRecAlpha += plateAlphaChangeSpeed;
                }
                else
                {
                    plateRecAlpha = plateMaxAlpha;
                    alphaState = ALPHA_STATS.max;
                }
                break;
            case ALPHA_STATS.decrease:
                if (plateRecAlpha > plateMinAlpha)
                {
                    plateRecAlpha -= plateAlphaChangeSpeed;
                }
                else
                {
                    plateRecAlpha = plateMinAlpha;
                    alphaState = ALPHA_STATS.min;
                }
                break;
            case ALPHA_STATS.min:
            case ALPHA_STATS.max:
                break;
        }
        Color tempColor = backGroundImage.color;
        tempColor.a = plateRecAlpha;
        backGroundImage.color = tempColor;
		UpdateCemeraTargetPosition();
    }
    public void FadeIn()
    {
        alphaState = ALPHA_STATS.increase;
    }
    public void FadeOut()
    {
        alphaState = ALPHA_STATS.decrease;
    }
	void UpdateCemeraTargetPosition()
	{
		if(alphaState != ALPHA_STATS.decrease && alphaState != ALPHA_STATS.min)
		{
			Vector2 direction = buttonRectT.anchoredPosition / panelRectT.rect.width / 2.0f;
			cameraTargetObj.transform.position = new Vector3(cameraTargetObj.transform.position.x + direction.x,
															cameraTargetObj.transform.position.y,
															cameraTargetObj.transform.position.z + direction.y);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SingleQuestRewardDisplayComponent : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _rewardName;
    [SerializeField]
    private new TextMeshProUGUI _rewardCount;
    [SerializeField]
    private Image _rewardIcon;
    [SerializeField]
    private List<Sprite> _rewardSprites= new List<Sprite>();

    public void SetRectTransform(float _xValue, float _yValue, float _zValue) => gameObject.GetComponent<RectTransform>().localPosition = new Vector3(_xValue, _yValue-150.0f, _zValue);
    public float GetPrevRewardPos() => gameObject.GetComponent<RectTransform>().localPosition.x;

    public void DisplayReward(QuestSingleRewardHandler.Reward _rewardType, int _rewardCount )
    {
        this._rewardName.text= _rewardType.ToString();
        this._rewardCount.text= _rewardCount.ToString();
        switch(_rewardType)
        {
            case QuestSingleRewardHandler.Reward.Sigil:
                this._rewardIcon.sprite = _rewardSprites[0];
                break;
            case QuestSingleRewardHandler.Reward.WeaponDagger:

                this._rewardIcon.sprite = _rewardSprites[1];
                break;
            case QuestSingleRewardHandler.Reward.WeaponAxe:

                this._rewardIcon.sprite = _rewardSprites[2];
                break;
            case QuestSingleRewardHandler.Reward.WeaponSword:

                this._rewardIcon.sprite = _rewardSprites[3];
                break;
            case QuestSingleRewardHandler.Reward.Exploration:

                this._rewardIcon.sprite = _rewardSprites[4];
                break;
            case QuestSingleRewardHandler.Reward.Thales:

                this._rewardIcon.sprite = _rewardSprites[5];
                break;
        }
    }

}

using UnityEngine;
using TMPro;
using Zenject;
using DG.Tweening;

public class FinalPanel : IFinalPanel
{
    private TMP_Text _text;
    private RectTransform _popup;

    [Inject]
    private void Construct(TMP_Text text, RectTransform popup)
    {
        _text = text;
        _popup = popup;
    }

    public void ActivateFinalPanel(string text)
    {
        TextOnFinishPanel(text);
        _popup.gameObject.SetActive(true);
        _popup.DOLocalMove(Vector3.zero, 2);
    }

    private void TextOnFinishPanel(string text)
    {
        _text.text = text;
    }
}

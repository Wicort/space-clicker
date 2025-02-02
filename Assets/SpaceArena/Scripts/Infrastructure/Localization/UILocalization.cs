using Assets.SpaceArena.Scripts.Infrastructure.Localization;
using Services;
using UnityEngine;
using UnityEngine.UI;

public class UILocalization : MonoBehaviour
{
    [SerializeField] private string _key;
    [SerializeField] private Text _uiElement; 


    private ILocalizationService _localizationService;

    private void Awake()
    {
        _localizationService = AllServices.Container.Single<ILocalizationService>();
        _uiElement = GetComponent<Text>();

        if (_key == null || _key == "") _key = _uiElement.text;
    }

    private void Start()
    {
        _uiElement.text = _localizationService.GetUIByKey(_key);
    }

    private void OnValidate()
    {
        if (_uiElement == null) _uiElement = GetComponent<Text>();

        if (_key == null || _key == "") _key = _uiElement.text;
    }
}

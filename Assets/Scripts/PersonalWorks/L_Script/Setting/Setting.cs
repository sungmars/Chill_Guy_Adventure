using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Setting : MonoBehaviour, IPointerUpHandler
{
    public Slider bgmSlider;
    public Slider sfxSlider;
    private const string BGMVolumeKey = "BGMVolume";
    private const string SFXVolumeKey = "SFXVolume";
    public AudioClip sfx;

    void Start()
    {
        //저장된 볼륨값을 불러오고 데이터 없으면 0.5가 기본값
        float bgmVolume = PlayerPrefs.GetFloat(BGMVolumeKey, 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 0.5f);
        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;
        bgmSlider.onValueChanged.AddListener(UpdateBGMVolume);
        sfxSlider.onValueChanged.AddListener(UpdateSFXVolume);

        // AudioManager에 저장된 볼륨값 설정
        AudioManager.Instance.SetBGMVolume(bgmVolume);
        AudioManager.Instance.SetSFXVolume(sfxVolume);
    }

    public void UpdateBGMVolume(float newVolume)
    {
        AudioManager.Instance.SetBGMVolume(newVolume);
        PlayerPrefs.SetFloat(BGMVolumeKey, newVolume);
        PlayerPrefs.Save();
    }

    public void UpdateSFXVolume(float newVolume)
    {
        Debug.Log("SFX Volume Updated: " + newVolume);
        AudioManager.Instance.SetSFXVolume(newVolume);
        PlayerPrefs.SetFloat(SFXVolumeKey, newVolume);
        PlayerPrefs.Save();
    }
    private void valueChanged()
    {
        AudioManager.Instance.PlayPlayerSound(sfx);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        valueChanged();
    }
    public void HandlePointerUp(BaseEventData data)
    {
        // data를 PointerEventData로 변환한 후 OnPointerUp 호출
        OnPointerUp(data as PointerEventData);
    }
}

using UnityEngine;
public class AudioManager : MonoBehaviour
{
    [Header("-------- Audio Source --------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-------- Audio Clip --------")]
    public AudioClip background;
    public AudioClip pickup;
    public AudioClip equip;
    public AudioClip door;
    public AudioClip cabinetOpen;
    public AudioClip safeOpen;
    public AudioClip lightMatch;
    public AudioClip paperRuffles;
    public AudioClip invOpen;
    public AudioClip invClose;
    public AudioClip inspect;
    public AudioClip placeFrame;
    public AudioClip hat;
    public AudioClip sweeping;
    public AudioClip locker;
    public AudioClip boxOpen;
    public AudioClip fridgeOpen;
    public AudioClip eating;
    public AudioClip poison;
    public AudioClip bite;
    public AudioClip deathSound;
    public AudioClip hey;

    private void Start () {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip) {
        SFXSource.PlayOneShot(clip);
    }
}

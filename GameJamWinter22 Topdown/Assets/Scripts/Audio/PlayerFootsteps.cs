using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] private AudioSource playerFootsteps;
    [SerializeField] private AudioClip[] playerFootstepsClips;


    public void Footsteps()
    {
        playerFootsteps.clip=playerFootstepsClips[Random.Range(0,playerFootstepsClips.Length)];
        playerFootsteps.PlayOneShot(playerFootsteps.clip);
    }
}

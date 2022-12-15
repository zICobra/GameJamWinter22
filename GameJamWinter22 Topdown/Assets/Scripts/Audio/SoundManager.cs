using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
   [Header("Audio")] 
   [SerializeField] private AudioSource witchDyingAudioSource;
   [SerializeField] private AudioSource witchHitByArrowAudioSource;
   [SerializeField] private AudioClip[] witchHitByArrowArray;
   [SerializeField] private AudioSource musicAudioSource;
   [SerializeField] private AudioClip[] musicArray;
   [SerializeField] private AudioSource paladinAttackAudioSource;
   [SerializeField] private AudioClip[] paladinAttackArray;
   [SerializeField] private AudioSource peasantDyingAudioSource;
   [SerializeField] private AudioClip[] peasantDyingArray;
   [SerializeField] private AudioSource peasantGetHitAudioSource;
   [SerializeField] private AudioClip[] peasantGetHitArray;
   [SerializeField] private AudioSource bulletImpactNoArmorAudioSource;
   [SerializeField] private AudioClip[] bulletImpactNoArmorArray;
   [SerializeField] private AudioSource lightningHitAudioSource;
   [SerializeField] private AudioSource knightDyingAudioSource;
   [SerializeField] private AudioClip[] knightDyingArray;
   [SerializeField] private AudioSource knightGetHitAudioSource;
   [SerializeField] private AudioClip[] knightGetHitArray;
   [SerializeField] private AudioSource bulletImpactArmorAudioSource;
   [SerializeField] private AudioClip[] bulletImpactArmorArray;
   [SerializeField] private AudioSource paladinHitAudioSource;
   [SerializeField] private AudioClip[] paladinHitArray;
   

   private void Awake()
   {
      musicAudioSource.clip=musicArray[Random.Range(0,musicArray.Length)];
      musicAudioSource.PlayOneShot(musicAudioSource.clip);
   }

   public void WitchDying()
   {
      witchDyingAudioSource.Play();
   }

   public void WitchHitByArrow()
   {
      witchHitByArrowAudioSource.clip=witchHitByArrowArray[Random.Range(0,witchHitByArrowArray.Length)];
      witchHitByArrowAudioSource.PlayOneShot(witchHitByArrowAudioSource.clip);
   }

   public void PaladinAttack()
   {
      paladinAttackAudioSource.clip=paladinAttackArray[Random.Range(0,paladinAttackArray.Length)];
      paladinAttackAudioSource.PlayOneShot(paladinAttackAudioSource.clip);
   }

   public void PeasantDying()
   {
      peasantDyingAudioSource.clip=peasantDyingArray[Random.Range(0,peasantDyingArray.Length)];
      peasantDyingAudioSource.PlayOneShot(peasantDyingAudioSource.clip);
   }

   public void PeasantGetHit()
   {
      peasantGetHitAudioSource.clip=peasantGetHitArray[Random.Range(0,peasantGetHitArray.Length)];
      peasantGetHitAudioSource.PlayOneShot(peasantGetHitAudioSource.clip);
   }

   public void BulletImpactNoArmor()
   {
      bulletImpactNoArmorAudioSource.clip=bulletImpactNoArmorArray[Random.Range(0,bulletImpactNoArmorArray.Length)];
      bulletImpactNoArmorAudioSource.PlayOneShot(bulletImpactNoArmorAudioSource.clip);
   }
   
   public void LightningHit()
   {
      lightningHitAudioSource.Play();
   }
   
   public void KnightDying()
   {
      knightDyingAudioSource.clip=knightDyingArray[Random.Range(0,knightDyingArray.Length)];
      knightDyingAudioSource.PlayOneShot(knightDyingAudioSource.clip);
   }
   
   public void KnightGetHit()
   {
      knightGetHitAudioSource.clip=knightGetHitArray[Random.Range(0,knightGetHitArray.Length)];
      knightGetHitAudioSource.PlayOneShot(knightGetHitAudioSource.clip);
   } 
   
   public void BulletImpactArmor()
   {
      bulletImpactArmorAudioSource.clip=bulletImpactArmorArray[Random.Range(0,bulletImpactArmorArray.Length)];
      bulletImpactArmorAudioSource.PlayOneShot(bulletImpactArmorAudioSource.clip);
   }
   
   public void PaladinHit()
   {
      paladinHitAudioSource.clip=paladinHitArray[Random.Range(0,paladinHitArray.Length)];
      paladinHitAudioSource.PlayOneShot(paladinHitAudioSource.clip);
   }
}

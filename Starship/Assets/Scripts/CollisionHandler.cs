using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float timeToLoadDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;


    bool isTransitioning =false;
    bool collisionDisable = false;

    void Start(){
        audioSource = GetComponent<AudioSource>(); 
    }

    void Update(){
        DebugKeys();
    }
    void DebugKeys(){
        if(Input.GetKeyDown(KeyCode.L)){
            LoadNextLevel();
        }
        if(Input.GetKeyDown(KeyCode.C)){
            collisionDisable = !collisionDisable;
        }
    }

    private void OnCollisionEnter(Collision other) {
        
        if (isTransitioning || collisionDisable) {return;}
        else {
            switch(other.gameObject.tag){
                case "Friendly":
                    Debug.Log("START PAD");
                    break;
                case "Finish":
                    StartNewLevelSequence();
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
    }

    void StartCrashSequence(){
        isTransitioning = true;
        GetComponent<RocketMovement>().enabled = false;
        
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();

        Invoke("ReloadScene",timeToLoadDelay);
    }

    void StartNewLevelSequence(){

        isTransitioning = true;
        GetComponent<RocketMovement>().enabled = false;

        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel",timeToLoadDelay);
    }


    void ReloadScene(){

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        
    }

    void LoadNextLevel(){

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex +1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        
        SceneManager.LoadScene(nextSceneIndex);
    }
}

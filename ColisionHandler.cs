using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    [SerializeField] AudioClip rocketExplosion;
    [SerializeField] AudioClip levelFinish;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool stopCollisions = false;
    // Are my changes immediately commited?

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        DebugKeys();
    }

    // Start learning to put thins in methods.  It makes it easier to change things later.
    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.C))  // I originally used GetKey.  This does not work if you want to toggle.
        {
            stopCollisions = !stopCollisions; // Toggle Bool
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) {return;}
        if (stopCollisions) {return;}

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You hit a friendly.");
                break;
            case "Finish":
                StartFinishSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }

    }

void StartCrashSequence()
{   
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(rocketExplosion); 
    crashParticles.Play();
    GetComponent<Movements>().enabled = false;
    Invoke("ReloadLevel", delay);
}

void StartFinishSequence()
{
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(levelFinish);
    successParticles.Play();
    GetComponent<Movements>().enabled = false;
    Invoke("LoadNextLevel", delay);
}

void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        // if (nextSceneIndex == 5)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

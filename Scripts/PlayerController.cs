using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // создаем класс Rigidbody для доступа к Rigidbody Player.
    private Rigidbody playerRB;
    // создаем класс Animator для доступа к Animator Player.
    private Animator playerAnim;
    // создаем класс ParticleSystem для того, чтобы использовать эффект взрыва у Player.
    public ParticleSystem explosionParticle;
    // создаем класс ParticleSystem для того, чтобы использовать эффект грязи у Player.
    public ParticleSystem dirtParticle;
    // создаем класс AudioSource для того чтобы проигрывать звуковой эффект.
    private AudioSource playerAudio;
    // создаем класс AudioClip для того чтобы проигрывать звуковой эффект при прыжке.
    public AudioClip jumpSound;
    // создаем класс AudioClip для того чтобы проигрывать звуковой эффект при столковении.
    public AudioClip crashSound;
    // создаем переменную для силы прыжка при нажатии space.
    public float jumpForce = 700f;
    // создаем переменную для изменения гравитации, изменяем значения в инспекторе.
    public float gravityModifier;
    // создаем переменную для проверки соприкосновения с поверхностью.
    public bool isOnGround;
    // создаем переменную для проверки соприкосновения с obstacle (препятствием).
    public bool gameOver;

    // Происходит при старте игры
    void Start()
    {
        // получаем доступ к Rigidbody player'a через компонент GetComponent, иначе нажатие на space не будет работать.
        playerRB = GetComponent<Rigidbody>();
        // получаем доступ к Animator player'a через компонент GetComponent.
        playerAnim = GetComponent<Animator>();
        // получаем доступ к AudioSource player'a через компонент GetComponent.
        playerAudio = GetComponent<AudioSource>();
        // изменяем силу гравитации, gravityModifier например может быть равен 2.
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        // Если нажимаем space и isOnGround == true, и игра не закончена.
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            // заставляем Player подпрыгивать при нажатии на space при помощи enum ForceMode.Impulse.
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // пока Player в воздухе не получится сделать двоиной прыжок.
            isOnGround = false;
            // используем SetTrigger для того чтобы происходила анимация при прыжке (значения берутся из Player Animator).
            playerAnim.SetTrigger("Jump_trig");
            // когда Player в прыжке, эффект грязи отключается.
            dirtParticle.Stop();
            // проигрываем звуковой эффект jumpSound с громкостью 0.7f, один раз, когда Player подпрыгивает.
            playerAudio.PlayOneShot(jumpSound, 0.7f);
        }      
    }

    // создаем OnCollisionEnter для того чтобы определить когда Player соприкасается с землей и препятствиями так как на них есть boxcollider.
    private void OnCollisionEnter(Collision other)
    {
        // если Player соприкасается с Ground (тэг задан у объекта Ground в инспекторе)
        if (other.gameObject.CompareTag("Ground"))
        {
            // когда Player соприкасается с поверхностью прыжок снова доступен.
            isOnGround = true;
            // когда Player на земле, эффект грязи включается.
            dirtParticle.Play();
        }
        // или же Player соприкасается с Obstacle (препятствием), (тэг задан у объекта Obstacle в инспекторе)
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game over");
            // игра заканчивается
            gameOver = true;
            // активируем смерть Player анимацией (значения берутся из Player Animator).
            playerAnim.SetBool("Death_b", true);
            // активируем анимацию смерти под номером 1 (значения берутся из Player Animator).
            playerAnim.SetInteger("DeathType_int", 1);
            // Запускаем ParticleSystem эффект дыма при соприкосновении с Obstacle, когда игрок умирает и игра заканчивается.
            explosionParticle.Play();
            // когда Player соприкасается с obstacle и умирает, эффект грязи отключается.
            dirtParticle.Stop();
            // проигрываем звуковой эффект crashSound с громкостью 0.2f, один раз, когда Player соприкасается с obstacle (препятствием) и умирает.
            playerAudio.PlayOneShot(crashSound, 0.2f);
        }
    }
}

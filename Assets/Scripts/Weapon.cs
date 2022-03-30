using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7 };
    public float[] pushForce = { 2.0f , 2.2f, 2.5f, 3f, 3.2f, 3.6f, 4f};

    // Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;// we do this so we can change the apperance of the sword

    // Swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

    // Awake executes before start and we put sprite renderer here so our game manager have a reference to this
    // Another way was puting the sprite renderer public
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // we do this so we can really have access to the sprite renderer
    }
    protected override void Start() // protected so it only interfires with the lower classes in hierarqy
    {
        base.Start(); // this runs the original code    
        anim = GetComponent<Animator>();
    }
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown)//---------------------whats the default value of lastSwing
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }
    protected override void OnColide(Collider2D coll)
    {
        if(coll.tag == "Fighter")
        {
            if (coll.name == "Player")
                return;

            // Create new damage object, then we will send it to the fighter we hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };
            coll.SendMessage("ReceiveDamage", dmg);      
        }
        
    }
    private void Swing()
    {
        anim.SetTrigger("Swing");
    }
    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

}

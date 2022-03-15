using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    // Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;// we do this so we can change the apperance of the sword

    // Swing
    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start() // protected so it only interfires with the lower classes in hierarqy
    {
        base.Start(); // this runs the original code
        spriteRenderer = GetComponent<SpriteRenderer>(); // we do this so we can really have access to the sprite renderer
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
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };
            coll.SendMessage("ReceiveDamage", dmg);      
        }
        
    }
    private void Swing()
    {
        Debug.Log("Swing");
    }

}//3:26:32

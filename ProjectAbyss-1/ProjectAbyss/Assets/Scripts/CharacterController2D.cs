using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 10000f;							// Quantidade de força adicionada para o personagem pular
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// porcentagem da velocidade normal do personagem, quando agachado
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// Amaciamento do movimento do personagem
	[SerializeField] private bool m_AirControl = false;							// Determina se o personagem pode se mover enquanto pula
	[SerializeField] private LayerMask m_WhatIsGround;                          // Determina o que é considerado "chão"
	[SerializeField] private LayerMask m_WhatIsCeiling;                         // Determina o que é considerado "teto"
	[SerializeField] private Transform m_GroundCheck;							// marca onde o personagem checa o que é chão
	[SerializeField] private Transform m_CeilingCheck;                          // marca onde o personagem checa o que é teto
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // colisor que é desativado enquando ag
	[SerializeField] private UI_Inventory uiInventory;

	const float k_GroundedRadius = .6f; // Raio do circulo que determina se o personagem está no chão
	public bool m_Grounded;            // se o personagem está ou nao no chao
	const float k_CeilingRadius = .2f; // raio determina se o personagem pode se levantar
	public Rigidbody2D m_Rigidbody2D;

	public bool m_FacingRight = true;  // Determina a direção que o personagem está olhando

	

	private Vector3 m_Velocity = Vector3.zero;
	//private bool isHurt = false;
	//private Inventory inventory;
	public GameObject childRenderer;
	public int health = 100;
    private bool immune = false;
	
    


	[Header("Events")]

	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	public bool m_wasCrouching = false;

	private void Awake()
	{
		//inventory = new Inventory();

		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();

	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		//o personagem está no chao se o circulo do groundcheck acerta qualquer coisa designado como chão
		
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
		/*if(isHurt == true)
		{
			Debug.Log("Wounded");
		}*/
	}


	public void Move(float move, bool crouch, bool jump)
	{
		// Se agachado, checa se o personagem pode se levantar
		if (!crouch)
		{
			// se houver teto, o personagem continua agachado
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsCeiling))
			{
				crouch = true;
			}
		}

		//Apenas controla o personagem se o AirColtrol estiver "true"
		if (m_Grounded || m_AirControl)
		{

			
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				
				move *= m_CrouchSpeed;

				// Desabilita o collider quando agachado
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				//  Habilita o collider quando não agachado
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move o personagem achando a velocidade alvo, amacia o movimento, e vira o sprite de acordo com a direção
			Vector3 targetVelocity = new Vector2(move * 30f, m_Rigidbody2D.velocity.y);
			
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			
			if (move > 0 && !m_FacingRight)
			{
				
				Flip();
			}
			
			else if (move < 0 && m_FacingRight)
			{
				
				Flip();
			}
		}
		// Faz o personagem pular adicionando força vertical
		if (jump == true)
		{
			
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			
		}
		
		
	}


	private void Flip()
	{
		//Troca o lado para que o personagem está olhando
		m_FacingRight = !m_FacingRight;
		transform.Rotate(0f, 180f, 0f);
		childRenderer.transform.Rotate(0f, 180f, 0f);
		childRenderer.GetComponent<SpriteRenderer>().flipX = !childRenderer.GetComponent<SpriteRenderer>().flipX;
	}
    private void OnCollisionEnter2D(Collision2D collisioninfo)
    {
		//Danos de contato feitos pelo Boss e perigos genericos, e um item que recupera vida
        if ( collisioninfo.collider.tag == "Danger")
        {
           
              TakeDamage(25);
             
              m_Rigidbody2D.AddForce(new Vector2(50f, 50f));
                
            
		}
		if (collisioninfo.collider.tag == "Boss" )
		{

			TakeDamage(100);

			m_Rigidbody2D.AddForce(new Vector2(50f, 50f));


		}
		if (collisioninfo.collider.name == "HealthItem")
		{
			health = 100;
			Destroy(collisioninfo.collider.gameObject);
		}

	}

    public void TakeDamage(int damage) //causa o dano recebido ao jogador, depois o deixa imune a estes danos depois por uns 2 segundos
    {
        if (!immune)
        {
            immune = true;
            Invoke("Recover", 2.0f);

            health -= damage;
        }


        if (health <= 0)
        {
            FindObjectOfType<GameManager>().EndGame();
            
        }

       
    }


    void Recover() //método Invokado para dar um tempo de recuperação pro jogador após tomar dano. desativa a imunidade
    {
        immune = false;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Danger"))
        {
            FindObjectOfType<GameManager>().EndGame();

        }
    }*/

}

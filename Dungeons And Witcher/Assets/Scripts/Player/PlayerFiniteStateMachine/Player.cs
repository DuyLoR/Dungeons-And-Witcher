using TMPro;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerTakendameState takendameState { get; private set; }
    public PlayerDeadState deadState { get; private set; }

    public Weapon weapon { get; private set; }
    public PlayerInputHandle inputHandle { get; private set; }
    public PlayerCollector playerCollector { get; private set; }

    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Collider2D col { get; private set; }

    public int facingDirection { get; private set; }

    [SerializeField]
    public PlayerData playerData;
    private Vector2 workspace;

    private float takendameTimer;

    private int currentHeal;

    private bool weaponHasBeenSet = false;
    private bool isTakendameOver;

    public void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
        moveState = new PlayerMoveState(this, stateMachine, playerData, "move");
        dashState = new PlayerDashState(this, stateMachine, playerData, "move");
        takendameState = new PlayerTakendameState(this, stateMachine, playerData, "takendame");
        deadState = new PlayerDeadState(this, stateMachine, playerData, "dead");
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputHandle = GetComponent<PlayerInputHandle>();
        weapon = GetComponentInChildren<Weapon>();
        animator = GetComponent<Animator>();
        col = GetComponentInChildren<Collider2D>();
        playerCollector = GetComponent<PlayerCollector>();
        playerCollector = GetComponent<PlayerCollector>();

        stateMachine.Initialize(idleState);
        playerCollector.Initialize(this);
        currentHeal = playerData.maxHp;
        takendameTimer = Time.time;
        facingDirection = 1;
    }
    private void Update()
    {
        stateMachine.currentState.LogicUpdate();
        if (Time.time >= takendameTimer + playerData.takendameDelay)
        {
            isTakendameOver = true;
        }
        //WEAPON
        if (!weaponHasBeenSet)
        {
            weaponHasBeenSet = true;
            weapon.SetWeaponData(InventoryManager.Instance.GetSeletedWeapon());
        }
        if (inputHandle.isWeaponInput)
        {
            InventoryManager.Instance.ChangeSelectedWeaponSlot(inputHandle.ChangeWeaponInput);
            weapon.SetWeaponData(InventoryManager.Instance.GetSeletedWeapon());
        }
        if (weapon.weaponData != null)
        {
            weapon.SetWeaponMouseTarget(inputHandle.mousePos);
            FixPositionWeapon();
            weapon.CheckIfAttack(inputHandle.attackInput && !inputHandle.inventoryInput);
            weapon.CheckIfShouldFlip(facingDirection);
            weapon.RotationAngleOfWeapon();
        }

        // INVENTORY
        InventoryManager.Instance.mainInventory.SetActive(inputHandle.inventoryInput);
        InventoryManager.Instance.orbInventorypanel.SetActive(inputHandle.inventoryInput);
        InventoryManager.Instance.weaponInfoPanel.SetActive(inputHandle.inventoryInput && WeaponInfo.instance.isWeaponDataNotNull());
    }
    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    public void SetVelocity(Vector2 velocity)
    {
        workspace = velocity;
        rb.velocity = workspace;
    }
    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction.normalized * velocity;
        rb.velocity = workspace;
    }
    public void CheckIfShouldFlip(Vector2 input)
    {
        Vector2 direction = input - rb.position;
        int xInput = (int)(direction * Vector2.right).normalized.x;
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }
    public void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    public void FixPositionWeapon()
    {
        Vector3 newPos = new Vector3(weapon.transform.localPosition.x, stateMachine.currentState == idleState ? 0.85f : 1.1f, 0f);
        weapon.ChangePosition(newPos);
    }

    public void Damage(int amount)
    {
        if (!isTakendameOver) return;

        stateMachine.ChangeState(takendameState);

        GameObject popUp = Instantiate(playerData.popupPrefabs, transform.position, Quaternion.identity, transform);
        TextMeshPro txt = popUp.GetComponent<TextMeshPro>();
        txt.text = amount.ToString();

        currentHeal -= amount;
        takendameTimer = Time.time;
        isTakendameOver = false;

        if (currentHeal <= 0)
        {
            stateMachine.ChangeState(deadState);
        }
    }
    public void DestroyGameObject()
    {
        //TODO: Spawn item after destroy
        gameObject.SetActive(false);
    }
}

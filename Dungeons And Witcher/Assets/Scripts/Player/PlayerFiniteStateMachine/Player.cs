using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerInputHandle inputHandle { get; private set; }
    public Weapon weapon { get; private set; }
    public PlayerCollector playerCollector { get; private set; }

    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Collider2D col { get; private set; }

    public int facingDirection { get; private set; }

    [SerializeField]
    public PlayerData playerData;
    private Vector2 workspace;

    private bool weaponHasBeenSet = false;

    public void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, playerData, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, playerData, "Move");
        dashState = new PlayerDashState(this, stateMachine, playerData);
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
        facingDirection = 1;
    }
    private void Update()
    {
        stateMachine.currentState.LogicUpdate();
        // INVENTORY
        InventoryManager.Instance.mainInventory.SetActive(inputHandle.inventoryInput);
        InventoryManager.Instance.orbInventory.SetActive(inputHandle.inventoryInput);

        if (!weaponHasBeenSet)
        {
            weaponHasBeenSet = true;
            weapon.InitializeWeapon(InventoryManager.Instance.GetSeletedWeapon());
        }

        //WEAPON
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
}

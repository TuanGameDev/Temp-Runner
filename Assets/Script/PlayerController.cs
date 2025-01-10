using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;

    private Animator _animator;

    private BoxCollider _boxCollider;

    [SerializeField]
    private float _speed = 20f;

    private float _horizontalInput;

    [SerializeField]
    private float jumpForce = 100f;

    private bool isGrounded = false;

    private bool isAlive = true;

    public int CurrentHP;

    private int _maxHP = 5;

    #region CurrentItem

    private int _currentBonusSpeed;

    private int _currentDef;

    #endregion

    private void Start()
    {
        CurrentHP = _maxHP;
        _rb = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _animator = gameObject.GetComponentInChildren<Animator>();

        GameUI.Instance.UpdateHP(CurrentHP, _maxHP);
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (transform.position.y < -5)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (isAlive == false) return;

        Vector3 forwardMove = transform.forward * _speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * _horizontalInput * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + horizontalMove + forwardMove);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("RaoChan"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
            Debug.Log("Va chạm với rào chắn");
        }
    }
    private void TakeDamage(int damage)
    {
        CurrentHP -= damage;
        CurrentHP = Mathf.Clamp(CurrentHP, 0, _maxHP);

        GameUI.Instance.UpdateHP(CurrentHP, _maxHP);

        if (CurrentHP <= 0)
        {
            Die();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Jump()
    {
        _animator.SetTrigger("Jump");
        _rb.AddForce(Vector3.up * jumpForce);
    }

    public void Die()
    {
        _animator.SetTrigger("IsAlive");
        isAlive = false;
        StartCoroutine(ResetIsAlive());
    }
    private IEnumerator ResetIsAlive()
    {
        yield return new WaitForSeconds(3);
        GameOver();

    }
    void GameOver()
    {
        SceneManager.LoadScene("GamePlay");
    }

    #region AddItem

    public void IncreaseHP(int amount)
    {
        CurrentHP += amount;
        Debug.Log("Tăng máu" + amount);
        CurrentHP = Mathf.Clamp(CurrentHP, 0, _maxHP);

        GameUI.Instance.UpdateHP(CurrentHP, _maxHP);
    }

    public void ActivateSpeedBonus(float amount, float duration)
    {
        if (_currentBonusSpeed == 0)
        {
            _speed += amount;
            Debug.Log("Tăng tốc độ" + amount);
            _currentBonusSpeed = 1;
            StartCoroutine(ResetSpeedBonus(amount, duration));
            GameUI.Instance.ShowSpeedTimer(duration);
        }
    }

    public void ActivateDefBonus(int amount, float duration)
    {
        if (_currentDef == 0)
        {
            _currentDef += amount;
            _rb.useGravity = false;
            _boxCollider.isTrigger = true;
            _currentDef = 1;
            StartCoroutine(ResetDefBonus(duration));
            GameUI.Instance.ShowDefTimer(duration);
        }
    }

    private IEnumerator ResetSpeedBonus(float amount, float duration)
    {
        yield return new WaitForSeconds(duration);
        _speed -= amount;
        _currentBonusSpeed = 0;
        GameUI.Instance.HideSpeedTimer();
    }

    private IEnumerator ResetDefBonus(float duration)
    {
        yield return new WaitForSeconds(duration);
        _rb.useGravity = true;
        _boxCollider.isTrigger = false;
        _currentDef = 0;
        GameUI.Instance.HideDefTimer();
    }

    #endregion
}

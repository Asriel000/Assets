using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover {

    public SpriteRenderer spriteRenderer;
    private Animator animator;

    // Arki - 0
    // Hila - 1
    // Chonk - 2
    // Konkon - 3
    public bool isAlive = true;

    protected override void Start() {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", x);
        animator.SetFloat("Vertical", x);
        animator.SetFloat("Horizontal", x);
        animator.SetFloat("Vertical", y);
        if (x != 0 || y != 0) animator.SetBool("isMove", true);
        else animator.SetBool("isMove", false);

        if (isAlive) UpdateMotor(new Vector3(x, y, 0));
    }

    public void SwapCharacter(int characterID) {
        animator.SetInteger("character", characterID);
        spriteRenderer.sprite = GameManager.instance.playerSprites[characterID];
        PlayerData.instance.characterID = characterID;
    }

    public void SetupAnimator(int characterID)
    {
        IEnumerator WaitUntilReady()
        {
            while (animator == null)
            {
                yield return null;
            }
            animator.SetInteger("character", characterID);
            spriteRenderer.sprite = GameManager.instance.playerSprites[characterID];
        }
        StartCoroutine(WaitUntilReady());
    }

    public int GetCharacterID() {
        return PlayerData.instance.characterID;
    }

    public void Respawn() {
        PlayerData.instance.Respawn();
    }

    public void StopPlayerMovement()
    {
        this.ySpeed = 0.0f;
        this.xSpeed = 0.0f;
    }

    public void ResumePlayerMovement()
    {
        this.ySpeed = 0.75f;
        this.xSpeed = 1.0f;
    }

    public void LoadNextScene()
    {
        GameManager.instance.LoadNextScene();
    }
}

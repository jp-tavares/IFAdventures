using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator
{
    SpriteRenderer spriteRenderer;
    List<Sprite> frames;
    float frameTime;

    int currentFrame;
    float timer;

    public List<Sprite> Frames { get => frames; }

    public SpriteAnimator(List<Sprite> frames, SpriteRenderer spriteRenderer, float frameTime=0.16f)
    {
        this.frames = frames;
        this.spriteRenderer = spriteRenderer;
        this.frameTime = frameTime;
    }

    public void Start()
    {
        currentFrame = 0;
        timer = 0f;
        spriteRenderer.sprite = frames[currentFrame];
    }

    public void HandleUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= frameTime)
        {
            currentFrame = (currentFrame + 1) % frames.Count;
            spriteRenderer.sprite = frames[currentFrame];
            timer -= frameTime;
        }
    }

}

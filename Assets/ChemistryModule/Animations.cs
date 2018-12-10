using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations {

    private Animation currentAnim = Animation.None;
    private Transform main, ext, g;
    private int animFrame = 0;

    public Animations(Transform global) {
        g = global;
    }

    public bool AnimationDone { get { return currentAnim == Animation.None; } }

    public void StartAnimation(Animation anim, Transform mainT, Transform extT) {
        if (!AnimationDone) return;

        currentAnim = anim;
        main = mainT;
        ext = extT;
        animFrame = 0;
    }

    public void UpdateAnimation() {
        switch (currentAnim) {
            case Animation.SolidExtend:
                if (animFrame < 20) {
                    main.localPosition = new Vector3(0, -.03f + animFrame * .0015f, 0);
                    ext.localPosition = new Vector3(0, .015f, -.054f);
                } else if (animFrame < 30) {
                    main.localPosition = new Vector3(0, 0, 0);
                    ext.localPosition = new Vector3(0, .015f, -.054f + ((animFrame - 20) * .0004f));
                } else {
                    main.localPosition = new Vector3(0, 0, 0);
                    ext.localPosition = new Vector3(0, .015f, -.05f);
                    currentAnim = Animation.None;
                }
                break;
            case Animation.SolidRetract:
                if (animFrame < 10) {
                    main.localPosition = new Vector3(0, 0, 0);
                    ext.localPosition = new Vector3(0, .015f, -.05f - animFrame * .0004f);
                } else if (animFrame < 30) {
                    main.localPosition = new Vector3(0, (animFrame - 10) * -.0015f, 0);
                    ext.localPosition = new Vector3(0, .015f, -.054f);
                } else {
                    main.localPosition = new Vector3(0, -.03f, 0);
                    ext.localPosition = new Vector3(0, .015f, -.054f);
                    currentAnim = Animation.None;
                }
                break;
            case Animation.LiquidExtend:
                if (animFrame < 20) {
                    main.localPosition = new Vector3(0, -.03f + animFrame * .0015f, 0);
                    ext.localPosition = new Vector3(0, .015f, -.054f);
                } else if (animFrame < 30) {
                    main.localPosition = new Vector3(0, 0, 0);
                    ext.localPosition = new Vector3(0, .015f, -.054f + ((animFrame - 20) * .0007f));
                } else {
                    main.localPosition = new Vector3(0, 0, 0);
                    ext.localPosition = new Vector3(0, .015f, -.047f);
                    currentAnim = Animation.None;
                }
                break;
            case Animation.LiquidRetract:
                if (animFrame < 10) {
                    main.localPosition = new Vector3(0, 0, 0);
                    ext.localPosition = new Vector3(0, .015f, -.047f - animFrame * .0007f);
                } else if (animFrame < 30) {
                    main.localPosition = new Vector3(0, (animFrame - 10) * -.0015f, 0);
                    ext.localPosition = new Vector3(0, .015f, -.054f);
                } else {
                    main.localPosition = new Vector3(0, -.03f, 0);
                    ext.localPosition = new Vector3(0, .015f, -.054f);
                    currentAnim = Animation.None;
                }
                break;
            case Animation.GasExtend:
                if (animFrame < 20) {
                    main.localPosition = new Vector3(0, -.03f + animFrame * .0015f, 0);
                    ext.localPosition = new Vector3(0, .015f, -.0645f);
                    ext.localScale = new Vector3(1, 1, 2.9f);
                } else if (animFrame < 40) {
                    main.localPosition = new Vector3(0, 0, 0);
                    ext.localPosition = new Vector3(0, .015f, -.0645f + ((animFrame - 20) * .003325f));
                    ext.localScale = new Vector3(1, 1, 2.9f + (animFrame - 20) * .405f);
                } else {
                    main.localPosition = new Vector3(0, 0, 0);
                    ext.localPosition = new Vector3(0, .015f, .002f);
                    ext.localScale = new Vector3(1, 1, 11f);
                    currentAnim = Animation.None;
                }
                break;
            case Animation.GasRetract:
                if (animFrame < 20) {
                    main.localPosition = new Vector3(0, 0, 0);
                    ext.localPosition = new Vector3(0, .015f, .002f - animFrame * .003325f);
                    ext.localScale = new Vector3(1, 1, 11f - animFrame * .405f);
                } else if (animFrame < 40) {
                    main.localPosition = new Vector3(0, (animFrame - 20) * -.0015f, 0);
                    ext.localPosition = new Vector3(0, .015f, -.0645f);
                    ext.localScale = new Vector3(1, 1, 2.9f);
                } else {
                    main.localPosition = new Vector3(0, -.03f, 0);
                    ext.localPosition = new Vector3(0, .015f, -.0645f);
                    ext.localScale = new Vector3(1, 1, 2.9f);
                    currentAnim = Animation.None;
                }
                break;
            default:
                return;
        }
        g.position = main.position;
        animFrame++;
    }

    public enum Animation {
        None = 0,
        SolidExtend,
        SolidRetract,
        LiquidExtend,
        LiquidRetract,
        GasExtend,
        GasRetract
    }
}

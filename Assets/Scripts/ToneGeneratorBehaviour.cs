using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ToneGeneratorBehaviour : Jundroo.SimplePlanes.ModTools.Parts.PartModifierBehaviour
{
    private ToneGenerator modifier;

    private AudioSource asrc;
    private AudioClipHolder alist;

    private bool InDesigner;
    private float Previous = 0f;

    private void Start()
    {
        InDesigner = ServiceProvider.Instance.GameState.IsInDesigner;

        modifier = (ToneGenerator)PartModifier;

        asrc = GetComponentInChildren<AudioSource>();
        alist = GetComponentInChildren<AudioClipHolder>();

        if (InDesigner)
        {
            asrc.enabled = false;
        }
        else
        {
            asrc.mute = false;
            ApplyValues();
        }
    }

    private void Update()
    {
        if (!InDesigner)
        {
            float input = InputController.Value;

            if (modifier.AudioType == "Sine" | modifier.AudioType == "Square" | modifier.AudioType == "Sawtooth")
            {
                asrc.pitch = input / 256f;
                if (input < 1f)
                {
                    asrc.Pause();
                }
                else if (Previous < 1f && input > 1f)
                {
                    asrc.Play();
                }
            }
            else
            {
                asrc.pitch = input;
                if (input < 0.001f)
                {
                    asrc.Pause();
                }
                else if (Previous < 0.001f && input > 0.001f)
                {
                    asrc.Play();
                }
            }

            Previous = input;
        }
    }

    private void ApplyValues()
    {
        asrc.clip = alist.SelectAudio(modifier.AudioType);

        asrc.bypassEffects = modifier.BypassEffects;
        asrc.bypassListenerEffects = modifier.BypassLEffects;
        asrc.bypassReverbZones = modifier.BypassReverb;

        asrc.volume = modifier.Volume / 2;
        asrc.panStereo = modifier.StereoPan;
        asrc.spatialBlend = modifier.SpatialBlend;
        asrc.reverbZoneMix = modifier.ReverbMix;
        asrc.dopplerLevel = modifier.Doppler;
        asrc.spread = modifier.Spread;
        asrc.minDistance = modifier.DistMin;
        asrc.maxDistance = modifier.DistMax;

        asrc.Play();
    }
}
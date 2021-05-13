using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ToneGenerator2Behaviour : Jundroo.SimplePlanes.ModTools.Parts.PartModifierBehaviour
{
    private ToneGenerator2 modifier;

    private AudioSource asrc;
    private AudioClipHolder alist;

    private bool InDesigner;
    private float Previous = 0f;

    private void Start()
    {
        InDesigner = ServiceProvider.Instance.GameState.IsInDesigner;

        modifier = (ToneGenerator2)PartModifier;

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

            asrc.volume = input / 2;
            if (input < 0.001f)
            {
                asrc.Pause();
            }
            else if (Previous < 0.001f && input > 0.001f)
            {
                asrc.Play();
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

        if (modifier.AudioType == "Sine" | modifier.AudioType == "Square" | modifier.AudioType == "Sawtooth")
        {
            asrc.pitch = modifier.Frequency / 256f;
        }
        else
        {
            asrc.pitch = 1f;
        }

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
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
            if (modifier.AudioType == "Sine" | modifier.AudioType == "Square" | modifier.AudioType == "Sawtooth")
            {
                asrc.pitch = InputController.Value / 256f;
            }
            else
            {
                asrc.pitch = InputController.Value;
            }
        }
    }

    private void ApplyValues()
    {
        asrc.clip = alist.SelectAudio(modifier.AudioType);

        asrc.bypassEffects = modifier.BypassEffects;
        asrc.bypassListenerEffects = modifier.BypassLEffects;
        asrc.bypassReverbZones = modifier.BypassReverb;

        asrc.volume = modifier.Volume;
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
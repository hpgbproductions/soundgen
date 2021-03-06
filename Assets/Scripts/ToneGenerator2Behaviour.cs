using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// Tone Generator, Variable Amplitude

public class ToneGenerator2Behaviour : Jundroo.SimplePlanes.ModTools.Parts.PartModifierBehaviour
{
    private ToneGenerator2 modifier;

    private AudioSource asrc;
    private AudioClipHolder alist;

    private bool InDesigner;
    private float Previous = 0f;

    private int FreqInputCh = -1;
    private ToneGeneratorBehaviour FreqInput;
    private bool FreqInputChecked = false;

    public int AmpOutputCh = -1;
    public float AmpOutput = 0f;

    private void Start()
    {
        InDesigner = ServiceProvider.Instance.GameState.IsInDesigner;

        modifier = (ToneGenerator2)PartModifier;
        FreqInputCh = modifier.FreqInputChannel;
        AmpOutputCh = modifier.AmpOutputChannel;

        asrc = GetComponentInChildren<AudioSource>();
        alist = GetComponentInChildren<AudioClipHolder>();

        if (InDesigner)
        {
            asrc.enabled = false;
        }
        else
        {
            if (AmpOutputCh >= 0)    // Amplitude transmitter mode
            {
                asrc.enabled = false;
            }
            else
            {
                asrc.mute = false;
                ApplyValues();
            }
        }
    }

    private void Update()
    {
        if (!InDesigner)
        {
            float input = InputController.Value;

            if (AmpOutputCh >= 0)    // Amplitude transmitter enabled
            {
                AmpOutput = input;
            }
            else
            {
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

            if (FreqInputCh >= 0)    // Frequency receiver enabled
            {
                // Apply frequency
                if (FreqInput == null)
                {
                    if (FreqInputChecked)
                    {
                        Debug.LogWarning("No TGVF block assigned to input channel " + FreqInputCh);
                    }
                    else
                    {
                        // Search for the first frequency transmitter of matching frequency channel
                        ToneGeneratorBehaviour[] FreqTxs = FindObjectsOfType<ToneGeneratorBehaviour>();
                        Debug.Log("FreqTxs Length: " + FreqTxs.Length);
                        foreach (ToneGeneratorBehaviour FreqTx in FreqTxs)
                        {
                            if (FreqTx.FreqOutputCh >= 0 && FreqTx.FreqOutputCh == FreqInputCh)
                            {
                                FreqInput = FreqTx;
                                break;
                            }
                        }
                        FreqInputChecked = true;
                    }
                }
                else
                {
                    if (modifier.AudioType == "Sine" | modifier.AudioType == "Square" | modifier.AudioType == "Sawtooth")
                    {
                        asrc.pitch = FreqInput.FreqOutput / 256f;
                    }
                    else
                    {
                        asrc.pitch = FreqInput.FreqOutput;
                    }
                }
            }
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
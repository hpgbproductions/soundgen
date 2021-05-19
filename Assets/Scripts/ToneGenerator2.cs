using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;
using Jundroo.SimplePlanes.ModTools.Parts;
using Jundroo.SimplePlanes.ModTools.Parts.Attributes;

/// <summary>
/// A part modifier for SimplePlanes.
/// A part modifier is responsible for attaching a part modifier behaviour script to a game object within a part's hierarchy.
/// </summary>
[Serializable]
public class ToneGenerator2 : Jundroo.SimplePlanes.ModTools.Parts.PartModifier
{
    [SerializeField]
    [DesignerPropertyToggleButton("Sine", "Square", "Sawtooth", "Noise", Header = "Variable Amplitude" , Label = "Audio Type", Order = 0)]
    private string _audiotype = "Sine";

    [SerializeField]
    [DesignerPropertyToggleButton(Header = "Basic Settings", Label = "Bypass Effects", Order = 100)]
    private bool _bypass_effects = false;

    [SerializeField]
    [DesignerPropertyToggleButton(Label = "Bypass Listener Effects", Order = 110)]
    private bool _bypass_leffects = false;

    [SerializeField]
    [DesignerPropertyToggleButton(Label = "Bypass Reverb Zones", Order = 120)]
    private bool _bypass_reverb = false;

    [SerializeField]
    [DesignerPropertySlider(Label = "Frequency", MaxValue = 3000f, MinValue = 100f, NumberOfSteps = 30, Order = 130)]
    private float _frequency = 256f;

    [SerializeField]
    [DesignerPropertySlider(Label = "Stereo Pan", MaxValue = 1f, MinValue = -1f, NumberOfSteps = 21, Order = 140)]
    private float _stereo_pan = 0f;

    [SerializeField]
    [DesignerPropertySlider(Label = "Spatial Blend", MaxValue = 1f, MinValue = 0f, NumberOfSteps = 21, Order = 150)]
    private float _spatialblend = 0f;

    [SerializeField]
    [DesignerPropertySlider(Label = "Reverb Zone Mix", MaxValue = 1.1f, MinValue = 0f, NumberOfSteps = 23, Order = 160)]
    private float _reverb_mix = 1f;

    [SerializeField]
    [DesignerPropertySlider(Header = "3D Sound Settings", Label = "Doppler Level", MaxValue = 5f, MinValue = 0f, NumberOfSteps = 26, Order = 200)]
    private float _doppler = 0f;

    [SerializeField]
    [DesignerPropertySlider(Label = "Spread", MaxValue = 180f, MinValue = 0f, NumberOfSteps = 19, Order = 210)]
    private float _spread = 0f;

    [SerializeField]
    [DesignerPropertySlider(Label = "Min. Distance", MaxValue = 100f, MinValue = 0f, NumberOfSteps = 21, Order = 220)]
    private float _dist_min = 5f;

    [SerializeField]
    [DesignerPropertySlider(Label = "Max. Distance", MaxValue = 100f, MinValue = 0f, NumberOfSteps = 21, Order = 230)]
    private float _dist_max = 50f;

    [SerializeField]
    [DesignerPropertySlider(Header = "Multi-Input Channels", MaxValue = 15, MinValue = -1, NumberOfSteps = 17, Label = "Frequency Input Channel", Order = 300)]
    private int _f_in = -1;

    [SerializeField]
    [DesignerPropertySlider(Label = "Amplitude Output Channel", MaxValue = 15, MinValue = -1, NumberOfSteps = 17, Order = 310)]
    private int _a_out = -1;

    public string AudioType
    {
        get
        {
            return _audiotype;
        }
    }

    public bool BypassEffects
    {
        get
        {
            return _bypass_effects;
        }
    }

    public bool BypassLEffects
    {
        get
        {
            return _bypass_leffects;
        }
    }

    public bool BypassReverb
    {
        get
        {
            return _bypass_reverb;
        }
    }

    public float Frequency
    {
        get
        {
            return _frequency;
        }
    }

    public float StereoPan
    {
        get
        {
            return _stereo_pan;
        }
    }

    public float SpatialBlend
    {
        get
        {
            return _spatialblend;
        }
    }

    public float ReverbMix
    {
        get
        {
            return _reverb_mix;
        }
    }

    public float Doppler
    {
        get
        {
            return _doppler;
        }
    }

    public float Spread
    {
        get
        {
            return _spread;
        }
    }

    public float DistMin
    {
        get
        {
            return _dist_min;
        }
    }

    public float DistMax
    {
        get
        {
            return _dist_max;
        }
    }

    public int FreqInputChannel
    {
        get
        {
            return _f_in;
        }
    }

    public int AmpOutputChannel
    {
        get
        {
            return _a_out;
        }
    }

    /// <summary>
    /// Called when this part modifiers is being initialized as the part game object is being created.
    /// </summary>
    /// <param name="partRootObject">The root game object that has been created for the part.</param>
    /// <returns>The created part modifier behaviour, or <c>null</c> if it was not created.</returns>
    public override Jundroo.SimplePlanes.ModTools.Parts.PartModifierBehaviour Initialize(UnityEngine.GameObject partRootObject)
    {
        // Attach the behaviour to the part's root object.
        var behaviour = partRootObject.AddComponent<ToneGenerator2Behaviour>();
        return behaviour;
    }
}
Shader "Custom/StencilMask"
{
    SubShader
    {
        Tags { "Queue"="Overlay" "RenderType"="Opaque" }

        Stencil
        {
            Ref 1        // Set stencil reference value to 1
            Comp Always  // Always pass
            Pass Replace // Replace stencil buffer value with Ref (1)
        }

        ColorMask 0  // Don't render the object itself
        ZWrite Off   

        Pass {}  // <-- You need at least one empty Pass to avoid errors!
    }
}

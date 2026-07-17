Shader "Shader Graphs/SHD_Objects_Colored"
{
    Properties
    {
        [IntRange] _StencilID("Stencil ID", Range(0,255)) = 0
        _Tilling("Tilling", Vector, 2) = (1, 1, 0, 0)
        [NoScaleOffset]_BaseColor("BaseColor", 2D) = "white" {}
        [Normal][NoScaleOffset]_Normal("Normal", 2D) = "bump" {}
        [NoScaleOffset]_ORM("ORM", 2D) = "white" {}
        [HelpBox(, 32)]_RoughnessMultiplier("RoughnessMultiplier", Float) = 1
        [ToggleUI]_UseCustomRoughness("UseCustomRoughness", Float) = 0
        _CustomRoughness("CustomRoughness", Float) = 1
        [HelpBox(, 32)]_AOColor("AOColor", Color) = (0, 0, 0, 1)
        _AOIntensity("AOIntensity", Range(0, 1)) = 1
        [RemapDrawer]_AOLevels("AOLevels", Vector, 2) = (0, 1, 0, 0)
        [ToggleUI]_DisableGradientMap("DisableGradientMap", Float) = 0
        [HelpBox(, 32)]_Color1("Color1", Color) = (1, 0, 0, 1)
        [RemapDrawer]_Color_1_Location("Color_1_Location", Vector, 2) = (0, 0.5, 0, 0)
        _Color2("Color2", Color) = (0, 1, 0, 1)
        [RemapDrawer]_Color_2_Location("Color_2_Location", Vector, 2) = (0.5, 1, 0, 0)
        _Color3("Color3", Color) = (0, 0, 1, 1)
        [ToggleUI]_DisableAllGradients("DisableAllGradients", Float) = 0
        [HelpBox(, 32)][ToggleUI]_X_Gradient("X_Gradient", Float) = 0
        _X_GradientIntensity("X_GradientIntensity", Range(0, 1)) = 0
        _X_GradientColor("X_GradientColor", Color) = (0, 0, 0, 1)
        _X_GradientStartPosition("X_GradientStartPosition", Float) = 0
        _X_GradientEndPosition("X_GradientEndPosition", Float) = 1
        [HelpBox(, 32)][ToggleUI]_Y_Gradient("Y_Gradient", Float) = 0
        _Y_GradientIntensity("Y_GradientIntensity", Range(0, 1)) = 0
        _Y_GradientColor("Y_GradientColor", Color) = (0, 0, 0, 1)
        _Y_GradientStartPosition("Y_GradientStartPosition", Float) = 0
        _Y_GradientEndPosition("Y_GradientEndPosition", Float) = 1
        [HelpBox(, 32)][ToggleUI]_Z_Gradient("Z_Gradient", Float) = 0
        _Z_GradientIntensity("Z_GradientIntensity", Range(0, 1)) = 0
        _Z_GradientColor("Z_GradientColor", Color) = (0, 0, 0, 1)
        _Z_GradientStartPosition("Z_GradientStartPosition", Float) = 0
        _Z_GradientEndPosition("Z_GradientEndPosition", Float) = 1
        _LightingGradientMapInfluence("LightingGradientMapInfluence", Range(0, 1)) = 0
        [KeywordEnum(Act 1, Act 2, Act 3)]_CURRENTACT("CurrentAct", Float) = 0
        [HelpBox(, 32)]_ACT1_Color_A("ACT1_Color_A", Color) = (0, 0, 0, 1)
        [RemapDrawer]_ACT1_Color_A_Location("ACT1_Color_A_Location", Vector, 2) = (0, 0.5, 0, 0)
        _ACT1_Color_B("ACT1_Color_B", Color) = (0.5, 0.5, 0.5, 1)
        [RemapDrawer]_ACT1_Color_B_Location("ACT1_Color_B_Location", Vector, 2) = (0.5, 1, 0, 0)
        _ACT1_Color_C("ACT1_Color_C", Color) = (1, 1, 1, 1)
        [HelpBox(, 32)]_ACT2_Color_A("ACT2_Color_A", Color) = (0, 0, 0, 1)
        [RemapDrawer]_ACT2_Color_A_Location("ACT2_Color_A_Location", Vector, 2) = (0, 0.5, 0, 0)
        _ACT2_Color_B("ACT2_Color_B", Color) = (0.5, 0.5, 0.5, 1)
        [RemapDrawer]_ACT2_Color_B_Location("ACT2_Color_B_Location", Vector, 2) = (0.5, 1, 0, 0)
        _ACT2_Color_C("ACT2_Color_C", Color) = (1, 1, 1, 1)
        [HelpBox(, 32)]_ACT3_Color_A("ACT3_Color_A", Color) = (0, 0, 0, 1)
        [RemapDrawer]_ACT3_Color_A_Location("ACT3_Color_A_Location", Vector, 2) = (0, 0.5, 0, 0)
        _ACT3_Color_B("ACT3_Color_B", Color) = (0.5, 0.5, 0.5, 1)
        [RemapDrawer]_ACT3_Color_B_Location("ACT3_Color_B_Location", Vector, 2) = (0.5, 1, 0, 0)
        _ACT3_Color_C("ACT3_Color_C", Color) = (1, 1, 1, 1)
        [HideInInspector]_WorkflowMode("_WorkflowMode", Float) = 1
        [HideInInspector]_CastShadows("_CastShadows", Float) = 1
        [HideInInspector]_ReceiveShadows("_ReceiveShadows", Float) = 1
        [HideInInspector]_Surface("_Surface", Float) = 0
        [HideInInspector]_Blend("_Blend", Float) = 0
        [HideInInspector]_AlphaClip("_AlphaClip", Float) = 0
        [HideInInspector]_BlendModePreserveSpecular("_BlendModePreserveSpecular", Float) = 1
        [HideInInspector]_SrcBlend("_SrcBlend", Float) = 1
        [HideInInspector]_DstBlend("_DstBlend", Float) = 0
        [HideInInspector]_SrcBlendAlpha("_SrcBlendAlpha", Float) = 1
        [HideInInspector]_DstBlendAlpha("_DstBlendAlpha", Float) = 0
        [HideInInspector][ToggleUI]_ZWrite("_ZWrite", Float) = 1
        [HideInInspector]_ZWriteControl("_ZWriteControl", Float) = 0
        [HideInInspector]_ZTest("_ZTest", Float) = 4
        [HideInInspector]_Cull("_Cull", Float) = 2
        [HideInInspector]_AlphaToMask("_AlphaToMask", Float) = 0
        [HideInInspector]_QueueOffset("_QueueOffset", Float) = 0
        [HideInInspector]_QueueControl("_QueueControl", Float) = -1
        [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Opaque"
            "UniversalMaterialType" = "Lit"
            "Queue"="Geometry"
            "DisableBatching"="False"
            "ShaderGraphShader"="true"
            "ShaderGraphTargetId"="UniversalLitSubTarget"
        }
        Pass
        {
            Name "Universal Forward"
            Tags
            {
                "LightMode" = "UniversalForward"
            }
        
        // Render State
        Cull [_Cull]
        Blend [_SrcBlend] [_DstBlend], [_SrcBlendAlpha] [_DstBlendAlpha]
        ZTest [_ZTest]
        ZWrite [_ZWrite]
        AlphaToMask [_AlphaToMask]
        
        Stencil
	    {
		Ref [_StencilID]
		Comp NotEqual
	    }

        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma multi_compile_instancing
        #pragma instancing_options renderinglayer
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
        #pragma multi_compile_fragment _ _SCREEN_SPACE_IRRADIANCE
        #pragma multi_compile _ LIGHTMAP_ON
        #pragma multi_compile _ DYNAMICLIGHTMAP_ON
        #pragma multi_compile _ DIRLIGHTMAP_COMBINED
        #pragma multi_compile _ USE_LEGACY_LIGHTMAPS
        #pragma multi_compile _ LIGHTMAP_BICUBIC_SAMPLING
        #pragma multi_compile _ REFLECTION_PROBE_ROTATION
        #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
        #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
        #pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
        #pragma multi_compile_fragment _ _REFLECTION_PROBE_BLENDING
        #pragma multi_compile_fragment _ _REFLECTION_PROBE_BOX_PROJECTION
        #pragma multi_compile_fragment _ _REFLECTION_PROBE_ATLAS
        #pragma multi_compile_fragment _ _SHADOWS_SOFT _SHADOWS_SOFT_LOW _SHADOWS_SOFT_MEDIUM _SHADOWS_SOFT_HIGH
        #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
        #pragma multi_compile _ SHADOWS_SHADOWMASK
        #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
        #pragma multi_compile_fragment _ _LIGHT_LAYERS
        #pragma multi_compile_fragment _ DEBUG_DISPLAY
        #pragma multi_compile_fragment _ _LIGHT_COOKIES
        #pragma multi_compile _ _CLUSTER_LIGHT_LOOP
        #pragma multi_compile _ EVALUATE_SH_MIXED EVALUATE_SH_VERTEX
        #pragma shader_feature_fragment _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local_fragment _ _ALPHAPREMULTIPLY_ON
        #pragma shader_feature_local_fragment _ _ALPHAMODULATE_ON
        #pragma shader_feature_local_fragment _ _ALPHATEST_ON
        #pragma shader_feature_local_fragment _ _SPECULAR_SETUP
        #pragma shader_feature_local _ _RECEIVE_SHADOWS_OFF
        #pragma shader_feature _CURRENTACT_ACT_1 _CURRENTACT_ACT_2 _CURRENTACT_ACT_3
        
        
        
        // Defines
        
        #define _NORMALMAP 1
        #define _NORMAL_DROPOFF_TS 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define ATTRIBUTES_NEED_TEXCOORD1
        #define ATTRIBUTES_NEED_TEXCOORD2
        #define FEATURES_GRAPH_VERTEX_NORMAL_OUTPUT
        #define FEATURES_GRAPH_VERTEX_TANGENT_OUTPUT
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TANGENT_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define VARYINGS_NEED_TEXCOORD1
        #define VARYINGS_NEED_TEXCOORD2
        #define VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
        #define VARYINGS_NEED_SHADOW_COORD
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_FORWARD
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Fog.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ProbeVolumeVariants.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 uv1 : TEXCOORD1;
             float4 uv2 : TEXCOORD2;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(ATTRIBUTES_NEED_INSTANCEID)
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
             float4 tangentWS;
             float4 texCoord0;
             float4 texCoord1;
             float4 texCoord2;
            #if defined(LIGHTMAP_ON)
             float2 staticLightmapUV;
            #endif
            #if defined(DYNAMICLIGHTMAP_ON)
             float2 dynamicLightmapUV;
            #endif
            #if !defined(LIGHTMAP_ON)
             float3 sh;
            #endif
            #if defined(USE_APV_PROBE_OCCLUSION)
             float4 probeOcclusion;
            #endif
             float4 fogFactorAndVertexLight;
            #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
             float4 shadowCoord;
            #endif
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 WorldSpaceNormal;
             float3 TangentSpaceNormal;
             float3 WorldSpaceTangent;
             float3 WorldSpaceBiTangent;
             float3 WorldSpaceViewDirection;
             float3 ObjectSpacePosition;
             float3 WorldSpacePosition;
             float3 AbsoluteWorldSpacePosition;
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float4 uv1;
             float4 uv2;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
            #if defined(LIGHTMAP_ON)
             float2 staticLightmapUV : INTERP0;
            #endif
            #if defined(DYNAMICLIGHTMAP_ON)
             float2 dynamicLightmapUV : INTERP1;
            #endif
            #if !defined(LIGHTMAP_ON)
             float3 sh : INTERP2;
            #endif
            #if defined(USE_APV_PROBE_OCCLUSION)
             float4 probeOcclusion : INTERP3;
            #endif
            #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
             float4 shadowCoord : INTERP4;
            #endif
             float4 tangentWS : INTERP5;
             float4 texCoord0 : INTERP6;
             float4 texCoord1 : INTERP7;
             float4 texCoord2 : INTERP8;
             float4 fogFactorAndVertexLight : INTERP9;
             float3 positionWS : INTERP10;
             float3 normalWS : INTERP11;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            #if defined(LIGHTMAP_ON)
            output.staticLightmapUV = input.staticLightmapUV;
            #endif
            #if defined(DYNAMICLIGHTMAP_ON)
            output.dynamicLightmapUV = input.dynamicLightmapUV;
            #endif
            #if !defined(LIGHTMAP_ON)
            output.sh = input.sh;
            #endif
            #if defined(USE_APV_PROBE_OCCLUSION)
            output.probeOcclusion = input.probeOcclusion;
            #endif
            #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
            output.shadowCoord = input.shadowCoord;
            #endif
            output.tangentWS.xyzw = input.tangentWS;
            output.texCoord0.xyzw = input.texCoord0;
            output.texCoord1.xyzw = input.texCoord1;
            output.texCoord2.xyzw = input.texCoord2;
            output.fogFactorAndVertexLight.xyzw = input.fogFactorAndVertexLight;
            output.positionWS.xyz = input.positionWS;
            output.normalWS.xyz = input.normalWS;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            #if defined(LIGHTMAP_ON)
            output.staticLightmapUV = input.staticLightmapUV;
            #endif
            #if defined(DYNAMICLIGHTMAP_ON)
            output.dynamicLightmapUV = input.dynamicLightmapUV;
            #endif
            #if !defined(LIGHTMAP_ON)
            output.sh = input.sh;
            #endif
            #if defined(USE_APV_PROBE_OCCLUSION)
            output.probeOcclusion = input.probeOcclusion;
            #endif
            #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
            output.shadowCoord = input.shadowCoord;
            #endif
            output.tangentWS = input.tangentWS.xyzw;
            output.texCoord0 = input.texCoord0.xyzw;
            output.texCoord1 = input.texCoord1.xyzw;
            output.texCoord2 = input.texCoord2.xyzw;
            output.fogFactorAndVertexLight = input.fogFactorAndVertexLight.xyzw;
            output.positionWS = input.positionWS.xyz;
            output.normalWS = input.normalWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _DisableAllGradients;
        float4 _ACT1_Color_B;
        float4 _ACT2_Color_B;
        float2 _ACT2_Color_B_Location;
        float4 _ACT2_Color_C;
        float4 _ACT2_Color_A;
        float2 _ACT2_Color_A_Location;
        float2 _ACT1_Color_B_Location;
        float4 _ACT1_Color_C;
        float4 _ACT1_Color_A;
        float2 _ACT1_Color_A_Location;
        float4 _ACT3_Color_B;
        float2 _ACT3_Color_B_Location;
        float4 _ACT3_Color_C;
        float4 _ACT3_Color_A;
        float2 _ACT3_Color_A_Location;
        float4 _ORM_TexelSize;
        float4 _Color3;
        float _Y_GradientEndPosition;
        float4 _Color2;
        float2 _Color_2_Location;
        float4 _AOColor;
        float4 _Normal_TexelSize;
        float2 _AOLevels;
        float4 _BaseColor_TexelSize;
        float4 _Color1;
        float2 _Color_1_Location;
        float _AOIntensity;
        float _RoughnessMultiplier;
        float2 _Tilling;
        float _DisableGradientMap;
        float4 _Y_GradientColor;
        float _Y_GradientIntensity;
        float _Y_GradientStartPosition;
        float _Y_Gradient;
        float _X_Gradient;
        float _X_GradientIntensity;
        float4 _X_GradientColor;
        float _X_GradientStartPosition;
        float _X_GradientEndPosition;
        float _Z_Gradient;
        float _Z_GradientIntensity;
        float4 _Z_GradientColor;
        float _Z_GradientStartPosition;
        float _Z_GradientEndPosition;
        float _UseCustomRoughness;
        float _CustomRoughness;
        float _LightingGradientMapInfluence;
        UNITY_TEXTURE_STREAMING_DEBUG_VARS;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ORM);
        SAMPLER(sampler_ORM);
        TEXTURE2D(_Normal);
        SAMPLER(sampler_Normal);
        TEXTURE2D(_BaseColor);
        SAMPLER(sampler_BaseColor);
        
        // Graph Includes
        #include_with_pragmas "Assets/Samples/Shader Graph/17.3.0/Custom Lighting/Components/Debug/DebugLightingComplexity.hlsl"
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
        {
            Out = lerp(A, B, T);
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Branch_float4(float Predicate, float4 True, float4 False, out float4 Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        void Unity_Branch_float(float Predicate, float True, float False, out float Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Divide_float(float A, float B, out float Out)
        {
            Out = A / B;
        }
        
        // unity-custom-func-begin
        void ApplyDecals_float(float4 positionCS, float3 baseColor, float3 specularColor, float3 normalWS, float metallic, float smoothness, float occlusion, out float3 baseColorOut, out float3 specularColorOut, out float3 normalWSOut, out float metallicOut, out float smoothnessOut, out float occlusionOut){
        #if !defined(SHADERGRAPH_PREVIEW) && defined(_DBUFFER)
        	ApplyDecal(positionCS, baseColor, specularColor, normalWS, metallic, occlusion, smoothness);
        	baseColorOut = baseColor;
        	specularColorOut = specularColor;
        	normalWSOut = normalWS;
        	metallicOut = metallic;
        	occlusionOut = occlusion;
        	smoothnessOut = smoothness;
        #else
        	baseColorOut = baseColor;
        	specularColorOut = specularColor;
        	normalWSOut = normalWS;
        	metallicOut = metallic;
        	occlusionOut = occlusion;
        	smoothnessOut = smoothness;
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float
        {
        float2 PixelPosition;
        };
        
        void SG_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float(float3 _Base_Color, float3 _NormalWS, float _Metallic, float _Smoothness, float _AmbientOcclusion, Bindings_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float IN, out float3 BaseColor_1, out float3 SpecularColor_2, out float3 NormalWS_3, out float Metallic_4, out float Smoothness_6, out float AmbientOcclusion_5)
        {
        float4 _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4 = float4(IN.PixelPosition.xy, 0, 0);
        float _Split_ad27d29658ef44f7b6941c97694d6866_R_1_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[0];
        float _Split_ad27d29658ef44f7b6941c97694d6866_G_2_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[1];
        float _Split_ad27d29658ef44f7b6941c97694d6866_B_3_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[2];
        float _Split_ad27d29658ef44f7b6941c97694d6866_A_4_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[3];
        float _Divide_3f9fb3b7b5b94d0d8246bbc34aa63f7b_Out_2_Float;
        Unity_Divide_float(_Split_ad27d29658ef44f7b6941c97694d6866_G_2_Float, _ScreenParams.y, _Divide_3f9fb3b7b5b94d0d8246bbc34aa63f7b_Out_2_Float);
        float _OneMinus_3cfec48ba27f4585a5feb790836dc9dc_Out_1_Float;
        Unity_OneMinus_float(_Divide_3f9fb3b7b5b94d0d8246bbc34aa63f7b_Out_2_Float, _OneMinus_3cfec48ba27f4585a5feb790836dc9dc_Out_1_Float);
        float _Multiply_a4baed73797c41c1b9631ae9bbddfe12_Out_2_Float;
        Unity_Multiply_float_float(_OneMinus_3cfec48ba27f4585a5feb790836dc9dc_Out_1_Float, _ScreenParams.y, _Multiply_a4baed73797c41c1b9631ae9bbddfe12_Out_2_Float);
        float2 _Vector2_eed86f79e1de4c188df97eb091955bc5_Out_0_Vector2 = float2(_Split_ad27d29658ef44f7b6941c97694d6866_R_1_Float, _Multiply_a4baed73797c41c1b9631ae9bbddfe12_Out_2_Float);
        float3 _Property_6219e38e66a84dddb55188eb0359a8c3_Out_0_Vector3 = _Base_Color;
        float3 _Property_f4c37d8281c1497e8dab743349080d88_Out_0_Vector3 = _NormalWS;
        float _Property_0826181079c84604befc19a2460f4daa_Out_0_Float = _Metallic;
        float _Property_d54a743184cc4f27b93d5f5b239c7b7e_Out_0_Float = _Smoothness;
        float _Property_bd6cbdae9db240b9b4ad935655106f79_Out_0_Float = _AmbientOcclusion;
        float3 _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_baseColorOut_8_Vector3;
        float3 _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_specularColorOut_9_Vector3;
        float3 _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_normalWSOut_10_Vector3;
        float _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_metallicOut_11_Float;
        float _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_smoothnessOut_13_Float;
        float _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_occlusionOut_12_Float;
        ApplyDecals_float((float4(_Vector2_eed86f79e1de4c188df97eb091955bc5_Out_0_Vector2, 0.0, 1.0)), _Property_6219e38e66a84dddb55188eb0359a8c3_Out_0_Vector3, float3 (0, 0, 0), _Property_f4c37d8281c1497e8dab743349080d88_Out_0_Vector3, _Property_0826181079c84604befc19a2460f4daa_Out_0_Float, _Property_d54a743184cc4f27b93d5f5b239c7b7e_Out_0_Float, _Property_bd6cbdae9db240b9b4ad935655106f79_Out_0_Float, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_baseColorOut_8_Vector3, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_specularColorOut_9_Vector3, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_normalWSOut_10_Vector3, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_metallicOut_11_Float, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_smoothnessOut_13_Float, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_occlusionOut_12_Float);
        BaseColor_1 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_baseColorOut_8_Vector3;
        SpecularColor_2 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_specularColorOut_9_Vector3;
        NormalWS_3 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_normalWSOut_10_Vector3;
        Metallic_4 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_metallicOut_11_Float;
        Smoothness_6 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_smoothnessOut_13_Float;
        AmbientOcclusion_5 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_occlusionOut_12_Float;
        }
        
        // unity-custom-func-begin
        void SwitchLightingDebug_float(float3 BaseColorIn, float3 NormalIn, float MetallicIn, float SmoothnessIn, float3 EmissionIn, float AmbientOcclusionIn, float3 positionWS, float3 bakedGI, out float3 BaseColorOut, out float3 NormalOut, out float MetallicOut, out float SmoothnessOut, out float3 EmissionOut, out float AmbientOcclusionOut){
        #if !defined(SHADERGRAPH_PREVIEW) && defined(DEBUG_DISPLAY)
        
        [branch] switch(int(_DebugLightingMode))
        
        {
        
            case 0: //none
        
        		BaseColorOut = BaseColorIn;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
            case 1: //SHADOW_CASCADES
        
        		half cascadeIndex = ComputeCascadeIndex(positionWS);
        
        		switch (uint(cascadeIndex))
        
        		{
        
        			case 0: BaseColorOut = kDebugColorShadowCascade0.rgb;break;
        
        			case 1: BaseColorOut = kDebugColorShadowCascade1.rgb;break;
        
        			case 2: BaseColorOut = kDebugColorShadowCascade2.rgb;break;
        
        			case 3: BaseColorOut = kDebugColorShadowCascade3.rgb;break;
        
        			default: BaseColorOut = kDebugColorBlack.rgb;break;
        
        		}
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
            case 2: //LIGHTING_WITHOUT_NORMAL_MAPS
        
        		BaseColorOut = float3(1,1,1);
        
        		MetallicOut = 0;
        
        		SmoothnessOut = 0;
        
        		NormalOut = float3(0,0,1);
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            case 3: //LIGHTING_WITH_NORMAL_MAPS
        
        		BaseColorOut = float3(1,1,1);
        
        		MetallicOut = 0;
        
        		SmoothnessOut = 0;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            case 4: //REFLECTIONS
        
        		BaseColorOut = float3(0.1,0.1,0.1);
        
        		MetallicOut = 1;
        
        		SmoothnessOut = 1;
        
        		NormalOut = float3(0,0,1);
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            case 5: //REFLECTIONS_WITH_SMOOTHNESS
        
        		BaseColorOut = float3(0.1,0.1,0.1);
        
        		MetallicOut = 1;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
            case 6: //GLOBAL_ILLUM
        
        		BaseColorOut = bakedGI;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = 0;
        
        		NormalOut = float3(0,0,1);
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            default:
        
        		BaseColorOut = BaseColorIn;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
        }
        
        #else
        
        		BaseColorOut = BaseColorIn;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_DebugLighting_61e571d2b9ede1240a524a849d20c997_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpacePosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_DebugLighting_61e571d2b9ede1240a524a849d20c997_float(float3 _Base_Color, float3 _NormalWS, float _Metallic, float _Smoothness, float3 _Emission, float _AmbientOcclusion, Bindings_DebugLighting_61e571d2b9ede1240a524a849d20c997_float IN, out float3 BaseColor_1, out float3 Normal_4, out float Metallic_2, out float Smoothness_3, out float3 Emission_5, out float AmbientOcclusion_6)
        {
        float3 _Property_501515703e3a4a1dbd19f4ae273add46_Out_0_Vector3 = _Base_Color;
        float3 _Property_e5bcb5bf3b62412b8983e3aa1ada8fcd_Out_0_Vector3 = _NormalWS;
        float3 _Transform_13c6b1e888d440fd8340d4a7138979ba_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_13c6b1e888d440fd8340d4a7138979ba_Out_1_Vector3 = TransformWorldToTangent(_Property_e5bcb5bf3b62412b8983e3aa1ada8fcd_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float _Property_7a450453146043b2b11397a72c325042_Out_0_Float = _Metallic;
        float _Property_f0326121e031478a90610d60b8321364_Out_0_Float = _Smoothness;
        float3 _Property_491d95b34bb245718ee21bff5fc249cd_Out_0_Vector3 = _Emission;
        float _Property_da91a6effd53499db08bb774d5686c68_Out_0_Float = _AmbientOcclusion;
        float3 _BakedGI_3f01c30cb8b64e9d9f7fbe474622c7dc_Out_1_Vector3 = SHADERGRAPH_BAKED_GI(IN.WorldSpacePosition, _Property_e5bcb5bf3b62412b8983e3aa1ada8fcd_Out_0_Vector3, IN.PixelPosition.xy, IN.uv1.xy, IN.uv2.xy, true);
        float3 _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_BaseColorOut_7_Vector3;
        float3 _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_NormalOut_11_Vector3;
        float _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_MetallicOut_9_Float;
        float _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_SmoothnessOut_10_Float;
        float3 _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_EmissionOut_12_Vector3;
        float _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_AmbientOcclusionOut_13_Float;
        SwitchLightingDebug_float(_Property_501515703e3a4a1dbd19f4ae273add46_Out_0_Vector3, _Transform_13c6b1e888d440fd8340d4a7138979ba_Out_1_Vector3, _Property_7a450453146043b2b11397a72c325042_Out_0_Float, _Property_f0326121e031478a90610d60b8321364_Out_0_Float, _Property_491d95b34bb245718ee21bff5fc249cd_Out_0_Vector3, _Property_da91a6effd53499db08bb774d5686c68_Out_0_Float, IN.WorldSpacePosition, _BakedGI_3f01c30cb8b64e9d9f7fbe474622c7dc_Out_1_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_BaseColorOut_7_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_NormalOut_11_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_MetallicOut_9_Float, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_SmoothnessOut_10_Float, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_EmissionOut_12_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_AmbientOcclusionOut_13_Float);
        BaseColor_1 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_BaseColorOut_7_Vector3;
        Normal_4 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_NormalOut_11_Vector3;
        Metallic_2 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_MetallicOut_9_Float;
        Smoothness_3 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_SmoothnessOut_10_Float;
        Emission_5 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_EmissionOut_12_Vector3;
        AmbientOcclusion_6 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_AmbientOcclusionOut_13_Float;
        }
        
        // unity-custom-func-begin
        void GetMainLightData_float(float3 worldPos, out float3 direction, out float3 color, out float shadowAtten){
        #ifdef SHADERGRAPH_PREVIEW
            direction = normalize(float3(-0.7,0.7,-0.7));
            color = float3(1,1,1);
            shadowAtten = 1;
        #else
            #if defined(UNIVERSAL_PIPELINE_CORE_INCLUDED)
                float4 shadowCoord = TransformWorldToShadowCoord(worldPos);
                Light mainLight = GetMainLight(shadowCoord);
                direction = mainLight.direction;
                color = mainLight.color;
                shadowAtten = mainLight.shadowAttenuation;
            #else
                direction = normalize(float3(-0.7, 0.7, -0.7));
                color = float3(1, 1, 1);
                shadowAtten = 0;
            #endif
        #endif
        }
        // unity-custom-func-end
        
        // unity-custom-func-begin
        void GetMainLightData_half(half3 worldPos, out half3 direction, out half3 color, out half shadowAtten){
        #ifdef SHADERGRAPH_PREVIEW
            direction = normalize(float3(-0.7,0.7,-0.7));
            color = float3(1,1,1);
            shadowAtten = 1;
        #else
            #if defined(UNIVERSAL_PIPELINE_CORE_INCLUDED)
                float4 shadowCoord = TransformWorldToShadowCoord(worldPos);
                Light mainLight = GetMainLight(shadowCoord);
                direction = mainLight.direction;
                color = mainLight.color;
                shadowAtten = mainLight.shadowAttenuation;
            #else
                direction = normalize(float3(-0.7, 0.7, -0.7));
                color = float3(1, 1, 1);
                shadowAtten = 0;
            #endif
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float
        {
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float IN, out float3 Direction_1, out float3 Color_2, out float ShadowAtten_3)
        {
        float3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        float3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        float _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        GetMainLightData_float(IN.AbsoluteWorldSpacePosition, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float);
        Direction_1 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        Color_2 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        ShadowAtten_3 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        }
        
        struct Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half
        {
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half(Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half IN, out half3 Direction_1, out half3 Color_2, out half ShadowAtten_3)
        {
        half3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        half3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        half _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        GetMainLightData_half(IN.AbsoluteWorldSpacePosition, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float);
        Direction_1 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        Color_2 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        ShadowAtten_3 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        }
        
        void Unity_DotProduct_float3(float3 A, float3 B, out float Out)
        {
            Out = dot(A, B);
        }
        
        void Unity_DotProduct_half3(half3 A, half3 B, out half Out)
        {
            Out = dot(A, B);
        }
        
        void Unity_Saturate_half(half In, out half Out)
        {
            Out = saturate(In);
        }
        
        struct Bindings_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half
        {
        float3 WorldSpaceNormal;
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half(half3 _NormalWS, bool _NormalWS_68a7999ae9ea4bfba3702fd95b0d1a14_IsConnected, half3 _LightVector, bool _LightVector_a12354c78b694cc6b2bdddd67d09ccdc_IsConnected, Bindings_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half IN, out half Diffuse_1)
        {
        half3 _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3 = _NormalWS;
        bool _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3_IsConnected = _NormalWS_68a7999ae9ea4bfba3702fd95b0d1a14_IsConnected;
        half3 _BranchOnInputConnection_71cde5ac4ee04aacb1e2544c8017ba47_Out_3_Vector3 = _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3_IsConnected ? _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3 : IN.WorldSpaceNormal;
        half3 _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3 = _LightVector;
        bool _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3_IsConnected = _LightVector_a12354c78b694cc6b2bdddd67d09ccdc_IsConnected;
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half _MainLight_fa0151c045984bcab58e58725bae0709;
        _MainLight_fa0151c045984bcab58e58725bae0709.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_fa0151c045984bcab58e58725bae0709_Direction_1_Vector3;
        half3 _MainLight_fa0151c045984bcab58e58725bae0709_Color_2_Vector3;
        half _MainLight_fa0151c045984bcab58e58725bae0709_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half(_MainLight_fa0151c045984bcab58e58725bae0709, _MainLight_fa0151c045984bcab58e58725bae0709_Direction_1_Vector3, _MainLight_fa0151c045984bcab58e58725bae0709_Color_2_Vector3, _MainLight_fa0151c045984bcab58e58725bae0709_ShadowAtten_3_Float);
        half3 _BranchOnInputConnection_d18845e766084954af1aa554531c90b9_Out_3_Vector3 = _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3_IsConnected ? _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3 : _MainLight_fa0151c045984bcab58e58725bae0709_Direction_1_Vector3;
        half _DotProduct_daa979e4f9384944a14a23e079be6a5c_Out_2_Float;
        Unity_DotProduct_half3(_BranchOnInputConnection_71cde5ac4ee04aacb1e2544c8017ba47_Out_3_Vector3, _BranchOnInputConnection_d18845e766084954af1aa554531c90b9_Out_3_Vector3, _DotProduct_daa979e4f9384944a14a23e079be6a5c_Out_2_Float);
        half _Saturate_4747439b9bfa431293a93646ce71aa12_Out_1_Float;
        Unity_Saturate_half(_DotProduct_daa979e4f9384944a14a23e079be6a5c_Out_2_Float, _Saturate_4747439b9bfa431293a93646ce71aa12_Out_1_Float);
        Diffuse_1 = _Saturate_4747439b9bfa431293a93646ce71aa12_Out_1_Float;
        }
        
        void Unity_Lerp_float(float A, float B, float T, out float Out)
        {
            Out = lerp(A, B, T);
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Reciprocal_float(float In, out float Out)
        {
            Out = 1.0/In;
        }
        
        void Unity_FresnelEffect_float(float3 Normal, float3 ViewDir, float Power, out float Out)
        {
            Out = pow((1.0 - saturate(dot(normalize(Normal), ViewDir))), Power);
        }
        
        void Unity_Lerp_float3(float3 A, float3 B, float3 T, out float3 Out)
        {
            Out = lerp(A, B, T);
        }
        
        struct Bindings_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        };
        
        void SG_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float(float3 _Base_Color, float3 _NormalWS, bool _NormalWS_3240674a787044a092398b1ca753ad83_IsConnected, float _Metallic, float _Smoothness, float _F0, Bindings_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float IN, out float3 Reflectance_1)
        {
        float _Property_4678b902494b4e1a9b08a8067b7bed85_Out_0_Float = _Smoothness;
        float _OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float;
        Unity_OneMinus_float(_Property_4678b902494b4e1a9b08a8067b7bed85_Out_0_Float, _OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float);
        float _Multiply_314f6a0aec0a44538333e69617e91cf9_Out_2_Float;
        Unity_Multiply_float_float(_OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float, _OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float, _Multiply_314f6a0aec0a44538333e69617e91cf9_Out_2_Float);
        float _Add_513724b99c2f4ea2a803e64d80f0c25b_Out_2_Float;
        Unity_Add_float(_Multiply_314f6a0aec0a44538333e69617e91cf9_Out_2_Float, float(1), _Add_513724b99c2f4ea2a803e64d80f0c25b_Out_2_Float);
        float _Reciprocal_67b338305d0043abb9e7d82dd0f16146_Out_1_Float;
        Unity_Reciprocal_float(_Add_513724b99c2f4ea2a803e64d80f0c25b_Out_2_Float, _Reciprocal_67b338305d0043abb9e7d82dd0f16146_Out_1_Float);
        float _Property_b67a6773dce34e91ae69bbf282d871cc_Out_0_Float = _F0;
        float _Property_703d9ec0a0894a3b965f0ed25a10435b_Out_0_Float = _F0;
        float _Add_8ad10875906b4a80872f2b2fb0518183_Out_2_Float;
        Unity_Add_float(_Property_4678b902494b4e1a9b08a8067b7bed85_Out_0_Float, _Property_703d9ec0a0894a3b965f0ed25a10435b_Out_0_Float, _Add_8ad10875906b4a80872f2b2fb0518183_Out_2_Float);
        float3 _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3 = _NormalWS;
        bool _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3_IsConnected = _NormalWS_3240674a787044a092398b1ca753ad83_IsConnected;
        float3 _BranchOnInputConnection_43b8bde55a8a41468ba21d53db128986_Out_3_Vector3 = _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3_IsConnected ? _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3 : IN.WorldSpaceNormal;
        float _FresnelEffect_34b729c62edd4d5f99dc20e2e7d0a7fa_Out_3_Float;
        Unity_FresnelEffect_float(_BranchOnInputConnection_43b8bde55a8a41468ba21d53db128986_Out_3_Vector3, IN.WorldSpaceViewDirection, float(4), _FresnelEffect_34b729c62edd4d5f99dc20e2e7d0a7fa_Out_3_Float);
        float _Lerp_1cad776e609842c181b8acfaef47c317_Out_3_Float;
        Unity_Lerp_float(_Property_b67a6773dce34e91ae69bbf282d871cc_Out_0_Float, _Add_8ad10875906b4a80872f2b2fb0518183_Out_2_Float, _FresnelEffect_34b729c62edd4d5f99dc20e2e7d0a7fa_Out_3_Float, _Lerp_1cad776e609842c181b8acfaef47c317_Out_3_Float);
        float _Multiply_0d364d1c231246d981281b868ea74a95_Out_2_Float;
        Unity_Multiply_float_float(_Reciprocal_67b338305d0043abb9e7d82dd0f16146_Out_1_Float, _Lerp_1cad776e609842c181b8acfaef47c317_Out_3_Float, _Multiply_0d364d1c231246d981281b868ea74a95_Out_2_Float);
        float3 _Property_87ae51a595c24e46ad9ef0f4493231fc_Out_0_Vector3 = _Base_Color;
        float _Property_ce0a90815c5046b48dd0564711f2b466_Out_0_Float = _Metallic;
        float3 _Lerp_6b4c86a1d5004851a7dba1bacc4fd953_Out_3_Vector3;
        Unity_Lerp_float3((_Multiply_0d364d1c231246d981281b868ea74a95_Out_2_Float.xxx), _Property_87ae51a595c24e46ad9ef0f4493231fc_Out_0_Vector3, (_Property_ce0a90815c5046b48dd0564711f2b466_Out_0_Float.xxx), _Lerp_6b4c86a1d5004851a7dba1bacc4fd953_Out_3_Vector3);
        Reflectance_1 = _Lerp_6b4c86a1d5004851a7dba1bacc4fd953_Out_3_Vector3;
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        void Unity_Normalize_float3(float3 In, out float3 Out)
        {
            Out = normalize(In);
        }
        
        struct Bindings_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float
        {
        };
        
        void SG_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float(float3 _viewDir, float3 _lightDir, Bindings_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float IN, out float3 Out_1)
        {
        float3 _Property_fde52ad74bda46adabbcc34b42b16131_Out_0_Vector3 = _viewDir;
        float3 _Property_1dc55a6640574aaf8c04290eb0d5e816_Out_0_Vector3 = _lightDir;
        float3 _Add_2a8cf5c52e8c4e3fb8c3f9a87b68b2a2_Out_2_Vector3;
        Unity_Add_float3(_Property_fde52ad74bda46adabbcc34b42b16131_Out_0_Vector3, _Property_1dc55a6640574aaf8c04290eb0d5e816_Out_0_Vector3, _Add_2a8cf5c52e8c4e3fb8c3f9a87b68b2a2_Out_2_Vector3);
        float3 _Normalize_b3b8196f46224ae3a998bba24956de7f_Out_1_Vector3;
        Unity_Normalize_float3(_Add_2a8cf5c52e8c4e3fb8c3f9a87b68b2a2_Out_2_Vector3, _Normalize_b3b8196f46224ae3a998bba24956de7f_Out_1_Vector3);
        Out_1 = _Normalize_b3b8196f46224ae3a998bba24956de7f_Out_1_Vector3;
        }
        
        void Unity_Exponential2_float(float In, out float Out)
        {
            Out = exp2(In);
        }
        
        struct Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float
        {
        };
        
        void SG_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float(float _Smoothness, Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float IN, out float SpecPower_1)
        {
        float _Property_80f639c6927445458cce37e8c24909a1_Out_0_Float = _Smoothness;
        float _Multiply_c7e9a2c01b804964a64dd7499f62a400_Out_2_Float;
        Unity_Multiply_float_float(_Property_80f639c6927445458cce37e8c24909a1_Out_0_Float, 10, _Multiply_c7e9a2c01b804964a64dd7499f62a400_Out_2_Float);
        float _Add_663b867a23b04deaa88c2ebd79bcdbc7_Out_2_Float;
        Unity_Add_float(_Multiply_c7e9a2c01b804964a64dd7499f62a400_Out_2_Float, float(1), _Add_663b867a23b04deaa88c2ebd79bcdbc7_Out_2_Float);
        float _Exponential_bd8d33b85b2e46508bef0c6cc36f9dae_Out_1_Float;
        Unity_Exponential2_float(_Add_663b867a23b04deaa88c2ebd79bcdbc7_Out_2_Float, _Exponential_bd8d33b85b2e46508bef0c6cc36f9dae_Out_1_Float);
        SpecPower_1 = _Exponential_bd8d33b85b2e46508bef0c6cc36f9dae_Out_1_Float;
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
        Out = A * B;
        }
        
        struct Bindings_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float(float3 _NormalWS, bool _NormalWS_5a3c9a3a7faa491894a42d170b5bfeb5_IsConnected, float _Smoothness, float3 _Reflectance, float3 _LightVector, bool _LightVector_3db37b6247094f32bcccc4cb689d525f_IsConnected, Bindings_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float IN, out float3 Specular_1)
        {
        float3 _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3 = _LightVector;
        bool _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3_IsConnected = _LightVector_3db37b6247094f32bcccc4cb689d525f_IsConnected;
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float _MainLight_6570bf88718b46ebb6bd80eec408287a;
        _MainLight_6570bf88718b46ebb6bd80eec408287a.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_6570bf88718b46ebb6bd80eec408287a_Direction_1_Vector3;
        half3 _MainLight_6570bf88718b46ebb6bd80eec408287a_Color_2_Vector3;
        half _MainLight_6570bf88718b46ebb6bd80eec408287a_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(_MainLight_6570bf88718b46ebb6bd80eec408287a, _MainLight_6570bf88718b46ebb6bd80eec408287a_Direction_1_Vector3, _MainLight_6570bf88718b46ebb6bd80eec408287a_Color_2_Vector3, _MainLight_6570bf88718b46ebb6bd80eec408287a_ShadowAtten_3_Float);
        float3 _BranchOnInputConnection_6a7b13b3cb82474aa187229c3d17a00f_Out_3_Vector3 = _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3_IsConnected ? _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3 : _MainLight_6570bf88718b46ebb6bd80eec408287a_Direction_1_Vector3;
        Bindings_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float _HalfAngle_f48886360d2649d8b7540e6fb3eef669;
        half3 _HalfAngle_f48886360d2649d8b7540e6fb3eef669_Out_1_Vector3;
        SG_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float(IN.WorldSpaceViewDirection, _BranchOnInputConnection_6a7b13b3cb82474aa187229c3d17a00f_Out_3_Vector3, _HalfAngle_f48886360d2649d8b7540e6fb3eef669, _HalfAngle_f48886360d2649d8b7540e6fb3eef669_Out_1_Vector3);
        float3 _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3 = _NormalWS;
        bool _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3_IsConnected = _NormalWS_5a3c9a3a7faa491894a42d170b5bfeb5_IsConnected;
        float3 _BranchOnInputConnection_72430741d0e04d2dbf5368b624a090cc_Out_3_Vector3 = _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3_IsConnected ? _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3 : IN.WorldSpaceNormal;
        float _DotProduct_4558c6edcb084d359ac8ee4b2934ea05_Out_2_Float;
        Unity_DotProduct_float3(_HalfAngle_f48886360d2649d8b7540e6fb3eef669_Out_1_Vector3, _BranchOnInputConnection_72430741d0e04d2dbf5368b624a090cc_Out_3_Vector3, _DotProduct_4558c6edcb084d359ac8ee4b2934ea05_Out_2_Float);
        float _Saturate_23d3962f416c4150a990dcb36b5ecbc4_Out_1_Float;
        Unity_Saturate_float(_DotProduct_4558c6edcb084d359ac8ee4b2934ea05_Out_2_Float, _Saturate_23d3962f416c4150a990dcb36b5ecbc4_Out_1_Float);
        float _Property_f4ccf6ae090a4694bb78a2cef88028e0_Out_0_Float = _Smoothness;
        Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9;
        half _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9_SpecPower_1_Float;
        SG_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float(_Property_f4ccf6ae090a4694bb78a2cef88028e0_Out_0_Float, _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9, _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9_SpecPower_1_Float);
        float _Power_b652c8bd47c44415bf7733f381883bf3_Out_2_Float;
        Unity_Power_float(_Saturate_23d3962f416c4150a990dcb36b5ecbc4_Out_1_Float, _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9_SpecPower_1_Float, _Power_b652c8bd47c44415bf7733f381883bf3_Out_2_Float);
        float _Property_1fcbde0798cd43628cbb75583e5d6e7a_Out_0_Float = _Smoothness;
        float _Multiply_8e72dd78784942f5ac8322f3055fd0ed_Out_2_Float;
        Unity_Multiply_float_float(_Power_b652c8bd47c44415bf7733f381883bf3_Out_2_Float, _Property_1fcbde0798cd43628cbb75583e5d6e7a_Out_0_Float, _Multiply_8e72dd78784942f5ac8322f3055fd0ed_Out_2_Float);
        float3 _Property_5b0bef6a4de54859800dd057235a4dbc_Out_0_Vector3 = _Reflectance;
        float3 _Multiply_73cf80666f44441494dd412a147e089c_Out_2_Vector3;
        Unity_Multiply_float3_float3((_Multiply_8e72dd78784942f5ac8322f3055fd0ed_Out_2_Float.xxx), _Property_5b0bef6a4de54859800dd057235a4dbc_Out_0_Vector3, _Multiply_73cf80666f44441494dd412a147e089c_Out_2_Vector3);
        float3 _Multiply_bceb91846ebf445c9093523786845bb5_Out_2_Vector3;
        Unity_Multiply_float3_float3(_Multiply_73cf80666f44441494dd412a147e089c_Out_2_Vector3, float3(10, 10, 10), _Multiply_bceb91846ebf445c9093523786845bb5_Out_2_Vector3);
        Specular_1 = _Multiply_bceb91846ebf445c9093523786845bb5_Out_2_Vector3;
        }
        
        // unity-custom-func-begin
        void AddAdditionalLightsSimple_float(float SpecPower, float3 WorldPosition, float3 WorldNormal, float3 WorldView, float MainDiffuse, float3 MainSpecular, float3 MainColor, float3 Reflectance, float2 ScreenPosition, out float Diffuse, out float3 Specular, out float3 Color){
        Diffuse = MainDiffuse;
        
        Specular = MainSpecular;
        
        Color = MainColor * (MainDiffuse + MainSpecular);
        
        
        
        #ifndef SHADERGRAPH_PREVIEW
        
            
        
        #if defined(_ADDITIONAL_LIGHTS) || defined(_CLUSTER_LIGHT_LOOP)
        
        
        
            #if defined(_ADDITIONAL_LIGHTS)
        
                uint pixelLightCount = GetAdditionalLightsCount();
        
            #endif
        
        
        
        #if USE_CLUSTER_LIGHT_LOOP
        
            // for Foward+ LIGHT_LOOP_BEGIN macro uses inputData.normalizedScreenSpaceUV and inputData.positionWS
        
            InputData inputData = (InputData)0;
        
        
        
            inputData.normalizedScreenSpaceUV = ScreenPosition;
        
            inputData.positionWS = WorldPosition;
        
        #endif
        
        
        
            LIGHT_LOOP_BEGIN(pixelLightCount)
        
        		// Call the URP additional light algorithm. This will not calculate shadows, since we don't pass a shadow mask value
        
        		Light light = GetAdditionalLight(lightIndex, WorldPosition);
        
        		// Manually set the shadow attenuation by calculating realtime shadows
        
        		light.shadowAttenuation = AdditionalLightRealtimeShadow(lightIndex, WorldPosition, light.direction);
        
                float NdotL = saturate(dot(WorldNormal, light.direction));
        
                float atten = light.distanceAttenuation * light.shadowAttenuation;
        
                float thisDiffuse = atten * NdotL;
        
                float3 halfAngle = normalize(light.direction + WorldView);
        
                float spec = pow(saturate(dot(halfAngle, WorldNormal)), SpecPower);
        
                float3 thisSpecular = spec * Reflectance * atten;
        
                Diffuse += thisDiffuse;
        
                Specular += thisSpecular;
        
                #if defined(_LIGHT_COOKIES)
        
                    float3 cookieColor = SampleAdditionalLightCookie(lightIndex, WorldPosition);
        
                    light.color *= cookieColor;
        
                #endif
        
                Color += light.color * (thisDiffuse + thisSpecular);
        
            LIGHT_LOOP_END
        
            float total = Diffuse + dot(Specular, float3(0.333, 0.333, 0.333));
        
            Color = total <= 0 ? MainColor : Color / total;
        
        #endif // _ADDITIONAL_LIGHTS || _CLUSTER_LIGHT_LOOP
        
        
        
        #endif // SHADERGRAPH_PREVIEW
        }
        // unity-custom-func-end
        
        struct Bindings_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        };
        
        void SG_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float(float _MainLightDiffuse, float3 _MainLightSpecular, float3 _MainLightColor, float3 _NormalWS, bool _NormalWS_70cbf5ac6da04bf6bd87eb71ccb7c48d_IsConnected, float _Smoothness, float3 _Reflectance, Bindings_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float IN, out float Diffuse_1, out float3 Specular_2, out float3 Color_3)
        {
        float _Property_b9f05025da4f4857a7b1b6f56259a629_Out_0_Float = _Smoothness;
        Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1;
        half _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1_SpecPower_1_Float;
        SG_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float(_Property_b9f05025da4f4857a7b1b6f56259a629_Out_0_Float, _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1, _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1_SpecPower_1_Float);
        float3 _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3 = _NormalWS;
        bool _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3_IsConnected = _NormalWS_70cbf5ac6da04bf6bd87eb71ccb7c48d_IsConnected;
        float3 _BranchOnInputConnection_d869e3d8654b48a491de945ad8af6301_Out_3_Vector3 = _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3_IsConnected ? _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3 : IN.WorldSpaceNormal;
        float _Property_25880f0697234954b8dc6ef11af3752d_Out_0_Float = _MainLightDiffuse;
        float3 _Property_1e29ad89226c4d84a936fe7530839aef_Out_0_Vector3 = _MainLightSpecular;
        float3 _Property_ac790fc8215b4b3d8851855d2153960d_Out_0_Vector3 = _MainLightColor;
        float3 _Property_eea8eda455d44ae7b30c65f80baac806_Out_0_Vector3 = _Reflectance;
        float4 _ScreenPosition_bb4bf3ece5524d4c898132bd377d7d8b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Diffuse_7_Float;
        float3 _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Specular_8_Vector3;
        float3 _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Color_9_Vector3;
        AddAdditionalLightsSimple_float(_SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1_SpecPower_1_Float, IN.WorldSpacePosition, _BranchOnInputConnection_d869e3d8654b48a491de945ad8af6301_Out_3_Vector3, IN.WorldSpaceViewDirection, _Property_25880f0697234954b8dc6ef11af3752d_Out_0_Float, _Property_1e29ad89226c4d84a936fe7530839aef_Out_0_Vector3, _Property_ac790fc8215b4b3d8851855d2153960d_Out_0_Vector3, _Property_eea8eda455d44ae7b30c65f80baac806_Out_0_Vector3, (_ScreenPosition_bb4bf3ece5524d4c898132bd377d7d8b_Out_0_Vector4.xy), _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Diffuse_7_Float, _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Specular_8_Vector3, _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Color_9_Vector3);
        Diffuse_1 = _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Diffuse_7_Float;
        Specular_2 = _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Specular_8_Vector3;
        Color_3 = _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Color_9_Vector3;
        }
        
        void Unity_Negate_float3(float3 In, out float3 Out)
        {
            Out = -1 * In;
        }
        
        void Unity_Reflection_float3(float3 In, float3 Normal, out float3 Out)
        {
            Out = reflect(In, Normal);
        }
        
        // unity-custom-func-begin
        void URPReflectionProbe_float(float3 positionWS, float3 reflectVector, float2 normalizedScreenSpaceUV, float roughness, float occlusion, out float3 reflection){
        #ifdef SHADERGRAPH_PREVIEW
        
            reflection = float3(0,0,0);
        
        #else
        
            reflection = GlossyEnvironmentReflection(reflectVector, positionWS, roughness, occlusion, normalizedScreenSpaceUV);
        
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        };
        
        void SG_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float(float3 _positionWS, bool _positionWS_d6701bdc1f184a57ac2283491fc460d9_IsConnected, float3 _reflectVector, bool _reflectVector_3e2eb19b69b8469eaf2302c7abc4cbc5_IsConnected, float _smoothness, float _occlusion, Bindings_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float IN, out float3 Out_1)
        {
        float3 _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3 = _positionWS;
        bool _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3_IsConnected = _positionWS_d6701bdc1f184a57ac2283491fc460d9_IsConnected;
        float3 _BranchOnInputConnection_8fb583036b0c4313a1ecd93143939f21_Out_3_Vector3 = _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3_IsConnected ? _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3 : IN.WorldSpacePosition;
        float3 _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3 = _reflectVector;
        bool _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3_IsConnected = _reflectVector_3e2eb19b69b8469eaf2302c7abc4cbc5_IsConnected;
        float3 _Negate_9cf7cea21c5641239fdbcb32480ac39e_Out_1_Vector3;
        Unity_Negate_float3(IN.WorldSpaceViewDirection, _Negate_9cf7cea21c5641239fdbcb32480ac39e_Out_1_Vector3);
        float3 _Reflection_c8689c494aab4411aadc74299549c6cb_Out_2_Vector3;
        Unity_Reflection_float3(_Negate_9cf7cea21c5641239fdbcb32480ac39e_Out_1_Vector3, IN.WorldSpaceNormal, _Reflection_c8689c494aab4411aadc74299549c6cb_Out_2_Vector3);
        float3 _BranchOnInputConnection_9600230d09794702a61c1a01f8e842a5_Out_3_Vector3 = _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3_IsConnected ? _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3 : _Reflection_c8689c494aab4411aadc74299549c6cb_Out_2_Vector3;
        float4 _ScreenPosition_270e438746a9466e8aaf01f4903f62fb_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float _Property_9012e47da801473d8ef85a4092281eb2_Out_0_Float = _smoothness;
        float _OneMinus_b2508f25afb44017ba3480edc35cf631_Out_1_Float;
        Unity_OneMinus_float(_Property_9012e47da801473d8ef85a4092281eb2_Out_0_Float, _OneMinus_b2508f25afb44017ba3480edc35cf631_Out_1_Float);
        float _Property_d602b1723845462cbf00324de1e9e82a_Out_0_Float = _occlusion;
        float3 _URPReflectionProbeCustomFunction_7233d098cd214f55baf898d486bfdf4b_reflection_4_Vector3;
        URPReflectionProbe_float(_BranchOnInputConnection_8fb583036b0c4313a1ecd93143939f21_Out_3_Vector3, _BranchOnInputConnection_9600230d09794702a61c1a01f8e842a5_Out_3_Vector3, (_ScreenPosition_270e438746a9466e8aaf01f4903f62fb_Out_0_Vector4.xy), _OneMinus_b2508f25afb44017ba3480edc35cf631_Out_1_Float, _Property_d602b1723845462cbf00324de1e9e82a_Out_0_Float, _URPReflectionProbeCustomFunction_7233d098cd214f55baf898d486bfdf4b_reflection_4_Vector3);
        Out_1 = _URPReflectionProbeCustomFunction_7233d098cd214f55baf898d486bfdf4b_reflection_4_Vector3;
        }
        
        // unity-custom-func-begin
        void SSAO_float(float2 normalizedScreenSpaceUV, out float indirectAmbientOcclusion, out float directAmbientOcclusion){
        #if defined(_SCREEN_SPACE_OCCLUSION) && !defined(_SURFACE_TYPE_TRANSPARENT) && !defined(SHADERGRAPH_PREVIEW)
        
            float ssao = saturate(SampleAmbientOcclusion(normalizedScreenSpaceUV) + (1.0 - _AmbientOcclusionParam.x));
        
            indirectAmbientOcclusion = ssao;
        
            directAmbientOcclusion = lerp(half(1.0), ssao, _AmbientOcclusionParam.w);
        
        #else
        
            directAmbientOcclusion = half(1.0);
        
            indirectAmbientOcclusion = half(1.0);
        
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float
        {
        float2 NDCPosition;
        };
        
        void SG_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float(Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float IN, out float indirectAO_1, out float directAO_2)
        {
        float4 _ScreenPosition_0fdc511287e14fd48ca909caba575383_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_indirectAmbientOcclusion_0_Float;
        float _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_directAmbientOcclusion_1_Float;
        SSAO_float((_ScreenPosition_0fdc511287e14fd48ca909caba575383_Out_0_Vector4.xy), _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_indirectAmbientOcclusion_0_Float, _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_directAmbientOcclusion_1_Float);
        indirectAO_1 = _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_indirectAmbientOcclusion_0_Float;
        directAO_2 = _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_directAmbientOcclusion_1_Float;
        }
        
        void Unity_Minimum_float(float A, float B, out float Out)
        {
            Out = min(A, B);
        };
        
        struct Bindings_AmbientURP_300875fdd653fe340b08ad1547984cf1_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_AmbientURP_300875fdd653fe340b08ad1547984cf1_float(float3 _Base_Color, float3 _NormalWS, bool _NormalWS_3a565a44841d4b729f8e86b08d09299c_IsConnected, float _Metallic, float _Smoothness, float3 _Reflectance, float _Ambient_Occlusion, Bindings_AmbientURP_300875fdd653fe340b08ad1547984cf1_float IN, out float3 Ambient_1, out float DirectAO_2)
        {
        float3 _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3 = _NormalWS;
        bool _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3_IsConnected = _NormalWS_3a565a44841d4b729f8e86b08d09299c_IsConnected;
        float3 _BranchOnInputConnection_5e35c0fc2eee4cf7ae47da45409fa2a7_Out_3_Vector3 = _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3_IsConnected ? _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3 : IN.WorldSpaceNormal;
        float3 _BakedGI_1ac35076ff2349f99fec2cef2550ff2d_Out_1_Vector3 = SHADERGRAPH_BAKED_GI(IN.WorldSpacePosition, _BranchOnInputConnection_5e35c0fc2eee4cf7ae47da45409fa2a7_Out_3_Vector3, IN.PixelPosition.xy, IN.uv1.xy, IN.uv2.xy, true);
        float3 _Property_5fb17e215f49424cb9cc9d0806f3f47d_Out_0_Vector3 = _Base_Color;
        float _Property_f995d8544fdb448d85ac845c7bdee967_Out_0_Float = _Metallic;
        float3 _Lerp_842ba4fcd0cf48a3afa108eecd7d56a8_Out_3_Vector3;
        Unity_Lerp_float3(_Property_5fb17e215f49424cb9cc9d0806f3f47d_Out_0_Vector3, float3(0, 0, 0), (_Property_f995d8544fdb448d85ac845c7bdee967_Out_0_Float.xxx), _Lerp_842ba4fcd0cf48a3afa108eecd7d56a8_Out_3_Vector3);
        float3 _Multiply_48ca9faac6354eefabc65eeb591f3fc4_Out_2_Vector3;
        Unity_Multiply_float3_float3(_BakedGI_1ac35076ff2349f99fec2cef2550ff2d_Out_1_Vector3, _Lerp_842ba4fcd0cf48a3afa108eecd7d56a8_Out_3_Vector3, _Multiply_48ca9faac6354eefabc65eeb591f3fc4_Out_2_Vector3);
        float3 _Negate_4f98a1dfbc4d4bd1abe58f406453303f_Out_1_Vector3;
        Unity_Negate_float3(IN.WorldSpaceViewDirection, _Negate_4f98a1dfbc4d4bd1abe58f406453303f_Out_1_Vector3);
        float3 _Reflection_710a69cb22b745929e6bb9b84d11de2f_Out_2_Vector3;
        Unity_Reflection_float3(_Negate_4f98a1dfbc4d4bd1abe58f406453303f_Out_1_Vector3, _BranchOnInputConnection_5e35c0fc2eee4cf7ae47da45409fa2a7_Out_3_Vector3, _Reflection_710a69cb22b745929e6bb9b84d11de2f_Out_2_Vector3);
        float _Property_8c3e921b9cb34f7b82d2a71254653c09_Out_0_Float = _Smoothness;
        Bindings_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.WorldSpaceNormal = IN.WorldSpaceNormal;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.WorldSpacePosition = IN.WorldSpacePosition;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.NDCPosition = IN.NDCPosition;
        float3 _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08_Out_1_Vector3;
        SG_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float(half3 (0, 0, 0), false, _Reflection_710a69cb22b745929e6bb9b84d11de2f_Out_2_Vector3, true, _Property_8c3e921b9cb34f7b82d2a71254653c09_Out_0_Float, half(1), _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08, _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08_Out_1_Vector3);
        float3 _Property_2ddaa58bd1e94d0b8508ce91ad39fa39_Out_0_Vector3 = _Reflectance;
        float3 _Multiply_40be6c08ca2f44ef99b6a2b81955d62c_Out_2_Vector3;
        Unity_Multiply_float3_float3(_SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08_Out_1_Vector3, _Property_2ddaa58bd1e94d0b8508ce91ad39fa39_Out_0_Vector3, _Multiply_40be6c08ca2f44ef99b6a2b81955d62c_Out_2_Vector3);
        float3 _Add_c0a57a54eb8b4419a7e907a65e6556a7_Out_2_Vector3;
        Unity_Add_float3(_Multiply_48ca9faac6354eefabc65eeb591f3fc4_Out_2_Vector3, _Multiply_40be6c08ca2f44ef99b6a2b81955d62c_Out_2_Vector3, _Add_c0a57a54eb8b4419a7e907a65e6556a7_Out_2_Vector3);
        float _Property_5a996c5d941d46019b3ac5ada526c475_Out_0_Float = _Ambient_Occlusion;
        Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e;
        _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e.NDCPosition = IN.NDCPosition;
        half _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_indirectAO_1_Float;
        half _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_directAO_2_Float;
        SG_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float(_ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_indirectAO_1_Float, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_directAO_2_Float);
        float _Minimum_699e1643e9974baa97f6f21c8f949d09_Out_2_Float;
        Unity_Minimum_float(_Property_5a996c5d941d46019b3ac5ada526c475_Out_0_Float, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_indirectAO_1_Float, _Minimum_699e1643e9974baa97f6f21c8f949d09_Out_2_Float);
        float3 _Multiply_ccaf4dfa5cdb4d9fae7fd92dc7d512da_Out_2_Vector3;
        Unity_Multiply_float3_float3(_Add_c0a57a54eb8b4419a7e907a65e6556a7_Out_2_Vector3, (_Minimum_699e1643e9974baa97f6f21c8f949d09_Out_2_Float.xxx), _Multiply_ccaf4dfa5cdb4d9fae7fd92dc7d512da_Out_2_Vector3);
        float _Minimum_c4dcab6b18b34dde987e68f86d22aed7_Out_2_Float;
        Unity_Minimum_float(_Property_5a996c5d941d46019b3ac5ada526c475_Out_0_Float, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_directAO_2_Float, _Minimum_c4dcab6b18b34dde987e68f86d22aed7_Out_2_Float);
        Ambient_1 = _Multiply_ccaf4dfa5cdb4d9fae7fd92dc7d512da_Out_2_Vector3;
        DirectAO_2 = _Minimum_c4dcab6b18b34dde987e68f86d22aed7_Out_2_Float;
        }
        
        void Unity_Fog_float(out float4 Color, out float Density, float3 Position)
        {
            SHADERGRAPH_FOG(Position, Color, Density);
        }
        
        struct Bindings_Fog_286ae83400099a24bba6faf005588be7_float
        {
        float3 ObjectSpacePosition;
        };
        
        void SG_Fog_286ae83400099a24bba6faf005588be7_float(float3 _In, Bindings_Fog_286ae83400099a24bba6faf005588be7_float IN, out float3 Out_1)
        {
        float3 _Property_626923dc627443639da97776de7dcc22_Out_0_Vector3 = _In;
        float4 _Fog_acabe3c84c9549f3880ce7d106150576_Color_0_Vector4;
        float _Fog_acabe3c84c9549f3880ce7d106150576_Density_1_Float;
        Unity_Fog_float(_Fog_acabe3c84c9549f3880ce7d106150576_Color_0_Vector4, _Fog_acabe3c84c9549f3880ce7d106150576_Density_1_Float, IN.ObjectSpacePosition);
        float3 _Lerp_9cfca9aa08c7423bab62b39a01237d64_Out_3_Vector3;
        Unity_Lerp_float3(_Property_626923dc627443639da97776de7dcc22_Out_0_Vector3, (_Fog_acabe3c84c9549f3880ce7d106150576_Color_0_Vector4.xyz), (_Fog_acabe3c84c9549f3880ce7d106150576_Density_1_Float.xxx), _Lerp_9cfca9aa08c7423bab62b39a01237d64_Out_3_Vector3);
        Out_1 = _Lerp_9cfca9aa08c7423bab62b39a01237d64_Out_3_Vector3;
        }
        
        void Unity_Saturate_float3(float3 In, out float3 Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Saturation_float(float3 In, float Saturation, out float3 Out)
        {
            float luma = dot(In, float3(0.2126729, 0.7151522, 0.0721750));
            Out =  luma.xxx + Saturation.xxx * (In - luma.xxx);
        }
        
        void Unity_Contrast_float(float3 In, float Contrast, out float3 Out)
        {
            float midpoint = pow(0.5, 2.2);
            Out =  (In - midpoint) * Contrast + midpoint;
        }
        
        void Unity_Remap_float3(float3 In, float2 InMinMax, float2 OutMinMax, out float3 Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        struct Bindings_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpaceViewDirection;
        float3 ObjectSpacePosition;
        float3 WorldSpacePosition;
        float3 AbsoluteWorldSpacePosition;
        float2 NDCPosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float(float3 _Base_Color, float3 _Normal, bool _Normal_e1611e545480449d80aa5a0e7c2b63c4_IsConnected, float _Metallic, float _Smoothness, float _Micro_Occlusion, float _Ambient_Occlusion, float4 _Color_A, float2 _Color_A_Location, float4 _Color_B, float2 _Color_B_Location, float4 _Color_C, Bindings_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float IN, out float3 Lit_1)
        {
        float4 _Property_bedee92e97ce4b2abff5524ce019b2a8_Out_0_Vector4 = _Color_A;
        float4 _Property_c648ff794fd34283beff09e33d8293fc_Out_0_Vector4 = _Color_B;
        float3 _Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3 = _Normal;
        bool _Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3_IsConnected = _Normal_e1611e545480449d80aa5a0e7c2b63c4_IsConnected;
        float3 _Transform_75d5405941a444c088600c650a7269f0_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_75d5405941a444c088600c650a7269f0_Out_1_Vector3 = TransformTangentToWorld(_Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float3 _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3 = _Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3_IsConnected ? _Transform_75d5405941a444c088600c650a7269f0_Out_1_Vector3 : IN.WorldSpaceNormal;
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float _MainLight_d918d0814080438585a810ba0b8afeb4;
        _MainLight_d918d0814080438585a810ba0b8afeb4.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3;
        half3 _MainLight_d918d0814080438585a810ba0b8afeb4_Color_2_Vector3;
        half _MainLight_d918d0814080438585a810ba0b8afeb4_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(_MainLight_d918d0814080438585a810ba0b8afeb4, _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3, _MainLight_d918d0814080438585a810ba0b8afeb4_Color_2_Vector3, _MainLight_d918d0814080438585a810ba0b8afeb4_ShadowAtten_3_Float);
        Bindings_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half _DiffuseLambert_7f9e988376a2438ebc87097469e065d3;
        _DiffuseLambert_7f9e988376a2438ebc87097469e065d3.WorldSpaceNormal = IN.WorldSpaceNormal;
        _DiffuseLambert_7f9e988376a2438ebc87097469e065d3.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half _DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float;
        SG_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half(_BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3, true, _DiffuseLambert_7f9e988376a2438ebc87097469e065d3, _DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float);
        float _Property_ac10139ecedb4301b4595fa5b13c00b8_Out_0_Float = _Smoothness;
        float3 _Property_58fa7de1b7784467935169b8914ee373_Out_0_Vector3 = _Base_Color;
        float _Property_433019e2d18944a2909e58d06f7cc1ec_Out_0_Float = _Metallic;
        float _Property_dd950e92d2d54fefbb89aaa0d1f6b713_Out_0_Float = _Smoothness;
        float _Property_dce33fc56d0e4204bd34d323af11f8ca_Out_0_Float = _Micro_Occlusion;
        float _Multiply_d120aee0dde541e69ff32688ddb2bee0_Out_2_Float;
        Unity_Multiply_float_float(_Property_dce33fc56d0e4204bd34d323af11f8ca_Out_0_Float, 0.5, _Multiply_d120aee0dde541e69ff32688ddb2bee0_Out_2_Float);
        float _Lerp_6742a96ab4984154ae535aef42b348f7_Out_3_Float;
        Unity_Lerp_float(float(0), float(0.08), _Multiply_d120aee0dde541e69ff32688ddb2bee0_Out_2_Float, _Lerp_6742a96ab4984154ae535aef42b348f7_Out_3_Float);
        Bindings_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d;
        _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d.WorldSpaceNormal = IN.WorldSpaceNormal;
        _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        half3 _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3;
        SG_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float(_Property_58fa7de1b7784467935169b8914ee373_Out_0_Vector3, _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_433019e2d18944a2909e58d06f7cc1ec_Out_0_Float, _Property_dd950e92d2d54fefbb89aaa0d1f6b713_Out_0_Float, _Lerp_6742a96ab4984154ae535aef42b348f7_Out_3_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3);
        Bindings_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3;
        _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3.WorldSpaceNormal = IN.WorldSpaceNormal;
        _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3_Specular_1_Vector3;
        SG_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float(_BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_ac10139ecedb4301b4595fa5b13c00b8_Out_0_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3, _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3, true, _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3, _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3_Specular_1_Vector3);
        float3 _Multiply_7b7e19c594534a458849928b4dc06aec_Out_2_Vector3;
        Unity_Multiply_float3_float3((_DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float.xxx), _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3_Specular_1_Vector3, _Multiply_7b7e19c594534a458849928b4dc06aec_Out_2_Vector3);
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e;
        _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Direction_1_Vector3;
        half3 _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Color_2_Vector3;
        half _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(_MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e, _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Direction_1_Vector3, _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Color_2_Vector3, _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_ShadowAtten_3_Float);
        float3 _Multiply_9545e2014858428792b92c57dd77e45c_Out_2_Vector3;
        Unity_Multiply_float3_float3(_MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Color_2_Vector3, (_MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_ShadowAtten_3_Float.xxx), _Multiply_9545e2014858428792b92c57dd77e45c_Out_2_Vector3);
        float _Property_be391ee1d2f24bada2da9fc9d603f6a9_Out_0_Float = _Smoothness;
        Bindings_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.WorldSpaceNormal = IN.WorldSpaceNormal;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.WorldSpacePosition = IN.WorldSpacePosition;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.NDCPosition = IN.NDCPosition;
        half _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Diffuse_1_Float;
        half3 _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Specular_2_Vector3;
        half3 _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Color_3_Vector3;
        SG_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float(_DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float, _Multiply_7b7e19c594534a458849928b4dc06aec_Out_2_Vector3, _Multiply_9545e2014858428792b92c57dd77e45c_Out_2_Vector3, _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_be391ee1d2f24bada2da9fc9d603f6a9_Out_0_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Diffuse_1_Float, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Specular_2_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Color_3_Vector3);
        float3 _Property_8804fec07c534721b9d4e6def9182fad_Out_0_Vector3 = _Base_Color;
        float3 _Multiply_765841b19beb4cecb786b3295b6aa8e9_Out_2_Vector3;
        Unity_Multiply_float3_float3((_AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Diffuse_1_Float.xxx), _Property_8804fec07c534721b9d4e6def9182fad_Out_0_Vector3, _Multiply_765841b19beb4cecb786b3295b6aa8e9_Out_2_Vector3);
        float3 _Add_77980781d2204e1b8d249e5f7058e9fb_Out_2_Vector3;
        Unity_Add_float3(_Multiply_765841b19beb4cecb786b3295b6aa8e9_Out_2_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Specular_2_Vector3, _Add_77980781d2204e1b8d249e5f7058e9fb_Out_2_Vector3);
        float3 _Multiply_9df1a4a5bf654e97b945b2eccf2fc855_Out_2_Vector3;
        Unity_Multiply_float3_float3(_Add_77980781d2204e1b8d249e5f7058e9fb_Out_2_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Color_3_Vector3, _Multiply_9df1a4a5bf654e97b945b2eccf2fc855_Out_2_Vector3);
        float3 _Property_e9725e93976b4fcaa5fea397628348dd_Out_0_Vector3 = _Base_Color;
        float _Property_e926bef11147490b98b69d5bec06eaa9_Out_0_Float = _Metallic;
        float _Property_1465a416fe734e4e83f2401b9c4d3fdb_Out_0_Float = _Smoothness;
        float _Property_0a056a259612407e813453c548affc50_Out_0_Float = _Ambient_Occlusion;
        Bindings_AmbientURP_300875fdd653fe340b08ad1547984cf1_float _AmbientURP_46e1712500da4aae848bd5b24a05f29f;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.WorldSpaceNormal = IN.WorldSpaceNormal;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.WorldSpacePosition = IN.WorldSpacePosition;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.NDCPosition = IN.NDCPosition;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.PixelPosition = IN.PixelPosition;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.uv1 = IN.uv1;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.uv2 = IN.uv2;
        half3 _AmbientURP_46e1712500da4aae848bd5b24a05f29f_Ambient_1_Vector3;
        half _AmbientURP_46e1712500da4aae848bd5b24a05f29f_DirectAO_2_Float;
        SG_AmbientURP_300875fdd653fe340b08ad1547984cf1_float(_Property_e9725e93976b4fcaa5fea397628348dd_Out_0_Vector3, _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_e926bef11147490b98b69d5bec06eaa9_Out_0_Float, _Property_1465a416fe734e4e83f2401b9c4d3fdb_Out_0_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3, _Property_0a056a259612407e813453c548affc50_Out_0_Float, _AmbientURP_46e1712500da4aae848bd5b24a05f29f, _AmbientURP_46e1712500da4aae848bd5b24a05f29f_Ambient_1_Vector3, _AmbientURP_46e1712500da4aae848bd5b24a05f29f_DirectAO_2_Float);
        float3 _Add_ad0b3eccabf24dceb374546c3c332ad7_Out_2_Vector3;
        Unity_Add_float3(_Multiply_9df1a4a5bf654e97b945b2eccf2fc855_Out_2_Vector3, _AmbientURP_46e1712500da4aae848bd5b24a05f29f_Ambient_1_Vector3, _Add_ad0b3eccabf24dceb374546c3c332ad7_Out_2_Vector3);
        Bindings_Fog_286ae83400099a24bba6faf005588be7_float _Fog_f4025f6ca9e74f948bc7263ef71d324a;
        _Fog_f4025f6ca9e74f948bc7263ef71d324a.ObjectSpacePosition = IN.ObjectSpacePosition;
        half3 _Fog_f4025f6ca9e74f948bc7263ef71d324a_Out_1_Vector3;
        SG_Fog_286ae83400099a24bba6faf005588be7_float(_Add_ad0b3eccabf24dceb374546c3c332ad7_Out_2_Vector3, _Fog_f4025f6ca9e74f948bc7263ef71d324a, _Fog_f4025f6ca9e74f948bc7263ef71d324a_Out_1_Vector3);
        float3 _Saturate_b798de6279164a2b9155cd8251d26092_Out_1_Vector3;
        Unity_Saturate_float3(_Fog_f4025f6ca9e74f948bc7263ef71d324a_Out_1_Vector3, _Saturate_b798de6279164a2b9155cd8251d26092_Out_1_Vector3);
        float3 _Saturation_ad4a69e303864b54940190442ea5d857_Out_2_Vector3;
        Unity_Saturation_float(_Saturate_b798de6279164a2b9155cd8251d26092_Out_1_Vector3, float(0), _Saturation_ad4a69e303864b54940190442ea5d857_Out_2_Vector3);
        float _Swizzle_bae47f72b28a4941b7665012b9c55203_Out_1_Float = (_Saturation_ad4a69e303864b54940190442ea5d857_Out_2_Vector3).x.x;
        float _Power_83030291203f43f9b048356e4fbaf99e_Out_2_Float;
        Unity_Power_float(_Swizzle_bae47f72b28a4941b7665012b9c55203_Out_1_Float, float(0.45), _Power_83030291203f43f9b048356e4fbaf99e_Out_2_Float);
        float3 _Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3;
        Unity_Contrast_float((_Power_83030291203f43f9b048356e4fbaf99e_Out_2_Float.xxx), float(2), _Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3);
        float2 _Property_7fc6298ec06c474b99191c5a5156da72_Out_0_Vector2 = _Color_A_Location;
        float3 _Remap_6d4565d12b3a4f71b23e18189b7389b3_Out_3_Vector3;
        Unity_Remap_float3(_Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3, _Property_7fc6298ec06c474b99191c5a5156da72_Out_0_Vector2, float2 (0, 1), _Remap_6d4565d12b3a4f71b23e18189b7389b3_Out_3_Vector3);
        float3 _Saturate_2728e5e4642645239b2f9a703cc1622a_Out_1_Vector3;
        Unity_Saturate_float3(_Remap_6d4565d12b3a4f71b23e18189b7389b3_Out_3_Vector3, _Saturate_2728e5e4642645239b2f9a703cc1622a_Out_1_Vector3);
        float3 _Lerp_20eaa2d06da4491384cac8b19ba49f2c_Out_3_Vector3;
        Unity_Lerp_float3((_Property_bedee92e97ce4b2abff5524ce019b2a8_Out_0_Vector4.xyz), (_Property_c648ff794fd34283beff09e33d8293fc_Out_0_Vector4.xyz), _Saturate_2728e5e4642645239b2f9a703cc1622a_Out_1_Vector3, _Lerp_20eaa2d06da4491384cac8b19ba49f2c_Out_3_Vector3);
        float4 _Property_fd1357d1b32e4a29a4cbcc3613f244e3_Out_0_Vector4 = _Color_C;
        float2 _Property_c2df701a69af4187a31c6f8cfcb26846_Out_0_Vector2 = _Color_B_Location;
        float3 _Remap_0c80541bb2da437698e33d379210a687_Out_3_Vector3;
        Unity_Remap_float3(_Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3, _Property_c2df701a69af4187a31c6f8cfcb26846_Out_0_Vector2, float2 (0, 1), _Remap_0c80541bb2da437698e33d379210a687_Out_3_Vector3);
        float3 _Saturate_5b42e55c7d0c45f1a92ce0c1c045003a_Out_1_Vector3;
        Unity_Saturate_float3(_Remap_0c80541bb2da437698e33d379210a687_Out_3_Vector3, _Saturate_5b42e55c7d0c45f1a92ce0c1c045003a_Out_1_Vector3);
        float3 _Lerp_b1cbf1c8cc0549a69912223d40ba4cfa_Out_3_Vector3;
        Unity_Lerp_float3(_Lerp_20eaa2d06da4491384cac8b19ba49f2c_Out_3_Vector3, (_Property_fd1357d1b32e4a29a4cbcc3613f244e3_Out_0_Vector4.xyz), _Saturate_5b42e55c7d0c45f1a92ce0c1c045003a_Out_1_Vector3, _Lerp_b1cbf1c8cc0549a69912223d40ba4cfa_Out_3_Vector3);
        Lit_1 = _Lerp_b1cbf1c8cc0549a69912223d40ba4cfa_Out_3_Vector3;
        }
        
        // unity-custom-func-begin
        void DebugMaterialSwitch_float(float3 None, float3 Albedo, float3 Specular, float3 Alpha, float3 Smoothness, float3 AmbientOcclusion, float3 Emission, float3 NormalWS, float3 NormalTS, float3 LightComplexity, float3 Metallic, float3 SpriteMask, float3 RenderingLayerMasks, out float3 Out){
        Out = None;
        #if !defined(SHADERGRAPH_PREVIEW) && defined(DEBUG_DISPLAY)
        [branch] switch(int(_DebugMaterialMode))
        
        {
        
            case 0:
        
                Out = None; break;
        
            case 1:
        
                Out = Albedo; break;
        
            case 2:
        
                Out = Specular; break;
            case 3:
        
                Out = Alpha; break;
            case 4:
        
                Out = Smoothness; break;
            case 5:
        
                Out = AmbientOcclusion;  break;
            case 6:
        
                Out = Emission;  break;
            case 7:
        
                Out = NormalWS * 0.5 + 0.5;  break;
            case 8:
        
                Out = NormalTS * 0.5 + 0.5;  break;
            case 9:
        
                Out = LightComplexity;  break;
            case 10:
        
                Out = Metallic;  break;
            case 11:
        
                Out = SpriteMask;  break;
            case 12:
        
                Out = RenderingLayerMasks;  break;
        
            default:
        
                Out = None; break;
        
        }
        #endif
        
        // Disable this define to prevent the global unlit
        // fragment pass to override the color output again.
        #undef DEBUG_DISPLAY
        }
        // unity-custom-func-end
        
        struct Bindings_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        };
        
        void SG_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float(float3 _In, float3 _Base_Color, float3 _Normal, float _Metallic, float _Smoothness, float3 _Emission, float _Ambient_Occlusion, float _Alpha, Bindings_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float IN, out float3 Out_1)
        {
        float3 _Property_dd011cc96ae64d1181317986b1fa1742_Out_0_Vector3 = _In;
        float3 _Property_5653941ce5a641f18f7ce7012652025d_Out_0_Vector3 = _Base_Color;
        float _Property_45f5c13ff5544581bd61c2442cecd0a1_Out_0_Float = _Alpha;
        float _Property_b6c8b448c5324bd3bc59540f628e43a3_Out_0_Float = _Smoothness;
        Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5;
        _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5.NDCPosition = IN.NDCPosition;
        half _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_indirectAO_1_Float;
        half _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_directAO_2_Float;
        SG_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float(_ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5, _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_indirectAO_1_Float, _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_directAO_2_Float);
        float _Property_441143660ff642349088dd1bcab6bc78_Out_0_Float = _Ambient_Occlusion;
        float _Minimum_8ee95b9bf7ac4776b6ee4edf1214b3c1_Out_2_Float;
        Unity_Minimum_float(_ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_indirectAO_1_Float, _Property_441143660ff642349088dd1bcab6bc78_Out_0_Float, _Minimum_8ee95b9bf7ac4776b6ee4edf1214b3c1_Out_2_Float);
        float3 _Property_b171431b5a3b4b0a9fc9fdede4a532a7_Out_0_Vector3 = _Emission;
        float3 _Property_db9eb36ed51d4aad95e383920b55e3d7_Out_0_Vector3 = _Normal;
        float3 _Transform_9e831bda1f4d4495b24f1f6e3075f2fb_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_9e831bda1f4d4495b24f1f6e3075f2fb_Out_1_Vector3 = TransformTangentToWorld(_Property_db9eb36ed51d4aad95e383920b55e3d7_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float3 _Property_4eaab22b2b784aeda3752622f7abaf85_Out_0_Vector3 = _Normal;
        float4 _ScreenPosition_121436dfdd464829910775b2326b046b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float3 _Property_1b1e0a48277e4883afeb1289a075c5d8_Out_0_Vector3 = _Base_Color;
        float3 _LightingComplexityCustomFunction_cbe0c0f96f9046a584e17ead8c001a55_Out_3_Vector3;
        LightingComplexity_float((_ScreenPosition_121436dfdd464829910775b2326b046b_Out_0_Vector4.xy), IN.WorldSpacePosition, _Property_1b1e0a48277e4883afeb1289a075c5d8_Out_0_Vector3, _LightingComplexityCustomFunction_cbe0c0f96f9046a584e17ead8c001a55_Out_3_Vector3);
        float _Property_dcd3ca7796af45c6857884fa7979898b_Out_0_Float = _Metallic;
        float3 _DebugMaterialSwitchCustomFunction_e1fabc2a3bcd4bc183f0e93c379657d4_Out_5_Vector3;
        DebugMaterialSwitch_float(_Property_dd011cc96ae64d1181317986b1fa1742_Out_0_Vector3, _Property_5653941ce5a641f18f7ce7012652025d_Out_0_Vector3, float3 (0, 0, 0), (_Property_45f5c13ff5544581bd61c2442cecd0a1_Out_0_Float.xxx), (_Property_b6c8b448c5324bd3bc59540f628e43a3_Out_0_Float.xxx), (_Minimum_8ee95b9bf7ac4776b6ee4edf1214b3c1_Out_2_Float.xxx), _Property_b171431b5a3b4b0a9fc9fdede4a532a7_Out_0_Vector3, _Transform_9e831bda1f4d4495b24f1f6e3075f2fb_Out_1_Vector3, _Property_4eaab22b2b784aeda3752622f7abaf85_Out_0_Vector3, _LightingComplexityCustomFunction_cbe0c0f96f9046a584e17ead8c001a55_Out_3_Vector3, (_Property_dcd3ca7796af45c6857884fa7979898b_Out_0_Float.xxx), float3 (0, 0, 0), float3 (0, 0, 0), _DebugMaterialSwitchCustomFunction_e1fabc2a3bcd4bc183f0e93c379657d4_Out_5_Vector3);
        Out_1 = _DebugMaterialSwitchCustomFunction_e1fabc2a3bcd4bc183f0e93c379657d4_Out_5_Vector3;
        }
        
        struct Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpaceViewDirection;
        float3 ObjectSpacePosition;
        float3 WorldSpacePosition;
        float3 AbsoluteWorldSpacePosition;
        float2 NDCPosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float(float3 _Base_Color, float3 _Normal, float _Metallic, float _Smoothness, float3 _Emission, float _AmbientOcclusion, float _Alpha, float4 _Color_A, float2 _Color_A_Location, float4 _Color_B, float2 _Color_B_Location, float4 _Color_C, Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float IN, out float3 Lit_1)
        {
        float3 _Property_04a055764411443d802bfbbd0d510c65_Out_0_Vector3 = _Base_Color;
        float3 _Property_383a017d83a8420dac016260bc833f58_Out_0_Vector3 = _Normal;
        float3 _Transform_3f94cf9dbe844abc9b6727d5d289074f_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_3f94cf9dbe844abc9b6727d5d289074f_Out_1_Vector3 = TransformTangentToWorld(_Property_383a017d83a8420dac016260bc833f58_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float _Property_11295d868ff34c388c9212b90b781aff_Out_0_Float = _Metallic;
        float _Property_b522b61b85ff4ecbb0eb63cff689f5cb_Out_0_Float = _Smoothness;
        float _Property_a1dc37a47c5640d0870861199df0bd70_Out_0_Float = _AmbientOcclusion;
        Bindings_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float _ApplyDecals_0413903f5da5491d911d117142eabddd;
        _ApplyDecals_0413903f5da5491d911d117142eabddd.PixelPosition = IN.PixelPosition;
        half3 _ApplyDecals_0413903f5da5491d911d117142eabddd_BaseColor_1_Vector3;
        half3 _ApplyDecals_0413903f5da5491d911d117142eabddd_SpecularColor_2_Vector3;
        half3 _ApplyDecals_0413903f5da5491d911d117142eabddd_NormalWS_3_Vector3;
        half _ApplyDecals_0413903f5da5491d911d117142eabddd_Metallic_4_Float;
        half _ApplyDecals_0413903f5da5491d911d117142eabddd_Smoothness_6_Float;
        half _ApplyDecals_0413903f5da5491d911d117142eabddd_AmbientOcclusion_5_Float;
        SG_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float(_Property_04a055764411443d802bfbbd0d510c65_Out_0_Vector3, _Transform_3f94cf9dbe844abc9b6727d5d289074f_Out_1_Vector3, _Property_11295d868ff34c388c9212b90b781aff_Out_0_Float, _Property_b522b61b85ff4ecbb0eb63cff689f5cb_Out_0_Float, _Property_a1dc37a47c5640d0870861199df0bd70_Out_0_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd, _ApplyDecals_0413903f5da5491d911d117142eabddd_BaseColor_1_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_SpecularColor_2_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_NormalWS_3_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_Metallic_4_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd_Smoothness_6_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd_AmbientOcclusion_5_Float);
        float3 _Property_b986326ad9b34d6ea3a7237ba2bd1cd6_Out_0_Vector3 = _Emission;
        Bindings_DebugLighting_61e571d2b9ede1240a524a849d20c997_float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpaceNormal = IN.WorldSpaceNormal;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpaceTangent = IN.WorldSpaceTangent;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpacePosition = IN.WorldSpacePosition;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.PixelPosition = IN.PixelPosition;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.uv1 = IN.uv1;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.uv2 = IN.uv2;
        float3 _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3;
        float3 _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3;
        float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float;
        float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float;
        float3 _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3;
        float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float;
        SG_DebugLighting_61e571d2b9ede1240a524a849d20c997_float(_ApplyDecals_0413903f5da5491d911d117142eabddd_BaseColor_1_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_NormalWS_3_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_Metallic_4_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd_Smoothness_6_Float, _Property_b986326ad9b34d6ea3a7237ba2bd1cd6_Out_0_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_AmbientOcclusion_5_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float);
        float4 _Property_a2c93b67d7e14184996710181bc8106a_Out_0_Vector4 = _Color_A;
        float2 _Property_30bd0eecac2f497db8c8b272e8e7d3e5_Out_0_Vector2 = _Color_A_Location;
        float4 _Property_47fc9a397b1241599709d29487238203_Out_0_Vector4 = _Color_B;
        float2 _Property_99bf1a52083e4b7f84197e960ed6a728_Out_0_Vector2 = _Color_B_Location;
        float4 _Property_d5f33cf319a54be08f26ec7c7538d6a4_Out_0_Vector4 = _Color_C;
        Bindings_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceNormal = IN.WorldSpaceNormal;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceTangent = IN.WorldSpaceTangent;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.ObjectSpacePosition = IN.ObjectSpacePosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpacePosition = IN.WorldSpacePosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.NDCPosition = IN.NDCPosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.PixelPosition = IN.PixelPosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.uv1 = IN.uv1;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.uv2 = IN.uv2;
        half3 _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc_Lit_1_Vector3;
        SG_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float(_DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3, true, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float, half(1), _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float, _Property_a2c93b67d7e14184996710181bc8106a_Out_0_Vector4, _Property_30bd0eecac2f497db8c8b272e8e7d3e5_Out_0_Vector2, _Property_47fc9a397b1241599709d29487238203_Out_0_Vector4, _Property_99bf1a52083e4b7f84197e960ed6a728_Out_0_Vector2, _Property_d5f33cf319a54be08f26ec7c7538d6a4_Out_0_Vector4, _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc, _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc_Lit_1_Vector3);
        float3 _Add_a11e24e7d4fd4494895fd67f375acb21_Out_2_Vector3;
        Unity_Add_float3(_SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc_Lit_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3, _Add_a11e24e7d4fd4494895fd67f375acb21_Out_2_Vector3);
        float _Property_d5e8251fc84a46aea1765511445b653e_Out_0_Float = _Alpha;
        Bindings_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpaceNormal = IN.WorldSpaceNormal;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpaceTangent = IN.WorldSpaceTangent;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpacePosition = IN.WorldSpacePosition;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.NDCPosition = IN.NDCPosition;
        float3 _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3_Out_1_Vector3;
        SG_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float(_Add_a11e24e7d4fd4494895fd67f375acb21_Out_2_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float, _Property_d5e8251fc84a46aea1765511445b653e_Out_0_Float, _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3, _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3_Out_1_Vector3);
        Lit_1 = _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3_Out_1_Vector3;
        }
        
        void Unity_Blend_Overwrite_float3(float3 Base, float3 Blend, out float3 Out, float Opacity)
        {
            Out = lerp(Base, Blend, Opacity);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float3 BaseColor;
            float3 NormalTS;
            float3 Emission;
            float Metallic;
            float3 Specular;
            float Smoothness;
            float Occlusion;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_debbfbf1a581455dbc61b338f851a8d4_Out_0_Boolean = _DisableAllGradients;
            float _Property_2fb138ca7c89409da2d3e517c9bcb36b_Out_0_Boolean = _DisableGradientMap;
            UnityTexture2D _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D = UnityBuildTexture2DStructInternal(_BaseColor, sampler_BaseColor, _BaseColor_TexelSize, float4(1, 1, 0, 0), float4(0, 0, 0, 0));
            float2 _Property_94094af0818841fea1396a1ccae7a167_Out_0_Vector2 = _Tilling;
            float2 _TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2;
            Unity_TilingAndOffset_float(IN.uv0.xy, _Property_94094af0818841fea1396a1ccae7a167_Out_0_Vector2, float2 (0, 0), _TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2);
            float4 _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.tex, _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.samplerstate, _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.GetTransformedUV(_TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2) );
            if (_Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.hdrDecode.x > 0)
                _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4, _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.hdrDecode);
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.r;
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_G_5_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.g;
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_B_6_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.b;
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_A_7_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.a;
            float4 _Property_de1bd6122d6c4aa4ba9d7692e6db8956_Out_0_Vector4 = _Color1;
            float4 _Property_fceba521f35a4cc88bbd9602ff68242f_Out_0_Vector4 = _Color2;
            float2 _Property_e60b8198308f4aad8dfa7a52168790ce_Out_0_Vector2 = _Color_1_Location;
            float _Remap_b9f81469352045d7ae4bb207f0c202fc_Out_3_Float;
            Unity_Remap_float(_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float, _Property_e60b8198308f4aad8dfa7a52168790ce_Out_0_Vector2, float2 (0, 1), _Remap_b9f81469352045d7ae4bb207f0c202fc_Out_3_Float);
            float _Saturate_473bb6d59e074412ad7162adcfe4cef1_Out_1_Float;
            Unity_Saturate_float(_Remap_b9f81469352045d7ae4bb207f0c202fc_Out_3_Float, _Saturate_473bb6d59e074412ad7162adcfe4cef1_Out_1_Float);
            float4 _Lerp_f3e294dc316840b6b4294bcb3e7b3f7b_Out_3_Vector4;
            Unity_Lerp_float4(_Property_de1bd6122d6c4aa4ba9d7692e6db8956_Out_0_Vector4, _Property_fceba521f35a4cc88bbd9602ff68242f_Out_0_Vector4, (_Saturate_473bb6d59e074412ad7162adcfe4cef1_Out_1_Float.xxxx), _Lerp_f3e294dc316840b6b4294bcb3e7b3f7b_Out_3_Vector4);
            float4 _Property_60ebe0bb4a0e4a6fbd758422fbc8e1af_Out_0_Vector4 = _Color3;
            float2 _Property_46eb955dacc0426abc5d73b1be33af42_Out_0_Vector2 = _Color_2_Location;
            float _Remap_c2e999a017a6490b91665566afc5916d_Out_3_Float;
            Unity_Remap_float(_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float, _Property_46eb955dacc0426abc5d73b1be33af42_Out_0_Vector2, float2 (0, 1), _Remap_c2e999a017a6490b91665566afc5916d_Out_3_Float);
            float _Saturate_72fb61dd2f064241a5337c41de422b36_Out_1_Float;
            Unity_Saturate_float(_Remap_c2e999a017a6490b91665566afc5916d_Out_3_Float, _Saturate_72fb61dd2f064241a5337c41de422b36_Out_1_Float);
            float4 _Lerp_72f03acdde2245b29f8cd4a47d6b4cd6_Out_3_Vector4;
            Unity_Lerp_float4(_Lerp_f3e294dc316840b6b4294bcb3e7b3f7b_Out_3_Vector4, _Property_60ebe0bb4a0e4a6fbd758422fbc8e1af_Out_0_Vector4, (_Saturate_72fb61dd2f064241a5337c41de422b36_Out_1_Float.xxxx), _Lerp_72f03acdde2245b29f8cd4a47d6b4cd6_Out_3_Vector4);
            float4 _Property_e84e577bc7db46749ec6367493b51e06_Out_0_Vector4 = _AOColor;
            UnityTexture2D _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D = UnityBuildTexture2DStructInternal(_ORM, sampler_ORM, _ORM_TexelSize, float4(1, 1, 0, 0), float4(0, 0, 0, 0));
            float4 _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.tex, _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.samplerstate, _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.GetTransformedUV(_TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2) );
            if (_Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.hdrDecode.x > 0)
                _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4, _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.hdrDecode);
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_R_4_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.r;
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_G_5_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.g;
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.b;
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_A_7_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.a;
            float _OneMinus_89dcf9413d9142c481cf9d5e5035b712_Out_1_Float;
            Unity_OneMinus_float(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_R_4_Float, _OneMinus_89dcf9413d9142c481cf9d5e5035b712_Out_1_Float);
            float2 _Property_753b87b3c47d403696efc934ea3dbea9_Out_0_Vector2 = _AOLevels;
            float _Remap_983e82311f6e4fe5b382c71d656497ec_Out_3_Float;
            Unity_Remap_float(_OneMinus_89dcf9413d9142c481cf9d5e5035b712_Out_1_Float, _Property_753b87b3c47d403696efc934ea3dbea9_Out_0_Vector2, float2 (1, 0), _Remap_983e82311f6e4fe5b382c71d656497ec_Out_3_Float);
            float _Property_ba1b5b1e95414eaebf54a9d26291e91f_Out_0_Float = _AOIntensity;
            float _Multiply_2341d6d389c44e96bba10c0c1e0c9f14_Out_2_Float;
            Unity_Multiply_float_float(_Remap_983e82311f6e4fe5b382c71d656497ec_Out_3_Float, _Property_ba1b5b1e95414eaebf54a9d26291e91f_Out_0_Float, _Multiply_2341d6d389c44e96bba10c0c1e0c9f14_Out_2_Float);
            float4 _Lerp_8d7f6575739049459e47aaca25cf59d0_Out_3_Vector4;
            Unity_Lerp_float4(_Lerp_72f03acdde2245b29f8cd4a47d6b4cd6_Out_3_Vector4, _Property_e84e577bc7db46749ec6367493b51e06_Out_0_Vector4, (_Multiply_2341d6d389c44e96bba10c0c1e0c9f14_Out_2_Float.xxxx), _Lerp_8d7f6575739049459e47aaca25cf59d0_Out_3_Vector4);
            float4 _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4;
            Unity_Branch_float4(_Property_2fb138ca7c89409da2d3e517c9bcb36b_Out_0_Boolean, (_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float.xxxx), _Lerp_8d7f6575739049459e47aaca25cf59d0_Out_3_Vector4, _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4);
            float _Property_18ebff8ae81646538dc78b9020d39b78_Out_0_Boolean = _Z_Gradient;
            float _Property_f94879ac790e4c109a34ec1f73a3c3a6_Out_0_Boolean = _Y_Gradient;
            float _Property_c61e8f7030cd47ac9b641cd98ca92fe3_Out_0_Boolean = _X_Gradient;
            float4 _Property_1f3e0b1c283f4c949004f43f37fe1a90_Out_0_Vector4 = _X_GradientColor;
            float _Property_94b16f357eac4058809921fa96f34787_Out_0_Float = _X_GradientStartPosition;
            float _Property_8c3dacdcc9cc4b2880b33b1d1913e9f1_Out_0_Float = _X_GradientEndPosition;
            float _Split_2ca42922e29b49b4b7113632901be932_R_1_Float = IN.AbsoluteWorldSpacePosition[0];
            float _Split_2ca42922e29b49b4b7113632901be932_G_2_Float = IN.AbsoluteWorldSpacePosition[1];
            float _Split_2ca42922e29b49b4b7113632901be932_B_3_Float = IN.AbsoluteWorldSpacePosition[2];
            float _Split_2ca42922e29b49b4b7113632901be932_A_4_Float = 0;
            float _Smoothstep_c73aebae28cd43fc896795bd80911919_Out_3_Float;
            Unity_Smoothstep_float(_Property_94b16f357eac4058809921fa96f34787_Out_0_Float, _Property_8c3dacdcc9cc4b2880b33b1d1913e9f1_Out_0_Float, _Split_2ca42922e29b49b4b7113632901be932_R_1_Float, _Smoothstep_c73aebae28cd43fc896795bd80911919_Out_3_Float);
            float _Property_dd9c997bc2514f2bbe579af0da9fecb2_Out_0_Float = _X_GradientIntensity;
            float _Multiply_961d468ad0db4c8ebb883194aa2c7cf5_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_c73aebae28cd43fc896795bd80911919_Out_3_Float, _Property_dd9c997bc2514f2bbe579af0da9fecb2_Out_0_Float, _Multiply_961d468ad0db4c8ebb883194aa2c7cf5_Out_2_Float);
            float4 _Lerp_ba89b24cd32049a0964667d354c52dcc_Out_3_Vector4;
            Unity_Lerp_float4(_Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4, _Property_1f3e0b1c283f4c949004f43f37fe1a90_Out_0_Vector4, (_Multiply_961d468ad0db4c8ebb883194aa2c7cf5_Out_2_Float.xxxx), _Lerp_ba89b24cd32049a0964667d354c52dcc_Out_3_Vector4);
            float4 _Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4;
            Unity_Branch_float4(_Property_c61e8f7030cd47ac9b641cd98ca92fe3_Out_0_Boolean, _Lerp_ba89b24cd32049a0964667d354c52dcc_Out_3_Vector4, _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4, _Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4);
            float4 _Property_bbb56023a68b4c17bbaa88d52a24ab64_Out_0_Vector4 = _Y_GradientColor;
            float _Property_e53d4551d24740e89ab9d2dde9d07fa9_Out_0_Float = _Y_GradientStartPosition;
            float _Property_f705a7a1ffdc47a388bfa2aa340dd71f_Out_0_Float = _Y_GradientEndPosition;
            float _Smoothstep_8f0d8c12283b443dabc9178711fca6ea_Out_3_Float;
            Unity_Smoothstep_float(_Property_e53d4551d24740e89ab9d2dde9d07fa9_Out_0_Float, _Property_f705a7a1ffdc47a388bfa2aa340dd71f_Out_0_Float, _Split_2ca42922e29b49b4b7113632901be932_G_2_Float, _Smoothstep_8f0d8c12283b443dabc9178711fca6ea_Out_3_Float);
            float _Property_c3a47369d5bb47ddb5fcb74791e32d8d_Out_0_Float = _Y_GradientIntensity;
            float _Multiply_0b75652c94cc4e928c2a6eda4bc65145_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_8f0d8c12283b443dabc9178711fca6ea_Out_3_Float, _Property_c3a47369d5bb47ddb5fcb74791e32d8d_Out_0_Float, _Multiply_0b75652c94cc4e928c2a6eda4bc65145_Out_2_Float);
            float4 _Lerp_2023214161dd495a9df4a86e9aab0a55_Out_3_Vector4;
            Unity_Lerp_float4(_Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4, _Property_bbb56023a68b4c17bbaa88d52a24ab64_Out_0_Vector4, (_Multiply_0b75652c94cc4e928c2a6eda4bc65145_Out_2_Float.xxxx), _Lerp_2023214161dd495a9df4a86e9aab0a55_Out_3_Vector4);
            float4 _Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4;
            Unity_Branch_float4(_Property_f94879ac790e4c109a34ec1f73a3c3a6_Out_0_Boolean, _Lerp_2023214161dd495a9df4a86e9aab0a55_Out_3_Vector4, _Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4, _Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4);
            float4 _Property_d20a45cfc0cb48e4a9823f3125a60fb7_Out_0_Vector4 = _Y_GradientColor;
            float _Property_a3a6fd6a552845c797564be4a2b63e5d_Out_0_Float = _Z_GradientStartPosition;
            float _Property_1f6239cf046944d6b4f70da4fca83661_Out_0_Float = _Z_GradientEndPosition;
            float _Smoothstep_9cb470a94c1b4410b6282b2c261d462d_Out_3_Float;
            Unity_Smoothstep_float(_Property_a3a6fd6a552845c797564be4a2b63e5d_Out_0_Float, _Property_1f6239cf046944d6b4f70da4fca83661_Out_0_Float, _Split_2ca42922e29b49b4b7113632901be932_B_3_Float, _Smoothstep_9cb470a94c1b4410b6282b2c261d462d_Out_3_Float);
            float _Property_1ac1a7f19f6342f0a029129f1adbfd67_Out_0_Float = _Z_GradientIntensity;
            float _Multiply_d258a35ceccb4063a2594879737f8bb1_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_9cb470a94c1b4410b6282b2c261d462d_Out_3_Float, _Property_1ac1a7f19f6342f0a029129f1adbfd67_Out_0_Float, _Multiply_d258a35ceccb4063a2594879737f8bb1_Out_2_Float);
            float4 _Lerp_5d732be3cb674212b3307c0a98052b15_Out_3_Vector4;
            Unity_Lerp_float4(_Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4, _Property_d20a45cfc0cb48e4a9823f3125a60fb7_Out_0_Vector4, (_Multiply_d258a35ceccb4063a2594879737f8bb1_Out_2_Float.xxxx), _Lerp_5d732be3cb674212b3307c0a98052b15_Out_3_Vector4);
            float4 _Branch_808c9eecf2bc4ac09cea8d7293c4d22a_Out_3_Vector4;
            Unity_Branch_float4(_Property_18ebff8ae81646538dc78b9020d39b78_Out_0_Boolean, _Lerp_5d732be3cb674212b3307c0a98052b15_Out_3_Vector4, _Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4, _Branch_808c9eecf2bc4ac09cea8d7293c4d22a_Out_3_Vector4);
            float4 _Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4;
            Unity_Branch_float4(_Property_debbfbf1a581455dbc61b338f851a8d4_Out_0_Boolean, _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4, _Branch_808c9eecf2bc4ac09cea8d7293c4d22a_Out_3_Vector4, _Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4);
            UnityTexture2D _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D = UnityBuildTexture2DStructInternal(_Normal, sampler_Normal, _Normal_TexelSize, float4(1, 1, 0, 0), float4(0, 0, 0, 0));
            float4 _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.tex, _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.samplerstate, _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.GetTransformedUV(_TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2) );
            if (_Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.hdrDecode.x > 0)
                _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4, _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.hdrDecode);
            _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.rgb = UnpackNormal(_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4);
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_R_4_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.r;
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_G_5_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.g;
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_B_6_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.b;
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_A_7_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.a;
            float _Property_3c169350af004240a8c8543dce8c320b_Out_0_Boolean = _UseCustomRoughness;
            float _Property_5c2d908353e24a3692fb0e08fe229355_Out_0_Float = _CustomRoughness;
            float _OneMinus_59ec04ea5505409baa7bf818ec2e91cd_Out_1_Float;
            Unity_OneMinus_float(_Property_5c2d908353e24a3692fb0e08fe229355_Out_0_Float, _OneMinus_59ec04ea5505409baa7bf818ec2e91cd_Out_1_Float);
            float _Property_a19c0c9a848947f4aab57660a9a18f93_Out_0_Float = _RoughnessMultiplier;
            float _Multiply_17be8d0efec54ca3bc59e321b0c596ee_Out_2_Float;
            Unity_Multiply_float_float(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_G_5_Float, _Property_a19c0c9a848947f4aab57660a9a18f93_Out_0_Float, _Multiply_17be8d0efec54ca3bc59e321b0c596ee_Out_2_Float);
            float _Saturate_04d0090531f74a9ebdada50fd6f42af5_Out_1_Float;
            Unity_Saturate_float(_Multiply_17be8d0efec54ca3bc59e321b0c596ee_Out_2_Float, _Saturate_04d0090531f74a9ebdada50fd6f42af5_Out_1_Float);
            float _OneMinus_c9932877d03347048da7ba94008426d5_Out_1_Float;
            Unity_OneMinus_float(_Saturate_04d0090531f74a9ebdada50fd6f42af5_Out_1_Float, _OneMinus_c9932877d03347048da7ba94008426d5_Out_1_Float);
            float _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float;
            Unity_Branch_float(_Property_3c169350af004240a8c8543dce8c320b_Out_0_Boolean, _OneMinus_59ec04ea5505409baa7bf818ec2e91cd_Out_1_Float, _OneMinus_c9932877d03347048da7ba94008426d5_Out_1_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float);
            float _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float;
            Unity_Saturate_float(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_R_4_Float, _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float);
            float4 _Property_34dac06f3b7442efa3e05c85b09ec445_Out_0_Vector4 = _ACT1_Color_A;
            float2 _Property_b3a982240db24cfbbc14de979535b458_Out_0_Vector2 = _ACT1_Color_A_Location;
            float4 _Property_0e317fd277254f1aae079db6e8d2e8dc_Out_0_Vector4 = _ACT1_Color_B;
            float2 _Property_3187bf5304c54604a1d464e65a9dac03_Out_0_Vector2 = _ACT1_Color_B_Location;
            float4 _Property_41e2d595ca9d41dd8806c1a749a3bb43_Out_0_Vector4 = _ACT1_Color_C;
            Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceNormal = IN.WorldSpaceNormal;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceTangent = IN.WorldSpaceTangent;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.ObjectSpacePosition = IN.ObjectSpacePosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpacePosition = IN.WorldSpacePosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.NDCPosition = IN.NDCPosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.PixelPosition = IN.PixelPosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.uv1 = IN.uv1;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.uv2 = IN.uv2;
            float3 _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d_Lit_1_Vector3;
            SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), (_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.xyz), _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float, float3 (0, 0, 0), _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float, float(1), _Property_34dac06f3b7442efa3e05c85b09ec445_Out_0_Vector4, _Property_b3a982240db24cfbbc14de979535b458_Out_0_Vector2, _Property_0e317fd277254f1aae079db6e8d2e8dc_Out_0_Vector4, _Property_3187bf5304c54604a1d464e65a9dac03_Out_0_Vector2, _Property_41e2d595ca9d41dd8806c1a749a3bb43_Out_0_Vector4, _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d, _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d_Lit_1_Vector3);
            float4 _Property_259fe0b41799487eb44d557cc24932a4_Out_0_Vector4 = _ACT2_Color_A;
            float2 _Property_83a57d16b4dc4f48985395b92f2de589_Out_0_Vector2 = _ACT2_Color_A_Location;
            float4 _Property_8ca39a18030a4bbd9348cb5b458a8372_Out_0_Vector4 = _ACT2_Color_B;
            float2 _Property_fa2809814b4148bd8ece170a87d230ef_Out_0_Vector2 = _ACT2_Color_B_Location;
            float4 _Property_c83f37081cf64cc7999c1bb19926d7c1_Out_0_Vector4 = _ACT2_Color_C;
            Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceNormal = IN.WorldSpaceNormal;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceTangent = IN.WorldSpaceTangent;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.ObjectSpacePosition = IN.ObjectSpacePosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpacePosition = IN.WorldSpacePosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.NDCPosition = IN.NDCPosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.PixelPosition = IN.PixelPosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.uv1 = IN.uv1;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.uv2 = IN.uv2;
            float3 _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370_Lit_1_Vector3;
            SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), (_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.xyz), _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float, float3 (0, 0, 0), _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float, float(1), _Property_259fe0b41799487eb44d557cc24932a4_Out_0_Vector4, _Property_83a57d16b4dc4f48985395b92f2de589_Out_0_Vector2, _Property_8ca39a18030a4bbd9348cb5b458a8372_Out_0_Vector4, _Property_fa2809814b4148bd8ece170a87d230ef_Out_0_Vector2, _Property_c83f37081cf64cc7999c1bb19926d7c1_Out_0_Vector4, _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370, _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370_Lit_1_Vector3);
            float4 _Property_13886bd11bee4d40b3eb7be3dff1c022_Out_0_Vector4 = _ACT3_Color_A;
            float2 _Property_5578cc8231d143d9a5f34ae24f110091_Out_0_Vector2 = _ACT3_Color_A_Location;
            float4 _Property_4d39077496e94d728ca3d19d42d3bd68_Out_0_Vector4 = _ACT3_Color_B;
            float2 _Property_5b1c9e69733942519a853e51dd4770f6_Out_0_Vector2 = _ACT3_Color_B_Location;
            float4 _Property_6a5550ba5eec4966850efea3418c844a_Out_0_Vector4 = _ACT3_Color_C;
            Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceNormal = IN.WorldSpaceNormal;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceTangent = IN.WorldSpaceTangent;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.ObjectSpacePosition = IN.ObjectSpacePosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpacePosition = IN.WorldSpacePosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.NDCPosition = IN.NDCPosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.PixelPosition = IN.PixelPosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.uv1 = IN.uv1;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.uv2 = IN.uv2;
            float3 _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a_Lit_1_Vector3;
            SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), (_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.xyz), _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float, float3 (0, 0, 0), _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float, float(1), _Property_13886bd11bee4d40b3eb7be3dff1c022_Out_0_Vector4, _Property_5578cc8231d143d9a5f34ae24f110091_Out_0_Vector2, _Property_4d39077496e94d728ca3d19d42d3bd68_Out_0_Vector4, _Property_5b1c9e69733942519a853e51dd4770f6_Out_0_Vector2, _Property_6a5550ba5eec4966850efea3418c844a_Out_0_Vector4, _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a, _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a_Lit_1_Vector3);
            float3 _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3;
            if (_CURRENTACT_ACT_1) _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3 = _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d_Lit_1_Vector3;
            else if (_CURRENTACT_ACT_2) _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3 = _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370_Lit_1_Vector3;
            else _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3 = _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a_Lit_1_Vector3;
            float _Property_854b1929338847e9b4a11e77fedb361b_Out_0_Float = _LightingGradientMapInfluence;
            float3 _Blend_ed10fcb74d224507bd92f8d23fb7e2d9_Out_2_Vector3;
            Unity_Blend_Overwrite_float3((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3, _Blend_ed10fcb74d224507bd92f8d23fb7e2d9_Out_2_Vector3, _Property_854b1929338847e9b4a11e77fedb361b_Out_0_Float);
            surface.BaseColor = _Blend_ed10fcb74d224507bd92f8d23fb7e2d9_Out_2_Vector3;
            surface.NormalTS = (_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.xyz);
            surface.Emission = float3(0, 0, 0);
            surface.Metallic = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float;
            surface.Specular = IsGammaSpace() ? float3(0.5, 0.5, 0.5) : SRGBToLinear(float3(0.5, 0.5, 0.5));
            surface.Smoothness = _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float;
            surface.Occlusion = _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float;
            surface.Alpha = float(1);
            surface.AlphaClipThreshold = float(0.5);
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
            // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            float3 unnormalizedNormalWS = input.normalWS;
            const float renormFactor = 1.0 / length(unnormalizedNormalWS);
        
            // use bitangent on the fly like in hdrp
            // IMPORTANT! If we ever support Flip on double sided materials ensure bitangent and tangent are NOT flipped.
            float crossSign = (input.tangentWS.w > 0.0 ? 1.0 : -1.0)* GetOddNegativeScale();
            float3 bitang = crossSign * cross(input.normalWS.xyz, input.tangentWS.xyz);
        
            output.WorldSpaceNormal = renormFactor * input.normalWS.xyz;      // we want a unit length Normal Vector node in shader graph
            output.TangentSpaceNormal = float3(0.0f, 0.0f, 1.0f);
        
            // to pr               eserve mikktspace compliance we use same scale renormFactor as was used on the normal.
            // This                is explained in section 2.2 in "surface gradient based bump mapping framework"
            output.WorldSpaceTangent = renormFactor * input.tangentWS.xyz;
            output.WorldSpaceBiTangent = renormFactor * bitang;
        
            output.WorldSpaceViewDirection = GetWorldSpaceNormalizeViewDir(input.positionWS);
            output.WorldSpacePosition = input.positionWS;
            output.ObjectSpacePosition = TransformWorldToObject(input.positionWS);
            output.AbsoluteWorldSpacePosition = GetAbsolutePositionWS(input.positionWS);
        
            #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x < 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #else
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x > 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScaledScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 = input.texCoord0;
            output.uv1 = input.texCoord1;
            output.uv2 = input.texCoord2;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/PBRForwardPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "GBuffer"
            Tags
            {
                "LightMode" = "UniversalGBuffer"
            }
        
        // Render State
        Cull [_Cull]
        Blend [_SrcBlend] [_DstBlend], [_SrcBlendAlpha] [_DstBlendAlpha]
        ZTest [_ZTest]
        ZWrite [_ZWrite]
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 4.5
        #pragma exclude_renderers gles3 glcore
        #pragma multi_compile_instancing
        #pragma instancing_options renderinglayer
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        #pragma multi_compile_fragment _ _SCREEN_SPACE_IRRADIANCE
        #pragma multi_compile _ LIGHTMAP_ON
        #pragma multi_compile _ DYNAMICLIGHTMAP_ON
        #pragma multi_compile _ DIRLIGHTMAP_COMBINED
        #pragma multi_compile _ USE_LEGACY_LIGHTMAPS
        #pragma multi_compile _ LIGHTMAP_BICUBIC_SAMPLING
        #pragma multi_compile _ REFLECTION_PROBE_ROTATION
        #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
        #pragma multi_compile_fragment _ _REFLECTION_PROBE_BLENDING
        #pragma multi_compile_fragment _ _REFLECTION_PROBE_BOX_PROJECTION
        #pragma multi_compile_fragment _ _SHADOWS_SOFT _SHADOWS_SOFT_LOW _SHADOWS_SOFT_MEDIUM _SHADOWS_SOFT_HIGH
        #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
        #pragma multi_compile _ SHADOWS_SHADOWMASK
        #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
        #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
        #pragma multi_compile_fragment _ _GBUFFER_NORMALS_OCT
        #pragma multi_compile_fragment _ _RENDER_PASS_ENABLED
        #pragma multi_compile_fragment _ DEBUG_DISPLAY
        #pragma multi_compile _ _CLUSTER_LIGHT_LOOP
        #pragma shader_feature_fragment _ _SURFACE_TYPE_TRANSPARENT
        #pragma shader_feature_local_fragment _ _ALPHAPREMULTIPLY_ON
        #pragma shader_feature_local_fragment _ _ALPHAMODULATE_ON
        #pragma shader_feature_local_fragment _ _ALPHATEST_ON
        #pragma shader_feature_local_fragment _ _SPECULAR_SETUP
        #pragma shader_feature_local _ _RECEIVE_SHADOWS_OFF
        #pragma shader_feature _CURRENTACT_ACT_1 _CURRENTACT_ACT_2 _CURRENTACT_ACT_3
        
        
        
        // Defines
        
        #define _NORMALMAP 1
        #define _NORMAL_DROPOFF_TS 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define ATTRIBUTES_NEED_TEXCOORD1
        #define ATTRIBUTES_NEED_TEXCOORD2
        #define FEATURES_GRAPH_VERTEX_NORMAL_OUTPUT
        #define FEATURES_GRAPH_VERTEX_TANGENT_OUTPUT
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TANGENT_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define VARYINGS_NEED_TEXCOORD1
        #define VARYINGS_NEED_TEXCOORD2
        #define VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
        #define VARYINGS_NEED_SHADOW_COORD
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_GBUFFER
        #define _FOG_FRAGMENT 1
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Fog.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ProbeVolumeVariants.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 uv1 : TEXCOORD1;
             float4 uv2 : TEXCOORD2;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(ATTRIBUTES_NEED_INSTANCEID)
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
             float4 tangentWS;
             float4 texCoord0;
             float4 texCoord1;
             float4 texCoord2;
            #if defined(LIGHTMAP_ON)
             float2 staticLightmapUV;
            #endif
            #if defined(DYNAMICLIGHTMAP_ON)
             float2 dynamicLightmapUV;
            #endif
            #if !defined(LIGHTMAP_ON)
             float3 sh;
            #endif
            #if defined(USE_APV_PROBE_OCCLUSION)
             float4 probeOcclusion;
            #endif
             float4 fogFactorAndVertexLight;
            #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
             float4 shadowCoord;
            #endif
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 WorldSpaceNormal;
             float3 TangentSpaceNormal;
             float3 WorldSpaceTangent;
             float3 WorldSpaceBiTangent;
             float3 WorldSpaceViewDirection;
             float3 ObjectSpacePosition;
             float3 WorldSpacePosition;
             float3 AbsoluteWorldSpacePosition;
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float4 uv1;
             float4 uv2;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
            #if defined(LIGHTMAP_ON)
             float2 staticLightmapUV : INTERP0;
            #endif
            #if defined(DYNAMICLIGHTMAP_ON)
             float2 dynamicLightmapUV : INTERP1;
            #endif
            #if !defined(LIGHTMAP_ON)
             float3 sh : INTERP2;
            #endif
            #if defined(USE_APV_PROBE_OCCLUSION)
             float4 probeOcclusion : INTERP3;
            #endif
            #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
             float4 shadowCoord : INTERP4;
            #endif
             float4 tangentWS : INTERP5;
             float4 texCoord0 : INTERP6;
             float4 texCoord1 : INTERP7;
             float4 texCoord2 : INTERP8;
             float4 fogFactorAndVertexLight : INTERP9;
             float3 positionWS : INTERP10;
             float3 normalWS : INTERP11;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            #if defined(LIGHTMAP_ON)
            output.staticLightmapUV = input.staticLightmapUV;
            #endif
            #if defined(DYNAMICLIGHTMAP_ON)
            output.dynamicLightmapUV = input.dynamicLightmapUV;
            #endif
            #if !defined(LIGHTMAP_ON)
            output.sh = input.sh;
            #endif
            #if defined(USE_APV_PROBE_OCCLUSION)
            output.probeOcclusion = input.probeOcclusion;
            #endif
            #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
            output.shadowCoord = input.shadowCoord;
            #endif
            output.tangentWS.xyzw = input.tangentWS;
            output.texCoord0.xyzw = input.texCoord0;
            output.texCoord1.xyzw = input.texCoord1;
            output.texCoord2.xyzw = input.texCoord2;
            output.fogFactorAndVertexLight.xyzw = input.fogFactorAndVertexLight;
            output.positionWS.xyz = input.positionWS;
            output.normalWS.xyz = input.normalWS;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            #if defined(LIGHTMAP_ON)
            output.staticLightmapUV = input.staticLightmapUV;
            #endif
            #if defined(DYNAMICLIGHTMAP_ON)
            output.dynamicLightmapUV = input.dynamicLightmapUV;
            #endif
            #if !defined(LIGHTMAP_ON)
            output.sh = input.sh;
            #endif
            #if defined(USE_APV_PROBE_OCCLUSION)
            output.probeOcclusion = input.probeOcclusion;
            #endif
            #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
            output.shadowCoord = input.shadowCoord;
            #endif
            output.tangentWS = input.tangentWS.xyzw;
            output.texCoord0 = input.texCoord0.xyzw;
            output.texCoord1 = input.texCoord1.xyzw;
            output.texCoord2 = input.texCoord2.xyzw;
            output.fogFactorAndVertexLight = input.fogFactorAndVertexLight.xyzw;
            output.positionWS = input.positionWS.xyz;
            output.normalWS = input.normalWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _DisableAllGradients;
        float4 _ACT1_Color_B;
        float4 _ACT2_Color_B;
        float2 _ACT2_Color_B_Location;
        float4 _ACT2_Color_C;
        float4 _ACT2_Color_A;
        float2 _ACT2_Color_A_Location;
        float2 _ACT1_Color_B_Location;
        float4 _ACT1_Color_C;
        float4 _ACT1_Color_A;
        float2 _ACT1_Color_A_Location;
        float4 _ACT3_Color_B;
        float2 _ACT3_Color_B_Location;
        float4 _ACT3_Color_C;
        float4 _ACT3_Color_A;
        float2 _ACT3_Color_A_Location;
        float4 _ORM_TexelSize;
        float4 _Color3;
        float _Y_GradientEndPosition;
        float4 _Color2;
        float2 _Color_2_Location;
        float4 _AOColor;
        float4 _Normal_TexelSize;
        float2 _AOLevels;
        float4 _BaseColor_TexelSize;
        float4 _Color1;
        float2 _Color_1_Location;
        float _AOIntensity;
        float _RoughnessMultiplier;
        float2 _Tilling;
        float _DisableGradientMap;
        float4 _Y_GradientColor;
        float _Y_GradientIntensity;
        float _Y_GradientStartPosition;
        float _Y_Gradient;
        float _X_Gradient;
        float _X_GradientIntensity;
        float4 _X_GradientColor;
        float _X_GradientStartPosition;
        float _X_GradientEndPosition;
        float _Z_Gradient;
        float _Z_GradientIntensity;
        float4 _Z_GradientColor;
        float _Z_GradientStartPosition;
        float _Z_GradientEndPosition;
        float _UseCustomRoughness;
        float _CustomRoughness;
        float _LightingGradientMapInfluence;
        UNITY_TEXTURE_STREAMING_DEBUG_VARS;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ORM);
        SAMPLER(sampler_ORM);
        TEXTURE2D(_Normal);
        SAMPLER(sampler_Normal);
        TEXTURE2D(_BaseColor);
        SAMPLER(sampler_BaseColor);
        
        // Graph Includes
        #include_with_pragmas "Assets/Samples/Shader Graph/17.3.0/Custom Lighting/Components/Debug/DebugLightingComplexity.hlsl"
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
        {
            Out = lerp(A, B, T);
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Branch_float4(float Predicate, float4 True, float4 False, out float4 Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        void Unity_Branch_float(float Predicate, float True, float False, out float Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Divide_float(float A, float B, out float Out)
        {
            Out = A / B;
        }
        
        // unity-custom-func-begin
        void ApplyDecals_float(float4 positionCS, float3 baseColor, float3 specularColor, float3 normalWS, float metallic, float smoothness, float occlusion, out float3 baseColorOut, out float3 specularColorOut, out float3 normalWSOut, out float metallicOut, out float smoothnessOut, out float occlusionOut){
        #if !defined(SHADERGRAPH_PREVIEW) && defined(_DBUFFER)
        	ApplyDecal(positionCS, baseColor, specularColor, normalWS, metallic, occlusion, smoothness);
        	baseColorOut = baseColor;
        	specularColorOut = specularColor;
        	normalWSOut = normalWS;
        	metallicOut = metallic;
        	occlusionOut = occlusion;
        	smoothnessOut = smoothness;
        #else
        	baseColorOut = baseColor;
        	specularColorOut = specularColor;
        	normalWSOut = normalWS;
        	metallicOut = metallic;
        	occlusionOut = occlusion;
        	smoothnessOut = smoothness;
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float
        {
        float2 PixelPosition;
        };
        
        void SG_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float(float3 _Base_Color, float3 _NormalWS, float _Metallic, float _Smoothness, float _AmbientOcclusion, Bindings_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float IN, out float3 BaseColor_1, out float3 SpecularColor_2, out float3 NormalWS_3, out float Metallic_4, out float Smoothness_6, out float AmbientOcclusion_5)
        {
        float4 _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4 = float4(IN.PixelPosition.xy, 0, 0);
        float _Split_ad27d29658ef44f7b6941c97694d6866_R_1_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[0];
        float _Split_ad27d29658ef44f7b6941c97694d6866_G_2_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[1];
        float _Split_ad27d29658ef44f7b6941c97694d6866_B_3_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[2];
        float _Split_ad27d29658ef44f7b6941c97694d6866_A_4_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[3];
        float _Divide_3f9fb3b7b5b94d0d8246bbc34aa63f7b_Out_2_Float;
        Unity_Divide_float(_Split_ad27d29658ef44f7b6941c97694d6866_G_2_Float, _ScreenParams.y, _Divide_3f9fb3b7b5b94d0d8246bbc34aa63f7b_Out_2_Float);
        float _OneMinus_3cfec48ba27f4585a5feb790836dc9dc_Out_1_Float;
        Unity_OneMinus_float(_Divide_3f9fb3b7b5b94d0d8246bbc34aa63f7b_Out_2_Float, _OneMinus_3cfec48ba27f4585a5feb790836dc9dc_Out_1_Float);
        float _Multiply_a4baed73797c41c1b9631ae9bbddfe12_Out_2_Float;
        Unity_Multiply_float_float(_OneMinus_3cfec48ba27f4585a5feb790836dc9dc_Out_1_Float, _ScreenParams.y, _Multiply_a4baed73797c41c1b9631ae9bbddfe12_Out_2_Float);
        float2 _Vector2_eed86f79e1de4c188df97eb091955bc5_Out_0_Vector2 = float2(_Split_ad27d29658ef44f7b6941c97694d6866_R_1_Float, _Multiply_a4baed73797c41c1b9631ae9bbddfe12_Out_2_Float);
        float3 _Property_6219e38e66a84dddb55188eb0359a8c3_Out_0_Vector3 = _Base_Color;
        float3 _Property_f4c37d8281c1497e8dab743349080d88_Out_0_Vector3 = _NormalWS;
        float _Property_0826181079c84604befc19a2460f4daa_Out_0_Float = _Metallic;
        float _Property_d54a743184cc4f27b93d5f5b239c7b7e_Out_0_Float = _Smoothness;
        float _Property_bd6cbdae9db240b9b4ad935655106f79_Out_0_Float = _AmbientOcclusion;
        float3 _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_baseColorOut_8_Vector3;
        float3 _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_specularColorOut_9_Vector3;
        float3 _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_normalWSOut_10_Vector3;
        float _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_metallicOut_11_Float;
        float _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_smoothnessOut_13_Float;
        float _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_occlusionOut_12_Float;
        ApplyDecals_float((float4(_Vector2_eed86f79e1de4c188df97eb091955bc5_Out_0_Vector2, 0.0, 1.0)), _Property_6219e38e66a84dddb55188eb0359a8c3_Out_0_Vector3, float3 (0, 0, 0), _Property_f4c37d8281c1497e8dab743349080d88_Out_0_Vector3, _Property_0826181079c84604befc19a2460f4daa_Out_0_Float, _Property_d54a743184cc4f27b93d5f5b239c7b7e_Out_0_Float, _Property_bd6cbdae9db240b9b4ad935655106f79_Out_0_Float, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_baseColorOut_8_Vector3, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_specularColorOut_9_Vector3, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_normalWSOut_10_Vector3, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_metallicOut_11_Float, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_smoothnessOut_13_Float, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_occlusionOut_12_Float);
        BaseColor_1 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_baseColorOut_8_Vector3;
        SpecularColor_2 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_specularColorOut_9_Vector3;
        NormalWS_3 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_normalWSOut_10_Vector3;
        Metallic_4 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_metallicOut_11_Float;
        Smoothness_6 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_smoothnessOut_13_Float;
        AmbientOcclusion_5 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_occlusionOut_12_Float;
        }
        
        // unity-custom-func-begin
        void SwitchLightingDebug_float(float3 BaseColorIn, float3 NormalIn, float MetallicIn, float SmoothnessIn, float3 EmissionIn, float AmbientOcclusionIn, float3 positionWS, float3 bakedGI, out float3 BaseColorOut, out float3 NormalOut, out float MetallicOut, out float SmoothnessOut, out float3 EmissionOut, out float AmbientOcclusionOut){
        #if !defined(SHADERGRAPH_PREVIEW) && defined(DEBUG_DISPLAY)
        
        [branch] switch(int(_DebugLightingMode))
        
        {
        
            case 0: //none
        
        		BaseColorOut = BaseColorIn;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
            case 1: //SHADOW_CASCADES
        
        		half cascadeIndex = ComputeCascadeIndex(positionWS);
        
        		switch (uint(cascadeIndex))
        
        		{
        
        			case 0: BaseColorOut = kDebugColorShadowCascade0.rgb;break;
        
        			case 1: BaseColorOut = kDebugColorShadowCascade1.rgb;break;
        
        			case 2: BaseColorOut = kDebugColorShadowCascade2.rgb;break;
        
        			case 3: BaseColorOut = kDebugColorShadowCascade3.rgb;break;
        
        			default: BaseColorOut = kDebugColorBlack.rgb;break;
        
        		}
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
            case 2: //LIGHTING_WITHOUT_NORMAL_MAPS
        
        		BaseColorOut = float3(1,1,1);
        
        		MetallicOut = 0;
        
        		SmoothnessOut = 0;
        
        		NormalOut = float3(0,0,1);
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            case 3: //LIGHTING_WITH_NORMAL_MAPS
        
        		BaseColorOut = float3(1,1,1);
        
        		MetallicOut = 0;
        
        		SmoothnessOut = 0;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            case 4: //REFLECTIONS
        
        		BaseColorOut = float3(0.1,0.1,0.1);
        
        		MetallicOut = 1;
        
        		SmoothnessOut = 1;
        
        		NormalOut = float3(0,0,1);
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            case 5: //REFLECTIONS_WITH_SMOOTHNESS
        
        		BaseColorOut = float3(0.1,0.1,0.1);
        
        		MetallicOut = 1;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
            case 6: //GLOBAL_ILLUM
        
        		BaseColorOut = bakedGI;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = 0;
        
        		NormalOut = float3(0,0,1);
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            default:
        
        		BaseColorOut = BaseColorIn;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
        }
        
        #else
        
        		BaseColorOut = BaseColorIn;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_DebugLighting_61e571d2b9ede1240a524a849d20c997_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpacePosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_DebugLighting_61e571d2b9ede1240a524a849d20c997_float(float3 _Base_Color, float3 _NormalWS, float _Metallic, float _Smoothness, float3 _Emission, float _AmbientOcclusion, Bindings_DebugLighting_61e571d2b9ede1240a524a849d20c997_float IN, out float3 BaseColor_1, out float3 Normal_4, out float Metallic_2, out float Smoothness_3, out float3 Emission_5, out float AmbientOcclusion_6)
        {
        float3 _Property_501515703e3a4a1dbd19f4ae273add46_Out_0_Vector3 = _Base_Color;
        float3 _Property_e5bcb5bf3b62412b8983e3aa1ada8fcd_Out_0_Vector3 = _NormalWS;
        float3 _Transform_13c6b1e888d440fd8340d4a7138979ba_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_13c6b1e888d440fd8340d4a7138979ba_Out_1_Vector3 = TransformWorldToTangent(_Property_e5bcb5bf3b62412b8983e3aa1ada8fcd_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float _Property_7a450453146043b2b11397a72c325042_Out_0_Float = _Metallic;
        float _Property_f0326121e031478a90610d60b8321364_Out_0_Float = _Smoothness;
        float3 _Property_491d95b34bb245718ee21bff5fc249cd_Out_0_Vector3 = _Emission;
        float _Property_da91a6effd53499db08bb774d5686c68_Out_0_Float = _AmbientOcclusion;
        float3 _BakedGI_3f01c30cb8b64e9d9f7fbe474622c7dc_Out_1_Vector3 = SHADERGRAPH_BAKED_GI(IN.WorldSpacePosition, _Property_e5bcb5bf3b62412b8983e3aa1ada8fcd_Out_0_Vector3, IN.PixelPosition.xy, IN.uv1.xy, IN.uv2.xy, true);
        float3 _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_BaseColorOut_7_Vector3;
        float3 _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_NormalOut_11_Vector3;
        float _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_MetallicOut_9_Float;
        float _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_SmoothnessOut_10_Float;
        float3 _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_EmissionOut_12_Vector3;
        float _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_AmbientOcclusionOut_13_Float;
        SwitchLightingDebug_float(_Property_501515703e3a4a1dbd19f4ae273add46_Out_0_Vector3, _Transform_13c6b1e888d440fd8340d4a7138979ba_Out_1_Vector3, _Property_7a450453146043b2b11397a72c325042_Out_0_Float, _Property_f0326121e031478a90610d60b8321364_Out_0_Float, _Property_491d95b34bb245718ee21bff5fc249cd_Out_0_Vector3, _Property_da91a6effd53499db08bb774d5686c68_Out_0_Float, IN.WorldSpacePosition, _BakedGI_3f01c30cb8b64e9d9f7fbe474622c7dc_Out_1_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_BaseColorOut_7_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_NormalOut_11_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_MetallicOut_9_Float, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_SmoothnessOut_10_Float, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_EmissionOut_12_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_AmbientOcclusionOut_13_Float);
        BaseColor_1 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_BaseColorOut_7_Vector3;
        Normal_4 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_NormalOut_11_Vector3;
        Metallic_2 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_MetallicOut_9_Float;
        Smoothness_3 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_SmoothnessOut_10_Float;
        Emission_5 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_EmissionOut_12_Vector3;
        AmbientOcclusion_6 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_AmbientOcclusionOut_13_Float;
        }
        
        // unity-custom-func-begin
        void GetMainLightData_float(float3 worldPos, out float3 direction, out float3 color, out float shadowAtten){
        #ifdef SHADERGRAPH_PREVIEW
            direction = normalize(float3(-0.7,0.7,-0.7));
            color = float3(1,1,1);
            shadowAtten = 1;
        #else
            #if defined(UNIVERSAL_PIPELINE_CORE_INCLUDED)
                float4 shadowCoord = TransformWorldToShadowCoord(worldPos);
                Light mainLight = GetMainLight(shadowCoord);
                direction = mainLight.direction;
                color = mainLight.color;
                shadowAtten = mainLight.shadowAttenuation;
            #else
                direction = normalize(float3(-0.7, 0.7, -0.7));
                color = float3(1, 1, 1);
                shadowAtten = 0;
            #endif
        #endif
        }
        // unity-custom-func-end
        
        // unity-custom-func-begin
        void GetMainLightData_half(half3 worldPos, out half3 direction, out half3 color, out half shadowAtten){
        #ifdef SHADERGRAPH_PREVIEW
            direction = normalize(float3(-0.7,0.7,-0.7));
            color = float3(1,1,1);
            shadowAtten = 1;
        #else
            #if defined(UNIVERSAL_PIPELINE_CORE_INCLUDED)
                float4 shadowCoord = TransformWorldToShadowCoord(worldPos);
                Light mainLight = GetMainLight(shadowCoord);
                direction = mainLight.direction;
                color = mainLight.color;
                shadowAtten = mainLight.shadowAttenuation;
            #else
                direction = normalize(float3(-0.7, 0.7, -0.7));
                color = float3(1, 1, 1);
                shadowAtten = 0;
            #endif
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float
        {
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float IN, out float3 Direction_1, out float3 Color_2, out float ShadowAtten_3)
        {
        float3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        float3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        float _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        GetMainLightData_float(IN.AbsoluteWorldSpacePosition, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float);
        Direction_1 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        Color_2 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        ShadowAtten_3 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        }
        
        struct Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half
        {
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half(Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half IN, out half3 Direction_1, out half3 Color_2, out half ShadowAtten_3)
        {
        half3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        half3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        half _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        GetMainLightData_half(IN.AbsoluteWorldSpacePosition, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float);
        Direction_1 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        Color_2 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        ShadowAtten_3 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        }
        
        void Unity_DotProduct_float3(float3 A, float3 B, out float Out)
        {
            Out = dot(A, B);
        }
        
        void Unity_DotProduct_half3(half3 A, half3 B, out half Out)
        {
            Out = dot(A, B);
        }
        
        void Unity_Saturate_half(half In, out half Out)
        {
            Out = saturate(In);
        }
        
        struct Bindings_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half
        {
        float3 WorldSpaceNormal;
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half(half3 _NormalWS, bool _NormalWS_68a7999ae9ea4bfba3702fd95b0d1a14_IsConnected, half3 _LightVector, bool _LightVector_a12354c78b694cc6b2bdddd67d09ccdc_IsConnected, Bindings_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half IN, out half Diffuse_1)
        {
        half3 _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3 = _NormalWS;
        bool _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3_IsConnected = _NormalWS_68a7999ae9ea4bfba3702fd95b0d1a14_IsConnected;
        half3 _BranchOnInputConnection_71cde5ac4ee04aacb1e2544c8017ba47_Out_3_Vector3 = _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3_IsConnected ? _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3 : IN.WorldSpaceNormal;
        half3 _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3 = _LightVector;
        bool _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3_IsConnected = _LightVector_a12354c78b694cc6b2bdddd67d09ccdc_IsConnected;
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half _MainLight_fa0151c045984bcab58e58725bae0709;
        _MainLight_fa0151c045984bcab58e58725bae0709.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_fa0151c045984bcab58e58725bae0709_Direction_1_Vector3;
        half3 _MainLight_fa0151c045984bcab58e58725bae0709_Color_2_Vector3;
        half _MainLight_fa0151c045984bcab58e58725bae0709_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half(_MainLight_fa0151c045984bcab58e58725bae0709, _MainLight_fa0151c045984bcab58e58725bae0709_Direction_1_Vector3, _MainLight_fa0151c045984bcab58e58725bae0709_Color_2_Vector3, _MainLight_fa0151c045984bcab58e58725bae0709_ShadowAtten_3_Float);
        half3 _BranchOnInputConnection_d18845e766084954af1aa554531c90b9_Out_3_Vector3 = _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3_IsConnected ? _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3 : _MainLight_fa0151c045984bcab58e58725bae0709_Direction_1_Vector3;
        half _DotProduct_daa979e4f9384944a14a23e079be6a5c_Out_2_Float;
        Unity_DotProduct_half3(_BranchOnInputConnection_71cde5ac4ee04aacb1e2544c8017ba47_Out_3_Vector3, _BranchOnInputConnection_d18845e766084954af1aa554531c90b9_Out_3_Vector3, _DotProduct_daa979e4f9384944a14a23e079be6a5c_Out_2_Float);
        half _Saturate_4747439b9bfa431293a93646ce71aa12_Out_1_Float;
        Unity_Saturate_half(_DotProduct_daa979e4f9384944a14a23e079be6a5c_Out_2_Float, _Saturate_4747439b9bfa431293a93646ce71aa12_Out_1_Float);
        Diffuse_1 = _Saturate_4747439b9bfa431293a93646ce71aa12_Out_1_Float;
        }
        
        void Unity_Lerp_float(float A, float B, float T, out float Out)
        {
            Out = lerp(A, B, T);
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Reciprocal_float(float In, out float Out)
        {
            Out = 1.0/In;
        }
        
        void Unity_FresnelEffect_float(float3 Normal, float3 ViewDir, float Power, out float Out)
        {
            Out = pow((1.0 - saturate(dot(normalize(Normal), ViewDir))), Power);
        }
        
        void Unity_Lerp_float3(float3 A, float3 B, float3 T, out float3 Out)
        {
            Out = lerp(A, B, T);
        }
        
        struct Bindings_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        };
        
        void SG_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float(float3 _Base_Color, float3 _NormalWS, bool _NormalWS_3240674a787044a092398b1ca753ad83_IsConnected, float _Metallic, float _Smoothness, float _F0, Bindings_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float IN, out float3 Reflectance_1)
        {
        float _Property_4678b902494b4e1a9b08a8067b7bed85_Out_0_Float = _Smoothness;
        float _OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float;
        Unity_OneMinus_float(_Property_4678b902494b4e1a9b08a8067b7bed85_Out_0_Float, _OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float);
        float _Multiply_314f6a0aec0a44538333e69617e91cf9_Out_2_Float;
        Unity_Multiply_float_float(_OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float, _OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float, _Multiply_314f6a0aec0a44538333e69617e91cf9_Out_2_Float);
        float _Add_513724b99c2f4ea2a803e64d80f0c25b_Out_2_Float;
        Unity_Add_float(_Multiply_314f6a0aec0a44538333e69617e91cf9_Out_2_Float, float(1), _Add_513724b99c2f4ea2a803e64d80f0c25b_Out_2_Float);
        float _Reciprocal_67b338305d0043abb9e7d82dd0f16146_Out_1_Float;
        Unity_Reciprocal_float(_Add_513724b99c2f4ea2a803e64d80f0c25b_Out_2_Float, _Reciprocal_67b338305d0043abb9e7d82dd0f16146_Out_1_Float);
        float _Property_b67a6773dce34e91ae69bbf282d871cc_Out_0_Float = _F0;
        float _Property_703d9ec0a0894a3b965f0ed25a10435b_Out_0_Float = _F0;
        float _Add_8ad10875906b4a80872f2b2fb0518183_Out_2_Float;
        Unity_Add_float(_Property_4678b902494b4e1a9b08a8067b7bed85_Out_0_Float, _Property_703d9ec0a0894a3b965f0ed25a10435b_Out_0_Float, _Add_8ad10875906b4a80872f2b2fb0518183_Out_2_Float);
        float3 _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3 = _NormalWS;
        bool _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3_IsConnected = _NormalWS_3240674a787044a092398b1ca753ad83_IsConnected;
        float3 _BranchOnInputConnection_43b8bde55a8a41468ba21d53db128986_Out_3_Vector3 = _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3_IsConnected ? _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3 : IN.WorldSpaceNormal;
        float _FresnelEffect_34b729c62edd4d5f99dc20e2e7d0a7fa_Out_3_Float;
        Unity_FresnelEffect_float(_BranchOnInputConnection_43b8bde55a8a41468ba21d53db128986_Out_3_Vector3, IN.WorldSpaceViewDirection, float(4), _FresnelEffect_34b729c62edd4d5f99dc20e2e7d0a7fa_Out_3_Float);
        float _Lerp_1cad776e609842c181b8acfaef47c317_Out_3_Float;
        Unity_Lerp_float(_Property_b67a6773dce34e91ae69bbf282d871cc_Out_0_Float, _Add_8ad10875906b4a80872f2b2fb0518183_Out_2_Float, _FresnelEffect_34b729c62edd4d5f99dc20e2e7d0a7fa_Out_3_Float, _Lerp_1cad776e609842c181b8acfaef47c317_Out_3_Float);
        float _Multiply_0d364d1c231246d981281b868ea74a95_Out_2_Float;
        Unity_Multiply_float_float(_Reciprocal_67b338305d0043abb9e7d82dd0f16146_Out_1_Float, _Lerp_1cad776e609842c181b8acfaef47c317_Out_3_Float, _Multiply_0d364d1c231246d981281b868ea74a95_Out_2_Float);
        float3 _Property_87ae51a595c24e46ad9ef0f4493231fc_Out_0_Vector3 = _Base_Color;
        float _Property_ce0a90815c5046b48dd0564711f2b466_Out_0_Float = _Metallic;
        float3 _Lerp_6b4c86a1d5004851a7dba1bacc4fd953_Out_3_Vector3;
        Unity_Lerp_float3((_Multiply_0d364d1c231246d981281b868ea74a95_Out_2_Float.xxx), _Property_87ae51a595c24e46ad9ef0f4493231fc_Out_0_Vector3, (_Property_ce0a90815c5046b48dd0564711f2b466_Out_0_Float.xxx), _Lerp_6b4c86a1d5004851a7dba1bacc4fd953_Out_3_Vector3);
        Reflectance_1 = _Lerp_6b4c86a1d5004851a7dba1bacc4fd953_Out_3_Vector3;
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        void Unity_Normalize_float3(float3 In, out float3 Out)
        {
            Out = normalize(In);
        }
        
        struct Bindings_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float
        {
        };
        
        void SG_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float(float3 _viewDir, float3 _lightDir, Bindings_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float IN, out float3 Out_1)
        {
        float3 _Property_fde52ad74bda46adabbcc34b42b16131_Out_0_Vector3 = _viewDir;
        float3 _Property_1dc55a6640574aaf8c04290eb0d5e816_Out_0_Vector3 = _lightDir;
        float3 _Add_2a8cf5c52e8c4e3fb8c3f9a87b68b2a2_Out_2_Vector3;
        Unity_Add_float3(_Property_fde52ad74bda46adabbcc34b42b16131_Out_0_Vector3, _Property_1dc55a6640574aaf8c04290eb0d5e816_Out_0_Vector3, _Add_2a8cf5c52e8c4e3fb8c3f9a87b68b2a2_Out_2_Vector3);
        float3 _Normalize_b3b8196f46224ae3a998bba24956de7f_Out_1_Vector3;
        Unity_Normalize_float3(_Add_2a8cf5c52e8c4e3fb8c3f9a87b68b2a2_Out_2_Vector3, _Normalize_b3b8196f46224ae3a998bba24956de7f_Out_1_Vector3);
        Out_1 = _Normalize_b3b8196f46224ae3a998bba24956de7f_Out_1_Vector3;
        }
        
        void Unity_Exponential2_float(float In, out float Out)
        {
            Out = exp2(In);
        }
        
        struct Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float
        {
        };
        
        void SG_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float(float _Smoothness, Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float IN, out float SpecPower_1)
        {
        float _Property_80f639c6927445458cce37e8c24909a1_Out_0_Float = _Smoothness;
        float _Multiply_c7e9a2c01b804964a64dd7499f62a400_Out_2_Float;
        Unity_Multiply_float_float(_Property_80f639c6927445458cce37e8c24909a1_Out_0_Float, 10, _Multiply_c7e9a2c01b804964a64dd7499f62a400_Out_2_Float);
        float _Add_663b867a23b04deaa88c2ebd79bcdbc7_Out_2_Float;
        Unity_Add_float(_Multiply_c7e9a2c01b804964a64dd7499f62a400_Out_2_Float, float(1), _Add_663b867a23b04deaa88c2ebd79bcdbc7_Out_2_Float);
        float _Exponential_bd8d33b85b2e46508bef0c6cc36f9dae_Out_1_Float;
        Unity_Exponential2_float(_Add_663b867a23b04deaa88c2ebd79bcdbc7_Out_2_Float, _Exponential_bd8d33b85b2e46508bef0c6cc36f9dae_Out_1_Float);
        SpecPower_1 = _Exponential_bd8d33b85b2e46508bef0c6cc36f9dae_Out_1_Float;
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
        Out = A * B;
        }
        
        struct Bindings_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float(float3 _NormalWS, bool _NormalWS_5a3c9a3a7faa491894a42d170b5bfeb5_IsConnected, float _Smoothness, float3 _Reflectance, float3 _LightVector, bool _LightVector_3db37b6247094f32bcccc4cb689d525f_IsConnected, Bindings_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float IN, out float3 Specular_1)
        {
        float3 _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3 = _LightVector;
        bool _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3_IsConnected = _LightVector_3db37b6247094f32bcccc4cb689d525f_IsConnected;
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float _MainLight_6570bf88718b46ebb6bd80eec408287a;
        _MainLight_6570bf88718b46ebb6bd80eec408287a.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_6570bf88718b46ebb6bd80eec408287a_Direction_1_Vector3;
        half3 _MainLight_6570bf88718b46ebb6bd80eec408287a_Color_2_Vector3;
        half _MainLight_6570bf88718b46ebb6bd80eec408287a_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(_MainLight_6570bf88718b46ebb6bd80eec408287a, _MainLight_6570bf88718b46ebb6bd80eec408287a_Direction_1_Vector3, _MainLight_6570bf88718b46ebb6bd80eec408287a_Color_2_Vector3, _MainLight_6570bf88718b46ebb6bd80eec408287a_ShadowAtten_3_Float);
        float3 _BranchOnInputConnection_6a7b13b3cb82474aa187229c3d17a00f_Out_3_Vector3 = _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3_IsConnected ? _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3 : _MainLight_6570bf88718b46ebb6bd80eec408287a_Direction_1_Vector3;
        Bindings_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float _HalfAngle_f48886360d2649d8b7540e6fb3eef669;
        half3 _HalfAngle_f48886360d2649d8b7540e6fb3eef669_Out_1_Vector3;
        SG_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float(IN.WorldSpaceViewDirection, _BranchOnInputConnection_6a7b13b3cb82474aa187229c3d17a00f_Out_3_Vector3, _HalfAngle_f48886360d2649d8b7540e6fb3eef669, _HalfAngle_f48886360d2649d8b7540e6fb3eef669_Out_1_Vector3);
        float3 _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3 = _NormalWS;
        bool _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3_IsConnected = _NormalWS_5a3c9a3a7faa491894a42d170b5bfeb5_IsConnected;
        float3 _BranchOnInputConnection_72430741d0e04d2dbf5368b624a090cc_Out_3_Vector3 = _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3_IsConnected ? _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3 : IN.WorldSpaceNormal;
        float _DotProduct_4558c6edcb084d359ac8ee4b2934ea05_Out_2_Float;
        Unity_DotProduct_float3(_HalfAngle_f48886360d2649d8b7540e6fb3eef669_Out_1_Vector3, _BranchOnInputConnection_72430741d0e04d2dbf5368b624a090cc_Out_3_Vector3, _DotProduct_4558c6edcb084d359ac8ee4b2934ea05_Out_2_Float);
        float _Saturate_23d3962f416c4150a990dcb36b5ecbc4_Out_1_Float;
        Unity_Saturate_float(_DotProduct_4558c6edcb084d359ac8ee4b2934ea05_Out_2_Float, _Saturate_23d3962f416c4150a990dcb36b5ecbc4_Out_1_Float);
        float _Property_f4ccf6ae090a4694bb78a2cef88028e0_Out_0_Float = _Smoothness;
        Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9;
        half _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9_SpecPower_1_Float;
        SG_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float(_Property_f4ccf6ae090a4694bb78a2cef88028e0_Out_0_Float, _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9, _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9_SpecPower_1_Float);
        float _Power_b652c8bd47c44415bf7733f381883bf3_Out_2_Float;
        Unity_Power_float(_Saturate_23d3962f416c4150a990dcb36b5ecbc4_Out_1_Float, _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9_SpecPower_1_Float, _Power_b652c8bd47c44415bf7733f381883bf3_Out_2_Float);
        float _Property_1fcbde0798cd43628cbb75583e5d6e7a_Out_0_Float = _Smoothness;
        float _Multiply_8e72dd78784942f5ac8322f3055fd0ed_Out_2_Float;
        Unity_Multiply_float_float(_Power_b652c8bd47c44415bf7733f381883bf3_Out_2_Float, _Property_1fcbde0798cd43628cbb75583e5d6e7a_Out_0_Float, _Multiply_8e72dd78784942f5ac8322f3055fd0ed_Out_2_Float);
        float3 _Property_5b0bef6a4de54859800dd057235a4dbc_Out_0_Vector3 = _Reflectance;
        float3 _Multiply_73cf80666f44441494dd412a147e089c_Out_2_Vector3;
        Unity_Multiply_float3_float3((_Multiply_8e72dd78784942f5ac8322f3055fd0ed_Out_2_Float.xxx), _Property_5b0bef6a4de54859800dd057235a4dbc_Out_0_Vector3, _Multiply_73cf80666f44441494dd412a147e089c_Out_2_Vector3);
        float3 _Multiply_bceb91846ebf445c9093523786845bb5_Out_2_Vector3;
        Unity_Multiply_float3_float3(_Multiply_73cf80666f44441494dd412a147e089c_Out_2_Vector3, float3(10, 10, 10), _Multiply_bceb91846ebf445c9093523786845bb5_Out_2_Vector3);
        Specular_1 = _Multiply_bceb91846ebf445c9093523786845bb5_Out_2_Vector3;
        }
        
        // unity-custom-func-begin
        void AddAdditionalLightsSimple_float(float SpecPower, float3 WorldPosition, float3 WorldNormal, float3 WorldView, float MainDiffuse, float3 MainSpecular, float3 MainColor, float3 Reflectance, float2 ScreenPosition, out float Diffuse, out float3 Specular, out float3 Color){
        Diffuse = MainDiffuse;
        
        Specular = MainSpecular;
        
        Color = MainColor * (MainDiffuse + MainSpecular);
        
        
        
        #ifndef SHADERGRAPH_PREVIEW
        
            
        
        #if defined(_ADDITIONAL_LIGHTS) || defined(_CLUSTER_LIGHT_LOOP)
        
        
        
            #if defined(_ADDITIONAL_LIGHTS)
        
                uint pixelLightCount = GetAdditionalLightsCount();
        
            #endif
        
        
        
        #if USE_CLUSTER_LIGHT_LOOP
        
            // for Foward+ LIGHT_LOOP_BEGIN macro uses inputData.normalizedScreenSpaceUV and inputData.positionWS
        
            InputData inputData = (InputData)0;
        
        
        
            inputData.normalizedScreenSpaceUV = ScreenPosition;
        
            inputData.positionWS = WorldPosition;
        
        #endif
        
        
        
            LIGHT_LOOP_BEGIN(pixelLightCount)
        
        		// Call the URP additional light algorithm. This will not calculate shadows, since we don't pass a shadow mask value
        
        		Light light = GetAdditionalLight(lightIndex, WorldPosition);
        
        		// Manually set the shadow attenuation by calculating realtime shadows
        
        		light.shadowAttenuation = AdditionalLightRealtimeShadow(lightIndex, WorldPosition, light.direction);
        
                float NdotL = saturate(dot(WorldNormal, light.direction));
        
                float atten = light.distanceAttenuation * light.shadowAttenuation;
        
                float thisDiffuse = atten * NdotL;
        
                float3 halfAngle = normalize(light.direction + WorldView);
        
                float spec = pow(saturate(dot(halfAngle, WorldNormal)), SpecPower);
        
                float3 thisSpecular = spec * Reflectance * atten;
        
                Diffuse += thisDiffuse;
        
                Specular += thisSpecular;
        
                #if defined(_LIGHT_COOKIES)
        
                    float3 cookieColor = SampleAdditionalLightCookie(lightIndex, WorldPosition);
        
                    light.color *= cookieColor;
        
                #endif
        
                Color += light.color * (thisDiffuse + thisSpecular);
        
            LIGHT_LOOP_END
        
            float total = Diffuse + dot(Specular, float3(0.333, 0.333, 0.333));
        
            Color = total <= 0 ? MainColor : Color / total;
        
        #endif // _ADDITIONAL_LIGHTS || _CLUSTER_LIGHT_LOOP
        
        
        
        #endif // SHADERGRAPH_PREVIEW
        }
        // unity-custom-func-end
        
        struct Bindings_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        };
        
        void SG_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float(float _MainLightDiffuse, float3 _MainLightSpecular, float3 _MainLightColor, float3 _NormalWS, bool _NormalWS_70cbf5ac6da04bf6bd87eb71ccb7c48d_IsConnected, float _Smoothness, float3 _Reflectance, Bindings_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float IN, out float Diffuse_1, out float3 Specular_2, out float3 Color_3)
        {
        float _Property_b9f05025da4f4857a7b1b6f56259a629_Out_0_Float = _Smoothness;
        Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1;
        half _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1_SpecPower_1_Float;
        SG_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float(_Property_b9f05025da4f4857a7b1b6f56259a629_Out_0_Float, _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1, _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1_SpecPower_1_Float);
        float3 _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3 = _NormalWS;
        bool _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3_IsConnected = _NormalWS_70cbf5ac6da04bf6bd87eb71ccb7c48d_IsConnected;
        float3 _BranchOnInputConnection_d869e3d8654b48a491de945ad8af6301_Out_3_Vector3 = _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3_IsConnected ? _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3 : IN.WorldSpaceNormal;
        float _Property_25880f0697234954b8dc6ef11af3752d_Out_0_Float = _MainLightDiffuse;
        float3 _Property_1e29ad89226c4d84a936fe7530839aef_Out_0_Vector3 = _MainLightSpecular;
        float3 _Property_ac790fc8215b4b3d8851855d2153960d_Out_0_Vector3 = _MainLightColor;
        float3 _Property_eea8eda455d44ae7b30c65f80baac806_Out_0_Vector3 = _Reflectance;
        float4 _ScreenPosition_bb4bf3ece5524d4c898132bd377d7d8b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Diffuse_7_Float;
        float3 _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Specular_8_Vector3;
        float3 _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Color_9_Vector3;
        AddAdditionalLightsSimple_float(_SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1_SpecPower_1_Float, IN.WorldSpacePosition, _BranchOnInputConnection_d869e3d8654b48a491de945ad8af6301_Out_3_Vector3, IN.WorldSpaceViewDirection, _Property_25880f0697234954b8dc6ef11af3752d_Out_0_Float, _Property_1e29ad89226c4d84a936fe7530839aef_Out_0_Vector3, _Property_ac790fc8215b4b3d8851855d2153960d_Out_0_Vector3, _Property_eea8eda455d44ae7b30c65f80baac806_Out_0_Vector3, (_ScreenPosition_bb4bf3ece5524d4c898132bd377d7d8b_Out_0_Vector4.xy), _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Diffuse_7_Float, _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Specular_8_Vector3, _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Color_9_Vector3);
        Diffuse_1 = _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Diffuse_7_Float;
        Specular_2 = _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Specular_8_Vector3;
        Color_3 = _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Color_9_Vector3;
        }
        
        void Unity_Negate_float3(float3 In, out float3 Out)
        {
            Out = -1 * In;
        }
        
        void Unity_Reflection_float3(float3 In, float3 Normal, out float3 Out)
        {
            Out = reflect(In, Normal);
        }
        
        // unity-custom-func-begin
        void URPReflectionProbe_float(float3 positionWS, float3 reflectVector, float2 normalizedScreenSpaceUV, float roughness, float occlusion, out float3 reflection){
        #ifdef SHADERGRAPH_PREVIEW
        
            reflection = float3(0,0,0);
        
        #else
        
            reflection = GlossyEnvironmentReflection(reflectVector, positionWS, roughness, occlusion, normalizedScreenSpaceUV);
        
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        };
        
        void SG_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float(float3 _positionWS, bool _positionWS_d6701bdc1f184a57ac2283491fc460d9_IsConnected, float3 _reflectVector, bool _reflectVector_3e2eb19b69b8469eaf2302c7abc4cbc5_IsConnected, float _smoothness, float _occlusion, Bindings_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float IN, out float3 Out_1)
        {
        float3 _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3 = _positionWS;
        bool _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3_IsConnected = _positionWS_d6701bdc1f184a57ac2283491fc460d9_IsConnected;
        float3 _BranchOnInputConnection_8fb583036b0c4313a1ecd93143939f21_Out_3_Vector3 = _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3_IsConnected ? _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3 : IN.WorldSpacePosition;
        float3 _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3 = _reflectVector;
        bool _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3_IsConnected = _reflectVector_3e2eb19b69b8469eaf2302c7abc4cbc5_IsConnected;
        float3 _Negate_9cf7cea21c5641239fdbcb32480ac39e_Out_1_Vector3;
        Unity_Negate_float3(IN.WorldSpaceViewDirection, _Negate_9cf7cea21c5641239fdbcb32480ac39e_Out_1_Vector3);
        float3 _Reflection_c8689c494aab4411aadc74299549c6cb_Out_2_Vector3;
        Unity_Reflection_float3(_Negate_9cf7cea21c5641239fdbcb32480ac39e_Out_1_Vector3, IN.WorldSpaceNormal, _Reflection_c8689c494aab4411aadc74299549c6cb_Out_2_Vector3);
        float3 _BranchOnInputConnection_9600230d09794702a61c1a01f8e842a5_Out_3_Vector3 = _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3_IsConnected ? _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3 : _Reflection_c8689c494aab4411aadc74299549c6cb_Out_2_Vector3;
        float4 _ScreenPosition_270e438746a9466e8aaf01f4903f62fb_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float _Property_9012e47da801473d8ef85a4092281eb2_Out_0_Float = _smoothness;
        float _OneMinus_b2508f25afb44017ba3480edc35cf631_Out_1_Float;
        Unity_OneMinus_float(_Property_9012e47da801473d8ef85a4092281eb2_Out_0_Float, _OneMinus_b2508f25afb44017ba3480edc35cf631_Out_1_Float);
        float _Property_d602b1723845462cbf00324de1e9e82a_Out_0_Float = _occlusion;
        float3 _URPReflectionProbeCustomFunction_7233d098cd214f55baf898d486bfdf4b_reflection_4_Vector3;
        URPReflectionProbe_float(_BranchOnInputConnection_8fb583036b0c4313a1ecd93143939f21_Out_3_Vector3, _BranchOnInputConnection_9600230d09794702a61c1a01f8e842a5_Out_3_Vector3, (_ScreenPosition_270e438746a9466e8aaf01f4903f62fb_Out_0_Vector4.xy), _OneMinus_b2508f25afb44017ba3480edc35cf631_Out_1_Float, _Property_d602b1723845462cbf00324de1e9e82a_Out_0_Float, _URPReflectionProbeCustomFunction_7233d098cd214f55baf898d486bfdf4b_reflection_4_Vector3);
        Out_1 = _URPReflectionProbeCustomFunction_7233d098cd214f55baf898d486bfdf4b_reflection_4_Vector3;
        }
        
        // unity-custom-func-begin
        void SSAO_float(float2 normalizedScreenSpaceUV, out float indirectAmbientOcclusion, out float directAmbientOcclusion){
        #if defined(_SCREEN_SPACE_OCCLUSION) && !defined(_SURFACE_TYPE_TRANSPARENT) && !defined(SHADERGRAPH_PREVIEW)
        
            float ssao = saturate(SampleAmbientOcclusion(normalizedScreenSpaceUV) + (1.0 - _AmbientOcclusionParam.x));
        
            indirectAmbientOcclusion = ssao;
        
            directAmbientOcclusion = lerp(half(1.0), ssao, _AmbientOcclusionParam.w);
        
        #else
        
            directAmbientOcclusion = half(1.0);
        
            indirectAmbientOcclusion = half(1.0);
        
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float
        {
        float2 NDCPosition;
        };
        
        void SG_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float(Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float IN, out float indirectAO_1, out float directAO_2)
        {
        float4 _ScreenPosition_0fdc511287e14fd48ca909caba575383_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_indirectAmbientOcclusion_0_Float;
        float _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_directAmbientOcclusion_1_Float;
        SSAO_float((_ScreenPosition_0fdc511287e14fd48ca909caba575383_Out_0_Vector4.xy), _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_indirectAmbientOcclusion_0_Float, _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_directAmbientOcclusion_1_Float);
        indirectAO_1 = _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_indirectAmbientOcclusion_0_Float;
        directAO_2 = _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_directAmbientOcclusion_1_Float;
        }
        
        void Unity_Minimum_float(float A, float B, out float Out)
        {
            Out = min(A, B);
        };
        
        struct Bindings_AmbientURP_300875fdd653fe340b08ad1547984cf1_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_AmbientURP_300875fdd653fe340b08ad1547984cf1_float(float3 _Base_Color, float3 _NormalWS, bool _NormalWS_3a565a44841d4b729f8e86b08d09299c_IsConnected, float _Metallic, float _Smoothness, float3 _Reflectance, float _Ambient_Occlusion, Bindings_AmbientURP_300875fdd653fe340b08ad1547984cf1_float IN, out float3 Ambient_1, out float DirectAO_2)
        {
        float3 _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3 = _NormalWS;
        bool _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3_IsConnected = _NormalWS_3a565a44841d4b729f8e86b08d09299c_IsConnected;
        float3 _BranchOnInputConnection_5e35c0fc2eee4cf7ae47da45409fa2a7_Out_3_Vector3 = _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3_IsConnected ? _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3 : IN.WorldSpaceNormal;
        float3 _BakedGI_1ac35076ff2349f99fec2cef2550ff2d_Out_1_Vector3 = SHADERGRAPH_BAKED_GI(IN.WorldSpacePosition, _BranchOnInputConnection_5e35c0fc2eee4cf7ae47da45409fa2a7_Out_3_Vector3, IN.PixelPosition.xy, IN.uv1.xy, IN.uv2.xy, true);
        float3 _Property_5fb17e215f49424cb9cc9d0806f3f47d_Out_0_Vector3 = _Base_Color;
        float _Property_f995d8544fdb448d85ac845c7bdee967_Out_0_Float = _Metallic;
        float3 _Lerp_842ba4fcd0cf48a3afa108eecd7d56a8_Out_3_Vector3;
        Unity_Lerp_float3(_Property_5fb17e215f49424cb9cc9d0806f3f47d_Out_0_Vector3, float3(0, 0, 0), (_Property_f995d8544fdb448d85ac845c7bdee967_Out_0_Float.xxx), _Lerp_842ba4fcd0cf48a3afa108eecd7d56a8_Out_3_Vector3);
        float3 _Multiply_48ca9faac6354eefabc65eeb591f3fc4_Out_2_Vector3;
        Unity_Multiply_float3_float3(_BakedGI_1ac35076ff2349f99fec2cef2550ff2d_Out_1_Vector3, _Lerp_842ba4fcd0cf48a3afa108eecd7d56a8_Out_3_Vector3, _Multiply_48ca9faac6354eefabc65eeb591f3fc4_Out_2_Vector3);
        float3 _Negate_4f98a1dfbc4d4bd1abe58f406453303f_Out_1_Vector3;
        Unity_Negate_float3(IN.WorldSpaceViewDirection, _Negate_4f98a1dfbc4d4bd1abe58f406453303f_Out_1_Vector3);
        float3 _Reflection_710a69cb22b745929e6bb9b84d11de2f_Out_2_Vector3;
        Unity_Reflection_float3(_Negate_4f98a1dfbc4d4bd1abe58f406453303f_Out_1_Vector3, _BranchOnInputConnection_5e35c0fc2eee4cf7ae47da45409fa2a7_Out_3_Vector3, _Reflection_710a69cb22b745929e6bb9b84d11de2f_Out_2_Vector3);
        float _Property_8c3e921b9cb34f7b82d2a71254653c09_Out_0_Float = _Smoothness;
        Bindings_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.WorldSpaceNormal = IN.WorldSpaceNormal;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.WorldSpacePosition = IN.WorldSpacePosition;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.NDCPosition = IN.NDCPosition;
        float3 _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08_Out_1_Vector3;
        SG_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float(half3 (0, 0, 0), false, _Reflection_710a69cb22b745929e6bb9b84d11de2f_Out_2_Vector3, true, _Property_8c3e921b9cb34f7b82d2a71254653c09_Out_0_Float, half(1), _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08, _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08_Out_1_Vector3);
        float3 _Property_2ddaa58bd1e94d0b8508ce91ad39fa39_Out_0_Vector3 = _Reflectance;
        float3 _Multiply_40be6c08ca2f44ef99b6a2b81955d62c_Out_2_Vector3;
        Unity_Multiply_float3_float3(_SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08_Out_1_Vector3, _Property_2ddaa58bd1e94d0b8508ce91ad39fa39_Out_0_Vector3, _Multiply_40be6c08ca2f44ef99b6a2b81955d62c_Out_2_Vector3);
        float3 _Add_c0a57a54eb8b4419a7e907a65e6556a7_Out_2_Vector3;
        Unity_Add_float3(_Multiply_48ca9faac6354eefabc65eeb591f3fc4_Out_2_Vector3, _Multiply_40be6c08ca2f44ef99b6a2b81955d62c_Out_2_Vector3, _Add_c0a57a54eb8b4419a7e907a65e6556a7_Out_2_Vector3);
        float _Property_5a996c5d941d46019b3ac5ada526c475_Out_0_Float = _Ambient_Occlusion;
        Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e;
        _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e.NDCPosition = IN.NDCPosition;
        half _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_indirectAO_1_Float;
        half _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_directAO_2_Float;
        SG_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float(_ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_indirectAO_1_Float, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_directAO_2_Float);
        float _Minimum_699e1643e9974baa97f6f21c8f949d09_Out_2_Float;
        Unity_Minimum_float(_Property_5a996c5d941d46019b3ac5ada526c475_Out_0_Float, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_indirectAO_1_Float, _Minimum_699e1643e9974baa97f6f21c8f949d09_Out_2_Float);
        float3 _Multiply_ccaf4dfa5cdb4d9fae7fd92dc7d512da_Out_2_Vector3;
        Unity_Multiply_float3_float3(_Add_c0a57a54eb8b4419a7e907a65e6556a7_Out_2_Vector3, (_Minimum_699e1643e9974baa97f6f21c8f949d09_Out_2_Float.xxx), _Multiply_ccaf4dfa5cdb4d9fae7fd92dc7d512da_Out_2_Vector3);
        float _Minimum_c4dcab6b18b34dde987e68f86d22aed7_Out_2_Float;
        Unity_Minimum_float(_Property_5a996c5d941d46019b3ac5ada526c475_Out_0_Float, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_directAO_2_Float, _Minimum_c4dcab6b18b34dde987e68f86d22aed7_Out_2_Float);
        Ambient_1 = _Multiply_ccaf4dfa5cdb4d9fae7fd92dc7d512da_Out_2_Vector3;
        DirectAO_2 = _Minimum_c4dcab6b18b34dde987e68f86d22aed7_Out_2_Float;
        }
        
        void Unity_Fog_float(out float4 Color, out float Density, float3 Position)
        {
            SHADERGRAPH_FOG(Position, Color, Density);
        }
        
        struct Bindings_Fog_286ae83400099a24bba6faf005588be7_float
        {
        float3 ObjectSpacePosition;
        };
        
        void SG_Fog_286ae83400099a24bba6faf005588be7_float(float3 _In, Bindings_Fog_286ae83400099a24bba6faf005588be7_float IN, out float3 Out_1)
        {
        float3 _Property_626923dc627443639da97776de7dcc22_Out_0_Vector3 = _In;
        float4 _Fog_acabe3c84c9549f3880ce7d106150576_Color_0_Vector4;
        float _Fog_acabe3c84c9549f3880ce7d106150576_Density_1_Float;
        Unity_Fog_float(_Fog_acabe3c84c9549f3880ce7d106150576_Color_0_Vector4, _Fog_acabe3c84c9549f3880ce7d106150576_Density_1_Float, IN.ObjectSpacePosition);
        float3 _Lerp_9cfca9aa08c7423bab62b39a01237d64_Out_3_Vector3;
        Unity_Lerp_float3(_Property_626923dc627443639da97776de7dcc22_Out_0_Vector3, (_Fog_acabe3c84c9549f3880ce7d106150576_Color_0_Vector4.xyz), (_Fog_acabe3c84c9549f3880ce7d106150576_Density_1_Float.xxx), _Lerp_9cfca9aa08c7423bab62b39a01237d64_Out_3_Vector3);
        Out_1 = _Lerp_9cfca9aa08c7423bab62b39a01237d64_Out_3_Vector3;
        }
        
        void Unity_Saturate_float3(float3 In, out float3 Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Saturation_float(float3 In, float Saturation, out float3 Out)
        {
            float luma = dot(In, float3(0.2126729, 0.7151522, 0.0721750));
            Out =  luma.xxx + Saturation.xxx * (In - luma.xxx);
        }
        
        void Unity_Contrast_float(float3 In, float Contrast, out float3 Out)
        {
            float midpoint = pow(0.5, 2.2);
            Out =  (In - midpoint) * Contrast + midpoint;
        }
        
        void Unity_Remap_float3(float3 In, float2 InMinMax, float2 OutMinMax, out float3 Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        struct Bindings_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpaceViewDirection;
        float3 ObjectSpacePosition;
        float3 WorldSpacePosition;
        float3 AbsoluteWorldSpacePosition;
        float2 NDCPosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float(float3 _Base_Color, float3 _Normal, bool _Normal_e1611e545480449d80aa5a0e7c2b63c4_IsConnected, float _Metallic, float _Smoothness, float _Micro_Occlusion, float _Ambient_Occlusion, float4 _Color_A, float2 _Color_A_Location, float4 _Color_B, float2 _Color_B_Location, float4 _Color_C, Bindings_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float IN, out float3 Lit_1)
        {
        float4 _Property_bedee92e97ce4b2abff5524ce019b2a8_Out_0_Vector4 = _Color_A;
        float4 _Property_c648ff794fd34283beff09e33d8293fc_Out_0_Vector4 = _Color_B;
        float3 _Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3 = _Normal;
        bool _Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3_IsConnected = _Normal_e1611e545480449d80aa5a0e7c2b63c4_IsConnected;
        float3 _Transform_75d5405941a444c088600c650a7269f0_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_75d5405941a444c088600c650a7269f0_Out_1_Vector3 = TransformTangentToWorld(_Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float3 _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3 = _Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3_IsConnected ? _Transform_75d5405941a444c088600c650a7269f0_Out_1_Vector3 : IN.WorldSpaceNormal;
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float _MainLight_d918d0814080438585a810ba0b8afeb4;
        _MainLight_d918d0814080438585a810ba0b8afeb4.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3;
        half3 _MainLight_d918d0814080438585a810ba0b8afeb4_Color_2_Vector3;
        half _MainLight_d918d0814080438585a810ba0b8afeb4_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(_MainLight_d918d0814080438585a810ba0b8afeb4, _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3, _MainLight_d918d0814080438585a810ba0b8afeb4_Color_2_Vector3, _MainLight_d918d0814080438585a810ba0b8afeb4_ShadowAtten_3_Float);
        Bindings_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half _DiffuseLambert_7f9e988376a2438ebc87097469e065d3;
        _DiffuseLambert_7f9e988376a2438ebc87097469e065d3.WorldSpaceNormal = IN.WorldSpaceNormal;
        _DiffuseLambert_7f9e988376a2438ebc87097469e065d3.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half _DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float;
        SG_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half(_BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3, true, _DiffuseLambert_7f9e988376a2438ebc87097469e065d3, _DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float);
        float _Property_ac10139ecedb4301b4595fa5b13c00b8_Out_0_Float = _Smoothness;
        float3 _Property_58fa7de1b7784467935169b8914ee373_Out_0_Vector3 = _Base_Color;
        float _Property_433019e2d18944a2909e58d06f7cc1ec_Out_0_Float = _Metallic;
        float _Property_dd950e92d2d54fefbb89aaa0d1f6b713_Out_0_Float = _Smoothness;
        float _Property_dce33fc56d0e4204bd34d323af11f8ca_Out_0_Float = _Micro_Occlusion;
        float _Multiply_d120aee0dde541e69ff32688ddb2bee0_Out_2_Float;
        Unity_Multiply_float_float(_Property_dce33fc56d0e4204bd34d323af11f8ca_Out_0_Float, 0.5, _Multiply_d120aee0dde541e69ff32688ddb2bee0_Out_2_Float);
        float _Lerp_6742a96ab4984154ae535aef42b348f7_Out_3_Float;
        Unity_Lerp_float(float(0), float(0.08), _Multiply_d120aee0dde541e69ff32688ddb2bee0_Out_2_Float, _Lerp_6742a96ab4984154ae535aef42b348f7_Out_3_Float);
        Bindings_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d;
        _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d.WorldSpaceNormal = IN.WorldSpaceNormal;
        _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        half3 _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3;
        SG_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float(_Property_58fa7de1b7784467935169b8914ee373_Out_0_Vector3, _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_433019e2d18944a2909e58d06f7cc1ec_Out_0_Float, _Property_dd950e92d2d54fefbb89aaa0d1f6b713_Out_0_Float, _Lerp_6742a96ab4984154ae535aef42b348f7_Out_3_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3);
        Bindings_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3;
        _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3.WorldSpaceNormal = IN.WorldSpaceNormal;
        _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3_Specular_1_Vector3;
        SG_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float(_BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_ac10139ecedb4301b4595fa5b13c00b8_Out_0_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3, _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3, true, _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3, _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3_Specular_1_Vector3);
        float3 _Multiply_7b7e19c594534a458849928b4dc06aec_Out_2_Vector3;
        Unity_Multiply_float3_float3((_DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float.xxx), _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3_Specular_1_Vector3, _Multiply_7b7e19c594534a458849928b4dc06aec_Out_2_Vector3);
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e;
        _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Direction_1_Vector3;
        half3 _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Color_2_Vector3;
        half _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(_MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e, _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Direction_1_Vector3, _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Color_2_Vector3, _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_ShadowAtten_3_Float);
        float3 _Multiply_9545e2014858428792b92c57dd77e45c_Out_2_Vector3;
        Unity_Multiply_float3_float3(_MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Color_2_Vector3, (_MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_ShadowAtten_3_Float.xxx), _Multiply_9545e2014858428792b92c57dd77e45c_Out_2_Vector3);
        float _Property_be391ee1d2f24bada2da9fc9d603f6a9_Out_0_Float = _Smoothness;
        Bindings_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.WorldSpaceNormal = IN.WorldSpaceNormal;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.WorldSpacePosition = IN.WorldSpacePosition;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.NDCPosition = IN.NDCPosition;
        half _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Diffuse_1_Float;
        half3 _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Specular_2_Vector3;
        half3 _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Color_3_Vector3;
        SG_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float(_DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float, _Multiply_7b7e19c594534a458849928b4dc06aec_Out_2_Vector3, _Multiply_9545e2014858428792b92c57dd77e45c_Out_2_Vector3, _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_be391ee1d2f24bada2da9fc9d603f6a9_Out_0_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Diffuse_1_Float, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Specular_2_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Color_3_Vector3);
        float3 _Property_8804fec07c534721b9d4e6def9182fad_Out_0_Vector3 = _Base_Color;
        float3 _Multiply_765841b19beb4cecb786b3295b6aa8e9_Out_2_Vector3;
        Unity_Multiply_float3_float3((_AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Diffuse_1_Float.xxx), _Property_8804fec07c534721b9d4e6def9182fad_Out_0_Vector3, _Multiply_765841b19beb4cecb786b3295b6aa8e9_Out_2_Vector3);
        float3 _Add_77980781d2204e1b8d249e5f7058e9fb_Out_2_Vector3;
        Unity_Add_float3(_Multiply_765841b19beb4cecb786b3295b6aa8e9_Out_2_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Specular_2_Vector3, _Add_77980781d2204e1b8d249e5f7058e9fb_Out_2_Vector3);
        float3 _Multiply_9df1a4a5bf654e97b945b2eccf2fc855_Out_2_Vector3;
        Unity_Multiply_float3_float3(_Add_77980781d2204e1b8d249e5f7058e9fb_Out_2_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Color_3_Vector3, _Multiply_9df1a4a5bf654e97b945b2eccf2fc855_Out_2_Vector3);
        float3 _Property_e9725e93976b4fcaa5fea397628348dd_Out_0_Vector3 = _Base_Color;
        float _Property_e926bef11147490b98b69d5bec06eaa9_Out_0_Float = _Metallic;
        float _Property_1465a416fe734e4e83f2401b9c4d3fdb_Out_0_Float = _Smoothness;
        float _Property_0a056a259612407e813453c548affc50_Out_0_Float = _Ambient_Occlusion;
        Bindings_AmbientURP_300875fdd653fe340b08ad1547984cf1_float _AmbientURP_46e1712500da4aae848bd5b24a05f29f;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.WorldSpaceNormal = IN.WorldSpaceNormal;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.WorldSpacePosition = IN.WorldSpacePosition;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.NDCPosition = IN.NDCPosition;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.PixelPosition = IN.PixelPosition;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.uv1 = IN.uv1;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.uv2 = IN.uv2;
        half3 _AmbientURP_46e1712500da4aae848bd5b24a05f29f_Ambient_1_Vector3;
        half _AmbientURP_46e1712500da4aae848bd5b24a05f29f_DirectAO_2_Float;
        SG_AmbientURP_300875fdd653fe340b08ad1547984cf1_float(_Property_e9725e93976b4fcaa5fea397628348dd_Out_0_Vector3, _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_e926bef11147490b98b69d5bec06eaa9_Out_0_Float, _Property_1465a416fe734e4e83f2401b9c4d3fdb_Out_0_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3, _Property_0a056a259612407e813453c548affc50_Out_0_Float, _AmbientURP_46e1712500da4aae848bd5b24a05f29f, _AmbientURP_46e1712500da4aae848bd5b24a05f29f_Ambient_1_Vector3, _AmbientURP_46e1712500da4aae848bd5b24a05f29f_DirectAO_2_Float);
        float3 _Add_ad0b3eccabf24dceb374546c3c332ad7_Out_2_Vector3;
        Unity_Add_float3(_Multiply_9df1a4a5bf654e97b945b2eccf2fc855_Out_2_Vector3, _AmbientURP_46e1712500da4aae848bd5b24a05f29f_Ambient_1_Vector3, _Add_ad0b3eccabf24dceb374546c3c332ad7_Out_2_Vector3);
        Bindings_Fog_286ae83400099a24bba6faf005588be7_float _Fog_f4025f6ca9e74f948bc7263ef71d324a;
        _Fog_f4025f6ca9e74f948bc7263ef71d324a.ObjectSpacePosition = IN.ObjectSpacePosition;
        half3 _Fog_f4025f6ca9e74f948bc7263ef71d324a_Out_1_Vector3;
        SG_Fog_286ae83400099a24bba6faf005588be7_float(_Add_ad0b3eccabf24dceb374546c3c332ad7_Out_2_Vector3, _Fog_f4025f6ca9e74f948bc7263ef71d324a, _Fog_f4025f6ca9e74f948bc7263ef71d324a_Out_1_Vector3);
        float3 _Saturate_b798de6279164a2b9155cd8251d26092_Out_1_Vector3;
        Unity_Saturate_float3(_Fog_f4025f6ca9e74f948bc7263ef71d324a_Out_1_Vector3, _Saturate_b798de6279164a2b9155cd8251d26092_Out_1_Vector3);
        float3 _Saturation_ad4a69e303864b54940190442ea5d857_Out_2_Vector3;
        Unity_Saturation_float(_Saturate_b798de6279164a2b9155cd8251d26092_Out_1_Vector3, float(0), _Saturation_ad4a69e303864b54940190442ea5d857_Out_2_Vector3);
        float _Swizzle_bae47f72b28a4941b7665012b9c55203_Out_1_Float = (_Saturation_ad4a69e303864b54940190442ea5d857_Out_2_Vector3).x.x;
        float _Power_83030291203f43f9b048356e4fbaf99e_Out_2_Float;
        Unity_Power_float(_Swizzle_bae47f72b28a4941b7665012b9c55203_Out_1_Float, float(0.45), _Power_83030291203f43f9b048356e4fbaf99e_Out_2_Float);
        float3 _Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3;
        Unity_Contrast_float((_Power_83030291203f43f9b048356e4fbaf99e_Out_2_Float.xxx), float(2), _Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3);
        float2 _Property_7fc6298ec06c474b99191c5a5156da72_Out_0_Vector2 = _Color_A_Location;
        float3 _Remap_6d4565d12b3a4f71b23e18189b7389b3_Out_3_Vector3;
        Unity_Remap_float3(_Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3, _Property_7fc6298ec06c474b99191c5a5156da72_Out_0_Vector2, float2 (0, 1), _Remap_6d4565d12b3a4f71b23e18189b7389b3_Out_3_Vector3);
        float3 _Saturate_2728e5e4642645239b2f9a703cc1622a_Out_1_Vector3;
        Unity_Saturate_float3(_Remap_6d4565d12b3a4f71b23e18189b7389b3_Out_3_Vector3, _Saturate_2728e5e4642645239b2f9a703cc1622a_Out_1_Vector3);
        float3 _Lerp_20eaa2d06da4491384cac8b19ba49f2c_Out_3_Vector3;
        Unity_Lerp_float3((_Property_bedee92e97ce4b2abff5524ce019b2a8_Out_0_Vector4.xyz), (_Property_c648ff794fd34283beff09e33d8293fc_Out_0_Vector4.xyz), _Saturate_2728e5e4642645239b2f9a703cc1622a_Out_1_Vector3, _Lerp_20eaa2d06da4491384cac8b19ba49f2c_Out_3_Vector3);
        float4 _Property_fd1357d1b32e4a29a4cbcc3613f244e3_Out_0_Vector4 = _Color_C;
        float2 _Property_c2df701a69af4187a31c6f8cfcb26846_Out_0_Vector2 = _Color_B_Location;
        float3 _Remap_0c80541bb2da437698e33d379210a687_Out_3_Vector3;
        Unity_Remap_float3(_Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3, _Property_c2df701a69af4187a31c6f8cfcb26846_Out_0_Vector2, float2 (0, 1), _Remap_0c80541bb2da437698e33d379210a687_Out_3_Vector3);
        float3 _Saturate_5b42e55c7d0c45f1a92ce0c1c045003a_Out_1_Vector3;
        Unity_Saturate_float3(_Remap_0c80541bb2da437698e33d379210a687_Out_3_Vector3, _Saturate_5b42e55c7d0c45f1a92ce0c1c045003a_Out_1_Vector3);
        float3 _Lerp_b1cbf1c8cc0549a69912223d40ba4cfa_Out_3_Vector3;
        Unity_Lerp_float3(_Lerp_20eaa2d06da4491384cac8b19ba49f2c_Out_3_Vector3, (_Property_fd1357d1b32e4a29a4cbcc3613f244e3_Out_0_Vector4.xyz), _Saturate_5b42e55c7d0c45f1a92ce0c1c045003a_Out_1_Vector3, _Lerp_b1cbf1c8cc0549a69912223d40ba4cfa_Out_3_Vector3);
        Lit_1 = _Lerp_b1cbf1c8cc0549a69912223d40ba4cfa_Out_3_Vector3;
        }
        
        // unity-custom-func-begin
        void DebugMaterialSwitch_float(float3 None, float3 Albedo, float3 Specular, float3 Alpha, float3 Smoothness, float3 AmbientOcclusion, float3 Emission, float3 NormalWS, float3 NormalTS, float3 LightComplexity, float3 Metallic, float3 SpriteMask, float3 RenderingLayerMasks, out float3 Out){
        Out = None;
        #if !defined(SHADERGRAPH_PREVIEW) && defined(DEBUG_DISPLAY)
        [branch] switch(int(_DebugMaterialMode))
        
        {
        
            case 0:
        
                Out = None; break;
        
            case 1:
        
                Out = Albedo; break;
        
            case 2:
        
                Out = Specular; break;
            case 3:
        
                Out = Alpha; break;
            case 4:
        
                Out = Smoothness; break;
            case 5:
        
                Out = AmbientOcclusion;  break;
            case 6:
        
                Out = Emission;  break;
            case 7:
        
                Out = NormalWS * 0.5 + 0.5;  break;
            case 8:
        
                Out = NormalTS * 0.5 + 0.5;  break;
            case 9:
        
                Out = LightComplexity;  break;
            case 10:
        
                Out = Metallic;  break;
            case 11:
        
                Out = SpriteMask;  break;
            case 12:
        
                Out = RenderingLayerMasks;  break;
        
            default:
        
                Out = None; break;
        
        }
        #endif
        
        // Disable this define to prevent the global unlit
        // fragment pass to override the color output again.
        #undef DEBUG_DISPLAY
        }
        // unity-custom-func-end
        
        struct Bindings_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        };
        
        void SG_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float(float3 _In, float3 _Base_Color, float3 _Normal, float _Metallic, float _Smoothness, float3 _Emission, float _Ambient_Occlusion, float _Alpha, Bindings_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float IN, out float3 Out_1)
        {
        float3 _Property_dd011cc96ae64d1181317986b1fa1742_Out_0_Vector3 = _In;
        float3 _Property_5653941ce5a641f18f7ce7012652025d_Out_0_Vector3 = _Base_Color;
        float _Property_45f5c13ff5544581bd61c2442cecd0a1_Out_0_Float = _Alpha;
        float _Property_b6c8b448c5324bd3bc59540f628e43a3_Out_0_Float = _Smoothness;
        Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5;
        _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5.NDCPosition = IN.NDCPosition;
        half _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_indirectAO_1_Float;
        half _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_directAO_2_Float;
        SG_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float(_ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5, _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_indirectAO_1_Float, _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_directAO_2_Float);
        float _Property_441143660ff642349088dd1bcab6bc78_Out_0_Float = _Ambient_Occlusion;
        float _Minimum_8ee95b9bf7ac4776b6ee4edf1214b3c1_Out_2_Float;
        Unity_Minimum_float(_ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_indirectAO_1_Float, _Property_441143660ff642349088dd1bcab6bc78_Out_0_Float, _Minimum_8ee95b9bf7ac4776b6ee4edf1214b3c1_Out_2_Float);
        float3 _Property_b171431b5a3b4b0a9fc9fdede4a532a7_Out_0_Vector3 = _Emission;
        float3 _Property_db9eb36ed51d4aad95e383920b55e3d7_Out_0_Vector3 = _Normal;
        float3 _Transform_9e831bda1f4d4495b24f1f6e3075f2fb_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_9e831bda1f4d4495b24f1f6e3075f2fb_Out_1_Vector3 = TransformTangentToWorld(_Property_db9eb36ed51d4aad95e383920b55e3d7_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float3 _Property_4eaab22b2b784aeda3752622f7abaf85_Out_0_Vector3 = _Normal;
        float4 _ScreenPosition_121436dfdd464829910775b2326b046b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float3 _Property_1b1e0a48277e4883afeb1289a075c5d8_Out_0_Vector3 = _Base_Color;
        float3 _LightingComplexityCustomFunction_cbe0c0f96f9046a584e17ead8c001a55_Out_3_Vector3;
        LightingComplexity_float((_ScreenPosition_121436dfdd464829910775b2326b046b_Out_0_Vector4.xy), IN.WorldSpacePosition, _Property_1b1e0a48277e4883afeb1289a075c5d8_Out_0_Vector3, _LightingComplexityCustomFunction_cbe0c0f96f9046a584e17ead8c001a55_Out_3_Vector3);
        float _Property_dcd3ca7796af45c6857884fa7979898b_Out_0_Float = _Metallic;
        float3 _DebugMaterialSwitchCustomFunction_e1fabc2a3bcd4bc183f0e93c379657d4_Out_5_Vector3;
        DebugMaterialSwitch_float(_Property_dd011cc96ae64d1181317986b1fa1742_Out_0_Vector3, _Property_5653941ce5a641f18f7ce7012652025d_Out_0_Vector3, float3 (0, 0, 0), (_Property_45f5c13ff5544581bd61c2442cecd0a1_Out_0_Float.xxx), (_Property_b6c8b448c5324bd3bc59540f628e43a3_Out_0_Float.xxx), (_Minimum_8ee95b9bf7ac4776b6ee4edf1214b3c1_Out_2_Float.xxx), _Property_b171431b5a3b4b0a9fc9fdede4a532a7_Out_0_Vector3, _Transform_9e831bda1f4d4495b24f1f6e3075f2fb_Out_1_Vector3, _Property_4eaab22b2b784aeda3752622f7abaf85_Out_0_Vector3, _LightingComplexityCustomFunction_cbe0c0f96f9046a584e17ead8c001a55_Out_3_Vector3, (_Property_dcd3ca7796af45c6857884fa7979898b_Out_0_Float.xxx), float3 (0, 0, 0), float3 (0, 0, 0), _DebugMaterialSwitchCustomFunction_e1fabc2a3bcd4bc183f0e93c379657d4_Out_5_Vector3);
        Out_1 = _DebugMaterialSwitchCustomFunction_e1fabc2a3bcd4bc183f0e93c379657d4_Out_5_Vector3;
        }
        
        struct Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpaceViewDirection;
        float3 ObjectSpacePosition;
        float3 WorldSpacePosition;
        float3 AbsoluteWorldSpacePosition;
        float2 NDCPosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float(float3 _Base_Color, float3 _Normal, float _Metallic, float _Smoothness, float3 _Emission, float _AmbientOcclusion, float _Alpha, float4 _Color_A, float2 _Color_A_Location, float4 _Color_B, float2 _Color_B_Location, float4 _Color_C, Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float IN, out float3 Lit_1)
        {
        float3 _Property_04a055764411443d802bfbbd0d510c65_Out_0_Vector3 = _Base_Color;
        float3 _Property_383a017d83a8420dac016260bc833f58_Out_0_Vector3 = _Normal;
        float3 _Transform_3f94cf9dbe844abc9b6727d5d289074f_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_3f94cf9dbe844abc9b6727d5d289074f_Out_1_Vector3 = TransformTangentToWorld(_Property_383a017d83a8420dac016260bc833f58_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float _Property_11295d868ff34c388c9212b90b781aff_Out_0_Float = _Metallic;
        float _Property_b522b61b85ff4ecbb0eb63cff689f5cb_Out_0_Float = _Smoothness;
        float _Property_a1dc37a47c5640d0870861199df0bd70_Out_0_Float = _AmbientOcclusion;
        Bindings_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float _ApplyDecals_0413903f5da5491d911d117142eabddd;
        _ApplyDecals_0413903f5da5491d911d117142eabddd.PixelPosition = IN.PixelPosition;
        half3 _ApplyDecals_0413903f5da5491d911d117142eabddd_BaseColor_1_Vector3;
        half3 _ApplyDecals_0413903f5da5491d911d117142eabddd_SpecularColor_2_Vector3;
        half3 _ApplyDecals_0413903f5da5491d911d117142eabddd_NormalWS_3_Vector3;
        half _ApplyDecals_0413903f5da5491d911d117142eabddd_Metallic_4_Float;
        half _ApplyDecals_0413903f5da5491d911d117142eabddd_Smoothness_6_Float;
        half _ApplyDecals_0413903f5da5491d911d117142eabddd_AmbientOcclusion_5_Float;
        SG_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float(_Property_04a055764411443d802bfbbd0d510c65_Out_0_Vector3, _Transform_3f94cf9dbe844abc9b6727d5d289074f_Out_1_Vector3, _Property_11295d868ff34c388c9212b90b781aff_Out_0_Float, _Property_b522b61b85ff4ecbb0eb63cff689f5cb_Out_0_Float, _Property_a1dc37a47c5640d0870861199df0bd70_Out_0_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd, _ApplyDecals_0413903f5da5491d911d117142eabddd_BaseColor_1_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_SpecularColor_2_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_NormalWS_3_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_Metallic_4_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd_Smoothness_6_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd_AmbientOcclusion_5_Float);
        float3 _Property_b986326ad9b34d6ea3a7237ba2bd1cd6_Out_0_Vector3 = _Emission;
        Bindings_DebugLighting_61e571d2b9ede1240a524a849d20c997_float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpaceNormal = IN.WorldSpaceNormal;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpaceTangent = IN.WorldSpaceTangent;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpacePosition = IN.WorldSpacePosition;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.PixelPosition = IN.PixelPosition;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.uv1 = IN.uv1;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.uv2 = IN.uv2;
        float3 _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3;
        float3 _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3;
        float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float;
        float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float;
        float3 _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3;
        float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float;
        SG_DebugLighting_61e571d2b9ede1240a524a849d20c997_float(_ApplyDecals_0413903f5da5491d911d117142eabddd_BaseColor_1_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_NormalWS_3_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_Metallic_4_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd_Smoothness_6_Float, _Property_b986326ad9b34d6ea3a7237ba2bd1cd6_Out_0_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_AmbientOcclusion_5_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float);
        float4 _Property_a2c93b67d7e14184996710181bc8106a_Out_0_Vector4 = _Color_A;
        float2 _Property_30bd0eecac2f497db8c8b272e8e7d3e5_Out_0_Vector2 = _Color_A_Location;
        float4 _Property_47fc9a397b1241599709d29487238203_Out_0_Vector4 = _Color_B;
        float2 _Property_99bf1a52083e4b7f84197e960ed6a728_Out_0_Vector2 = _Color_B_Location;
        float4 _Property_d5f33cf319a54be08f26ec7c7538d6a4_Out_0_Vector4 = _Color_C;
        Bindings_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceNormal = IN.WorldSpaceNormal;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceTangent = IN.WorldSpaceTangent;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.ObjectSpacePosition = IN.ObjectSpacePosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpacePosition = IN.WorldSpacePosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.NDCPosition = IN.NDCPosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.PixelPosition = IN.PixelPosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.uv1 = IN.uv1;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.uv2 = IN.uv2;
        half3 _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc_Lit_1_Vector3;
        SG_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float(_DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3, true, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float, half(1), _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float, _Property_a2c93b67d7e14184996710181bc8106a_Out_0_Vector4, _Property_30bd0eecac2f497db8c8b272e8e7d3e5_Out_0_Vector2, _Property_47fc9a397b1241599709d29487238203_Out_0_Vector4, _Property_99bf1a52083e4b7f84197e960ed6a728_Out_0_Vector2, _Property_d5f33cf319a54be08f26ec7c7538d6a4_Out_0_Vector4, _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc, _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc_Lit_1_Vector3);
        float3 _Add_a11e24e7d4fd4494895fd67f375acb21_Out_2_Vector3;
        Unity_Add_float3(_SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc_Lit_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3, _Add_a11e24e7d4fd4494895fd67f375acb21_Out_2_Vector3);
        float _Property_d5e8251fc84a46aea1765511445b653e_Out_0_Float = _Alpha;
        Bindings_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpaceNormal = IN.WorldSpaceNormal;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpaceTangent = IN.WorldSpaceTangent;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpacePosition = IN.WorldSpacePosition;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.NDCPosition = IN.NDCPosition;
        float3 _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3_Out_1_Vector3;
        SG_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float(_Add_a11e24e7d4fd4494895fd67f375acb21_Out_2_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float, _Property_d5e8251fc84a46aea1765511445b653e_Out_0_Float, _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3, _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3_Out_1_Vector3);
        Lit_1 = _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3_Out_1_Vector3;
        }
        
        void Unity_Blend_Overwrite_float3(float3 Base, float3 Blend, out float3 Out, float Opacity)
        {
            Out = lerp(Base, Blend, Opacity);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float3 BaseColor;
            float3 NormalTS;
            float3 Emission;
            float Metallic;
            float3 Specular;
            float Smoothness;
            float Occlusion;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_debbfbf1a581455dbc61b338f851a8d4_Out_0_Boolean = _DisableAllGradients;
            float _Property_2fb138ca7c89409da2d3e517c9bcb36b_Out_0_Boolean = _DisableGradientMap;
            UnityTexture2D _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D = UnityBuildTexture2DStructInternal(_BaseColor, sampler_BaseColor, _BaseColor_TexelSize, float4(1, 1, 0, 0), float4(0, 0, 0, 0));
            float2 _Property_94094af0818841fea1396a1ccae7a167_Out_0_Vector2 = _Tilling;
            float2 _TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2;
            Unity_TilingAndOffset_float(IN.uv0.xy, _Property_94094af0818841fea1396a1ccae7a167_Out_0_Vector2, float2 (0, 0), _TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2);
            float4 _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.tex, _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.samplerstate, _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.GetTransformedUV(_TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2) );
            if (_Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.hdrDecode.x > 0)
                _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4, _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.hdrDecode);
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.r;
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_G_5_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.g;
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_B_6_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.b;
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_A_7_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.a;
            float4 _Property_de1bd6122d6c4aa4ba9d7692e6db8956_Out_0_Vector4 = _Color1;
            float4 _Property_fceba521f35a4cc88bbd9602ff68242f_Out_0_Vector4 = _Color2;
            float2 _Property_e60b8198308f4aad8dfa7a52168790ce_Out_0_Vector2 = _Color_1_Location;
            float _Remap_b9f81469352045d7ae4bb207f0c202fc_Out_3_Float;
            Unity_Remap_float(_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float, _Property_e60b8198308f4aad8dfa7a52168790ce_Out_0_Vector2, float2 (0, 1), _Remap_b9f81469352045d7ae4bb207f0c202fc_Out_3_Float);
            float _Saturate_473bb6d59e074412ad7162adcfe4cef1_Out_1_Float;
            Unity_Saturate_float(_Remap_b9f81469352045d7ae4bb207f0c202fc_Out_3_Float, _Saturate_473bb6d59e074412ad7162adcfe4cef1_Out_1_Float);
            float4 _Lerp_f3e294dc316840b6b4294bcb3e7b3f7b_Out_3_Vector4;
            Unity_Lerp_float4(_Property_de1bd6122d6c4aa4ba9d7692e6db8956_Out_0_Vector4, _Property_fceba521f35a4cc88bbd9602ff68242f_Out_0_Vector4, (_Saturate_473bb6d59e074412ad7162adcfe4cef1_Out_1_Float.xxxx), _Lerp_f3e294dc316840b6b4294bcb3e7b3f7b_Out_3_Vector4);
            float4 _Property_60ebe0bb4a0e4a6fbd758422fbc8e1af_Out_0_Vector4 = _Color3;
            float2 _Property_46eb955dacc0426abc5d73b1be33af42_Out_0_Vector2 = _Color_2_Location;
            float _Remap_c2e999a017a6490b91665566afc5916d_Out_3_Float;
            Unity_Remap_float(_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float, _Property_46eb955dacc0426abc5d73b1be33af42_Out_0_Vector2, float2 (0, 1), _Remap_c2e999a017a6490b91665566afc5916d_Out_3_Float);
            float _Saturate_72fb61dd2f064241a5337c41de422b36_Out_1_Float;
            Unity_Saturate_float(_Remap_c2e999a017a6490b91665566afc5916d_Out_3_Float, _Saturate_72fb61dd2f064241a5337c41de422b36_Out_1_Float);
            float4 _Lerp_72f03acdde2245b29f8cd4a47d6b4cd6_Out_3_Vector4;
            Unity_Lerp_float4(_Lerp_f3e294dc316840b6b4294bcb3e7b3f7b_Out_3_Vector4, _Property_60ebe0bb4a0e4a6fbd758422fbc8e1af_Out_0_Vector4, (_Saturate_72fb61dd2f064241a5337c41de422b36_Out_1_Float.xxxx), _Lerp_72f03acdde2245b29f8cd4a47d6b4cd6_Out_3_Vector4);
            float4 _Property_e84e577bc7db46749ec6367493b51e06_Out_0_Vector4 = _AOColor;
            UnityTexture2D _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D = UnityBuildTexture2DStructInternal(_ORM, sampler_ORM, _ORM_TexelSize, float4(1, 1, 0, 0), float4(0, 0, 0, 0));
            float4 _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.tex, _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.samplerstate, _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.GetTransformedUV(_TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2) );
            if (_Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.hdrDecode.x > 0)
                _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4, _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.hdrDecode);
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_R_4_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.r;
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_G_5_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.g;
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.b;
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_A_7_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.a;
            float _OneMinus_89dcf9413d9142c481cf9d5e5035b712_Out_1_Float;
            Unity_OneMinus_float(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_R_4_Float, _OneMinus_89dcf9413d9142c481cf9d5e5035b712_Out_1_Float);
            float2 _Property_753b87b3c47d403696efc934ea3dbea9_Out_0_Vector2 = _AOLevels;
            float _Remap_983e82311f6e4fe5b382c71d656497ec_Out_3_Float;
            Unity_Remap_float(_OneMinus_89dcf9413d9142c481cf9d5e5035b712_Out_1_Float, _Property_753b87b3c47d403696efc934ea3dbea9_Out_0_Vector2, float2 (1, 0), _Remap_983e82311f6e4fe5b382c71d656497ec_Out_3_Float);
            float _Property_ba1b5b1e95414eaebf54a9d26291e91f_Out_0_Float = _AOIntensity;
            float _Multiply_2341d6d389c44e96bba10c0c1e0c9f14_Out_2_Float;
            Unity_Multiply_float_float(_Remap_983e82311f6e4fe5b382c71d656497ec_Out_3_Float, _Property_ba1b5b1e95414eaebf54a9d26291e91f_Out_0_Float, _Multiply_2341d6d389c44e96bba10c0c1e0c9f14_Out_2_Float);
            float4 _Lerp_8d7f6575739049459e47aaca25cf59d0_Out_3_Vector4;
            Unity_Lerp_float4(_Lerp_72f03acdde2245b29f8cd4a47d6b4cd6_Out_3_Vector4, _Property_e84e577bc7db46749ec6367493b51e06_Out_0_Vector4, (_Multiply_2341d6d389c44e96bba10c0c1e0c9f14_Out_2_Float.xxxx), _Lerp_8d7f6575739049459e47aaca25cf59d0_Out_3_Vector4);
            float4 _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4;
            Unity_Branch_float4(_Property_2fb138ca7c89409da2d3e517c9bcb36b_Out_0_Boolean, (_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float.xxxx), _Lerp_8d7f6575739049459e47aaca25cf59d0_Out_3_Vector4, _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4);
            float _Property_18ebff8ae81646538dc78b9020d39b78_Out_0_Boolean = _Z_Gradient;
            float _Property_f94879ac790e4c109a34ec1f73a3c3a6_Out_0_Boolean = _Y_Gradient;
            float _Property_c61e8f7030cd47ac9b641cd98ca92fe3_Out_0_Boolean = _X_Gradient;
            float4 _Property_1f3e0b1c283f4c949004f43f37fe1a90_Out_0_Vector4 = _X_GradientColor;
            float _Property_94b16f357eac4058809921fa96f34787_Out_0_Float = _X_GradientStartPosition;
            float _Property_8c3dacdcc9cc4b2880b33b1d1913e9f1_Out_0_Float = _X_GradientEndPosition;
            float _Split_2ca42922e29b49b4b7113632901be932_R_1_Float = IN.AbsoluteWorldSpacePosition[0];
            float _Split_2ca42922e29b49b4b7113632901be932_G_2_Float = IN.AbsoluteWorldSpacePosition[1];
            float _Split_2ca42922e29b49b4b7113632901be932_B_3_Float = IN.AbsoluteWorldSpacePosition[2];
            float _Split_2ca42922e29b49b4b7113632901be932_A_4_Float = 0;
            float _Smoothstep_c73aebae28cd43fc896795bd80911919_Out_3_Float;
            Unity_Smoothstep_float(_Property_94b16f357eac4058809921fa96f34787_Out_0_Float, _Property_8c3dacdcc9cc4b2880b33b1d1913e9f1_Out_0_Float, _Split_2ca42922e29b49b4b7113632901be932_R_1_Float, _Smoothstep_c73aebae28cd43fc896795bd80911919_Out_3_Float);
            float _Property_dd9c997bc2514f2bbe579af0da9fecb2_Out_0_Float = _X_GradientIntensity;
            float _Multiply_961d468ad0db4c8ebb883194aa2c7cf5_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_c73aebae28cd43fc896795bd80911919_Out_3_Float, _Property_dd9c997bc2514f2bbe579af0da9fecb2_Out_0_Float, _Multiply_961d468ad0db4c8ebb883194aa2c7cf5_Out_2_Float);
            float4 _Lerp_ba89b24cd32049a0964667d354c52dcc_Out_3_Vector4;
            Unity_Lerp_float4(_Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4, _Property_1f3e0b1c283f4c949004f43f37fe1a90_Out_0_Vector4, (_Multiply_961d468ad0db4c8ebb883194aa2c7cf5_Out_2_Float.xxxx), _Lerp_ba89b24cd32049a0964667d354c52dcc_Out_3_Vector4);
            float4 _Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4;
            Unity_Branch_float4(_Property_c61e8f7030cd47ac9b641cd98ca92fe3_Out_0_Boolean, _Lerp_ba89b24cd32049a0964667d354c52dcc_Out_3_Vector4, _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4, _Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4);
            float4 _Property_bbb56023a68b4c17bbaa88d52a24ab64_Out_0_Vector4 = _Y_GradientColor;
            float _Property_e53d4551d24740e89ab9d2dde9d07fa9_Out_0_Float = _Y_GradientStartPosition;
            float _Property_f705a7a1ffdc47a388bfa2aa340dd71f_Out_0_Float = _Y_GradientEndPosition;
            float _Smoothstep_8f0d8c12283b443dabc9178711fca6ea_Out_3_Float;
            Unity_Smoothstep_float(_Property_e53d4551d24740e89ab9d2dde9d07fa9_Out_0_Float, _Property_f705a7a1ffdc47a388bfa2aa340dd71f_Out_0_Float, _Split_2ca42922e29b49b4b7113632901be932_G_2_Float, _Smoothstep_8f0d8c12283b443dabc9178711fca6ea_Out_3_Float);
            float _Property_c3a47369d5bb47ddb5fcb74791e32d8d_Out_0_Float = _Y_GradientIntensity;
            float _Multiply_0b75652c94cc4e928c2a6eda4bc65145_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_8f0d8c12283b443dabc9178711fca6ea_Out_3_Float, _Property_c3a47369d5bb47ddb5fcb74791e32d8d_Out_0_Float, _Multiply_0b75652c94cc4e928c2a6eda4bc65145_Out_2_Float);
            float4 _Lerp_2023214161dd495a9df4a86e9aab0a55_Out_3_Vector4;
            Unity_Lerp_float4(_Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4, _Property_bbb56023a68b4c17bbaa88d52a24ab64_Out_0_Vector4, (_Multiply_0b75652c94cc4e928c2a6eda4bc65145_Out_2_Float.xxxx), _Lerp_2023214161dd495a9df4a86e9aab0a55_Out_3_Vector4);
            float4 _Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4;
            Unity_Branch_float4(_Property_f94879ac790e4c109a34ec1f73a3c3a6_Out_0_Boolean, _Lerp_2023214161dd495a9df4a86e9aab0a55_Out_3_Vector4, _Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4, _Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4);
            float4 _Property_d20a45cfc0cb48e4a9823f3125a60fb7_Out_0_Vector4 = _Y_GradientColor;
            float _Property_a3a6fd6a552845c797564be4a2b63e5d_Out_0_Float = _Z_GradientStartPosition;
            float _Property_1f6239cf046944d6b4f70da4fca83661_Out_0_Float = _Z_GradientEndPosition;
            float _Smoothstep_9cb470a94c1b4410b6282b2c261d462d_Out_3_Float;
            Unity_Smoothstep_float(_Property_a3a6fd6a552845c797564be4a2b63e5d_Out_0_Float, _Property_1f6239cf046944d6b4f70da4fca83661_Out_0_Float, _Split_2ca42922e29b49b4b7113632901be932_B_3_Float, _Smoothstep_9cb470a94c1b4410b6282b2c261d462d_Out_3_Float);
            float _Property_1ac1a7f19f6342f0a029129f1adbfd67_Out_0_Float = _Z_GradientIntensity;
            float _Multiply_d258a35ceccb4063a2594879737f8bb1_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_9cb470a94c1b4410b6282b2c261d462d_Out_3_Float, _Property_1ac1a7f19f6342f0a029129f1adbfd67_Out_0_Float, _Multiply_d258a35ceccb4063a2594879737f8bb1_Out_2_Float);
            float4 _Lerp_5d732be3cb674212b3307c0a98052b15_Out_3_Vector4;
            Unity_Lerp_float4(_Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4, _Property_d20a45cfc0cb48e4a9823f3125a60fb7_Out_0_Vector4, (_Multiply_d258a35ceccb4063a2594879737f8bb1_Out_2_Float.xxxx), _Lerp_5d732be3cb674212b3307c0a98052b15_Out_3_Vector4);
            float4 _Branch_808c9eecf2bc4ac09cea8d7293c4d22a_Out_3_Vector4;
            Unity_Branch_float4(_Property_18ebff8ae81646538dc78b9020d39b78_Out_0_Boolean, _Lerp_5d732be3cb674212b3307c0a98052b15_Out_3_Vector4, _Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4, _Branch_808c9eecf2bc4ac09cea8d7293c4d22a_Out_3_Vector4);
            float4 _Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4;
            Unity_Branch_float4(_Property_debbfbf1a581455dbc61b338f851a8d4_Out_0_Boolean, _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4, _Branch_808c9eecf2bc4ac09cea8d7293c4d22a_Out_3_Vector4, _Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4);
            UnityTexture2D _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D = UnityBuildTexture2DStructInternal(_Normal, sampler_Normal, _Normal_TexelSize, float4(1, 1, 0, 0), float4(0, 0, 0, 0));
            float4 _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.tex, _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.samplerstate, _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.GetTransformedUV(_TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2) );
            if (_Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.hdrDecode.x > 0)
                _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4, _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.hdrDecode);
            _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.rgb = UnpackNormal(_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4);
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_R_4_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.r;
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_G_5_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.g;
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_B_6_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.b;
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_A_7_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.a;
            float _Property_3c169350af004240a8c8543dce8c320b_Out_0_Boolean = _UseCustomRoughness;
            float _Property_5c2d908353e24a3692fb0e08fe229355_Out_0_Float = _CustomRoughness;
            float _OneMinus_59ec04ea5505409baa7bf818ec2e91cd_Out_1_Float;
            Unity_OneMinus_float(_Property_5c2d908353e24a3692fb0e08fe229355_Out_0_Float, _OneMinus_59ec04ea5505409baa7bf818ec2e91cd_Out_1_Float);
            float _Property_a19c0c9a848947f4aab57660a9a18f93_Out_0_Float = _RoughnessMultiplier;
            float _Multiply_17be8d0efec54ca3bc59e321b0c596ee_Out_2_Float;
            Unity_Multiply_float_float(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_G_5_Float, _Property_a19c0c9a848947f4aab57660a9a18f93_Out_0_Float, _Multiply_17be8d0efec54ca3bc59e321b0c596ee_Out_2_Float);
            float _Saturate_04d0090531f74a9ebdada50fd6f42af5_Out_1_Float;
            Unity_Saturate_float(_Multiply_17be8d0efec54ca3bc59e321b0c596ee_Out_2_Float, _Saturate_04d0090531f74a9ebdada50fd6f42af5_Out_1_Float);
            float _OneMinus_c9932877d03347048da7ba94008426d5_Out_1_Float;
            Unity_OneMinus_float(_Saturate_04d0090531f74a9ebdada50fd6f42af5_Out_1_Float, _OneMinus_c9932877d03347048da7ba94008426d5_Out_1_Float);
            float _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float;
            Unity_Branch_float(_Property_3c169350af004240a8c8543dce8c320b_Out_0_Boolean, _OneMinus_59ec04ea5505409baa7bf818ec2e91cd_Out_1_Float, _OneMinus_c9932877d03347048da7ba94008426d5_Out_1_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float);
            float _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float;
            Unity_Saturate_float(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_R_4_Float, _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float);
            float4 _Property_34dac06f3b7442efa3e05c85b09ec445_Out_0_Vector4 = _ACT1_Color_A;
            float2 _Property_b3a982240db24cfbbc14de979535b458_Out_0_Vector2 = _ACT1_Color_A_Location;
            float4 _Property_0e317fd277254f1aae079db6e8d2e8dc_Out_0_Vector4 = _ACT1_Color_B;
            float2 _Property_3187bf5304c54604a1d464e65a9dac03_Out_0_Vector2 = _ACT1_Color_B_Location;
            float4 _Property_41e2d595ca9d41dd8806c1a749a3bb43_Out_0_Vector4 = _ACT1_Color_C;
            Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceNormal = IN.WorldSpaceNormal;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceTangent = IN.WorldSpaceTangent;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.ObjectSpacePosition = IN.ObjectSpacePosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpacePosition = IN.WorldSpacePosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.NDCPosition = IN.NDCPosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.PixelPosition = IN.PixelPosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.uv1 = IN.uv1;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.uv2 = IN.uv2;
            float3 _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d_Lit_1_Vector3;
            SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), (_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.xyz), _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float, float3 (0, 0, 0), _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float, float(1), _Property_34dac06f3b7442efa3e05c85b09ec445_Out_0_Vector4, _Property_b3a982240db24cfbbc14de979535b458_Out_0_Vector2, _Property_0e317fd277254f1aae079db6e8d2e8dc_Out_0_Vector4, _Property_3187bf5304c54604a1d464e65a9dac03_Out_0_Vector2, _Property_41e2d595ca9d41dd8806c1a749a3bb43_Out_0_Vector4, _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d, _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d_Lit_1_Vector3);
            float4 _Property_259fe0b41799487eb44d557cc24932a4_Out_0_Vector4 = _ACT2_Color_A;
            float2 _Property_83a57d16b4dc4f48985395b92f2de589_Out_0_Vector2 = _ACT2_Color_A_Location;
            float4 _Property_8ca39a18030a4bbd9348cb5b458a8372_Out_0_Vector4 = _ACT2_Color_B;
            float2 _Property_fa2809814b4148bd8ece170a87d230ef_Out_0_Vector2 = _ACT2_Color_B_Location;
            float4 _Property_c83f37081cf64cc7999c1bb19926d7c1_Out_0_Vector4 = _ACT2_Color_C;
            Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceNormal = IN.WorldSpaceNormal;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceTangent = IN.WorldSpaceTangent;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.ObjectSpacePosition = IN.ObjectSpacePosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpacePosition = IN.WorldSpacePosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.NDCPosition = IN.NDCPosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.PixelPosition = IN.PixelPosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.uv1 = IN.uv1;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.uv2 = IN.uv2;
            float3 _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370_Lit_1_Vector3;
            SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), (_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.xyz), _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float, float3 (0, 0, 0), _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float, float(1), _Property_259fe0b41799487eb44d557cc24932a4_Out_0_Vector4, _Property_83a57d16b4dc4f48985395b92f2de589_Out_0_Vector2, _Property_8ca39a18030a4bbd9348cb5b458a8372_Out_0_Vector4, _Property_fa2809814b4148bd8ece170a87d230ef_Out_0_Vector2, _Property_c83f37081cf64cc7999c1bb19926d7c1_Out_0_Vector4, _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370, _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370_Lit_1_Vector3);
            float4 _Property_13886bd11bee4d40b3eb7be3dff1c022_Out_0_Vector4 = _ACT3_Color_A;
            float2 _Property_5578cc8231d143d9a5f34ae24f110091_Out_0_Vector2 = _ACT3_Color_A_Location;
            float4 _Property_4d39077496e94d728ca3d19d42d3bd68_Out_0_Vector4 = _ACT3_Color_B;
            float2 _Property_5b1c9e69733942519a853e51dd4770f6_Out_0_Vector2 = _ACT3_Color_B_Location;
            float4 _Property_6a5550ba5eec4966850efea3418c844a_Out_0_Vector4 = _ACT3_Color_C;
            Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceNormal = IN.WorldSpaceNormal;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceTangent = IN.WorldSpaceTangent;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.ObjectSpacePosition = IN.ObjectSpacePosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpacePosition = IN.WorldSpacePosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.NDCPosition = IN.NDCPosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.PixelPosition = IN.PixelPosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.uv1 = IN.uv1;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.uv2 = IN.uv2;
            float3 _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a_Lit_1_Vector3;
            SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), (_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.xyz), _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float, float3 (0, 0, 0), _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float, float(1), _Property_13886bd11bee4d40b3eb7be3dff1c022_Out_0_Vector4, _Property_5578cc8231d143d9a5f34ae24f110091_Out_0_Vector2, _Property_4d39077496e94d728ca3d19d42d3bd68_Out_0_Vector4, _Property_5b1c9e69733942519a853e51dd4770f6_Out_0_Vector2, _Property_6a5550ba5eec4966850efea3418c844a_Out_0_Vector4, _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a, _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a_Lit_1_Vector3);
            float3 _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3;
            if (_CURRENTACT_ACT_1) _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3 = _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d_Lit_1_Vector3;
            else if (_CURRENTACT_ACT_2) _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3 = _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370_Lit_1_Vector3;
            else _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3 = _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a_Lit_1_Vector3;
            float _Property_854b1929338847e9b4a11e77fedb361b_Out_0_Float = _LightingGradientMapInfluence;
            float3 _Blend_ed10fcb74d224507bd92f8d23fb7e2d9_Out_2_Vector3;
            Unity_Blend_Overwrite_float3((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3, _Blend_ed10fcb74d224507bd92f8d23fb7e2d9_Out_2_Vector3, _Property_854b1929338847e9b4a11e77fedb361b_Out_0_Float);
            surface.BaseColor = _Blend_ed10fcb74d224507bd92f8d23fb7e2d9_Out_2_Vector3;
            surface.NormalTS = (_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.xyz);
            surface.Emission = float3(0, 0, 0);
            surface.Metallic = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float;
            surface.Specular = IsGammaSpace() ? float3(0.5, 0.5, 0.5) : SRGBToLinear(float3(0.5, 0.5, 0.5));
            surface.Smoothness = _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float;
            surface.Occlusion = _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float;
            surface.Alpha = float(1);
            surface.AlphaClipThreshold = float(0.5);
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
            // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            float3 unnormalizedNormalWS = input.normalWS;
            const float renormFactor = 1.0 / length(unnormalizedNormalWS);
        
            // use bitangent on the fly like in hdrp
            // IMPORTANT! If we ever support Flip on double sided materials ensure bitangent and tangent are NOT flipped.
            float crossSign = (input.tangentWS.w > 0.0 ? 1.0 : -1.0)* GetOddNegativeScale();
            float3 bitang = crossSign * cross(input.normalWS.xyz, input.tangentWS.xyz);
        
            output.WorldSpaceNormal = renormFactor * input.normalWS.xyz;      // we want a unit length Normal Vector node in shader graph
            output.TangentSpaceNormal = float3(0.0f, 0.0f, 1.0f);
        
            // to pr               eserve mikktspace compliance we use same scale renormFactor as was used on the normal.
            // This                is explained in section 2.2 in "surface gradient based bump mapping framework"
            output.WorldSpaceTangent = renormFactor * input.tangentWS.xyz;
            output.WorldSpaceBiTangent = renormFactor * bitang;
        
            output.WorldSpaceViewDirection = GetWorldSpaceNormalizeViewDir(input.positionWS);
            output.WorldSpacePosition = input.positionWS;
            output.ObjectSpacePosition = TransformWorldToObject(input.positionWS);
            output.AbsoluteWorldSpacePosition = GetAbsolutePositionWS(input.positionWS);
        
            #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x < 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #else
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x > 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScaledScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 = input.texCoord0;
            output.uv1 = input.texCoord1;
            output.uv2 = input.texCoord2;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/GBufferOutput.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/PBRGBufferPass.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/GBufferOutputFormat.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "ShadowCaster"
            Tags
            {
                "LightMode" = "ShadowCaster"
            }
        
        // Render State
        Cull [_Cull]
        ZTest LEqual
        ZWrite On
        ColorMask 0
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma multi_compile_instancing
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        #pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW
        #pragma shader_feature_local_fragment _ _ALPHATEST_ON
        #pragma shader_feature _CURRENTACT_ACT_1 _CURRENTACT_ACT_2 _CURRENTACT_ACT_3
        
        
        
        // Defines
        
        #define _NORMALMAP 1
        #define _NORMAL_DROPOFF_TS 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define FEATURES_GRAPH_VERTEX_NORMAL_OUTPUT
        #define FEATURES_GRAPH_VERTEX_TANGENT_OUTPUT
        #define VARYINGS_NEED_NORMAL_WS
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_SHADOWCASTER
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(ATTRIBUTES_NEED_INSTANCEID)
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 normalWS;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float3 normalWS : INTERP0;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.normalWS.xyz = input.normalWS;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.normalWS = input.normalWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _DisableAllGradients;
        float4 _ACT1_Color_B;
        float4 _ACT2_Color_B;
        float2 _ACT2_Color_B_Location;
        float4 _ACT2_Color_C;
        float4 _ACT2_Color_A;
        float2 _ACT2_Color_A_Location;
        float2 _ACT1_Color_B_Location;
        float4 _ACT1_Color_C;
        float4 _ACT1_Color_A;
        float2 _ACT1_Color_A_Location;
        float4 _ACT3_Color_B;
        float2 _ACT3_Color_B_Location;
        float4 _ACT3_Color_C;
        float4 _ACT3_Color_A;
        float2 _ACT3_Color_A_Location;
        float4 _ORM_TexelSize;
        float4 _Color3;
        float _Y_GradientEndPosition;
        float4 _Color2;
        float2 _Color_2_Location;
        float4 _AOColor;
        float4 _Normal_TexelSize;
        float2 _AOLevels;
        float4 _BaseColor_TexelSize;
        float4 _Color1;
        float2 _Color_1_Location;
        float _AOIntensity;
        float _RoughnessMultiplier;
        float2 _Tilling;
        float _DisableGradientMap;
        float4 _Y_GradientColor;
        float _Y_GradientIntensity;
        float _Y_GradientStartPosition;
        float _Y_Gradient;
        float _X_Gradient;
        float _X_GradientIntensity;
        float4 _X_GradientColor;
        float _X_GradientStartPosition;
        float _X_GradientEndPosition;
        float _Z_Gradient;
        float _Z_GradientIntensity;
        float4 _Z_GradientColor;
        float _Z_GradientStartPosition;
        float _Z_GradientEndPosition;
        float _UseCustomRoughness;
        float _CustomRoughness;
        float _LightingGradientMapInfluence;
        UNITY_TEXTURE_STREAMING_DEBUG_VARS;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ORM);
        SAMPLER(sampler_ORM);
        TEXTURE2D(_Normal);
        SAMPLER(sampler_Normal);
        TEXTURE2D(_BaseColor);
        SAMPLER(sampler_BaseColor);
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        // GraphFunctions: <None>
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            surface.Alpha = float(1);
            surface.AlphaClipThreshold = float(0.5);
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
        
            #if UNITY_UV_STARTS_AT_TOP
            #else
            #endif
        
        
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShadowCasterPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "MotionVectors"
            Tags
            {
                "LightMode" = "MotionVectors"
            }
        
        // Render State
        Cull [_Cull]
        ZTest LEqual
        ZWrite On
        ColorMask RG
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 3.5
        #pragma multi_compile_instancing
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        #pragma shader_feature_local_fragment _ _ALPHATEST_ON
        #pragma shader_feature _CURRENTACT_ACT_1 _CURRENTACT_ACT_2 _CURRENTACT_ACT_3
        
        
        
        // Defines
        
        #define _NORMALMAP 1
        #define _NORMAL_DROPOFF_TS 1
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_MOTION_VECTORS
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(ATTRIBUTES_NEED_INSTANCEID)
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _DisableAllGradients;
        float4 _ACT1_Color_B;
        float4 _ACT2_Color_B;
        float2 _ACT2_Color_B_Location;
        float4 _ACT2_Color_C;
        float4 _ACT2_Color_A;
        float2 _ACT2_Color_A_Location;
        float2 _ACT1_Color_B_Location;
        float4 _ACT1_Color_C;
        float4 _ACT1_Color_A;
        float2 _ACT1_Color_A_Location;
        float4 _ACT3_Color_B;
        float2 _ACT3_Color_B_Location;
        float4 _ACT3_Color_C;
        float4 _ACT3_Color_A;
        float2 _ACT3_Color_A_Location;
        float4 _ORM_TexelSize;
        float4 _Color3;
        float _Y_GradientEndPosition;
        float4 _Color2;
        float2 _Color_2_Location;
        float4 _AOColor;
        float4 _Normal_TexelSize;
        float2 _AOLevels;
        float4 _BaseColor_TexelSize;
        float4 _Color1;
        float2 _Color_1_Location;
        float _AOIntensity;
        float _RoughnessMultiplier;
        float2 _Tilling;
        float _DisableGradientMap;
        float4 _Y_GradientColor;
        float _Y_GradientIntensity;
        float _Y_GradientStartPosition;
        float _Y_Gradient;
        float _X_Gradient;
        float _X_GradientIntensity;
        float4 _X_GradientColor;
        float _X_GradientStartPosition;
        float _X_GradientEndPosition;
        float _Z_Gradient;
        float _Z_GradientIntensity;
        float4 _Z_GradientColor;
        float _Z_GradientStartPosition;
        float _Z_GradientEndPosition;
        float _UseCustomRoughness;
        float _CustomRoughness;
        float _LightingGradientMapInfluence;
        UNITY_TEXTURE_STREAMING_DEBUG_VARS;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ORM);
        SAMPLER(sampler_ORM);
        TEXTURE2D(_Normal);
        SAMPLER(sampler_Normal);
        TEXTURE2D(_BaseColor);
        SAMPLER(sampler_BaseColor);
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        // GraphFunctions: <None>
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            surface.Alpha = float(1);
            surface.AlphaClipThreshold = float(0.5);
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpacePosition =                        input.positionOS;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
        
            #if UNITY_UV_STARTS_AT_TOP
            #else
            #endif
        
        
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/MotionVectorPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "DepthOnly"
            Tags
            {
                "LightMode" = "DepthOnly"
            }
        
        // Render State
        Cull [_Cull]
        ZTest LEqual
        ZWrite On
        ColorMask R
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma multi_compile_instancing
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        #pragma shader_feature_local_fragment _ _ALPHATEST_ON
        #pragma shader_feature _CURRENTACT_ACT_1 _CURRENTACT_ACT_2 _CURRENTACT_ACT_3
        
        
        
        // Defines
        
        #define _NORMALMAP 1
        #define _NORMAL_DROPOFF_TS 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define FEATURES_GRAPH_VERTEX_NORMAL_OUTPUT
        #define FEATURES_GRAPH_VERTEX_TANGENT_OUTPUT
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHONLY
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(ATTRIBUTES_NEED_INSTANCEID)
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _DisableAllGradients;
        float4 _ACT1_Color_B;
        float4 _ACT2_Color_B;
        float2 _ACT2_Color_B_Location;
        float4 _ACT2_Color_C;
        float4 _ACT2_Color_A;
        float2 _ACT2_Color_A_Location;
        float2 _ACT1_Color_B_Location;
        float4 _ACT1_Color_C;
        float4 _ACT1_Color_A;
        float2 _ACT1_Color_A_Location;
        float4 _ACT3_Color_B;
        float2 _ACT3_Color_B_Location;
        float4 _ACT3_Color_C;
        float4 _ACT3_Color_A;
        float2 _ACT3_Color_A_Location;
        float4 _ORM_TexelSize;
        float4 _Color3;
        float _Y_GradientEndPosition;
        float4 _Color2;
        float2 _Color_2_Location;
        float4 _AOColor;
        float4 _Normal_TexelSize;
        float2 _AOLevels;
        float4 _BaseColor_TexelSize;
        float4 _Color1;
        float2 _Color_1_Location;
        float _AOIntensity;
        float _RoughnessMultiplier;
        float2 _Tilling;
        float _DisableGradientMap;
        float4 _Y_GradientColor;
        float _Y_GradientIntensity;
        float _Y_GradientStartPosition;
        float _Y_Gradient;
        float _X_Gradient;
        float _X_GradientIntensity;
        float4 _X_GradientColor;
        float _X_GradientStartPosition;
        float _X_GradientEndPosition;
        float _Z_Gradient;
        float _Z_GradientIntensity;
        float4 _Z_GradientColor;
        float _Z_GradientStartPosition;
        float _Z_GradientEndPosition;
        float _UseCustomRoughness;
        float _CustomRoughness;
        float _LightingGradientMapInfluence;
        UNITY_TEXTURE_STREAMING_DEBUG_VARS;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ORM);
        SAMPLER(sampler_ORM);
        TEXTURE2D(_Normal);
        SAMPLER(sampler_Normal);
        TEXTURE2D(_BaseColor);
        SAMPLER(sampler_BaseColor);
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        // GraphFunctions: <None>
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            surface.Alpha = float(1);
            surface.AlphaClipThreshold = float(0.5);
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
        
            #if UNITY_UV_STARTS_AT_TOP
            #else
            #endif
        
        
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/DepthOnlyPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "DepthNormals"
            Tags
            {
                "LightMode" = "DepthNormals"
            }
        
        // Render State
        Cull [_Cull]
        ZTest LEqual
        ZWrite On
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma multi_compile_instancing
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        #pragma shader_feature_local_fragment _ _ALPHATEST_ON
        #pragma shader_feature _CURRENTACT_ACT_1 _CURRENTACT_ACT_2 _CURRENTACT_ACT_3
        
        
        
        // Defines
        
        #define _NORMALMAP 1
        #define _NORMAL_DROPOFF_TS 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define ATTRIBUTES_NEED_TEXCOORD1
        #define FEATURES_GRAPH_VERTEX_NORMAL_OUTPUT
        #define FEATURES_GRAPH_VERTEX_TANGENT_OUTPUT
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TANGENT_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHNORMALS
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 uv1 : TEXCOORD1;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(ATTRIBUTES_NEED_INSTANCEID)
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 normalWS;
             float4 tangentWS;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 TangentSpaceNormal;
             float4 uv0;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 tangentWS : INTERP0;
             float4 texCoord0 : INTERP1;
             float3 normalWS : INTERP2;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.tangentWS.xyzw = input.tangentWS;
            output.texCoord0.xyzw = input.texCoord0;
            output.normalWS.xyz = input.normalWS;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.tangentWS = input.tangentWS.xyzw;
            output.texCoord0 = input.texCoord0.xyzw;
            output.normalWS = input.normalWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _DisableAllGradients;
        float4 _ACT1_Color_B;
        float4 _ACT2_Color_B;
        float2 _ACT2_Color_B_Location;
        float4 _ACT2_Color_C;
        float4 _ACT2_Color_A;
        float2 _ACT2_Color_A_Location;
        float2 _ACT1_Color_B_Location;
        float4 _ACT1_Color_C;
        float4 _ACT1_Color_A;
        float2 _ACT1_Color_A_Location;
        float4 _ACT3_Color_B;
        float2 _ACT3_Color_B_Location;
        float4 _ACT3_Color_C;
        float4 _ACT3_Color_A;
        float2 _ACT3_Color_A_Location;
        float4 _ORM_TexelSize;
        float4 _Color3;
        float _Y_GradientEndPosition;
        float4 _Color2;
        float2 _Color_2_Location;
        float4 _AOColor;
        float4 _Normal_TexelSize;
        float2 _AOLevels;
        float4 _BaseColor_TexelSize;
        float4 _Color1;
        float2 _Color_1_Location;
        float _AOIntensity;
        float _RoughnessMultiplier;
        float2 _Tilling;
        float _DisableGradientMap;
        float4 _Y_GradientColor;
        float _Y_GradientIntensity;
        float _Y_GradientStartPosition;
        float _Y_Gradient;
        float _X_Gradient;
        float _X_GradientIntensity;
        float4 _X_GradientColor;
        float _X_GradientStartPosition;
        float _X_GradientEndPosition;
        float _Z_Gradient;
        float _Z_GradientIntensity;
        float4 _Z_GradientColor;
        float _Z_GradientStartPosition;
        float _Z_GradientEndPosition;
        float _UseCustomRoughness;
        float _CustomRoughness;
        float _LightingGradientMapInfluence;
        UNITY_TEXTURE_STREAMING_DEBUG_VARS;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ORM);
        SAMPLER(sampler_ORM);
        TEXTURE2D(_Normal);
        SAMPLER(sampler_Normal);
        TEXTURE2D(_BaseColor);
        SAMPLER(sampler_BaseColor);
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float3 NormalTS;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D = UnityBuildTexture2DStructInternal(_Normal, sampler_Normal, _Normal_TexelSize, float4(1, 1, 0, 0), float4(0, 0, 0, 0));
            float2 _Property_94094af0818841fea1396a1ccae7a167_Out_0_Vector2 = _Tilling;
            float2 _TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2;
            Unity_TilingAndOffset_float(IN.uv0.xy, _Property_94094af0818841fea1396a1ccae7a167_Out_0_Vector2, float2 (0, 0), _TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2);
            float4 _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.tex, _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.samplerstate, _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.GetTransformedUV(_TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2) );
            if (_Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.hdrDecode.x > 0)
                _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4, _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.hdrDecode);
            _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.rgb = UnpackNormal(_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4);
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_R_4_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.r;
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_G_5_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.g;
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_B_6_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.b;
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_A_7_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.a;
            surface.NormalTS = (_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.xyz);
            surface.Alpha = float(1);
            surface.AlphaClipThreshold = float(0.5);
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
            output.TangentSpaceNormal = float3(0.0f, 0.0f, 1.0f);
        
        
        
            #if UNITY_UV_STARTS_AT_TOP
            #else
            #endif
        
        
            output.uv0 = input.texCoord0;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/DepthNormalsOnlyPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "Meta"
            Tags
            {
                "LightMode" = "Meta"
            }
        
        // Render State
        Cull Off
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        #pragma shader_feature _ EDITOR_VISUALIZATION
        #pragma shader_feature_local_fragment _ _ALPHATEST_ON
        #pragma shader_feature _CURRENTACT_ACT_1 _CURRENTACT_ACT_2 _CURRENTACT_ACT_3
        
        
        
        // Defines
        
        #define _NORMALMAP 1
        #define _NORMAL_DROPOFF_TS 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define ATTRIBUTES_NEED_TEXCOORD1
        #define ATTRIBUTES_NEED_TEXCOORD2
        #define ATTRIBUTES_NEED_INSTANCEID
        #define FEATURES_GRAPH_VERTEX_NORMAL_OUTPUT
        #define FEATURES_GRAPH_VERTEX_TANGENT_OUTPUT
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TANGENT_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define VARYINGS_NEED_TEXCOORD1
        #define VARYINGS_NEED_TEXCOORD2
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_META
        #define _FOG_FRAGMENT 1
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/MetaInput.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 uv1 : TEXCOORD1;
             float4 uv2 : TEXCOORD2;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(ATTRIBUTES_NEED_INSTANCEID)
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
             float4 tangentWS;
             float4 texCoord0;
             float4 texCoord1;
             float4 texCoord2;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 WorldSpaceNormal;
             float3 WorldSpaceTangent;
             float3 WorldSpaceBiTangent;
             float3 WorldSpaceViewDirection;
             float3 ObjectSpacePosition;
             float3 WorldSpacePosition;
             float3 AbsoluteWorldSpacePosition;
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float4 uv1;
             float4 uv2;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 tangentWS : INTERP0;
             float4 texCoord0 : INTERP1;
             float4 texCoord1 : INTERP2;
             float4 texCoord2 : INTERP3;
             float3 positionWS : INTERP4;
             float3 normalWS : INTERP5;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.tangentWS.xyzw = input.tangentWS;
            output.texCoord0.xyzw = input.texCoord0;
            output.texCoord1.xyzw = input.texCoord1;
            output.texCoord2.xyzw = input.texCoord2;
            output.positionWS.xyz = input.positionWS;
            output.normalWS.xyz = input.normalWS;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.tangentWS = input.tangentWS.xyzw;
            output.texCoord0 = input.texCoord0.xyzw;
            output.texCoord1 = input.texCoord1.xyzw;
            output.texCoord2 = input.texCoord2.xyzw;
            output.positionWS = input.positionWS.xyz;
            output.normalWS = input.normalWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _DisableAllGradients;
        float4 _ACT1_Color_B;
        float4 _ACT2_Color_B;
        float2 _ACT2_Color_B_Location;
        float4 _ACT2_Color_C;
        float4 _ACT2_Color_A;
        float2 _ACT2_Color_A_Location;
        float2 _ACT1_Color_B_Location;
        float4 _ACT1_Color_C;
        float4 _ACT1_Color_A;
        float2 _ACT1_Color_A_Location;
        float4 _ACT3_Color_B;
        float2 _ACT3_Color_B_Location;
        float4 _ACT3_Color_C;
        float4 _ACT3_Color_A;
        float2 _ACT3_Color_A_Location;
        float4 _ORM_TexelSize;
        float4 _Color3;
        float _Y_GradientEndPosition;
        float4 _Color2;
        float2 _Color_2_Location;
        float4 _AOColor;
        float4 _Normal_TexelSize;
        float2 _AOLevels;
        float4 _BaseColor_TexelSize;
        float4 _Color1;
        float2 _Color_1_Location;
        float _AOIntensity;
        float _RoughnessMultiplier;
        float2 _Tilling;
        float _DisableGradientMap;
        float4 _Y_GradientColor;
        float _Y_GradientIntensity;
        float _Y_GradientStartPosition;
        float _Y_Gradient;
        float _X_Gradient;
        float _X_GradientIntensity;
        float4 _X_GradientColor;
        float _X_GradientStartPosition;
        float _X_GradientEndPosition;
        float _Z_Gradient;
        float _Z_GradientIntensity;
        float4 _Z_GradientColor;
        float _Z_GradientStartPosition;
        float _Z_GradientEndPosition;
        float _UseCustomRoughness;
        float _CustomRoughness;
        float _LightingGradientMapInfluence;
        UNITY_TEXTURE_STREAMING_DEBUG_VARS;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ORM);
        SAMPLER(sampler_ORM);
        TEXTURE2D(_Normal);
        SAMPLER(sampler_Normal);
        TEXTURE2D(_BaseColor);
        SAMPLER(sampler_BaseColor);
        
        // Graph Includes
        #include_with_pragmas "Assets/Samples/Shader Graph/17.3.0/Custom Lighting/Components/Debug/DebugLightingComplexity.hlsl"
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
        {
            Out = lerp(A, B, T);
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Branch_float4(float Predicate, float4 True, float4 False, out float4 Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        void Unity_Branch_float(float Predicate, float True, float False, out float Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Divide_float(float A, float B, out float Out)
        {
            Out = A / B;
        }
        
        // unity-custom-func-begin
        void ApplyDecals_float(float4 positionCS, float3 baseColor, float3 specularColor, float3 normalWS, float metallic, float smoothness, float occlusion, out float3 baseColorOut, out float3 specularColorOut, out float3 normalWSOut, out float metallicOut, out float smoothnessOut, out float occlusionOut){
        #if !defined(SHADERGRAPH_PREVIEW) && defined(_DBUFFER)
        	ApplyDecal(positionCS, baseColor, specularColor, normalWS, metallic, occlusion, smoothness);
        	baseColorOut = baseColor;
        	specularColorOut = specularColor;
        	normalWSOut = normalWS;
        	metallicOut = metallic;
        	occlusionOut = occlusion;
        	smoothnessOut = smoothness;
        #else
        	baseColorOut = baseColor;
        	specularColorOut = specularColor;
        	normalWSOut = normalWS;
        	metallicOut = metallic;
        	occlusionOut = occlusion;
        	smoothnessOut = smoothness;
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float
        {
        float2 PixelPosition;
        };
        
        void SG_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float(float3 _Base_Color, float3 _NormalWS, float _Metallic, float _Smoothness, float _AmbientOcclusion, Bindings_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float IN, out float3 BaseColor_1, out float3 SpecularColor_2, out float3 NormalWS_3, out float Metallic_4, out float Smoothness_6, out float AmbientOcclusion_5)
        {
        float4 _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4 = float4(IN.PixelPosition.xy, 0, 0);
        float _Split_ad27d29658ef44f7b6941c97694d6866_R_1_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[0];
        float _Split_ad27d29658ef44f7b6941c97694d6866_G_2_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[1];
        float _Split_ad27d29658ef44f7b6941c97694d6866_B_3_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[2];
        float _Split_ad27d29658ef44f7b6941c97694d6866_A_4_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[3];
        float _Divide_3f9fb3b7b5b94d0d8246bbc34aa63f7b_Out_2_Float;
        Unity_Divide_float(_Split_ad27d29658ef44f7b6941c97694d6866_G_2_Float, _ScreenParams.y, _Divide_3f9fb3b7b5b94d0d8246bbc34aa63f7b_Out_2_Float);
        float _OneMinus_3cfec48ba27f4585a5feb790836dc9dc_Out_1_Float;
        Unity_OneMinus_float(_Divide_3f9fb3b7b5b94d0d8246bbc34aa63f7b_Out_2_Float, _OneMinus_3cfec48ba27f4585a5feb790836dc9dc_Out_1_Float);
        float _Multiply_a4baed73797c41c1b9631ae9bbddfe12_Out_2_Float;
        Unity_Multiply_float_float(_OneMinus_3cfec48ba27f4585a5feb790836dc9dc_Out_1_Float, _ScreenParams.y, _Multiply_a4baed73797c41c1b9631ae9bbddfe12_Out_2_Float);
        float2 _Vector2_eed86f79e1de4c188df97eb091955bc5_Out_0_Vector2 = float2(_Split_ad27d29658ef44f7b6941c97694d6866_R_1_Float, _Multiply_a4baed73797c41c1b9631ae9bbddfe12_Out_2_Float);
        float3 _Property_6219e38e66a84dddb55188eb0359a8c3_Out_0_Vector3 = _Base_Color;
        float3 _Property_f4c37d8281c1497e8dab743349080d88_Out_0_Vector3 = _NormalWS;
        float _Property_0826181079c84604befc19a2460f4daa_Out_0_Float = _Metallic;
        float _Property_d54a743184cc4f27b93d5f5b239c7b7e_Out_0_Float = _Smoothness;
        float _Property_bd6cbdae9db240b9b4ad935655106f79_Out_0_Float = _AmbientOcclusion;
        float3 _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_baseColorOut_8_Vector3;
        float3 _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_specularColorOut_9_Vector3;
        float3 _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_normalWSOut_10_Vector3;
        float _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_metallicOut_11_Float;
        float _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_smoothnessOut_13_Float;
        float _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_occlusionOut_12_Float;
        ApplyDecals_float((float4(_Vector2_eed86f79e1de4c188df97eb091955bc5_Out_0_Vector2, 0.0, 1.0)), _Property_6219e38e66a84dddb55188eb0359a8c3_Out_0_Vector3, float3 (0, 0, 0), _Property_f4c37d8281c1497e8dab743349080d88_Out_0_Vector3, _Property_0826181079c84604befc19a2460f4daa_Out_0_Float, _Property_d54a743184cc4f27b93d5f5b239c7b7e_Out_0_Float, _Property_bd6cbdae9db240b9b4ad935655106f79_Out_0_Float, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_baseColorOut_8_Vector3, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_specularColorOut_9_Vector3, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_normalWSOut_10_Vector3, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_metallicOut_11_Float, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_smoothnessOut_13_Float, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_occlusionOut_12_Float);
        BaseColor_1 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_baseColorOut_8_Vector3;
        SpecularColor_2 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_specularColorOut_9_Vector3;
        NormalWS_3 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_normalWSOut_10_Vector3;
        Metallic_4 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_metallicOut_11_Float;
        Smoothness_6 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_smoothnessOut_13_Float;
        AmbientOcclusion_5 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_occlusionOut_12_Float;
        }
        
        // unity-custom-func-begin
        void SwitchLightingDebug_float(float3 BaseColorIn, float3 NormalIn, float MetallicIn, float SmoothnessIn, float3 EmissionIn, float AmbientOcclusionIn, float3 positionWS, float3 bakedGI, out float3 BaseColorOut, out float3 NormalOut, out float MetallicOut, out float SmoothnessOut, out float3 EmissionOut, out float AmbientOcclusionOut){
        #if !defined(SHADERGRAPH_PREVIEW) && defined(DEBUG_DISPLAY)
        
        [branch] switch(int(_DebugLightingMode))
        
        {
        
            case 0: //none
        
        		BaseColorOut = BaseColorIn;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
            case 1: //SHADOW_CASCADES
        
        		half cascadeIndex = ComputeCascadeIndex(positionWS);
        
        		switch (uint(cascadeIndex))
        
        		{
        
        			case 0: BaseColorOut = kDebugColorShadowCascade0.rgb;break;
        
        			case 1: BaseColorOut = kDebugColorShadowCascade1.rgb;break;
        
        			case 2: BaseColorOut = kDebugColorShadowCascade2.rgb;break;
        
        			case 3: BaseColorOut = kDebugColorShadowCascade3.rgb;break;
        
        			default: BaseColorOut = kDebugColorBlack.rgb;break;
        
        		}
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
            case 2: //LIGHTING_WITHOUT_NORMAL_MAPS
        
        		BaseColorOut = float3(1,1,1);
        
        		MetallicOut = 0;
        
        		SmoothnessOut = 0;
        
        		NormalOut = float3(0,0,1);
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            case 3: //LIGHTING_WITH_NORMAL_MAPS
        
        		BaseColorOut = float3(1,1,1);
        
        		MetallicOut = 0;
        
        		SmoothnessOut = 0;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            case 4: //REFLECTIONS
        
        		BaseColorOut = float3(0.1,0.1,0.1);
        
        		MetallicOut = 1;
        
        		SmoothnessOut = 1;
        
        		NormalOut = float3(0,0,1);
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            case 5: //REFLECTIONS_WITH_SMOOTHNESS
        
        		BaseColorOut = float3(0.1,0.1,0.1);
        
        		MetallicOut = 1;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
            case 6: //GLOBAL_ILLUM
        
        		BaseColorOut = bakedGI;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = 0;
        
        		NormalOut = float3(0,0,1);
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            default:
        
        		BaseColorOut = BaseColorIn;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
        }
        
        #else
        
        		BaseColorOut = BaseColorIn;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_DebugLighting_61e571d2b9ede1240a524a849d20c997_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpacePosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_DebugLighting_61e571d2b9ede1240a524a849d20c997_float(float3 _Base_Color, float3 _NormalWS, float _Metallic, float _Smoothness, float3 _Emission, float _AmbientOcclusion, Bindings_DebugLighting_61e571d2b9ede1240a524a849d20c997_float IN, out float3 BaseColor_1, out float3 Normal_4, out float Metallic_2, out float Smoothness_3, out float3 Emission_5, out float AmbientOcclusion_6)
        {
        float3 _Property_501515703e3a4a1dbd19f4ae273add46_Out_0_Vector3 = _Base_Color;
        float3 _Property_e5bcb5bf3b62412b8983e3aa1ada8fcd_Out_0_Vector3 = _NormalWS;
        float3 _Transform_13c6b1e888d440fd8340d4a7138979ba_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_13c6b1e888d440fd8340d4a7138979ba_Out_1_Vector3 = TransformWorldToTangent(_Property_e5bcb5bf3b62412b8983e3aa1ada8fcd_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float _Property_7a450453146043b2b11397a72c325042_Out_0_Float = _Metallic;
        float _Property_f0326121e031478a90610d60b8321364_Out_0_Float = _Smoothness;
        float3 _Property_491d95b34bb245718ee21bff5fc249cd_Out_0_Vector3 = _Emission;
        float _Property_da91a6effd53499db08bb774d5686c68_Out_0_Float = _AmbientOcclusion;
        float3 _BakedGI_3f01c30cb8b64e9d9f7fbe474622c7dc_Out_1_Vector3 = SHADERGRAPH_BAKED_GI(IN.WorldSpacePosition, _Property_e5bcb5bf3b62412b8983e3aa1ada8fcd_Out_0_Vector3, IN.PixelPosition.xy, IN.uv1.xy, IN.uv2.xy, true);
        float3 _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_BaseColorOut_7_Vector3;
        float3 _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_NormalOut_11_Vector3;
        float _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_MetallicOut_9_Float;
        float _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_SmoothnessOut_10_Float;
        float3 _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_EmissionOut_12_Vector3;
        float _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_AmbientOcclusionOut_13_Float;
        SwitchLightingDebug_float(_Property_501515703e3a4a1dbd19f4ae273add46_Out_0_Vector3, _Transform_13c6b1e888d440fd8340d4a7138979ba_Out_1_Vector3, _Property_7a450453146043b2b11397a72c325042_Out_0_Float, _Property_f0326121e031478a90610d60b8321364_Out_0_Float, _Property_491d95b34bb245718ee21bff5fc249cd_Out_0_Vector3, _Property_da91a6effd53499db08bb774d5686c68_Out_0_Float, IN.WorldSpacePosition, _BakedGI_3f01c30cb8b64e9d9f7fbe474622c7dc_Out_1_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_BaseColorOut_7_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_NormalOut_11_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_MetallicOut_9_Float, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_SmoothnessOut_10_Float, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_EmissionOut_12_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_AmbientOcclusionOut_13_Float);
        BaseColor_1 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_BaseColorOut_7_Vector3;
        Normal_4 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_NormalOut_11_Vector3;
        Metallic_2 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_MetallicOut_9_Float;
        Smoothness_3 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_SmoothnessOut_10_Float;
        Emission_5 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_EmissionOut_12_Vector3;
        AmbientOcclusion_6 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_AmbientOcclusionOut_13_Float;
        }
        
        // unity-custom-func-begin
        void GetMainLightData_float(float3 worldPos, out float3 direction, out float3 color, out float shadowAtten){
        #ifdef SHADERGRAPH_PREVIEW
            direction = normalize(float3(-0.7,0.7,-0.7));
            color = float3(1,1,1);
            shadowAtten = 1;
        #else
            #if defined(UNIVERSAL_PIPELINE_CORE_INCLUDED)
                float4 shadowCoord = TransformWorldToShadowCoord(worldPos);
                Light mainLight = GetMainLight(shadowCoord);
                direction = mainLight.direction;
                color = mainLight.color;
                shadowAtten = mainLight.shadowAttenuation;
            #else
                direction = normalize(float3(-0.7, 0.7, -0.7));
                color = float3(1, 1, 1);
                shadowAtten = 0;
            #endif
        #endif
        }
        // unity-custom-func-end
        
        // unity-custom-func-begin
        void GetMainLightData_half(half3 worldPos, out half3 direction, out half3 color, out half shadowAtten){
        #ifdef SHADERGRAPH_PREVIEW
            direction = normalize(float3(-0.7,0.7,-0.7));
            color = float3(1,1,1);
            shadowAtten = 1;
        #else
            #if defined(UNIVERSAL_PIPELINE_CORE_INCLUDED)
                float4 shadowCoord = TransformWorldToShadowCoord(worldPos);
                Light mainLight = GetMainLight(shadowCoord);
                direction = mainLight.direction;
                color = mainLight.color;
                shadowAtten = mainLight.shadowAttenuation;
            #else
                direction = normalize(float3(-0.7, 0.7, -0.7));
                color = float3(1, 1, 1);
                shadowAtten = 0;
            #endif
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float
        {
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float IN, out float3 Direction_1, out float3 Color_2, out float ShadowAtten_3)
        {
        float3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        float3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        float _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        GetMainLightData_float(IN.AbsoluteWorldSpacePosition, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float);
        Direction_1 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        Color_2 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        ShadowAtten_3 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        }
        
        struct Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half
        {
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half(Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half IN, out half3 Direction_1, out half3 Color_2, out half ShadowAtten_3)
        {
        half3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        half3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        half _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        GetMainLightData_half(IN.AbsoluteWorldSpacePosition, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float);
        Direction_1 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        Color_2 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        ShadowAtten_3 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        }
        
        void Unity_DotProduct_float3(float3 A, float3 B, out float Out)
        {
            Out = dot(A, B);
        }
        
        void Unity_DotProduct_half3(half3 A, half3 B, out half Out)
        {
            Out = dot(A, B);
        }
        
        void Unity_Saturate_half(half In, out half Out)
        {
            Out = saturate(In);
        }
        
        struct Bindings_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half
        {
        float3 WorldSpaceNormal;
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half(half3 _NormalWS, bool _NormalWS_68a7999ae9ea4bfba3702fd95b0d1a14_IsConnected, half3 _LightVector, bool _LightVector_a12354c78b694cc6b2bdddd67d09ccdc_IsConnected, Bindings_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half IN, out half Diffuse_1)
        {
        half3 _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3 = _NormalWS;
        bool _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3_IsConnected = _NormalWS_68a7999ae9ea4bfba3702fd95b0d1a14_IsConnected;
        half3 _BranchOnInputConnection_71cde5ac4ee04aacb1e2544c8017ba47_Out_3_Vector3 = _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3_IsConnected ? _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3 : IN.WorldSpaceNormal;
        half3 _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3 = _LightVector;
        bool _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3_IsConnected = _LightVector_a12354c78b694cc6b2bdddd67d09ccdc_IsConnected;
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half _MainLight_fa0151c045984bcab58e58725bae0709;
        _MainLight_fa0151c045984bcab58e58725bae0709.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_fa0151c045984bcab58e58725bae0709_Direction_1_Vector3;
        half3 _MainLight_fa0151c045984bcab58e58725bae0709_Color_2_Vector3;
        half _MainLight_fa0151c045984bcab58e58725bae0709_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half(_MainLight_fa0151c045984bcab58e58725bae0709, _MainLight_fa0151c045984bcab58e58725bae0709_Direction_1_Vector3, _MainLight_fa0151c045984bcab58e58725bae0709_Color_2_Vector3, _MainLight_fa0151c045984bcab58e58725bae0709_ShadowAtten_3_Float);
        half3 _BranchOnInputConnection_d18845e766084954af1aa554531c90b9_Out_3_Vector3 = _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3_IsConnected ? _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3 : _MainLight_fa0151c045984bcab58e58725bae0709_Direction_1_Vector3;
        half _DotProduct_daa979e4f9384944a14a23e079be6a5c_Out_2_Float;
        Unity_DotProduct_half3(_BranchOnInputConnection_71cde5ac4ee04aacb1e2544c8017ba47_Out_3_Vector3, _BranchOnInputConnection_d18845e766084954af1aa554531c90b9_Out_3_Vector3, _DotProduct_daa979e4f9384944a14a23e079be6a5c_Out_2_Float);
        half _Saturate_4747439b9bfa431293a93646ce71aa12_Out_1_Float;
        Unity_Saturate_half(_DotProduct_daa979e4f9384944a14a23e079be6a5c_Out_2_Float, _Saturate_4747439b9bfa431293a93646ce71aa12_Out_1_Float);
        Diffuse_1 = _Saturate_4747439b9bfa431293a93646ce71aa12_Out_1_Float;
        }
        
        void Unity_Lerp_float(float A, float B, float T, out float Out)
        {
            Out = lerp(A, B, T);
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Reciprocal_float(float In, out float Out)
        {
            Out = 1.0/In;
        }
        
        void Unity_FresnelEffect_float(float3 Normal, float3 ViewDir, float Power, out float Out)
        {
            Out = pow((1.0 - saturate(dot(normalize(Normal), ViewDir))), Power);
        }
        
        void Unity_Lerp_float3(float3 A, float3 B, float3 T, out float3 Out)
        {
            Out = lerp(A, B, T);
        }
        
        struct Bindings_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        };
        
        void SG_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float(float3 _Base_Color, float3 _NormalWS, bool _NormalWS_3240674a787044a092398b1ca753ad83_IsConnected, float _Metallic, float _Smoothness, float _F0, Bindings_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float IN, out float3 Reflectance_1)
        {
        float _Property_4678b902494b4e1a9b08a8067b7bed85_Out_0_Float = _Smoothness;
        float _OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float;
        Unity_OneMinus_float(_Property_4678b902494b4e1a9b08a8067b7bed85_Out_0_Float, _OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float);
        float _Multiply_314f6a0aec0a44538333e69617e91cf9_Out_2_Float;
        Unity_Multiply_float_float(_OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float, _OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float, _Multiply_314f6a0aec0a44538333e69617e91cf9_Out_2_Float);
        float _Add_513724b99c2f4ea2a803e64d80f0c25b_Out_2_Float;
        Unity_Add_float(_Multiply_314f6a0aec0a44538333e69617e91cf9_Out_2_Float, float(1), _Add_513724b99c2f4ea2a803e64d80f0c25b_Out_2_Float);
        float _Reciprocal_67b338305d0043abb9e7d82dd0f16146_Out_1_Float;
        Unity_Reciprocal_float(_Add_513724b99c2f4ea2a803e64d80f0c25b_Out_2_Float, _Reciprocal_67b338305d0043abb9e7d82dd0f16146_Out_1_Float);
        float _Property_b67a6773dce34e91ae69bbf282d871cc_Out_0_Float = _F0;
        float _Property_703d9ec0a0894a3b965f0ed25a10435b_Out_0_Float = _F0;
        float _Add_8ad10875906b4a80872f2b2fb0518183_Out_2_Float;
        Unity_Add_float(_Property_4678b902494b4e1a9b08a8067b7bed85_Out_0_Float, _Property_703d9ec0a0894a3b965f0ed25a10435b_Out_0_Float, _Add_8ad10875906b4a80872f2b2fb0518183_Out_2_Float);
        float3 _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3 = _NormalWS;
        bool _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3_IsConnected = _NormalWS_3240674a787044a092398b1ca753ad83_IsConnected;
        float3 _BranchOnInputConnection_43b8bde55a8a41468ba21d53db128986_Out_3_Vector3 = _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3_IsConnected ? _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3 : IN.WorldSpaceNormal;
        float _FresnelEffect_34b729c62edd4d5f99dc20e2e7d0a7fa_Out_3_Float;
        Unity_FresnelEffect_float(_BranchOnInputConnection_43b8bde55a8a41468ba21d53db128986_Out_3_Vector3, IN.WorldSpaceViewDirection, float(4), _FresnelEffect_34b729c62edd4d5f99dc20e2e7d0a7fa_Out_3_Float);
        float _Lerp_1cad776e609842c181b8acfaef47c317_Out_3_Float;
        Unity_Lerp_float(_Property_b67a6773dce34e91ae69bbf282d871cc_Out_0_Float, _Add_8ad10875906b4a80872f2b2fb0518183_Out_2_Float, _FresnelEffect_34b729c62edd4d5f99dc20e2e7d0a7fa_Out_3_Float, _Lerp_1cad776e609842c181b8acfaef47c317_Out_3_Float);
        float _Multiply_0d364d1c231246d981281b868ea74a95_Out_2_Float;
        Unity_Multiply_float_float(_Reciprocal_67b338305d0043abb9e7d82dd0f16146_Out_1_Float, _Lerp_1cad776e609842c181b8acfaef47c317_Out_3_Float, _Multiply_0d364d1c231246d981281b868ea74a95_Out_2_Float);
        float3 _Property_87ae51a595c24e46ad9ef0f4493231fc_Out_0_Vector3 = _Base_Color;
        float _Property_ce0a90815c5046b48dd0564711f2b466_Out_0_Float = _Metallic;
        float3 _Lerp_6b4c86a1d5004851a7dba1bacc4fd953_Out_3_Vector3;
        Unity_Lerp_float3((_Multiply_0d364d1c231246d981281b868ea74a95_Out_2_Float.xxx), _Property_87ae51a595c24e46ad9ef0f4493231fc_Out_0_Vector3, (_Property_ce0a90815c5046b48dd0564711f2b466_Out_0_Float.xxx), _Lerp_6b4c86a1d5004851a7dba1bacc4fd953_Out_3_Vector3);
        Reflectance_1 = _Lerp_6b4c86a1d5004851a7dba1bacc4fd953_Out_3_Vector3;
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        void Unity_Normalize_float3(float3 In, out float3 Out)
        {
            Out = normalize(In);
        }
        
        struct Bindings_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float
        {
        };
        
        void SG_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float(float3 _viewDir, float3 _lightDir, Bindings_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float IN, out float3 Out_1)
        {
        float3 _Property_fde52ad74bda46adabbcc34b42b16131_Out_0_Vector3 = _viewDir;
        float3 _Property_1dc55a6640574aaf8c04290eb0d5e816_Out_0_Vector3 = _lightDir;
        float3 _Add_2a8cf5c52e8c4e3fb8c3f9a87b68b2a2_Out_2_Vector3;
        Unity_Add_float3(_Property_fde52ad74bda46adabbcc34b42b16131_Out_0_Vector3, _Property_1dc55a6640574aaf8c04290eb0d5e816_Out_0_Vector3, _Add_2a8cf5c52e8c4e3fb8c3f9a87b68b2a2_Out_2_Vector3);
        float3 _Normalize_b3b8196f46224ae3a998bba24956de7f_Out_1_Vector3;
        Unity_Normalize_float3(_Add_2a8cf5c52e8c4e3fb8c3f9a87b68b2a2_Out_2_Vector3, _Normalize_b3b8196f46224ae3a998bba24956de7f_Out_1_Vector3);
        Out_1 = _Normalize_b3b8196f46224ae3a998bba24956de7f_Out_1_Vector3;
        }
        
        void Unity_Exponential2_float(float In, out float Out)
        {
            Out = exp2(In);
        }
        
        struct Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float
        {
        };
        
        void SG_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float(float _Smoothness, Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float IN, out float SpecPower_1)
        {
        float _Property_80f639c6927445458cce37e8c24909a1_Out_0_Float = _Smoothness;
        float _Multiply_c7e9a2c01b804964a64dd7499f62a400_Out_2_Float;
        Unity_Multiply_float_float(_Property_80f639c6927445458cce37e8c24909a1_Out_0_Float, 10, _Multiply_c7e9a2c01b804964a64dd7499f62a400_Out_2_Float);
        float _Add_663b867a23b04deaa88c2ebd79bcdbc7_Out_2_Float;
        Unity_Add_float(_Multiply_c7e9a2c01b804964a64dd7499f62a400_Out_2_Float, float(1), _Add_663b867a23b04deaa88c2ebd79bcdbc7_Out_2_Float);
        float _Exponential_bd8d33b85b2e46508bef0c6cc36f9dae_Out_1_Float;
        Unity_Exponential2_float(_Add_663b867a23b04deaa88c2ebd79bcdbc7_Out_2_Float, _Exponential_bd8d33b85b2e46508bef0c6cc36f9dae_Out_1_Float);
        SpecPower_1 = _Exponential_bd8d33b85b2e46508bef0c6cc36f9dae_Out_1_Float;
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
        Out = A * B;
        }
        
        struct Bindings_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float(float3 _NormalWS, bool _NormalWS_5a3c9a3a7faa491894a42d170b5bfeb5_IsConnected, float _Smoothness, float3 _Reflectance, float3 _LightVector, bool _LightVector_3db37b6247094f32bcccc4cb689d525f_IsConnected, Bindings_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float IN, out float3 Specular_1)
        {
        float3 _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3 = _LightVector;
        bool _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3_IsConnected = _LightVector_3db37b6247094f32bcccc4cb689d525f_IsConnected;
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float _MainLight_6570bf88718b46ebb6bd80eec408287a;
        _MainLight_6570bf88718b46ebb6bd80eec408287a.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_6570bf88718b46ebb6bd80eec408287a_Direction_1_Vector3;
        half3 _MainLight_6570bf88718b46ebb6bd80eec408287a_Color_2_Vector3;
        half _MainLight_6570bf88718b46ebb6bd80eec408287a_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(_MainLight_6570bf88718b46ebb6bd80eec408287a, _MainLight_6570bf88718b46ebb6bd80eec408287a_Direction_1_Vector3, _MainLight_6570bf88718b46ebb6bd80eec408287a_Color_2_Vector3, _MainLight_6570bf88718b46ebb6bd80eec408287a_ShadowAtten_3_Float);
        float3 _BranchOnInputConnection_6a7b13b3cb82474aa187229c3d17a00f_Out_3_Vector3 = _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3_IsConnected ? _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3 : _MainLight_6570bf88718b46ebb6bd80eec408287a_Direction_1_Vector3;
        Bindings_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float _HalfAngle_f48886360d2649d8b7540e6fb3eef669;
        half3 _HalfAngle_f48886360d2649d8b7540e6fb3eef669_Out_1_Vector3;
        SG_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float(IN.WorldSpaceViewDirection, _BranchOnInputConnection_6a7b13b3cb82474aa187229c3d17a00f_Out_3_Vector3, _HalfAngle_f48886360d2649d8b7540e6fb3eef669, _HalfAngle_f48886360d2649d8b7540e6fb3eef669_Out_1_Vector3);
        float3 _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3 = _NormalWS;
        bool _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3_IsConnected = _NormalWS_5a3c9a3a7faa491894a42d170b5bfeb5_IsConnected;
        float3 _BranchOnInputConnection_72430741d0e04d2dbf5368b624a090cc_Out_3_Vector3 = _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3_IsConnected ? _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3 : IN.WorldSpaceNormal;
        float _DotProduct_4558c6edcb084d359ac8ee4b2934ea05_Out_2_Float;
        Unity_DotProduct_float3(_HalfAngle_f48886360d2649d8b7540e6fb3eef669_Out_1_Vector3, _BranchOnInputConnection_72430741d0e04d2dbf5368b624a090cc_Out_3_Vector3, _DotProduct_4558c6edcb084d359ac8ee4b2934ea05_Out_2_Float);
        float _Saturate_23d3962f416c4150a990dcb36b5ecbc4_Out_1_Float;
        Unity_Saturate_float(_DotProduct_4558c6edcb084d359ac8ee4b2934ea05_Out_2_Float, _Saturate_23d3962f416c4150a990dcb36b5ecbc4_Out_1_Float);
        float _Property_f4ccf6ae090a4694bb78a2cef88028e0_Out_0_Float = _Smoothness;
        Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9;
        half _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9_SpecPower_1_Float;
        SG_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float(_Property_f4ccf6ae090a4694bb78a2cef88028e0_Out_0_Float, _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9, _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9_SpecPower_1_Float);
        float _Power_b652c8bd47c44415bf7733f381883bf3_Out_2_Float;
        Unity_Power_float(_Saturate_23d3962f416c4150a990dcb36b5ecbc4_Out_1_Float, _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9_SpecPower_1_Float, _Power_b652c8bd47c44415bf7733f381883bf3_Out_2_Float);
        float _Property_1fcbde0798cd43628cbb75583e5d6e7a_Out_0_Float = _Smoothness;
        float _Multiply_8e72dd78784942f5ac8322f3055fd0ed_Out_2_Float;
        Unity_Multiply_float_float(_Power_b652c8bd47c44415bf7733f381883bf3_Out_2_Float, _Property_1fcbde0798cd43628cbb75583e5d6e7a_Out_0_Float, _Multiply_8e72dd78784942f5ac8322f3055fd0ed_Out_2_Float);
        float3 _Property_5b0bef6a4de54859800dd057235a4dbc_Out_0_Vector3 = _Reflectance;
        float3 _Multiply_73cf80666f44441494dd412a147e089c_Out_2_Vector3;
        Unity_Multiply_float3_float3((_Multiply_8e72dd78784942f5ac8322f3055fd0ed_Out_2_Float.xxx), _Property_5b0bef6a4de54859800dd057235a4dbc_Out_0_Vector3, _Multiply_73cf80666f44441494dd412a147e089c_Out_2_Vector3);
        float3 _Multiply_bceb91846ebf445c9093523786845bb5_Out_2_Vector3;
        Unity_Multiply_float3_float3(_Multiply_73cf80666f44441494dd412a147e089c_Out_2_Vector3, float3(10, 10, 10), _Multiply_bceb91846ebf445c9093523786845bb5_Out_2_Vector3);
        Specular_1 = _Multiply_bceb91846ebf445c9093523786845bb5_Out_2_Vector3;
        }
        
        // unity-custom-func-begin
        void AddAdditionalLightsSimple_float(float SpecPower, float3 WorldPosition, float3 WorldNormal, float3 WorldView, float MainDiffuse, float3 MainSpecular, float3 MainColor, float3 Reflectance, float2 ScreenPosition, out float Diffuse, out float3 Specular, out float3 Color){
        Diffuse = MainDiffuse;
        
        Specular = MainSpecular;
        
        Color = MainColor * (MainDiffuse + MainSpecular);
        
        
        
        #ifndef SHADERGRAPH_PREVIEW
        
            
        
        #if defined(_ADDITIONAL_LIGHTS) || defined(_CLUSTER_LIGHT_LOOP)
        
        
        
            #if defined(_ADDITIONAL_LIGHTS)
        
                uint pixelLightCount = GetAdditionalLightsCount();
        
            #endif
        
        
        
        #if USE_CLUSTER_LIGHT_LOOP
        
            // for Foward+ LIGHT_LOOP_BEGIN macro uses inputData.normalizedScreenSpaceUV and inputData.positionWS
        
            InputData inputData = (InputData)0;
        
        
        
            inputData.normalizedScreenSpaceUV = ScreenPosition;
        
            inputData.positionWS = WorldPosition;
        
        #endif
        
        
        
            LIGHT_LOOP_BEGIN(pixelLightCount)
        
        		// Call the URP additional light algorithm. This will not calculate shadows, since we don't pass a shadow mask value
        
        		Light light = GetAdditionalLight(lightIndex, WorldPosition);
        
        		// Manually set the shadow attenuation by calculating realtime shadows
        
        		light.shadowAttenuation = AdditionalLightRealtimeShadow(lightIndex, WorldPosition, light.direction);
        
                float NdotL = saturate(dot(WorldNormal, light.direction));
        
                float atten = light.distanceAttenuation * light.shadowAttenuation;
        
                float thisDiffuse = atten * NdotL;
        
                float3 halfAngle = normalize(light.direction + WorldView);
        
                float spec = pow(saturate(dot(halfAngle, WorldNormal)), SpecPower);
        
                float3 thisSpecular = spec * Reflectance * atten;
        
                Diffuse += thisDiffuse;
        
                Specular += thisSpecular;
        
                #if defined(_LIGHT_COOKIES)
        
                    float3 cookieColor = SampleAdditionalLightCookie(lightIndex, WorldPosition);
        
                    light.color *= cookieColor;
        
                #endif
        
                Color += light.color * (thisDiffuse + thisSpecular);
        
            LIGHT_LOOP_END
        
            float total = Diffuse + dot(Specular, float3(0.333, 0.333, 0.333));
        
            Color = total <= 0 ? MainColor : Color / total;
        
        #endif // _ADDITIONAL_LIGHTS || _CLUSTER_LIGHT_LOOP
        
        
        
        #endif // SHADERGRAPH_PREVIEW
        }
        // unity-custom-func-end
        
        struct Bindings_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        };
        
        void SG_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float(float _MainLightDiffuse, float3 _MainLightSpecular, float3 _MainLightColor, float3 _NormalWS, bool _NormalWS_70cbf5ac6da04bf6bd87eb71ccb7c48d_IsConnected, float _Smoothness, float3 _Reflectance, Bindings_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float IN, out float Diffuse_1, out float3 Specular_2, out float3 Color_3)
        {
        float _Property_b9f05025da4f4857a7b1b6f56259a629_Out_0_Float = _Smoothness;
        Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1;
        half _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1_SpecPower_1_Float;
        SG_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float(_Property_b9f05025da4f4857a7b1b6f56259a629_Out_0_Float, _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1, _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1_SpecPower_1_Float);
        float3 _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3 = _NormalWS;
        bool _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3_IsConnected = _NormalWS_70cbf5ac6da04bf6bd87eb71ccb7c48d_IsConnected;
        float3 _BranchOnInputConnection_d869e3d8654b48a491de945ad8af6301_Out_3_Vector3 = _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3_IsConnected ? _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3 : IN.WorldSpaceNormal;
        float _Property_25880f0697234954b8dc6ef11af3752d_Out_0_Float = _MainLightDiffuse;
        float3 _Property_1e29ad89226c4d84a936fe7530839aef_Out_0_Vector3 = _MainLightSpecular;
        float3 _Property_ac790fc8215b4b3d8851855d2153960d_Out_0_Vector3 = _MainLightColor;
        float3 _Property_eea8eda455d44ae7b30c65f80baac806_Out_0_Vector3 = _Reflectance;
        float4 _ScreenPosition_bb4bf3ece5524d4c898132bd377d7d8b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Diffuse_7_Float;
        float3 _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Specular_8_Vector3;
        float3 _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Color_9_Vector3;
        AddAdditionalLightsSimple_float(_SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1_SpecPower_1_Float, IN.WorldSpacePosition, _BranchOnInputConnection_d869e3d8654b48a491de945ad8af6301_Out_3_Vector3, IN.WorldSpaceViewDirection, _Property_25880f0697234954b8dc6ef11af3752d_Out_0_Float, _Property_1e29ad89226c4d84a936fe7530839aef_Out_0_Vector3, _Property_ac790fc8215b4b3d8851855d2153960d_Out_0_Vector3, _Property_eea8eda455d44ae7b30c65f80baac806_Out_0_Vector3, (_ScreenPosition_bb4bf3ece5524d4c898132bd377d7d8b_Out_0_Vector4.xy), _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Diffuse_7_Float, _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Specular_8_Vector3, _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Color_9_Vector3);
        Diffuse_1 = _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Diffuse_7_Float;
        Specular_2 = _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Specular_8_Vector3;
        Color_3 = _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Color_9_Vector3;
        }
        
        void Unity_Negate_float3(float3 In, out float3 Out)
        {
            Out = -1 * In;
        }
        
        void Unity_Reflection_float3(float3 In, float3 Normal, out float3 Out)
        {
            Out = reflect(In, Normal);
        }
        
        // unity-custom-func-begin
        void URPReflectionProbe_float(float3 positionWS, float3 reflectVector, float2 normalizedScreenSpaceUV, float roughness, float occlusion, out float3 reflection){
        #ifdef SHADERGRAPH_PREVIEW
        
            reflection = float3(0,0,0);
        
        #else
        
            reflection = GlossyEnvironmentReflection(reflectVector, positionWS, roughness, occlusion, normalizedScreenSpaceUV);
        
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        };
        
        void SG_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float(float3 _positionWS, bool _positionWS_d6701bdc1f184a57ac2283491fc460d9_IsConnected, float3 _reflectVector, bool _reflectVector_3e2eb19b69b8469eaf2302c7abc4cbc5_IsConnected, float _smoothness, float _occlusion, Bindings_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float IN, out float3 Out_1)
        {
        float3 _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3 = _positionWS;
        bool _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3_IsConnected = _positionWS_d6701bdc1f184a57ac2283491fc460d9_IsConnected;
        float3 _BranchOnInputConnection_8fb583036b0c4313a1ecd93143939f21_Out_3_Vector3 = _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3_IsConnected ? _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3 : IN.WorldSpacePosition;
        float3 _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3 = _reflectVector;
        bool _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3_IsConnected = _reflectVector_3e2eb19b69b8469eaf2302c7abc4cbc5_IsConnected;
        float3 _Negate_9cf7cea21c5641239fdbcb32480ac39e_Out_1_Vector3;
        Unity_Negate_float3(IN.WorldSpaceViewDirection, _Negate_9cf7cea21c5641239fdbcb32480ac39e_Out_1_Vector3);
        float3 _Reflection_c8689c494aab4411aadc74299549c6cb_Out_2_Vector3;
        Unity_Reflection_float3(_Negate_9cf7cea21c5641239fdbcb32480ac39e_Out_1_Vector3, IN.WorldSpaceNormal, _Reflection_c8689c494aab4411aadc74299549c6cb_Out_2_Vector3);
        float3 _BranchOnInputConnection_9600230d09794702a61c1a01f8e842a5_Out_3_Vector3 = _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3_IsConnected ? _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3 : _Reflection_c8689c494aab4411aadc74299549c6cb_Out_2_Vector3;
        float4 _ScreenPosition_270e438746a9466e8aaf01f4903f62fb_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float _Property_9012e47da801473d8ef85a4092281eb2_Out_0_Float = _smoothness;
        float _OneMinus_b2508f25afb44017ba3480edc35cf631_Out_1_Float;
        Unity_OneMinus_float(_Property_9012e47da801473d8ef85a4092281eb2_Out_0_Float, _OneMinus_b2508f25afb44017ba3480edc35cf631_Out_1_Float);
        float _Property_d602b1723845462cbf00324de1e9e82a_Out_0_Float = _occlusion;
        float3 _URPReflectionProbeCustomFunction_7233d098cd214f55baf898d486bfdf4b_reflection_4_Vector3;
        URPReflectionProbe_float(_BranchOnInputConnection_8fb583036b0c4313a1ecd93143939f21_Out_3_Vector3, _BranchOnInputConnection_9600230d09794702a61c1a01f8e842a5_Out_3_Vector3, (_ScreenPosition_270e438746a9466e8aaf01f4903f62fb_Out_0_Vector4.xy), _OneMinus_b2508f25afb44017ba3480edc35cf631_Out_1_Float, _Property_d602b1723845462cbf00324de1e9e82a_Out_0_Float, _URPReflectionProbeCustomFunction_7233d098cd214f55baf898d486bfdf4b_reflection_4_Vector3);
        Out_1 = _URPReflectionProbeCustomFunction_7233d098cd214f55baf898d486bfdf4b_reflection_4_Vector3;
        }
        
        // unity-custom-func-begin
        void SSAO_float(float2 normalizedScreenSpaceUV, out float indirectAmbientOcclusion, out float directAmbientOcclusion){
        #if defined(_SCREEN_SPACE_OCCLUSION) && !defined(_SURFACE_TYPE_TRANSPARENT) && !defined(SHADERGRAPH_PREVIEW)
        
            float ssao = saturate(SampleAmbientOcclusion(normalizedScreenSpaceUV) + (1.0 - _AmbientOcclusionParam.x));
        
            indirectAmbientOcclusion = ssao;
        
            directAmbientOcclusion = lerp(half(1.0), ssao, _AmbientOcclusionParam.w);
        
        #else
        
            directAmbientOcclusion = half(1.0);
        
            indirectAmbientOcclusion = half(1.0);
        
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float
        {
        float2 NDCPosition;
        };
        
        void SG_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float(Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float IN, out float indirectAO_1, out float directAO_2)
        {
        float4 _ScreenPosition_0fdc511287e14fd48ca909caba575383_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_indirectAmbientOcclusion_0_Float;
        float _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_directAmbientOcclusion_1_Float;
        SSAO_float((_ScreenPosition_0fdc511287e14fd48ca909caba575383_Out_0_Vector4.xy), _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_indirectAmbientOcclusion_0_Float, _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_directAmbientOcclusion_1_Float);
        indirectAO_1 = _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_indirectAmbientOcclusion_0_Float;
        directAO_2 = _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_directAmbientOcclusion_1_Float;
        }
        
        void Unity_Minimum_float(float A, float B, out float Out)
        {
            Out = min(A, B);
        };
        
        struct Bindings_AmbientURP_300875fdd653fe340b08ad1547984cf1_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_AmbientURP_300875fdd653fe340b08ad1547984cf1_float(float3 _Base_Color, float3 _NormalWS, bool _NormalWS_3a565a44841d4b729f8e86b08d09299c_IsConnected, float _Metallic, float _Smoothness, float3 _Reflectance, float _Ambient_Occlusion, Bindings_AmbientURP_300875fdd653fe340b08ad1547984cf1_float IN, out float3 Ambient_1, out float DirectAO_2)
        {
        float3 _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3 = _NormalWS;
        bool _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3_IsConnected = _NormalWS_3a565a44841d4b729f8e86b08d09299c_IsConnected;
        float3 _BranchOnInputConnection_5e35c0fc2eee4cf7ae47da45409fa2a7_Out_3_Vector3 = _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3_IsConnected ? _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3 : IN.WorldSpaceNormal;
        float3 _BakedGI_1ac35076ff2349f99fec2cef2550ff2d_Out_1_Vector3 = SHADERGRAPH_BAKED_GI(IN.WorldSpacePosition, _BranchOnInputConnection_5e35c0fc2eee4cf7ae47da45409fa2a7_Out_3_Vector3, IN.PixelPosition.xy, IN.uv1.xy, IN.uv2.xy, true);
        float3 _Property_5fb17e215f49424cb9cc9d0806f3f47d_Out_0_Vector3 = _Base_Color;
        float _Property_f995d8544fdb448d85ac845c7bdee967_Out_0_Float = _Metallic;
        float3 _Lerp_842ba4fcd0cf48a3afa108eecd7d56a8_Out_3_Vector3;
        Unity_Lerp_float3(_Property_5fb17e215f49424cb9cc9d0806f3f47d_Out_0_Vector3, float3(0, 0, 0), (_Property_f995d8544fdb448d85ac845c7bdee967_Out_0_Float.xxx), _Lerp_842ba4fcd0cf48a3afa108eecd7d56a8_Out_3_Vector3);
        float3 _Multiply_48ca9faac6354eefabc65eeb591f3fc4_Out_2_Vector3;
        Unity_Multiply_float3_float3(_BakedGI_1ac35076ff2349f99fec2cef2550ff2d_Out_1_Vector3, _Lerp_842ba4fcd0cf48a3afa108eecd7d56a8_Out_3_Vector3, _Multiply_48ca9faac6354eefabc65eeb591f3fc4_Out_2_Vector3);
        float3 _Negate_4f98a1dfbc4d4bd1abe58f406453303f_Out_1_Vector3;
        Unity_Negate_float3(IN.WorldSpaceViewDirection, _Negate_4f98a1dfbc4d4bd1abe58f406453303f_Out_1_Vector3);
        float3 _Reflection_710a69cb22b745929e6bb9b84d11de2f_Out_2_Vector3;
        Unity_Reflection_float3(_Negate_4f98a1dfbc4d4bd1abe58f406453303f_Out_1_Vector3, _BranchOnInputConnection_5e35c0fc2eee4cf7ae47da45409fa2a7_Out_3_Vector3, _Reflection_710a69cb22b745929e6bb9b84d11de2f_Out_2_Vector3);
        float _Property_8c3e921b9cb34f7b82d2a71254653c09_Out_0_Float = _Smoothness;
        Bindings_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.WorldSpaceNormal = IN.WorldSpaceNormal;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.WorldSpacePosition = IN.WorldSpacePosition;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.NDCPosition = IN.NDCPosition;
        float3 _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08_Out_1_Vector3;
        SG_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float(half3 (0, 0, 0), false, _Reflection_710a69cb22b745929e6bb9b84d11de2f_Out_2_Vector3, true, _Property_8c3e921b9cb34f7b82d2a71254653c09_Out_0_Float, half(1), _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08, _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08_Out_1_Vector3);
        float3 _Property_2ddaa58bd1e94d0b8508ce91ad39fa39_Out_0_Vector3 = _Reflectance;
        float3 _Multiply_40be6c08ca2f44ef99b6a2b81955d62c_Out_2_Vector3;
        Unity_Multiply_float3_float3(_SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08_Out_1_Vector3, _Property_2ddaa58bd1e94d0b8508ce91ad39fa39_Out_0_Vector3, _Multiply_40be6c08ca2f44ef99b6a2b81955d62c_Out_2_Vector3);
        float3 _Add_c0a57a54eb8b4419a7e907a65e6556a7_Out_2_Vector3;
        Unity_Add_float3(_Multiply_48ca9faac6354eefabc65eeb591f3fc4_Out_2_Vector3, _Multiply_40be6c08ca2f44ef99b6a2b81955d62c_Out_2_Vector3, _Add_c0a57a54eb8b4419a7e907a65e6556a7_Out_2_Vector3);
        float _Property_5a996c5d941d46019b3ac5ada526c475_Out_0_Float = _Ambient_Occlusion;
        Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e;
        _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e.NDCPosition = IN.NDCPosition;
        half _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_indirectAO_1_Float;
        half _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_directAO_2_Float;
        SG_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float(_ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_indirectAO_1_Float, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_directAO_2_Float);
        float _Minimum_699e1643e9974baa97f6f21c8f949d09_Out_2_Float;
        Unity_Minimum_float(_Property_5a996c5d941d46019b3ac5ada526c475_Out_0_Float, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_indirectAO_1_Float, _Minimum_699e1643e9974baa97f6f21c8f949d09_Out_2_Float);
        float3 _Multiply_ccaf4dfa5cdb4d9fae7fd92dc7d512da_Out_2_Vector3;
        Unity_Multiply_float3_float3(_Add_c0a57a54eb8b4419a7e907a65e6556a7_Out_2_Vector3, (_Minimum_699e1643e9974baa97f6f21c8f949d09_Out_2_Float.xxx), _Multiply_ccaf4dfa5cdb4d9fae7fd92dc7d512da_Out_2_Vector3);
        float _Minimum_c4dcab6b18b34dde987e68f86d22aed7_Out_2_Float;
        Unity_Minimum_float(_Property_5a996c5d941d46019b3ac5ada526c475_Out_0_Float, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_directAO_2_Float, _Minimum_c4dcab6b18b34dde987e68f86d22aed7_Out_2_Float);
        Ambient_1 = _Multiply_ccaf4dfa5cdb4d9fae7fd92dc7d512da_Out_2_Vector3;
        DirectAO_2 = _Minimum_c4dcab6b18b34dde987e68f86d22aed7_Out_2_Float;
        }
        
        void Unity_Fog_float(out float4 Color, out float Density, float3 Position)
        {
            SHADERGRAPH_FOG(Position, Color, Density);
        }
        
        struct Bindings_Fog_286ae83400099a24bba6faf005588be7_float
        {
        float3 ObjectSpacePosition;
        };
        
        void SG_Fog_286ae83400099a24bba6faf005588be7_float(float3 _In, Bindings_Fog_286ae83400099a24bba6faf005588be7_float IN, out float3 Out_1)
        {
        float3 _Property_626923dc627443639da97776de7dcc22_Out_0_Vector3 = _In;
        float4 _Fog_acabe3c84c9549f3880ce7d106150576_Color_0_Vector4;
        float _Fog_acabe3c84c9549f3880ce7d106150576_Density_1_Float;
        Unity_Fog_float(_Fog_acabe3c84c9549f3880ce7d106150576_Color_0_Vector4, _Fog_acabe3c84c9549f3880ce7d106150576_Density_1_Float, IN.ObjectSpacePosition);
        float3 _Lerp_9cfca9aa08c7423bab62b39a01237d64_Out_3_Vector3;
        Unity_Lerp_float3(_Property_626923dc627443639da97776de7dcc22_Out_0_Vector3, (_Fog_acabe3c84c9549f3880ce7d106150576_Color_0_Vector4.xyz), (_Fog_acabe3c84c9549f3880ce7d106150576_Density_1_Float.xxx), _Lerp_9cfca9aa08c7423bab62b39a01237d64_Out_3_Vector3);
        Out_1 = _Lerp_9cfca9aa08c7423bab62b39a01237d64_Out_3_Vector3;
        }
        
        void Unity_Saturate_float3(float3 In, out float3 Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Saturation_float(float3 In, float Saturation, out float3 Out)
        {
            float luma = dot(In, float3(0.2126729, 0.7151522, 0.0721750));
            Out =  luma.xxx + Saturation.xxx * (In - luma.xxx);
        }
        
        void Unity_Contrast_float(float3 In, float Contrast, out float3 Out)
        {
            float midpoint = pow(0.5, 2.2);
            Out =  (In - midpoint) * Contrast + midpoint;
        }
        
        void Unity_Remap_float3(float3 In, float2 InMinMax, float2 OutMinMax, out float3 Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        struct Bindings_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpaceViewDirection;
        float3 ObjectSpacePosition;
        float3 WorldSpacePosition;
        float3 AbsoluteWorldSpacePosition;
        float2 NDCPosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float(float3 _Base_Color, float3 _Normal, bool _Normal_e1611e545480449d80aa5a0e7c2b63c4_IsConnected, float _Metallic, float _Smoothness, float _Micro_Occlusion, float _Ambient_Occlusion, float4 _Color_A, float2 _Color_A_Location, float4 _Color_B, float2 _Color_B_Location, float4 _Color_C, Bindings_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float IN, out float3 Lit_1)
        {
        float4 _Property_bedee92e97ce4b2abff5524ce019b2a8_Out_0_Vector4 = _Color_A;
        float4 _Property_c648ff794fd34283beff09e33d8293fc_Out_0_Vector4 = _Color_B;
        float3 _Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3 = _Normal;
        bool _Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3_IsConnected = _Normal_e1611e545480449d80aa5a0e7c2b63c4_IsConnected;
        float3 _Transform_75d5405941a444c088600c650a7269f0_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_75d5405941a444c088600c650a7269f0_Out_1_Vector3 = TransformTangentToWorld(_Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float3 _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3 = _Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3_IsConnected ? _Transform_75d5405941a444c088600c650a7269f0_Out_1_Vector3 : IN.WorldSpaceNormal;
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float _MainLight_d918d0814080438585a810ba0b8afeb4;
        _MainLight_d918d0814080438585a810ba0b8afeb4.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3;
        half3 _MainLight_d918d0814080438585a810ba0b8afeb4_Color_2_Vector3;
        half _MainLight_d918d0814080438585a810ba0b8afeb4_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(_MainLight_d918d0814080438585a810ba0b8afeb4, _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3, _MainLight_d918d0814080438585a810ba0b8afeb4_Color_2_Vector3, _MainLight_d918d0814080438585a810ba0b8afeb4_ShadowAtten_3_Float);
        Bindings_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half _DiffuseLambert_7f9e988376a2438ebc87097469e065d3;
        _DiffuseLambert_7f9e988376a2438ebc87097469e065d3.WorldSpaceNormal = IN.WorldSpaceNormal;
        _DiffuseLambert_7f9e988376a2438ebc87097469e065d3.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half _DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float;
        SG_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half(_BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3, true, _DiffuseLambert_7f9e988376a2438ebc87097469e065d3, _DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float);
        float _Property_ac10139ecedb4301b4595fa5b13c00b8_Out_0_Float = _Smoothness;
        float3 _Property_58fa7de1b7784467935169b8914ee373_Out_0_Vector3 = _Base_Color;
        float _Property_433019e2d18944a2909e58d06f7cc1ec_Out_0_Float = _Metallic;
        float _Property_dd950e92d2d54fefbb89aaa0d1f6b713_Out_0_Float = _Smoothness;
        float _Property_dce33fc56d0e4204bd34d323af11f8ca_Out_0_Float = _Micro_Occlusion;
        float _Multiply_d120aee0dde541e69ff32688ddb2bee0_Out_2_Float;
        Unity_Multiply_float_float(_Property_dce33fc56d0e4204bd34d323af11f8ca_Out_0_Float, 0.5, _Multiply_d120aee0dde541e69ff32688ddb2bee0_Out_2_Float);
        float _Lerp_6742a96ab4984154ae535aef42b348f7_Out_3_Float;
        Unity_Lerp_float(float(0), float(0.08), _Multiply_d120aee0dde541e69ff32688ddb2bee0_Out_2_Float, _Lerp_6742a96ab4984154ae535aef42b348f7_Out_3_Float);
        Bindings_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d;
        _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d.WorldSpaceNormal = IN.WorldSpaceNormal;
        _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        half3 _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3;
        SG_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float(_Property_58fa7de1b7784467935169b8914ee373_Out_0_Vector3, _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_433019e2d18944a2909e58d06f7cc1ec_Out_0_Float, _Property_dd950e92d2d54fefbb89aaa0d1f6b713_Out_0_Float, _Lerp_6742a96ab4984154ae535aef42b348f7_Out_3_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3);
        Bindings_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3;
        _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3.WorldSpaceNormal = IN.WorldSpaceNormal;
        _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3_Specular_1_Vector3;
        SG_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float(_BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_ac10139ecedb4301b4595fa5b13c00b8_Out_0_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3, _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3, true, _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3, _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3_Specular_1_Vector3);
        float3 _Multiply_7b7e19c594534a458849928b4dc06aec_Out_2_Vector3;
        Unity_Multiply_float3_float3((_DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float.xxx), _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3_Specular_1_Vector3, _Multiply_7b7e19c594534a458849928b4dc06aec_Out_2_Vector3);
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e;
        _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Direction_1_Vector3;
        half3 _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Color_2_Vector3;
        half _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(_MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e, _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Direction_1_Vector3, _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Color_2_Vector3, _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_ShadowAtten_3_Float);
        float3 _Multiply_9545e2014858428792b92c57dd77e45c_Out_2_Vector3;
        Unity_Multiply_float3_float3(_MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Color_2_Vector3, (_MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_ShadowAtten_3_Float.xxx), _Multiply_9545e2014858428792b92c57dd77e45c_Out_2_Vector3);
        float _Property_be391ee1d2f24bada2da9fc9d603f6a9_Out_0_Float = _Smoothness;
        Bindings_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.WorldSpaceNormal = IN.WorldSpaceNormal;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.WorldSpacePosition = IN.WorldSpacePosition;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.NDCPosition = IN.NDCPosition;
        half _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Diffuse_1_Float;
        half3 _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Specular_2_Vector3;
        half3 _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Color_3_Vector3;
        SG_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float(_DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float, _Multiply_7b7e19c594534a458849928b4dc06aec_Out_2_Vector3, _Multiply_9545e2014858428792b92c57dd77e45c_Out_2_Vector3, _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_be391ee1d2f24bada2da9fc9d603f6a9_Out_0_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Diffuse_1_Float, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Specular_2_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Color_3_Vector3);
        float3 _Property_8804fec07c534721b9d4e6def9182fad_Out_0_Vector3 = _Base_Color;
        float3 _Multiply_765841b19beb4cecb786b3295b6aa8e9_Out_2_Vector3;
        Unity_Multiply_float3_float3((_AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Diffuse_1_Float.xxx), _Property_8804fec07c534721b9d4e6def9182fad_Out_0_Vector3, _Multiply_765841b19beb4cecb786b3295b6aa8e9_Out_2_Vector3);
        float3 _Add_77980781d2204e1b8d249e5f7058e9fb_Out_2_Vector3;
        Unity_Add_float3(_Multiply_765841b19beb4cecb786b3295b6aa8e9_Out_2_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Specular_2_Vector3, _Add_77980781d2204e1b8d249e5f7058e9fb_Out_2_Vector3);
        float3 _Multiply_9df1a4a5bf654e97b945b2eccf2fc855_Out_2_Vector3;
        Unity_Multiply_float3_float3(_Add_77980781d2204e1b8d249e5f7058e9fb_Out_2_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Color_3_Vector3, _Multiply_9df1a4a5bf654e97b945b2eccf2fc855_Out_2_Vector3);
        float3 _Property_e9725e93976b4fcaa5fea397628348dd_Out_0_Vector3 = _Base_Color;
        float _Property_e926bef11147490b98b69d5bec06eaa9_Out_0_Float = _Metallic;
        float _Property_1465a416fe734e4e83f2401b9c4d3fdb_Out_0_Float = _Smoothness;
        float _Property_0a056a259612407e813453c548affc50_Out_0_Float = _Ambient_Occlusion;
        Bindings_AmbientURP_300875fdd653fe340b08ad1547984cf1_float _AmbientURP_46e1712500da4aae848bd5b24a05f29f;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.WorldSpaceNormal = IN.WorldSpaceNormal;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.WorldSpacePosition = IN.WorldSpacePosition;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.NDCPosition = IN.NDCPosition;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.PixelPosition = IN.PixelPosition;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.uv1 = IN.uv1;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.uv2 = IN.uv2;
        half3 _AmbientURP_46e1712500da4aae848bd5b24a05f29f_Ambient_1_Vector3;
        half _AmbientURP_46e1712500da4aae848bd5b24a05f29f_DirectAO_2_Float;
        SG_AmbientURP_300875fdd653fe340b08ad1547984cf1_float(_Property_e9725e93976b4fcaa5fea397628348dd_Out_0_Vector3, _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_e926bef11147490b98b69d5bec06eaa9_Out_0_Float, _Property_1465a416fe734e4e83f2401b9c4d3fdb_Out_0_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3, _Property_0a056a259612407e813453c548affc50_Out_0_Float, _AmbientURP_46e1712500da4aae848bd5b24a05f29f, _AmbientURP_46e1712500da4aae848bd5b24a05f29f_Ambient_1_Vector3, _AmbientURP_46e1712500da4aae848bd5b24a05f29f_DirectAO_2_Float);
        float3 _Add_ad0b3eccabf24dceb374546c3c332ad7_Out_2_Vector3;
        Unity_Add_float3(_Multiply_9df1a4a5bf654e97b945b2eccf2fc855_Out_2_Vector3, _AmbientURP_46e1712500da4aae848bd5b24a05f29f_Ambient_1_Vector3, _Add_ad0b3eccabf24dceb374546c3c332ad7_Out_2_Vector3);
        Bindings_Fog_286ae83400099a24bba6faf005588be7_float _Fog_f4025f6ca9e74f948bc7263ef71d324a;
        _Fog_f4025f6ca9e74f948bc7263ef71d324a.ObjectSpacePosition = IN.ObjectSpacePosition;
        half3 _Fog_f4025f6ca9e74f948bc7263ef71d324a_Out_1_Vector3;
        SG_Fog_286ae83400099a24bba6faf005588be7_float(_Add_ad0b3eccabf24dceb374546c3c332ad7_Out_2_Vector3, _Fog_f4025f6ca9e74f948bc7263ef71d324a, _Fog_f4025f6ca9e74f948bc7263ef71d324a_Out_1_Vector3);
        float3 _Saturate_b798de6279164a2b9155cd8251d26092_Out_1_Vector3;
        Unity_Saturate_float3(_Fog_f4025f6ca9e74f948bc7263ef71d324a_Out_1_Vector3, _Saturate_b798de6279164a2b9155cd8251d26092_Out_1_Vector3);
        float3 _Saturation_ad4a69e303864b54940190442ea5d857_Out_2_Vector3;
        Unity_Saturation_float(_Saturate_b798de6279164a2b9155cd8251d26092_Out_1_Vector3, float(0), _Saturation_ad4a69e303864b54940190442ea5d857_Out_2_Vector3);
        float _Swizzle_bae47f72b28a4941b7665012b9c55203_Out_1_Float = (_Saturation_ad4a69e303864b54940190442ea5d857_Out_2_Vector3).x.x;
        float _Power_83030291203f43f9b048356e4fbaf99e_Out_2_Float;
        Unity_Power_float(_Swizzle_bae47f72b28a4941b7665012b9c55203_Out_1_Float, float(0.45), _Power_83030291203f43f9b048356e4fbaf99e_Out_2_Float);
        float3 _Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3;
        Unity_Contrast_float((_Power_83030291203f43f9b048356e4fbaf99e_Out_2_Float.xxx), float(2), _Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3);
        float2 _Property_7fc6298ec06c474b99191c5a5156da72_Out_0_Vector2 = _Color_A_Location;
        float3 _Remap_6d4565d12b3a4f71b23e18189b7389b3_Out_3_Vector3;
        Unity_Remap_float3(_Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3, _Property_7fc6298ec06c474b99191c5a5156da72_Out_0_Vector2, float2 (0, 1), _Remap_6d4565d12b3a4f71b23e18189b7389b3_Out_3_Vector3);
        float3 _Saturate_2728e5e4642645239b2f9a703cc1622a_Out_1_Vector3;
        Unity_Saturate_float3(_Remap_6d4565d12b3a4f71b23e18189b7389b3_Out_3_Vector3, _Saturate_2728e5e4642645239b2f9a703cc1622a_Out_1_Vector3);
        float3 _Lerp_20eaa2d06da4491384cac8b19ba49f2c_Out_3_Vector3;
        Unity_Lerp_float3((_Property_bedee92e97ce4b2abff5524ce019b2a8_Out_0_Vector4.xyz), (_Property_c648ff794fd34283beff09e33d8293fc_Out_0_Vector4.xyz), _Saturate_2728e5e4642645239b2f9a703cc1622a_Out_1_Vector3, _Lerp_20eaa2d06da4491384cac8b19ba49f2c_Out_3_Vector3);
        float4 _Property_fd1357d1b32e4a29a4cbcc3613f244e3_Out_0_Vector4 = _Color_C;
        float2 _Property_c2df701a69af4187a31c6f8cfcb26846_Out_0_Vector2 = _Color_B_Location;
        float3 _Remap_0c80541bb2da437698e33d379210a687_Out_3_Vector3;
        Unity_Remap_float3(_Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3, _Property_c2df701a69af4187a31c6f8cfcb26846_Out_0_Vector2, float2 (0, 1), _Remap_0c80541bb2da437698e33d379210a687_Out_3_Vector3);
        float3 _Saturate_5b42e55c7d0c45f1a92ce0c1c045003a_Out_1_Vector3;
        Unity_Saturate_float3(_Remap_0c80541bb2da437698e33d379210a687_Out_3_Vector3, _Saturate_5b42e55c7d0c45f1a92ce0c1c045003a_Out_1_Vector3);
        float3 _Lerp_b1cbf1c8cc0549a69912223d40ba4cfa_Out_3_Vector3;
        Unity_Lerp_float3(_Lerp_20eaa2d06da4491384cac8b19ba49f2c_Out_3_Vector3, (_Property_fd1357d1b32e4a29a4cbcc3613f244e3_Out_0_Vector4.xyz), _Saturate_5b42e55c7d0c45f1a92ce0c1c045003a_Out_1_Vector3, _Lerp_b1cbf1c8cc0549a69912223d40ba4cfa_Out_3_Vector3);
        Lit_1 = _Lerp_b1cbf1c8cc0549a69912223d40ba4cfa_Out_3_Vector3;
        }
        
        // unity-custom-func-begin
        void DebugMaterialSwitch_float(float3 None, float3 Albedo, float3 Specular, float3 Alpha, float3 Smoothness, float3 AmbientOcclusion, float3 Emission, float3 NormalWS, float3 NormalTS, float3 LightComplexity, float3 Metallic, float3 SpriteMask, float3 RenderingLayerMasks, out float3 Out){
        Out = None;
        #if !defined(SHADERGRAPH_PREVIEW) && defined(DEBUG_DISPLAY)
        [branch] switch(int(_DebugMaterialMode))
        
        {
        
            case 0:
        
                Out = None; break;
        
            case 1:
        
                Out = Albedo; break;
        
            case 2:
        
                Out = Specular; break;
            case 3:
        
                Out = Alpha; break;
            case 4:
        
                Out = Smoothness; break;
            case 5:
        
                Out = AmbientOcclusion;  break;
            case 6:
        
                Out = Emission;  break;
            case 7:
        
                Out = NormalWS * 0.5 + 0.5;  break;
            case 8:
        
                Out = NormalTS * 0.5 + 0.5;  break;
            case 9:
        
                Out = LightComplexity;  break;
            case 10:
        
                Out = Metallic;  break;
            case 11:
        
                Out = SpriteMask;  break;
            case 12:
        
                Out = RenderingLayerMasks;  break;
        
            default:
        
                Out = None; break;
        
        }
        #endif
        
        // Disable this define to prevent the global unlit
        // fragment pass to override the color output again.
        #undef DEBUG_DISPLAY
        }
        // unity-custom-func-end
        
        struct Bindings_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        };
        
        void SG_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float(float3 _In, float3 _Base_Color, float3 _Normal, float _Metallic, float _Smoothness, float3 _Emission, float _Ambient_Occlusion, float _Alpha, Bindings_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float IN, out float3 Out_1)
        {
        float3 _Property_dd011cc96ae64d1181317986b1fa1742_Out_0_Vector3 = _In;
        float3 _Property_5653941ce5a641f18f7ce7012652025d_Out_0_Vector3 = _Base_Color;
        float _Property_45f5c13ff5544581bd61c2442cecd0a1_Out_0_Float = _Alpha;
        float _Property_b6c8b448c5324bd3bc59540f628e43a3_Out_0_Float = _Smoothness;
        Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5;
        _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5.NDCPosition = IN.NDCPosition;
        half _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_indirectAO_1_Float;
        half _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_directAO_2_Float;
        SG_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float(_ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5, _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_indirectAO_1_Float, _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_directAO_2_Float);
        float _Property_441143660ff642349088dd1bcab6bc78_Out_0_Float = _Ambient_Occlusion;
        float _Minimum_8ee95b9bf7ac4776b6ee4edf1214b3c1_Out_2_Float;
        Unity_Minimum_float(_ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_indirectAO_1_Float, _Property_441143660ff642349088dd1bcab6bc78_Out_0_Float, _Minimum_8ee95b9bf7ac4776b6ee4edf1214b3c1_Out_2_Float);
        float3 _Property_b171431b5a3b4b0a9fc9fdede4a532a7_Out_0_Vector3 = _Emission;
        float3 _Property_db9eb36ed51d4aad95e383920b55e3d7_Out_0_Vector3 = _Normal;
        float3 _Transform_9e831bda1f4d4495b24f1f6e3075f2fb_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_9e831bda1f4d4495b24f1f6e3075f2fb_Out_1_Vector3 = TransformTangentToWorld(_Property_db9eb36ed51d4aad95e383920b55e3d7_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float3 _Property_4eaab22b2b784aeda3752622f7abaf85_Out_0_Vector3 = _Normal;
        float4 _ScreenPosition_121436dfdd464829910775b2326b046b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float3 _Property_1b1e0a48277e4883afeb1289a075c5d8_Out_0_Vector3 = _Base_Color;
        float3 _LightingComplexityCustomFunction_cbe0c0f96f9046a584e17ead8c001a55_Out_3_Vector3;
        LightingComplexity_float((_ScreenPosition_121436dfdd464829910775b2326b046b_Out_0_Vector4.xy), IN.WorldSpacePosition, _Property_1b1e0a48277e4883afeb1289a075c5d8_Out_0_Vector3, _LightingComplexityCustomFunction_cbe0c0f96f9046a584e17ead8c001a55_Out_3_Vector3);
        float _Property_dcd3ca7796af45c6857884fa7979898b_Out_0_Float = _Metallic;
        float3 _DebugMaterialSwitchCustomFunction_e1fabc2a3bcd4bc183f0e93c379657d4_Out_5_Vector3;
        DebugMaterialSwitch_float(_Property_dd011cc96ae64d1181317986b1fa1742_Out_0_Vector3, _Property_5653941ce5a641f18f7ce7012652025d_Out_0_Vector3, float3 (0, 0, 0), (_Property_45f5c13ff5544581bd61c2442cecd0a1_Out_0_Float.xxx), (_Property_b6c8b448c5324bd3bc59540f628e43a3_Out_0_Float.xxx), (_Minimum_8ee95b9bf7ac4776b6ee4edf1214b3c1_Out_2_Float.xxx), _Property_b171431b5a3b4b0a9fc9fdede4a532a7_Out_0_Vector3, _Transform_9e831bda1f4d4495b24f1f6e3075f2fb_Out_1_Vector3, _Property_4eaab22b2b784aeda3752622f7abaf85_Out_0_Vector3, _LightingComplexityCustomFunction_cbe0c0f96f9046a584e17ead8c001a55_Out_3_Vector3, (_Property_dcd3ca7796af45c6857884fa7979898b_Out_0_Float.xxx), float3 (0, 0, 0), float3 (0, 0, 0), _DebugMaterialSwitchCustomFunction_e1fabc2a3bcd4bc183f0e93c379657d4_Out_5_Vector3);
        Out_1 = _DebugMaterialSwitchCustomFunction_e1fabc2a3bcd4bc183f0e93c379657d4_Out_5_Vector3;
        }
        
        struct Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpaceViewDirection;
        float3 ObjectSpacePosition;
        float3 WorldSpacePosition;
        float3 AbsoluteWorldSpacePosition;
        float2 NDCPosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float(float3 _Base_Color, float3 _Normal, float _Metallic, float _Smoothness, float3 _Emission, float _AmbientOcclusion, float _Alpha, float4 _Color_A, float2 _Color_A_Location, float4 _Color_B, float2 _Color_B_Location, float4 _Color_C, Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float IN, out float3 Lit_1)
        {
        float3 _Property_04a055764411443d802bfbbd0d510c65_Out_0_Vector3 = _Base_Color;
        float3 _Property_383a017d83a8420dac016260bc833f58_Out_0_Vector3 = _Normal;
        float3 _Transform_3f94cf9dbe844abc9b6727d5d289074f_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_3f94cf9dbe844abc9b6727d5d289074f_Out_1_Vector3 = TransformTangentToWorld(_Property_383a017d83a8420dac016260bc833f58_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float _Property_11295d868ff34c388c9212b90b781aff_Out_0_Float = _Metallic;
        float _Property_b522b61b85ff4ecbb0eb63cff689f5cb_Out_0_Float = _Smoothness;
        float _Property_a1dc37a47c5640d0870861199df0bd70_Out_0_Float = _AmbientOcclusion;
        Bindings_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float _ApplyDecals_0413903f5da5491d911d117142eabddd;
        _ApplyDecals_0413903f5da5491d911d117142eabddd.PixelPosition = IN.PixelPosition;
        half3 _ApplyDecals_0413903f5da5491d911d117142eabddd_BaseColor_1_Vector3;
        half3 _ApplyDecals_0413903f5da5491d911d117142eabddd_SpecularColor_2_Vector3;
        half3 _ApplyDecals_0413903f5da5491d911d117142eabddd_NormalWS_3_Vector3;
        half _ApplyDecals_0413903f5da5491d911d117142eabddd_Metallic_4_Float;
        half _ApplyDecals_0413903f5da5491d911d117142eabddd_Smoothness_6_Float;
        half _ApplyDecals_0413903f5da5491d911d117142eabddd_AmbientOcclusion_5_Float;
        SG_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float(_Property_04a055764411443d802bfbbd0d510c65_Out_0_Vector3, _Transform_3f94cf9dbe844abc9b6727d5d289074f_Out_1_Vector3, _Property_11295d868ff34c388c9212b90b781aff_Out_0_Float, _Property_b522b61b85ff4ecbb0eb63cff689f5cb_Out_0_Float, _Property_a1dc37a47c5640d0870861199df0bd70_Out_0_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd, _ApplyDecals_0413903f5da5491d911d117142eabddd_BaseColor_1_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_SpecularColor_2_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_NormalWS_3_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_Metallic_4_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd_Smoothness_6_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd_AmbientOcclusion_5_Float);
        float3 _Property_b986326ad9b34d6ea3a7237ba2bd1cd6_Out_0_Vector3 = _Emission;
        Bindings_DebugLighting_61e571d2b9ede1240a524a849d20c997_float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpaceNormal = IN.WorldSpaceNormal;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpaceTangent = IN.WorldSpaceTangent;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpacePosition = IN.WorldSpacePosition;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.PixelPosition = IN.PixelPosition;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.uv1 = IN.uv1;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.uv2 = IN.uv2;
        float3 _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3;
        float3 _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3;
        float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float;
        float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float;
        float3 _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3;
        float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float;
        SG_DebugLighting_61e571d2b9ede1240a524a849d20c997_float(_ApplyDecals_0413903f5da5491d911d117142eabddd_BaseColor_1_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_NormalWS_3_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_Metallic_4_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd_Smoothness_6_Float, _Property_b986326ad9b34d6ea3a7237ba2bd1cd6_Out_0_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_AmbientOcclusion_5_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float);
        float4 _Property_a2c93b67d7e14184996710181bc8106a_Out_0_Vector4 = _Color_A;
        float2 _Property_30bd0eecac2f497db8c8b272e8e7d3e5_Out_0_Vector2 = _Color_A_Location;
        float4 _Property_47fc9a397b1241599709d29487238203_Out_0_Vector4 = _Color_B;
        float2 _Property_99bf1a52083e4b7f84197e960ed6a728_Out_0_Vector2 = _Color_B_Location;
        float4 _Property_d5f33cf319a54be08f26ec7c7538d6a4_Out_0_Vector4 = _Color_C;
        Bindings_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceNormal = IN.WorldSpaceNormal;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceTangent = IN.WorldSpaceTangent;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.ObjectSpacePosition = IN.ObjectSpacePosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpacePosition = IN.WorldSpacePosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.NDCPosition = IN.NDCPosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.PixelPosition = IN.PixelPosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.uv1 = IN.uv1;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.uv2 = IN.uv2;
        half3 _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc_Lit_1_Vector3;
        SG_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float(_DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3, true, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float, half(1), _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float, _Property_a2c93b67d7e14184996710181bc8106a_Out_0_Vector4, _Property_30bd0eecac2f497db8c8b272e8e7d3e5_Out_0_Vector2, _Property_47fc9a397b1241599709d29487238203_Out_0_Vector4, _Property_99bf1a52083e4b7f84197e960ed6a728_Out_0_Vector2, _Property_d5f33cf319a54be08f26ec7c7538d6a4_Out_0_Vector4, _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc, _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc_Lit_1_Vector3);
        float3 _Add_a11e24e7d4fd4494895fd67f375acb21_Out_2_Vector3;
        Unity_Add_float3(_SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc_Lit_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3, _Add_a11e24e7d4fd4494895fd67f375acb21_Out_2_Vector3);
        float _Property_d5e8251fc84a46aea1765511445b653e_Out_0_Float = _Alpha;
        Bindings_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpaceNormal = IN.WorldSpaceNormal;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpaceTangent = IN.WorldSpaceTangent;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpacePosition = IN.WorldSpacePosition;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.NDCPosition = IN.NDCPosition;
        float3 _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3_Out_1_Vector3;
        SG_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float(_Add_a11e24e7d4fd4494895fd67f375acb21_Out_2_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float, _Property_d5e8251fc84a46aea1765511445b653e_Out_0_Float, _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3, _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3_Out_1_Vector3);
        Lit_1 = _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3_Out_1_Vector3;
        }
        
        void Unity_Blend_Overwrite_float3(float3 Base, float3 Blend, out float3 Out, float Opacity)
        {
            Out = lerp(Base, Blend, Opacity);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float3 BaseColor;
            float3 Emission;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_debbfbf1a581455dbc61b338f851a8d4_Out_0_Boolean = _DisableAllGradients;
            float _Property_2fb138ca7c89409da2d3e517c9bcb36b_Out_0_Boolean = _DisableGradientMap;
            UnityTexture2D _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D = UnityBuildTexture2DStructInternal(_BaseColor, sampler_BaseColor, _BaseColor_TexelSize, float4(1, 1, 0, 0), float4(0, 0, 0, 0));
            float2 _Property_94094af0818841fea1396a1ccae7a167_Out_0_Vector2 = _Tilling;
            float2 _TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2;
            Unity_TilingAndOffset_float(IN.uv0.xy, _Property_94094af0818841fea1396a1ccae7a167_Out_0_Vector2, float2 (0, 0), _TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2);
            float4 _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.tex, _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.samplerstate, _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.GetTransformedUV(_TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2) );
            if (_Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.hdrDecode.x > 0)
                _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4, _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.hdrDecode);
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.r;
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_G_5_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.g;
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_B_6_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.b;
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_A_7_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.a;
            float4 _Property_de1bd6122d6c4aa4ba9d7692e6db8956_Out_0_Vector4 = _Color1;
            float4 _Property_fceba521f35a4cc88bbd9602ff68242f_Out_0_Vector4 = _Color2;
            float2 _Property_e60b8198308f4aad8dfa7a52168790ce_Out_0_Vector2 = _Color_1_Location;
            float _Remap_b9f81469352045d7ae4bb207f0c202fc_Out_3_Float;
            Unity_Remap_float(_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float, _Property_e60b8198308f4aad8dfa7a52168790ce_Out_0_Vector2, float2 (0, 1), _Remap_b9f81469352045d7ae4bb207f0c202fc_Out_3_Float);
            float _Saturate_473bb6d59e074412ad7162adcfe4cef1_Out_1_Float;
            Unity_Saturate_float(_Remap_b9f81469352045d7ae4bb207f0c202fc_Out_3_Float, _Saturate_473bb6d59e074412ad7162adcfe4cef1_Out_1_Float);
            float4 _Lerp_f3e294dc316840b6b4294bcb3e7b3f7b_Out_3_Vector4;
            Unity_Lerp_float4(_Property_de1bd6122d6c4aa4ba9d7692e6db8956_Out_0_Vector4, _Property_fceba521f35a4cc88bbd9602ff68242f_Out_0_Vector4, (_Saturate_473bb6d59e074412ad7162adcfe4cef1_Out_1_Float.xxxx), _Lerp_f3e294dc316840b6b4294bcb3e7b3f7b_Out_3_Vector4);
            float4 _Property_60ebe0bb4a0e4a6fbd758422fbc8e1af_Out_0_Vector4 = _Color3;
            float2 _Property_46eb955dacc0426abc5d73b1be33af42_Out_0_Vector2 = _Color_2_Location;
            float _Remap_c2e999a017a6490b91665566afc5916d_Out_3_Float;
            Unity_Remap_float(_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float, _Property_46eb955dacc0426abc5d73b1be33af42_Out_0_Vector2, float2 (0, 1), _Remap_c2e999a017a6490b91665566afc5916d_Out_3_Float);
            float _Saturate_72fb61dd2f064241a5337c41de422b36_Out_1_Float;
            Unity_Saturate_float(_Remap_c2e999a017a6490b91665566afc5916d_Out_3_Float, _Saturate_72fb61dd2f064241a5337c41de422b36_Out_1_Float);
            float4 _Lerp_72f03acdde2245b29f8cd4a47d6b4cd6_Out_3_Vector4;
            Unity_Lerp_float4(_Lerp_f3e294dc316840b6b4294bcb3e7b3f7b_Out_3_Vector4, _Property_60ebe0bb4a0e4a6fbd758422fbc8e1af_Out_0_Vector4, (_Saturate_72fb61dd2f064241a5337c41de422b36_Out_1_Float.xxxx), _Lerp_72f03acdde2245b29f8cd4a47d6b4cd6_Out_3_Vector4);
            float4 _Property_e84e577bc7db46749ec6367493b51e06_Out_0_Vector4 = _AOColor;
            UnityTexture2D _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D = UnityBuildTexture2DStructInternal(_ORM, sampler_ORM, _ORM_TexelSize, float4(1, 1, 0, 0), float4(0, 0, 0, 0));
            float4 _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.tex, _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.samplerstate, _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.GetTransformedUV(_TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2) );
            if (_Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.hdrDecode.x > 0)
                _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4, _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.hdrDecode);
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_R_4_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.r;
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_G_5_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.g;
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.b;
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_A_7_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.a;
            float _OneMinus_89dcf9413d9142c481cf9d5e5035b712_Out_1_Float;
            Unity_OneMinus_float(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_R_4_Float, _OneMinus_89dcf9413d9142c481cf9d5e5035b712_Out_1_Float);
            float2 _Property_753b87b3c47d403696efc934ea3dbea9_Out_0_Vector2 = _AOLevels;
            float _Remap_983e82311f6e4fe5b382c71d656497ec_Out_3_Float;
            Unity_Remap_float(_OneMinus_89dcf9413d9142c481cf9d5e5035b712_Out_1_Float, _Property_753b87b3c47d403696efc934ea3dbea9_Out_0_Vector2, float2 (1, 0), _Remap_983e82311f6e4fe5b382c71d656497ec_Out_3_Float);
            float _Property_ba1b5b1e95414eaebf54a9d26291e91f_Out_0_Float = _AOIntensity;
            float _Multiply_2341d6d389c44e96bba10c0c1e0c9f14_Out_2_Float;
            Unity_Multiply_float_float(_Remap_983e82311f6e4fe5b382c71d656497ec_Out_3_Float, _Property_ba1b5b1e95414eaebf54a9d26291e91f_Out_0_Float, _Multiply_2341d6d389c44e96bba10c0c1e0c9f14_Out_2_Float);
            float4 _Lerp_8d7f6575739049459e47aaca25cf59d0_Out_3_Vector4;
            Unity_Lerp_float4(_Lerp_72f03acdde2245b29f8cd4a47d6b4cd6_Out_3_Vector4, _Property_e84e577bc7db46749ec6367493b51e06_Out_0_Vector4, (_Multiply_2341d6d389c44e96bba10c0c1e0c9f14_Out_2_Float.xxxx), _Lerp_8d7f6575739049459e47aaca25cf59d0_Out_3_Vector4);
            float4 _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4;
            Unity_Branch_float4(_Property_2fb138ca7c89409da2d3e517c9bcb36b_Out_0_Boolean, (_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float.xxxx), _Lerp_8d7f6575739049459e47aaca25cf59d0_Out_3_Vector4, _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4);
            float _Property_18ebff8ae81646538dc78b9020d39b78_Out_0_Boolean = _Z_Gradient;
            float _Property_f94879ac790e4c109a34ec1f73a3c3a6_Out_0_Boolean = _Y_Gradient;
            float _Property_c61e8f7030cd47ac9b641cd98ca92fe3_Out_0_Boolean = _X_Gradient;
            float4 _Property_1f3e0b1c283f4c949004f43f37fe1a90_Out_0_Vector4 = _X_GradientColor;
            float _Property_94b16f357eac4058809921fa96f34787_Out_0_Float = _X_GradientStartPosition;
            float _Property_8c3dacdcc9cc4b2880b33b1d1913e9f1_Out_0_Float = _X_GradientEndPosition;
            float _Split_2ca42922e29b49b4b7113632901be932_R_1_Float = IN.AbsoluteWorldSpacePosition[0];
            float _Split_2ca42922e29b49b4b7113632901be932_G_2_Float = IN.AbsoluteWorldSpacePosition[1];
            float _Split_2ca42922e29b49b4b7113632901be932_B_3_Float = IN.AbsoluteWorldSpacePosition[2];
            float _Split_2ca42922e29b49b4b7113632901be932_A_4_Float = 0;
            float _Smoothstep_c73aebae28cd43fc896795bd80911919_Out_3_Float;
            Unity_Smoothstep_float(_Property_94b16f357eac4058809921fa96f34787_Out_0_Float, _Property_8c3dacdcc9cc4b2880b33b1d1913e9f1_Out_0_Float, _Split_2ca42922e29b49b4b7113632901be932_R_1_Float, _Smoothstep_c73aebae28cd43fc896795bd80911919_Out_3_Float);
            float _Property_dd9c997bc2514f2bbe579af0da9fecb2_Out_0_Float = _X_GradientIntensity;
            float _Multiply_961d468ad0db4c8ebb883194aa2c7cf5_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_c73aebae28cd43fc896795bd80911919_Out_3_Float, _Property_dd9c997bc2514f2bbe579af0da9fecb2_Out_0_Float, _Multiply_961d468ad0db4c8ebb883194aa2c7cf5_Out_2_Float);
            float4 _Lerp_ba89b24cd32049a0964667d354c52dcc_Out_3_Vector4;
            Unity_Lerp_float4(_Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4, _Property_1f3e0b1c283f4c949004f43f37fe1a90_Out_0_Vector4, (_Multiply_961d468ad0db4c8ebb883194aa2c7cf5_Out_2_Float.xxxx), _Lerp_ba89b24cd32049a0964667d354c52dcc_Out_3_Vector4);
            float4 _Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4;
            Unity_Branch_float4(_Property_c61e8f7030cd47ac9b641cd98ca92fe3_Out_0_Boolean, _Lerp_ba89b24cd32049a0964667d354c52dcc_Out_3_Vector4, _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4, _Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4);
            float4 _Property_bbb56023a68b4c17bbaa88d52a24ab64_Out_0_Vector4 = _Y_GradientColor;
            float _Property_e53d4551d24740e89ab9d2dde9d07fa9_Out_0_Float = _Y_GradientStartPosition;
            float _Property_f705a7a1ffdc47a388bfa2aa340dd71f_Out_0_Float = _Y_GradientEndPosition;
            float _Smoothstep_8f0d8c12283b443dabc9178711fca6ea_Out_3_Float;
            Unity_Smoothstep_float(_Property_e53d4551d24740e89ab9d2dde9d07fa9_Out_0_Float, _Property_f705a7a1ffdc47a388bfa2aa340dd71f_Out_0_Float, _Split_2ca42922e29b49b4b7113632901be932_G_2_Float, _Smoothstep_8f0d8c12283b443dabc9178711fca6ea_Out_3_Float);
            float _Property_c3a47369d5bb47ddb5fcb74791e32d8d_Out_0_Float = _Y_GradientIntensity;
            float _Multiply_0b75652c94cc4e928c2a6eda4bc65145_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_8f0d8c12283b443dabc9178711fca6ea_Out_3_Float, _Property_c3a47369d5bb47ddb5fcb74791e32d8d_Out_0_Float, _Multiply_0b75652c94cc4e928c2a6eda4bc65145_Out_2_Float);
            float4 _Lerp_2023214161dd495a9df4a86e9aab0a55_Out_3_Vector4;
            Unity_Lerp_float4(_Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4, _Property_bbb56023a68b4c17bbaa88d52a24ab64_Out_0_Vector4, (_Multiply_0b75652c94cc4e928c2a6eda4bc65145_Out_2_Float.xxxx), _Lerp_2023214161dd495a9df4a86e9aab0a55_Out_3_Vector4);
            float4 _Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4;
            Unity_Branch_float4(_Property_f94879ac790e4c109a34ec1f73a3c3a6_Out_0_Boolean, _Lerp_2023214161dd495a9df4a86e9aab0a55_Out_3_Vector4, _Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4, _Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4);
            float4 _Property_d20a45cfc0cb48e4a9823f3125a60fb7_Out_0_Vector4 = _Y_GradientColor;
            float _Property_a3a6fd6a552845c797564be4a2b63e5d_Out_0_Float = _Z_GradientStartPosition;
            float _Property_1f6239cf046944d6b4f70da4fca83661_Out_0_Float = _Z_GradientEndPosition;
            float _Smoothstep_9cb470a94c1b4410b6282b2c261d462d_Out_3_Float;
            Unity_Smoothstep_float(_Property_a3a6fd6a552845c797564be4a2b63e5d_Out_0_Float, _Property_1f6239cf046944d6b4f70da4fca83661_Out_0_Float, _Split_2ca42922e29b49b4b7113632901be932_B_3_Float, _Smoothstep_9cb470a94c1b4410b6282b2c261d462d_Out_3_Float);
            float _Property_1ac1a7f19f6342f0a029129f1adbfd67_Out_0_Float = _Z_GradientIntensity;
            float _Multiply_d258a35ceccb4063a2594879737f8bb1_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_9cb470a94c1b4410b6282b2c261d462d_Out_3_Float, _Property_1ac1a7f19f6342f0a029129f1adbfd67_Out_0_Float, _Multiply_d258a35ceccb4063a2594879737f8bb1_Out_2_Float);
            float4 _Lerp_5d732be3cb674212b3307c0a98052b15_Out_3_Vector4;
            Unity_Lerp_float4(_Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4, _Property_d20a45cfc0cb48e4a9823f3125a60fb7_Out_0_Vector4, (_Multiply_d258a35ceccb4063a2594879737f8bb1_Out_2_Float.xxxx), _Lerp_5d732be3cb674212b3307c0a98052b15_Out_3_Vector4);
            float4 _Branch_808c9eecf2bc4ac09cea8d7293c4d22a_Out_3_Vector4;
            Unity_Branch_float4(_Property_18ebff8ae81646538dc78b9020d39b78_Out_0_Boolean, _Lerp_5d732be3cb674212b3307c0a98052b15_Out_3_Vector4, _Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4, _Branch_808c9eecf2bc4ac09cea8d7293c4d22a_Out_3_Vector4);
            float4 _Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4;
            Unity_Branch_float4(_Property_debbfbf1a581455dbc61b338f851a8d4_Out_0_Boolean, _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4, _Branch_808c9eecf2bc4ac09cea8d7293c4d22a_Out_3_Vector4, _Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4);
            UnityTexture2D _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D = UnityBuildTexture2DStructInternal(_Normal, sampler_Normal, _Normal_TexelSize, float4(1, 1, 0, 0), float4(0, 0, 0, 0));
            float4 _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.tex, _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.samplerstate, _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.GetTransformedUV(_TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2) );
            if (_Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.hdrDecode.x > 0)
                _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4, _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.hdrDecode);
            _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.rgb = UnpackNormal(_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4);
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_R_4_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.r;
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_G_5_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.g;
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_B_6_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.b;
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_A_7_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.a;
            float _Property_3c169350af004240a8c8543dce8c320b_Out_0_Boolean = _UseCustomRoughness;
            float _Property_5c2d908353e24a3692fb0e08fe229355_Out_0_Float = _CustomRoughness;
            float _OneMinus_59ec04ea5505409baa7bf818ec2e91cd_Out_1_Float;
            Unity_OneMinus_float(_Property_5c2d908353e24a3692fb0e08fe229355_Out_0_Float, _OneMinus_59ec04ea5505409baa7bf818ec2e91cd_Out_1_Float);
            float _Property_a19c0c9a848947f4aab57660a9a18f93_Out_0_Float = _RoughnessMultiplier;
            float _Multiply_17be8d0efec54ca3bc59e321b0c596ee_Out_2_Float;
            Unity_Multiply_float_float(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_G_5_Float, _Property_a19c0c9a848947f4aab57660a9a18f93_Out_0_Float, _Multiply_17be8d0efec54ca3bc59e321b0c596ee_Out_2_Float);
            float _Saturate_04d0090531f74a9ebdada50fd6f42af5_Out_1_Float;
            Unity_Saturate_float(_Multiply_17be8d0efec54ca3bc59e321b0c596ee_Out_2_Float, _Saturate_04d0090531f74a9ebdada50fd6f42af5_Out_1_Float);
            float _OneMinus_c9932877d03347048da7ba94008426d5_Out_1_Float;
            Unity_OneMinus_float(_Saturate_04d0090531f74a9ebdada50fd6f42af5_Out_1_Float, _OneMinus_c9932877d03347048da7ba94008426d5_Out_1_Float);
            float _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float;
            Unity_Branch_float(_Property_3c169350af004240a8c8543dce8c320b_Out_0_Boolean, _OneMinus_59ec04ea5505409baa7bf818ec2e91cd_Out_1_Float, _OneMinus_c9932877d03347048da7ba94008426d5_Out_1_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float);
            float _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float;
            Unity_Saturate_float(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_R_4_Float, _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float);
            float4 _Property_34dac06f3b7442efa3e05c85b09ec445_Out_0_Vector4 = _ACT1_Color_A;
            float2 _Property_b3a982240db24cfbbc14de979535b458_Out_0_Vector2 = _ACT1_Color_A_Location;
            float4 _Property_0e317fd277254f1aae079db6e8d2e8dc_Out_0_Vector4 = _ACT1_Color_B;
            float2 _Property_3187bf5304c54604a1d464e65a9dac03_Out_0_Vector2 = _ACT1_Color_B_Location;
            float4 _Property_41e2d595ca9d41dd8806c1a749a3bb43_Out_0_Vector4 = _ACT1_Color_C;
            Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceNormal = IN.WorldSpaceNormal;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceTangent = IN.WorldSpaceTangent;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.ObjectSpacePosition = IN.ObjectSpacePosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpacePosition = IN.WorldSpacePosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.NDCPosition = IN.NDCPosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.PixelPosition = IN.PixelPosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.uv1 = IN.uv1;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.uv2 = IN.uv2;
            float3 _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d_Lit_1_Vector3;
            SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), (_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.xyz), _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float, float3 (0, 0, 0), _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float, float(1), _Property_34dac06f3b7442efa3e05c85b09ec445_Out_0_Vector4, _Property_b3a982240db24cfbbc14de979535b458_Out_0_Vector2, _Property_0e317fd277254f1aae079db6e8d2e8dc_Out_0_Vector4, _Property_3187bf5304c54604a1d464e65a9dac03_Out_0_Vector2, _Property_41e2d595ca9d41dd8806c1a749a3bb43_Out_0_Vector4, _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d, _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d_Lit_1_Vector3);
            float4 _Property_259fe0b41799487eb44d557cc24932a4_Out_0_Vector4 = _ACT2_Color_A;
            float2 _Property_83a57d16b4dc4f48985395b92f2de589_Out_0_Vector2 = _ACT2_Color_A_Location;
            float4 _Property_8ca39a18030a4bbd9348cb5b458a8372_Out_0_Vector4 = _ACT2_Color_B;
            float2 _Property_fa2809814b4148bd8ece170a87d230ef_Out_0_Vector2 = _ACT2_Color_B_Location;
            float4 _Property_c83f37081cf64cc7999c1bb19926d7c1_Out_0_Vector4 = _ACT2_Color_C;
            Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceNormal = IN.WorldSpaceNormal;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceTangent = IN.WorldSpaceTangent;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.ObjectSpacePosition = IN.ObjectSpacePosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpacePosition = IN.WorldSpacePosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.NDCPosition = IN.NDCPosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.PixelPosition = IN.PixelPosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.uv1 = IN.uv1;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.uv2 = IN.uv2;
            float3 _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370_Lit_1_Vector3;
            SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), (_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.xyz), _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float, float3 (0, 0, 0), _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float, float(1), _Property_259fe0b41799487eb44d557cc24932a4_Out_0_Vector4, _Property_83a57d16b4dc4f48985395b92f2de589_Out_0_Vector2, _Property_8ca39a18030a4bbd9348cb5b458a8372_Out_0_Vector4, _Property_fa2809814b4148bd8ece170a87d230ef_Out_0_Vector2, _Property_c83f37081cf64cc7999c1bb19926d7c1_Out_0_Vector4, _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370, _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370_Lit_1_Vector3);
            float4 _Property_13886bd11bee4d40b3eb7be3dff1c022_Out_0_Vector4 = _ACT3_Color_A;
            float2 _Property_5578cc8231d143d9a5f34ae24f110091_Out_0_Vector2 = _ACT3_Color_A_Location;
            float4 _Property_4d39077496e94d728ca3d19d42d3bd68_Out_0_Vector4 = _ACT3_Color_B;
            float2 _Property_5b1c9e69733942519a853e51dd4770f6_Out_0_Vector2 = _ACT3_Color_B_Location;
            float4 _Property_6a5550ba5eec4966850efea3418c844a_Out_0_Vector4 = _ACT3_Color_C;
            Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceNormal = IN.WorldSpaceNormal;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceTangent = IN.WorldSpaceTangent;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.ObjectSpacePosition = IN.ObjectSpacePosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpacePosition = IN.WorldSpacePosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.NDCPosition = IN.NDCPosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.PixelPosition = IN.PixelPosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.uv1 = IN.uv1;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.uv2 = IN.uv2;
            float3 _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a_Lit_1_Vector3;
            SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), (_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.xyz), _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float, float3 (0, 0, 0), _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float, float(1), _Property_13886bd11bee4d40b3eb7be3dff1c022_Out_0_Vector4, _Property_5578cc8231d143d9a5f34ae24f110091_Out_0_Vector2, _Property_4d39077496e94d728ca3d19d42d3bd68_Out_0_Vector4, _Property_5b1c9e69733942519a853e51dd4770f6_Out_0_Vector2, _Property_6a5550ba5eec4966850efea3418c844a_Out_0_Vector4, _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a, _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a_Lit_1_Vector3);
            float3 _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3;
            if (_CURRENTACT_ACT_1) _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3 = _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d_Lit_1_Vector3;
            else if (_CURRENTACT_ACT_2) _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3 = _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370_Lit_1_Vector3;
            else _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3 = _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a_Lit_1_Vector3;
            float _Property_854b1929338847e9b4a11e77fedb361b_Out_0_Float = _LightingGradientMapInfluence;
            float3 _Blend_ed10fcb74d224507bd92f8d23fb7e2d9_Out_2_Vector3;
            Unity_Blend_Overwrite_float3((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3, _Blend_ed10fcb74d224507bd92f8d23fb7e2d9_Out_2_Vector3, _Property_854b1929338847e9b4a11e77fedb361b_Out_0_Float);
            surface.BaseColor = _Blend_ed10fcb74d224507bd92f8d23fb7e2d9_Out_2_Vector3;
            surface.Emission = float3(0, 0, 0);
            surface.Alpha = float(1);
            surface.AlphaClipThreshold = float(0.5);
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
            // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            float3 unnormalizedNormalWS = input.normalWS;
            const float renormFactor = 1.0 / length(unnormalizedNormalWS);
        
            // use bitangent on the fly like in hdrp
            // IMPORTANT! If we ever support Flip on double sided materials ensure bitangent and tangent are NOT flipped.
            float crossSign = (input.tangentWS.w > 0.0 ? 1.0 : -1.0)* GetOddNegativeScale();
            float3 bitang = crossSign * cross(input.normalWS.xyz, input.tangentWS.xyz);
        
            output.WorldSpaceNormal = renormFactor * input.normalWS.xyz;      // we want a unit length Normal Vector node in shader graph
        
            // to pr               eserve mikktspace compliance we use same scale renormFactor as was used on the normal.
            // This                is explained in section 2.2 in "surface gradient based bump mapping framework"
            output.WorldSpaceTangent = renormFactor * input.tangentWS.xyz;
            output.WorldSpaceBiTangent = renormFactor * bitang;
        
            output.WorldSpaceViewDirection = GetWorldSpaceNormalizeViewDir(input.positionWS);
            output.WorldSpacePosition = input.positionWS;
            output.ObjectSpacePosition = TransformWorldToObject(input.positionWS);
            output.AbsoluteWorldSpacePosition = GetAbsolutePositionWS(input.positionWS);
        
            #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x < 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #else
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x > 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScaledScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 = input.texCoord0;
            output.uv1 = input.texCoord1;
            output.uv2 = input.texCoord2;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/LightingMetaPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "SceneSelectionPass"
            Tags
            {
                "LightMode" = "SceneSelectionPass"
            }
        
        // Render State
        Cull Off
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        #pragma shader_feature_local_fragment _ _ALPHATEST_ON
        #pragma shader_feature _CURRENTACT_ACT_1 _CURRENTACT_ACT_2 _CURRENTACT_ACT_3
        
        
        
        // Defines
        
        #define _NORMALMAP 1
        #define _NORMAL_DROPOFF_TS 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define FEATURES_GRAPH_VERTEX_NORMAL_OUTPUT
        #define FEATURES_GRAPH_VERTEX_TANGENT_OUTPUT
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHONLY
        #define SCENESELECTIONPASS 1
        #define ALPHA_CLIP_THRESHOLD 1
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(ATTRIBUTES_NEED_INSTANCEID)
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _DisableAllGradients;
        float4 _ACT1_Color_B;
        float4 _ACT2_Color_B;
        float2 _ACT2_Color_B_Location;
        float4 _ACT2_Color_C;
        float4 _ACT2_Color_A;
        float2 _ACT2_Color_A_Location;
        float2 _ACT1_Color_B_Location;
        float4 _ACT1_Color_C;
        float4 _ACT1_Color_A;
        float2 _ACT1_Color_A_Location;
        float4 _ACT3_Color_B;
        float2 _ACT3_Color_B_Location;
        float4 _ACT3_Color_C;
        float4 _ACT3_Color_A;
        float2 _ACT3_Color_A_Location;
        float4 _ORM_TexelSize;
        float4 _Color3;
        float _Y_GradientEndPosition;
        float4 _Color2;
        float2 _Color_2_Location;
        float4 _AOColor;
        float4 _Normal_TexelSize;
        float2 _AOLevels;
        float4 _BaseColor_TexelSize;
        float4 _Color1;
        float2 _Color_1_Location;
        float _AOIntensity;
        float _RoughnessMultiplier;
        float2 _Tilling;
        float _DisableGradientMap;
        float4 _Y_GradientColor;
        float _Y_GradientIntensity;
        float _Y_GradientStartPosition;
        float _Y_Gradient;
        float _X_Gradient;
        float _X_GradientIntensity;
        float4 _X_GradientColor;
        float _X_GradientStartPosition;
        float _X_GradientEndPosition;
        float _Z_Gradient;
        float _Z_GradientIntensity;
        float4 _Z_GradientColor;
        float _Z_GradientStartPosition;
        float _Z_GradientEndPosition;
        float _UseCustomRoughness;
        float _CustomRoughness;
        float _LightingGradientMapInfluence;
        UNITY_TEXTURE_STREAMING_DEBUG_VARS;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ORM);
        SAMPLER(sampler_ORM);
        TEXTURE2D(_Normal);
        SAMPLER(sampler_Normal);
        TEXTURE2D(_BaseColor);
        SAMPLER(sampler_BaseColor);
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        // GraphFunctions: <None>
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            surface.Alpha = float(1);
            surface.AlphaClipThreshold = float(0.5);
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
        
            #if UNITY_UV_STARTS_AT_TOP
            #else
            #endif
        
        
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/SelectionPickingPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "ScenePickingPass"
            Tags
            {
                "LightMode" = "Picking"
            }
        
        // Render State
        Cull [_Cull]
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        #pragma shader_feature_local_fragment _ _ALPHATEST_ON
        #pragma shader_feature _CURRENTACT_ACT_1 _CURRENTACT_ACT_2 _CURRENTACT_ACT_3
        
        
        
        // Defines
        
        #define _NORMALMAP 1
        #define _NORMAL_DROPOFF_TS 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define ATTRIBUTES_NEED_TEXCOORD1
        #define ATTRIBUTES_NEED_TEXCOORD2
        #define FEATURES_GRAPH_VERTEX_NORMAL_OUTPUT
        #define FEATURES_GRAPH_VERTEX_TANGENT_OUTPUT
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TANGENT_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define VARYINGS_NEED_TEXCOORD1
        #define VARYINGS_NEED_TEXCOORD2
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHONLY
        #define SCENEPICKINGPASS 1
        #define ALPHA_CLIP_THRESHOLD 1
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 uv1 : TEXCOORD1;
             float4 uv2 : TEXCOORD2;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(ATTRIBUTES_NEED_INSTANCEID)
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
             float4 tangentWS;
             float4 texCoord0;
             float4 texCoord1;
             float4 texCoord2;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 WorldSpaceNormal;
             float3 WorldSpaceTangent;
             float3 WorldSpaceBiTangent;
             float3 WorldSpaceViewDirection;
             float3 ObjectSpacePosition;
             float3 WorldSpacePosition;
             float3 AbsoluteWorldSpacePosition;
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float4 uv1;
             float4 uv2;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 tangentWS : INTERP0;
             float4 texCoord0 : INTERP1;
             float4 texCoord1 : INTERP2;
             float4 texCoord2 : INTERP3;
             float3 positionWS : INTERP4;
             float3 normalWS : INTERP5;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.tangentWS.xyzw = input.tangentWS;
            output.texCoord0.xyzw = input.texCoord0;
            output.texCoord1.xyzw = input.texCoord1;
            output.texCoord2.xyzw = input.texCoord2;
            output.positionWS.xyz = input.positionWS;
            output.normalWS.xyz = input.normalWS;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.tangentWS = input.tangentWS.xyzw;
            output.texCoord0 = input.texCoord0.xyzw;
            output.texCoord1 = input.texCoord1.xyzw;
            output.texCoord2 = input.texCoord2.xyzw;
            output.positionWS = input.positionWS.xyz;
            output.normalWS = input.normalWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _DisableAllGradients;
        float4 _ACT1_Color_B;
        float4 _ACT2_Color_B;
        float2 _ACT2_Color_B_Location;
        float4 _ACT2_Color_C;
        float4 _ACT2_Color_A;
        float2 _ACT2_Color_A_Location;
        float2 _ACT1_Color_B_Location;
        float4 _ACT1_Color_C;
        float4 _ACT1_Color_A;
        float2 _ACT1_Color_A_Location;
        float4 _ACT3_Color_B;
        float2 _ACT3_Color_B_Location;
        float4 _ACT3_Color_C;
        float4 _ACT3_Color_A;
        float2 _ACT3_Color_A_Location;
        float4 _ORM_TexelSize;
        float4 _Color3;
        float _Y_GradientEndPosition;
        float4 _Color2;
        float2 _Color_2_Location;
        float4 _AOColor;
        float4 _Normal_TexelSize;
        float2 _AOLevels;
        float4 _BaseColor_TexelSize;
        float4 _Color1;
        float2 _Color_1_Location;
        float _AOIntensity;
        float _RoughnessMultiplier;
        float2 _Tilling;
        float _DisableGradientMap;
        float4 _Y_GradientColor;
        float _Y_GradientIntensity;
        float _Y_GradientStartPosition;
        float _Y_Gradient;
        float _X_Gradient;
        float _X_GradientIntensity;
        float4 _X_GradientColor;
        float _X_GradientStartPosition;
        float _X_GradientEndPosition;
        float _Z_Gradient;
        float _Z_GradientIntensity;
        float4 _Z_GradientColor;
        float _Z_GradientStartPosition;
        float _Z_GradientEndPosition;
        float _UseCustomRoughness;
        float _CustomRoughness;
        float _LightingGradientMapInfluence;
        UNITY_TEXTURE_STREAMING_DEBUG_VARS;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ORM);
        SAMPLER(sampler_ORM);
        TEXTURE2D(_Normal);
        SAMPLER(sampler_Normal);
        TEXTURE2D(_BaseColor);
        SAMPLER(sampler_BaseColor);
        
        // Graph Includes
        #include_with_pragmas "Assets/Samples/Shader Graph/17.3.0/Custom Lighting/Components/Debug/DebugLightingComplexity.hlsl"
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
        {
            Out = lerp(A, B, T);
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Branch_float4(float Predicate, float4 True, float4 False, out float4 Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        void Unity_Branch_float(float Predicate, float True, float False, out float Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Divide_float(float A, float B, out float Out)
        {
            Out = A / B;
        }
        
        // unity-custom-func-begin
        void ApplyDecals_float(float4 positionCS, float3 baseColor, float3 specularColor, float3 normalWS, float metallic, float smoothness, float occlusion, out float3 baseColorOut, out float3 specularColorOut, out float3 normalWSOut, out float metallicOut, out float smoothnessOut, out float occlusionOut){
        #if !defined(SHADERGRAPH_PREVIEW) && defined(_DBUFFER)
        	ApplyDecal(positionCS, baseColor, specularColor, normalWS, metallic, occlusion, smoothness);
        	baseColorOut = baseColor;
        	specularColorOut = specularColor;
        	normalWSOut = normalWS;
        	metallicOut = metallic;
        	occlusionOut = occlusion;
        	smoothnessOut = smoothness;
        #else
        	baseColorOut = baseColor;
        	specularColorOut = specularColor;
        	normalWSOut = normalWS;
        	metallicOut = metallic;
        	occlusionOut = occlusion;
        	smoothnessOut = smoothness;
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float
        {
        float2 PixelPosition;
        };
        
        void SG_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float(float3 _Base_Color, float3 _NormalWS, float _Metallic, float _Smoothness, float _AmbientOcclusion, Bindings_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float IN, out float3 BaseColor_1, out float3 SpecularColor_2, out float3 NormalWS_3, out float Metallic_4, out float Smoothness_6, out float AmbientOcclusion_5)
        {
        float4 _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4 = float4(IN.PixelPosition.xy, 0, 0);
        float _Split_ad27d29658ef44f7b6941c97694d6866_R_1_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[0];
        float _Split_ad27d29658ef44f7b6941c97694d6866_G_2_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[1];
        float _Split_ad27d29658ef44f7b6941c97694d6866_B_3_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[2];
        float _Split_ad27d29658ef44f7b6941c97694d6866_A_4_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[3];
        float _Divide_3f9fb3b7b5b94d0d8246bbc34aa63f7b_Out_2_Float;
        Unity_Divide_float(_Split_ad27d29658ef44f7b6941c97694d6866_G_2_Float, _ScreenParams.y, _Divide_3f9fb3b7b5b94d0d8246bbc34aa63f7b_Out_2_Float);
        float _OneMinus_3cfec48ba27f4585a5feb790836dc9dc_Out_1_Float;
        Unity_OneMinus_float(_Divide_3f9fb3b7b5b94d0d8246bbc34aa63f7b_Out_2_Float, _OneMinus_3cfec48ba27f4585a5feb790836dc9dc_Out_1_Float);
        float _Multiply_a4baed73797c41c1b9631ae9bbddfe12_Out_2_Float;
        Unity_Multiply_float_float(_OneMinus_3cfec48ba27f4585a5feb790836dc9dc_Out_1_Float, _ScreenParams.y, _Multiply_a4baed73797c41c1b9631ae9bbddfe12_Out_2_Float);
        float2 _Vector2_eed86f79e1de4c188df97eb091955bc5_Out_0_Vector2 = float2(_Split_ad27d29658ef44f7b6941c97694d6866_R_1_Float, _Multiply_a4baed73797c41c1b9631ae9bbddfe12_Out_2_Float);
        float3 _Property_6219e38e66a84dddb55188eb0359a8c3_Out_0_Vector3 = _Base_Color;
        float3 _Property_f4c37d8281c1497e8dab743349080d88_Out_0_Vector3 = _NormalWS;
        float _Property_0826181079c84604befc19a2460f4daa_Out_0_Float = _Metallic;
        float _Property_d54a743184cc4f27b93d5f5b239c7b7e_Out_0_Float = _Smoothness;
        float _Property_bd6cbdae9db240b9b4ad935655106f79_Out_0_Float = _AmbientOcclusion;
        float3 _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_baseColorOut_8_Vector3;
        float3 _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_specularColorOut_9_Vector3;
        float3 _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_normalWSOut_10_Vector3;
        float _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_metallicOut_11_Float;
        float _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_smoothnessOut_13_Float;
        float _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_occlusionOut_12_Float;
        ApplyDecals_float((float4(_Vector2_eed86f79e1de4c188df97eb091955bc5_Out_0_Vector2, 0.0, 1.0)), _Property_6219e38e66a84dddb55188eb0359a8c3_Out_0_Vector3, float3 (0, 0, 0), _Property_f4c37d8281c1497e8dab743349080d88_Out_0_Vector3, _Property_0826181079c84604befc19a2460f4daa_Out_0_Float, _Property_d54a743184cc4f27b93d5f5b239c7b7e_Out_0_Float, _Property_bd6cbdae9db240b9b4ad935655106f79_Out_0_Float, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_baseColorOut_8_Vector3, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_specularColorOut_9_Vector3, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_normalWSOut_10_Vector3, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_metallicOut_11_Float, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_smoothnessOut_13_Float, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_occlusionOut_12_Float);
        BaseColor_1 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_baseColorOut_8_Vector3;
        SpecularColor_2 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_specularColorOut_9_Vector3;
        NormalWS_3 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_normalWSOut_10_Vector3;
        Metallic_4 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_metallicOut_11_Float;
        Smoothness_6 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_smoothnessOut_13_Float;
        AmbientOcclusion_5 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_occlusionOut_12_Float;
        }
        
        // unity-custom-func-begin
        void SwitchLightingDebug_float(float3 BaseColorIn, float3 NormalIn, float MetallicIn, float SmoothnessIn, float3 EmissionIn, float AmbientOcclusionIn, float3 positionWS, float3 bakedGI, out float3 BaseColorOut, out float3 NormalOut, out float MetallicOut, out float SmoothnessOut, out float3 EmissionOut, out float AmbientOcclusionOut){
        #if !defined(SHADERGRAPH_PREVIEW) && defined(DEBUG_DISPLAY)
        
        [branch] switch(int(_DebugLightingMode))
        
        {
        
            case 0: //none
        
        		BaseColorOut = BaseColorIn;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
            case 1: //SHADOW_CASCADES
        
        		half cascadeIndex = ComputeCascadeIndex(positionWS);
        
        		switch (uint(cascadeIndex))
        
        		{
        
        			case 0: BaseColorOut = kDebugColorShadowCascade0.rgb;break;
        
        			case 1: BaseColorOut = kDebugColorShadowCascade1.rgb;break;
        
        			case 2: BaseColorOut = kDebugColorShadowCascade2.rgb;break;
        
        			case 3: BaseColorOut = kDebugColorShadowCascade3.rgb;break;
        
        			default: BaseColorOut = kDebugColorBlack.rgb;break;
        
        		}
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
            case 2: //LIGHTING_WITHOUT_NORMAL_MAPS
        
        		BaseColorOut = float3(1,1,1);
        
        		MetallicOut = 0;
        
        		SmoothnessOut = 0;
        
        		NormalOut = float3(0,0,1);
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            case 3: //LIGHTING_WITH_NORMAL_MAPS
        
        		BaseColorOut = float3(1,1,1);
        
        		MetallicOut = 0;
        
        		SmoothnessOut = 0;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            case 4: //REFLECTIONS
        
        		BaseColorOut = float3(0.1,0.1,0.1);
        
        		MetallicOut = 1;
        
        		SmoothnessOut = 1;
        
        		NormalOut = float3(0,0,1);
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            case 5: //REFLECTIONS_WITH_SMOOTHNESS
        
        		BaseColorOut = float3(0.1,0.1,0.1);
        
        		MetallicOut = 1;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
            case 6: //GLOBAL_ILLUM
        
        		BaseColorOut = bakedGI;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = 0;
        
        		NormalOut = float3(0,0,1);
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            default:
        
        		BaseColorOut = BaseColorIn;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
        }
        
        #else
        
        		BaseColorOut = BaseColorIn;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_DebugLighting_61e571d2b9ede1240a524a849d20c997_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpacePosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_DebugLighting_61e571d2b9ede1240a524a849d20c997_float(float3 _Base_Color, float3 _NormalWS, float _Metallic, float _Smoothness, float3 _Emission, float _AmbientOcclusion, Bindings_DebugLighting_61e571d2b9ede1240a524a849d20c997_float IN, out float3 BaseColor_1, out float3 Normal_4, out float Metallic_2, out float Smoothness_3, out float3 Emission_5, out float AmbientOcclusion_6)
        {
        float3 _Property_501515703e3a4a1dbd19f4ae273add46_Out_0_Vector3 = _Base_Color;
        float3 _Property_e5bcb5bf3b62412b8983e3aa1ada8fcd_Out_0_Vector3 = _NormalWS;
        float3 _Transform_13c6b1e888d440fd8340d4a7138979ba_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_13c6b1e888d440fd8340d4a7138979ba_Out_1_Vector3 = TransformWorldToTangent(_Property_e5bcb5bf3b62412b8983e3aa1ada8fcd_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float _Property_7a450453146043b2b11397a72c325042_Out_0_Float = _Metallic;
        float _Property_f0326121e031478a90610d60b8321364_Out_0_Float = _Smoothness;
        float3 _Property_491d95b34bb245718ee21bff5fc249cd_Out_0_Vector3 = _Emission;
        float _Property_da91a6effd53499db08bb774d5686c68_Out_0_Float = _AmbientOcclusion;
        float3 _BakedGI_3f01c30cb8b64e9d9f7fbe474622c7dc_Out_1_Vector3 = SHADERGRAPH_BAKED_GI(IN.WorldSpacePosition, _Property_e5bcb5bf3b62412b8983e3aa1ada8fcd_Out_0_Vector3, IN.PixelPosition.xy, IN.uv1.xy, IN.uv2.xy, true);
        float3 _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_BaseColorOut_7_Vector3;
        float3 _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_NormalOut_11_Vector3;
        float _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_MetallicOut_9_Float;
        float _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_SmoothnessOut_10_Float;
        float3 _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_EmissionOut_12_Vector3;
        float _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_AmbientOcclusionOut_13_Float;
        SwitchLightingDebug_float(_Property_501515703e3a4a1dbd19f4ae273add46_Out_0_Vector3, _Transform_13c6b1e888d440fd8340d4a7138979ba_Out_1_Vector3, _Property_7a450453146043b2b11397a72c325042_Out_0_Float, _Property_f0326121e031478a90610d60b8321364_Out_0_Float, _Property_491d95b34bb245718ee21bff5fc249cd_Out_0_Vector3, _Property_da91a6effd53499db08bb774d5686c68_Out_0_Float, IN.WorldSpacePosition, _BakedGI_3f01c30cb8b64e9d9f7fbe474622c7dc_Out_1_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_BaseColorOut_7_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_NormalOut_11_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_MetallicOut_9_Float, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_SmoothnessOut_10_Float, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_EmissionOut_12_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_AmbientOcclusionOut_13_Float);
        BaseColor_1 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_BaseColorOut_7_Vector3;
        Normal_4 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_NormalOut_11_Vector3;
        Metallic_2 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_MetallicOut_9_Float;
        Smoothness_3 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_SmoothnessOut_10_Float;
        Emission_5 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_EmissionOut_12_Vector3;
        AmbientOcclusion_6 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_AmbientOcclusionOut_13_Float;
        }
        
        // unity-custom-func-begin
        void GetMainLightData_float(float3 worldPos, out float3 direction, out float3 color, out float shadowAtten){
        #ifdef SHADERGRAPH_PREVIEW
            direction = normalize(float3(-0.7,0.7,-0.7));
            color = float3(1,1,1);
            shadowAtten = 1;
        #else
            #if defined(UNIVERSAL_PIPELINE_CORE_INCLUDED)
                float4 shadowCoord = TransformWorldToShadowCoord(worldPos);
                Light mainLight = GetMainLight(shadowCoord);
                direction = mainLight.direction;
                color = mainLight.color;
                shadowAtten = mainLight.shadowAttenuation;
            #else
                direction = normalize(float3(-0.7, 0.7, -0.7));
                color = float3(1, 1, 1);
                shadowAtten = 0;
            #endif
        #endif
        }
        // unity-custom-func-end
        
        // unity-custom-func-begin
        void GetMainLightData_half(half3 worldPos, out half3 direction, out half3 color, out half shadowAtten){
        #ifdef SHADERGRAPH_PREVIEW
            direction = normalize(float3(-0.7,0.7,-0.7));
            color = float3(1,1,1);
            shadowAtten = 1;
        #else
            #if defined(UNIVERSAL_PIPELINE_CORE_INCLUDED)
                float4 shadowCoord = TransformWorldToShadowCoord(worldPos);
                Light mainLight = GetMainLight(shadowCoord);
                direction = mainLight.direction;
                color = mainLight.color;
                shadowAtten = mainLight.shadowAttenuation;
            #else
                direction = normalize(float3(-0.7, 0.7, -0.7));
                color = float3(1, 1, 1);
                shadowAtten = 0;
            #endif
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float
        {
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float IN, out float3 Direction_1, out float3 Color_2, out float ShadowAtten_3)
        {
        float3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        float3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        float _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        GetMainLightData_float(IN.AbsoluteWorldSpacePosition, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float);
        Direction_1 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        Color_2 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        ShadowAtten_3 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        }
        
        struct Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half
        {
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half(Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half IN, out half3 Direction_1, out half3 Color_2, out half ShadowAtten_3)
        {
        half3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        half3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        half _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        GetMainLightData_half(IN.AbsoluteWorldSpacePosition, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float);
        Direction_1 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        Color_2 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        ShadowAtten_3 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        }
        
        void Unity_DotProduct_float3(float3 A, float3 B, out float Out)
        {
            Out = dot(A, B);
        }
        
        void Unity_DotProduct_half3(half3 A, half3 B, out half Out)
        {
            Out = dot(A, B);
        }
        
        void Unity_Saturate_half(half In, out half Out)
        {
            Out = saturate(In);
        }
        
        struct Bindings_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half
        {
        float3 WorldSpaceNormal;
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half(half3 _NormalWS, bool _NormalWS_68a7999ae9ea4bfba3702fd95b0d1a14_IsConnected, half3 _LightVector, bool _LightVector_a12354c78b694cc6b2bdddd67d09ccdc_IsConnected, Bindings_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half IN, out half Diffuse_1)
        {
        half3 _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3 = _NormalWS;
        bool _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3_IsConnected = _NormalWS_68a7999ae9ea4bfba3702fd95b0d1a14_IsConnected;
        half3 _BranchOnInputConnection_71cde5ac4ee04aacb1e2544c8017ba47_Out_3_Vector3 = _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3_IsConnected ? _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3 : IN.WorldSpaceNormal;
        half3 _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3 = _LightVector;
        bool _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3_IsConnected = _LightVector_a12354c78b694cc6b2bdddd67d09ccdc_IsConnected;
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half _MainLight_fa0151c045984bcab58e58725bae0709;
        _MainLight_fa0151c045984bcab58e58725bae0709.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_fa0151c045984bcab58e58725bae0709_Direction_1_Vector3;
        half3 _MainLight_fa0151c045984bcab58e58725bae0709_Color_2_Vector3;
        half _MainLight_fa0151c045984bcab58e58725bae0709_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half(_MainLight_fa0151c045984bcab58e58725bae0709, _MainLight_fa0151c045984bcab58e58725bae0709_Direction_1_Vector3, _MainLight_fa0151c045984bcab58e58725bae0709_Color_2_Vector3, _MainLight_fa0151c045984bcab58e58725bae0709_ShadowAtten_3_Float);
        half3 _BranchOnInputConnection_d18845e766084954af1aa554531c90b9_Out_3_Vector3 = _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3_IsConnected ? _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3 : _MainLight_fa0151c045984bcab58e58725bae0709_Direction_1_Vector3;
        half _DotProduct_daa979e4f9384944a14a23e079be6a5c_Out_2_Float;
        Unity_DotProduct_half3(_BranchOnInputConnection_71cde5ac4ee04aacb1e2544c8017ba47_Out_3_Vector3, _BranchOnInputConnection_d18845e766084954af1aa554531c90b9_Out_3_Vector3, _DotProduct_daa979e4f9384944a14a23e079be6a5c_Out_2_Float);
        half _Saturate_4747439b9bfa431293a93646ce71aa12_Out_1_Float;
        Unity_Saturate_half(_DotProduct_daa979e4f9384944a14a23e079be6a5c_Out_2_Float, _Saturate_4747439b9bfa431293a93646ce71aa12_Out_1_Float);
        Diffuse_1 = _Saturate_4747439b9bfa431293a93646ce71aa12_Out_1_Float;
        }
        
        void Unity_Lerp_float(float A, float B, float T, out float Out)
        {
            Out = lerp(A, B, T);
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Reciprocal_float(float In, out float Out)
        {
            Out = 1.0/In;
        }
        
        void Unity_FresnelEffect_float(float3 Normal, float3 ViewDir, float Power, out float Out)
        {
            Out = pow((1.0 - saturate(dot(normalize(Normal), ViewDir))), Power);
        }
        
        void Unity_Lerp_float3(float3 A, float3 B, float3 T, out float3 Out)
        {
            Out = lerp(A, B, T);
        }
        
        struct Bindings_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        };
        
        void SG_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float(float3 _Base_Color, float3 _NormalWS, bool _NormalWS_3240674a787044a092398b1ca753ad83_IsConnected, float _Metallic, float _Smoothness, float _F0, Bindings_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float IN, out float3 Reflectance_1)
        {
        float _Property_4678b902494b4e1a9b08a8067b7bed85_Out_0_Float = _Smoothness;
        float _OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float;
        Unity_OneMinus_float(_Property_4678b902494b4e1a9b08a8067b7bed85_Out_0_Float, _OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float);
        float _Multiply_314f6a0aec0a44538333e69617e91cf9_Out_2_Float;
        Unity_Multiply_float_float(_OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float, _OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float, _Multiply_314f6a0aec0a44538333e69617e91cf9_Out_2_Float);
        float _Add_513724b99c2f4ea2a803e64d80f0c25b_Out_2_Float;
        Unity_Add_float(_Multiply_314f6a0aec0a44538333e69617e91cf9_Out_2_Float, float(1), _Add_513724b99c2f4ea2a803e64d80f0c25b_Out_2_Float);
        float _Reciprocal_67b338305d0043abb9e7d82dd0f16146_Out_1_Float;
        Unity_Reciprocal_float(_Add_513724b99c2f4ea2a803e64d80f0c25b_Out_2_Float, _Reciprocal_67b338305d0043abb9e7d82dd0f16146_Out_1_Float);
        float _Property_b67a6773dce34e91ae69bbf282d871cc_Out_0_Float = _F0;
        float _Property_703d9ec0a0894a3b965f0ed25a10435b_Out_0_Float = _F0;
        float _Add_8ad10875906b4a80872f2b2fb0518183_Out_2_Float;
        Unity_Add_float(_Property_4678b902494b4e1a9b08a8067b7bed85_Out_0_Float, _Property_703d9ec0a0894a3b965f0ed25a10435b_Out_0_Float, _Add_8ad10875906b4a80872f2b2fb0518183_Out_2_Float);
        float3 _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3 = _NormalWS;
        bool _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3_IsConnected = _NormalWS_3240674a787044a092398b1ca753ad83_IsConnected;
        float3 _BranchOnInputConnection_43b8bde55a8a41468ba21d53db128986_Out_3_Vector3 = _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3_IsConnected ? _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3 : IN.WorldSpaceNormal;
        float _FresnelEffect_34b729c62edd4d5f99dc20e2e7d0a7fa_Out_3_Float;
        Unity_FresnelEffect_float(_BranchOnInputConnection_43b8bde55a8a41468ba21d53db128986_Out_3_Vector3, IN.WorldSpaceViewDirection, float(4), _FresnelEffect_34b729c62edd4d5f99dc20e2e7d0a7fa_Out_3_Float);
        float _Lerp_1cad776e609842c181b8acfaef47c317_Out_3_Float;
        Unity_Lerp_float(_Property_b67a6773dce34e91ae69bbf282d871cc_Out_0_Float, _Add_8ad10875906b4a80872f2b2fb0518183_Out_2_Float, _FresnelEffect_34b729c62edd4d5f99dc20e2e7d0a7fa_Out_3_Float, _Lerp_1cad776e609842c181b8acfaef47c317_Out_3_Float);
        float _Multiply_0d364d1c231246d981281b868ea74a95_Out_2_Float;
        Unity_Multiply_float_float(_Reciprocal_67b338305d0043abb9e7d82dd0f16146_Out_1_Float, _Lerp_1cad776e609842c181b8acfaef47c317_Out_3_Float, _Multiply_0d364d1c231246d981281b868ea74a95_Out_2_Float);
        float3 _Property_87ae51a595c24e46ad9ef0f4493231fc_Out_0_Vector3 = _Base_Color;
        float _Property_ce0a90815c5046b48dd0564711f2b466_Out_0_Float = _Metallic;
        float3 _Lerp_6b4c86a1d5004851a7dba1bacc4fd953_Out_3_Vector3;
        Unity_Lerp_float3((_Multiply_0d364d1c231246d981281b868ea74a95_Out_2_Float.xxx), _Property_87ae51a595c24e46ad9ef0f4493231fc_Out_0_Vector3, (_Property_ce0a90815c5046b48dd0564711f2b466_Out_0_Float.xxx), _Lerp_6b4c86a1d5004851a7dba1bacc4fd953_Out_3_Vector3);
        Reflectance_1 = _Lerp_6b4c86a1d5004851a7dba1bacc4fd953_Out_3_Vector3;
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        void Unity_Normalize_float3(float3 In, out float3 Out)
        {
            Out = normalize(In);
        }
        
        struct Bindings_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float
        {
        };
        
        void SG_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float(float3 _viewDir, float3 _lightDir, Bindings_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float IN, out float3 Out_1)
        {
        float3 _Property_fde52ad74bda46adabbcc34b42b16131_Out_0_Vector3 = _viewDir;
        float3 _Property_1dc55a6640574aaf8c04290eb0d5e816_Out_0_Vector3 = _lightDir;
        float3 _Add_2a8cf5c52e8c4e3fb8c3f9a87b68b2a2_Out_2_Vector3;
        Unity_Add_float3(_Property_fde52ad74bda46adabbcc34b42b16131_Out_0_Vector3, _Property_1dc55a6640574aaf8c04290eb0d5e816_Out_0_Vector3, _Add_2a8cf5c52e8c4e3fb8c3f9a87b68b2a2_Out_2_Vector3);
        float3 _Normalize_b3b8196f46224ae3a998bba24956de7f_Out_1_Vector3;
        Unity_Normalize_float3(_Add_2a8cf5c52e8c4e3fb8c3f9a87b68b2a2_Out_2_Vector3, _Normalize_b3b8196f46224ae3a998bba24956de7f_Out_1_Vector3);
        Out_1 = _Normalize_b3b8196f46224ae3a998bba24956de7f_Out_1_Vector3;
        }
        
        void Unity_Exponential2_float(float In, out float Out)
        {
            Out = exp2(In);
        }
        
        struct Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float
        {
        };
        
        void SG_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float(float _Smoothness, Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float IN, out float SpecPower_1)
        {
        float _Property_80f639c6927445458cce37e8c24909a1_Out_0_Float = _Smoothness;
        float _Multiply_c7e9a2c01b804964a64dd7499f62a400_Out_2_Float;
        Unity_Multiply_float_float(_Property_80f639c6927445458cce37e8c24909a1_Out_0_Float, 10, _Multiply_c7e9a2c01b804964a64dd7499f62a400_Out_2_Float);
        float _Add_663b867a23b04deaa88c2ebd79bcdbc7_Out_2_Float;
        Unity_Add_float(_Multiply_c7e9a2c01b804964a64dd7499f62a400_Out_2_Float, float(1), _Add_663b867a23b04deaa88c2ebd79bcdbc7_Out_2_Float);
        float _Exponential_bd8d33b85b2e46508bef0c6cc36f9dae_Out_1_Float;
        Unity_Exponential2_float(_Add_663b867a23b04deaa88c2ebd79bcdbc7_Out_2_Float, _Exponential_bd8d33b85b2e46508bef0c6cc36f9dae_Out_1_Float);
        SpecPower_1 = _Exponential_bd8d33b85b2e46508bef0c6cc36f9dae_Out_1_Float;
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
        Out = A * B;
        }
        
        struct Bindings_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float(float3 _NormalWS, bool _NormalWS_5a3c9a3a7faa491894a42d170b5bfeb5_IsConnected, float _Smoothness, float3 _Reflectance, float3 _LightVector, bool _LightVector_3db37b6247094f32bcccc4cb689d525f_IsConnected, Bindings_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float IN, out float3 Specular_1)
        {
        float3 _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3 = _LightVector;
        bool _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3_IsConnected = _LightVector_3db37b6247094f32bcccc4cb689d525f_IsConnected;
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float _MainLight_6570bf88718b46ebb6bd80eec408287a;
        _MainLight_6570bf88718b46ebb6bd80eec408287a.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_6570bf88718b46ebb6bd80eec408287a_Direction_1_Vector3;
        half3 _MainLight_6570bf88718b46ebb6bd80eec408287a_Color_2_Vector3;
        half _MainLight_6570bf88718b46ebb6bd80eec408287a_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(_MainLight_6570bf88718b46ebb6bd80eec408287a, _MainLight_6570bf88718b46ebb6bd80eec408287a_Direction_1_Vector3, _MainLight_6570bf88718b46ebb6bd80eec408287a_Color_2_Vector3, _MainLight_6570bf88718b46ebb6bd80eec408287a_ShadowAtten_3_Float);
        float3 _BranchOnInputConnection_6a7b13b3cb82474aa187229c3d17a00f_Out_3_Vector3 = _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3_IsConnected ? _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3 : _MainLight_6570bf88718b46ebb6bd80eec408287a_Direction_1_Vector3;
        Bindings_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float _HalfAngle_f48886360d2649d8b7540e6fb3eef669;
        half3 _HalfAngle_f48886360d2649d8b7540e6fb3eef669_Out_1_Vector3;
        SG_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float(IN.WorldSpaceViewDirection, _BranchOnInputConnection_6a7b13b3cb82474aa187229c3d17a00f_Out_3_Vector3, _HalfAngle_f48886360d2649d8b7540e6fb3eef669, _HalfAngle_f48886360d2649d8b7540e6fb3eef669_Out_1_Vector3);
        float3 _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3 = _NormalWS;
        bool _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3_IsConnected = _NormalWS_5a3c9a3a7faa491894a42d170b5bfeb5_IsConnected;
        float3 _BranchOnInputConnection_72430741d0e04d2dbf5368b624a090cc_Out_3_Vector3 = _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3_IsConnected ? _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3 : IN.WorldSpaceNormal;
        float _DotProduct_4558c6edcb084d359ac8ee4b2934ea05_Out_2_Float;
        Unity_DotProduct_float3(_HalfAngle_f48886360d2649d8b7540e6fb3eef669_Out_1_Vector3, _BranchOnInputConnection_72430741d0e04d2dbf5368b624a090cc_Out_3_Vector3, _DotProduct_4558c6edcb084d359ac8ee4b2934ea05_Out_2_Float);
        float _Saturate_23d3962f416c4150a990dcb36b5ecbc4_Out_1_Float;
        Unity_Saturate_float(_DotProduct_4558c6edcb084d359ac8ee4b2934ea05_Out_2_Float, _Saturate_23d3962f416c4150a990dcb36b5ecbc4_Out_1_Float);
        float _Property_f4ccf6ae090a4694bb78a2cef88028e0_Out_0_Float = _Smoothness;
        Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9;
        half _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9_SpecPower_1_Float;
        SG_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float(_Property_f4ccf6ae090a4694bb78a2cef88028e0_Out_0_Float, _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9, _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9_SpecPower_1_Float);
        float _Power_b652c8bd47c44415bf7733f381883bf3_Out_2_Float;
        Unity_Power_float(_Saturate_23d3962f416c4150a990dcb36b5ecbc4_Out_1_Float, _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9_SpecPower_1_Float, _Power_b652c8bd47c44415bf7733f381883bf3_Out_2_Float);
        float _Property_1fcbde0798cd43628cbb75583e5d6e7a_Out_0_Float = _Smoothness;
        float _Multiply_8e72dd78784942f5ac8322f3055fd0ed_Out_2_Float;
        Unity_Multiply_float_float(_Power_b652c8bd47c44415bf7733f381883bf3_Out_2_Float, _Property_1fcbde0798cd43628cbb75583e5d6e7a_Out_0_Float, _Multiply_8e72dd78784942f5ac8322f3055fd0ed_Out_2_Float);
        float3 _Property_5b0bef6a4de54859800dd057235a4dbc_Out_0_Vector3 = _Reflectance;
        float3 _Multiply_73cf80666f44441494dd412a147e089c_Out_2_Vector3;
        Unity_Multiply_float3_float3((_Multiply_8e72dd78784942f5ac8322f3055fd0ed_Out_2_Float.xxx), _Property_5b0bef6a4de54859800dd057235a4dbc_Out_0_Vector3, _Multiply_73cf80666f44441494dd412a147e089c_Out_2_Vector3);
        float3 _Multiply_bceb91846ebf445c9093523786845bb5_Out_2_Vector3;
        Unity_Multiply_float3_float3(_Multiply_73cf80666f44441494dd412a147e089c_Out_2_Vector3, float3(10, 10, 10), _Multiply_bceb91846ebf445c9093523786845bb5_Out_2_Vector3);
        Specular_1 = _Multiply_bceb91846ebf445c9093523786845bb5_Out_2_Vector3;
        }
        
        // unity-custom-func-begin
        void AddAdditionalLightsSimple_float(float SpecPower, float3 WorldPosition, float3 WorldNormal, float3 WorldView, float MainDiffuse, float3 MainSpecular, float3 MainColor, float3 Reflectance, float2 ScreenPosition, out float Diffuse, out float3 Specular, out float3 Color){
        Diffuse = MainDiffuse;
        
        Specular = MainSpecular;
        
        Color = MainColor * (MainDiffuse + MainSpecular);
        
        
        
        #ifndef SHADERGRAPH_PREVIEW
        
            
        
        #if defined(_ADDITIONAL_LIGHTS) || defined(_CLUSTER_LIGHT_LOOP)
        
        
        
            #if defined(_ADDITIONAL_LIGHTS)
        
                uint pixelLightCount = GetAdditionalLightsCount();
        
            #endif
        
        
        
        #if USE_CLUSTER_LIGHT_LOOP
        
            // for Foward+ LIGHT_LOOP_BEGIN macro uses inputData.normalizedScreenSpaceUV and inputData.positionWS
        
            InputData inputData = (InputData)0;
        
        
        
            inputData.normalizedScreenSpaceUV = ScreenPosition;
        
            inputData.positionWS = WorldPosition;
        
        #endif
        
        
        
            LIGHT_LOOP_BEGIN(pixelLightCount)
        
        		// Call the URP additional light algorithm. This will not calculate shadows, since we don't pass a shadow mask value
        
        		Light light = GetAdditionalLight(lightIndex, WorldPosition);
        
        		// Manually set the shadow attenuation by calculating realtime shadows
        
        		light.shadowAttenuation = AdditionalLightRealtimeShadow(lightIndex, WorldPosition, light.direction);
        
                float NdotL = saturate(dot(WorldNormal, light.direction));
        
                float atten = light.distanceAttenuation * light.shadowAttenuation;
        
                float thisDiffuse = atten * NdotL;
        
                float3 halfAngle = normalize(light.direction + WorldView);
        
                float spec = pow(saturate(dot(halfAngle, WorldNormal)), SpecPower);
        
                float3 thisSpecular = spec * Reflectance * atten;
        
                Diffuse += thisDiffuse;
        
                Specular += thisSpecular;
        
                #if defined(_LIGHT_COOKIES)
        
                    float3 cookieColor = SampleAdditionalLightCookie(lightIndex, WorldPosition);
        
                    light.color *= cookieColor;
        
                #endif
        
                Color += light.color * (thisDiffuse + thisSpecular);
        
            LIGHT_LOOP_END
        
            float total = Diffuse + dot(Specular, float3(0.333, 0.333, 0.333));
        
            Color = total <= 0 ? MainColor : Color / total;
        
        #endif // _ADDITIONAL_LIGHTS || _CLUSTER_LIGHT_LOOP
        
        
        
        #endif // SHADERGRAPH_PREVIEW
        }
        // unity-custom-func-end
        
        struct Bindings_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        };
        
        void SG_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float(float _MainLightDiffuse, float3 _MainLightSpecular, float3 _MainLightColor, float3 _NormalWS, bool _NormalWS_70cbf5ac6da04bf6bd87eb71ccb7c48d_IsConnected, float _Smoothness, float3 _Reflectance, Bindings_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float IN, out float Diffuse_1, out float3 Specular_2, out float3 Color_3)
        {
        float _Property_b9f05025da4f4857a7b1b6f56259a629_Out_0_Float = _Smoothness;
        Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1;
        half _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1_SpecPower_1_Float;
        SG_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float(_Property_b9f05025da4f4857a7b1b6f56259a629_Out_0_Float, _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1, _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1_SpecPower_1_Float);
        float3 _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3 = _NormalWS;
        bool _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3_IsConnected = _NormalWS_70cbf5ac6da04bf6bd87eb71ccb7c48d_IsConnected;
        float3 _BranchOnInputConnection_d869e3d8654b48a491de945ad8af6301_Out_3_Vector3 = _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3_IsConnected ? _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3 : IN.WorldSpaceNormal;
        float _Property_25880f0697234954b8dc6ef11af3752d_Out_0_Float = _MainLightDiffuse;
        float3 _Property_1e29ad89226c4d84a936fe7530839aef_Out_0_Vector3 = _MainLightSpecular;
        float3 _Property_ac790fc8215b4b3d8851855d2153960d_Out_0_Vector3 = _MainLightColor;
        float3 _Property_eea8eda455d44ae7b30c65f80baac806_Out_0_Vector3 = _Reflectance;
        float4 _ScreenPosition_bb4bf3ece5524d4c898132bd377d7d8b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Diffuse_7_Float;
        float3 _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Specular_8_Vector3;
        float3 _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Color_9_Vector3;
        AddAdditionalLightsSimple_float(_SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1_SpecPower_1_Float, IN.WorldSpacePosition, _BranchOnInputConnection_d869e3d8654b48a491de945ad8af6301_Out_3_Vector3, IN.WorldSpaceViewDirection, _Property_25880f0697234954b8dc6ef11af3752d_Out_0_Float, _Property_1e29ad89226c4d84a936fe7530839aef_Out_0_Vector3, _Property_ac790fc8215b4b3d8851855d2153960d_Out_0_Vector3, _Property_eea8eda455d44ae7b30c65f80baac806_Out_0_Vector3, (_ScreenPosition_bb4bf3ece5524d4c898132bd377d7d8b_Out_0_Vector4.xy), _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Diffuse_7_Float, _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Specular_8_Vector3, _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Color_9_Vector3);
        Diffuse_1 = _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Diffuse_7_Float;
        Specular_2 = _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Specular_8_Vector3;
        Color_3 = _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Color_9_Vector3;
        }
        
        void Unity_Negate_float3(float3 In, out float3 Out)
        {
            Out = -1 * In;
        }
        
        void Unity_Reflection_float3(float3 In, float3 Normal, out float3 Out)
        {
            Out = reflect(In, Normal);
        }
        
        // unity-custom-func-begin
        void URPReflectionProbe_float(float3 positionWS, float3 reflectVector, float2 normalizedScreenSpaceUV, float roughness, float occlusion, out float3 reflection){
        #ifdef SHADERGRAPH_PREVIEW
        
            reflection = float3(0,0,0);
        
        #else
        
            reflection = GlossyEnvironmentReflection(reflectVector, positionWS, roughness, occlusion, normalizedScreenSpaceUV);
        
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        };
        
        void SG_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float(float3 _positionWS, bool _positionWS_d6701bdc1f184a57ac2283491fc460d9_IsConnected, float3 _reflectVector, bool _reflectVector_3e2eb19b69b8469eaf2302c7abc4cbc5_IsConnected, float _smoothness, float _occlusion, Bindings_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float IN, out float3 Out_1)
        {
        float3 _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3 = _positionWS;
        bool _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3_IsConnected = _positionWS_d6701bdc1f184a57ac2283491fc460d9_IsConnected;
        float3 _BranchOnInputConnection_8fb583036b0c4313a1ecd93143939f21_Out_3_Vector3 = _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3_IsConnected ? _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3 : IN.WorldSpacePosition;
        float3 _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3 = _reflectVector;
        bool _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3_IsConnected = _reflectVector_3e2eb19b69b8469eaf2302c7abc4cbc5_IsConnected;
        float3 _Negate_9cf7cea21c5641239fdbcb32480ac39e_Out_1_Vector3;
        Unity_Negate_float3(IN.WorldSpaceViewDirection, _Negate_9cf7cea21c5641239fdbcb32480ac39e_Out_1_Vector3);
        float3 _Reflection_c8689c494aab4411aadc74299549c6cb_Out_2_Vector3;
        Unity_Reflection_float3(_Negate_9cf7cea21c5641239fdbcb32480ac39e_Out_1_Vector3, IN.WorldSpaceNormal, _Reflection_c8689c494aab4411aadc74299549c6cb_Out_2_Vector3);
        float3 _BranchOnInputConnection_9600230d09794702a61c1a01f8e842a5_Out_3_Vector3 = _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3_IsConnected ? _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3 : _Reflection_c8689c494aab4411aadc74299549c6cb_Out_2_Vector3;
        float4 _ScreenPosition_270e438746a9466e8aaf01f4903f62fb_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float _Property_9012e47da801473d8ef85a4092281eb2_Out_0_Float = _smoothness;
        float _OneMinus_b2508f25afb44017ba3480edc35cf631_Out_1_Float;
        Unity_OneMinus_float(_Property_9012e47da801473d8ef85a4092281eb2_Out_0_Float, _OneMinus_b2508f25afb44017ba3480edc35cf631_Out_1_Float);
        float _Property_d602b1723845462cbf00324de1e9e82a_Out_0_Float = _occlusion;
        float3 _URPReflectionProbeCustomFunction_7233d098cd214f55baf898d486bfdf4b_reflection_4_Vector3;
        URPReflectionProbe_float(_BranchOnInputConnection_8fb583036b0c4313a1ecd93143939f21_Out_3_Vector3, _BranchOnInputConnection_9600230d09794702a61c1a01f8e842a5_Out_3_Vector3, (_ScreenPosition_270e438746a9466e8aaf01f4903f62fb_Out_0_Vector4.xy), _OneMinus_b2508f25afb44017ba3480edc35cf631_Out_1_Float, _Property_d602b1723845462cbf00324de1e9e82a_Out_0_Float, _URPReflectionProbeCustomFunction_7233d098cd214f55baf898d486bfdf4b_reflection_4_Vector3);
        Out_1 = _URPReflectionProbeCustomFunction_7233d098cd214f55baf898d486bfdf4b_reflection_4_Vector3;
        }
        
        // unity-custom-func-begin
        void SSAO_float(float2 normalizedScreenSpaceUV, out float indirectAmbientOcclusion, out float directAmbientOcclusion){
        #if defined(_SCREEN_SPACE_OCCLUSION) && !defined(_SURFACE_TYPE_TRANSPARENT) && !defined(SHADERGRAPH_PREVIEW)
        
            float ssao = saturate(SampleAmbientOcclusion(normalizedScreenSpaceUV) + (1.0 - _AmbientOcclusionParam.x));
        
            indirectAmbientOcclusion = ssao;
        
            directAmbientOcclusion = lerp(half(1.0), ssao, _AmbientOcclusionParam.w);
        
        #else
        
            directAmbientOcclusion = half(1.0);
        
            indirectAmbientOcclusion = half(1.0);
        
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float
        {
        float2 NDCPosition;
        };
        
        void SG_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float(Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float IN, out float indirectAO_1, out float directAO_2)
        {
        float4 _ScreenPosition_0fdc511287e14fd48ca909caba575383_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_indirectAmbientOcclusion_0_Float;
        float _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_directAmbientOcclusion_1_Float;
        SSAO_float((_ScreenPosition_0fdc511287e14fd48ca909caba575383_Out_0_Vector4.xy), _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_indirectAmbientOcclusion_0_Float, _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_directAmbientOcclusion_1_Float);
        indirectAO_1 = _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_indirectAmbientOcclusion_0_Float;
        directAO_2 = _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_directAmbientOcclusion_1_Float;
        }
        
        void Unity_Minimum_float(float A, float B, out float Out)
        {
            Out = min(A, B);
        };
        
        struct Bindings_AmbientURP_300875fdd653fe340b08ad1547984cf1_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_AmbientURP_300875fdd653fe340b08ad1547984cf1_float(float3 _Base_Color, float3 _NormalWS, bool _NormalWS_3a565a44841d4b729f8e86b08d09299c_IsConnected, float _Metallic, float _Smoothness, float3 _Reflectance, float _Ambient_Occlusion, Bindings_AmbientURP_300875fdd653fe340b08ad1547984cf1_float IN, out float3 Ambient_1, out float DirectAO_2)
        {
        float3 _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3 = _NormalWS;
        bool _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3_IsConnected = _NormalWS_3a565a44841d4b729f8e86b08d09299c_IsConnected;
        float3 _BranchOnInputConnection_5e35c0fc2eee4cf7ae47da45409fa2a7_Out_3_Vector3 = _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3_IsConnected ? _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3 : IN.WorldSpaceNormal;
        float3 _BakedGI_1ac35076ff2349f99fec2cef2550ff2d_Out_1_Vector3 = SHADERGRAPH_BAKED_GI(IN.WorldSpacePosition, _BranchOnInputConnection_5e35c0fc2eee4cf7ae47da45409fa2a7_Out_3_Vector3, IN.PixelPosition.xy, IN.uv1.xy, IN.uv2.xy, true);
        float3 _Property_5fb17e215f49424cb9cc9d0806f3f47d_Out_0_Vector3 = _Base_Color;
        float _Property_f995d8544fdb448d85ac845c7bdee967_Out_0_Float = _Metallic;
        float3 _Lerp_842ba4fcd0cf48a3afa108eecd7d56a8_Out_3_Vector3;
        Unity_Lerp_float3(_Property_5fb17e215f49424cb9cc9d0806f3f47d_Out_0_Vector3, float3(0, 0, 0), (_Property_f995d8544fdb448d85ac845c7bdee967_Out_0_Float.xxx), _Lerp_842ba4fcd0cf48a3afa108eecd7d56a8_Out_3_Vector3);
        float3 _Multiply_48ca9faac6354eefabc65eeb591f3fc4_Out_2_Vector3;
        Unity_Multiply_float3_float3(_BakedGI_1ac35076ff2349f99fec2cef2550ff2d_Out_1_Vector3, _Lerp_842ba4fcd0cf48a3afa108eecd7d56a8_Out_3_Vector3, _Multiply_48ca9faac6354eefabc65eeb591f3fc4_Out_2_Vector3);
        float3 _Negate_4f98a1dfbc4d4bd1abe58f406453303f_Out_1_Vector3;
        Unity_Negate_float3(IN.WorldSpaceViewDirection, _Negate_4f98a1dfbc4d4bd1abe58f406453303f_Out_1_Vector3);
        float3 _Reflection_710a69cb22b745929e6bb9b84d11de2f_Out_2_Vector3;
        Unity_Reflection_float3(_Negate_4f98a1dfbc4d4bd1abe58f406453303f_Out_1_Vector3, _BranchOnInputConnection_5e35c0fc2eee4cf7ae47da45409fa2a7_Out_3_Vector3, _Reflection_710a69cb22b745929e6bb9b84d11de2f_Out_2_Vector3);
        float _Property_8c3e921b9cb34f7b82d2a71254653c09_Out_0_Float = _Smoothness;
        Bindings_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.WorldSpaceNormal = IN.WorldSpaceNormal;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.WorldSpacePosition = IN.WorldSpacePosition;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.NDCPosition = IN.NDCPosition;
        float3 _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08_Out_1_Vector3;
        SG_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float(half3 (0, 0, 0), false, _Reflection_710a69cb22b745929e6bb9b84d11de2f_Out_2_Vector3, true, _Property_8c3e921b9cb34f7b82d2a71254653c09_Out_0_Float, half(1), _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08, _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08_Out_1_Vector3);
        float3 _Property_2ddaa58bd1e94d0b8508ce91ad39fa39_Out_0_Vector3 = _Reflectance;
        float3 _Multiply_40be6c08ca2f44ef99b6a2b81955d62c_Out_2_Vector3;
        Unity_Multiply_float3_float3(_SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08_Out_1_Vector3, _Property_2ddaa58bd1e94d0b8508ce91ad39fa39_Out_0_Vector3, _Multiply_40be6c08ca2f44ef99b6a2b81955d62c_Out_2_Vector3);
        float3 _Add_c0a57a54eb8b4419a7e907a65e6556a7_Out_2_Vector3;
        Unity_Add_float3(_Multiply_48ca9faac6354eefabc65eeb591f3fc4_Out_2_Vector3, _Multiply_40be6c08ca2f44ef99b6a2b81955d62c_Out_2_Vector3, _Add_c0a57a54eb8b4419a7e907a65e6556a7_Out_2_Vector3);
        float _Property_5a996c5d941d46019b3ac5ada526c475_Out_0_Float = _Ambient_Occlusion;
        Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e;
        _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e.NDCPosition = IN.NDCPosition;
        half _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_indirectAO_1_Float;
        half _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_directAO_2_Float;
        SG_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float(_ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_indirectAO_1_Float, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_directAO_2_Float);
        float _Minimum_699e1643e9974baa97f6f21c8f949d09_Out_2_Float;
        Unity_Minimum_float(_Property_5a996c5d941d46019b3ac5ada526c475_Out_0_Float, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_indirectAO_1_Float, _Minimum_699e1643e9974baa97f6f21c8f949d09_Out_2_Float);
        float3 _Multiply_ccaf4dfa5cdb4d9fae7fd92dc7d512da_Out_2_Vector3;
        Unity_Multiply_float3_float3(_Add_c0a57a54eb8b4419a7e907a65e6556a7_Out_2_Vector3, (_Minimum_699e1643e9974baa97f6f21c8f949d09_Out_2_Float.xxx), _Multiply_ccaf4dfa5cdb4d9fae7fd92dc7d512da_Out_2_Vector3);
        float _Minimum_c4dcab6b18b34dde987e68f86d22aed7_Out_2_Float;
        Unity_Minimum_float(_Property_5a996c5d941d46019b3ac5ada526c475_Out_0_Float, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_directAO_2_Float, _Minimum_c4dcab6b18b34dde987e68f86d22aed7_Out_2_Float);
        Ambient_1 = _Multiply_ccaf4dfa5cdb4d9fae7fd92dc7d512da_Out_2_Vector3;
        DirectAO_2 = _Minimum_c4dcab6b18b34dde987e68f86d22aed7_Out_2_Float;
        }
        
        void Unity_Fog_float(out float4 Color, out float Density, float3 Position)
        {
            SHADERGRAPH_FOG(Position, Color, Density);
        }
        
        struct Bindings_Fog_286ae83400099a24bba6faf005588be7_float
        {
        float3 ObjectSpacePosition;
        };
        
        void SG_Fog_286ae83400099a24bba6faf005588be7_float(float3 _In, Bindings_Fog_286ae83400099a24bba6faf005588be7_float IN, out float3 Out_1)
        {
        float3 _Property_626923dc627443639da97776de7dcc22_Out_0_Vector3 = _In;
        float4 _Fog_acabe3c84c9549f3880ce7d106150576_Color_0_Vector4;
        float _Fog_acabe3c84c9549f3880ce7d106150576_Density_1_Float;
        Unity_Fog_float(_Fog_acabe3c84c9549f3880ce7d106150576_Color_0_Vector4, _Fog_acabe3c84c9549f3880ce7d106150576_Density_1_Float, IN.ObjectSpacePosition);
        float3 _Lerp_9cfca9aa08c7423bab62b39a01237d64_Out_3_Vector3;
        Unity_Lerp_float3(_Property_626923dc627443639da97776de7dcc22_Out_0_Vector3, (_Fog_acabe3c84c9549f3880ce7d106150576_Color_0_Vector4.xyz), (_Fog_acabe3c84c9549f3880ce7d106150576_Density_1_Float.xxx), _Lerp_9cfca9aa08c7423bab62b39a01237d64_Out_3_Vector3);
        Out_1 = _Lerp_9cfca9aa08c7423bab62b39a01237d64_Out_3_Vector3;
        }
        
        void Unity_Saturate_float3(float3 In, out float3 Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Saturation_float(float3 In, float Saturation, out float3 Out)
        {
            float luma = dot(In, float3(0.2126729, 0.7151522, 0.0721750));
            Out =  luma.xxx + Saturation.xxx * (In - luma.xxx);
        }
        
        void Unity_Contrast_float(float3 In, float Contrast, out float3 Out)
        {
            float midpoint = pow(0.5, 2.2);
            Out =  (In - midpoint) * Contrast + midpoint;
        }
        
        void Unity_Remap_float3(float3 In, float2 InMinMax, float2 OutMinMax, out float3 Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        struct Bindings_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpaceViewDirection;
        float3 ObjectSpacePosition;
        float3 WorldSpacePosition;
        float3 AbsoluteWorldSpacePosition;
        float2 NDCPosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float(float3 _Base_Color, float3 _Normal, bool _Normal_e1611e545480449d80aa5a0e7c2b63c4_IsConnected, float _Metallic, float _Smoothness, float _Micro_Occlusion, float _Ambient_Occlusion, float4 _Color_A, float2 _Color_A_Location, float4 _Color_B, float2 _Color_B_Location, float4 _Color_C, Bindings_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float IN, out float3 Lit_1)
        {
        float4 _Property_bedee92e97ce4b2abff5524ce019b2a8_Out_0_Vector4 = _Color_A;
        float4 _Property_c648ff794fd34283beff09e33d8293fc_Out_0_Vector4 = _Color_B;
        float3 _Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3 = _Normal;
        bool _Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3_IsConnected = _Normal_e1611e545480449d80aa5a0e7c2b63c4_IsConnected;
        float3 _Transform_75d5405941a444c088600c650a7269f0_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_75d5405941a444c088600c650a7269f0_Out_1_Vector3 = TransformTangentToWorld(_Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float3 _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3 = _Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3_IsConnected ? _Transform_75d5405941a444c088600c650a7269f0_Out_1_Vector3 : IN.WorldSpaceNormal;
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float _MainLight_d918d0814080438585a810ba0b8afeb4;
        _MainLight_d918d0814080438585a810ba0b8afeb4.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3;
        half3 _MainLight_d918d0814080438585a810ba0b8afeb4_Color_2_Vector3;
        half _MainLight_d918d0814080438585a810ba0b8afeb4_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(_MainLight_d918d0814080438585a810ba0b8afeb4, _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3, _MainLight_d918d0814080438585a810ba0b8afeb4_Color_2_Vector3, _MainLight_d918d0814080438585a810ba0b8afeb4_ShadowAtten_3_Float);
        Bindings_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half _DiffuseLambert_7f9e988376a2438ebc87097469e065d3;
        _DiffuseLambert_7f9e988376a2438ebc87097469e065d3.WorldSpaceNormal = IN.WorldSpaceNormal;
        _DiffuseLambert_7f9e988376a2438ebc87097469e065d3.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half _DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float;
        SG_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half(_BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3, true, _DiffuseLambert_7f9e988376a2438ebc87097469e065d3, _DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float);
        float _Property_ac10139ecedb4301b4595fa5b13c00b8_Out_0_Float = _Smoothness;
        float3 _Property_58fa7de1b7784467935169b8914ee373_Out_0_Vector3 = _Base_Color;
        float _Property_433019e2d18944a2909e58d06f7cc1ec_Out_0_Float = _Metallic;
        float _Property_dd950e92d2d54fefbb89aaa0d1f6b713_Out_0_Float = _Smoothness;
        float _Property_dce33fc56d0e4204bd34d323af11f8ca_Out_0_Float = _Micro_Occlusion;
        float _Multiply_d120aee0dde541e69ff32688ddb2bee0_Out_2_Float;
        Unity_Multiply_float_float(_Property_dce33fc56d0e4204bd34d323af11f8ca_Out_0_Float, 0.5, _Multiply_d120aee0dde541e69ff32688ddb2bee0_Out_2_Float);
        float _Lerp_6742a96ab4984154ae535aef42b348f7_Out_3_Float;
        Unity_Lerp_float(float(0), float(0.08), _Multiply_d120aee0dde541e69ff32688ddb2bee0_Out_2_Float, _Lerp_6742a96ab4984154ae535aef42b348f7_Out_3_Float);
        Bindings_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d;
        _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d.WorldSpaceNormal = IN.WorldSpaceNormal;
        _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        half3 _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3;
        SG_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float(_Property_58fa7de1b7784467935169b8914ee373_Out_0_Vector3, _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_433019e2d18944a2909e58d06f7cc1ec_Out_0_Float, _Property_dd950e92d2d54fefbb89aaa0d1f6b713_Out_0_Float, _Lerp_6742a96ab4984154ae535aef42b348f7_Out_3_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3);
        Bindings_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3;
        _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3.WorldSpaceNormal = IN.WorldSpaceNormal;
        _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3_Specular_1_Vector3;
        SG_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float(_BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_ac10139ecedb4301b4595fa5b13c00b8_Out_0_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3, _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3, true, _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3, _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3_Specular_1_Vector3);
        float3 _Multiply_7b7e19c594534a458849928b4dc06aec_Out_2_Vector3;
        Unity_Multiply_float3_float3((_DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float.xxx), _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3_Specular_1_Vector3, _Multiply_7b7e19c594534a458849928b4dc06aec_Out_2_Vector3);
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e;
        _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Direction_1_Vector3;
        half3 _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Color_2_Vector3;
        half _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(_MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e, _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Direction_1_Vector3, _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Color_2_Vector3, _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_ShadowAtten_3_Float);
        float3 _Multiply_9545e2014858428792b92c57dd77e45c_Out_2_Vector3;
        Unity_Multiply_float3_float3(_MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Color_2_Vector3, (_MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_ShadowAtten_3_Float.xxx), _Multiply_9545e2014858428792b92c57dd77e45c_Out_2_Vector3);
        float _Property_be391ee1d2f24bada2da9fc9d603f6a9_Out_0_Float = _Smoothness;
        Bindings_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.WorldSpaceNormal = IN.WorldSpaceNormal;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.WorldSpacePosition = IN.WorldSpacePosition;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.NDCPosition = IN.NDCPosition;
        half _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Diffuse_1_Float;
        half3 _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Specular_2_Vector3;
        half3 _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Color_3_Vector3;
        SG_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float(_DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float, _Multiply_7b7e19c594534a458849928b4dc06aec_Out_2_Vector3, _Multiply_9545e2014858428792b92c57dd77e45c_Out_2_Vector3, _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_be391ee1d2f24bada2da9fc9d603f6a9_Out_0_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Diffuse_1_Float, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Specular_2_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Color_3_Vector3);
        float3 _Property_8804fec07c534721b9d4e6def9182fad_Out_0_Vector3 = _Base_Color;
        float3 _Multiply_765841b19beb4cecb786b3295b6aa8e9_Out_2_Vector3;
        Unity_Multiply_float3_float3((_AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Diffuse_1_Float.xxx), _Property_8804fec07c534721b9d4e6def9182fad_Out_0_Vector3, _Multiply_765841b19beb4cecb786b3295b6aa8e9_Out_2_Vector3);
        float3 _Add_77980781d2204e1b8d249e5f7058e9fb_Out_2_Vector3;
        Unity_Add_float3(_Multiply_765841b19beb4cecb786b3295b6aa8e9_Out_2_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Specular_2_Vector3, _Add_77980781d2204e1b8d249e5f7058e9fb_Out_2_Vector3);
        float3 _Multiply_9df1a4a5bf654e97b945b2eccf2fc855_Out_2_Vector3;
        Unity_Multiply_float3_float3(_Add_77980781d2204e1b8d249e5f7058e9fb_Out_2_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Color_3_Vector3, _Multiply_9df1a4a5bf654e97b945b2eccf2fc855_Out_2_Vector3);
        float3 _Property_e9725e93976b4fcaa5fea397628348dd_Out_0_Vector3 = _Base_Color;
        float _Property_e926bef11147490b98b69d5bec06eaa9_Out_0_Float = _Metallic;
        float _Property_1465a416fe734e4e83f2401b9c4d3fdb_Out_0_Float = _Smoothness;
        float _Property_0a056a259612407e813453c548affc50_Out_0_Float = _Ambient_Occlusion;
        Bindings_AmbientURP_300875fdd653fe340b08ad1547984cf1_float _AmbientURP_46e1712500da4aae848bd5b24a05f29f;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.WorldSpaceNormal = IN.WorldSpaceNormal;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.WorldSpacePosition = IN.WorldSpacePosition;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.NDCPosition = IN.NDCPosition;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.PixelPosition = IN.PixelPosition;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.uv1 = IN.uv1;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.uv2 = IN.uv2;
        half3 _AmbientURP_46e1712500da4aae848bd5b24a05f29f_Ambient_1_Vector3;
        half _AmbientURP_46e1712500da4aae848bd5b24a05f29f_DirectAO_2_Float;
        SG_AmbientURP_300875fdd653fe340b08ad1547984cf1_float(_Property_e9725e93976b4fcaa5fea397628348dd_Out_0_Vector3, _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_e926bef11147490b98b69d5bec06eaa9_Out_0_Float, _Property_1465a416fe734e4e83f2401b9c4d3fdb_Out_0_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3, _Property_0a056a259612407e813453c548affc50_Out_0_Float, _AmbientURP_46e1712500da4aae848bd5b24a05f29f, _AmbientURP_46e1712500da4aae848bd5b24a05f29f_Ambient_1_Vector3, _AmbientURP_46e1712500da4aae848bd5b24a05f29f_DirectAO_2_Float);
        float3 _Add_ad0b3eccabf24dceb374546c3c332ad7_Out_2_Vector3;
        Unity_Add_float3(_Multiply_9df1a4a5bf654e97b945b2eccf2fc855_Out_2_Vector3, _AmbientURP_46e1712500da4aae848bd5b24a05f29f_Ambient_1_Vector3, _Add_ad0b3eccabf24dceb374546c3c332ad7_Out_2_Vector3);
        Bindings_Fog_286ae83400099a24bba6faf005588be7_float _Fog_f4025f6ca9e74f948bc7263ef71d324a;
        _Fog_f4025f6ca9e74f948bc7263ef71d324a.ObjectSpacePosition = IN.ObjectSpacePosition;
        half3 _Fog_f4025f6ca9e74f948bc7263ef71d324a_Out_1_Vector3;
        SG_Fog_286ae83400099a24bba6faf005588be7_float(_Add_ad0b3eccabf24dceb374546c3c332ad7_Out_2_Vector3, _Fog_f4025f6ca9e74f948bc7263ef71d324a, _Fog_f4025f6ca9e74f948bc7263ef71d324a_Out_1_Vector3);
        float3 _Saturate_b798de6279164a2b9155cd8251d26092_Out_1_Vector3;
        Unity_Saturate_float3(_Fog_f4025f6ca9e74f948bc7263ef71d324a_Out_1_Vector3, _Saturate_b798de6279164a2b9155cd8251d26092_Out_1_Vector3);
        float3 _Saturation_ad4a69e303864b54940190442ea5d857_Out_2_Vector3;
        Unity_Saturation_float(_Saturate_b798de6279164a2b9155cd8251d26092_Out_1_Vector3, float(0), _Saturation_ad4a69e303864b54940190442ea5d857_Out_2_Vector3);
        float _Swizzle_bae47f72b28a4941b7665012b9c55203_Out_1_Float = (_Saturation_ad4a69e303864b54940190442ea5d857_Out_2_Vector3).x.x;
        float _Power_83030291203f43f9b048356e4fbaf99e_Out_2_Float;
        Unity_Power_float(_Swizzle_bae47f72b28a4941b7665012b9c55203_Out_1_Float, float(0.45), _Power_83030291203f43f9b048356e4fbaf99e_Out_2_Float);
        float3 _Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3;
        Unity_Contrast_float((_Power_83030291203f43f9b048356e4fbaf99e_Out_2_Float.xxx), float(2), _Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3);
        float2 _Property_7fc6298ec06c474b99191c5a5156da72_Out_0_Vector2 = _Color_A_Location;
        float3 _Remap_6d4565d12b3a4f71b23e18189b7389b3_Out_3_Vector3;
        Unity_Remap_float3(_Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3, _Property_7fc6298ec06c474b99191c5a5156da72_Out_0_Vector2, float2 (0, 1), _Remap_6d4565d12b3a4f71b23e18189b7389b3_Out_3_Vector3);
        float3 _Saturate_2728e5e4642645239b2f9a703cc1622a_Out_1_Vector3;
        Unity_Saturate_float3(_Remap_6d4565d12b3a4f71b23e18189b7389b3_Out_3_Vector3, _Saturate_2728e5e4642645239b2f9a703cc1622a_Out_1_Vector3);
        float3 _Lerp_20eaa2d06da4491384cac8b19ba49f2c_Out_3_Vector3;
        Unity_Lerp_float3((_Property_bedee92e97ce4b2abff5524ce019b2a8_Out_0_Vector4.xyz), (_Property_c648ff794fd34283beff09e33d8293fc_Out_0_Vector4.xyz), _Saturate_2728e5e4642645239b2f9a703cc1622a_Out_1_Vector3, _Lerp_20eaa2d06da4491384cac8b19ba49f2c_Out_3_Vector3);
        float4 _Property_fd1357d1b32e4a29a4cbcc3613f244e3_Out_0_Vector4 = _Color_C;
        float2 _Property_c2df701a69af4187a31c6f8cfcb26846_Out_0_Vector2 = _Color_B_Location;
        float3 _Remap_0c80541bb2da437698e33d379210a687_Out_3_Vector3;
        Unity_Remap_float3(_Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3, _Property_c2df701a69af4187a31c6f8cfcb26846_Out_0_Vector2, float2 (0, 1), _Remap_0c80541bb2da437698e33d379210a687_Out_3_Vector3);
        float3 _Saturate_5b42e55c7d0c45f1a92ce0c1c045003a_Out_1_Vector3;
        Unity_Saturate_float3(_Remap_0c80541bb2da437698e33d379210a687_Out_3_Vector3, _Saturate_5b42e55c7d0c45f1a92ce0c1c045003a_Out_1_Vector3);
        float3 _Lerp_b1cbf1c8cc0549a69912223d40ba4cfa_Out_3_Vector3;
        Unity_Lerp_float3(_Lerp_20eaa2d06da4491384cac8b19ba49f2c_Out_3_Vector3, (_Property_fd1357d1b32e4a29a4cbcc3613f244e3_Out_0_Vector4.xyz), _Saturate_5b42e55c7d0c45f1a92ce0c1c045003a_Out_1_Vector3, _Lerp_b1cbf1c8cc0549a69912223d40ba4cfa_Out_3_Vector3);
        Lit_1 = _Lerp_b1cbf1c8cc0549a69912223d40ba4cfa_Out_3_Vector3;
        }
        
        // unity-custom-func-begin
        void DebugMaterialSwitch_float(float3 None, float3 Albedo, float3 Specular, float3 Alpha, float3 Smoothness, float3 AmbientOcclusion, float3 Emission, float3 NormalWS, float3 NormalTS, float3 LightComplexity, float3 Metallic, float3 SpriteMask, float3 RenderingLayerMasks, out float3 Out){
        Out = None;
        #if !defined(SHADERGRAPH_PREVIEW) && defined(DEBUG_DISPLAY)
        [branch] switch(int(_DebugMaterialMode))
        
        {
        
            case 0:
        
                Out = None; break;
        
            case 1:
        
                Out = Albedo; break;
        
            case 2:
        
                Out = Specular; break;
            case 3:
        
                Out = Alpha; break;
            case 4:
        
                Out = Smoothness; break;
            case 5:
        
                Out = AmbientOcclusion;  break;
            case 6:
        
                Out = Emission;  break;
            case 7:
        
                Out = NormalWS * 0.5 + 0.5;  break;
            case 8:
        
                Out = NormalTS * 0.5 + 0.5;  break;
            case 9:
        
                Out = LightComplexity;  break;
            case 10:
        
                Out = Metallic;  break;
            case 11:
        
                Out = SpriteMask;  break;
            case 12:
        
                Out = RenderingLayerMasks;  break;
        
            default:
        
                Out = None; break;
        
        }
        #endif
        
        // Disable this define to prevent the global unlit
        // fragment pass to override the color output again.
        #undef DEBUG_DISPLAY
        }
        // unity-custom-func-end
        
        struct Bindings_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        };
        
        void SG_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float(float3 _In, float3 _Base_Color, float3 _Normal, float _Metallic, float _Smoothness, float3 _Emission, float _Ambient_Occlusion, float _Alpha, Bindings_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float IN, out float3 Out_1)
        {
        float3 _Property_dd011cc96ae64d1181317986b1fa1742_Out_0_Vector3 = _In;
        float3 _Property_5653941ce5a641f18f7ce7012652025d_Out_0_Vector3 = _Base_Color;
        float _Property_45f5c13ff5544581bd61c2442cecd0a1_Out_0_Float = _Alpha;
        float _Property_b6c8b448c5324bd3bc59540f628e43a3_Out_0_Float = _Smoothness;
        Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5;
        _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5.NDCPosition = IN.NDCPosition;
        half _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_indirectAO_1_Float;
        half _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_directAO_2_Float;
        SG_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float(_ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5, _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_indirectAO_1_Float, _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_directAO_2_Float);
        float _Property_441143660ff642349088dd1bcab6bc78_Out_0_Float = _Ambient_Occlusion;
        float _Minimum_8ee95b9bf7ac4776b6ee4edf1214b3c1_Out_2_Float;
        Unity_Minimum_float(_ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_indirectAO_1_Float, _Property_441143660ff642349088dd1bcab6bc78_Out_0_Float, _Minimum_8ee95b9bf7ac4776b6ee4edf1214b3c1_Out_2_Float);
        float3 _Property_b171431b5a3b4b0a9fc9fdede4a532a7_Out_0_Vector3 = _Emission;
        float3 _Property_db9eb36ed51d4aad95e383920b55e3d7_Out_0_Vector3 = _Normal;
        float3 _Transform_9e831bda1f4d4495b24f1f6e3075f2fb_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_9e831bda1f4d4495b24f1f6e3075f2fb_Out_1_Vector3 = TransformTangentToWorld(_Property_db9eb36ed51d4aad95e383920b55e3d7_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float3 _Property_4eaab22b2b784aeda3752622f7abaf85_Out_0_Vector3 = _Normal;
        float4 _ScreenPosition_121436dfdd464829910775b2326b046b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float3 _Property_1b1e0a48277e4883afeb1289a075c5d8_Out_0_Vector3 = _Base_Color;
        float3 _LightingComplexityCustomFunction_cbe0c0f96f9046a584e17ead8c001a55_Out_3_Vector3;
        LightingComplexity_float((_ScreenPosition_121436dfdd464829910775b2326b046b_Out_0_Vector4.xy), IN.WorldSpacePosition, _Property_1b1e0a48277e4883afeb1289a075c5d8_Out_0_Vector3, _LightingComplexityCustomFunction_cbe0c0f96f9046a584e17ead8c001a55_Out_3_Vector3);
        float _Property_dcd3ca7796af45c6857884fa7979898b_Out_0_Float = _Metallic;
        float3 _DebugMaterialSwitchCustomFunction_e1fabc2a3bcd4bc183f0e93c379657d4_Out_5_Vector3;
        DebugMaterialSwitch_float(_Property_dd011cc96ae64d1181317986b1fa1742_Out_0_Vector3, _Property_5653941ce5a641f18f7ce7012652025d_Out_0_Vector3, float3 (0, 0, 0), (_Property_45f5c13ff5544581bd61c2442cecd0a1_Out_0_Float.xxx), (_Property_b6c8b448c5324bd3bc59540f628e43a3_Out_0_Float.xxx), (_Minimum_8ee95b9bf7ac4776b6ee4edf1214b3c1_Out_2_Float.xxx), _Property_b171431b5a3b4b0a9fc9fdede4a532a7_Out_0_Vector3, _Transform_9e831bda1f4d4495b24f1f6e3075f2fb_Out_1_Vector3, _Property_4eaab22b2b784aeda3752622f7abaf85_Out_0_Vector3, _LightingComplexityCustomFunction_cbe0c0f96f9046a584e17ead8c001a55_Out_3_Vector3, (_Property_dcd3ca7796af45c6857884fa7979898b_Out_0_Float.xxx), float3 (0, 0, 0), float3 (0, 0, 0), _DebugMaterialSwitchCustomFunction_e1fabc2a3bcd4bc183f0e93c379657d4_Out_5_Vector3);
        Out_1 = _DebugMaterialSwitchCustomFunction_e1fabc2a3bcd4bc183f0e93c379657d4_Out_5_Vector3;
        }
        
        struct Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpaceViewDirection;
        float3 ObjectSpacePosition;
        float3 WorldSpacePosition;
        float3 AbsoluteWorldSpacePosition;
        float2 NDCPosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float(float3 _Base_Color, float3 _Normal, float _Metallic, float _Smoothness, float3 _Emission, float _AmbientOcclusion, float _Alpha, float4 _Color_A, float2 _Color_A_Location, float4 _Color_B, float2 _Color_B_Location, float4 _Color_C, Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float IN, out float3 Lit_1)
        {
        float3 _Property_04a055764411443d802bfbbd0d510c65_Out_0_Vector3 = _Base_Color;
        float3 _Property_383a017d83a8420dac016260bc833f58_Out_0_Vector3 = _Normal;
        float3 _Transform_3f94cf9dbe844abc9b6727d5d289074f_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_3f94cf9dbe844abc9b6727d5d289074f_Out_1_Vector3 = TransformTangentToWorld(_Property_383a017d83a8420dac016260bc833f58_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float _Property_11295d868ff34c388c9212b90b781aff_Out_0_Float = _Metallic;
        float _Property_b522b61b85ff4ecbb0eb63cff689f5cb_Out_0_Float = _Smoothness;
        float _Property_a1dc37a47c5640d0870861199df0bd70_Out_0_Float = _AmbientOcclusion;
        Bindings_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float _ApplyDecals_0413903f5da5491d911d117142eabddd;
        _ApplyDecals_0413903f5da5491d911d117142eabddd.PixelPosition = IN.PixelPosition;
        half3 _ApplyDecals_0413903f5da5491d911d117142eabddd_BaseColor_1_Vector3;
        half3 _ApplyDecals_0413903f5da5491d911d117142eabddd_SpecularColor_2_Vector3;
        half3 _ApplyDecals_0413903f5da5491d911d117142eabddd_NormalWS_3_Vector3;
        half _ApplyDecals_0413903f5da5491d911d117142eabddd_Metallic_4_Float;
        half _ApplyDecals_0413903f5da5491d911d117142eabddd_Smoothness_6_Float;
        half _ApplyDecals_0413903f5da5491d911d117142eabddd_AmbientOcclusion_5_Float;
        SG_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float(_Property_04a055764411443d802bfbbd0d510c65_Out_0_Vector3, _Transform_3f94cf9dbe844abc9b6727d5d289074f_Out_1_Vector3, _Property_11295d868ff34c388c9212b90b781aff_Out_0_Float, _Property_b522b61b85ff4ecbb0eb63cff689f5cb_Out_0_Float, _Property_a1dc37a47c5640d0870861199df0bd70_Out_0_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd, _ApplyDecals_0413903f5da5491d911d117142eabddd_BaseColor_1_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_SpecularColor_2_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_NormalWS_3_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_Metallic_4_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd_Smoothness_6_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd_AmbientOcclusion_5_Float);
        float3 _Property_b986326ad9b34d6ea3a7237ba2bd1cd6_Out_0_Vector3 = _Emission;
        Bindings_DebugLighting_61e571d2b9ede1240a524a849d20c997_float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpaceNormal = IN.WorldSpaceNormal;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpaceTangent = IN.WorldSpaceTangent;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpacePosition = IN.WorldSpacePosition;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.PixelPosition = IN.PixelPosition;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.uv1 = IN.uv1;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.uv2 = IN.uv2;
        float3 _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3;
        float3 _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3;
        float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float;
        float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float;
        float3 _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3;
        float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float;
        SG_DebugLighting_61e571d2b9ede1240a524a849d20c997_float(_ApplyDecals_0413903f5da5491d911d117142eabddd_BaseColor_1_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_NormalWS_3_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_Metallic_4_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd_Smoothness_6_Float, _Property_b986326ad9b34d6ea3a7237ba2bd1cd6_Out_0_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_AmbientOcclusion_5_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float);
        float4 _Property_a2c93b67d7e14184996710181bc8106a_Out_0_Vector4 = _Color_A;
        float2 _Property_30bd0eecac2f497db8c8b272e8e7d3e5_Out_0_Vector2 = _Color_A_Location;
        float4 _Property_47fc9a397b1241599709d29487238203_Out_0_Vector4 = _Color_B;
        float2 _Property_99bf1a52083e4b7f84197e960ed6a728_Out_0_Vector2 = _Color_B_Location;
        float4 _Property_d5f33cf319a54be08f26ec7c7538d6a4_Out_0_Vector4 = _Color_C;
        Bindings_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceNormal = IN.WorldSpaceNormal;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceTangent = IN.WorldSpaceTangent;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.ObjectSpacePosition = IN.ObjectSpacePosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpacePosition = IN.WorldSpacePosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.NDCPosition = IN.NDCPosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.PixelPosition = IN.PixelPosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.uv1 = IN.uv1;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.uv2 = IN.uv2;
        half3 _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc_Lit_1_Vector3;
        SG_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float(_DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3, true, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float, half(1), _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float, _Property_a2c93b67d7e14184996710181bc8106a_Out_0_Vector4, _Property_30bd0eecac2f497db8c8b272e8e7d3e5_Out_0_Vector2, _Property_47fc9a397b1241599709d29487238203_Out_0_Vector4, _Property_99bf1a52083e4b7f84197e960ed6a728_Out_0_Vector2, _Property_d5f33cf319a54be08f26ec7c7538d6a4_Out_0_Vector4, _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc, _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc_Lit_1_Vector3);
        float3 _Add_a11e24e7d4fd4494895fd67f375acb21_Out_2_Vector3;
        Unity_Add_float3(_SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc_Lit_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3, _Add_a11e24e7d4fd4494895fd67f375acb21_Out_2_Vector3);
        float _Property_d5e8251fc84a46aea1765511445b653e_Out_0_Float = _Alpha;
        Bindings_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpaceNormal = IN.WorldSpaceNormal;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpaceTangent = IN.WorldSpaceTangent;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpacePosition = IN.WorldSpacePosition;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.NDCPosition = IN.NDCPosition;
        float3 _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3_Out_1_Vector3;
        SG_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float(_Add_a11e24e7d4fd4494895fd67f375acb21_Out_2_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float, _Property_d5e8251fc84a46aea1765511445b653e_Out_0_Float, _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3, _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3_Out_1_Vector3);
        Lit_1 = _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3_Out_1_Vector3;
        }
        
        void Unity_Blend_Overwrite_float3(float3 Base, float3 Blend, out float3 Out, float Opacity)
        {
            Out = lerp(Base, Blend, Opacity);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float3 BaseColor;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_debbfbf1a581455dbc61b338f851a8d4_Out_0_Boolean = _DisableAllGradients;
            float _Property_2fb138ca7c89409da2d3e517c9bcb36b_Out_0_Boolean = _DisableGradientMap;
            UnityTexture2D _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D = UnityBuildTexture2DStructInternal(_BaseColor, sampler_BaseColor, _BaseColor_TexelSize, float4(1, 1, 0, 0), float4(0, 0, 0, 0));
            float2 _Property_94094af0818841fea1396a1ccae7a167_Out_0_Vector2 = _Tilling;
            float2 _TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2;
            Unity_TilingAndOffset_float(IN.uv0.xy, _Property_94094af0818841fea1396a1ccae7a167_Out_0_Vector2, float2 (0, 0), _TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2);
            float4 _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.tex, _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.samplerstate, _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.GetTransformedUV(_TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2) );
            if (_Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.hdrDecode.x > 0)
                _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4, _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.hdrDecode);
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.r;
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_G_5_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.g;
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_B_6_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.b;
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_A_7_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.a;
            float4 _Property_de1bd6122d6c4aa4ba9d7692e6db8956_Out_0_Vector4 = _Color1;
            float4 _Property_fceba521f35a4cc88bbd9602ff68242f_Out_0_Vector4 = _Color2;
            float2 _Property_e60b8198308f4aad8dfa7a52168790ce_Out_0_Vector2 = _Color_1_Location;
            float _Remap_b9f81469352045d7ae4bb207f0c202fc_Out_3_Float;
            Unity_Remap_float(_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float, _Property_e60b8198308f4aad8dfa7a52168790ce_Out_0_Vector2, float2 (0, 1), _Remap_b9f81469352045d7ae4bb207f0c202fc_Out_3_Float);
            float _Saturate_473bb6d59e074412ad7162adcfe4cef1_Out_1_Float;
            Unity_Saturate_float(_Remap_b9f81469352045d7ae4bb207f0c202fc_Out_3_Float, _Saturate_473bb6d59e074412ad7162adcfe4cef1_Out_1_Float);
            float4 _Lerp_f3e294dc316840b6b4294bcb3e7b3f7b_Out_3_Vector4;
            Unity_Lerp_float4(_Property_de1bd6122d6c4aa4ba9d7692e6db8956_Out_0_Vector4, _Property_fceba521f35a4cc88bbd9602ff68242f_Out_0_Vector4, (_Saturate_473bb6d59e074412ad7162adcfe4cef1_Out_1_Float.xxxx), _Lerp_f3e294dc316840b6b4294bcb3e7b3f7b_Out_3_Vector4);
            float4 _Property_60ebe0bb4a0e4a6fbd758422fbc8e1af_Out_0_Vector4 = _Color3;
            float2 _Property_46eb955dacc0426abc5d73b1be33af42_Out_0_Vector2 = _Color_2_Location;
            float _Remap_c2e999a017a6490b91665566afc5916d_Out_3_Float;
            Unity_Remap_float(_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float, _Property_46eb955dacc0426abc5d73b1be33af42_Out_0_Vector2, float2 (0, 1), _Remap_c2e999a017a6490b91665566afc5916d_Out_3_Float);
            float _Saturate_72fb61dd2f064241a5337c41de422b36_Out_1_Float;
            Unity_Saturate_float(_Remap_c2e999a017a6490b91665566afc5916d_Out_3_Float, _Saturate_72fb61dd2f064241a5337c41de422b36_Out_1_Float);
            float4 _Lerp_72f03acdde2245b29f8cd4a47d6b4cd6_Out_3_Vector4;
            Unity_Lerp_float4(_Lerp_f3e294dc316840b6b4294bcb3e7b3f7b_Out_3_Vector4, _Property_60ebe0bb4a0e4a6fbd758422fbc8e1af_Out_0_Vector4, (_Saturate_72fb61dd2f064241a5337c41de422b36_Out_1_Float.xxxx), _Lerp_72f03acdde2245b29f8cd4a47d6b4cd6_Out_3_Vector4);
            float4 _Property_e84e577bc7db46749ec6367493b51e06_Out_0_Vector4 = _AOColor;
            UnityTexture2D _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D = UnityBuildTexture2DStructInternal(_ORM, sampler_ORM, _ORM_TexelSize, float4(1, 1, 0, 0), float4(0, 0, 0, 0));
            float4 _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.tex, _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.samplerstate, _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.GetTransformedUV(_TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2) );
            if (_Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.hdrDecode.x > 0)
                _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4, _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.hdrDecode);
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_R_4_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.r;
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_G_5_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.g;
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.b;
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_A_7_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.a;
            float _OneMinus_89dcf9413d9142c481cf9d5e5035b712_Out_1_Float;
            Unity_OneMinus_float(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_R_4_Float, _OneMinus_89dcf9413d9142c481cf9d5e5035b712_Out_1_Float);
            float2 _Property_753b87b3c47d403696efc934ea3dbea9_Out_0_Vector2 = _AOLevels;
            float _Remap_983e82311f6e4fe5b382c71d656497ec_Out_3_Float;
            Unity_Remap_float(_OneMinus_89dcf9413d9142c481cf9d5e5035b712_Out_1_Float, _Property_753b87b3c47d403696efc934ea3dbea9_Out_0_Vector2, float2 (1, 0), _Remap_983e82311f6e4fe5b382c71d656497ec_Out_3_Float);
            float _Property_ba1b5b1e95414eaebf54a9d26291e91f_Out_0_Float = _AOIntensity;
            float _Multiply_2341d6d389c44e96bba10c0c1e0c9f14_Out_2_Float;
            Unity_Multiply_float_float(_Remap_983e82311f6e4fe5b382c71d656497ec_Out_3_Float, _Property_ba1b5b1e95414eaebf54a9d26291e91f_Out_0_Float, _Multiply_2341d6d389c44e96bba10c0c1e0c9f14_Out_2_Float);
            float4 _Lerp_8d7f6575739049459e47aaca25cf59d0_Out_3_Vector4;
            Unity_Lerp_float4(_Lerp_72f03acdde2245b29f8cd4a47d6b4cd6_Out_3_Vector4, _Property_e84e577bc7db46749ec6367493b51e06_Out_0_Vector4, (_Multiply_2341d6d389c44e96bba10c0c1e0c9f14_Out_2_Float.xxxx), _Lerp_8d7f6575739049459e47aaca25cf59d0_Out_3_Vector4);
            float4 _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4;
            Unity_Branch_float4(_Property_2fb138ca7c89409da2d3e517c9bcb36b_Out_0_Boolean, (_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float.xxxx), _Lerp_8d7f6575739049459e47aaca25cf59d0_Out_3_Vector4, _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4);
            float _Property_18ebff8ae81646538dc78b9020d39b78_Out_0_Boolean = _Z_Gradient;
            float _Property_f94879ac790e4c109a34ec1f73a3c3a6_Out_0_Boolean = _Y_Gradient;
            float _Property_c61e8f7030cd47ac9b641cd98ca92fe3_Out_0_Boolean = _X_Gradient;
            float4 _Property_1f3e0b1c283f4c949004f43f37fe1a90_Out_0_Vector4 = _X_GradientColor;
            float _Property_94b16f357eac4058809921fa96f34787_Out_0_Float = _X_GradientStartPosition;
            float _Property_8c3dacdcc9cc4b2880b33b1d1913e9f1_Out_0_Float = _X_GradientEndPosition;
            float _Split_2ca42922e29b49b4b7113632901be932_R_1_Float = IN.AbsoluteWorldSpacePosition[0];
            float _Split_2ca42922e29b49b4b7113632901be932_G_2_Float = IN.AbsoluteWorldSpacePosition[1];
            float _Split_2ca42922e29b49b4b7113632901be932_B_3_Float = IN.AbsoluteWorldSpacePosition[2];
            float _Split_2ca42922e29b49b4b7113632901be932_A_4_Float = 0;
            float _Smoothstep_c73aebae28cd43fc896795bd80911919_Out_3_Float;
            Unity_Smoothstep_float(_Property_94b16f357eac4058809921fa96f34787_Out_0_Float, _Property_8c3dacdcc9cc4b2880b33b1d1913e9f1_Out_0_Float, _Split_2ca42922e29b49b4b7113632901be932_R_1_Float, _Smoothstep_c73aebae28cd43fc896795bd80911919_Out_3_Float);
            float _Property_dd9c997bc2514f2bbe579af0da9fecb2_Out_0_Float = _X_GradientIntensity;
            float _Multiply_961d468ad0db4c8ebb883194aa2c7cf5_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_c73aebae28cd43fc896795bd80911919_Out_3_Float, _Property_dd9c997bc2514f2bbe579af0da9fecb2_Out_0_Float, _Multiply_961d468ad0db4c8ebb883194aa2c7cf5_Out_2_Float);
            float4 _Lerp_ba89b24cd32049a0964667d354c52dcc_Out_3_Vector4;
            Unity_Lerp_float4(_Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4, _Property_1f3e0b1c283f4c949004f43f37fe1a90_Out_0_Vector4, (_Multiply_961d468ad0db4c8ebb883194aa2c7cf5_Out_2_Float.xxxx), _Lerp_ba89b24cd32049a0964667d354c52dcc_Out_3_Vector4);
            float4 _Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4;
            Unity_Branch_float4(_Property_c61e8f7030cd47ac9b641cd98ca92fe3_Out_0_Boolean, _Lerp_ba89b24cd32049a0964667d354c52dcc_Out_3_Vector4, _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4, _Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4);
            float4 _Property_bbb56023a68b4c17bbaa88d52a24ab64_Out_0_Vector4 = _Y_GradientColor;
            float _Property_e53d4551d24740e89ab9d2dde9d07fa9_Out_0_Float = _Y_GradientStartPosition;
            float _Property_f705a7a1ffdc47a388bfa2aa340dd71f_Out_0_Float = _Y_GradientEndPosition;
            float _Smoothstep_8f0d8c12283b443dabc9178711fca6ea_Out_3_Float;
            Unity_Smoothstep_float(_Property_e53d4551d24740e89ab9d2dde9d07fa9_Out_0_Float, _Property_f705a7a1ffdc47a388bfa2aa340dd71f_Out_0_Float, _Split_2ca42922e29b49b4b7113632901be932_G_2_Float, _Smoothstep_8f0d8c12283b443dabc9178711fca6ea_Out_3_Float);
            float _Property_c3a47369d5bb47ddb5fcb74791e32d8d_Out_0_Float = _Y_GradientIntensity;
            float _Multiply_0b75652c94cc4e928c2a6eda4bc65145_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_8f0d8c12283b443dabc9178711fca6ea_Out_3_Float, _Property_c3a47369d5bb47ddb5fcb74791e32d8d_Out_0_Float, _Multiply_0b75652c94cc4e928c2a6eda4bc65145_Out_2_Float);
            float4 _Lerp_2023214161dd495a9df4a86e9aab0a55_Out_3_Vector4;
            Unity_Lerp_float4(_Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4, _Property_bbb56023a68b4c17bbaa88d52a24ab64_Out_0_Vector4, (_Multiply_0b75652c94cc4e928c2a6eda4bc65145_Out_2_Float.xxxx), _Lerp_2023214161dd495a9df4a86e9aab0a55_Out_3_Vector4);
            float4 _Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4;
            Unity_Branch_float4(_Property_f94879ac790e4c109a34ec1f73a3c3a6_Out_0_Boolean, _Lerp_2023214161dd495a9df4a86e9aab0a55_Out_3_Vector4, _Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4, _Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4);
            float4 _Property_d20a45cfc0cb48e4a9823f3125a60fb7_Out_0_Vector4 = _Y_GradientColor;
            float _Property_a3a6fd6a552845c797564be4a2b63e5d_Out_0_Float = _Z_GradientStartPosition;
            float _Property_1f6239cf046944d6b4f70da4fca83661_Out_0_Float = _Z_GradientEndPosition;
            float _Smoothstep_9cb470a94c1b4410b6282b2c261d462d_Out_3_Float;
            Unity_Smoothstep_float(_Property_a3a6fd6a552845c797564be4a2b63e5d_Out_0_Float, _Property_1f6239cf046944d6b4f70da4fca83661_Out_0_Float, _Split_2ca42922e29b49b4b7113632901be932_B_3_Float, _Smoothstep_9cb470a94c1b4410b6282b2c261d462d_Out_3_Float);
            float _Property_1ac1a7f19f6342f0a029129f1adbfd67_Out_0_Float = _Z_GradientIntensity;
            float _Multiply_d258a35ceccb4063a2594879737f8bb1_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_9cb470a94c1b4410b6282b2c261d462d_Out_3_Float, _Property_1ac1a7f19f6342f0a029129f1adbfd67_Out_0_Float, _Multiply_d258a35ceccb4063a2594879737f8bb1_Out_2_Float);
            float4 _Lerp_5d732be3cb674212b3307c0a98052b15_Out_3_Vector4;
            Unity_Lerp_float4(_Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4, _Property_d20a45cfc0cb48e4a9823f3125a60fb7_Out_0_Vector4, (_Multiply_d258a35ceccb4063a2594879737f8bb1_Out_2_Float.xxxx), _Lerp_5d732be3cb674212b3307c0a98052b15_Out_3_Vector4);
            float4 _Branch_808c9eecf2bc4ac09cea8d7293c4d22a_Out_3_Vector4;
            Unity_Branch_float4(_Property_18ebff8ae81646538dc78b9020d39b78_Out_0_Boolean, _Lerp_5d732be3cb674212b3307c0a98052b15_Out_3_Vector4, _Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4, _Branch_808c9eecf2bc4ac09cea8d7293c4d22a_Out_3_Vector4);
            float4 _Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4;
            Unity_Branch_float4(_Property_debbfbf1a581455dbc61b338f851a8d4_Out_0_Boolean, _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4, _Branch_808c9eecf2bc4ac09cea8d7293c4d22a_Out_3_Vector4, _Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4);
            UnityTexture2D _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D = UnityBuildTexture2DStructInternal(_Normal, sampler_Normal, _Normal_TexelSize, float4(1, 1, 0, 0), float4(0, 0, 0, 0));
            float4 _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.tex, _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.samplerstate, _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.GetTransformedUV(_TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2) );
            if (_Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.hdrDecode.x > 0)
                _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4, _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.hdrDecode);
            _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.rgb = UnpackNormal(_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4);
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_R_4_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.r;
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_G_5_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.g;
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_B_6_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.b;
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_A_7_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.a;
            float _Property_3c169350af004240a8c8543dce8c320b_Out_0_Boolean = _UseCustomRoughness;
            float _Property_5c2d908353e24a3692fb0e08fe229355_Out_0_Float = _CustomRoughness;
            float _OneMinus_59ec04ea5505409baa7bf818ec2e91cd_Out_1_Float;
            Unity_OneMinus_float(_Property_5c2d908353e24a3692fb0e08fe229355_Out_0_Float, _OneMinus_59ec04ea5505409baa7bf818ec2e91cd_Out_1_Float);
            float _Property_a19c0c9a848947f4aab57660a9a18f93_Out_0_Float = _RoughnessMultiplier;
            float _Multiply_17be8d0efec54ca3bc59e321b0c596ee_Out_2_Float;
            Unity_Multiply_float_float(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_G_5_Float, _Property_a19c0c9a848947f4aab57660a9a18f93_Out_0_Float, _Multiply_17be8d0efec54ca3bc59e321b0c596ee_Out_2_Float);
            float _Saturate_04d0090531f74a9ebdada50fd6f42af5_Out_1_Float;
            Unity_Saturate_float(_Multiply_17be8d0efec54ca3bc59e321b0c596ee_Out_2_Float, _Saturate_04d0090531f74a9ebdada50fd6f42af5_Out_1_Float);
            float _OneMinus_c9932877d03347048da7ba94008426d5_Out_1_Float;
            Unity_OneMinus_float(_Saturate_04d0090531f74a9ebdada50fd6f42af5_Out_1_Float, _OneMinus_c9932877d03347048da7ba94008426d5_Out_1_Float);
            float _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float;
            Unity_Branch_float(_Property_3c169350af004240a8c8543dce8c320b_Out_0_Boolean, _OneMinus_59ec04ea5505409baa7bf818ec2e91cd_Out_1_Float, _OneMinus_c9932877d03347048da7ba94008426d5_Out_1_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float);
            float _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float;
            Unity_Saturate_float(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_R_4_Float, _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float);
            float4 _Property_34dac06f3b7442efa3e05c85b09ec445_Out_0_Vector4 = _ACT1_Color_A;
            float2 _Property_b3a982240db24cfbbc14de979535b458_Out_0_Vector2 = _ACT1_Color_A_Location;
            float4 _Property_0e317fd277254f1aae079db6e8d2e8dc_Out_0_Vector4 = _ACT1_Color_B;
            float2 _Property_3187bf5304c54604a1d464e65a9dac03_Out_0_Vector2 = _ACT1_Color_B_Location;
            float4 _Property_41e2d595ca9d41dd8806c1a749a3bb43_Out_0_Vector4 = _ACT1_Color_C;
            Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceNormal = IN.WorldSpaceNormal;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceTangent = IN.WorldSpaceTangent;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.ObjectSpacePosition = IN.ObjectSpacePosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpacePosition = IN.WorldSpacePosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.NDCPosition = IN.NDCPosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.PixelPosition = IN.PixelPosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.uv1 = IN.uv1;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.uv2 = IN.uv2;
            float3 _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d_Lit_1_Vector3;
            SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), (_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.xyz), _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float, float3 (0, 0, 0), _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float, float(1), _Property_34dac06f3b7442efa3e05c85b09ec445_Out_0_Vector4, _Property_b3a982240db24cfbbc14de979535b458_Out_0_Vector2, _Property_0e317fd277254f1aae079db6e8d2e8dc_Out_0_Vector4, _Property_3187bf5304c54604a1d464e65a9dac03_Out_0_Vector2, _Property_41e2d595ca9d41dd8806c1a749a3bb43_Out_0_Vector4, _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d, _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d_Lit_1_Vector3);
            float4 _Property_259fe0b41799487eb44d557cc24932a4_Out_0_Vector4 = _ACT2_Color_A;
            float2 _Property_83a57d16b4dc4f48985395b92f2de589_Out_0_Vector2 = _ACT2_Color_A_Location;
            float4 _Property_8ca39a18030a4bbd9348cb5b458a8372_Out_0_Vector4 = _ACT2_Color_B;
            float2 _Property_fa2809814b4148bd8ece170a87d230ef_Out_0_Vector2 = _ACT2_Color_B_Location;
            float4 _Property_c83f37081cf64cc7999c1bb19926d7c1_Out_0_Vector4 = _ACT2_Color_C;
            Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceNormal = IN.WorldSpaceNormal;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceTangent = IN.WorldSpaceTangent;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.ObjectSpacePosition = IN.ObjectSpacePosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpacePosition = IN.WorldSpacePosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.NDCPosition = IN.NDCPosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.PixelPosition = IN.PixelPosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.uv1 = IN.uv1;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.uv2 = IN.uv2;
            float3 _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370_Lit_1_Vector3;
            SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), (_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.xyz), _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float, float3 (0, 0, 0), _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float, float(1), _Property_259fe0b41799487eb44d557cc24932a4_Out_0_Vector4, _Property_83a57d16b4dc4f48985395b92f2de589_Out_0_Vector2, _Property_8ca39a18030a4bbd9348cb5b458a8372_Out_0_Vector4, _Property_fa2809814b4148bd8ece170a87d230ef_Out_0_Vector2, _Property_c83f37081cf64cc7999c1bb19926d7c1_Out_0_Vector4, _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370, _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370_Lit_1_Vector3);
            float4 _Property_13886bd11bee4d40b3eb7be3dff1c022_Out_0_Vector4 = _ACT3_Color_A;
            float2 _Property_5578cc8231d143d9a5f34ae24f110091_Out_0_Vector2 = _ACT3_Color_A_Location;
            float4 _Property_4d39077496e94d728ca3d19d42d3bd68_Out_0_Vector4 = _ACT3_Color_B;
            float2 _Property_5b1c9e69733942519a853e51dd4770f6_Out_0_Vector2 = _ACT3_Color_B_Location;
            float4 _Property_6a5550ba5eec4966850efea3418c844a_Out_0_Vector4 = _ACT3_Color_C;
            Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceNormal = IN.WorldSpaceNormal;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceTangent = IN.WorldSpaceTangent;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.ObjectSpacePosition = IN.ObjectSpacePosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpacePosition = IN.WorldSpacePosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.NDCPosition = IN.NDCPosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.PixelPosition = IN.PixelPosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.uv1 = IN.uv1;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.uv2 = IN.uv2;
            float3 _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a_Lit_1_Vector3;
            SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), (_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.xyz), _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float, float3 (0, 0, 0), _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float, float(1), _Property_13886bd11bee4d40b3eb7be3dff1c022_Out_0_Vector4, _Property_5578cc8231d143d9a5f34ae24f110091_Out_0_Vector2, _Property_4d39077496e94d728ca3d19d42d3bd68_Out_0_Vector4, _Property_5b1c9e69733942519a853e51dd4770f6_Out_0_Vector2, _Property_6a5550ba5eec4966850efea3418c844a_Out_0_Vector4, _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a, _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a_Lit_1_Vector3);
            float3 _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3;
            if (_CURRENTACT_ACT_1) _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3 = _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d_Lit_1_Vector3;
            else if (_CURRENTACT_ACT_2) _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3 = _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370_Lit_1_Vector3;
            else _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3 = _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a_Lit_1_Vector3;
            float _Property_854b1929338847e9b4a11e77fedb361b_Out_0_Float = _LightingGradientMapInfluence;
            float3 _Blend_ed10fcb74d224507bd92f8d23fb7e2d9_Out_2_Vector3;
            Unity_Blend_Overwrite_float3((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3, _Blend_ed10fcb74d224507bd92f8d23fb7e2d9_Out_2_Vector3, _Property_854b1929338847e9b4a11e77fedb361b_Out_0_Float);
            surface.BaseColor = _Blend_ed10fcb74d224507bd92f8d23fb7e2d9_Out_2_Vector3;
            surface.Alpha = float(1);
            surface.AlphaClipThreshold = float(0.5);
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
            // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            float3 unnormalizedNormalWS = input.normalWS;
            const float renormFactor = 1.0 / length(unnormalizedNormalWS);
        
            // use bitangent on the fly like in hdrp
            // IMPORTANT! If we ever support Flip on double sided materials ensure bitangent and tangent are NOT flipped.
            float crossSign = (input.tangentWS.w > 0.0 ? 1.0 : -1.0)* GetOddNegativeScale();
            float3 bitang = crossSign * cross(input.normalWS.xyz, input.tangentWS.xyz);
        
            output.WorldSpaceNormal = renormFactor * input.normalWS.xyz;      // we want a unit length Normal Vector node in shader graph
        
            // to pr               eserve mikktspace compliance we use same scale renormFactor as was used on the normal.
            // This                is explained in section 2.2 in "surface gradient based bump mapping framework"
            output.WorldSpaceTangent = renormFactor * input.tangentWS.xyz;
            output.WorldSpaceBiTangent = renormFactor * bitang;
        
            output.WorldSpaceViewDirection = GetWorldSpaceNormalizeViewDir(input.positionWS);
            output.WorldSpacePosition = input.positionWS;
            output.ObjectSpacePosition = TransformWorldToObject(input.positionWS);
            output.AbsoluteWorldSpacePosition = GetAbsolutePositionWS(input.positionWS);
        
            #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x < 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #else
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x > 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScaledScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 = input.texCoord0;
            output.uv1 = input.texCoord1;
            output.uv2 = input.texCoord2;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/SelectionPickingPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "Universal 2D"
            Tags
            {
                "LightMode" = "Universal2D"
            }
        
        // Render State
        Cull [_Cull]
        Blend [_SrcBlend] [_DstBlend], [_SrcBlendAlpha] [_DstBlendAlpha]
        ZTest [_ZTest]
        ZWrite [_ZWrite]
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        #pragma shader_feature_local_fragment _ _ALPHATEST_ON
        #pragma shader_feature _CURRENTACT_ACT_1 _CURRENTACT_ACT_2 _CURRENTACT_ACT_3
        
        
        
        // Defines
        
        #define _NORMALMAP 1
        #define _NORMAL_DROPOFF_TS 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define ATTRIBUTES_NEED_TEXCOORD1
        #define ATTRIBUTES_NEED_TEXCOORD2
        #define FEATURES_GRAPH_VERTEX_NORMAL_OUTPUT
        #define FEATURES_GRAPH_VERTEX_TANGENT_OUTPUT
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TANGENT_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define VARYINGS_NEED_TEXCOORD1
        #define VARYINGS_NEED_TEXCOORD2
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_2D
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRenderingKeywords.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/FoveatedRendering.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/DebugMipmapStreamingMacros.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 uv1 : TEXCOORD1;
             float4 uv2 : TEXCOORD2;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(ATTRIBUTES_NEED_INSTANCEID)
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
             float4 tangentWS;
             float4 texCoord0;
             float4 texCoord1;
             float4 texCoord2;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 WorldSpaceNormal;
             float3 WorldSpaceTangent;
             float3 WorldSpaceBiTangent;
             float3 WorldSpaceViewDirection;
             float3 ObjectSpacePosition;
             float3 WorldSpacePosition;
             float3 AbsoluteWorldSpacePosition;
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float4 uv1;
             float4 uv2;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 tangentWS : INTERP0;
             float4 texCoord0 : INTERP1;
             float4 texCoord1 : INTERP2;
             float4 texCoord2 : INTERP3;
             float3 positionWS : INTERP4;
             float3 normalWS : INTERP5;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.tangentWS.xyzw = input.tangentWS;
            output.texCoord0.xyzw = input.texCoord0;
            output.texCoord1.xyzw = input.texCoord1;
            output.texCoord2.xyzw = input.texCoord2;
            output.positionWS.xyz = input.positionWS;
            output.normalWS.xyz = input.normalWS;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.tangentWS = input.tangentWS.xyzw;
            output.texCoord0 = input.texCoord0.xyzw;
            output.texCoord1 = input.texCoord1.xyzw;
            output.texCoord2 = input.texCoord2.xyzw;
            output.positionWS = input.positionWS.xyz;
            output.normalWS = input.normalWS.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(VARYINGS_NEED_INSTANCEID)
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _DisableAllGradients;
        float4 _ACT1_Color_B;
        float4 _ACT2_Color_B;
        float2 _ACT2_Color_B_Location;
        float4 _ACT2_Color_C;
        float4 _ACT2_Color_A;
        float2 _ACT2_Color_A_Location;
        float2 _ACT1_Color_B_Location;
        float4 _ACT1_Color_C;
        float4 _ACT1_Color_A;
        float2 _ACT1_Color_A_Location;
        float4 _ACT3_Color_B;
        float2 _ACT3_Color_B_Location;
        float4 _ACT3_Color_C;
        float4 _ACT3_Color_A;
        float2 _ACT3_Color_A_Location;
        float4 _ORM_TexelSize;
        float4 _Color3;
        float _Y_GradientEndPosition;
        float4 _Color2;
        float2 _Color_2_Location;
        float4 _AOColor;
        float4 _Normal_TexelSize;
        float2 _AOLevels;
        float4 _BaseColor_TexelSize;
        float4 _Color1;
        float2 _Color_1_Location;
        float _AOIntensity;
        float _RoughnessMultiplier;
        float2 _Tilling;
        float _DisableGradientMap;
        float4 _Y_GradientColor;
        float _Y_GradientIntensity;
        float _Y_GradientStartPosition;
        float _Y_Gradient;
        float _X_Gradient;
        float _X_GradientIntensity;
        float4 _X_GradientColor;
        float _X_GradientStartPosition;
        float _X_GradientEndPosition;
        float _Z_Gradient;
        float _Z_GradientIntensity;
        float4 _Z_GradientColor;
        float _Z_GradientStartPosition;
        float _Z_GradientEndPosition;
        float _UseCustomRoughness;
        float _CustomRoughness;
        float _LightingGradientMapInfluence;
        UNITY_TEXTURE_STREAMING_DEBUG_VARS;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ORM);
        SAMPLER(sampler_ORM);
        TEXTURE2D(_Normal);
        SAMPLER(sampler_Normal);
        TEXTURE2D(_BaseColor);
        SAMPLER(sampler_BaseColor);
        
        // Graph Includes
        #include_with_pragmas "Assets/Samples/Shader Graph/17.3.0/Custom Lighting/Components/Debug/DebugLightingComplexity.hlsl"
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
        {
            Out = lerp(A, B, T);
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Branch_float4(float Predicate, float4 True, float4 False, out float4 Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        void Unity_Branch_float(float Predicate, float True, float False, out float Out)
        {
            Out = Predicate ? True : False;
        }
        
        void Unity_Divide_float(float A, float B, out float Out)
        {
            Out = A / B;
        }
        
        // unity-custom-func-begin
        void ApplyDecals_float(float4 positionCS, float3 baseColor, float3 specularColor, float3 normalWS, float metallic, float smoothness, float occlusion, out float3 baseColorOut, out float3 specularColorOut, out float3 normalWSOut, out float metallicOut, out float smoothnessOut, out float occlusionOut){
        #if !defined(SHADERGRAPH_PREVIEW) && defined(_DBUFFER)
        	ApplyDecal(positionCS, baseColor, specularColor, normalWS, metallic, occlusion, smoothness);
        	baseColorOut = baseColor;
        	specularColorOut = specularColor;
        	normalWSOut = normalWS;
        	metallicOut = metallic;
        	occlusionOut = occlusion;
        	smoothnessOut = smoothness;
        #else
        	baseColorOut = baseColor;
        	specularColorOut = specularColor;
        	normalWSOut = normalWS;
        	metallicOut = metallic;
        	occlusionOut = occlusion;
        	smoothnessOut = smoothness;
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float
        {
        float2 PixelPosition;
        };
        
        void SG_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float(float3 _Base_Color, float3 _NormalWS, float _Metallic, float _Smoothness, float _AmbientOcclusion, Bindings_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float IN, out float3 BaseColor_1, out float3 SpecularColor_2, out float3 NormalWS_3, out float Metallic_4, out float Smoothness_6, out float AmbientOcclusion_5)
        {
        float4 _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4 = float4(IN.PixelPosition.xy, 0, 0);
        float _Split_ad27d29658ef44f7b6941c97694d6866_R_1_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[0];
        float _Split_ad27d29658ef44f7b6941c97694d6866_G_2_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[1];
        float _Split_ad27d29658ef44f7b6941c97694d6866_B_3_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[2];
        float _Split_ad27d29658ef44f7b6941c97694d6866_A_4_Float = _ScreenPosition_475ffbde1b774459b0d45276e537a836_Out_0_Vector4[3];
        float _Divide_3f9fb3b7b5b94d0d8246bbc34aa63f7b_Out_2_Float;
        Unity_Divide_float(_Split_ad27d29658ef44f7b6941c97694d6866_G_2_Float, _ScreenParams.y, _Divide_3f9fb3b7b5b94d0d8246bbc34aa63f7b_Out_2_Float);
        float _OneMinus_3cfec48ba27f4585a5feb790836dc9dc_Out_1_Float;
        Unity_OneMinus_float(_Divide_3f9fb3b7b5b94d0d8246bbc34aa63f7b_Out_2_Float, _OneMinus_3cfec48ba27f4585a5feb790836dc9dc_Out_1_Float);
        float _Multiply_a4baed73797c41c1b9631ae9bbddfe12_Out_2_Float;
        Unity_Multiply_float_float(_OneMinus_3cfec48ba27f4585a5feb790836dc9dc_Out_1_Float, _ScreenParams.y, _Multiply_a4baed73797c41c1b9631ae9bbddfe12_Out_2_Float);
        float2 _Vector2_eed86f79e1de4c188df97eb091955bc5_Out_0_Vector2 = float2(_Split_ad27d29658ef44f7b6941c97694d6866_R_1_Float, _Multiply_a4baed73797c41c1b9631ae9bbddfe12_Out_2_Float);
        float3 _Property_6219e38e66a84dddb55188eb0359a8c3_Out_0_Vector3 = _Base_Color;
        float3 _Property_f4c37d8281c1497e8dab743349080d88_Out_0_Vector3 = _NormalWS;
        float _Property_0826181079c84604befc19a2460f4daa_Out_0_Float = _Metallic;
        float _Property_d54a743184cc4f27b93d5f5b239c7b7e_Out_0_Float = _Smoothness;
        float _Property_bd6cbdae9db240b9b4ad935655106f79_Out_0_Float = _AmbientOcclusion;
        float3 _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_baseColorOut_8_Vector3;
        float3 _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_specularColorOut_9_Vector3;
        float3 _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_normalWSOut_10_Vector3;
        float _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_metallicOut_11_Float;
        float _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_smoothnessOut_13_Float;
        float _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_occlusionOut_12_Float;
        ApplyDecals_float((float4(_Vector2_eed86f79e1de4c188df97eb091955bc5_Out_0_Vector2, 0.0, 1.0)), _Property_6219e38e66a84dddb55188eb0359a8c3_Out_0_Vector3, float3 (0, 0, 0), _Property_f4c37d8281c1497e8dab743349080d88_Out_0_Vector3, _Property_0826181079c84604befc19a2460f4daa_Out_0_Float, _Property_d54a743184cc4f27b93d5f5b239c7b7e_Out_0_Float, _Property_bd6cbdae9db240b9b4ad935655106f79_Out_0_Float, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_baseColorOut_8_Vector3, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_specularColorOut_9_Vector3, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_normalWSOut_10_Vector3, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_metallicOut_11_Float, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_smoothnessOut_13_Float, _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_occlusionOut_12_Float);
        BaseColor_1 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_baseColorOut_8_Vector3;
        SpecularColor_2 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_specularColorOut_9_Vector3;
        NormalWS_3 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_normalWSOut_10_Vector3;
        Metallic_4 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_metallicOut_11_Float;
        Smoothness_6 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_smoothnessOut_13_Float;
        AmbientOcclusion_5 = _ApplyDecalsCustomFunction_181ae7291f3f43ed9e347bab0596980a_occlusionOut_12_Float;
        }
        
        // unity-custom-func-begin
        void SwitchLightingDebug_float(float3 BaseColorIn, float3 NormalIn, float MetallicIn, float SmoothnessIn, float3 EmissionIn, float AmbientOcclusionIn, float3 positionWS, float3 bakedGI, out float3 BaseColorOut, out float3 NormalOut, out float MetallicOut, out float SmoothnessOut, out float3 EmissionOut, out float AmbientOcclusionOut){
        #if !defined(SHADERGRAPH_PREVIEW) && defined(DEBUG_DISPLAY)
        
        [branch] switch(int(_DebugLightingMode))
        
        {
        
            case 0: //none
        
        		BaseColorOut = BaseColorIn;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
            case 1: //SHADOW_CASCADES
        
        		half cascadeIndex = ComputeCascadeIndex(positionWS);
        
        		switch (uint(cascadeIndex))
        
        		{
        
        			case 0: BaseColorOut = kDebugColorShadowCascade0.rgb;break;
        
        			case 1: BaseColorOut = kDebugColorShadowCascade1.rgb;break;
        
        			case 2: BaseColorOut = kDebugColorShadowCascade2.rgb;break;
        
        			case 3: BaseColorOut = kDebugColorShadowCascade3.rgb;break;
        
        			default: BaseColorOut = kDebugColorBlack.rgb;break;
        
        		}
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
            case 2: //LIGHTING_WITHOUT_NORMAL_MAPS
        
        		BaseColorOut = float3(1,1,1);
        
        		MetallicOut = 0;
        
        		SmoothnessOut = 0;
        
        		NormalOut = float3(0,0,1);
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            case 3: //LIGHTING_WITH_NORMAL_MAPS
        
        		BaseColorOut = float3(1,1,1);
        
        		MetallicOut = 0;
        
        		SmoothnessOut = 0;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            case 4: //REFLECTIONS
        
        		BaseColorOut = float3(0.1,0.1,0.1);
        
        		MetallicOut = 1;
        
        		SmoothnessOut = 1;
        
        		NormalOut = float3(0,0,1);
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            case 5: //REFLECTIONS_WITH_SMOOTHNESS
        
        		BaseColorOut = float3(0.1,0.1,0.1);
        
        		MetallicOut = 1;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
            case 6: //GLOBAL_ILLUM
        
        		BaseColorOut = bakedGI;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = 0;
        
        		NormalOut = float3(0,0,1);
        
        		EmissionOut = float3(0,0,0);
        
        		AmbientOcclusionOut = 1;
        
        		break;
        
            default:
        
        		BaseColorOut = BaseColorIn;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        		break;
        
        }
        
        #else
        
        		BaseColorOut = BaseColorIn;
        
        		MetallicOut = MetallicIn;
        
        		SmoothnessOut = SmoothnessIn;
        
        		NormalOut = NormalIn;
        
        		EmissionOut = EmissionIn;
        
        		AmbientOcclusionOut = AmbientOcclusionIn;
        
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_DebugLighting_61e571d2b9ede1240a524a849d20c997_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpacePosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_DebugLighting_61e571d2b9ede1240a524a849d20c997_float(float3 _Base_Color, float3 _NormalWS, float _Metallic, float _Smoothness, float3 _Emission, float _AmbientOcclusion, Bindings_DebugLighting_61e571d2b9ede1240a524a849d20c997_float IN, out float3 BaseColor_1, out float3 Normal_4, out float Metallic_2, out float Smoothness_3, out float3 Emission_5, out float AmbientOcclusion_6)
        {
        float3 _Property_501515703e3a4a1dbd19f4ae273add46_Out_0_Vector3 = _Base_Color;
        float3 _Property_e5bcb5bf3b62412b8983e3aa1ada8fcd_Out_0_Vector3 = _NormalWS;
        float3 _Transform_13c6b1e888d440fd8340d4a7138979ba_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_13c6b1e888d440fd8340d4a7138979ba_Out_1_Vector3 = TransformWorldToTangent(_Property_e5bcb5bf3b62412b8983e3aa1ada8fcd_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float _Property_7a450453146043b2b11397a72c325042_Out_0_Float = _Metallic;
        float _Property_f0326121e031478a90610d60b8321364_Out_0_Float = _Smoothness;
        float3 _Property_491d95b34bb245718ee21bff5fc249cd_Out_0_Vector3 = _Emission;
        float _Property_da91a6effd53499db08bb774d5686c68_Out_0_Float = _AmbientOcclusion;
        float3 _BakedGI_3f01c30cb8b64e9d9f7fbe474622c7dc_Out_1_Vector3 = SHADERGRAPH_BAKED_GI(IN.WorldSpacePosition, _Property_e5bcb5bf3b62412b8983e3aa1ada8fcd_Out_0_Vector3, IN.PixelPosition.xy, IN.uv1.xy, IN.uv2.xy, true);
        float3 _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_BaseColorOut_7_Vector3;
        float3 _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_NormalOut_11_Vector3;
        float _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_MetallicOut_9_Float;
        float _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_SmoothnessOut_10_Float;
        float3 _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_EmissionOut_12_Vector3;
        float _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_AmbientOcclusionOut_13_Float;
        SwitchLightingDebug_float(_Property_501515703e3a4a1dbd19f4ae273add46_Out_0_Vector3, _Transform_13c6b1e888d440fd8340d4a7138979ba_Out_1_Vector3, _Property_7a450453146043b2b11397a72c325042_Out_0_Float, _Property_f0326121e031478a90610d60b8321364_Out_0_Float, _Property_491d95b34bb245718ee21bff5fc249cd_Out_0_Vector3, _Property_da91a6effd53499db08bb774d5686c68_Out_0_Float, IN.WorldSpacePosition, _BakedGI_3f01c30cb8b64e9d9f7fbe474622c7dc_Out_1_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_BaseColorOut_7_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_NormalOut_11_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_MetallicOut_9_Float, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_SmoothnessOut_10_Float, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_EmissionOut_12_Vector3, _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_AmbientOcclusionOut_13_Float);
        BaseColor_1 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_BaseColorOut_7_Vector3;
        Normal_4 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_NormalOut_11_Vector3;
        Metallic_2 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_MetallicOut_9_Float;
        Smoothness_3 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_SmoothnessOut_10_Float;
        Emission_5 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_EmissionOut_12_Vector3;
        AmbientOcclusion_6 = _SwitchLightingDebugCustomFunction_42498302ac764da3bfbfa011ea59470b_AmbientOcclusionOut_13_Float;
        }
        
        // unity-custom-func-begin
        void GetMainLightData_float(float3 worldPos, out float3 direction, out float3 color, out float shadowAtten){
        #ifdef SHADERGRAPH_PREVIEW
            direction = normalize(float3(-0.7,0.7,-0.7));
            color = float3(1,1,1);
            shadowAtten = 1;
        #else
            #if defined(UNIVERSAL_PIPELINE_CORE_INCLUDED)
                float4 shadowCoord = TransformWorldToShadowCoord(worldPos);
                Light mainLight = GetMainLight(shadowCoord);
                direction = mainLight.direction;
                color = mainLight.color;
                shadowAtten = mainLight.shadowAttenuation;
            #else
                direction = normalize(float3(-0.7, 0.7, -0.7));
                color = float3(1, 1, 1);
                shadowAtten = 0;
            #endif
        #endif
        }
        // unity-custom-func-end
        
        // unity-custom-func-begin
        void GetMainLightData_half(half3 worldPos, out half3 direction, out half3 color, out half shadowAtten){
        #ifdef SHADERGRAPH_PREVIEW
            direction = normalize(float3(-0.7,0.7,-0.7));
            color = float3(1,1,1);
            shadowAtten = 1;
        #else
            #if defined(UNIVERSAL_PIPELINE_CORE_INCLUDED)
                float4 shadowCoord = TransformWorldToShadowCoord(worldPos);
                Light mainLight = GetMainLight(shadowCoord);
                direction = mainLight.direction;
                color = mainLight.color;
                shadowAtten = mainLight.shadowAttenuation;
            #else
                direction = normalize(float3(-0.7, 0.7, -0.7));
                color = float3(1, 1, 1);
                shadowAtten = 0;
            #endif
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float
        {
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float IN, out float3 Direction_1, out float3 Color_2, out float ShadowAtten_3)
        {
        float3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        float3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        float _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        GetMainLightData_float(IN.AbsoluteWorldSpacePosition, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float);
        Direction_1 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        Color_2 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        ShadowAtten_3 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        }
        
        struct Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half
        {
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half(Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half IN, out half3 Direction_1, out half3 Color_2, out half ShadowAtten_3)
        {
        half3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        half3 _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        half _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        GetMainLightData_half(IN.AbsoluteWorldSpacePosition, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3, _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float);
        Direction_1 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_direction_2_Vector3;
        Color_2 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_color_3_Vector3;
        ShadowAtten_3 = _GetMainLightDataCustomFunction_70d2891fcf6441078175ae15c3935f14_shadowAtten_5_Float;
        }
        
        void Unity_DotProduct_float3(float3 A, float3 B, out float Out)
        {
            Out = dot(A, B);
        }
        
        void Unity_DotProduct_half3(half3 A, half3 B, out half Out)
        {
            Out = dot(A, B);
        }
        
        void Unity_Saturate_half(half In, out half Out)
        {
            Out = saturate(In);
        }
        
        struct Bindings_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half
        {
        float3 WorldSpaceNormal;
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half(half3 _NormalWS, bool _NormalWS_68a7999ae9ea4bfba3702fd95b0d1a14_IsConnected, half3 _LightVector, bool _LightVector_a12354c78b694cc6b2bdddd67d09ccdc_IsConnected, Bindings_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half IN, out half Diffuse_1)
        {
        half3 _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3 = _NormalWS;
        bool _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3_IsConnected = _NormalWS_68a7999ae9ea4bfba3702fd95b0d1a14_IsConnected;
        half3 _BranchOnInputConnection_71cde5ac4ee04aacb1e2544c8017ba47_Out_3_Vector3 = _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3_IsConnected ? _Property_c979bfc9e06b459ca5e503658f2eda27_Out_0_Vector3 : IN.WorldSpaceNormal;
        half3 _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3 = _LightVector;
        bool _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3_IsConnected = _LightVector_a12354c78b694cc6b2bdddd67d09ccdc_IsConnected;
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half _MainLight_fa0151c045984bcab58e58725bae0709;
        _MainLight_fa0151c045984bcab58e58725bae0709.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_fa0151c045984bcab58e58725bae0709_Direction_1_Vector3;
        half3 _MainLight_fa0151c045984bcab58e58725bae0709_Color_2_Vector3;
        half _MainLight_fa0151c045984bcab58e58725bae0709_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_half(_MainLight_fa0151c045984bcab58e58725bae0709, _MainLight_fa0151c045984bcab58e58725bae0709_Direction_1_Vector3, _MainLight_fa0151c045984bcab58e58725bae0709_Color_2_Vector3, _MainLight_fa0151c045984bcab58e58725bae0709_ShadowAtten_3_Float);
        half3 _BranchOnInputConnection_d18845e766084954af1aa554531c90b9_Out_3_Vector3 = _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3_IsConnected ? _Property_99ccf4aecf59420794efa0951355f7ab_Out_0_Vector3 : _MainLight_fa0151c045984bcab58e58725bae0709_Direction_1_Vector3;
        half _DotProduct_daa979e4f9384944a14a23e079be6a5c_Out_2_Float;
        Unity_DotProduct_half3(_BranchOnInputConnection_71cde5ac4ee04aacb1e2544c8017ba47_Out_3_Vector3, _BranchOnInputConnection_d18845e766084954af1aa554531c90b9_Out_3_Vector3, _DotProduct_daa979e4f9384944a14a23e079be6a5c_Out_2_Float);
        half _Saturate_4747439b9bfa431293a93646ce71aa12_Out_1_Float;
        Unity_Saturate_half(_DotProduct_daa979e4f9384944a14a23e079be6a5c_Out_2_Float, _Saturate_4747439b9bfa431293a93646ce71aa12_Out_1_Float);
        Diffuse_1 = _Saturate_4747439b9bfa431293a93646ce71aa12_Out_1_Float;
        }
        
        void Unity_Lerp_float(float A, float B, float T, out float Out)
        {
            Out = lerp(A, B, T);
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Reciprocal_float(float In, out float Out)
        {
            Out = 1.0/In;
        }
        
        void Unity_FresnelEffect_float(float3 Normal, float3 ViewDir, float Power, out float Out)
        {
            Out = pow((1.0 - saturate(dot(normalize(Normal), ViewDir))), Power);
        }
        
        void Unity_Lerp_float3(float3 A, float3 B, float3 T, out float3 Out)
        {
            Out = lerp(A, B, T);
        }
        
        struct Bindings_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        };
        
        void SG_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float(float3 _Base_Color, float3 _NormalWS, bool _NormalWS_3240674a787044a092398b1ca753ad83_IsConnected, float _Metallic, float _Smoothness, float _F0, Bindings_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float IN, out float3 Reflectance_1)
        {
        float _Property_4678b902494b4e1a9b08a8067b7bed85_Out_0_Float = _Smoothness;
        float _OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float;
        Unity_OneMinus_float(_Property_4678b902494b4e1a9b08a8067b7bed85_Out_0_Float, _OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float);
        float _Multiply_314f6a0aec0a44538333e69617e91cf9_Out_2_Float;
        Unity_Multiply_float_float(_OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float, _OneMinus_0aa2823e9b1a4726bd6382418b3e6a87_Out_1_Float, _Multiply_314f6a0aec0a44538333e69617e91cf9_Out_2_Float);
        float _Add_513724b99c2f4ea2a803e64d80f0c25b_Out_2_Float;
        Unity_Add_float(_Multiply_314f6a0aec0a44538333e69617e91cf9_Out_2_Float, float(1), _Add_513724b99c2f4ea2a803e64d80f0c25b_Out_2_Float);
        float _Reciprocal_67b338305d0043abb9e7d82dd0f16146_Out_1_Float;
        Unity_Reciprocal_float(_Add_513724b99c2f4ea2a803e64d80f0c25b_Out_2_Float, _Reciprocal_67b338305d0043abb9e7d82dd0f16146_Out_1_Float);
        float _Property_b67a6773dce34e91ae69bbf282d871cc_Out_0_Float = _F0;
        float _Property_703d9ec0a0894a3b965f0ed25a10435b_Out_0_Float = _F0;
        float _Add_8ad10875906b4a80872f2b2fb0518183_Out_2_Float;
        Unity_Add_float(_Property_4678b902494b4e1a9b08a8067b7bed85_Out_0_Float, _Property_703d9ec0a0894a3b965f0ed25a10435b_Out_0_Float, _Add_8ad10875906b4a80872f2b2fb0518183_Out_2_Float);
        float3 _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3 = _NormalWS;
        bool _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3_IsConnected = _NormalWS_3240674a787044a092398b1ca753ad83_IsConnected;
        float3 _BranchOnInputConnection_43b8bde55a8a41468ba21d53db128986_Out_3_Vector3 = _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3_IsConnected ? _Property_b5d757941bc04f70897cc735055cef09_Out_0_Vector3 : IN.WorldSpaceNormal;
        float _FresnelEffect_34b729c62edd4d5f99dc20e2e7d0a7fa_Out_3_Float;
        Unity_FresnelEffect_float(_BranchOnInputConnection_43b8bde55a8a41468ba21d53db128986_Out_3_Vector3, IN.WorldSpaceViewDirection, float(4), _FresnelEffect_34b729c62edd4d5f99dc20e2e7d0a7fa_Out_3_Float);
        float _Lerp_1cad776e609842c181b8acfaef47c317_Out_3_Float;
        Unity_Lerp_float(_Property_b67a6773dce34e91ae69bbf282d871cc_Out_0_Float, _Add_8ad10875906b4a80872f2b2fb0518183_Out_2_Float, _FresnelEffect_34b729c62edd4d5f99dc20e2e7d0a7fa_Out_3_Float, _Lerp_1cad776e609842c181b8acfaef47c317_Out_3_Float);
        float _Multiply_0d364d1c231246d981281b868ea74a95_Out_2_Float;
        Unity_Multiply_float_float(_Reciprocal_67b338305d0043abb9e7d82dd0f16146_Out_1_Float, _Lerp_1cad776e609842c181b8acfaef47c317_Out_3_Float, _Multiply_0d364d1c231246d981281b868ea74a95_Out_2_Float);
        float3 _Property_87ae51a595c24e46ad9ef0f4493231fc_Out_0_Vector3 = _Base_Color;
        float _Property_ce0a90815c5046b48dd0564711f2b466_Out_0_Float = _Metallic;
        float3 _Lerp_6b4c86a1d5004851a7dba1bacc4fd953_Out_3_Vector3;
        Unity_Lerp_float3((_Multiply_0d364d1c231246d981281b868ea74a95_Out_2_Float.xxx), _Property_87ae51a595c24e46ad9ef0f4493231fc_Out_0_Vector3, (_Property_ce0a90815c5046b48dd0564711f2b466_Out_0_Float.xxx), _Lerp_6b4c86a1d5004851a7dba1bacc4fd953_Out_3_Vector3);
        Reflectance_1 = _Lerp_6b4c86a1d5004851a7dba1bacc4fd953_Out_3_Vector3;
        }
        
        void Unity_Add_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A + B;
        }
        
        void Unity_Normalize_float3(float3 In, out float3 Out)
        {
            Out = normalize(In);
        }
        
        struct Bindings_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float
        {
        };
        
        void SG_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float(float3 _viewDir, float3 _lightDir, Bindings_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float IN, out float3 Out_1)
        {
        float3 _Property_fde52ad74bda46adabbcc34b42b16131_Out_0_Vector3 = _viewDir;
        float3 _Property_1dc55a6640574aaf8c04290eb0d5e816_Out_0_Vector3 = _lightDir;
        float3 _Add_2a8cf5c52e8c4e3fb8c3f9a87b68b2a2_Out_2_Vector3;
        Unity_Add_float3(_Property_fde52ad74bda46adabbcc34b42b16131_Out_0_Vector3, _Property_1dc55a6640574aaf8c04290eb0d5e816_Out_0_Vector3, _Add_2a8cf5c52e8c4e3fb8c3f9a87b68b2a2_Out_2_Vector3);
        float3 _Normalize_b3b8196f46224ae3a998bba24956de7f_Out_1_Vector3;
        Unity_Normalize_float3(_Add_2a8cf5c52e8c4e3fb8c3f9a87b68b2a2_Out_2_Vector3, _Normalize_b3b8196f46224ae3a998bba24956de7f_Out_1_Vector3);
        Out_1 = _Normalize_b3b8196f46224ae3a998bba24956de7f_Out_1_Vector3;
        }
        
        void Unity_Exponential2_float(float In, out float Out)
        {
            Out = exp2(In);
        }
        
        struct Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float
        {
        };
        
        void SG_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float(float _Smoothness, Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float IN, out float SpecPower_1)
        {
        float _Property_80f639c6927445458cce37e8c24909a1_Out_0_Float = _Smoothness;
        float _Multiply_c7e9a2c01b804964a64dd7499f62a400_Out_2_Float;
        Unity_Multiply_float_float(_Property_80f639c6927445458cce37e8c24909a1_Out_0_Float, 10, _Multiply_c7e9a2c01b804964a64dd7499f62a400_Out_2_Float);
        float _Add_663b867a23b04deaa88c2ebd79bcdbc7_Out_2_Float;
        Unity_Add_float(_Multiply_c7e9a2c01b804964a64dd7499f62a400_Out_2_Float, float(1), _Add_663b867a23b04deaa88c2ebd79bcdbc7_Out_2_Float);
        float _Exponential_bd8d33b85b2e46508bef0c6cc36f9dae_Out_1_Float;
        Unity_Exponential2_float(_Add_663b867a23b04deaa88c2ebd79bcdbc7_Out_2_Float, _Exponential_bd8d33b85b2e46508bef0c6cc36f9dae_Out_1_Float);
        SpecPower_1 = _Exponential_bd8d33b85b2e46508bef0c6cc36f9dae_Out_1_Float;
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
        Out = A * B;
        }
        
        struct Bindings_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 AbsoluteWorldSpacePosition;
        };
        
        void SG_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float(float3 _NormalWS, bool _NormalWS_5a3c9a3a7faa491894a42d170b5bfeb5_IsConnected, float _Smoothness, float3 _Reflectance, float3 _LightVector, bool _LightVector_3db37b6247094f32bcccc4cb689d525f_IsConnected, Bindings_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float IN, out float3 Specular_1)
        {
        float3 _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3 = _LightVector;
        bool _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3_IsConnected = _LightVector_3db37b6247094f32bcccc4cb689d525f_IsConnected;
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float _MainLight_6570bf88718b46ebb6bd80eec408287a;
        _MainLight_6570bf88718b46ebb6bd80eec408287a.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_6570bf88718b46ebb6bd80eec408287a_Direction_1_Vector3;
        half3 _MainLight_6570bf88718b46ebb6bd80eec408287a_Color_2_Vector3;
        half _MainLight_6570bf88718b46ebb6bd80eec408287a_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(_MainLight_6570bf88718b46ebb6bd80eec408287a, _MainLight_6570bf88718b46ebb6bd80eec408287a_Direction_1_Vector3, _MainLight_6570bf88718b46ebb6bd80eec408287a_Color_2_Vector3, _MainLight_6570bf88718b46ebb6bd80eec408287a_ShadowAtten_3_Float);
        float3 _BranchOnInputConnection_6a7b13b3cb82474aa187229c3d17a00f_Out_3_Vector3 = _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3_IsConnected ? _Property_031663d8c32f41ec8c5aa64dfd664823_Out_0_Vector3 : _MainLight_6570bf88718b46ebb6bd80eec408287a_Direction_1_Vector3;
        Bindings_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float _HalfAngle_f48886360d2649d8b7540e6fb3eef669;
        half3 _HalfAngle_f48886360d2649d8b7540e6fb3eef669_Out_1_Vector3;
        SG_HalfAngle_e6f25cf5cc5b2cc4da597068c1610d98_float(IN.WorldSpaceViewDirection, _BranchOnInputConnection_6a7b13b3cb82474aa187229c3d17a00f_Out_3_Vector3, _HalfAngle_f48886360d2649d8b7540e6fb3eef669, _HalfAngle_f48886360d2649d8b7540e6fb3eef669_Out_1_Vector3);
        float3 _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3 = _NormalWS;
        bool _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3_IsConnected = _NormalWS_5a3c9a3a7faa491894a42d170b5bfeb5_IsConnected;
        float3 _BranchOnInputConnection_72430741d0e04d2dbf5368b624a090cc_Out_3_Vector3 = _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3_IsConnected ? _Property_801dfbee5fa540ac80af739a98535520_Out_0_Vector3 : IN.WorldSpaceNormal;
        float _DotProduct_4558c6edcb084d359ac8ee4b2934ea05_Out_2_Float;
        Unity_DotProduct_float3(_HalfAngle_f48886360d2649d8b7540e6fb3eef669_Out_1_Vector3, _BranchOnInputConnection_72430741d0e04d2dbf5368b624a090cc_Out_3_Vector3, _DotProduct_4558c6edcb084d359ac8ee4b2934ea05_Out_2_Float);
        float _Saturate_23d3962f416c4150a990dcb36b5ecbc4_Out_1_Float;
        Unity_Saturate_float(_DotProduct_4558c6edcb084d359ac8ee4b2934ea05_Out_2_Float, _Saturate_23d3962f416c4150a990dcb36b5ecbc4_Out_1_Float);
        float _Property_f4ccf6ae090a4694bb78a2cef88028e0_Out_0_Float = _Smoothness;
        Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9;
        half _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9_SpecPower_1_Float;
        SG_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float(_Property_f4ccf6ae090a4694bb78a2cef88028e0_Out_0_Float, _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9, _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9_SpecPower_1_Float);
        float _Power_b652c8bd47c44415bf7733f381883bf3_Out_2_Float;
        Unity_Power_float(_Saturate_23d3962f416c4150a990dcb36b5ecbc4_Out_1_Float, _SmoothnessToSpecPower_20ef94060ea843a582e3dfe7e1761de9_SpecPower_1_Float, _Power_b652c8bd47c44415bf7733f381883bf3_Out_2_Float);
        float _Property_1fcbde0798cd43628cbb75583e5d6e7a_Out_0_Float = _Smoothness;
        float _Multiply_8e72dd78784942f5ac8322f3055fd0ed_Out_2_Float;
        Unity_Multiply_float_float(_Power_b652c8bd47c44415bf7733f381883bf3_Out_2_Float, _Property_1fcbde0798cd43628cbb75583e5d6e7a_Out_0_Float, _Multiply_8e72dd78784942f5ac8322f3055fd0ed_Out_2_Float);
        float3 _Property_5b0bef6a4de54859800dd057235a4dbc_Out_0_Vector3 = _Reflectance;
        float3 _Multiply_73cf80666f44441494dd412a147e089c_Out_2_Vector3;
        Unity_Multiply_float3_float3((_Multiply_8e72dd78784942f5ac8322f3055fd0ed_Out_2_Float.xxx), _Property_5b0bef6a4de54859800dd057235a4dbc_Out_0_Vector3, _Multiply_73cf80666f44441494dd412a147e089c_Out_2_Vector3);
        float3 _Multiply_bceb91846ebf445c9093523786845bb5_Out_2_Vector3;
        Unity_Multiply_float3_float3(_Multiply_73cf80666f44441494dd412a147e089c_Out_2_Vector3, float3(10, 10, 10), _Multiply_bceb91846ebf445c9093523786845bb5_Out_2_Vector3);
        Specular_1 = _Multiply_bceb91846ebf445c9093523786845bb5_Out_2_Vector3;
        }
        
        // unity-custom-func-begin
        void AddAdditionalLightsSimple_float(float SpecPower, float3 WorldPosition, float3 WorldNormal, float3 WorldView, float MainDiffuse, float3 MainSpecular, float3 MainColor, float3 Reflectance, float2 ScreenPosition, out float Diffuse, out float3 Specular, out float3 Color){
        Diffuse = MainDiffuse;
        
        Specular = MainSpecular;
        
        Color = MainColor * (MainDiffuse + MainSpecular);
        
        
        
        #ifndef SHADERGRAPH_PREVIEW
        
            
        
        #if defined(_ADDITIONAL_LIGHTS) || defined(_CLUSTER_LIGHT_LOOP)
        
        
        
            #if defined(_ADDITIONAL_LIGHTS)
        
                uint pixelLightCount = GetAdditionalLightsCount();
        
            #endif
        
        
        
        #if USE_CLUSTER_LIGHT_LOOP
        
            // for Foward+ LIGHT_LOOP_BEGIN macro uses inputData.normalizedScreenSpaceUV and inputData.positionWS
        
            InputData inputData = (InputData)0;
        
        
        
            inputData.normalizedScreenSpaceUV = ScreenPosition;
        
            inputData.positionWS = WorldPosition;
        
        #endif
        
        
        
            LIGHT_LOOP_BEGIN(pixelLightCount)
        
        		// Call the URP additional light algorithm. This will not calculate shadows, since we don't pass a shadow mask value
        
        		Light light = GetAdditionalLight(lightIndex, WorldPosition);
        
        		// Manually set the shadow attenuation by calculating realtime shadows
        
        		light.shadowAttenuation = AdditionalLightRealtimeShadow(lightIndex, WorldPosition, light.direction);
        
                float NdotL = saturate(dot(WorldNormal, light.direction));
        
                float atten = light.distanceAttenuation * light.shadowAttenuation;
        
                float thisDiffuse = atten * NdotL;
        
                float3 halfAngle = normalize(light.direction + WorldView);
        
                float spec = pow(saturate(dot(halfAngle, WorldNormal)), SpecPower);
        
                float3 thisSpecular = spec * Reflectance * atten;
        
                Diffuse += thisDiffuse;
        
                Specular += thisSpecular;
        
                #if defined(_LIGHT_COOKIES)
        
                    float3 cookieColor = SampleAdditionalLightCookie(lightIndex, WorldPosition);
        
                    light.color *= cookieColor;
        
                #endif
        
                Color += light.color * (thisDiffuse + thisSpecular);
        
            LIGHT_LOOP_END
        
            float total = Diffuse + dot(Specular, float3(0.333, 0.333, 0.333));
        
            Color = total <= 0 ? MainColor : Color / total;
        
        #endif // _ADDITIONAL_LIGHTS || _CLUSTER_LIGHT_LOOP
        
        
        
        #endif // SHADERGRAPH_PREVIEW
        }
        // unity-custom-func-end
        
        struct Bindings_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        };
        
        void SG_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float(float _MainLightDiffuse, float3 _MainLightSpecular, float3 _MainLightColor, float3 _NormalWS, bool _NormalWS_70cbf5ac6da04bf6bd87eb71ccb7c48d_IsConnected, float _Smoothness, float3 _Reflectance, Bindings_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float IN, out float Diffuse_1, out float3 Specular_2, out float3 Color_3)
        {
        float _Property_b9f05025da4f4857a7b1b6f56259a629_Out_0_Float = _Smoothness;
        Bindings_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1;
        half _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1_SpecPower_1_Float;
        SG_SmoothnessToSpecPower_acd07cd8756c57f4cb2dfbed1d358845_float(_Property_b9f05025da4f4857a7b1b6f56259a629_Out_0_Float, _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1, _SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1_SpecPower_1_Float);
        float3 _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3 = _NormalWS;
        bool _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3_IsConnected = _NormalWS_70cbf5ac6da04bf6bd87eb71ccb7c48d_IsConnected;
        float3 _BranchOnInputConnection_d869e3d8654b48a491de945ad8af6301_Out_3_Vector3 = _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3_IsConnected ? _Property_3e48999a139848e6ab2e955c61810b83_Out_0_Vector3 : IN.WorldSpaceNormal;
        float _Property_25880f0697234954b8dc6ef11af3752d_Out_0_Float = _MainLightDiffuse;
        float3 _Property_1e29ad89226c4d84a936fe7530839aef_Out_0_Vector3 = _MainLightSpecular;
        float3 _Property_ac790fc8215b4b3d8851855d2153960d_Out_0_Vector3 = _MainLightColor;
        float3 _Property_eea8eda455d44ae7b30c65f80baac806_Out_0_Vector3 = _Reflectance;
        float4 _ScreenPosition_bb4bf3ece5524d4c898132bd377d7d8b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Diffuse_7_Float;
        float3 _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Specular_8_Vector3;
        float3 _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Color_9_Vector3;
        AddAdditionalLightsSimple_float(_SmoothnessToSpecPower_e4f652b8ee6d4c50a925dbcc702ee7a1_SpecPower_1_Float, IN.WorldSpacePosition, _BranchOnInputConnection_d869e3d8654b48a491de945ad8af6301_Out_3_Vector3, IN.WorldSpaceViewDirection, _Property_25880f0697234954b8dc6ef11af3752d_Out_0_Float, _Property_1e29ad89226c4d84a936fe7530839aef_Out_0_Vector3, _Property_ac790fc8215b4b3d8851855d2153960d_Out_0_Vector3, _Property_eea8eda455d44ae7b30c65f80baac806_Out_0_Vector3, (_ScreenPosition_bb4bf3ece5524d4c898132bd377d7d8b_Out_0_Vector4.xy), _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Diffuse_7_Float, _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Specular_8_Vector3, _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Color_9_Vector3);
        Diffuse_1 = _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Diffuse_7_Float;
        Specular_2 = _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Specular_8_Vector3;
        Color_3 = _AddAdditionalLightsSimpleCustomFunction_af7e463337fa464994ef4fefeb1ef2b0_Color_9_Vector3;
        }
        
        void Unity_Negate_float3(float3 In, out float3 Out)
        {
            Out = -1 * In;
        }
        
        void Unity_Reflection_float3(float3 In, float3 Normal, out float3 Out)
        {
            Out = reflect(In, Normal);
        }
        
        // unity-custom-func-begin
        void URPReflectionProbe_float(float3 positionWS, float3 reflectVector, float2 normalizedScreenSpaceUV, float roughness, float occlusion, out float3 reflection){
        #ifdef SHADERGRAPH_PREVIEW
        
            reflection = float3(0,0,0);
        
        #else
        
            reflection = GlossyEnvironmentReflection(reflectVector, positionWS, roughness, occlusion, normalizedScreenSpaceUV);
        
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        };
        
        void SG_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float(float3 _positionWS, bool _positionWS_d6701bdc1f184a57ac2283491fc460d9_IsConnected, float3 _reflectVector, bool _reflectVector_3e2eb19b69b8469eaf2302c7abc4cbc5_IsConnected, float _smoothness, float _occlusion, Bindings_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float IN, out float3 Out_1)
        {
        float3 _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3 = _positionWS;
        bool _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3_IsConnected = _positionWS_d6701bdc1f184a57ac2283491fc460d9_IsConnected;
        float3 _BranchOnInputConnection_8fb583036b0c4313a1ecd93143939f21_Out_3_Vector3 = _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3_IsConnected ? _Property_b993dfa9c8bb4e4ea4f3cb1768e92822_Out_0_Vector3 : IN.WorldSpacePosition;
        float3 _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3 = _reflectVector;
        bool _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3_IsConnected = _reflectVector_3e2eb19b69b8469eaf2302c7abc4cbc5_IsConnected;
        float3 _Negate_9cf7cea21c5641239fdbcb32480ac39e_Out_1_Vector3;
        Unity_Negate_float3(IN.WorldSpaceViewDirection, _Negate_9cf7cea21c5641239fdbcb32480ac39e_Out_1_Vector3);
        float3 _Reflection_c8689c494aab4411aadc74299549c6cb_Out_2_Vector3;
        Unity_Reflection_float3(_Negate_9cf7cea21c5641239fdbcb32480ac39e_Out_1_Vector3, IN.WorldSpaceNormal, _Reflection_c8689c494aab4411aadc74299549c6cb_Out_2_Vector3);
        float3 _BranchOnInputConnection_9600230d09794702a61c1a01f8e842a5_Out_3_Vector3 = _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3_IsConnected ? _Property_27869743c4c14e898d5c15f6fdd4e044_Out_0_Vector3 : _Reflection_c8689c494aab4411aadc74299549c6cb_Out_2_Vector3;
        float4 _ScreenPosition_270e438746a9466e8aaf01f4903f62fb_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float _Property_9012e47da801473d8ef85a4092281eb2_Out_0_Float = _smoothness;
        float _OneMinus_b2508f25afb44017ba3480edc35cf631_Out_1_Float;
        Unity_OneMinus_float(_Property_9012e47da801473d8ef85a4092281eb2_Out_0_Float, _OneMinus_b2508f25afb44017ba3480edc35cf631_Out_1_Float);
        float _Property_d602b1723845462cbf00324de1e9e82a_Out_0_Float = _occlusion;
        float3 _URPReflectionProbeCustomFunction_7233d098cd214f55baf898d486bfdf4b_reflection_4_Vector3;
        URPReflectionProbe_float(_BranchOnInputConnection_8fb583036b0c4313a1ecd93143939f21_Out_3_Vector3, _BranchOnInputConnection_9600230d09794702a61c1a01f8e842a5_Out_3_Vector3, (_ScreenPosition_270e438746a9466e8aaf01f4903f62fb_Out_0_Vector4.xy), _OneMinus_b2508f25afb44017ba3480edc35cf631_Out_1_Float, _Property_d602b1723845462cbf00324de1e9e82a_Out_0_Float, _URPReflectionProbeCustomFunction_7233d098cd214f55baf898d486bfdf4b_reflection_4_Vector3);
        Out_1 = _URPReflectionProbeCustomFunction_7233d098cd214f55baf898d486bfdf4b_reflection_4_Vector3;
        }
        
        // unity-custom-func-begin
        void SSAO_float(float2 normalizedScreenSpaceUV, out float indirectAmbientOcclusion, out float directAmbientOcclusion){
        #if defined(_SCREEN_SPACE_OCCLUSION) && !defined(_SURFACE_TYPE_TRANSPARENT) && !defined(SHADERGRAPH_PREVIEW)
        
            float ssao = saturate(SampleAmbientOcclusion(normalizedScreenSpaceUV) + (1.0 - _AmbientOcclusionParam.x));
        
            indirectAmbientOcclusion = ssao;
        
            directAmbientOcclusion = lerp(half(1.0), ssao, _AmbientOcclusionParam.w);
        
        #else
        
            directAmbientOcclusion = half(1.0);
        
            indirectAmbientOcclusion = half(1.0);
        
        #endif
        }
        // unity-custom-func-end
        
        struct Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float
        {
        float2 NDCPosition;
        };
        
        void SG_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float(Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float IN, out float indirectAO_1, out float directAO_2)
        {
        float4 _ScreenPosition_0fdc511287e14fd48ca909caba575383_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_indirectAmbientOcclusion_0_Float;
        float _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_directAmbientOcclusion_1_Float;
        SSAO_float((_ScreenPosition_0fdc511287e14fd48ca909caba575383_Out_0_Vector4.xy), _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_indirectAmbientOcclusion_0_Float, _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_directAmbientOcclusion_1_Float);
        indirectAO_1 = _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_indirectAmbientOcclusion_0_Float;
        directAO_2 = _SSAOCustomFunction_0f48e52099d04b51a6b1dc52cce50d39_directAmbientOcclusion_1_Float;
        }
        
        void Unity_Minimum_float(float A, float B, out float Out)
        {
            Out = min(A, B);
        };
        
        struct Bindings_AmbientURP_300875fdd653fe340b08ad1547984cf1_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceViewDirection;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_AmbientURP_300875fdd653fe340b08ad1547984cf1_float(float3 _Base_Color, float3 _NormalWS, bool _NormalWS_3a565a44841d4b729f8e86b08d09299c_IsConnected, float _Metallic, float _Smoothness, float3 _Reflectance, float _Ambient_Occlusion, Bindings_AmbientURP_300875fdd653fe340b08ad1547984cf1_float IN, out float3 Ambient_1, out float DirectAO_2)
        {
        float3 _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3 = _NormalWS;
        bool _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3_IsConnected = _NormalWS_3a565a44841d4b729f8e86b08d09299c_IsConnected;
        float3 _BranchOnInputConnection_5e35c0fc2eee4cf7ae47da45409fa2a7_Out_3_Vector3 = _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3_IsConnected ? _Property_d72d925c86ff4010a968b702f0972f69_Out_0_Vector3 : IN.WorldSpaceNormal;
        float3 _BakedGI_1ac35076ff2349f99fec2cef2550ff2d_Out_1_Vector3 = SHADERGRAPH_BAKED_GI(IN.WorldSpacePosition, _BranchOnInputConnection_5e35c0fc2eee4cf7ae47da45409fa2a7_Out_3_Vector3, IN.PixelPosition.xy, IN.uv1.xy, IN.uv2.xy, true);
        float3 _Property_5fb17e215f49424cb9cc9d0806f3f47d_Out_0_Vector3 = _Base_Color;
        float _Property_f995d8544fdb448d85ac845c7bdee967_Out_0_Float = _Metallic;
        float3 _Lerp_842ba4fcd0cf48a3afa108eecd7d56a8_Out_3_Vector3;
        Unity_Lerp_float3(_Property_5fb17e215f49424cb9cc9d0806f3f47d_Out_0_Vector3, float3(0, 0, 0), (_Property_f995d8544fdb448d85ac845c7bdee967_Out_0_Float.xxx), _Lerp_842ba4fcd0cf48a3afa108eecd7d56a8_Out_3_Vector3);
        float3 _Multiply_48ca9faac6354eefabc65eeb591f3fc4_Out_2_Vector3;
        Unity_Multiply_float3_float3(_BakedGI_1ac35076ff2349f99fec2cef2550ff2d_Out_1_Vector3, _Lerp_842ba4fcd0cf48a3afa108eecd7d56a8_Out_3_Vector3, _Multiply_48ca9faac6354eefabc65eeb591f3fc4_Out_2_Vector3);
        float3 _Negate_4f98a1dfbc4d4bd1abe58f406453303f_Out_1_Vector3;
        Unity_Negate_float3(IN.WorldSpaceViewDirection, _Negate_4f98a1dfbc4d4bd1abe58f406453303f_Out_1_Vector3);
        float3 _Reflection_710a69cb22b745929e6bb9b84d11de2f_Out_2_Vector3;
        Unity_Reflection_float3(_Negate_4f98a1dfbc4d4bd1abe58f406453303f_Out_1_Vector3, _BranchOnInputConnection_5e35c0fc2eee4cf7ae47da45409fa2a7_Out_3_Vector3, _Reflection_710a69cb22b745929e6bb9b84d11de2f_Out_2_Vector3);
        float _Property_8c3e921b9cb34f7b82d2a71254653c09_Out_0_Float = _Smoothness;
        Bindings_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.WorldSpaceNormal = IN.WorldSpaceNormal;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.WorldSpacePosition = IN.WorldSpacePosition;
        _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08.NDCPosition = IN.NDCPosition;
        float3 _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08_Out_1_Vector3;
        SG_SampleReflectionProbes_f70e1da6fb6a720439a85a6a2c814a87_float(half3 (0, 0, 0), false, _Reflection_710a69cb22b745929e6bb9b84d11de2f_Out_2_Vector3, true, _Property_8c3e921b9cb34f7b82d2a71254653c09_Out_0_Float, half(1), _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08, _SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08_Out_1_Vector3);
        float3 _Property_2ddaa58bd1e94d0b8508ce91ad39fa39_Out_0_Vector3 = _Reflectance;
        float3 _Multiply_40be6c08ca2f44ef99b6a2b81955d62c_Out_2_Vector3;
        Unity_Multiply_float3_float3(_SampleReflectionProbes_50169e69b46a45469255f5e1768e7f08_Out_1_Vector3, _Property_2ddaa58bd1e94d0b8508ce91ad39fa39_Out_0_Vector3, _Multiply_40be6c08ca2f44ef99b6a2b81955d62c_Out_2_Vector3);
        float3 _Add_c0a57a54eb8b4419a7e907a65e6556a7_Out_2_Vector3;
        Unity_Add_float3(_Multiply_48ca9faac6354eefabc65eeb591f3fc4_Out_2_Vector3, _Multiply_40be6c08ca2f44ef99b6a2b81955d62c_Out_2_Vector3, _Add_c0a57a54eb8b4419a7e907a65e6556a7_Out_2_Vector3);
        float _Property_5a996c5d941d46019b3ac5ada526c475_Out_0_Float = _Ambient_Occlusion;
        Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e;
        _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e.NDCPosition = IN.NDCPosition;
        half _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_indirectAO_1_Float;
        half _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_directAO_2_Float;
        SG_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float(_ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_indirectAO_1_Float, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_directAO_2_Float);
        float _Minimum_699e1643e9974baa97f6f21c8f949d09_Out_2_Float;
        Unity_Minimum_float(_Property_5a996c5d941d46019b3ac5ada526c475_Out_0_Float, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_indirectAO_1_Float, _Minimum_699e1643e9974baa97f6f21c8f949d09_Out_2_Float);
        float3 _Multiply_ccaf4dfa5cdb4d9fae7fd92dc7d512da_Out_2_Vector3;
        Unity_Multiply_float3_float3(_Add_c0a57a54eb8b4419a7e907a65e6556a7_Out_2_Vector3, (_Minimum_699e1643e9974baa97f6f21c8f949d09_Out_2_Float.xxx), _Multiply_ccaf4dfa5cdb4d9fae7fd92dc7d512da_Out_2_Vector3);
        float _Minimum_c4dcab6b18b34dde987e68f86d22aed7_Out_2_Float;
        Unity_Minimum_float(_Property_5a996c5d941d46019b3ac5ada526c475_Out_0_Float, _ScreenSpaceAmbientOcclusion_61ac988e39cc4471b438c57e0592da7e_directAO_2_Float, _Minimum_c4dcab6b18b34dde987e68f86d22aed7_Out_2_Float);
        Ambient_1 = _Multiply_ccaf4dfa5cdb4d9fae7fd92dc7d512da_Out_2_Vector3;
        DirectAO_2 = _Minimum_c4dcab6b18b34dde987e68f86d22aed7_Out_2_Float;
        }
        
        void Unity_Fog_float(out float4 Color, out float Density, float3 Position)
        {
            SHADERGRAPH_FOG(Position, Color, Density);
        }
        
        struct Bindings_Fog_286ae83400099a24bba6faf005588be7_float
        {
        float3 ObjectSpacePosition;
        };
        
        void SG_Fog_286ae83400099a24bba6faf005588be7_float(float3 _In, Bindings_Fog_286ae83400099a24bba6faf005588be7_float IN, out float3 Out_1)
        {
        float3 _Property_626923dc627443639da97776de7dcc22_Out_0_Vector3 = _In;
        float4 _Fog_acabe3c84c9549f3880ce7d106150576_Color_0_Vector4;
        float _Fog_acabe3c84c9549f3880ce7d106150576_Density_1_Float;
        Unity_Fog_float(_Fog_acabe3c84c9549f3880ce7d106150576_Color_0_Vector4, _Fog_acabe3c84c9549f3880ce7d106150576_Density_1_Float, IN.ObjectSpacePosition);
        float3 _Lerp_9cfca9aa08c7423bab62b39a01237d64_Out_3_Vector3;
        Unity_Lerp_float3(_Property_626923dc627443639da97776de7dcc22_Out_0_Vector3, (_Fog_acabe3c84c9549f3880ce7d106150576_Color_0_Vector4.xyz), (_Fog_acabe3c84c9549f3880ce7d106150576_Density_1_Float.xxx), _Lerp_9cfca9aa08c7423bab62b39a01237d64_Out_3_Vector3);
        Out_1 = _Lerp_9cfca9aa08c7423bab62b39a01237d64_Out_3_Vector3;
        }
        
        void Unity_Saturate_float3(float3 In, out float3 Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Saturation_float(float3 In, float Saturation, out float3 Out)
        {
            float luma = dot(In, float3(0.2126729, 0.7151522, 0.0721750));
            Out =  luma.xxx + Saturation.xxx * (In - luma.xxx);
        }
        
        void Unity_Contrast_float(float3 In, float Contrast, out float3 Out)
        {
            float midpoint = pow(0.5, 2.2);
            Out =  (In - midpoint) * Contrast + midpoint;
        }
        
        void Unity_Remap_float3(float3 In, float2 InMinMax, float2 OutMinMax, out float3 Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        struct Bindings_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpaceViewDirection;
        float3 ObjectSpacePosition;
        float3 WorldSpacePosition;
        float3 AbsoluteWorldSpacePosition;
        float2 NDCPosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float(float3 _Base_Color, float3 _Normal, bool _Normal_e1611e545480449d80aa5a0e7c2b63c4_IsConnected, float _Metallic, float _Smoothness, float _Micro_Occlusion, float _Ambient_Occlusion, float4 _Color_A, float2 _Color_A_Location, float4 _Color_B, float2 _Color_B_Location, float4 _Color_C, Bindings_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float IN, out float3 Lit_1)
        {
        float4 _Property_bedee92e97ce4b2abff5524ce019b2a8_Out_0_Vector4 = _Color_A;
        float4 _Property_c648ff794fd34283beff09e33d8293fc_Out_0_Vector4 = _Color_B;
        float3 _Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3 = _Normal;
        bool _Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3_IsConnected = _Normal_e1611e545480449d80aa5a0e7c2b63c4_IsConnected;
        float3 _Transform_75d5405941a444c088600c650a7269f0_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_75d5405941a444c088600c650a7269f0_Out_1_Vector3 = TransformTangentToWorld(_Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float3 _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3 = _Property_dbe43d633d7c40db91271fec54a22ee2_Out_0_Vector3_IsConnected ? _Transform_75d5405941a444c088600c650a7269f0_Out_1_Vector3 : IN.WorldSpaceNormal;
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float _MainLight_d918d0814080438585a810ba0b8afeb4;
        _MainLight_d918d0814080438585a810ba0b8afeb4.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3;
        half3 _MainLight_d918d0814080438585a810ba0b8afeb4_Color_2_Vector3;
        half _MainLight_d918d0814080438585a810ba0b8afeb4_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(_MainLight_d918d0814080438585a810ba0b8afeb4, _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3, _MainLight_d918d0814080438585a810ba0b8afeb4_Color_2_Vector3, _MainLight_d918d0814080438585a810ba0b8afeb4_ShadowAtten_3_Float);
        Bindings_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half _DiffuseLambert_7f9e988376a2438ebc87097469e065d3;
        _DiffuseLambert_7f9e988376a2438ebc87097469e065d3.WorldSpaceNormal = IN.WorldSpaceNormal;
        _DiffuseLambert_7f9e988376a2438ebc87097469e065d3.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half _DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float;
        SG_DiffuseLambert_cb5cb9cef826e9f448011787f0d627b2_half(_BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3, true, _DiffuseLambert_7f9e988376a2438ebc87097469e065d3, _DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float);
        float _Property_ac10139ecedb4301b4595fa5b13c00b8_Out_0_Float = _Smoothness;
        float3 _Property_58fa7de1b7784467935169b8914ee373_Out_0_Vector3 = _Base_Color;
        float _Property_433019e2d18944a2909e58d06f7cc1ec_Out_0_Float = _Metallic;
        float _Property_dd950e92d2d54fefbb89aaa0d1f6b713_Out_0_Float = _Smoothness;
        float _Property_dce33fc56d0e4204bd34d323af11f8ca_Out_0_Float = _Micro_Occlusion;
        float _Multiply_d120aee0dde541e69ff32688ddb2bee0_Out_2_Float;
        Unity_Multiply_float_float(_Property_dce33fc56d0e4204bd34d323af11f8ca_Out_0_Float, 0.5, _Multiply_d120aee0dde541e69ff32688ddb2bee0_Out_2_Float);
        float _Lerp_6742a96ab4984154ae535aef42b348f7_Out_3_Float;
        Unity_Lerp_float(float(0), float(0.08), _Multiply_d120aee0dde541e69ff32688ddb2bee0_Out_2_Float, _Lerp_6742a96ab4984154ae535aef42b348f7_Out_3_Float);
        Bindings_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d;
        _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d.WorldSpaceNormal = IN.WorldSpaceNormal;
        _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        half3 _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3;
        SG_ReflectanceURP_57a8ea7c8f931c941b2bb57aedc451aa_float(_Property_58fa7de1b7784467935169b8914ee373_Out_0_Vector3, _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_433019e2d18944a2909e58d06f7cc1ec_Out_0_Float, _Property_dd950e92d2d54fefbb89aaa0d1f6b713_Out_0_Float, _Lerp_6742a96ab4984154ae535aef42b348f7_Out_3_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3);
        Bindings_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3;
        _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3.WorldSpaceNormal = IN.WorldSpaceNormal;
        _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3_Specular_1_Vector3;
        SG_SpecularBlinn_b5f627370f568984bb4be446b9f54d15_float(_BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_ac10139ecedb4301b4595fa5b13c00b8_Out_0_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3, _MainLight_d918d0814080438585a810ba0b8afeb4_Direction_1_Vector3, true, _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3, _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3_Specular_1_Vector3);
        float3 _Multiply_7b7e19c594534a458849928b4dc06aec_Out_2_Vector3;
        Unity_Multiply_float3_float3((_DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float.xxx), _SpecularBlinn_f0a5657246a6403b839cfc5b3396ddb3_Specular_1_Vector3, _Multiply_7b7e19c594534a458849928b4dc06aec_Out_2_Vector3);
        Bindings_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e;
        _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        half3 _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Direction_1_Vector3;
        half3 _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Color_2_Vector3;
        half _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_ShadowAtten_3_Float;
        SG_MainLight_f97aba6a33b86e5498cdd202ba0c9313_float(_MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e, _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Direction_1_Vector3, _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Color_2_Vector3, _MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_ShadowAtten_3_Float);
        float3 _Multiply_9545e2014858428792b92c57dd77e45c_Out_2_Vector3;
        Unity_Multiply_float3_float3(_MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_Color_2_Vector3, (_MainLight_7a0edc4a8be9421ea8d3ff96cbc83a8e_ShadowAtten_3_Float.xxx), _Multiply_9545e2014858428792b92c57dd77e45c_Out_2_Vector3);
        float _Property_be391ee1d2f24bada2da9fc9d603f6a9_Out_0_Float = _Smoothness;
        Bindings_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.WorldSpaceNormal = IN.WorldSpaceNormal;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.WorldSpacePosition = IN.WorldSpacePosition;
        _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e.NDCPosition = IN.NDCPosition;
        half _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Diffuse_1_Float;
        half3 _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Specular_2_Vector3;
        half3 _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Color_3_Vector3;
        SG_AdditionalLightsSimple_52f2243396571e9449f18f8b5f237d00_float(_DiffuseLambert_7f9e988376a2438ebc87097469e065d3_Diffuse_1_Float, _Multiply_7b7e19c594534a458849928b4dc06aec_Out_2_Vector3, _Multiply_9545e2014858428792b92c57dd77e45c_Out_2_Vector3, _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_be391ee1d2f24bada2da9fc9d603f6a9_Out_0_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Diffuse_1_Float, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Specular_2_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Color_3_Vector3);
        float3 _Property_8804fec07c534721b9d4e6def9182fad_Out_0_Vector3 = _Base_Color;
        float3 _Multiply_765841b19beb4cecb786b3295b6aa8e9_Out_2_Vector3;
        Unity_Multiply_float3_float3((_AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Diffuse_1_Float.xxx), _Property_8804fec07c534721b9d4e6def9182fad_Out_0_Vector3, _Multiply_765841b19beb4cecb786b3295b6aa8e9_Out_2_Vector3);
        float3 _Add_77980781d2204e1b8d249e5f7058e9fb_Out_2_Vector3;
        Unity_Add_float3(_Multiply_765841b19beb4cecb786b3295b6aa8e9_Out_2_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Specular_2_Vector3, _Add_77980781d2204e1b8d249e5f7058e9fb_Out_2_Vector3);
        float3 _Multiply_9df1a4a5bf654e97b945b2eccf2fc855_Out_2_Vector3;
        Unity_Multiply_float3_float3(_Add_77980781d2204e1b8d249e5f7058e9fb_Out_2_Vector3, _AdditionalLightsSimple_08ea73f041c5480c9a2b2c41f49e809e_Color_3_Vector3, _Multiply_9df1a4a5bf654e97b945b2eccf2fc855_Out_2_Vector3);
        float3 _Property_e9725e93976b4fcaa5fea397628348dd_Out_0_Vector3 = _Base_Color;
        float _Property_e926bef11147490b98b69d5bec06eaa9_Out_0_Float = _Metallic;
        float _Property_1465a416fe734e4e83f2401b9c4d3fdb_Out_0_Float = _Smoothness;
        float _Property_0a056a259612407e813453c548affc50_Out_0_Float = _Ambient_Occlusion;
        Bindings_AmbientURP_300875fdd653fe340b08ad1547984cf1_float _AmbientURP_46e1712500da4aae848bd5b24a05f29f;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.WorldSpaceNormal = IN.WorldSpaceNormal;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.WorldSpacePosition = IN.WorldSpacePosition;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.NDCPosition = IN.NDCPosition;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.PixelPosition = IN.PixelPosition;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.uv1 = IN.uv1;
        _AmbientURP_46e1712500da4aae848bd5b24a05f29f.uv2 = IN.uv2;
        half3 _AmbientURP_46e1712500da4aae848bd5b24a05f29f_Ambient_1_Vector3;
        half _AmbientURP_46e1712500da4aae848bd5b24a05f29f_DirectAO_2_Float;
        SG_AmbientURP_300875fdd653fe340b08ad1547984cf1_float(_Property_e9725e93976b4fcaa5fea397628348dd_Out_0_Vector3, _BranchOnInputConnection_b3c5feecd28b43998390fb43ac1592b1_Out_3_Vector3, true, _Property_e926bef11147490b98b69d5bec06eaa9_Out_0_Float, _Property_1465a416fe734e4e83f2401b9c4d3fdb_Out_0_Float, _ReflectanceURP_04ca83d0da3b469cb9b8a8c5c2b3451d_Reflectance_1_Vector3, _Property_0a056a259612407e813453c548affc50_Out_0_Float, _AmbientURP_46e1712500da4aae848bd5b24a05f29f, _AmbientURP_46e1712500da4aae848bd5b24a05f29f_Ambient_1_Vector3, _AmbientURP_46e1712500da4aae848bd5b24a05f29f_DirectAO_2_Float);
        float3 _Add_ad0b3eccabf24dceb374546c3c332ad7_Out_2_Vector3;
        Unity_Add_float3(_Multiply_9df1a4a5bf654e97b945b2eccf2fc855_Out_2_Vector3, _AmbientURP_46e1712500da4aae848bd5b24a05f29f_Ambient_1_Vector3, _Add_ad0b3eccabf24dceb374546c3c332ad7_Out_2_Vector3);
        Bindings_Fog_286ae83400099a24bba6faf005588be7_float _Fog_f4025f6ca9e74f948bc7263ef71d324a;
        _Fog_f4025f6ca9e74f948bc7263ef71d324a.ObjectSpacePosition = IN.ObjectSpacePosition;
        half3 _Fog_f4025f6ca9e74f948bc7263ef71d324a_Out_1_Vector3;
        SG_Fog_286ae83400099a24bba6faf005588be7_float(_Add_ad0b3eccabf24dceb374546c3c332ad7_Out_2_Vector3, _Fog_f4025f6ca9e74f948bc7263ef71d324a, _Fog_f4025f6ca9e74f948bc7263ef71d324a_Out_1_Vector3);
        float3 _Saturate_b798de6279164a2b9155cd8251d26092_Out_1_Vector3;
        Unity_Saturate_float3(_Fog_f4025f6ca9e74f948bc7263ef71d324a_Out_1_Vector3, _Saturate_b798de6279164a2b9155cd8251d26092_Out_1_Vector3);
        float3 _Saturation_ad4a69e303864b54940190442ea5d857_Out_2_Vector3;
        Unity_Saturation_float(_Saturate_b798de6279164a2b9155cd8251d26092_Out_1_Vector3, float(0), _Saturation_ad4a69e303864b54940190442ea5d857_Out_2_Vector3);
        float _Swizzle_bae47f72b28a4941b7665012b9c55203_Out_1_Float = (_Saturation_ad4a69e303864b54940190442ea5d857_Out_2_Vector3).x.x;
        float _Power_83030291203f43f9b048356e4fbaf99e_Out_2_Float;
        Unity_Power_float(_Swizzle_bae47f72b28a4941b7665012b9c55203_Out_1_Float, float(0.45), _Power_83030291203f43f9b048356e4fbaf99e_Out_2_Float);
        float3 _Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3;
        Unity_Contrast_float((_Power_83030291203f43f9b048356e4fbaf99e_Out_2_Float.xxx), float(2), _Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3);
        float2 _Property_7fc6298ec06c474b99191c5a5156da72_Out_0_Vector2 = _Color_A_Location;
        float3 _Remap_6d4565d12b3a4f71b23e18189b7389b3_Out_3_Vector3;
        Unity_Remap_float3(_Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3, _Property_7fc6298ec06c474b99191c5a5156da72_Out_0_Vector2, float2 (0, 1), _Remap_6d4565d12b3a4f71b23e18189b7389b3_Out_3_Vector3);
        float3 _Saturate_2728e5e4642645239b2f9a703cc1622a_Out_1_Vector3;
        Unity_Saturate_float3(_Remap_6d4565d12b3a4f71b23e18189b7389b3_Out_3_Vector3, _Saturate_2728e5e4642645239b2f9a703cc1622a_Out_1_Vector3);
        float3 _Lerp_20eaa2d06da4491384cac8b19ba49f2c_Out_3_Vector3;
        Unity_Lerp_float3((_Property_bedee92e97ce4b2abff5524ce019b2a8_Out_0_Vector4.xyz), (_Property_c648ff794fd34283beff09e33d8293fc_Out_0_Vector4.xyz), _Saturate_2728e5e4642645239b2f9a703cc1622a_Out_1_Vector3, _Lerp_20eaa2d06da4491384cac8b19ba49f2c_Out_3_Vector3);
        float4 _Property_fd1357d1b32e4a29a4cbcc3613f244e3_Out_0_Vector4 = _Color_C;
        float2 _Property_c2df701a69af4187a31c6f8cfcb26846_Out_0_Vector2 = _Color_B_Location;
        float3 _Remap_0c80541bb2da437698e33d379210a687_Out_3_Vector3;
        Unity_Remap_float3(_Contrast_0db41761b27d4cc3a3c8a94b9adad1d6_Out_2_Vector3, _Property_c2df701a69af4187a31c6f8cfcb26846_Out_0_Vector2, float2 (0, 1), _Remap_0c80541bb2da437698e33d379210a687_Out_3_Vector3);
        float3 _Saturate_5b42e55c7d0c45f1a92ce0c1c045003a_Out_1_Vector3;
        Unity_Saturate_float3(_Remap_0c80541bb2da437698e33d379210a687_Out_3_Vector3, _Saturate_5b42e55c7d0c45f1a92ce0c1c045003a_Out_1_Vector3);
        float3 _Lerp_b1cbf1c8cc0549a69912223d40ba4cfa_Out_3_Vector3;
        Unity_Lerp_float3(_Lerp_20eaa2d06da4491384cac8b19ba49f2c_Out_3_Vector3, (_Property_fd1357d1b32e4a29a4cbcc3613f244e3_Out_0_Vector4.xyz), _Saturate_5b42e55c7d0c45f1a92ce0c1c045003a_Out_1_Vector3, _Lerp_b1cbf1c8cc0549a69912223d40ba4cfa_Out_3_Vector3);
        Lit_1 = _Lerp_b1cbf1c8cc0549a69912223d40ba4cfa_Out_3_Vector3;
        }
        
        // unity-custom-func-begin
        void DebugMaterialSwitch_float(float3 None, float3 Albedo, float3 Specular, float3 Alpha, float3 Smoothness, float3 AmbientOcclusion, float3 Emission, float3 NormalWS, float3 NormalTS, float3 LightComplexity, float3 Metallic, float3 SpriteMask, float3 RenderingLayerMasks, out float3 Out){
        Out = None;
        #if !defined(SHADERGRAPH_PREVIEW) && defined(DEBUG_DISPLAY)
        [branch] switch(int(_DebugMaterialMode))
        
        {
        
            case 0:
        
                Out = None; break;
        
            case 1:
        
                Out = Albedo; break;
        
            case 2:
        
                Out = Specular; break;
            case 3:
        
                Out = Alpha; break;
            case 4:
        
                Out = Smoothness; break;
            case 5:
        
                Out = AmbientOcclusion;  break;
            case 6:
        
                Out = Emission;  break;
            case 7:
        
                Out = NormalWS * 0.5 + 0.5;  break;
            case 8:
        
                Out = NormalTS * 0.5 + 0.5;  break;
            case 9:
        
                Out = LightComplexity;  break;
            case 10:
        
                Out = Metallic;  break;
            case 11:
        
                Out = SpriteMask;  break;
            case 12:
        
                Out = RenderingLayerMasks;  break;
        
            default:
        
                Out = None; break;
        
        }
        #endif
        
        // Disable this define to prevent the global unlit
        // fragment pass to override the color output again.
        #undef DEBUG_DISPLAY
        }
        // unity-custom-func-end
        
        struct Bindings_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpacePosition;
        float2 NDCPosition;
        };
        
        void SG_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float(float3 _In, float3 _Base_Color, float3 _Normal, float _Metallic, float _Smoothness, float3 _Emission, float _Ambient_Occlusion, float _Alpha, Bindings_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float IN, out float3 Out_1)
        {
        float3 _Property_dd011cc96ae64d1181317986b1fa1742_Out_0_Vector3 = _In;
        float3 _Property_5653941ce5a641f18f7ce7012652025d_Out_0_Vector3 = _Base_Color;
        float _Property_45f5c13ff5544581bd61c2442cecd0a1_Out_0_Float = _Alpha;
        float _Property_b6c8b448c5324bd3bc59540f628e43a3_Out_0_Float = _Smoothness;
        Bindings_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5;
        _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5.NDCPosition = IN.NDCPosition;
        half _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_indirectAO_1_Float;
        half _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_directAO_2_Float;
        SG_ScreenSpaceAmbientOcclusion_616a08cdf385c2d4997a5ed9d01e16a8_float(_ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5, _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_indirectAO_1_Float, _ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_directAO_2_Float);
        float _Property_441143660ff642349088dd1bcab6bc78_Out_0_Float = _Ambient_Occlusion;
        float _Minimum_8ee95b9bf7ac4776b6ee4edf1214b3c1_Out_2_Float;
        Unity_Minimum_float(_ScreenSpaceAmbientOcclusion_40f694c4c3ca4e89acb75242921983f5_indirectAO_1_Float, _Property_441143660ff642349088dd1bcab6bc78_Out_0_Float, _Minimum_8ee95b9bf7ac4776b6ee4edf1214b3c1_Out_2_Float);
        float3 _Property_b171431b5a3b4b0a9fc9fdede4a532a7_Out_0_Vector3 = _Emission;
        float3 _Property_db9eb36ed51d4aad95e383920b55e3d7_Out_0_Vector3 = _Normal;
        float3 _Transform_9e831bda1f4d4495b24f1f6e3075f2fb_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_9e831bda1f4d4495b24f1f6e3075f2fb_Out_1_Vector3 = TransformTangentToWorld(_Property_db9eb36ed51d4aad95e383920b55e3d7_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float3 _Property_4eaab22b2b784aeda3752622f7abaf85_Out_0_Vector3 = _Normal;
        float4 _ScreenPosition_121436dfdd464829910775b2326b046b_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
        float3 _Property_1b1e0a48277e4883afeb1289a075c5d8_Out_0_Vector3 = _Base_Color;
        float3 _LightingComplexityCustomFunction_cbe0c0f96f9046a584e17ead8c001a55_Out_3_Vector3;
        LightingComplexity_float((_ScreenPosition_121436dfdd464829910775b2326b046b_Out_0_Vector4.xy), IN.WorldSpacePosition, _Property_1b1e0a48277e4883afeb1289a075c5d8_Out_0_Vector3, _LightingComplexityCustomFunction_cbe0c0f96f9046a584e17ead8c001a55_Out_3_Vector3);
        float _Property_dcd3ca7796af45c6857884fa7979898b_Out_0_Float = _Metallic;
        float3 _DebugMaterialSwitchCustomFunction_e1fabc2a3bcd4bc183f0e93c379657d4_Out_5_Vector3;
        DebugMaterialSwitch_float(_Property_dd011cc96ae64d1181317986b1fa1742_Out_0_Vector3, _Property_5653941ce5a641f18f7ce7012652025d_Out_0_Vector3, float3 (0, 0, 0), (_Property_45f5c13ff5544581bd61c2442cecd0a1_Out_0_Float.xxx), (_Property_b6c8b448c5324bd3bc59540f628e43a3_Out_0_Float.xxx), (_Minimum_8ee95b9bf7ac4776b6ee4edf1214b3c1_Out_2_Float.xxx), _Property_b171431b5a3b4b0a9fc9fdede4a532a7_Out_0_Vector3, _Transform_9e831bda1f4d4495b24f1f6e3075f2fb_Out_1_Vector3, _Property_4eaab22b2b784aeda3752622f7abaf85_Out_0_Vector3, _LightingComplexityCustomFunction_cbe0c0f96f9046a584e17ead8c001a55_Out_3_Vector3, (_Property_dcd3ca7796af45c6857884fa7979898b_Out_0_Float.xxx), float3 (0, 0, 0), float3 (0, 0, 0), _DebugMaterialSwitchCustomFunction_e1fabc2a3bcd4bc183f0e93c379657d4_Out_5_Vector3);
        Out_1 = _DebugMaterialSwitchCustomFunction_e1fabc2a3bcd4bc183f0e93c379657d4_Out_5_Vector3;
        }
        
        struct Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float
        {
        float3 WorldSpaceNormal;
        float3 WorldSpaceTangent;
        float3 WorldSpaceBiTangent;
        float3 WorldSpaceViewDirection;
        float3 ObjectSpacePosition;
        float3 WorldSpacePosition;
        float3 AbsoluteWorldSpacePosition;
        float2 NDCPosition;
        float2 PixelPosition;
        half4 uv1;
        half4 uv2;
        };
        
        void SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float(float3 _Base_Color, float3 _Normal, float _Metallic, float _Smoothness, float3 _Emission, float _AmbientOcclusion, float _Alpha, float4 _Color_A, float2 _Color_A_Location, float4 _Color_B, float2 _Color_B_Location, float4 _Color_C, Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float IN, out float3 Lit_1)
        {
        float3 _Property_04a055764411443d802bfbbd0d510c65_Out_0_Vector3 = _Base_Color;
        float3 _Property_383a017d83a8420dac016260bc833f58_Out_0_Vector3 = _Normal;
        float3 _Transform_3f94cf9dbe844abc9b6727d5d289074f_Out_1_Vector3;
        {
        float3x3 tangentTransform = float3x3(IN.WorldSpaceTangent, IN.WorldSpaceBiTangent, IN.WorldSpaceNormal);
        _Transform_3f94cf9dbe844abc9b6727d5d289074f_Out_1_Vector3 = TransformTangentToWorld(_Property_383a017d83a8420dac016260bc833f58_Out_0_Vector3.xyz, tangentTransform, true);
        }
        float _Property_11295d868ff34c388c9212b90b781aff_Out_0_Float = _Metallic;
        float _Property_b522b61b85ff4ecbb0eb63cff689f5cb_Out_0_Float = _Smoothness;
        float _Property_a1dc37a47c5640d0870861199df0bd70_Out_0_Float = _AmbientOcclusion;
        Bindings_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float _ApplyDecals_0413903f5da5491d911d117142eabddd;
        _ApplyDecals_0413903f5da5491d911d117142eabddd.PixelPosition = IN.PixelPosition;
        half3 _ApplyDecals_0413903f5da5491d911d117142eabddd_BaseColor_1_Vector3;
        half3 _ApplyDecals_0413903f5da5491d911d117142eabddd_SpecularColor_2_Vector3;
        half3 _ApplyDecals_0413903f5da5491d911d117142eabddd_NormalWS_3_Vector3;
        half _ApplyDecals_0413903f5da5491d911d117142eabddd_Metallic_4_Float;
        half _ApplyDecals_0413903f5da5491d911d117142eabddd_Smoothness_6_Float;
        half _ApplyDecals_0413903f5da5491d911d117142eabddd_AmbientOcclusion_5_Float;
        SG_ApplyDecals_66e8fc89f7971da4c8964580a0eb9f80_float(_Property_04a055764411443d802bfbbd0d510c65_Out_0_Vector3, _Transform_3f94cf9dbe844abc9b6727d5d289074f_Out_1_Vector3, _Property_11295d868ff34c388c9212b90b781aff_Out_0_Float, _Property_b522b61b85ff4ecbb0eb63cff689f5cb_Out_0_Float, _Property_a1dc37a47c5640d0870861199df0bd70_Out_0_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd, _ApplyDecals_0413903f5da5491d911d117142eabddd_BaseColor_1_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_SpecularColor_2_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_NormalWS_3_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_Metallic_4_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd_Smoothness_6_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd_AmbientOcclusion_5_Float);
        float3 _Property_b986326ad9b34d6ea3a7237ba2bd1cd6_Out_0_Vector3 = _Emission;
        Bindings_DebugLighting_61e571d2b9ede1240a524a849d20c997_float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpaceNormal = IN.WorldSpaceNormal;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpaceTangent = IN.WorldSpaceTangent;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.WorldSpacePosition = IN.WorldSpacePosition;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.PixelPosition = IN.PixelPosition;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.uv1 = IN.uv1;
        _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9.uv2 = IN.uv2;
        float3 _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3;
        float3 _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3;
        float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float;
        float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float;
        float3 _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3;
        float _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float;
        SG_DebugLighting_61e571d2b9ede1240a524a849d20c997_float(_ApplyDecals_0413903f5da5491d911d117142eabddd_BaseColor_1_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_NormalWS_3_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_Metallic_4_Float, _ApplyDecals_0413903f5da5491d911d117142eabddd_Smoothness_6_Float, _Property_b986326ad9b34d6ea3a7237ba2bd1cd6_Out_0_Vector3, _ApplyDecals_0413903f5da5491d911d117142eabddd_AmbientOcclusion_5_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float);
        float4 _Property_a2c93b67d7e14184996710181bc8106a_Out_0_Vector4 = _Color_A;
        float2 _Property_30bd0eecac2f497db8c8b272e8e7d3e5_Out_0_Vector2 = _Color_A_Location;
        float4 _Property_47fc9a397b1241599709d29487238203_Out_0_Vector4 = _Color_B;
        float2 _Property_99bf1a52083e4b7f84197e960ed6a728_Out_0_Vector2 = _Color_B_Location;
        float4 _Property_d5f33cf319a54be08f26ec7c7538d6a4_Out_0_Vector4 = _Color_C;
        Bindings_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceNormal = IN.WorldSpaceNormal;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceTangent = IN.WorldSpaceTangent;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.ObjectSpacePosition = IN.ObjectSpacePosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.WorldSpacePosition = IN.WorldSpacePosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.NDCPosition = IN.NDCPosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.PixelPosition = IN.PixelPosition;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.uv1 = IN.uv1;
        _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc.uv2 = IN.uv2;
        half3 _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc_Lit_1_Vector3;
        SG_SSHDCustomCoreModel02_1a211cc8629adf04ebb92171157e366f_float(_DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3, true, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float, half(1), _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float, _Property_a2c93b67d7e14184996710181bc8106a_Out_0_Vector4, _Property_30bd0eecac2f497db8c8b272e8e7d3e5_Out_0_Vector2, _Property_47fc9a397b1241599709d29487238203_Out_0_Vector4, _Property_99bf1a52083e4b7f84197e960ed6a728_Out_0_Vector2, _Property_d5f33cf319a54be08f26ec7c7538d6a4_Out_0_Vector4, _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc, _SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc_Lit_1_Vector3);
        float3 _Add_a11e24e7d4fd4494895fd67f375acb21_Out_2_Vector3;
        Unity_Add_float3(_SSHDCustomCoreModel02_c960e5977e5547229796b270259082cc_Lit_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3, _Add_a11e24e7d4fd4494895fd67f375acb21_Out_2_Vector3);
        float _Property_d5e8251fc84a46aea1765511445b653e_Out_0_Float = _Alpha;
        Bindings_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpaceNormal = IN.WorldSpaceNormal;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpaceTangent = IN.WorldSpaceTangent;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.WorldSpacePosition = IN.WorldSpacePosition;
        _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3.NDCPosition = IN.NDCPosition;
        float3 _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3_Out_1_Vector3;
        SG_DebugMaterials_53d20e1f36b55014f99f9ccae649c798_float(_Add_a11e24e7d4fd4494895fd67f375acb21_Out_2_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_BaseColor_1_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Normal_4_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Metallic_2_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Smoothness_3_Float, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_Emission_5_Vector3, _DebugLighting_7c013feaa8eb4525bc13d6e4722049d9_AmbientOcclusion_6_Float, _Property_d5e8251fc84a46aea1765511445b653e_Out_0_Float, _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3, _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3_Out_1_Vector3);
        Lit_1 = _DebugMaterials_c34c7000ddbd4d37b5d71bf460b2fca3_Out_1_Vector3;
        }
        
        void Unity_Blend_Overwrite_float3(float3 Base, float3 Blend, out float3 Out, float Opacity)
        {
            Out = lerp(Base, Blend, Opacity);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float3 BaseColor;
            float Alpha;
            float AlphaClipThreshold;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_debbfbf1a581455dbc61b338f851a8d4_Out_0_Boolean = _DisableAllGradients;
            float _Property_2fb138ca7c89409da2d3e517c9bcb36b_Out_0_Boolean = _DisableGradientMap;
            UnityTexture2D _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D = UnityBuildTexture2DStructInternal(_BaseColor, sampler_BaseColor, _BaseColor_TexelSize, float4(1, 1, 0, 0), float4(0, 0, 0, 0));
            float2 _Property_94094af0818841fea1396a1ccae7a167_Out_0_Vector2 = _Tilling;
            float2 _TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2;
            Unity_TilingAndOffset_float(IN.uv0.xy, _Property_94094af0818841fea1396a1ccae7a167_Out_0_Vector2, float2 (0, 0), _TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2);
            float4 _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.tex, _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.samplerstate, _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.GetTransformedUV(_TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2) );
            if (_Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.hdrDecode.x > 0)
                _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4, _Property_48080d6bb97c48348eb44b8f13db8b96_Out_0_Texture2D.hdrDecode);
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.r;
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_G_5_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.g;
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_B_6_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.b;
            float _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_A_7_Float = _SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_RGBA_0_Vector4.a;
            float4 _Property_de1bd6122d6c4aa4ba9d7692e6db8956_Out_0_Vector4 = _Color1;
            float4 _Property_fceba521f35a4cc88bbd9602ff68242f_Out_0_Vector4 = _Color2;
            float2 _Property_e60b8198308f4aad8dfa7a52168790ce_Out_0_Vector2 = _Color_1_Location;
            float _Remap_b9f81469352045d7ae4bb207f0c202fc_Out_3_Float;
            Unity_Remap_float(_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float, _Property_e60b8198308f4aad8dfa7a52168790ce_Out_0_Vector2, float2 (0, 1), _Remap_b9f81469352045d7ae4bb207f0c202fc_Out_3_Float);
            float _Saturate_473bb6d59e074412ad7162adcfe4cef1_Out_1_Float;
            Unity_Saturate_float(_Remap_b9f81469352045d7ae4bb207f0c202fc_Out_3_Float, _Saturate_473bb6d59e074412ad7162adcfe4cef1_Out_1_Float);
            float4 _Lerp_f3e294dc316840b6b4294bcb3e7b3f7b_Out_3_Vector4;
            Unity_Lerp_float4(_Property_de1bd6122d6c4aa4ba9d7692e6db8956_Out_0_Vector4, _Property_fceba521f35a4cc88bbd9602ff68242f_Out_0_Vector4, (_Saturate_473bb6d59e074412ad7162adcfe4cef1_Out_1_Float.xxxx), _Lerp_f3e294dc316840b6b4294bcb3e7b3f7b_Out_3_Vector4);
            float4 _Property_60ebe0bb4a0e4a6fbd758422fbc8e1af_Out_0_Vector4 = _Color3;
            float2 _Property_46eb955dacc0426abc5d73b1be33af42_Out_0_Vector2 = _Color_2_Location;
            float _Remap_c2e999a017a6490b91665566afc5916d_Out_3_Float;
            Unity_Remap_float(_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float, _Property_46eb955dacc0426abc5d73b1be33af42_Out_0_Vector2, float2 (0, 1), _Remap_c2e999a017a6490b91665566afc5916d_Out_3_Float);
            float _Saturate_72fb61dd2f064241a5337c41de422b36_Out_1_Float;
            Unity_Saturate_float(_Remap_c2e999a017a6490b91665566afc5916d_Out_3_Float, _Saturate_72fb61dd2f064241a5337c41de422b36_Out_1_Float);
            float4 _Lerp_72f03acdde2245b29f8cd4a47d6b4cd6_Out_3_Vector4;
            Unity_Lerp_float4(_Lerp_f3e294dc316840b6b4294bcb3e7b3f7b_Out_3_Vector4, _Property_60ebe0bb4a0e4a6fbd758422fbc8e1af_Out_0_Vector4, (_Saturate_72fb61dd2f064241a5337c41de422b36_Out_1_Float.xxxx), _Lerp_72f03acdde2245b29f8cd4a47d6b4cd6_Out_3_Vector4);
            float4 _Property_e84e577bc7db46749ec6367493b51e06_Out_0_Vector4 = _AOColor;
            UnityTexture2D _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D = UnityBuildTexture2DStructInternal(_ORM, sampler_ORM, _ORM_TexelSize, float4(1, 1, 0, 0), float4(0, 0, 0, 0));
            float4 _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.tex, _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.samplerstate, _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.GetTransformedUV(_TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2) );
            if (_Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.hdrDecode.x > 0)
                _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4, _Property_020100d1c6634f1a9866b5beb34049bc_Out_0_Texture2D.hdrDecode);
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_R_4_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.r;
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_G_5_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.g;
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.b;
            float _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_A_7_Float = _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_RGBA_0_Vector4.a;
            float _OneMinus_89dcf9413d9142c481cf9d5e5035b712_Out_1_Float;
            Unity_OneMinus_float(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_R_4_Float, _OneMinus_89dcf9413d9142c481cf9d5e5035b712_Out_1_Float);
            float2 _Property_753b87b3c47d403696efc934ea3dbea9_Out_0_Vector2 = _AOLevels;
            float _Remap_983e82311f6e4fe5b382c71d656497ec_Out_3_Float;
            Unity_Remap_float(_OneMinus_89dcf9413d9142c481cf9d5e5035b712_Out_1_Float, _Property_753b87b3c47d403696efc934ea3dbea9_Out_0_Vector2, float2 (1, 0), _Remap_983e82311f6e4fe5b382c71d656497ec_Out_3_Float);
            float _Property_ba1b5b1e95414eaebf54a9d26291e91f_Out_0_Float = _AOIntensity;
            float _Multiply_2341d6d389c44e96bba10c0c1e0c9f14_Out_2_Float;
            Unity_Multiply_float_float(_Remap_983e82311f6e4fe5b382c71d656497ec_Out_3_Float, _Property_ba1b5b1e95414eaebf54a9d26291e91f_Out_0_Float, _Multiply_2341d6d389c44e96bba10c0c1e0c9f14_Out_2_Float);
            float4 _Lerp_8d7f6575739049459e47aaca25cf59d0_Out_3_Vector4;
            Unity_Lerp_float4(_Lerp_72f03acdde2245b29f8cd4a47d6b4cd6_Out_3_Vector4, _Property_e84e577bc7db46749ec6367493b51e06_Out_0_Vector4, (_Multiply_2341d6d389c44e96bba10c0c1e0c9f14_Out_2_Float.xxxx), _Lerp_8d7f6575739049459e47aaca25cf59d0_Out_3_Vector4);
            float4 _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4;
            Unity_Branch_float4(_Property_2fb138ca7c89409da2d3e517c9bcb36b_Out_0_Boolean, (_SampleTexture2D_05f49fd9dcc74e12b660c11ac8b79ae5_R_4_Float.xxxx), _Lerp_8d7f6575739049459e47aaca25cf59d0_Out_3_Vector4, _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4);
            float _Property_18ebff8ae81646538dc78b9020d39b78_Out_0_Boolean = _Z_Gradient;
            float _Property_f94879ac790e4c109a34ec1f73a3c3a6_Out_0_Boolean = _Y_Gradient;
            float _Property_c61e8f7030cd47ac9b641cd98ca92fe3_Out_0_Boolean = _X_Gradient;
            float4 _Property_1f3e0b1c283f4c949004f43f37fe1a90_Out_0_Vector4 = _X_GradientColor;
            float _Property_94b16f357eac4058809921fa96f34787_Out_0_Float = _X_GradientStartPosition;
            float _Property_8c3dacdcc9cc4b2880b33b1d1913e9f1_Out_0_Float = _X_GradientEndPosition;
            float _Split_2ca42922e29b49b4b7113632901be932_R_1_Float = IN.AbsoluteWorldSpacePosition[0];
            float _Split_2ca42922e29b49b4b7113632901be932_G_2_Float = IN.AbsoluteWorldSpacePosition[1];
            float _Split_2ca42922e29b49b4b7113632901be932_B_3_Float = IN.AbsoluteWorldSpacePosition[2];
            float _Split_2ca42922e29b49b4b7113632901be932_A_4_Float = 0;
            float _Smoothstep_c73aebae28cd43fc896795bd80911919_Out_3_Float;
            Unity_Smoothstep_float(_Property_94b16f357eac4058809921fa96f34787_Out_0_Float, _Property_8c3dacdcc9cc4b2880b33b1d1913e9f1_Out_0_Float, _Split_2ca42922e29b49b4b7113632901be932_R_1_Float, _Smoothstep_c73aebae28cd43fc896795bd80911919_Out_3_Float);
            float _Property_dd9c997bc2514f2bbe579af0da9fecb2_Out_0_Float = _X_GradientIntensity;
            float _Multiply_961d468ad0db4c8ebb883194aa2c7cf5_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_c73aebae28cd43fc896795bd80911919_Out_3_Float, _Property_dd9c997bc2514f2bbe579af0da9fecb2_Out_0_Float, _Multiply_961d468ad0db4c8ebb883194aa2c7cf5_Out_2_Float);
            float4 _Lerp_ba89b24cd32049a0964667d354c52dcc_Out_3_Vector4;
            Unity_Lerp_float4(_Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4, _Property_1f3e0b1c283f4c949004f43f37fe1a90_Out_0_Vector4, (_Multiply_961d468ad0db4c8ebb883194aa2c7cf5_Out_2_Float.xxxx), _Lerp_ba89b24cd32049a0964667d354c52dcc_Out_3_Vector4);
            float4 _Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4;
            Unity_Branch_float4(_Property_c61e8f7030cd47ac9b641cd98ca92fe3_Out_0_Boolean, _Lerp_ba89b24cd32049a0964667d354c52dcc_Out_3_Vector4, _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4, _Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4);
            float4 _Property_bbb56023a68b4c17bbaa88d52a24ab64_Out_0_Vector4 = _Y_GradientColor;
            float _Property_e53d4551d24740e89ab9d2dde9d07fa9_Out_0_Float = _Y_GradientStartPosition;
            float _Property_f705a7a1ffdc47a388bfa2aa340dd71f_Out_0_Float = _Y_GradientEndPosition;
            float _Smoothstep_8f0d8c12283b443dabc9178711fca6ea_Out_3_Float;
            Unity_Smoothstep_float(_Property_e53d4551d24740e89ab9d2dde9d07fa9_Out_0_Float, _Property_f705a7a1ffdc47a388bfa2aa340dd71f_Out_0_Float, _Split_2ca42922e29b49b4b7113632901be932_G_2_Float, _Smoothstep_8f0d8c12283b443dabc9178711fca6ea_Out_3_Float);
            float _Property_c3a47369d5bb47ddb5fcb74791e32d8d_Out_0_Float = _Y_GradientIntensity;
            float _Multiply_0b75652c94cc4e928c2a6eda4bc65145_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_8f0d8c12283b443dabc9178711fca6ea_Out_3_Float, _Property_c3a47369d5bb47ddb5fcb74791e32d8d_Out_0_Float, _Multiply_0b75652c94cc4e928c2a6eda4bc65145_Out_2_Float);
            float4 _Lerp_2023214161dd495a9df4a86e9aab0a55_Out_3_Vector4;
            Unity_Lerp_float4(_Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4, _Property_bbb56023a68b4c17bbaa88d52a24ab64_Out_0_Vector4, (_Multiply_0b75652c94cc4e928c2a6eda4bc65145_Out_2_Float.xxxx), _Lerp_2023214161dd495a9df4a86e9aab0a55_Out_3_Vector4);
            float4 _Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4;
            Unity_Branch_float4(_Property_f94879ac790e4c109a34ec1f73a3c3a6_Out_0_Boolean, _Lerp_2023214161dd495a9df4a86e9aab0a55_Out_3_Vector4, _Branch_433483067cd34bd7b3b1f56283a26bc7_Out_3_Vector4, _Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4);
            float4 _Property_d20a45cfc0cb48e4a9823f3125a60fb7_Out_0_Vector4 = _Y_GradientColor;
            float _Property_a3a6fd6a552845c797564be4a2b63e5d_Out_0_Float = _Z_GradientStartPosition;
            float _Property_1f6239cf046944d6b4f70da4fca83661_Out_0_Float = _Z_GradientEndPosition;
            float _Smoothstep_9cb470a94c1b4410b6282b2c261d462d_Out_3_Float;
            Unity_Smoothstep_float(_Property_a3a6fd6a552845c797564be4a2b63e5d_Out_0_Float, _Property_1f6239cf046944d6b4f70da4fca83661_Out_0_Float, _Split_2ca42922e29b49b4b7113632901be932_B_3_Float, _Smoothstep_9cb470a94c1b4410b6282b2c261d462d_Out_3_Float);
            float _Property_1ac1a7f19f6342f0a029129f1adbfd67_Out_0_Float = _Z_GradientIntensity;
            float _Multiply_d258a35ceccb4063a2594879737f8bb1_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_9cb470a94c1b4410b6282b2c261d462d_Out_3_Float, _Property_1ac1a7f19f6342f0a029129f1adbfd67_Out_0_Float, _Multiply_d258a35ceccb4063a2594879737f8bb1_Out_2_Float);
            float4 _Lerp_5d732be3cb674212b3307c0a98052b15_Out_3_Vector4;
            Unity_Lerp_float4(_Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4, _Property_d20a45cfc0cb48e4a9823f3125a60fb7_Out_0_Vector4, (_Multiply_d258a35ceccb4063a2594879737f8bb1_Out_2_Float.xxxx), _Lerp_5d732be3cb674212b3307c0a98052b15_Out_3_Vector4);
            float4 _Branch_808c9eecf2bc4ac09cea8d7293c4d22a_Out_3_Vector4;
            Unity_Branch_float4(_Property_18ebff8ae81646538dc78b9020d39b78_Out_0_Boolean, _Lerp_5d732be3cb674212b3307c0a98052b15_Out_3_Vector4, _Branch_daf9e8f61e6741ecbbfc5dddfc5447f1_Out_3_Vector4, _Branch_808c9eecf2bc4ac09cea8d7293c4d22a_Out_3_Vector4);
            float4 _Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4;
            Unity_Branch_float4(_Property_debbfbf1a581455dbc61b338f851a8d4_Out_0_Boolean, _Branch_84bad7510d2e4d6c95450d30a136397e_Out_3_Vector4, _Branch_808c9eecf2bc4ac09cea8d7293c4d22a_Out_3_Vector4, _Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4);
            UnityTexture2D _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D = UnityBuildTexture2DStructInternal(_Normal, sampler_Normal, _Normal_TexelSize, float4(1, 1, 0, 0), float4(0, 0, 0, 0));
            float4 _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(_Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.tex, _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.samplerstate, _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.GetTransformedUV(_TilingAndOffset_8bcd0d096a4e44cb8580157888f322c7_Out_3_Vector2) );
            if (_Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.hdrDecode.x > 0)
                _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4, _Property_1de0b429b5a7444598c8a3ee00d95f2d_Out_0_Texture2D.hdrDecode);
            _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.rgb = UnpackNormal(_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4);
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_R_4_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.r;
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_G_5_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.g;
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_B_6_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.b;
            float _SampleTexture2D_8815625319994dfd8269620c16e99d88_A_7_Float = _SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.a;
            float _Property_3c169350af004240a8c8543dce8c320b_Out_0_Boolean = _UseCustomRoughness;
            float _Property_5c2d908353e24a3692fb0e08fe229355_Out_0_Float = _CustomRoughness;
            float _OneMinus_59ec04ea5505409baa7bf818ec2e91cd_Out_1_Float;
            Unity_OneMinus_float(_Property_5c2d908353e24a3692fb0e08fe229355_Out_0_Float, _OneMinus_59ec04ea5505409baa7bf818ec2e91cd_Out_1_Float);
            float _Property_a19c0c9a848947f4aab57660a9a18f93_Out_0_Float = _RoughnessMultiplier;
            float _Multiply_17be8d0efec54ca3bc59e321b0c596ee_Out_2_Float;
            Unity_Multiply_float_float(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_G_5_Float, _Property_a19c0c9a848947f4aab57660a9a18f93_Out_0_Float, _Multiply_17be8d0efec54ca3bc59e321b0c596ee_Out_2_Float);
            float _Saturate_04d0090531f74a9ebdada50fd6f42af5_Out_1_Float;
            Unity_Saturate_float(_Multiply_17be8d0efec54ca3bc59e321b0c596ee_Out_2_Float, _Saturate_04d0090531f74a9ebdada50fd6f42af5_Out_1_Float);
            float _OneMinus_c9932877d03347048da7ba94008426d5_Out_1_Float;
            Unity_OneMinus_float(_Saturate_04d0090531f74a9ebdada50fd6f42af5_Out_1_Float, _OneMinus_c9932877d03347048da7ba94008426d5_Out_1_Float);
            float _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float;
            Unity_Branch_float(_Property_3c169350af004240a8c8543dce8c320b_Out_0_Boolean, _OneMinus_59ec04ea5505409baa7bf818ec2e91cd_Out_1_Float, _OneMinus_c9932877d03347048da7ba94008426d5_Out_1_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float);
            float _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float;
            Unity_Saturate_float(_SampleTexture2D_440c4ffefe7243329e36af96a21a4018_R_4_Float, _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float);
            float4 _Property_34dac06f3b7442efa3e05c85b09ec445_Out_0_Vector4 = _ACT1_Color_A;
            float2 _Property_b3a982240db24cfbbc14de979535b458_Out_0_Vector2 = _ACT1_Color_A_Location;
            float4 _Property_0e317fd277254f1aae079db6e8d2e8dc_Out_0_Vector4 = _ACT1_Color_B;
            float2 _Property_3187bf5304c54604a1d464e65a9dac03_Out_0_Vector2 = _ACT1_Color_B_Location;
            float4 _Property_41e2d595ca9d41dd8806c1a749a3bb43_Out_0_Vector4 = _ACT1_Color_C;
            Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceNormal = IN.WorldSpaceNormal;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceTangent = IN.WorldSpaceTangent;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.ObjectSpacePosition = IN.ObjectSpacePosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.WorldSpacePosition = IN.WorldSpacePosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.NDCPosition = IN.NDCPosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.PixelPosition = IN.PixelPosition;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.uv1 = IN.uv1;
            _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d.uv2 = IN.uv2;
            float3 _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d_Lit_1_Vector3;
            SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), (_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.xyz), _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float, float3 (0, 0, 0), _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float, float(1), _Property_34dac06f3b7442efa3e05c85b09ec445_Out_0_Vector4, _Property_b3a982240db24cfbbc14de979535b458_Out_0_Vector2, _Property_0e317fd277254f1aae079db6e8d2e8dc_Out_0_Vector4, _Property_3187bf5304c54604a1d464e65a9dac03_Out_0_Vector2, _Property_41e2d595ca9d41dd8806c1a749a3bb43_Out_0_Vector4, _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d, _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d_Lit_1_Vector3);
            float4 _Property_259fe0b41799487eb44d557cc24932a4_Out_0_Vector4 = _ACT2_Color_A;
            float2 _Property_83a57d16b4dc4f48985395b92f2de589_Out_0_Vector2 = _ACT2_Color_A_Location;
            float4 _Property_8ca39a18030a4bbd9348cb5b458a8372_Out_0_Vector4 = _ACT2_Color_B;
            float2 _Property_fa2809814b4148bd8ece170a87d230ef_Out_0_Vector2 = _ACT2_Color_B_Location;
            float4 _Property_c83f37081cf64cc7999c1bb19926d7c1_Out_0_Vector4 = _ACT2_Color_C;
            Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceNormal = IN.WorldSpaceNormal;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceTangent = IN.WorldSpaceTangent;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.ObjectSpacePosition = IN.ObjectSpacePosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.WorldSpacePosition = IN.WorldSpacePosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.NDCPosition = IN.NDCPosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.PixelPosition = IN.PixelPosition;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.uv1 = IN.uv1;
            _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370.uv2 = IN.uv2;
            float3 _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370_Lit_1_Vector3;
            SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), (_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.xyz), _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float, float3 (0, 0, 0), _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float, float(1), _Property_259fe0b41799487eb44d557cc24932a4_Out_0_Vector4, _Property_83a57d16b4dc4f48985395b92f2de589_Out_0_Vector2, _Property_8ca39a18030a4bbd9348cb5b458a8372_Out_0_Vector4, _Property_fa2809814b4148bd8ece170a87d230ef_Out_0_Vector2, _Property_c83f37081cf64cc7999c1bb19926d7c1_Out_0_Vector4, _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370, _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370_Lit_1_Vector3);
            float4 _Property_13886bd11bee4d40b3eb7be3dff1c022_Out_0_Vector4 = _ACT3_Color_A;
            float2 _Property_5578cc8231d143d9a5f34ae24f110091_Out_0_Vector2 = _ACT3_Color_A_Location;
            float4 _Property_4d39077496e94d728ca3d19d42d3bd68_Out_0_Vector4 = _ACT3_Color_B;
            float2 _Property_5b1c9e69733942519a853e51dd4770f6_Out_0_Vector2 = _ACT3_Color_B_Location;
            float4 _Property_6a5550ba5eec4966850efea3418c844a_Out_0_Vector4 = _ACT3_Color_C;
            Bindings_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceNormal = IN.WorldSpaceNormal;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceTangent = IN.WorldSpaceTangent;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.ObjectSpacePosition = IN.ObjectSpacePosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.WorldSpacePosition = IN.WorldSpacePosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.AbsoluteWorldSpacePosition = IN.AbsoluteWorldSpacePosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.NDCPosition = IN.NDCPosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.PixelPosition = IN.PixelPosition;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.uv1 = IN.uv1;
            _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a.uv2 = IN.uv2;
            float3 _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a_Lit_1_Vector3;
            SG_SSHDCustomLightModel_93cac63e944684d468bc43443cfc3017_float((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), (_SampleTexture2D_8815625319994dfd8269620c16e99d88_RGBA_0_Vector4.xyz), _SampleTexture2D_440c4ffefe7243329e36af96a21a4018_B_6_Float, _Branch_660ceeedaa394a149641d8a40c437205_Out_3_Float, float3 (0, 0, 0), _Saturate_d338ecc1383d49de81d44776b9657ad6_Out_1_Float, float(1), _Property_13886bd11bee4d40b3eb7be3dff1c022_Out_0_Vector4, _Property_5578cc8231d143d9a5f34ae24f110091_Out_0_Vector2, _Property_4d39077496e94d728ca3d19d42d3bd68_Out_0_Vector4, _Property_5b1c9e69733942519a853e51dd4770f6_Out_0_Vector2, _Property_6a5550ba5eec4966850efea3418c844a_Out_0_Vector4, _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a, _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a_Lit_1_Vector3);
            float3 _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3;
            if (_CURRENTACT_ACT_1) _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3 = _SSHDCustomLightModel_0da5a7cf67f24a3780448004d1b3742d_Lit_1_Vector3;
            else if (_CURRENTACT_ACT_2) _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3 = _SSHDCustomLightModel_5e0f8075efa54480b5552070f6c6a370_Lit_1_Vector3;
            else _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3 = _SSHDCustomLightModel_bd3c8be323974927a588e411e9f5806a_Lit_1_Vector3;
            float _Property_854b1929338847e9b4a11e77fedb361b_Out_0_Float = _LightingGradientMapInfluence;
            float3 _Blend_ed10fcb74d224507bd92f8d23fb7e2d9_Out_2_Vector3;
            Unity_Blend_Overwrite_float3((_Branch_b60c3d0667d742edac3fdb7c006b7e1d_Out_3_Vector4.xyz), _CurrentAct_7ed0ea88fa494e1dba731b2369cc98fd_Out_0_Vector3, _Blend_ed10fcb74d224507bd92f8d23fb7e2d9_Out_2_Vector3, _Property_854b1929338847e9b4a11e77fedb361b_Out_0_Float);
            surface.BaseColor = _Blend_ed10fcb74d224507bd92f8d23fb7e2d9_Out_2_Vector3;
            surface.Alpha = float(1);
            surface.AlphaClipThreshold = float(0.5);
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
        #if VFX_USE_GRAPH_VALUES
            uint instanceActiveIndex = asuint(UNITY_ACCESS_INSTANCED_PROP(PerInstance, _InstanceActiveIndex));
            /* WARNING: $splice Could not find named fragment 'VFXLoadGraphValues' */
        #endif
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
            // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            float3 unnormalizedNormalWS = input.normalWS;
            const float renormFactor = 1.0 / length(unnormalizedNormalWS);
        
            // use bitangent on the fly like in hdrp
            // IMPORTANT! If we ever support Flip on double sided materials ensure bitangent and tangent are NOT flipped.
            float crossSign = (input.tangentWS.w > 0.0 ? 1.0 : -1.0)* GetOddNegativeScale();
            float3 bitang = crossSign * cross(input.normalWS.xyz, input.tangentWS.xyz);
        
            output.WorldSpaceNormal = renormFactor * input.normalWS.xyz;      // we want a unit length Normal Vector node in shader graph
        
            // to pr               eserve mikktspace compliance we use same scale renormFactor as was used on the normal.
            // This                is explained in section 2.2 in "surface gradient based bump mapping framework"
            output.WorldSpaceTangent = renormFactor * input.tangentWS.xyz;
            output.WorldSpaceBiTangent = renormFactor * bitang;
        
            output.WorldSpaceViewDirection = GetWorldSpaceNormalizeViewDir(input.positionWS);
            output.WorldSpacePosition = input.positionWS;
            output.ObjectSpacePosition = TransformWorldToObject(input.positionWS);
            output.AbsoluteWorldSpacePosition = GetAbsolutePositionWS(input.positionWS);
        
            #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x < 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #else
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x > 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScaledScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 = input.texCoord0;
            output.uv1 = input.texCoord1;
            output.uv2 = input.texCoord2;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/PBR2DPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
    }
    CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
    CustomEditorForRenderPipeline "UnityEditor.ShaderGraphLitGUI" "UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset"
    FallBack "Hidden/Shader Graph/FallbackError"
}
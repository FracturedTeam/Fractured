Shader "Shader Graphs/SHD_TextMist"
{
    Properties
    {
        [IntRange] _StencilID("Stencil ID", Range(0,255)) = 0
        _Color("Color", Color) = (1, 1, 1, 1)
        _Color2("Color2", Color) = (0.5943396, 0.5943396, 0.5943396, 1)
        _CellsTiling("CellsTiling", Vector, 2) = (0.2, 0.2, 0, 0)
        _CellsSpeed1("CellsSpeed1", Float) = 0
        _CellsSpeed2("CellsSpeed2", Float) = 0
        _CellsIntensity("CellsIntensity", Float) = 0.1
        _SceneColorClamp("SceneColorClamp", Float) = 0.7
        _SceneColorLevelsCorrection("SceneColorLevelsCorrection", Vector, 2) = (0, 1, 0, 0)
        _NoiseTiling("NoiseTiling", Vector, 2) = (1, 1, 0, 0)
        _NoiseIntensity("NoiseIntensity", Float) = 0.05
        _NoiseSpeed("NoiseSpeed", Vector, 2) = (0, 0, 0, 0)
        _CircleSmoothstep("CircleSmoothstep", Vector, 2) = (0, 1, 0, 0)
        _CirclePower("CirclePower", Float) = 4
        _SecondaryCellsLightenAmount("SecondaryCellsLightenAmount", Range(0, 1)) = 0.1
        _SecondaryCellsHightlightColor("SecondaryCellsHightlightColor", Color) = (0, 0, 0, 1)
        _SecondaryCellsDistortionAmount("SecondaryCellsDistortionAmount", Range(0, 1)) = 0.05
        _SecondaryCellsTilingOffset("SecondaryCellsTilingOffset", Vector, 4) = (1, 1, 0, 0)
        _ChromaticAberration("ChromaticAberration", Float) = 0.01
        _ChromaticAberrationOpacity("ChromaticAberrationOpacity", Range(0, 1)) = 0
        _ChromaticAberrationLevelsCorrection("ChromaticAberrationLevelsCorrection", Vector, 2) = (0, 1, 0, 0)
        _ChromaticAberrationColorMax("ChromaticAberrationColorMax", Vector, 3) = (1, 1, 1, 0)
        [NonModifiableTextureData][NoScaleOffset]_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D("Texture2D", 2D) = "white" {}
        [NonModifiableTextureData][NoScaleOffset]_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D("Texture2D", 2D) = "white" {}
        [NonModifiableTextureData][NoScaleOffset]_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D("Texture2D", 2D) = "white" {}
        [NonModifiableTextureData][NoScaleOffset]_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D("Texture2D", 2D) = "white" {}
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
            "RenderType"="Transparent"
            "UniversalMaterialType" = "Unlit"
            "Queue"="Transparent"
            "DisableBatching"="False"
            "ShaderGraphShader"="true"
            "ShaderGraphTargetId"="UniversalUnlitSubTarget"
        }
        Pass
        {
            Name "Universal Forward"
            Tags
            {
                // LightMode: <None>
            }
        
        // Render State
        Cull Back
        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        ZTest LEqual
        ZWrite Off
        
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
        #pragma multi_compile _ LIGHTMAP_ON
        #pragma multi_compile _ DIRLIGHTMAP_COMBINED
        #pragma multi_compile _ USE_LEGACY_LIGHTMAPS
        #pragma multi_compile _ LIGHTMAP_BICUBIC_SAMPLING
        #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
        #pragma multi_compile_fragment _ DEBUG_DISPLAY
        #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX_NORMAL_OUTPUT
        #define FEATURES_GRAPH_VERTEX_TANGENT_OUTPUT
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_UNLIT
        #define _FOG_FRAGMENT 1
        #define _SURFACE_TYPE_TRANSPARENT 1
        #define UNLIT_DEFAULT_DECAL_BLENDING 1
        #define REQUIRE_DEPTH_TEXTURE
        #define REQUIRE_OPAQUE_TEXTURE
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Fog.hlsl"
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
            #if UNITY_ANY_INSTANCING_ENABLED || defined(ATTRIBUTES_NEED_INSTANCEID)
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
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
             float3 WorldSpacePosition;
             float4 ScreenPosition;
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float3 TimeParameters;
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
             float4 texCoord0 : INTERP0;
             float3 positionWS : INTERP1;
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
            output.texCoord0.xyzw = input.texCoord0;
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
            output.texCoord0 = input.texCoord0.xyzw;
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
        float4 _SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D_TexelSize;
        float4 _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D_TexelSize;
        float4 _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D_TexelSize;
        float4 _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D_TexelSize;
        float4 _Color2;
        float4 _Color;
        float _CellsSpeed2;
        float _CellsSpeed1;
        float2 _CellsTiling;
        float2 _SceneColorLevelsCorrection;
        float _NoiseIntensity;
        float _CellsIntensity;
        float _SceneColorClamp;
        float2 _CircleSmoothstep;
        float _CirclePower;
        float _ChromaticAberration;
        float _SecondaryCellsLightenAmount;
        float4 _SecondaryCellsHightlightColor;
        float _SecondaryCellsDistortionAmount;
        float4 _SecondaryCellsTilingOffset;
        float _ChromaticAberrationOpacity;
        float2 _ChromaticAberrationLevelsCorrection;
        float3 _ChromaticAberrationColorMax;
        float2 _NoiseTiling;
        float2 _NoiseSpeed;
        UNITY_TEXTURE_STREAMING_DEBUG_VARS;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D);
        TEXTURE2D(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D);
        TEXTURE2D(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D);
        TEXTURE2D(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D);
        
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
        
        void Unity_SceneColor_float(float4 UV, out float3 Out)
        {
            Out = SHADERGRAPH_SAMPLE_SCENE_COLOR(UV.xy);
        }
        
        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            Rotation = Rotation * (3.1415926f/180.0f);
            UV -= Center;
            float s, c;
            sincos(Rotation, s, c);
            float3 r3 = float3(-s, c, s);
            float2 r1;
            r1.y = dot(UV, r3.xy);
            r1.x = dot(UV, r3.yz);
            Out = r1 + Center;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        void Unity_Maximum_float(float A, float B, out float Out)
        {
            Out = max(A, B);
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        void Unity_Add_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A + B;
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
        {
            Out = lerp(A, B, T);
        }
        
        void Unity_Saturation_float(float3 In, float Saturation, out float3 Out)
        {
            float luma = dot(In, float3(0.2126729, 0.7151522, 0.0721750));
            Out =  luma.xxx + Saturation.xxx * (In - luma.xxx);
        }
        
        void Unity_Smoothstep_float3(float3 Edge1, float3 Edge2, float3 In, out float3 Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        void Unity_Lerp_float3(float3 A, float3 B, float3 T, out float3 Out)
        {
            Out = lerp(A, B, T);
        }
        
        void Unity_Negate_float(float In, out float Out)
        {
            Out = -1 * In;
        }
        
        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }
        
        void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
        {
            RGBA = float4(R, G, B, A);
            RGB = float3(R, G, B);
            RG = float2(R, G);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        void Unity_Distance_float2(float2 A, float2 B, out float Out)
        {
            Out = distance(A, B);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_SceneDepth_Linear01_float(float4 UV, out float Out)
        {
            Out = Linear01Depth(SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), _ZBufferParams);
        }
        
        void Unity_Subtract_float(float A, float B, out float Out)
        {
            Out = A - B;
        }
        
        struct Bindings_SoftParticles_d0f40856fc355224fab6453746e0507a_float
        {
        float4 ScreenPosition;
        float2 NDCPosition;
        };
        
        void SG_SoftParticles_d0f40856fc355224fab6453746e0507a_float(float2 Vector2_AC648482, Bindings_SoftParticles_d0f40856fc355224fab6453746e0507a_float IN, out float Output1_1)
        {
        float _SceneDepth_4582691621dbf18cb0eb2f8d5aa469b7_Out_1_Float;
        Unity_SceneDepth_Linear01_float(float4(IN.NDCPosition.xy, 0, 0), _SceneDepth_4582691621dbf18cb0eb2f8d5aa469b7_Out_1_Float);
        float _Multiply_abd94f3d0b36a08380f6ab11443878f2_Out_2_Float;
        Unity_Multiply_float_float(_SceneDepth_4582691621dbf18cb0eb2f8d5aa469b7_Out_1_Float, _ProjectionParams.z, _Multiply_abd94f3d0b36a08380f6ab11443878f2_Out_2_Float);
        float4 _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4 = IN.ScreenPosition;
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_R_1_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[0];
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_G_2_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[1];
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_B_3_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[2];
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_A_4_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[3];
        float _Subtract_f130a0b621d472829a7045e95984f2a9_Out_2_Float;
        Unity_Subtract_float(_Multiply_abd94f3d0b36a08380f6ab11443878f2_Out_2_Float, _Split_8ee3f43b8caa0c8ab120e9a242f543ca_A_4_Float, _Subtract_f130a0b621d472829a7045e95984f2a9_Out_2_Float);
        float2 _Property_90bb3b6ce940ef868c970ea6aae7c26b_Out_0_Vector2 = Vector2_AC648482;
        float _Remap_db5867ad2542128e864c4c228cc1f3f5_Out_3_Float;
        Unity_Remap_float(_Subtract_f130a0b621d472829a7045e95984f2a9_Out_2_Float, _Property_90bb3b6ce940ef868c970ea6aae7c26b_Out_0_Vector2, float2 (0, 1), _Remap_db5867ad2542128e864c4c228cc1f3f5_Out_3_Float);
        float _Saturate_377f82d9fb446684a06124602b80b432_Out_1_Float;
        Unity_Saturate_float(_Remap_db5867ad2542128e864c4c228cc1f3f5_Out_3_Float, _Saturate_377f82d9fb446684a06124602b80b432_Out_1_Float);
        Output1_1 = _Saturate_377f82d9fb446684a06124602b80b432_Out_1_Float;
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
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float3 _SceneColor_797c5e0605664f268a243b5925dbab4e_Out_1_Vector3;
            Unity_SceneColor_float(float4(IN.NDCPosition.xy, 0, 0), _SceneColor_797c5e0605664f268a243b5925dbab4e_Out_1_Vector3);
            float4 _ScreenPosition_fd10197a85f44b469f625df7a4fee1ac_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
            float2 _Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2;
            Unity_Rotate_Degrees_float(IN.uv0.xy, float2 (0.5, 0.5), float(200.5), _Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2);
            float2 _Property_db2bf3c93cbf4880929bbbbd5b8d32c7_Out_0_Vector2 = _CellsTiling;
            float _Property_20fbe3c647284f08addb9b4125ce6af1_Out_0_Float = _CellsSpeed1;
            float _Multiply_e2672b0aac2d4a33a0d6cc512462530c_Out_2_Float;
            Unity_Multiply_float_float(IN.TimeParameters.x, _Property_20fbe3c647284f08addb9b4125ce6af1_Out_0_Float, _Multiply_e2672b0aac2d4a33a0d6cc512462530c_Out_2_Float);
            float2 _Vector2_0f755d5a1ff04d9b9de7cb63561d44f7_Out_0_Vector2 = float2(float(0), _Multiply_e2672b0aac2d4a33a0d6cc512462530c_Out_2_Float);
            float2 _TilingAndOffset_be8cabdd23314d6f9cb006714bd1aabe_Out_3_Vector2;
            Unity_TilingAndOffset_float(_Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2, _Property_db2bf3c93cbf4880929bbbbd5b8d32c7_Out_0_Vector2, _Vector2_0f755d5a1ff04d9b9de7cb63561d44f7_Out_0_Vector2, _TilingAndOffset_be8cabdd23314d6f9cb006714bd1aabe_Out_3_Vector2);
            float4 _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).tex, UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).samplerstate, UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).GetTransformedUV(_TilingAndOffset_be8cabdd23314d6f9cb006714bd1aabe_Out_3_Vector2) );
            if (UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).hdrDecode.x > 0)
                _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4, UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).hdrDecode);
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_R_4_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.r;
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_G_5_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.g;
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_B_6_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.b;
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_A_7_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.a;
            float _Property_028d8bf17bf4473788d23219249f7451_Out_0_Float = _CellsSpeed2;
            float _Multiply_f0eecebc84f54abc89ef38eaa68f0774_Out_2_Float;
            Unity_Multiply_float_float(IN.TimeParameters.x, _Property_028d8bf17bf4473788d23219249f7451_Out_0_Float, _Multiply_f0eecebc84f54abc89ef38eaa68f0774_Out_2_Float);
            float2 _Vector2_f174d0c85fa546a58eb678ccf778d92c_Out_0_Vector2 = float2(float(0.2), _Multiply_f0eecebc84f54abc89ef38eaa68f0774_Out_2_Float);
            float2 _TilingAndOffset_8f83ef02ec4b473a9913eced11cf3340_Out_3_Vector2;
            Unity_TilingAndOffset_float(_Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2, _Property_db2bf3c93cbf4880929bbbbd5b8d32c7_Out_0_Vector2, _Vector2_f174d0c85fa546a58eb678ccf778d92c_Out_0_Vector2, _TilingAndOffset_8f83ef02ec4b473a9913eced11cf3340_Out_3_Vector2);
            float4 _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).tex, UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).samplerstate, UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).GetTransformedUV(_TilingAndOffset_8f83ef02ec4b473a9913eced11cf3340_Out_3_Vector2) );
            if (UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).hdrDecode.x > 0)
                _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4, UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).hdrDecode);
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_R_4_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.r;
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_G_5_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.g;
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_B_6_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.b;
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_A_7_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.a;
            float _Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float;
            Unity_Maximum_float(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_R_4_Float, _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_R_4_Float, _Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float);
            float _Remap_b4e9369e64e949e880da5427e2de37ac_Out_3_Float;
            Unity_Remap_float(_Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float, float2 (0, 1), float2 (-1, 1), _Remap_b4e9369e64e949e880da5427e2de37ac_Out_3_Float);
            float _Property_5fcd73795f5f4d32a8136c2d77214ef7_Out_0_Float = _CellsIntensity;
            float _Multiply_0003ff715d4349d9b884f7fa02ed336d_Out_2_Float;
            Unity_Multiply_float_float(_Remap_b4e9369e64e949e880da5427e2de37ac_Out_3_Float, _Property_5fcd73795f5f4d32a8136c2d77214ef7_Out_0_Float, _Multiply_0003ff715d4349d9b884f7fa02ed336d_Out_2_Float);
            float4 _Add_5df1c353a4c941d48f93908d8b693661_Out_2_Vector4;
            Unity_Add_float4(_ScreenPosition_fd10197a85f44b469f625df7a4fee1ac_Out_0_Vector4, (_Multiply_0003ff715d4349d9b884f7fa02ed336d_Out_2_Float.xxxx), _Add_5df1c353a4c941d48f93908d8b693661_Out_2_Vector4);
            float2 _Property_61718f0a85b14358b09dbd29786d4112_Out_0_Vector2 = _NoiseTiling;
            float2 _Property_669fd697aeea497ea757ddf406adc31e_Out_0_Vector2 = _NoiseSpeed;
            float2 _Multiply_9341ca9d0a724c2fb64ac31413324f2c_Out_2_Vector2;
            Unity_Multiply_float2_float2((IN.TimeParameters.x.xx), _Property_669fd697aeea497ea757ddf406adc31e_Out_0_Vector2, _Multiply_9341ca9d0a724c2fb64ac31413324f2c_Out_2_Vector2);
            float2 _TilingAndOffset_e203137a933f462cae47a7786a69017b_Out_3_Vector2;
            Unity_TilingAndOffset_float((_Add_5df1c353a4c941d48f93908d8b693661_Out_2_Vector4.xy), _Property_61718f0a85b14358b09dbd29786d4112_Out_0_Vector2, _Multiply_9341ca9d0a724c2fb64ac31413324f2c_Out_2_Vector2, _TilingAndOffset_e203137a933f462cae47a7786a69017b_Out_3_Vector2);
            float4 _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D).tex, UnityBuildTexture2DStructNoScale(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D).samplerstate, UnityBuildTexture2DStructNoScale(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D).GetTransformedUV(_TilingAndOffset_e203137a933f462cae47a7786a69017b_Out_3_Vector2) );
            if (UnityBuildTexture2DStructNoScale(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D).hdrDecode.x > 0)
                _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4, UnityBuildTexture2DStructNoScale(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D).hdrDecode);
            float _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_R_4_Float = _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4.r;
            float _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_G_5_Float = _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4.g;
            float _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_B_6_Float = _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4.b;
            float _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_A_7_Float = _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4.a;
            float _Remap_9915619d36d84b0ebfe6af2b431b96dc_Out_3_Float;
            Unity_Remap_float(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_R_4_Float, float2 (0, 1), float2 (-1, 1), _Remap_9915619d36d84b0ebfe6af2b431b96dc_Out_3_Float);
            float _Property_6b8d94abde1b469c9ef3297cb7e09445_Out_0_Float = _NoiseIntensity;
            float _Multiply_b09f52c62d914ac698c0794266029c75_Out_2_Float;
            Unity_Multiply_float_float(_Remap_9915619d36d84b0ebfe6af2b431b96dc_Out_3_Float, _Property_6b8d94abde1b469c9ef3297cb7e09445_Out_0_Float, _Multiply_b09f52c62d914ac698c0794266029c75_Out_2_Float);
            float4 _Add_d002c03d51f14f30af141e990ebf125b_Out_2_Vector4;
            Unity_Add_float4(_ScreenPosition_fd10197a85f44b469f625df7a4fee1ac_Out_0_Vector4, (_Multiply_b09f52c62d914ac698c0794266029c75_Out_2_Float.xxxx), _Add_d002c03d51f14f30af141e990ebf125b_Out_2_Vector4);
            float4 _Add_7bc501e7b51c415caef9a001d811c6df_Out_2_Vector4;
            Unity_Add_float4(_Add_d002c03d51f14f30af141e990ebf125b_Out_2_Vector4, (_Multiply_0003ff715d4349d9b884f7fa02ed336d_Out_2_Float.xxxx), _Add_7bc501e7b51c415caef9a001d811c6df_Out_2_Vector4);
            float4 _Property_4041d0fd2b654b26a12fe5921ae02a4e_Out_0_Vector4 = _SecondaryCellsTilingOffset;
            float2 _Swizzle_afe1c91ba1b94a8da9622fec61c36808_Out_1_Vector2 = (_Property_4041d0fd2b654b26a12fe5921ae02a4e_Out_0_Vector4.xy).xy;
            float2 _Swizzle_7bad8020207740dc9a08ed5cbafa7a9c_Out_1_Vector2 = _Property_4041d0fd2b654b26a12fe5921ae02a4e_Out_0_Vector4.zw;
            float2 _TilingAndOffset_d84fa061a1be4c66af6b7b69c8e42ee9_Out_3_Vector2;
            Unity_TilingAndOffset_float((_Add_5df1c353a4c941d48f93908d8b693661_Out_2_Vector4.xy), _Swizzle_afe1c91ba1b94a8da9622fec61c36808_Out_1_Vector2, _Swizzle_7bad8020207740dc9a08ed5cbafa7a9c_Out_1_Vector2, _TilingAndOffset_d84fa061a1be4c66af6b7b69c8e42ee9_Out_3_Vector2);
            float4 _SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D).tex, UnityBuildTexture2DStructNoScale(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D).samplerstate, UnityBuildTexture2DStructNoScale(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D).GetTransformedUV(_TilingAndOffset_d84fa061a1be4c66af6b7b69c8e42ee9_Out_3_Vector2) );
            if (UnityBuildTexture2DStructNoScale(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D).hdrDecode.x > 0)
                _SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4, UnityBuildTexture2DStructNoScale(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D).hdrDecode);
            float _SampleTexture2D_2d280125d80e4736968e48ac98141acf_R_4_Float = _SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4.r;
            float _SampleTexture2D_2d280125d80e4736968e48ac98141acf_G_5_Float = _SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4.g;
            float _SampleTexture2D_2d280125d80e4736968e48ac98141acf_B_6_Float = _SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4.b;
            float _SampleTexture2D_2d280125d80e4736968e48ac98141acf_A_7_Float = _SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4.a;
            float _Remap_50f49c7bbb3f4e30a8a3b48af5408b21_Out_3_Float;
            Unity_Remap_float(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_R_4_Float, float2 (0, 1), float2 (-1, 1), _Remap_50f49c7bbb3f4e30a8a3b48af5408b21_Out_3_Float);
            float _Property_f05f92dc6e4f468f8bdd1154535a3d54_Out_0_Float = _SecondaryCellsDistortionAmount;
            float _Multiply_f3882cee4ac545c4aa8f7eb352afd853_Out_2_Float;
            Unity_Multiply_float_float(_Remap_50f49c7bbb3f4e30a8a3b48af5408b21_Out_3_Float, _Property_f05f92dc6e4f468f8bdd1154535a3d54_Out_0_Float, _Multiply_f3882cee4ac545c4aa8f7eb352afd853_Out_2_Float);
            float4 _Add_156fd364558e4068ba15a0f640bc2bfc_Out_2_Vector4;
            Unity_Add_float4(_Add_7bc501e7b51c415caef9a001d811c6df_Out_2_Vector4, (_Multiply_f3882cee4ac545c4aa8f7eb352afd853_Out_2_Float.xxxx), _Add_156fd364558e4068ba15a0f640bc2bfc_Out_2_Vector4);
            float3 _SceneColor_be2bb10b74d74724b26430d8097c9132_Out_1_Vector3;
            Unity_SceneColor_float(_Add_156fd364558e4068ba15a0f640bc2bfc_Out_2_Vector4, _SceneColor_be2bb10b74d74724b26430d8097c9132_Out_1_Vector3);
            float4 _Property_96426d6d56a54e1ca5d788c45c5e9a17_Out_0_Vector4 = _Color;
            float4 _Property_c180718d1cf84124bf9c17ca4739f3b9_Out_0_Vector4 = _Color2;
            float4 _Lerp_bcd4cf1dfeb74a6f9858eaee379c7d36_Out_3_Vector4;
            Unity_Lerp_float4(_Property_96426d6d56a54e1ca5d788c45c5e9a17_Out_0_Vector4, _Property_c180718d1cf84124bf9c17ca4739f3b9_Out_0_Vector4, (_Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float.xxxx), _Lerp_bcd4cf1dfeb74a6f9858eaee379c7d36_Out_3_Vector4);
            float4 _Property_f191c9709ecc402cb9ec1863c504e750_Out_0_Vector4 = _SecondaryCellsHightlightColor;
            float _Property_33ac0341637943b79dc4220e90bc6cb3_Out_0_Float = _SecondaryCellsLightenAmount;
            float _Multiply_0c6f8dc9b9624ddd83466012c1f0473e_Out_2_Float;
            Unity_Multiply_float_float(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_R_4_Float, _Property_33ac0341637943b79dc4220e90bc6cb3_Out_0_Float, _Multiply_0c6f8dc9b9624ddd83466012c1f0473e_Out_2_Float);
            float4 _Lerp_70729269dac64cb8bd68f49a994a0c87_Out_3_Vector4;
            Unity_Lerp_float4(_Lerp_bcd4cf1dfeb74a6f9858eaee379c7d36_Out_3_Vector4, _Property_f191c9709ecc402cb9ec1863c504e750_Out_0_Vector4, (_Multiply_0c6f8dc9b9624ddd83466012c1f0473e_Out_2_Float.xxxx), _Lerp_70729269dac64cb8bd68f49a994a0c87_Out_3_Vector4);
            float2 _Property_7ecb7f3889da4edd8d256eeb7dc379c0_Out_0_Vector2 = _SceneColorLevelsCorrection;
            float _Split_1b1faf2fc072490089f587c7a8dd0365_R_1_Float = _Property_7ecb7f3889da4edd8d256eeb7dc379c0_Out_0_Vector2[0];
            float _Split_1b1faf2fc072490089f587c7a8dd0365_G_2_Float = _Property_7ecb7f3889da4edd8d256eeb7dc379c0_Out_0_Vector2[1];
            float _Split_1b1faf2fc072490089f587c7a8dd0365_B_3_Float = 0;
            float _Split_1b1faf2fc072490089f587c7a8dd0365_A_4_Float = 0;
            float3 _Saturation_62cdd009cbfc43b5a6909a6ed8d85c62_Out_2_Vector3;
            Unity_Saturation_float(_SceneColor_be2bb10b74d74724b26430d8097c9132_Out_1_Vector3, float(0), _Saturation_62cdd009cbfc43b5a6909a6ed8d85c62_Out_2_Vector3);
            float3 _Smoothstep_2ad597db2b9d456398efcdde08dca730_Out_3_Vector3;
            Unity_Smoothstep_float3((_Split_1b1faf2fc072490089f587c7a8dd0365_R_1_Float.xxx), (_Split_1b1faf2fc072490089f587c7a8dd0365_G_2_Float.xxx), _Saturation_62cdd009cbfc43b5a6909a6ed8d85c62_Out_2_Vector3, _Smoothstep_2ad597db2b9d456398efcdde08dca730_Out_3_Vector3);
            float3 _Lerp_b0fa91facd6a432ab71c6b036f65e3a3_Out_3_Vector3;
            Unity_Lerp_float3(_SceneColor_be2bb10b74d74724b26430d8097c9132_Out_1_Vector3, (_Lerp_70729269dac64cb8bd68f49a994a0c87_Out_3_Vector4.xyz), _Smoothstep_2ad597db2b9d456398efcdde08dca730_Out_3_Vector3, _Lerp_b0fa91facd6a432ab71c6b036f65e3a3_Out_3_Vector3);
            float3 _Property_aa59c625101940d5a091f21db9362c1f_Out_0_Vector3 = _ChromaticAberrationColorMax;
            float _Property_0c3440cb57bf462abacb04bf3eda2ee2_Out_0_Float = _ChromaticAberrationOpacity;
            float2 _Property_c658b9f8acd14a91ae75168205cee28c_Out_0_Vector2 = _ChromaticAberrationLevelsCorrection;
            float _Split_be90da803c834a459aa11bd685c539c1_R_1_Float = _Property_c658b9f8acd14a91ae75168205cee28c_Out_0_Vector2[0];
            float _Split_be90da803c834a459aa11bd685c539c1_G_2_Float = _Property_c658b9f8acd14a91ae75168205cee28c_Out_0_Vector2[1];
            float _Split_be90da803c834a459aa11bd685c539c1_B_3_Float = 0;
            float _Split_be90da803c834a459aa11bd685c539c1_A_4_Float = 0;
            float _Property_3d28fedd9c1b4ae8925c13ed8822d26c_Out_0_Float = _ChromaticAberration;
            float _Multiply_66933dbf527542539db7923a55361940_Out_2_Float;
            Unity_Multiply_float_float(_Property_3d28fedd9c1b4ae8925c13ed8822d26c_Out_0_Float, 0.001, _Multiply_66933dbf527542539db7923a55361940_Out_2_Float);
            float _Negate_4f6a77e415ec48d0905951f646fd06d0_Out_1_Float;
            Unity_Negate_float(_Multiply_66933dbf527542539db7923a55361940_Out_2_Float, _Negate_4f6a77e415ec48d0905951f646fd06d0_Out_1_Float);
            float2 _Vector2_60e5e05cb9c94b44b8ef356c8a0cf51a_Out_0_Vector2 = float2(_Negate_4f6a77e415ec48d0905951f646fd06d0_Out_1_Float, _Multiply_66933dbf527542539db7923a55361940_Out_2_Float);
            float2 _Add_ae2604b2df614aa3b9dcb1ecec9f4486_Out_2_Vector2;
            Unity_Add_float2((_Add_156fd364558e4068ba15a0f640bc2bfc_Out_2_Vector4.xy), _Vector2_60e5e05cb9c94b44b8ef356c8a0cf51a_Out_0_Vector2, _Add_ae2604b2df614aa3b9dcb1ecec9f4486_Out_2_Vector2);
            float3 _SceneColor_6efd0253931e4e5883787f89929021e1_Out_1_Vector3;
            Unity_SceneColor_float((float4(_Add_ae2604b2df614aa3b9dcb1ecec9f4486_Out_2_Vector2, 0.0, 1.0)), _SceneColor_6efd0253931e4e5883787f89929021e1_Out_1_Vector3);
            float3 _Saturation_57b2d1dc792d4b9faf785251dadc5558_Out_2_Vector3;
            Unity_Saturation_float(_SceneColor_6efd0253931e4e5883787f89929021e1_Out_1_Vector3, float(0), _Saturation_57b2d1dc792d4b9faf785251dadc5558_Out_2_Vector3);
            float3 _Smoothstep_37ecb3e11f0a4fff9ebd9656d6cecf39_Out_3_Vector3;
            Unity_Smoothstep_float3((_Split_be90da803c834a459aa11bd685c539c1_R_1_Float.xxx), (_Split_be90da803c834a459aa11bd685c539c1_G_2_Float.xxx), _Saturation_57b2d1dc792d4b9faf785251dadc5558_Out_2_Vector3, _Smoothstep_37ecb3e11f0a4fff9ebd9656d6cecf39_Out_3_Vector3);
            float2 _Vector2_6765997c92d54a82a7e0955e0fc51e93_Out_0_Vector2 = float2(_Negate_4f6a77e415ec48d0905951f646fd06d0_Out_1_Float, _Negate_4f6a77e415ec48d0905951f646fd06d0_Out_1_Float);
            float2 _Add_72ed283444024fd8ab4da82b3a998f33_Out_2_Vector2;
            Unity_Add_float2((_Add_156fd364558e4068ba15a0f640bc2bfc_Out_2_Vector4.xy), _Vector2_6765997c92d54a82a7e0955e0fc51e93_Out_0_Vector2, _Add_72ed283444024fd8ab4da82b3a998f33_Out_2_Vector2);
            float3 _SceneColor_e0be9321e1dd40d1bb12f0a0bc1519c1_Out_1_Vector3;
            Unity_SceneColor_float((float4(_Add_72ed283444024fd8ab4da82b3a998f33_Out_2_Vector2, 0.0, 1.0)), _SceneColor_e0be9321e1dd40d1bb12f0a0bc1519c1_Out_1_Vector3);
            float3 _Saturation_ee467f8815fd444391dd4277dbf70b91_Out_2_Vector3;
            Unity_Saturation_float(_SceneColor_e0be9321e1dd40d1bb12f0a0bc1519c1_Out_1_Vector3, float(0), _Saturation_ee467f8815fd444391dd4277dbf70b91_Out_2_Vector3);
            float3 _Smoothstep_c946ec1206c54154af27837cf3914d85_Out_3_Vector3;
            Unity_Smoothstep_float3((_Split_be90da803c834a459aa11bd685c539c1_R_1_Float.xxx), (_Split_be90da803c834a459aa11bd685c539c1_G_2_Float.xxx), _Saturation_ee467f8815fd444391dd4277dbf70b91_Out_2_Vector3, _Smoothstep_c946ec1206c54154af27837cf3914d85_Out_3_Vector3);
            float2 _Vector2_4c1b45d58a7c4335a13602bcb14e057d_Out_0_Vector2 = float2(_Multiply_66933dbf527542539db7923a55361940_Out_2_Float, _Negate_4f6a77e415ec48d0905951f646fd06d0_Out_1_Float);
            float2 _Add_a18e51b946e9474aa6c509838493bcf0_Out_2_Vector2;
            Unity_Add_float2((_Add_156fd364558e4068ba15a0f640bc2bfc_Out_2_Vector4.xy), _Vector2_4c1b45d58a7c4335a13602bcb14e057d_Out_0_Vector2, _Add_a18e51b946e9474aa6c509838493bcf0_Out_2_Vector2);
            float3 _SceneColor_bbadb668faae4fee86cd3d7901ca0b69_Out_1_Vector3;
            Unity_SceneColor_float((float4(_Add_a18e51b946e9474aa6c509838493bcf0_Out_2_Vector2, 0.0, 1.0)), _SceneColor_bbadb668faae4fee86cd3d7901ca0b69_Out_1_Vector3);
            float3 _Saturation_806176d4b1ee4b73a59c78bd57963c23_Out_2_Vector3;
            Unity_Saturation_float(_SceneColor_bbadb668faae4fee86cd3d7901ca0b69_Out_1_Vector3, float(0), _Saturation_806176d4b1ee4b73a59c78bd57963c23_Out_2_Vector3);
            float3 _Smoothstep_f0a69a29cd3b4b43b5dcfa9febb15269_Out_3_Vector3;
            Unity_Smoothstep_float3((_Split_be90da803c834a459aa11bd685c539c1_R_1_Float.xxx), (_Split_be90da803c834a459aa11bd685c539c1_G_2_Float.xxx), _Saturation_806176d4b1ee4b73a59c78bd57963c23_Out_2_Vector3, _Smoothstep_f0a69a29cd3b4b43b5dcfa9febb15269_Out_3_Vector3);
            float4 _Combine_d72d384256874619b4a04639e94f206d_RGBA_4_Vector4;
            float3 _Combine_d72d384256874619b4a04639e94f206d_RGB_5_Vector3;
            float2 _Combine_d72d384256874619b4a04639e94f206d_RG_6_Vector2;
            Unity_Combine_float((_Smoothstep_37ecb3e11f0a4fff9ebd9656d6cecf39_Out_3_Vector3).x, (_Smoothstep_c946ec1206c54154af27837cf3914d85_Out_3_Vector3).x, (_Smoothstep_f0a69a29cd3b4b43b5dcfa9febb15269_Out_3_Vector3).x, float(0), _Combine_d72d384256874619b4a04639e94f206d_RGBA_4_Vector4, _Combine_d72d384256874619b4a04639e94f206d_RGB_5_Vector3, _Combine_d72d384256874619b4a04639e94f206d_RG_6_Vector2);
            float3 _Multiply_266dacc6e1054dd885156572661c7eb1_Out_2_Vector3;
            Unity_Multiply_float3_float3((_Property_0c3440cb57bf462abacb04bf3eda2ee2_Out_0_Float.xxx), _Combine_d72d384256874619b4a04639e94f206d_RGB_5_Vector3, _Multiply_266dacc6e1054dd885156572661c7eb1_Out_2_Vector3);
            float3 _Lerp_3dd9073d874244ae88c8688335cc44a3_Out_3_Vector3;
            Unity_Lerp_float3(_Lerp_b0fa91facd6a432ab71c6b036f65e3a3_Out_3_Vector3, _Property_aa59c625101940d5a091f21db9362c1f_Out_0_Vector3, _Multiply_266dacc6e1054dd885156572661c7eb1_Out_2_Vector3, _Lerp_3dd9073d874244ae88c8688335cc44a3_Out_3_Vector3);
            float4 _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4 = IN.uv0;
            float _Split_e649f2f75c6e474a848600755e877ab3_R_1_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[0];
            float _Split_e649f2f75c6e474a848600755e877ab3_G_2_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[1];
            float _Split_e649f2f75c6e474a848600755e877ab3_B_3_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[2];
            float _Split_e649f2f75c6e474a848600755e877ab3_A_4_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[3];
            float _OneMinus_96ffa84f34534286bacbeac79c1e6a04_Out_1_Float;
            Unity_OneMinus_float(_Split_e649f2f75c6e474a848600755e877ab3_R_1_Float, _OneMinus_96ffa84f34534286bacbeac79c1e6a04_Out_1_Float);
            float _Multiply_55d6e7f4b6d246399079ceee11e52645_Out_2_Float;
            Unity_Multiply_float_float(_Split_e649f2f75c6e474a848600755e877ab3_R_1_Float, _OneMinus_96ffa84f34534286bacbeac79c1e6a04_Out_1_Float, _Multiply_55d6e7f4b6d246399079ceee11e52645_Out_2_Float);
            float _OneMinus_22f755e38c114b9ca586c68d75f661d2_Out_1_Float;
            Unity_OneMinus_float(_Split_e649f2f75c6e474a848600755e877ab3_G_2_Float, _OneMinus_22f755e38c114b9ca586c68d75f661d2_Out_1_Float);
            float _Multiply_df72511ff7b44e8983661009c4ed7d02_Out_2_Float;
            Unity_Multiply_float_float(_Split_e649f2f75c6e474a848600755e877ab3_G_2_Float, _OneMinus_22f755e38c114b9ca586c68d75f661d2_Out_1_Float, _Multiply_df72511ff7b44e8983661009c4ed7d02_Out_2_Float);
            float _Multiply_9d0107fe0fc041d483af0038e916149b_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_55d6e7f4b6d246399079ceee11e52645_Out_2_Float, _Multiply_df72511ff7b44e8983661009c4ed7d02_Out_2_Float, _Multiply_9d0107fe0fc041d483af0038e916149b_Out_2_Float);
            float _Smoothstep_abb9639c55aa4810bae13fb0b4949e74_Out_3_Float;
            Unity_Smoothstep_float(float(0), float(0.05), _Multiply_9d0107fe0fc041d483af0038e916149b_Out_2_Float, _Smoothstep_abb9639c55aa4810bae13fb0b4949e74_Out_3_Float);
            float2 _Property_182212378d374ce8bfa20f4f52d6532d_Out_0_Vector2 = _CircleSmoothstep;
            float _Split_075223437c63458bb5e99e2413cc2604_R_1_Float = _Property_182212378d374ce8bfa20f4f52d6532d_Out_0_Vector2[0];
            float _Split_075223437c63458bb5e99e2413cc2604_G_2_Float = _Property_182212378d374ce8bfa20f4f52d6532d_Out_0_Vector2[1];
            float _Split_075223437c63458bb5e99e2413cc2604_B_3_Float = 0;
            float _Split_075223437c63458bb5e99e2413cc2604_A_4_Float = 0;
            float4 _UV_a8efe86069ac4781a2dc587a5eb92bf9_Out_0_Vector4 = IN.uv0;
            float2 _Vector2_494e0112c5b5405991a62fa7d5437cf9_Out_0_Vector2 = float2(float(0.5), float(0.5));
            float _Distance_b68a33dde5ea4dbb995d9993afa14d01_Out_2_Float;
            Unity_Distance_float2((_UV_a8efe86069ac4781a2dc587a5eb92bf9_Out_0_Vector4.xy), _Vector2_494e0112c5b5405991a62fa7d5437cf9_Out_0_Vector2, _Distance_b68a33dde5ea4dbb995d9993afa14d01_Out_2_Float);
            float _Smoothstep_82f21a94208f4efba46de32f75274968_Out_3_Float;
            Unity_Smoothstep_float(_Split_075223437c63458bb5e99e2413cc2604_G_2_Float, _Split_075223437c63458bb5e99e2413cc2604_R_1_Float, _Distance_b68a33dde5ea4dbb995d9993afa14d01_Out_2_Float, _Smoothstep_82f21a94208f4efba46de32f75274968_Out_3_Float);
            float _Property_5bfabba54df4446ca8c86d35440d67c1_Out_0_Float = _CirclePower;
            float _Power_7c70f6a6e559406e8ae2294ab1eebe04_Out_2_Float;
            Unity_Power_float(_Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float, _Property_5bfabba54df4446ca8c86d35440d67c1_Out_0_Float, _Power_7c70f6a6e559406e8ae2294ab1eebe04_Out_2_Float);
            float _Add_b72754a7e33c41faa48a4ea1fa2edfb3_Out_2_Float;
            Unity_Add_float(_Smoothstep_82f21a94208f4efba46de32f75274968_Out_3_Float, _Power_7c70f6a6e559406e8ae2294ab1eebe04_Out_2_Float, _Add_b72754a7e33c41faa48a4ea1fa2edfb3_Out_2_Float);
            float _Multiply_374c554884fb4f6b9ab82528b2a73e85_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_abb9639c55aa4810bae13fb0b4949e74_Out_3_Float, _Add_b72754a7e33c41faa48a4ea1fa2edfb3_Out_2_Float, _Multiply_374c554884fb4f6b9ab82528b2a73e85_Out_2_Float);
            float _Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float;
            Unity_Saturate_float(_Multiply_374c554884fb4f6b9ab82528b2a73e85_Out_2_Float, _Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float);
            float3 _Lerp_180be64a42cb4958819f855681034de2_Out_3_Vector3;
            Unity_Lerp_float3(_SceneColor_797c5e0605664f268a243b5925dbab4e_Out_1_Vector3, _Lerp_3dd9073d874244ae88c8688335cc44a3_Out_3_Vector3, (_Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float.xxx), _Lerp_180be64a42cb4958819f855681034de2_Out_3_Vector3);
            Bindings_SoftParticles_d0f40856fc355224fab6453746e0507a_float _SoftParticles_2d24478f7f3246149eb145045221b789;
            _SoftParticles_2d24478f7f3246149eb145045221b789.ScreenPosition = IN.ScreenPosition;
            _SoftParticles_2d24478f7f3246149eb145045221b789.NDCPosition = IN.NDCPosition;
            float _SoftParticles_2d24478f7f3246149eb145045221b789_Output1_1_Float;
            SG_SoftParticles_d0f40856fc355224fab6453746e0507a_float(float2 (0, 0.5), _SoftParticles_2d24478f7f3246149eb145045221b789, _SoftParticles_2d24478f7f3246149eb145045221b789_Output1_1_Float);
            float _Multiply_de2953022bc948e69f7268a693c8f3e3_Out_2_Float;
            Unity_Multiply_float_float(_Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float, _SoftParticles_2d24478f7f3246149eb145045221b789_Output1_1_Float, _Multiply_de2953022bc948e69f7268a693c8f3e3_Out_2_Float);
            surface.BaseColor = _Lerp_180be64a42cb4958819f855681034de2_Out_3_Vector3;
            surface.Alpha = _Multiply_de2953022bc948e69f7268a693c8f3e3_Out_2_Float;
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
        
            
        
        
        
        
        
            output.WorldSpacePosition = input.positionWS;
            output.ScreenPosition = ComputeScreenPos(TransformWorldToHClip(input.positionWS), _ProjectionParams.x);
        
            #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x < 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #else
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x > 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScaledScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 = input.texCoord0;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
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
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/UnlitPass.hlsl"
        
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
        Cull Back
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
        // PassKeywords: <None>
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_MOTION_VECTORS
        #define REQUIRE_DEPTH_TEXTURE
        
        
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
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED || defined(ATTRIBUTES_NEED_INSTANCEID)
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
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
             float3 WorldSpacePosition;
             float4 ScreenPosition;
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 texCoord0 : INTERP0;
             float3 positionWS : INTERP1;
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
            output.texCoord0.xyzw = input.texCoord0;
            output.positionWS.xyz = input.positionWS;
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
            output.texCoord0 = input.texCoord0.xyzw;
            output.positionWS = input.positionWS.xyz;
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
        float4 _SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D_TexelSize;
        float4 _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D_TexelSize;
        float4 _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D_TexelSize;
        float4 _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D_TexelSize;
        float4 _Color2;
        float4 _Color;
        float _CellsSpeed2;
        float _CellsSpeed1;
        float2 _CellsTiling;
        float2 _SceneColorLevelsCorrection;
        float _NoiseIntensity;
        float _CellsIntensity;
        float _SceneColorClamp;
        float2 _CircleSmoothstep;
        float _CirclePower;
        float _ChromaticAberration;
        float _SecondaryCellsLightenAmount;
        float4 _SecondaryCellsHightlightColor;
        float _SecondaryCellsDistortionAmount;
        float4 _SecondaryCellsTilingOffset;
        float _ChromaticAberrationOpacity;
        float2 _ChromaticAberrationLevelsCorrection;
        float3 _ChromaticAberrationColorMax;
        float2 _NoiseTiling;
        float2 _NoiseSpeed;
        UNITY_TEXTURE_STREAMING_DEBUG_VARS;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D);
        TEXTURE2D(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D);
        TEXTURE2D(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D);
        TEXTURE2D(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D);
        
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
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        void Unity_Distance_float2(float2 A, float2 B, out float Out)
        {
            Out = distance(A, B);
        }
        
        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            Rotation = Rotation * (3.1415926f/180.0f);
            UV -= Center;
            float s, c;
            sincos(Rotation, s, c);
            float3 r3 = float3(-s, c, s);
            float2 r1;
            r1.y = dot(UV, r3.xy);
            r1.x = dot(UV, r3.yz);
            Out = r1 + Center;
        }
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        void Unity_Maximum_float(float A, float B, out float Out)
        {
            Out = max(A, B);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_SceneDepth_Linear01_float(float4 UV, out float Out)
        {
            Out = Linear01Depth(SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), _ZBufferParams);
        }
        
        void Unity_Subtract_float(float A, float B, out float Out)
        {
            Out = A - B;
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        struct Bindings_SoftParticles_d0f40856fc355224fab6453746e0507a_float
        {
        float4 ScreenPosition;
        float2 NDCPosition;
        };
        
        void SG_SoftParticles_d0f40856fc355224fab6453746e0507a_float(float2 Vector2_AC648482, Bindings_SoftParticles_d0f40856fc355224fab6453746e0507a_float IN, out float Output1_1)
        {
        float _SceneDepth_4582691621dbf18cb0eb2f8d5aa469b7_Out_1_Float;
        Unity_SceneDepth_Linear01_float(float4(IN.NDCPosition.xy, 0, 0), _SceneDepth_4582691621dbf18cb0eb2f8d5aa469b7_Out_1_Float);
        float _Multiply_abd94f3d0b36a08380f6ab11443878f2_Out_2_Float;
        Unity_Multiply_float_float(_SceneDepth_4582691621dbf18cb0eb2f8d5aa469b7_Out_1_Float, _ProjectionParams.z, _Multiply_abd94f3d0b36a08380f6ab11443878f2_Out_2_Float);
        float4 _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4 = IN.ScreenPosition;
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_R_1_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[0];
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_G_2_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[1];
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_B_3_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[2];
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_A_4_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[3];
        float _Subtract_f130a0b621d472829a7045e95984f2a9_Out_2_Float;
        Unity_Subtract_float(_Multiply_abd94f3d0b36a08380f6ab11443878f2_Out_2_Float, _Split_8ee3f43b8caa0c8ab120e9a242f543ca_A_4_Float, _Subtract_f130a0b621d472829a7045e95984f2a9_Out_2_Float);
        float2 _Property_90bb3b6ce940ef868c970ea6aae7c26b_Out_0_Vector2 = Vector2_AC648482;
        float _Remap_db5867ad2542128e864c4c228cc1f3f5_Out_3_Float;
        Unity_Remap_float(_Subtract_f130a0b621d472829a7045e95984f2a9_Out_2_Float, _Property_90bb3b6ce940ef868c970ea6aae7c26b_Out_0_Vector2, float2 (0, 1), _Remap_db5867ad2542128e864c4c228cc1f3f5_Out_3_Float);
        float _Saturate_377f82d9fb446684a06124602b80b432_Out_1_Float;
        Unity_Saturate_float(_Remap_db5867ad2542128e864c4c228cc1f3f5_Out_3_Float, _Saturate_377f82d9fb446684a06124602b80b432_Out_1_Float);
        Output1_1 = _Saturate_377f82d9fb446684a06124602b80b432_Out_1_Float;
        }
        
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
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4 = IN.uv0;
            float _Split_e649f2f75c6e474a848600755e877ab3_R_1_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[0];
            float _Split_e649f2f75c6e474a848600755e877ab3_G_2_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[1];
            float _Split_e649f2f75c6e474a848600755e877ab3_B_3_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[2];
            float _Split_e649f2f75c6e474a848600755e877ab3_A_4_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[3];
            float _OneMinus_96ffa84f34534286bacbeac79c1e6a04_Out_1_Float;
            Unity_OneMinus_float(_Split_e649f2f75c6e474a848600755e877ab3_R_1_Float, _OneMinus_96ffa84f34534286bacbeac79c1e6a04_Out_1_Float);
            float _Multiply_55d6e7f4b6d246399079ceee11e52645_Out_2_Float;
            Unity_Multiply_float_float(_Split_e649f2f75c6e474a848600755e877ab3_R_1_Float, _OneMinus_96ffa84f34534286bacbeac79c1e6a04_Out_1_Float, _Multiply_55d6e7f4b6d246399079ceee11e52645_Out_2_Float);
            float _OneMinus_22f755e38c114b9ca586c68d75f661d2_Out_1_Float;
            Unity_OneMinus_float(_Split_e649f2f75c6e474a848600755e877ab3_G_2_Float, _OneMinus_22f755e38c114b9ca586c68d75f661d2_Out_1_Float);
            float _Multiply_df72511ff7b44e8983661009c4ed7d02_Out_2_Float;
            Unity_Multiply_float_float(_Split_e649f2f75c6e474a848600755e877ab3_G_2_Float, _OneMinus_22f755e38c114b9ca586c68d75f661d2_Out_1_Float, _Multiply_df72511ff7b44e8983661009c4ed7d02_Out_2_Float);
            float _Multiply_9d0107fe0fc041d483af0038e916149b_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_55d6e7f4b6d246399079ceee11e52645_Out_2_Float, _Multiply_df72511ff7b44e8983661009c4ed7d02_Out_2_Float, _Multiply_9d0107fe0fc041d483af0038e916149b_Out_2_Float);
            float _Smoothstep_abb9639c55aa4810bae13fb0b4949e74_Out_3_Float;
            Unity_Smoothstep_float(float(0), float(0.05), _Multiply_9d0107fe0fc041d483af0038e916149b_Out_2_Float, _Smoothstep_abb9639c55aa4810bae13fb0b4949e74_Out_3_Float);
            float2 _Property_182212378d374ce8bfa20f4f52d6532d_Out_0_Vector2 = _CircleSmoothstep;
            float _Split_075223437c63458bb5e99e2413cc2604_R_1_Float = _Property_182212378d374ce8bfa20f4f52d6532d_Out_0_Vector2[0];
            float _Split_075223437c63458bb5e99e2413cc2604_G_2_Float = _Property_182212378d374ce8bfa20f4f52d6532d_Out_0_Vector2[1];
            float _Split_075223437c63458bb5e99e2413cc2604_B_3_Float = 0;
            float _Split_075223437c63458bb5e99e2413cc2604_A_4_Float = 0;
            float4 _UV_a8efe86069ac4781a2dc587a5eb92bf9_Out_0_Vector4 = IN.uv0;
            float2 _Vector2_494e0112c5b5405991a62fa7d5437cf9_Out_0_Vector2 = float2(float(0.5), float(0.5));
            float _Distance_b68a33dde5ea4dbb995d9993afa14d01_Out_2_Float;
            Unity_Distance_float2((_UV_a8efe86069ac4781a2dc587a5eb92bf9_Out_0_Vector4.xy), _Vector2_494e0112c5b5405991a62fa7d5437cf9_Out_0_Vector2, _Distance_b68a33dde5ea4dbb995d9993afa14d01_Out_2_Float);
            float _Smoothstep_82f21a94208f4efba46de32f75274968_Out_3_Float;
            Unity_Smoothstep_float(_Split_075223437c63458bb5e99e2413cc2604_G_2_Float, _Split_075223437c63458bb5e99e2413cc2604_R_1_Float, _Distance_b68a33dde5ea4dbb995d9993afa14d01_Out_2_Float, _Smoothstep_82f21a94208f4efba46de32f75274968_Out_3_Float);
            float2 _Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2;
            Unity_Rotate_Degrees_float(IN.uv0.xy, float2 (0.5, 0.5), float(200.5), _Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2);
            float2 _Property_db2bf3c93cbf4880929bbbbd5b8d32c7_Out_0_Vector2 = _CellsTiling;
            float _Property_20fbe3c647284f08addb9b4125ce6af1_Out_0_Float = _CellsSpeed1;
            float _Multiply_e2672b0aac2d4a33a0d6cc512462530c_Out_2_Float;
            Unity_Multiply_float_float(IN.TimeParameters.x, _Property_20fbe3c647284f08addb9b4125ce6af1_Out_0_Float, _Multiply_e2672b0aac2d4a33a0d6cc512462530c_Out_2_Float);
            float2 _Vector2_0f755d5a1ff04d9b9de7cb63561d44f7_Out_0_Vector2 = float2(float(0), _Multiply_e2672b0aac2d4a33a0d6cc512462530c_Out_2_Float);
            float2 _TilingAndOffset_be8cabdd23314d6f9cb006714bd1aabe_Out_3_Vector2;
            Unity_TilingAndOffset_float(_Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2, _Property_db2bf3c93cbf4880929bbbbd5b8d32c7_Out_0_Vector2, _Vector2_0f755d5a1ff04d9b9de7cb63561d44f7_Out_0_Vector2, _TilingAndOffset_be8cabdd23314d6f9cb006714bd1aabe_Out_3_Vector2);
            float4 _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).tex, UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).samplerstate, UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).GetTransformedUV(_TilingAndOffset_be8cabdd23314d6f9cb006714bd1aabe_Out_3_Vector2) );
            if (UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).hdrDecode.x > 0)
                _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4, UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).hdrDecode);
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_R_4_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.r;
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_G_5_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.g;
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_B_6_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.b;
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_A_7_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.a;
            float _Property_028d8bf17bf4473788d23219249f7451_Out_0_Float = _CellsSpeed2;
            float _Multiply_f0eecebc84f54abc89ef38eaa68f0774_Out_2_Float;
            Unity_Multiply_float_float(IN.TimeParameters.x, _Property_028d8bf17bf4473788d23219249f7451_Out_0_Float, _Multiply_f0eecebc84f54abc89ef38eaa68f0774_Out_2_Float);
            float2 _Vector2_f174d0c85fa546a58eb678ccf778d92c_Out_0_Vector2 = float2(float(0.2), _Multiply_f0eecebc84f54abc89ef38eaa68f0774_Out_2_Float);
            float2 _TilingAndOffset_8f83ef02ec4b473a9913eced11cf3340_Out_3_Vector2;
            Unity_TilingAndOffset_float(_Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2, _Property_db2bf3c93cbf4880929bbbbd5b8d32c7_Out_0_Vector2, _Vector2_f174d0c85fa546a58eb678ccf778d92c_Out_0_Vector2, _TilingAndOffset_8f83ef02ec4b473a9913eced11cf3340_Out_3_Vector2);
            float4 _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).tex, UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).samplerstate, UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).GetTransformedUV(_TilingAndOffset_8f83ef02ec4b473a9913eced11cf3340_Out_3_Vector2) );
            if (UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).hdrDecode.x > 0)
                _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4, UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).hdrDecode);
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_R_4_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.r;
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_G_5_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.g;
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_B_6_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.b;
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_A_7_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.a;
            float _Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float;
            Unity_Maximum_float(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_R_4_Float, _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_R_4_Float, _Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float);
            float _Property_5bfabba54df4446ca8c86d35440d67c1_Out_0_Float = _CirclePower;
            float _Power_7c70f6a6e559406e8ae2294ab1eebe04_Out_2_Float;
            Unity_Power_float(_Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float, _Property_5bfabba54df4446ca8c86d35440d67c1_Out_0_Float, _Power_7c70f6a6e559406e8ae2294ab1eebe04_Out_2_Float);
            float _Add_b72754a7e33c41faa48a4ea1fa2edfb3_Out_2_Float;
            Unity_Add_float(_Smoothstep_82f21a94208f4efba46de32f75274968_Out_3_Float, _Power_7c70f6a6e559406e8ae2294ab1eebe04_Out_2_Float, _Add_b72754a7e33c41faa48a4ea1fa2edfb3_Out_2_Float);
            float _Multiply_374c554884fb4f6b9ab82528b2a73e85_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_abb9639c55aa4810bae13fb0b4949e74_Out_3_Float, _Add_b72754a7e33c41faa48a4ea1fa2edfb3_Out_2_Float, _Multiply_374c554884fb4f6b9ab82528b2a73e85_Out_2_Float);
            float _Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float;
            Unity_Saturate_float(_Multiply_374c554884fb4f6b9ab82528b2a73e85_Out_2_Float, _Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float);
            Bindings_SoftParticles_d0f40856fc355224fab6453746e0507a_float _SoftParticles_2d24478f7f3246149eb145045221b789;
            _SoftParticles_2d24478f7f3246149eb145045221b789.ScreenPosition = IN.ScreenPosition;
            _SoftParticles_2d24478f7f3246149eb145045221b789.NDCPosition = IN.NDCPosition;
            float _SoftParticles_2d24478f7f3246149eb145045221b789_Output1_1_Float;
            SG_SoftParticles_d0f40856fc355224fab6453746e0507a_float(float2 (0, 0.5), _SoftParticles_2d24478f7f3246149eb145045221b789, _SoftParticles_2d24478f7f3246149eb145045221b789_Output1_1_Float);
            float _Multiply_de2953022bc948e69f7268a693c8f3e3_Out_2_Float;
            Unity_Multiply_float_float(_Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float, _SoftParticles_2d24478f7f3246149eb145045221b789_Output1_1_Float, _Multiply_de2953022bc948e69f7268a693c8f3e3_Out_2_Float);
            surface.Alpha = _Multiply_de2953022bc948e69f7268a693c8f3e3_Out_2_Float;
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
        
            
        
        
        
        
        
            output.WorldSpacePosition = input.positionWS;
            output.ScreenPosition = ComputeScreenPos(TransformWorldToHClip(input.positionWS), _ProjectionParams.x);
        
            #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x < 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #else
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x > 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScaledScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 = input.texCoord0;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
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
            Name "DepthNormalsOnly"
            Tags
            {
                "LightMode" = "DepthNormalsOnly"
            }
        
        // Render State
        Cull Back
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
        #pragma multi_compile_fragment _ _GBUFFER_NORMALS_OCT
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX_NORMAL_OUTPUT
        #define FEATURES_GRAPH_VERTEX_TANGENT_OUTPUT
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHNORMALSONLY
        #define _SURFACE_TYPE_TRANSPARENT 1
        #define REQUIRE_DEPTH_TEXTURE
        
        
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
            #if UNITY_ANY_INSTANCING_ENABLED || defined(ATTRIBUTES_NEED_INSTANCEID)
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
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
             float3 WorldSpacePosition;
             float4 ScreenPosition;
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float3 TimeParameters;
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
             float4 texCoord0 : INTERP0;
             float3 positionWS : INTERP1;
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
            output.texCoord0.xyzw = input.texCoord0;
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
            output.texCoord0 = input.texCoord0.xyzw;
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
        float4 _SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D_TexelSize;
        float4 _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D_TexelSize;
        float4 _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D_TexelSize;
        float4 _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D_TexelSize;
        float4 _Color2;
        float4 _Color;
        float _CellsSpeed2;
        float _CellsSpeed1;
        float2 _CellsTiling;
        float2 _SceneColorLevelsCorrection;
        float _NoiseIntensity;
        float _CellsIntensity;
        float _SceneColorClamp;
        float2 _CircleSmoothstep;
        float _CirclePower;
        float _ChromaticAberration;
        float _SecondaryCellsLightenAmount;
        float4 _SecondaryCellsHightlightColor;
        float _SecondaryCellsDistortionAmount;
        float4 _SecondaryCellsTilingOffset;
        float _ChromaticAberrationOpacity;
        float2 _ChromaticAberrationLevelsCorrection;
        float3 _ChromaticAberrationColorMax;
        float2 _NoiseTiling;
        float2 _NoiseSpeed;
        UNITY_TEXTURE_STREAMING_DEBUG_VARS;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D);
        TEXTURE2D(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D);
        TEXTURE2D(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D);
        TEXTURE2D(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D);
        
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
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        void Unity_Distance_float2(float2 A, float2 B, out float Out)
        {
            Out = distance(A, B);
        }
        
        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            Rotation = Rotation * (3.1415926f/180.0f);
            UV -= Center;
            float s, c;
            sincos(Rotation, s, c);
            float3 r3 = float3(-s, c, s);
            float2 r1;
            r1.y = dot(UV, r3.xy);
            r1.x = dot(UV, r3.yz);
            Out = r1 + Center;
        }
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        void Unity_Maximum_float(float A, float B, out float Out)
        {
            Out = max(A, B);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_SceneDepth_Linear01_float(float4 UV, out float Out)
        {
            Out = Linear01Depth(SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), _ZBufferParams);
        }
        
        void Unity_Subtract_float(float A, float B, out float Out)
        {
            Out = A - B;
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        struct Bindings_SoftParticles_d0f40856fc355224fab6453746e0507a_float
        {
        float4 ScreenPosition;
        float2 NDCPosition;
        };
        
        void SG_SoftParticles_d0f40856fc355224fab6453746e0507a_float(float2 Vector2_AC648482, Bindings_SoftParticles_d0f40856fc355224fab6453746e0507a_float IN, out float Output1_1)
        {
        float _SceneDepth_4582691621dbf18cb0eb2f8d5aa469b7_Out_1_Float;
        Unity_SceneDepth_Linear01_float(float4(IN.NDCPosition.xy, 0, 0), _SceneDepth_4582691621dbf18cb0eb2f8d5aa469b7_Out_1_Float);
        float _Multiply_abd94f3d0b36a08380f6ab11443878f2_Out_2_Float;
        Unity_Multiply_float_float(_SceneDepth_4582691621dbf18cb0eb2f8d5aa469b7_Out_1_Float, _ProjectionParams.z, _Multiply_abd94f3d0b36a08380f6ab11443878f2_Out_2_Float);
        float4 _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4 = IN.ScreenPosition;
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_R_1_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[0];
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_G_2_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[1];
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_B_3_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[2];
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_A_4_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[3];
        float _Subtract_f130a0b621d472829a7045e95984f2a9_Out_2_Float;
        Unity_Subtract_float(_Multiply_abd94f3d0b36a08380f6ab11443878f2_Out_2_Float, _Split_8ee3f43b8caa0c8ab120e9a242f543ca_A_4_Float, _Subtract_f130a0b621d472829a7045e95984f2a9_Out_2_Float);
        float2 _Property_90bb3b6ce940ef868c970ea6aae7c26b_Out_0_Vector2 = Vector2_AC648482;
        float _Remap_db5867ad2542128e864c4c228cc1f3f5_Out_3_Float;
        Unity_Remap_float(_Subtract_f130a0b621d472829a7045e95984f2a9_Out_2_Float, _Property_90bb3b6ce940ef868c970ea6aae7c26b_Out_0_Vector2, float2 (0, 1), _Remap_db5867ad2542128e864c4c228cc1f3f5_Out_3_Float);
        float _Saturate_377f82d9fb446684a06124602b80b432_Out_1_Float;
        Unity_Saturate_float(_Remap_db5867ad2542128e864c4c228cc1f3f5_Out_3_Float, _Saturate_377f82d9fb446684a06124602b80b432_Out_1_Float);
        Output1_1 = _Saturate_377f82d9fb446684a06124602b80b432_Out_1_Float;
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
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4 = IN.uv0;
            float _Split_e649f2f75c6e474a848600755e877ab3_R_1_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[0];
            float _Split_e649f2f75c6e474a848600755e877ab3_G_2_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[1];
            float _Split_e649f2f75c6e474a848600755e877ab3_B_3_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[2];
            float _Split_e649f2f75c6e474a848600755e877ab3_A_4_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[3];
            float _OneMinus_96ffa84f34534286bacbeac79c1e6a04_Out_1_Float;
            Unity_OneMinus_float(_Split_e649f2f75c6e474a848600755e877ab3_R_1_Float, _OneMinus_96ffa84f34534286bacbeac79c1e6a04_Out_1_Float);
            float _Multiply_55d6e7f4b6d246399079ceee11e52645_Out_2_Float;
            Unity_Multiply_float_float(_Split_e649f2f75c6e474a848600755e877ab3_R_1_Float, _OneMinus_96ffa84f34534286bacbeac79c1e6a04_Out_1_Float, _Multiply_55d6e7f4b6d246399079ceee11e52645_Out_2_Float);
            float _OneMinus_22f755e38c114b9ca586c68d75f661d2_Out_1_Float;
            Unity_OneMinus_float(_Split_e649f2f75c6e474a848600755e877ab3_G_2_Float, _OneMinus_22f755e38c114b9ca586c68d75f661d2_Out_1_Float);
            float _Multiply_df72511ff7b44e8983661009c4ed7d02_Out_2_Float;
            Unity_Multiply_float_float(_Split_e649f2f75c6e474a848600755e877ab3_G_2_Float, _OneMinus_22f755e38c114b9ca586c68d75f661d2_Out_1_Float, _Multiply_df72511ff7b44e8983661009c4ed7d02_Out_2_Float);
            float _Multiply_9d0107fe0fc041d483af0038e916149b_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_55d6e7f4b6d246399079ceee11e52645_Out_2_Float, _Multiply_df72511ff7b44e8983661009c4ed7d02_Out_2_Float, _Multiply_9d0107fe0fc041d483af0038e916149b_Out_2_Float);
            float _Smoothstep_abb9639c55aa4810bae13fb0b4949e74_Out_3_Float;
            Unity_Smoothstep_float(float(0), float(0.05), _Multiply_9d0107fe0fc041d483af0038e916149b_Out_2_Float, _Smoothstep_abb9639c55aa4810bae13fb0b4949e74_Out_3_Float);
            float2 _Property_182212378d374ce8bfa20f4f52d6532d_Out_0_Vector2 = _CircleSmoothstep;
            float _Split_075223437c63458bb5e99e2413cc2604_R_1_Float = _Property_182212378d374ce8bfa20f4f52d6532d_Out_0_Vector2[0];
            float _Split_075223437c63458bb5e99e2413cc2604_G_2_Float = _Property_182212378d374ce8bfa20f4f52d6532d_Out_0_Vector2[1];
            float _Split_075223437c63458bb5e99e2413cc2604_B_3_Float = 0;
            float _Split_075223437c63458bb5e99e2413cc2604_A_4_Float = 0;
            float4 _UV_a8efe86069ac4781a2dc587a5eb92bf9_Out_0_Vector4 = IN.uv0;
            float2 _Vector2_494e0112c5b5405991a62fa7d5437cf9_Out_0_Vector2 = float2(float(0.5), float(0.5));
            float _Distance_b68a33dde5ea4dbb995d9993afa14d01_Out_2_Float;
            Unity_Distance_float2((_UV_a8efe86069ac4781a2dc587a5eb92bf9_Out_0_Vector4.xy), _Vector2_494e0112c5b5405991a62fa7d5437cf9_Out_0_Vector2, _Distance_b68a33dde5ea4dbb995d9993afa14d01_Out_2_Float);
            float _Smoothstep_82f21a94208f4efba46de32f75274968_Out_3_Float;
            Unity_Smoothstep_float(_Split_075223437c63458bb5e99e2413cc2604_G_2_Float, _Split_075223437c63458bb5e99e2413cc2604_R_1_Float, _Distance_b68a33dde5ea4dbb995d9993afa14d01_Out_2_Float, _Smoothstep_82f21a94208f4efba46de32f75274968_Out_3_Float);
            float2 _Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2;
            Unity_Rotate_Degrees_float(IN.uv0.xy, float2 (0.5, 0.5), float(200.5), _Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2);
            float2 _Property_db2bf3c93cbf4880929bbbbd5b8d32c7_Out_0_Vector2 = _CellsTiling;
            float _Property_20fbe3c647284f08addb9b4125ce6af1_Out_0_Float = _CellsSpeed1;
            float _Multiply_e2672b0aac2d4a33a0d6cc512462530c_Out_2_Float;
            Unity_Multiply_float_float(IN.TimeParameters.x, _Property_20fbe3c647284f08addb9b4125ce6af1_Out_0_Float, _Multiply_e2672b0aac2d4a33a0d6cc512462530c_Out_2_Float);
            float2 _Vector2_0f755d5a1ff04d9b9de7cb63561d44f7_Out_0_Vector2 = float2(float(0), _Multiply_e2672b0aac2d4a33a0d6cc512462530c_Out_2_Float);
            float2 _TilingAndOffset_be8cabdd23314d6f9cb006714bd1aabe_Out_3_Vector2;
            Unity_TilingAndOffset_float(_Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2, _Property_db2bf3c93cbf4880929bbbbd5b8d32c7_Out_0_Vector2, _Vector2_0f755d5a1ff04d9b9de7cb63561d44f7_Out_0_Vector2, _TilingAndOffset_be8cabdd23314d6f9cb006714bd1aabe_Out_3_Vector2);
            float4 _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).tex, UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).samplerstate, UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).GetTransformedUV(_TilingAndOffset_be8cabdd23314d6f9cb006714bd1aabe_Out_3_Vector2) );
            if (UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).hdrDecode.x > 0)
                _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4, UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).hdrDecode);
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_R_4_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.r;
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_G_5_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.g;
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_B_6_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.b;
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_A_7_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.a;
            float _Property_028d8bf17bf4473788d23219249f7451_Out_0_Float = _CellsSpeed2;
            float _Multiply_f0eecebc84f54abc89ef38eaa68f0774_Out_2_Float;
            Unity_Multiply_float_float(IN.TimeParameters.x, _Property_028d8bf17bf4473788d23219249f7451_Out_0_Float, _Multiply_f0eecebc84f54abc89ef38eaa68f0774_Out_2_Float);
            float2 _Vector2_f174d0c85fa546a58eb678ccf778d92c_Out_0_Vector2 = float2(float(0.2), _Multiply_f0eecebc84f54abc89ef38eaa68f0774_Out_2_Float);
            float2 _TilingAndOffset_8f83ef02ec4b473a9913eced11cf3340_Out_3_Vector2;
            Unity_TilingAndOffset_float(_Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2, _Property_db2bf3c93cbf4880929bbbbd5b8d32c7_Out_0_Vector2, _Vector2_f174d0c85fa546a58eb678ccf778d92c_Out_0_Vector2, _TilingAndOffset_8f83ef02ec4b473a9913eced11cf3340_Out_3_Vector2);
            float4 _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).tex, UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).samplerstate, UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).GetTransformedUV(_TilingAndOffset_8f83ef02ec4b473a9913eced11cf3340_Out_3_Vector2) );
            if (UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).hdrDecode.x > 0)
                _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4, UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).hdrDecode);
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_R_4_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.r;
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_G_5_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.g;
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_B_6_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.b;
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_A_7_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.a;
            float _Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float;
            Unity_Maximum_float(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_R_4_Float, _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_R_4_Float, _Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float);
            float _Property_5bfabba54df4446ca8c86d35440d67c1_Out_0_Float = _CirclePower;
            float _Power_7c70f6a6e559406e8ae2294ab1eebe04_Out_2_Float;
            Unity_Power_float(_Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float, _Property_5bfabba54df4446ca8c86d35440d67c1_Out_0_Float, _Power_7c70f6a6e559406e8ae2294ab1eebe04_Out_2_Float);
            float _Add_b72754a7e33c41faa48a4ea1fa2edfb3_Out_2_Float;
            Unity_Add_float(_Smoothstep_82f21a94208f4efba46de32f75274968_Out_3_Float, _Power_7c70f6a6e559406e8ae2294ab1eebe04_Out_2_Float, _Add_b72754a7e33c41faa48a4ea1fa2edfb3_Out_2_Float);
            float _Multiply_374c554884fb4f6b9ab82528b2a73e85_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_abb9639c55aa4810bae13fb0b4949e74_Out_3_Float, _Add_b72754a7e33c41faa48a4ea1fa2edfb3_Out_2_Float, _Multiply_374c554884fb4f6b9ab82528b2a73e85_Out_2_Float);
            float _Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float;
            Unity_Saturate_float(_Multiply_374c554884fb4f6b9ab82528b2a73e85_Out_2_Float, _Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float);
            Bindings_SoftParticles_d0f40856fc355224fab6453746e0507a_float _SoftParticles_2d24478f7f3246149eb145045221b789;
            _SoftParticles_2d24478f7f3246149eb145045221b789.ScreenPosition = IN.ScreenPosition;
            _SoftParticles_2d24478f7f3246149eb145045221b789.NDCPosition = IN.NDCPosition;
            float _SoftParticles_2d24478f7f3246149eb145045221b789_Output1_1_Float;
            SG_SoftParticles_d0f40856fc355224fab6453746e0507a_float(float2 (0, 0.5), _SoftParticles_2d24478f7f3246149eb145045221b789, _SoftParticles_2d24478f7f3246149eb145045221b789_Output1_1_Float);
            float _Multiply_de2953022bc948e69f7268a693c8f3e3_Out_2_Float;
            Unity_Multiply_float_float(_Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float, _SoftParticles_2d24478f7f3246149eb145045221b789_Output1_1_Float, _Multiply_de2953022bc948e69f7268a693c8f3e3_Out_2_Float);
            surface.Alpha = _Multiply_de2953022bc948e69f7268a693c8f3e3_Out_2_Float;
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
        
            
        
        
        
        
        
            output.WorldSpacePosition = input.positionWS;
            output.ScreenPosition = ComputeScreenPos(TransformWorldToHClip(input.positionWS), _ProjectionParams.x);
        
            #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x < 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #else
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x > 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScaledScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 = input.texCoord0;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
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
            Name "GBuffer"
            Tags
            {
                "LightMode" = "UniversalGBuffer"
            }
        
        // Render State
        Cull Back
        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        ZTest LEqual
        ZWrite Off
        
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
        #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
        #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
        #pragma multi_compile_fragment _ _RENDER_PASS_ENABLED
        #pragma multi_compile_fragment _ _GBUFFER_NORMALS_OCT
        #pragma multi_compile _ SHADOWS_SHADOWMASK
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX_NORMAL_OUTPUT
        #define FEATURES_GRAPH_VERTEX_TANGENT_OUTPUT
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_GBUFFER
        #define _SURFACE_TYPE_TRANSPARENT 1
        #define REQUIRE_DEPTH_TEXTURE
        #define REQUIRE_OPAQUE_TEXTURE
        
        
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
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"
        #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"
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
            #if UNITY_ANY_INSTANCING_ENABLED || defined(ATTRIBUTES_NEED_INSTANCEID)
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
             float4 texCoord0;
            #if !defined(LIGHTMAP_ON)
             float3 sh;
            #endif
            #if defined(USE_APV_PROBE_OCCLUSION)
             float4 probeOcclusion;
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
             float3 WorldSpacePosition;
             float4 ScreenPosition;
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float3 TimeParameters;
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
            #if !defined(LIGHTMAP_ON)
             float3 sh : INTERP0;
            #endif
            #if defined(USE_APV_PROBE_OCCLUSION)
             float4 probeOcclusion : INTERP1;
            #endif
             float4 texCoord0 : INTERP2;
             float3 positionWS : INTERP3;
             float3 normalWS : INTERP4;
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
            #if !defined(LIGHTMAP_ON)
            output.sh = input.sh;
            #endif
            #if defined(USE_APV_PROBE_OCCLUSION)
            output.probeOcclusion = input.probeOcclusion;
            #endif
            output.texCoord0.xyzw = input.texCoord0;
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
            #if !defined(LIGHTMAP_ON)
            output.sh = input.sh;
            #endif
            #if defined(USE_APV_PROBE_OCCLUSION)
            output.probeOcclusion = input.probeOcclusion;
            #endif
            output.texCoord0 = input.texCoord0.xyzw;
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
        float4 _SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D_TexelSize;
        float4 _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D_TexelSize;
        float4 _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D_TexelSize;
        float4 _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D_TexelSize;
        float4 _Color2;
        float4 _Color;
        float _CellsSpeed2;
        float _CellsSpeed1;
        float2 _CellsTiling;
        float2 _SceneColorLevelsCorrection;
        float _NoiseIntensity;
        float _CellsIntensity;
        float _SceneColorClamp;
        float2 _CircleSmoothstep;
        float _CirclePower;
        float _ChromaticAberration;
        float _SecondaryCellsLightenAmount;
        float4 _SecondaryCellsHightlightColor;
        float _SecondaryCellsDistortionAmount;
        float4 _SecondaryCellsTilingOffset;
        float _ChromaticAberrationOpacity;
        float2 _ChromaticAberrationLevelsCorrection;
        float3 _ChromaticAberrationColorMax;
        float2 _NoiseTiling;
        float2 _NoiseSpeed;
        UNITY_TEXTURE_STREAMING_DEBUG_VARS;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D);
        TEXTURE2D(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D);
        TEXTURE2D(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D);
        TEXTURE2D(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D);
        
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
        
        void Unity_SceneColor_float(float4 UV, out float3 Out)
        {
            Out = SHADERGRAPH_SAMPLE_SCENE_COLOR(UV.xy);
        }
        
        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            Rotation = Rotation * (3.1415926f/180.0f);
            UV -= Center;
            float s, c;
            sincos(Rotation, s, c);
            float3 r3 = float3(-s, c, s);
            float2 r1;
            r1.y = dot(UV, r3.xy);
            r1.x = dot(UV, r3.yz);
            Out = r1 + Center;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        void Unity_Maximum_float(float A, float B, out float Out)
        {
            Out = max(A, B);
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        void Unity_Add_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A + B;
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
        {
            Out = lerp(A, B, T);
        }
        
        void Unity_Saturation_float(float3 In, float Saturation, out float3 Out)
        {
            float luma = dot(In, float3(0.2126729, 0.7151522, 0.0721750));
            Out =  luma.xxx + Saturation.xxx * (In - luma.xxx);
        }
        
        void Unity_Smoothstep_float3(float3 Edge1, float3 Edge2, float3 In, out float3 Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        void Unity_Lerp_float3(float3 A, float3 B, float3 T, out float3 Out)
        {
            Out = lerp(A, B, T);
        }
        
        void Unity_Negate_float(float In, out float Out)
        {
            Out = -1 * In;
        }
        
        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }
        
        void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
        {
            RGBA = float4(R, G, B, A);
            RGB = float3(R, G, B);
            RG = float2(R, G);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        void Unity_Distance_float2(float2 A, float2 B, out float Out)
        {
            Out = distance(A, B);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_SceneDepth_Linear01_float(float4 UV, out float Out)
        {
            Out = Linear01Depth(SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), _ZBufferParams);
        }
        
        void Unity_Subtract_float(float A, float B, out float Out)
        {
            Out = A - B;
        }
        
        struct Bindings_SoftParticles_d0f40856fc355224fab6453746e0507a_float
        {
        float4 ScreenPosition;
        float2 NDCPosition;
        };
        
        void SG_SoftParticles_d0f40856fc355224fab6453746e0507a_float(float2 Vector2_AC648482, Bindings_SoftParticles_d0f40856fc355224fab6453746e0507a_float IN, out float Output1_1)
        {
        float _SceneDepth_4582691621dbf18cb0eb2f8d5aa469b7_Out_1_Float;
        Unity_SceneDepth_Linear01_float(float4(IN.NDCPosition.xy, 0, 0), _SceneDepth_4582691621dbf18cb0eb2f8d5aa469b7_Out_1_Float);
        float _Multiply_abd94f3d0b36a08380f6ab11443878f2_Out_2_Float;
        Unity_Multiply_float_float(_SceneDepth_4582691621dbf18cb0eb2f8d5aa469b7_Out_1_Float, _ProjectionParams.z, _Multiply_abd94f3d0b36a08380f6ab11443878f2_Out_2_Float);
        float4 _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4 = IN.ScreenPosition;
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_R_1_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[0];
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_G_2_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[1];
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_B_3_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[2];
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_A_4_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[3];
        float _Subtract_f130a0b621d472829a7045e95984f2a9_Out_2_Float;
        Unity_Subtract_float(_Multiply_abd94f3d0b36a08380f6ab11443878f2_Out_2_Float, _Split_8ee3f43b8caa0c8ab120e9a242f543ca_A_4_Float, _Subtract_f130a0b621d472829a7045e95984f2a9_Out_2_Float);
        float2 _Property_90bb3b6ce940ef868c970ea6aae7c26b_Out_0_Vector2 = Vector2_AC648482;
        float _Remap_db5867ad2542128e864c4c228cc1f3f5_Out_3_Float;
        Unity_Remap_float(_Subtract_f130a0b621d472829a7045e95984f2a9_Out_2_Float, _Property_90bb3b6ce940ef868c970ea6aae7c26b_Out_0_Vector2, float2 (0, 1), _Remap_db5867ad2542128e864c4c228cc1f3f5_Out_3_Float);
        float _Saturate_377f82d9fb446684a06124602b80b432_Out_1_Float;
        Unity_Saturate_float(_Remap_db5867ad2542128e864c4c228cc1f3f5_Out_3_Float, _Saturate_377f82d9fb446684a06124602b80b432_Out_1_Float);
        Output1_1 = _Saturate_377f82d9fb446684a06124602b80b432_Out_1_Float;
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
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float3 _SceneColor_797c5e0605664f268a243b5925dbab4e_Out_1_Vector3;
            Unity_SceneColor_float(float4(IN.NDCPosition.xy, 0, 0), _SceneColor_797c5e0605664f268a243b5925dbab4e_Out_1_Vector3);
            float4 _ScreenPosition_fd10197a85f44b469f625df7a4fee1ac_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
            float2 _Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2;
            Unity_Rotate_Degrees_float(IN.uv0.xy, float2 (0.5, 0.5), float(200.5), _Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2);
            float2 _Property_db2bf3c93cbf4880929bbbbd5b8d32c7_Out_0_Vector2 = _CellsTiling;
            float _Property_20fbe3c647284f08addb9b4125ce6af1_Out_0_Float = _CellsSpeed1;
            float _Multiply_e2672b0aac2d4a33a0d6cc512462530c_Out_2_Float;
            Unity_Multiply_float_float(IN.TimeParameters.x, _Property_20fbe3c647284f08addb9b4125ce6af1_Out_0_Float, _Multiply_e2672b0aac2d4a33a0d6cc512462530c_Out_2_Float);
            float2 _Vector2_0f755d5a1ff04d9b9de7cb63561d44f7_Out_0_Vector2 = float2(float(0), _Multiply_e2672b0aac2d4a33a0d6cc512462530c_Out_2_Float);
            float2 _TilingAndOffset_be8cabdd23314d6f9cb006714bd1aabe_Out_3_Vector2;
            Unity_TilingAndOffset_float(_Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2, _Property_db2bf3c93cbf4880929bbbbd5b8d32c7_Out_0_Vector2, _Vector2_0f755d5a1ff04d9b9de7cb63561d44f7_Out_0_Vector2, _TilingAndOffset_be8cabdd23314d6f9cb006714bd1aabe_Out_3_Vector2);
            float4 _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).tex, UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).samplerstate, UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).GetTransformedUV(_TilingAndOffset_be8cabdd23314d6f9cb006714bd1aabe_Out_3_Vector2) );
            if (UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).hdrDecode.x > 0)
                _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4, UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).hdrDecode);
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_R_4_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.r;
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_G_5_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.g;
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_B_6_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.b;
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_A_7_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.a;
            float _Property_028d8bf17bf4473788d23219249f7451_Out_0_Float = _CellsSpeed2;
            float _Multiply_f0eecebc84f54abc89ef38eaa68f0774_Out_2_Float;
            Unity_Multiply_float_float(IN.TimeParameters.x, _Property_028d8bf17bf4473788d23219249f7451_Out_0_Float, _Multiply_f0eecebc84f54abc89ef38eaa68f0774_Out_2_Float);
            float2 _Vector2_f174d0c85fa546a58eb678ccf778d92c_Out_0_Vector2 = float2(float(0.2), _Multiply_f0eecebc84f54abc89ef38eaa68f0774_Out_2_Float);
            float2 _TilingAndOffset_8f83ef02ec4b473a9913eced11cf3340_Out_3_Vector2;
            Unity_TilingAndOffset_float(_Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2, _Property_db2bf3c93cbf4880929bbbbd5b8d32c7_Out_0_Vector2, _Vector2_f174d0c85fa546a58eb678ccf778d92c_Out_0_Vector2, _TilingAndOffset_8f83ef02ec4b473a9913eced11cf3340_Out_3_Vector2);
            float4 _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).tex, UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).samplerstate, UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).GetTransformedUV(_TilingAndOffset_8f83ef02ec4b473a9913eced11cf3340_Out_3_Vector2) );
            if (UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).hdrDecode.x > 0)
                _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4, UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).hdrDecode);
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_R_4_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.r;
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_G_5_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.g;
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_B_6_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.b;
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_A_7_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.a;
            float _Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float;
            Unity_Maximum_float(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_R_4_Float, _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_R_4_Float, _Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float);
            float _Remap_b4e9369e64e949e880da5427e2de37ac_Out_3_Float;
            Unity_Remap_float(_Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float, float2 (0, 1), float2 (-1, 1), _Remap_b4e9369e64e949e880da5427e2de37ac_Out_3_Float);
            float _Property_5fcd73795f5f4d32a8136c2d77214ef7_Out_0_Float = _CellsIntensity;
            float _Multiply_0003ff715d4349d9b884f7fa02ed336d_Out_2_Float;
            Unity_Multiply_float_float(_Remap_b4e9369e64e949e880da5427e2de37ac_Out_3_Float, _Property_5fcd73795f5f4d32a8136c2d77214ef7_Out_0_Float, _Multiply_0003ff715d4349d9b884f7fa02ed336d_Out_2_Float);
            float4 _Add_5df1c353a4c941d48f93908d8b693661_Out_2_Vector4;
            Unity_Add_float4(_ScreenPosition_fd10197a85f44b469f625df7a4fee1ac_Out_0_Vector4, (_Multiply_0003ff715d4349d9b884f7fa02ed336d_Out_2_Float.xxxx), _Add_5df1c353a4c941d48f93908d8b693661_Out_2_Vector4);
            float2 _Property_61718f0a85b14358b09dbd29786d4112_Out_0_Vector2 = _NoiseTiling;
            float2 _Property_669fd697aeea497ea757ddf406adc31e_Out_0_Vector2 = _NoiseSpeed;
            float2 _Multiply_9341ca9d0a724c2fb64ac31413324f2c_Out_2_Vector2;
            Unity_Multiply_float2_float2((IN.TimeParameters.x.xx), _Property_669fd697aeea497ea757ddf406adc31e_Out_0_Vector2, _Multiply_9341ca9d0a724c2fb64ac31413324f2c_Out_2_Vector2);
            float2 _TilingAndOffset_e203137a933f462cae47a7786a69017b_Out_3_Vector2;
            Unity_TilingAndOffset_float((_Add_5df1c353a4c941d48f93908d8b693661_Out_2_Vector4.xy), _Property_61718f0a85b14358b09dbd29786d4112_Out_0_Vector2, _Multiply_9341ca9d0a724c2fb64ac31413324f2c_Out_2_Vector2, _TilingAndOffset_e203137a933f462cae47a7786a69017b_Out_3_Vector2);
            float4 _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D).tex, UnityBuildTexture2DStructNoScale(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D).samplerstate, UnityBuildTexture2DStructNoScale(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D).GetTransformedUV(_TilingAndOffset_e203137a933f462cae47a7786a69017b_Out_3_Vector2) );
            if (UnityBuildTexture2DStructNoScale(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D).hdrDecode.x > 0)
                _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4, UnityBuildTexture2DStructNoScale(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D).hdrDecode);
            float _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_R_4_Float = _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4.r;
            float _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_G_5_Float = _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4.g;
            float _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_B_6_Float = _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4.b;
            float _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_A_7_Float = _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4.a;
            float _Remap_9915619d36d84b0ebfe6af2b431b96dc_Out_3_Float;
            Unity_Remap_float(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_R_4_Float, float2 (0, 1), float2 (-1, 1), _Remap_9915619d36d84b0ebfe6af2b431b96dc_Out_3_Float);
            float _Property_6b8d94abde1b469c9ef3297cb7e09445_Out_0_Float = _NoiseIntensity;
            float _Multiply_b09f52c62d914ac698c0794266029c75_Out_2_Float;
            Unity_Multiply_float_float(_Remap_9915619d36d84b0ebfe6af2b431b96dc_Out_3_Float, _Property_6b8d94abde1b469c9ef3297cb7e09445_Out_0_Float, _Multiply_b09f52c62d914ac698c0794266029c75_Out_2_Float);
            float4 _Add_d002c03d51f14f30af141e990ebf125b_Out_2_Vector4;
            Unity_Add_float4(_ScreenPosition_fd10197a85f44b469f625df7a4fee1ac_Out_0_Vector4, (_Multiply_b09f52c62d914ac698c0794266029c75_Out_2_Float.xxxx), _Add_d002c03d51f14f30af141e990ebf125b_Out_2_Vector4);
            float4 _Add_7bc501e7b51c415caef9a001d811c6df_Out_2_Vector4;
            Unity_Add_float4(_Add_d002c03d51f14f30af141e990ebf125b_Out_2_Vector4, (_Multiply_0003ff715d4349d9b884f7fa02ed336d_Out_2_Float.xxxx), _Add_7bc501e7b51c415caef9a001d811c6df_Out_2_Vector4);
            float4 _Property_4041d0fd2b654b26a12fe5921ae02a4e_Out_0_Vector4 = _SecondaryCellsTilingOffset;
            float2 _Swizzle_afe1c91ba1b94a8da9622fec61c36808_Out_1_Vector2 = (_Property_4041d0fd2b654b26a12fe5921ae02a4e_Out_0_Vector4.xy).xy;
            float2 _Swizzle_7bad8020207740dc9a08ed5cbafa7a9c_Out_1_Vector2 = _Property_4041d0fd2b654b26a12fe5921ae02a4e_Out_0_Vector4.zw;
            float2 _TilingAndOffset_d84fa061a1be4c66af6b7b69c8e42ee9_Out_3_Vector2;
            Unity_TilingAndOffset_float((_Add_5df1c353a4c941d48f93908d8b693661_Out_2_Vector4.xy), _Swizzle_afe1c91ba1b94a8da9622fec61c36808_Out_1_Vector2, _Swizzle_7bad8020207740dc9a08ed5cbafa7a9c_Out_1_Vector2, _TilingAndOffset_d84fa061a1be4c66af6b7b69c8e42ee9_Out_3_Vector2);
            float4 _SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D).tex, UnityBuildTexture2DStructNoScale(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D).samplerstate, UnityBuildTexture2DStructNoScale(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D).GetTransformedUV(_TilingAndOffset_d84fa061a1be4c66af6b7b69c8e42ee9_Out_3_Vector2) );
            if (UnityBuildTexture2DStructNoScale(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D).hdrDecode.x > 0)
                _SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4, UnityBuildTexture2DStructNoScale(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D).hdrDecode);
            float _SampleTexture2D_2d280125d80e4736968e48ac98141acf_R_4_Float = _SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4.r;
            float _SampleTexture2D_2d280125d80e4736968e48ac98141acf_G_5_Float = _SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4.g;
            float _SampleTexture2D_2d280125d80e4736968e48ac98141acf_B_6_Float = _SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4.b;
            float _SampleTexture2D_2d280125d80e4736968e48ac98141acf_A_7_Float = _SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4.a;
            float _Remap_50f49c7bbb3f4e30a8a3b48af5408b21_Out_3_Float;
            Unity_Remap_float(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_R_4_Float, float2 (0, 1), float2 (-1, 1), _Remap_50f49c7bbb3f4e30a8a3b48af5408b21_Out_3_Float);
            float _Property_f05f92dc6e4f468f8bdd1154535a3d54_Out_0_Float = _SecondaryCellsDistortionAmount;
            float _Multiply_f3882cee4ac545c4aa8f7eb352afd853_Out_2_Float;
            Unity_Multiply_float_float(_Remap_50f49c7bbb3f4e30a8a3b48af5408b21_Out_3_Float, _Property_f05f92dc6e4f468f8bdd1154535a3d54_Out_0_Float, _Multiply_f3882cee4ac545c4aa8f7eb352afd853_Out_2_Float);
            float4 _Add_156fd364558e4068ba15a0f640bc2bfc_Out_2_Vector4;
            Unity_Add_float4(_Add_7bc501e7b51c415caef9a001d811c6df_Out_2_Vector4, (_Multiply_f3882cee4ac545c4aa8f7eb352afd853_Out_2_Float.xxxx), _Add_156fd364558e4068ba15a0f640bc2bfc_Out_2_Vector4);
            float3 _SceneColor_be2bb10b74d74724b26430d8097c9132_Out_1_Vector3;
            Unity_SceneColor_float(_Add_156fd364558e4068ba15a0f640bc2bfc_Out_2_Vector4, _SceneColor_be2bb10b74d74724b26430d8097c9132_Out_1_Vector3);
            float4 _Property_96426d6d56a54e1ca5d788c45c5e9a17_Out_0_Vector4 = _Color;
            float4 _Property_c180718d1cf84124bf9c17ca4739f3b9_Out_0_Vector4 = _Color2;
            float4 _Lerp_bcd4cf1dfeb74a6f9858eaee379c7d36_Out_3_Vector4;
            Unity_Lerp_float4(_Property_96426d6d56a54e1ca5d788c45c5e9a17_Out_0_Vector4, _Property_c180718d1cf84124bf9c17ca4739f3b9_Out_0_Vector4, (_Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float.xxxx), _Lerp_bcd4cf1dfeb74a6f9858eaee379c7d36_Out_3_Vector4);
            float4 _Property_f191c9709ecc402cb9ec1863c504e750_Out_0_Vector4 = _SecondaryCellsHightlightColor;
            float _Property_33ac0341637943b79dc4220e90bc6cb3_Out_0_Float = _SecondaryCellsLightenAmount;
            float _Multiply_0c6f8dc9b9624ddd83466012c1f0473e_Out_2_Float;
            Unity_Multiply_float_float(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_R_4_Float, _Property_33ac0341637943b79dc4220e90bc6cb3_Out_0_Float, _Multiply_0c6f8dc9b9624ddd83466012c1f0473e_Out_2_Float);
            float4 _Lerp_70729269dac64cb8bd68f49a994a0c87_Out_3_Vector4;
            Unity_Lerp_float4(_Lerp_bcd4cf1dfeb74a6f9858eaee379c7d36_Out_3_Vector4, _Property_f191c9709ecc402cb9ec1863c504e750_Out_0_Vector4, (_Multiply_0c6f8dc9b9624ddd83466012c1f0473e_Out_2_Float.xxxx), _Lerp_70729269dac64cb8bd68f49a994a0c87_Out_3_Vector4);
            float2 _Property_7ecb7f3889da4edd8d256eeb7dc379c0_Out_0_Vector2 = _SceneColorLevelsCorrection;
            float _Split_1b1faf2fc072490089f587c7a8dd0365_R_1_Float = _Property_7ecb7f3889da4edd8d256eeb7dc379c0_Out_0_Vector2[0];
            float _Split_1b1faf2fc072490089f587c7a8dd0365_G_2_Float = _Property_7ecb7f3889da4edd8d256eeb7dc379c0_Out_0_Vector2[1];
            float _Split_1b1faf2fc072490089f587c7a8dd0365_B_3_Float = 0;
            float _Split_1b1faf2fc072490089f587c7a8dd0365_A_4_Float = 0;
            float3 _Saturation_62cdd009cbfc43b5a6909a6ed8d85c62_Out_2_Vector3;
            Unity_Saturation_float(_SceneColor_be2bb10b74d74724b26430d8097c9132_Out_1_Vector3, float(0), _Saturation_62cdd009cbfc43b5a6909a6ed8d85c62_Out_2_Vector3);
            float3 _Smoothstep_2ad597db2b9d456398efcdde08dca730_Out_3_Vector3;
            Unity_Smoothstep_float3((_Split_1b1faf2fc072490089f587c7a8dd0365_R_1_Float.xxx), (_Split_1b1faf2fc072490089f587c7a8dd0365_G_2_Float.xxx), _Saturation_62cdd009cbfc43b5a6909a6ed8d85c62_Out_2_Vector3, _Smoothstep_2ad597db2b9d456398efcdde08dca730_Out_3_Vector3);
            float3 _Lerp_b0fa91facd6a432ab71c6b036f65e3a3_Out_3_Vector3;
            Unity_Lerp_float3(_SceneColor_be2bb10b74d74724b26430d8097c9132_Out_1_Vector3, (_Lerp_70729269dac64cb8bd68f49a994a0c87_Out_3_Vector4.xyz), _Smoothstep_2ad597db2b9d456398efcdde08dca730_Out_3_Vector3, _Lerp_b0fa91facd6a432ab71c6b036f65e3a3_Out_3_Vector3);
            float3 _Property_aa59c625101940d5a091f21db9362c1f_Out_0_Vector3 = _ChromaticAberrationColorMax;
            float _Property_0c3440cb57bf462abacb04bf3eda2ee2_Out_0_Float = _ChromaticAberrationOpacity;
            float2 _Property_c658b9f8acd14a91ae75168205cee28c_Out_0_Vector2 = _ChromaticAberrationLevelsCorrection;
            float _Split_be90da803c834a459aa11bd685c539c1_R_1_Float = _Property_c658b9f8acd14a91ae75168205cee28c_Out_0_Vector2[0];
            float _Split_be90da803c834a459aa11bd685c539c1_G_2_Float = _Property_c658b9f8acd14a91ae75168205cee28c_Out_0_Vector2[1];
            float _Split_be90da803c834a459aa11bd685c539c1_B_3_Float = 0;
            float _Split_be90da803c834a459aa11bd685c539c1_A_4_Float = 0;
            float _Property_3d28fedd9c1b4ae8925c13ed8822d26c_Out_0_Float = _ChromaticAberration;
            float _Multiply_66933dbf527542539db7923a55361940_Out_2_Float;
            Unity_Multiply_float_float(_Property_3d28fedd9c1b4ae8925c13ed8822d26c_Out_0_Float, 0.001, _Multiply_66933dbf527542539db7923a55361940_Out_2_Float);
            float _Negate_4f6a77e415ec48d0905951f646fd06d0_Out_1_Float;
            Unity_Negate_float(_Multiply_66933dbf527542539db7923a55361940_Out_2_Float, _Negate_4f6a77e415ec48d0905951f646fd06d0_Out_1_Float);
            float2 _Vector2_60e5e05cb9c94b44b8ef356c8a0cf51a_Out_0_Vector2 = float2(_Negate_4f6a77e415ec48d0905951f646fd06d0_Out_1_Float, _Multiply_66933dbf527542539db7923a55361940_Out_2_Float);
            float2 _Add_ae2604b2df614aa3b9dcb1ecec9f4486_Out_2_Vector2;
            Unity_Add_float2((_Add_156fd364558e4068ba15a0f640bc2bfc_Out_2_Vector4.xy), _Vector2_60e5e05cb9c94b44b8ef356c8a0cf51a_Out_0_Vector2, _Add_ae2604b2df614aa3b9dcb1ecec9f4486_Out_2_Vector2);
            float3 _SceneColor_6efd0253931e4e5883787f89929021e1_Out_1_Vector3;
            Unity_SceneColor_float((float4(_Add_ae2604b2df614aa3b9dcb1ecec9f4486_Out_2_Vector2, 0.0, 1.0)), _SceneColor_6efd0253931e4e5883787f89929021e1_Out_1_Vector3);
            float3 _Saturation_57b2d1dc792d4b9faf785251dadc5558_Out_2_Vector3;
            Unity_Saturation_float(_SceneColor_6efd0253931e4e5883787f89929021e1_Out_1_Vector3, float(0), _Saturation_57b2d1dc792d4b9faf785251dadc5558_Out_2_Vector3);
            float3 _Smoothstep_37ecb3e11f0a4fff9ebd9656d6cecf39_Out_3_Vector3;
            Unity_Smoothstep_float3((_Split_be90da803c834a459aa11bd685c539c1_R_1_Float.xxx), (_Split_be90da803c834a459aa11bd685c539c1_G_2_Float.xxx), _Saturation_57b2d1dc792d4b9faf785251dadc5558_Out_2_Vector3, _Smoothstep_37ecb3e11f0a4fff9ebd9656d6cecf39_Out_3_Vector3);
            float2 _Vector2_6765997c92d54a82a7e0955e0fc51e93_Out_0_Vector2 = float2(_Negate_4f6a77e415ec48d0905951f646fd06d0_Out_1_Float, _Negate_4f6a77e415ec48d0905951f646fd06d0_Out_1_Float);
            float2 _Add_72ed283444024fd8ab4da82b3a998f33_Out_2_Vector2;
            Unity_Add_float2((_Add_156fd364558e4068ba15a0f640bc2bfc_Out_2_Vector4.xy), _Vector2_6765997c92d54a82a7e0955e0fc51e93_Out_0_Vector2, _Add_72ed283444024fd8ab4da82b3a998f33_Out_2_Vector2);
            float3 _SceneColor_e0be9321e1dd40d1bb12f0a0bc1519c1_Out_1_Vector3;
            Unity_SceneColor_float((float4(_Add_72ed283444024fd8ab4da82b3a998f33_Out_2_Vector2, 0.0, 1.0)), _SceneColor_e0be9321e1dd40d1bb12f0a0bc1519c1_Out_1_Vector3);
            float3 _Saturation_ee467f8815fd444391dd4277dbf70b91_Out_2_Vector3;
            Unity_Saturation_float(_SceneColor_e0be9321e1dd40d1bb12f0a0bc1519c1_Out_1_Vector3, float(0), _Saturation_ee467f8815fd444391dd4277dbf70b91_Out_2_Vector3);
            float3 _Smoothstep_c946ec1206c54154af27837cf3914d85_Out_3_Vector3;
            Unity_Smoothstep_float3((_Split_be90da803c834a459aa11bd685c539c1_R_1_Float.xxx), (_Split_be90da803c834a459aa11bd685c539c1_G_2_Float.xxx), _Saturation_ee467f8815fd444391dd4277dbf70b91_Out_2_Vector3, _Smoothstep_c946ec1206c54154af27837cf3914d85_Out_3_Vector3);
            float2 _Vector2_4c1b45d58a7c4335a13602bcb14e057d_Out_0_Vector2 = float2(_Multiply_66933dbf527542539db7923a55361940_Out_2_Float, _Negate_4f6a77e415ec48d0905951f646fd06d0_Out_1_Float);
            float2 _Add_a18e51b946e9474aa6c509838493bcf0_Out_2_Vector2;
            Unity_Add_float2((_Add_156fd364558e4068ba15a0f640bc2bfc_Out_2_Vector4.xy), _Vector2_4c1b45d58a7c4335a13602bcb14e057d_Out_0_Vector2, _Add_a18e51b946e9474aa6c509838493bcf0_Out_2_Vector2);
            float3 _SceneColor_bbadb668faae4fee86cd3d7901ca0b69_Out_1_Vector3;
            Unity_SceneColor_float((float4(_Add_a18e51b946e9474aa6c509838493bcf0_Out_2_Vector2, 0.0, 1.0)), _SceneColor_bbadb668faae4fee86cd3d7901ca0b69_Out_1_Vector3);
            float3 _Saturation_806176d4b1ee4b73a59c78bd57963c23_Out_2_Vector3;
            Unity_Saturation_float(_SceneColor_bbadb668faae4fee86cd3d7901ca0b69_Out_1_Vector3, float(0), _Saturation_806176d4b1ee4b73a59c78bd57963c23_Out_2_Vector3);
            float3 _Smoothstep_f0a69a29cd3b4b43b5dcfa9febb15269_Out_3_Vector3;
            Unity_Smoothstep_float3((_Split_be90da803c834a459aa11bd685c539c1_R_1_Float.xxx), (_Split_be90da803c834a459aa11bd685c539c1_G_2_Float.xxx), _Saturation_806176d4b1ee4b73a59c78bd57963c23_Out_2_Vector3, _Smoothstep_f0a69a29cd3b4b43b5dcfa9febb15269_Out_3_Vector3);
            float4 _Combine_d72d384256874619b4a04639e94f206d_RGBA_4_Vector4;
            float3 _Combine_d72d384256874619b4a04639e94f206d_RGB_5_Vector3;
            float2 _Combine_d72d384256874619b4a04639e94f206d_RG_6_Vector2;
            Unity_Combine_float((_Smoothstep_37ecb3e11f0a4fff9ebd9656d6cecf39_Out_3_Vector3).x, (_Smoothstep_c946ec1206c54154af27837cf3914d85_Out_3_Vector3).x, (_Smoothstep_f0a69a29cd3b4b43b5dcfa9febb15269_Out_3_Vector3).x, float(0), _Combine_d72d384256874619b4a04639e94f206d_RGBA_4_Vector4, _Combine_d72d384256874619b4a04639e94f206d_RGB_5_Vector3, _Combine_d72d384256874619b4a04639e94f206d_RG_6_Vector2);
            float3 _Multiply_266dacc6e1054dd885156572661c7eb1_Out_2_Vector3;
            Unity_Multiply_float3_float3((_Property_0c3440cb57bf462abacb04bf3eda2ee2_Out_0_Float.xxx), _Combine_d72d384256874619b4a04639e94f206d_RGB_5_Vector3, _Multiply_266dacc6e1054dd885156572661c7eb1_Out_2_Vector3);
            float3 _Lerp_3dd9073d874244ae88c8688335cc44a3_Out_3_Vector3;
            Unity_Lerp_float3(_Lerp_b0fa91facd6a432ab71c6b036f65e3a3_Out_3_Vector3, _Property_aa59c625101940d5a091f21db9362c1f_Out_0_Vector3, _Multiply_266dacc6e1054dd885156572661c7eb1_Out_2_Vector3, _Lerp_3dd9073d874244ae88c8688335cc44a3_Out_3_Vector3);
            float4 _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4 = IN.uv0;
            float _Split_e649f2f75c6e474a848600755e877ab3_R_1_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[0];
            float _Split_e649f2f75c6e474a848600755e877ab3_G_2_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[1];
            float _Split_e649f2f75c6e474a848600755e877ab3_B_3_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[2];
            float _Split_e649f2f75c6e474a848600755e877ab3_A_4_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[3];
            float _OneMinus_96ffa84f34534286bacbeac79c1e6a04_Out_1_Float;
            Unity_OneMinus_float(_Split_e649f2f75c6e474a848600755e877ab3_R_1_Float, _OneMinus_96ffa84f34534286bacbeac79c1e6a04_Out_1_Float);
            float _Multiply_55d6e7f4b6d246399079ceee11e52645_Out_2_Float;
            Unity_Multiply_float_float(_Split_e649f2f75c6e474a848600755e877ab3_R_1_Float, _OneMinus_96ffa84f34534286bacbeac79c1e6a04_Out_1_Float, _Multiply_55d6e7f4b6d246399079ceee11e52645_Out_2_Float);
            float _OneMinus_22f755e38c114b9ca586c68d75f661d2_Out_1_Float;
            Unity_OneMinus_float(_Split_e649f2f75c6e474a848600755e877ab3_G_2_Float, _OneMinus_22f755e38c114b9ca586c68d75f661d2_Out_1_Float);
            float _Multiply_df72511ff7b44e8983661009c4ed7d02_Out_2_Float;
            Unity_Multiply_float_float(_Split_e649f2f75c6e474a848600755e877ab3_G_2_Float, _OneMinus_22f755e38c114b9ca586c68d75f661d2_Out_1_Float, _Multiply_df72511ff7b44e8983661009c4ed7d02_Out_2_Float);
            float _Multiply_9d0107fe0fc041d483af0038e916149b_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_55d6e7f4b6d246399079ceee11e52645_Out_2_Float, _Multiply_df72511ff7b44e8983661009c4ed7d02_Out_2_Float, _Multiply_9d0107fe0fc041d483af0038e916149b_Out_2_Float);
            float _Smoothstep_abb9639c55aa4810bae13fb0b4949e74_Out_3_Float;
            Unity_Smoothstep_float(float(0), float(0.05), _Multiply_9d0107fe0fc041d483af0038e916149b_Out_2_Float, _Smoothstep_abb9639c55aa4810bae13fb0b4949e74_Out_3_Float);
            float2 _Property_182212378d374ce8bfa20f4f52d6532d_Out_0_Vector2 = _CircleSmoothstep;
            float _Split_075223437c63458bb5e99e2413cc2604_R_1_Float = _Property_182212378d374ce8bfa20f4f52d6532d_Out_0_Vector2[0];
            float _Split_075223437c63458bb5e99e2413cc2604_G_2_Float = _Property_182212378d374ce8bfa20f4f52d6532d_Out_0_Vector2[1];
            float _Split_075223437c63458bb5e99e2413cc2604_B_3_Float = 0;
            float _Split_075223437c63458bb5e99e2413cc2604_A_4_Float = 0;
            float4 _UV_a8efe86069ac4781a2dc587a5eb92bf9_Out_0_Vector4 = IN.uv0;
            float2 _Vector2_494e0112c5b5405991a62fa7d5437cf9_Out_0_Vector2 = float2(float(0.5), float(0.5));
            float _Distance_b68a33dde5ea4dbb995d9993afa14d01_Out_2_Float;
            Unity_Distance_float2((_UV_a8efe86069ac4781a2dc587a5eb92bf9_Out_0_Vector4.xy), _Vector2_494e0112c5b5405991a62fa7d5437cf9_Out_0_Vector2, _Distance_b68a33dde5ea4dbb995d9993afa14d01_Out_2_Float);
            float _Smoothstep_82f21a94208f4efba46de32f75274968_Out_3_Float;
            Unity_Smoothstep_float(_Split_075223437c63458bb5e99e2413cc2604_G_2_Float, _Split_075223437c63458bb5e99e2413cc2604_R_1_Float, _Distance_b68a33dde5ea4dbb995d9993afa14d01_Out_2_Float, _Smoothstep_82f21a94208f4efba46de32f75274968_Out_3_Float);
            float _Property_5bfabba54df4446ca8c86d35440d67c1_Out_0_Float = _CirclePower;
            float _Power_7c70f6a6e559406e8ae2294ab1eebe04_Out_2_Float;
            Unity_Power_float(_Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float, _Property_5bfabba54df4446ca8c86d35440d67c1_Out_0_Float, _Power_7c70f6a6e559406e8ae2294ab1eebe04_Out_2_Float);
            float _Add_b72754a7e33c41faa48a4ea1fa2edfb3_Out_2_Float;
            Unity_Add_float(_Smoothstep_82f21a94208f4efba46de32f75274968_Out_3_Float, _Power_7c70f6a6e559406e8ae2294ab1eebe04_Out_2_Float, _Add_b72754a7e33c41faa48a4ea1fa2edfb3_Out_2_Float);
            float _Multiply_374c554884fb4f6b9ab82528b2a73e85_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_abb9639c55aa4810bae13fb0b4949e74_Out_3_Float, _Add_b72754a7e33c41faa48a4ea1fa2edfb3_Out_2_Float, _Multiply_374c554884fb4f6b9ab82528b2a73e85_Out_2_Float);
            float _Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float;
            Unity_Saturate_float(_Multiply_374c554884fb4f6b9ab82528b2a73e85_Out_2_Float, _Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float);
            float3 _Lerp_180be64a42cb4958819f855681034de2_Out_3_Vector3;
            Unity_Lerp_float3(_SceneColor_797c5e0605664f268a243b5925dbab4e_Out_1_Vector3, _Lerp_3dd9073d874244ae88c8688335cc44a3_Out_3_Vector3, (_Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float.xxx), _Lerp_180be64a42cb4958819f855681034de2_Out_3_Vector3);
            Bindings_SoftParticles_d0f40856fc355224fab6453746e0507a_float _SoftParticles_2d24478f7f3246149eb145045221b789;
            _SoftParticles_2d24478f7f3246149eb145045221b789.ScreenPosition = IN.ScreenPosition;
            _SoftParticles_2d24478f7f3246149eb145045221b789.NDCPosition = IN.NDCPosition;
            float _SoftParticles_2d24478f7f3246149eb145045221b789_Output1_1_Float;
            SG_SoftParticles_d0f40856fc355224fab6453746e0507a_float(float2 (0, 0.5), _SoftParticles_2d24478f7f3246149eb145045221b789, _SoftParticles_2d24478f7f3246149eb145045221b789_Output1_1_Float);
            float _Multiply_de2953022bc948e69f7268a693c8f3e3_Out_2_Float;
            Unity_Multiply_float_float(_Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float, _SoftParticles_2d24478f7f3246149eb145045221b789_Output1_1_Float, _Multiply_de2953022bc948e69f7268a693c8f3e3_Out_2_Float);
            surface.BaseColor = _Lerp_180be64a42cb4958819f855681034de2_Out_3_Vector3;
            surface.Alpha = _Multiply_de2953022bc948e69f7268a693c8f3e3_Out_2_Float;
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
        
            
        
        
        
        
        
            output.WorldSpacePosition = input.positionWS;
            output.ScreenPosition = ComputeScreenPos(TransformWorldToHClip(input.positionWS), _ProjectionParams.x);
        
            #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x < 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #else
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x > 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScaledScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 = input.texCoord0;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
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
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/UnlitGBufferPass.hlsl"
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
        // PassKeywords: <None>
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX_NORMAL_OUTPUT
        #define FEATURES_GRAPH_VERTEX_TANGENT_OUTPUT
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHONLY
        #define SCENESELECTIONPASS 1
        #define ALPHA_CLIP_THRESHOLD 1
        #define REQUIRE_DEPTH_TEXTURE
        
        
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
            #if UNITY_ANY_INSTANCING_ENABLED || defined(ATTRIBUTES_NEED_INSTANCEID)
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
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
             float3 WorldSpacePosition;
             float4 ScreenPosition;
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float3 TimeParameters;
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
             float4 texCoord0 : INTERP0;
             float3 positionWS : INTERP1;
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
            output.texCoord0.xyzw = input.texCoord0;
            output.positionWS.xyz = input.positionWS;
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
            output.texCoord0 = input.texCoord0.xyzw;
            output.positionWS = input.positionWS.xyz;
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
        float4 _SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D_TexelSize;
        float4 _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D_TexelSize;
        float4 _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D_TexelSize;
        float4 _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D_TexelSize;
        float4 _Color2;
        float4 _Color;
        float _CellsSpeed2;
        float _CellsSpeed1;
        float2 _CellsTiling;
        float2 _SceneColorLevelsCorrection;
        float _NoiseIntensity;
        float _CellsIntensity;
        float _SceneColorClamp;
        float2 _CircleSmoothstep;
        float _CirclePower;
        float _ChromaticAberration;
        float _SecondaryCellsLightenAmount;
        float4 _SecondaryCellsHightlightColor;
        float _SecondaryCellsDistortionAmount;
        float4 _SecondaryCellsTilingOffset;
        float _ChromaticAberrationOpacity;
        float2 _ChromaticAberrationLevelsCorrection;
        float3 _ChromaticAberrationColorMax;
        float2 _NoiseTiling;
        float2 _NoiseSpeed;
        UNITY_TEXTURE_STREAMING_DEBUG_VARS;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D);
        TEXTURE2D(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D);
        TEXTURE2D(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D);
        TEXTURE2D(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D);
        
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
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        void Unity_Distance_float2(float2 A, float2 B, out float Out)
        {
            Out = distance(A, B);
        }
        
        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            Rotation = Rotation * (3.1415926f/180.0f);
            UV -= Center;
            float s, c;
            sincos(Rotation, s, c);
            float3 r3 = float3(-s, c, s);
            float2 r1;
            r1.y = dot(UV, r3.xy);
            r1.x = dot(UV, r3.yz);
            Out = r1 + Center;
        }
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        void Unity_Maximum_float(float A, float B, out float Out)
        {
            Out = max(A, B);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_SceneDepth_Linear01_float(float4 UV, out float Out)
        {
            Out = Linear01Depth(SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), _ZBufferParams);
        }
        
        void Unity_Subtract_float(float A, float B, out float Out)
        {
            Out = A - B;
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        struct Bindings_SoftParticles_d0f40856fc355224fab6453746e0507a_float
        {
        float4 ScreenPosition;
        float2 NDCPosition;
        };
        
        void SG_SoftParticles_d0f40856fc355224fab6453746e0507a_float(float2 Vector2_AC648482, Bindings_SoftParticles_d0f40856fc355224fab6453746e0507a_float IN, out float Output1_1)
        {
        float _SceneDepth_4582691621dbf18cb0eb2f8d5aa469b7_Out_1_Float;
        Unity_SceneDepth_Linear01_float(float4(IN.NDCPosition.xy, 0, 0), _SceneDepth_4582691621dbf18cb0eb2f8d5aa469b7_Out_1_Float);
        float _Multiply_abd94f3d0b36a08380f6ab11443878f2_Out_2_Float;
        Unity_Multiply_float_float(_SceneDepth_4582691621dbf18cb0eb2f8d5aa469b7_Out_1_Float, _ProjectionParams.z, _Multiply_abd94f3d0b36a08380f6ab11443878f2_Out_2_Float);
        float4 _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4 = IN.ScreenPosition;
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_R_1_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[0];
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_G_2_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[1];
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_B_3_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[2];
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_A_4_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[3];
        float _Subtract_f130a0b621d472829a7045e95984f2a9_Out_2_Float;
        Unity_Subtract_float(_Multiply_abd94f3d0b36a08380f6ab11443878f2_Out_2_Float, _Split_8ee3f43b8caa0c8ab120e9a242f543ca_A_4_Float, _Subtract_f130a0b621d472829a7045e95984f2a9_Out_2_Float);
        float2 _Property_90bb3b6ce940ef868c970ea6aae7c26b_Out_0_Vector2 = Vector2_AC648482;
        float _Remap_db5867ad2542128e864c4c228cc1f3f5_Out_3_Float;
        Unity_Remap_float(_Subtract_f130a0b621d472829a7045e95984f2a9_Out_2_Float, _Property_90bb3b6ce940ef868c970ea6aae7c26b_Out_0_Vector2, float2 (0, 1), _Remap_db5867ad2542128e864c4c228cc1f3f5_Out_3_Float);
        float _Saturate_377f82d9fb446684a06124602b80b432_Out_1_Float;
        Unity_Saturate_float(_Remap_db5867ad2542128e864c4c228cc1f3f5_Out_3_Float, _Saturate_377f82d9fb446684a06124602b80b432_Out_1_Float);
        Output1_1 = _Saturate_377f82d9fb446684a06124602b80b432_Out_1_Float;
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
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4 = IN.uv0;
            float _Split_e649f2f75c6e474a848600755e877ab3_R_1_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[0];
            float _Split_e649f2f75c6e474a848600755e877ab3_G_2_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[1];
            float _Split_e649f2f75c6e474a848600755e877ab3_B_3_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[2];
            float _Split_e649f2f75c6e474a848600755e877ab3_A_4_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[3];
            float _OneMinus_96ffa84f34534286bacbeac79c1e6a04_Out_1_Float;
            Unity_OneMinus_float(_Split_e649f2f75c6e474a848600755e877ab3_R_1_Float, _OneMinus_96ffa84f34534286bacbeac79c1e6a04_Out_1_Float);
            float _Multiply_55d6e7f4b6d246399079ceee11e52645_Out_2_Float;
            Unity_Multiply_float_float(_Split_e649f2f75c6e474a848600755e877ab3_R_1_Float, _OneMinus_96ffa84f34534286bacbeac79c1e6a04_Out_1_Float, _Multiply_55d6e7f4b6d246399079ceee11e52645_Out_2_Float);
            float _OneMinus_22f755e38c114b9ca586c68d75f661d2_Out_1_Float;
            Unity_OneMinus_float(_Split_e649f2f75c6e474a848600755e877ab3_G_2_Float, _OneMinus_22f755e38c114b9ca586c68d75f661d2_Out_1_Float);
            float _Multiply_df72511ff7b44e8983661009c4ed7d02_Out_2_Float;
            Unity_Multiply_float_float(_Split_e649f2f75c6e474a848600755e877ab3_G_2_Float, _OneMinus_22f755e38c114b9ca586c68d75f661d2_Out_1_Float, _Multiply_df72511ff7b44e8983661009c4ed7d02_Out_2_Float);
            float _Multiply_9d0107fe0fc041d483af0038e916149b_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_55d6e7f4b6d246399079ceee11e52645_Out_2_Float, _Multiply_df72511ff7b44e8983661009c4ed7d02_Out_2_Float, _Multiply_9d0107fe0fc041d483af0038e916149b_Out_2_Float);
            float _Smoothstep_abb9639c55aa4810bae13fb0b4949e74_Out_3_Float;
            Unity_Smoothstep_float(float(0), float(0.05), _Multiply_9d0107fe0fc041d483af0038e916149b_Out_2_Float, _Smoothstep_abb9639c55aa4810bae13fb0b4949e74_Out_3_Float);
            float2 _Property_182212378d374ce8bfa20f4f52d6532d_Out_0_Vector2 = _CircleSmoothstep;
            float _Split_075223437c63458bb5e99e2413cc2604_R_1_Float = _Property_182212378d374ce8bfa20f4f52d6532d_Out_0_Vector2[0];
            float _Split_075223437c63458bb5e99e2413cc2604_G_2_Float = _Property_182212378d374ce8bfa20f4f52d6532d_Out_0_Vector2[1];
            float _Split_075223437c63458bb5e99e2413cc2604_B_3_Float = 0;
            float _Split_075223437c63458bb5e99e2413cc2604_A_4_Float = 0;
            float4 _UV_a8efe86069ac4781a2dc587a5eb92bf9_Out_0_Vector4 = IN.uv0;
            float2 _Vector2_494e0112c5b5405991a62fa7d5437cf9_Out_0_Vector2 = float2(float(0.5), float(0.5));
            float _Distance_b68a33dde5ea4dbb995d9993afa14d01_Out_2_Float;
            Unity_Distance_float2((_UV_a8efe86069ac4781a2dc587a5eb92bf9_Out_0_Vector4.xy), _Vector2_494e0112c5b5405991a62fa7d5437cf9_Out_0_Vector2, _Distance_b68a33dde5ea4dbb995d9993afa14d01_Out_2_Float);
            float _Smoothstep_82f21a94208f4efba46de32f75274968_Out_3_Float;
            Unity_Smoothstep_float(_Split_075223437c63458bb5e99e2413cc2604_G_2_Float, _Split_075223437c63458bb5e99e2413cc2604_R_1_Float, _Distance_b68a33dde5ea4dbb995d9993afa14d01_Out_2_Float, _Smoothstep_82f21a94208f4efba46de32f75274968_Out_3_Float);
            float2 _Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2;
            Unity_Rotate_Degrees_float(IN.uv0.xy, float2 (0.5, 0.5), float(200.5), _Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2);
            float2 _Property_db2bf3c93cbf4880929bbbbd5b8d32c7_Out_0_Vector2 = _CellsTiling;
            float _Property_20fbe3c647284f08addb9b4125ce6af1_Out_0_Float = _CellsSpeed1;
            float _Multiply_e2672b0aac2d4a33a0d6cc512462530c_Out_2_Float;
            Unity_Multiply_float_float(IN.TimeParameters.x, _Property_20fbe3c647284f08addb9b4125ce6af1_Out_0_Float, _Multiply_e2672b0aac2d4a33a0d6cc512462530c_Out_2_Float);
            float2 _Vector2_0f755d5a1ff04d9b9de7cb63561d44f7_Out_0_Vector2 = float2(float(0), _Multiply_e2672b0aac2d4a33a0d6cc512462530c_Out_2_Float);
            float2 _TilingAndOffset_be8cabdd23314d6f9cb006714bd1aabe_Out_3_Vector2;
            Unity_TilingAndOffset_float(_Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2, _Property_db2bf3c93cbf4880929bbbbd5b8d32c7_Out_0_Vector2, _Vector2_0f755d5a1ff04d9b9de7cb63561d44f7_Out_0_Vector2, _TilingAndOffset_be8cabdd23314d6f9cb006714bd1aabe_Out_3_Vector2);
            float4 _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).tex, UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).samplerstate, UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).GetTransformedUV(_TilingAndOffset_be8cabdd23314d6f9cb006714bd1aabe_Out_3_Vector2) );
            if (UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).hdrDecode.x > 0)
                _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4, UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).hdrDecode);
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_R_4_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.r;
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_G_5_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.g;
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_B_6_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.b;
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_A_7_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.a;
            float _Property_028d8bf17bf4473788d23219249f7451_Out_0_Float = _CellsSpeed2;
            float _Multiply_f0eecebc84f54abc89ef38eaa68f0774_Out_2_Float;
            Unity_Multiply_float_float(IN.TimeParameters.x, _Property_028d8bf17bf4473788d23219249f7451_Out_0_Float, _Multiply_f0eecebc84f54abc89ef38eaa68f0774_Out_2_Float);
            float2 _Vector2_f174d0c85fa546a58eb678ccf778d92c_Out_0_Vector2 = float2(float(0.2), _Multiply_f0eecebc84f54abc89ef38eaa68f0774_Out_2_Float);
            float2 _TilingAndOffset_8f83ef02ec4b473a9913eced11cf3340_Out_3_Vector2;
            Unity_TilingAndOffset_float(_Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2, _Property_db2bf3c93cbf4880929bbbbd5b8d32c7_Out_0_Vector2, _Vector2_f174d0c85fa546a58eb678ccf778d92c_Out_0_Vector2, _TilingAndOffset_8f83ef02ec4b473a9913eced11cf3340_Out_3_Vector2);
            float4 _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).tex, UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).samplerstate, UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).GetTransformedUV(_TilingAndOffset_8f83ef02ec4b473a9913eced11cf3340_Out_3_Vector2) );
            if (UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).hdrDecode.x > 0)
                _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4, UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).hdrDecode);
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_R_4_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.r;
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_G_5_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.g;
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_B_6_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.b;
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_A_7_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.a;
            float _Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float;
            Unity_Maximum_float(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_R_4_Float, _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_R_4_Float, _Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float);
            float _Property_5bfabba54df4446ca8c86d35440d67c1_Out_0_Float = _CirclePower;
            float _Power_7c70f6a6e559406e8ae2294ab1eebe04_Out_2_Float;
            Unity_Power_float(_Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float, _Property_5bfabba54df4446ca8c86d35440d67c1_Out_0_Float, _Power_7c70f6a6e559406e8ae2294ab1eebe04_Out_2_Float);
            float _Add_b72754a7e33c41faa48a4ea1fa2edfb3_Out_2_Float;
            Unity_Add_float(_Smoothstep_82f21a94208f4efba46de32f75274968_Out_3_Float, _Power_7c70f6a6e559406e8ae2294ab1eebe04_Out_2_Float, _Add_b72754a7e33c41faa48a4ea1fa2edfb3_Out_2_Float);
            float _Multiply_374c554884fb4f6b9ab82528b2a73e85_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_abb9639c55aa4810bae13fb0b4949e74_Out_3_Float, _Add_b72754a7e33c41faa48a4ea1fa2edfb3_Out_2_Float, _Multiply_374c554884fb4f6b9ab82528b2a73e85_Out_2_Float);
            float _Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float;
            Unity_Saturate_float(_Multiply_374c554884fb4f6b9ab82528b2a73e85_Out_2_Float, _Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float);
            Bindings_SoftParticles_d0f40856fc355224fab6453746e0507a_float _SoftParticles_2d24478f7f3246149eb145045221b789;
            _SoftParticles_2d24478f7f3246149eb145045221b789.ScreenPosition = IN.ScreenPosition;
            _SoftParticles_2d24478f7f3246149eb145045221b789.NDCPosition = IN.NDCPosition;
            float _SoftParticles_2d24478f7f3246149eb145045221b789_Output1_1_Float;
            SG_SoftParticles_d0f40856fc355224fab6453746e0507a_float(float2 (0, 0.5), _SoftParticles_2d24478f7f3246149eb145045221b789, _SoftParticles_2d24478f7f3246149eb145045221b789_Output1_1_Float);
            float _Multiply_de2953022bc948e69f7268a693c8f3e3_Out_2_Float;
            Unity_Multiply_float_float(_Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float, _SoftParticles_2d24478f7f3246149eb145045221b789_Output1_1_Float, _Multiply_de2953022bc948e69f7268a693c8f3e3_Out_2_Float);
            surface.Alpha = _Multiply_de2953022bc948e69f7268a693c8f3e3_Out_2_Float;
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
        
            
        
        
        
        
        
            output.WorldSpacePosition = input.positionWS;
            output.ScreenPosition = ComputeScreenPos(TransformWorldToHClip(input.positionWS), _ProjectionParams.x);
        
            #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x < 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #else
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x > 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScaledScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 = input.texCoord0;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
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
        Cull Back
        
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
        // PassKeywords: <None>
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX_NORMAL_OUTPUT
        #define FEATURES_GRAPH_VERTEX_TANGENT_OUTPUT
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHONLY
        #define SCENEPICKINGPASS 1
        #define ALPHA_CLIP_THRESHOLD 1
        #define REQUIRE_DEPTH_TEXTURE
        #define REQUIRE_OPAQUE_TEXTURE
        
        
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
            #if UNITY_ANY_INSTANCING_ENABLED || defined(ATTRIBUTES_NEED_INSTANCEID)
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
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
             float3 WorldSpacePosition;
             float4 ScreenPosition;
             float2 NDCPosition;
             float2 PixelPosition;
             float4 uv0;
             float3 TimeParameters;
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
             float4 texCoord0 : INTERP0;
             float3 positionWS : INTERP1;
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
            output.texCoord0.xyzw = input.texCoord0;
            output.positionWS.xyz = input.positionWS;
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
            output.texCoord0 = input.texCoord0.xyzw;
            output.positionWS = input.positionWS.xyz;
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
        float4 _SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D_TexelSize;
        float4 _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D_TexelSize;
        float4 _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D_TexelSize;
        float4 _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D_TexelSize;
        float4 _Color2;
        float4 _Color;
        float _CellsSpeed2;
        float _CellsSpeed1;
        float2 _CellsTiling;
        float2 _SceneColorLevelsCorrection;
        float _NoiseIntensity;
        float _CellsIntensity;
        float _SceneColorClamp;
        float2 _CircleSmoothstep;
        float _CirclePower;
        float _ChromaticAberration;
        float _SecondaryCellsLightenAmount;
        float4 _SecondaryCellsHightlightColor;
        float _SecondaryCellsDistortionAmount;
        float4 _SecondaryCellsTilingOffset;
        float _ChromaticAberrationOpacity;
        float2 _ChromaticAberrationLevelsCorrection;
        float3 _ChromaticAberrationColorMax;
        float2 _NoiseTiling;
        float2 _NoiseSpeed;
        UNITY_TEXTURE_STREAMING_DEBUG_VARS;
        CBUFFER_END
        
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D);
        TEXTURE2D(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D);
        TEXTURE2D(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D);
        TEXTURE2D(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D);
        SAMPLER(sampler_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D);
        
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
        
        void Unity_SceneColor_float(float4 UV, out float3 Out)
        {
            Out = SHADERGRAPH_SAMPLE_SCENE_COLOR(UV.xy);
        }
        
        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            Rotation = Rotation * (3.1415926f/180.0f);
            UV -= Center;
            float s, c;
            sincos(Rotation, s, c);
            float3 r3 = float3(-s, c, s);
            float2 r1;
            r1.y = dot(UV, r3.xy);
            r1.x = dot(UV, r3.yz);
            Out = r1 + Center;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        void Unity_Maximum_float(float A, float B, out float Out)
        {
            Out = max(A, B);
        }
        
        void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
        {
            Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
        }
        
        void Unity_Add_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A + B;
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
        {
            Out = lerp(A, B, T);
        }
        
        void Unity_Saturation_float(float3 In, float Saturation, out float3 Out)
        {
            float luma = dot(In, float3(0.2126729, 0.7151522, 0.0721750));
            Out =  luma.xxx + Saturation.xxx * (In - luma.xxx);
        }
        
        void Unity_Smoothstep_float3(float3 Edge1, float3 Edge2, float3 In, out float3 Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        void Unity_Lerp_float3(float3 A, float3 B, float3 T, out float3 Out)
        {
            Out = lerp(A, B, T);
        }
        
        void Unity_Negate_float(float In, out float Out)
        {
            Out = -1 * In;
        }
        
        void Unity_Add_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A + B;
        }
        
        void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
        {
            RGBA = float4(R, G, B, A);
            RGB = float3(R, G, B);
            RG = float2(R, G);
        }
        
        void Unity_Multiply_float3_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A * B;
        }
        
        void Unity_OneMinus_float(float In, out float Out)
        {
            Out = 1 - In;
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        void Unity_Distance_float2(float2 A, float2 B, out float Out)
        {
            Out = distance(A, B);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Add_float(float A, float B, out float Out)
        {
            Out = A + B;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_SceneDepth_Linear01_float(float4 UV, out float Out)
        {
            Out = Linear01Depth(SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV.xy), _ZBufferParams);
        }
        
        void Unity_Subtract_float(float A, float B, out float Out)
        {
            Out = A - B;
        }
        
        struct Bindings_SoftParticles_d0f40856fc355224fab6453746e0507a_float
        {
        float4 ScreenPosition;
        float2 NDCPosition;
        };
        
        void SG_SoftParticles_d0f40856fc355224fab6453746e0507a_float(float2 Vector2_AC648482, Bindings_SoftParticles_d0f40856fc355224fab6453746e0507a_float IN, out float Output1_1)
        {
        float _SceneDepth_4582691621dbf18cb0eb2f8d5aa469b7_Out_1_Float;
        Unity_SceneDepth_Linear01_float(float4(IN.NDCPosition.xy, 0, 0), _SceneDepth_4582691621dbf18cb0eb2f8d5aa469b7_Out_1_Float);
        float _Multiply_abd94f3d0b36a08380f6ab11443878f2_Out_2_Float;
        Unity_Multiply_float_float(_SceneDepth_4582691621dbf18cb0eb2f8d5aa469b7_Out_1_Float, _ProjectionParams.z, _Multiply_abd94f3d0b36a08380f6ab11443878f2_Out_2_Float);
        float4 _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4 = IN.ScreenPosition;
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_R_1_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[0];
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_G_2_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[1];
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_B_3_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[2];
        float _Split_8ee3f43b8caa0c8ab120e9a242f543ca_A_4_Float = _ScreenPosition_b95ac2e7459cca8794636de5b2f8ec39_Out_0_Vector4[3];
        float _Subtract_f130a0b621d472829a7045e95984f2a9_Out_2_Float;
        Unity_Subtract_float(_Multiply_abd94f3d0b36a08380f6ab11443878f2_Out_2_Float, _Split_8ee3f43b8caa0c8ab120e9a242f543ca_A_4_Float, _Subtract_f130a0b621d472829a7045e95984f2a9_Out_2_Float);
        float2 _Property_90bb3b6ce940ef868c970ea6aae7c26b_Out_0_Vector2 = Vector2_AC648482;
        float _Remap_db5867ad2542128e864c4c228cc1f3f5_Out_3_Float;
        Unity_Remap_float(_Subtract_f130a0b621d472829a7045e95984f2a9_Out_2_Float, _Property_90bb3b6ce940ef868c970ea6aae7c26b_Out_0_Vector2, float2 (0, 1), _Remap_db5867ad2542128e864c4c228cc1f3f5_Out_3_Float);
        float _Saturate_377f82d9fb446684a06124602b80b432_Out_1_Float;
        Unity_Saturate_float(_Remap_db5867ad2542128e864c4c228cc1f3f5_Out_3_Float, _Saturate_377f82d9fb446684a06124602b80b432_Out_1_Float);
        Output1_1 = _Saturate_377f82d9fb446684a06124602b80b432_Out_1_Float;
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
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float3 _SceneColor_797c5e0605664f268a243b5925dbab4e_Out_1_Vector3;
            Unity_SceneColor_float(float4(IN.NDCPosition.xy, 0, 0), _SceneColor_797c5e0605664f268a243b5925dbab4e_Out_1_Vector3);
            float4 _ScreenPosition_fd10197a85f44b469f625df7a4fee1ac_Out_0_Vector4 = float4(IN.NDCPosition.xy, 0, 0);
            float2 _Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2;
            Unity_Rotate_Degrees_float(IN.uv0.xy, float2 (0.5, 0.5), float(200.5), _Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2);
            float2 _Property_db2bf3c93cbf4880929bbbbd5b8d32c7_Out_0_Vector2 = _CellsTiling;
            float _Property_20fbe3c647284f08addb9b4125ce6af1_Out_0_Float = _CellsSpeed1;
            float _Multiply_e2672b0aac2d4a33a0d6cc512462530c_Out_2_Float;
            Unity_Multiply_float_float(IN.TimeParameters.x, _Property_20fbe3c647284f08addb9b4125ce6af1_Out_0_Float, _Multiply_e2672b0aac2d4a33a0d6cc512462530c_Out_2_Float);
            float2 _Vector2_0f755d5a1ff04d9b9de7cb63561d44f7_Out_0_Vector2 = float2(float(0), _Multiply_e2672b0aac2d4a33a0d6cc512462530c_Out_2_Float);
            float2 _TilingAndOffset_be8cabdd23314d6f9cb006714bd1aabe_Out_3_Vector2;
            Unity_TilingAndOffset_float(_Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2, _Property_db2bf3c93cbf4880929bbbbd5b8d32c7_Out_0_Vector2, _Vector2_0f755d5a1ff04d9b9de7cb63561d44f7_Out_0_Vector2, _TilingAndOffset_be8cabdd23314d6f9cb006714bd1aabe_Out_3_Vector2);
            float4 _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).tex, UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).samplerstate, UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).GetTransformedUV(_TilingAndOffset_be8cabdd23314d6f9cb006714bd1aabe_Out_3_Vector2) );
            if (UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).hdrDecode.x > 0)
                _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4, UnityBuildTexture2DStructNoScale(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_Texture_1_Texture2D).hdrDecode);
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_R_4_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.r;
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_G_5_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.g;
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_B_6_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.b;
            float _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_A_7_Float = _SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_RGBA_0_Vector4.a;
            float _Property_028d8bf17bf4473788d23219249f7451_Out_0_Float = _CellsSpeed2;
            float _Multiply_f0eecebc84f54abc89ef38eaa68f0774_Out_2_Float;
            Unity_Multiply_float_float(IN.TimeParameters.x, _Property_028d8bf17bf4473788d23219249f7451_Out_0_Float, _Multiply_f0eecebc84f54abc89ef38eaa68f0774_Out_2_Float);
            float2 _Vector2_f174d0c85fa546a58eb678ccf778d92c_Out_0_Vector2 = float2(float(0.2), _Multiply_f0eecebc84f54abc89ef38eaa68f0774_Out_2_Float);
            float2 _TilingAndOffset_8f83ef02ec4b473a9913eced11cf3340_Out_3_Vector2;
            Unity_TilingAndOffset_float(_Rotate_d694dfd0101f4716af64f01bff6d9cbc_Out_3_Vector2, _Property_db2bf3c93cbf4880929bbbbd5b8d32c7_Out_0_Vector2, _Vector2_f174d0c85fa546a58eb678ccf778d92c_Out_0_Vector2, _TilingAndOffset_8f83ef02ec4b473a9913eced11cf3340_Out_3_Vector2);
            float4 _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).tex, UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).samplerstate, UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).GetTransformedUV(_TilingAndOffset_8f83ef02ec4b473a9913eced11cf3340_Out_3_Vector2) );
            if (UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).hdrDecode.x > 0)
                _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4, UnityBuildTexture2DStructNoScale(_SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_Texture_1_Texture2D).hdrDecode);
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_R_4_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.r;
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_G_5_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.g;
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_B_6_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.b;
            float _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_A_7_Float = _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_RGBA_0_Vector4.a;
            float _Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float;
            Unity_Maximum_float(_SampleTexture2D_bfe53c5a4b814516bb4d49e60488741a_R_4_Float, _SampleTexture2D_5a8858ed6c90421e9949f3803aa9a2c4_R_4_Float, _Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float);
            float _Remap_b4e9369e64e949e880da5427e2de37ac_Out_3_Float;
            Unity_Remap_float(_Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float, float2 (0, 1), float2 (-1, 1), _Remap_b4e9369e64e949e880da5427e2de37ac_Out_3_Float);
            float _Property_5fcd73795f5f4d32a8136c2d77214ef7_Out_0_Float = _CellsIntensity;
            float _Multiply_0003ff715d4349d9b884f7fa02ed336d_Out_2_Float;
            Unity_Multiply_float_float(_Remap_b4e9369e64e949e880da5427e2de37ac_Out_3_Float, _Property_5fcd73795f5f4d32a8136c2d77214ef7_Out_0_Float, _Multiply_0003ff715d4349d9b884f7fa02ed336d_Out_2_Float);
            float4 _Add_5df1c353a4c941d48f93908d8b693661_Out_2_Vector4;
            Unity_Add_float4(_ScreenPosition_fd10197a85f44b469f625df7a4fee1ac_Out_0_Vector4, (_Multiply_0003ff715d4349d9b884f7fa02ed336d_Out_2_Float.xxxx), _Add_5df1c353a4c941d48f93908d8b693661_Out_2_Vector4);
            float2 _Property_61718f0a85b14358b09dbd29786d4112_Out_0_Vector2 = _NoiseTiling;
            float2 _Property_669fd697aeea497ea757ddf406adc31e_Out_0_Vector2 = _NoiseSpeed;
            float2 _Multiply_9341ca9d0a724c2fb64ac31413324f2c_Out_2_Vector2;
            Unity_Multiply_float2_float2((IN.TimeParameters.x.xx), _Property_669fd697aeea497ea757ddf406adc31e_Out_0_Vector2, _Multiply_9341ca9d0a724c2fb64ac31413324f2c_Out_2_Vector2);
            float2 _TilingAndOffset_e203137a933f462cae47a7786a69017b_Out_3_Vector2;
            Unity_TilingAndOffset_float((_Add_5df1c353a4c941d48f93908d8b693661_Out_2_Vector4.xy), _Property_61718f0a85b14358b09dbd29786d4112_Out_0_Vector2, _Multiply_9341ca9d0a724c2fb64ac31413324f2c_Out_2_Vector2, _TilingAndOffset_e203137a933f462cae47a7786a69017b_Out_3_Vector2);
            float4 _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D).tex, UnityBuildTexture2DStructNoScale(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D).samplerstate, UnityBuildTexture2DStructNoScale(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D).GetTransformedUV(_TilingAndOffset_e203137a933f462cae47a7786a69017b_Out_3_Vector2) );
            if (UnityBuildTexture2DStructNoScale(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D).hdrDecode.x > 0)
                _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4, UnityBuildTexture2DStructNoScale(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_Texture_1_Texture2D).hdrDecode);
            float _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_R_4_Float = _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4.r;
            float _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_G_5_Float = _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4.g;
            float _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_B_6_Float = _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4.b;
            float _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_A_7_Float = _SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_RGBA_0_Vector4.a;
            float _Remap_9915619d36d84b0ebfe6af2b431b96dc_Out_3_Float;
            Unity_Remap_float(_SampleTexture2D_43f028f520914bf2a0b3f59f8629eb51_R_4_Float, float2 (0, 1), float2 (-1, 1), _Remap_9915619d36d84b0ebfe6af2b431b96dc_Out_3_Float);
            float _Property_6b8d94abde1b469c9ef3297cb7e09445_Out_0_Float = _NoiseIntensity;
            float _Multiply_b09f52c62d914ac698c0794266029c75_Out_2_Float;
            Unity_Multiply_float_float(_Remap_9915619d36d84b0ebfe6af2b431b96dc_Out_3_Float, _Property_6b8d94abde1b469c9ef3297cb7e09445_Out_0_Float, _Multiply_b09f52c62d914ac698c0794266029c75_Out_2_Float);
            float4 _Add_d002c03d51f14f30af141e990ebf125b_Out_2_Vector4;
            Unity_Add_float4(_ScreenPosition_fd10197a85f44b469f625df7a4fee1ac_Out_0_Vector4, (_Multiply_b09f52c62d914ac698c0794266029c75_Out_2_Float.xxxx), _Add_d002c03d51f14f30af141e990ebf125b_Out_2_Vector4);
            float4 _Add_7bc501e7b51c415caef9a001d811c6df_Out_2_Vector4;
            Unity_Add_float4(_Add_d002c03d51f14f30af141e990ebf125b_Out_2_Vector4, (_Multiply_0003ff715d4349d9b884f7fa02ed336d_Out_2_Float.xxxx), _Add_7bc501e7b51c415caef9a001d811c6df_Out_2_Vector4);
            float4 _Property_4041d0fd2b654b26a12fe5921ae02a4e_Out_0_Vector4 = _SecondaryCellsTilingOffset;
            float2 _Swizzle_afe1c91ba1b94a8da9622fec61c36808_Out_1_Vector2 = (_Property_4041d0fd2b654b26a12fe5921ae02a4e_Out_0_Vector4.xy).xy;
            float2 _Swizzle_7bad8020207740dc9a08ed5cbafa7a9c_Out_1_Vector2 = _Property_4041d0fd2b654b26a12fe5921ae02a4e_Out_0_Vector4.zw;
            float2 _TilingAndOffset_d84fa061a1be4c66af6b7b69c8e42ee9_Out_3_Vector2;
            Unity_TilingAndOffset_float((_Add_5df1c353a4c941d48f93908d8b693661_Out_2_Vector4.xy), _Swizzle_afe1c91ba1b94a8da9622fec61c36808_Out_1_Vector2, _Swizzle_7bad8020207740dc9a08ed5cbafa7a9c_Out_1_Vector2, _TilingAndOffset_d84fa061a1be4c66af6b7b69c8e42ee9_Out_3_Vector2);
            float4 _SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4 = SAMPLE_TEXTURE2D(UnityBuildTexture2DStructNoScale(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D).tex, UnityBuildTexture2DStructNoScale(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D).samplerstate, UnityBuildTexture2DStructNoScale(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D).GetTransformedUV(_TilingAndOffset_d84fa061a1be4c66af6b7b69c8e42ee9_Out_3_Vector2) );
            if (UnityBuildTexture2DStructNoScale(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D).hdrDecode.x > 0)
                _SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4 = DecodeHDRSample(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4, UnityBuildTexture2DStructNoScale(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_Texture_1_Texture2D).hdrDecode);
            float _SampleTexture2D_2d280125d80e4736968e48ac98141acf_R_4_Float = _SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4.r;
            float _SampleTexture2D_2d280125d80e4736968e48ac98141acf_G_5_Float = _SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4.g;
            float _SampleTexture2D_2d280125d80e4736968e48ac98141acf_B_6_Float = _SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4.b;
            float _SampleTexture2D_2d280125d80e4736968e48ac98141acf_A_7_Float = _SampleTexture2D_2d280125d80e4736968e48ac98141acf_RGBA_0_Vector4.a;
            float _Remap_50f49c7bbb3f4e30a8a3b48af5408b21_Out_3_Float;
            Unity_Remap_float(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_R_4_Float, float2 (0, 1), float2 (-1, 1), _Remap_50f49c7bbb3f4e30a8a3b48af5408b21_Out_3_Float);
            float _Property_f05f92dc6e4f468f8bdd1154535a3d54_Out_0_Float = _SecondaryCellsDistortionAmount;
            float _Multiply_f3882cee4ac545c4aa8f7eb352afd853_Out_2_Float;
            Unity_Multiply_float_float(_Remap_50f49c7bbb3f4e30a8a3b48af5408b21_Out_3_Float, _Property_f05f92dc6e4f468f8bdd1154535a3d54_Out_0_Float, _Multiply_f3882cee4ac545c4aa8f7eb352afd853_Out_2_Float);
            float4 _Add_156fd364558e4068ba15a0f640bc2bfc_Out_2_Vector4;
            Unity_Add_float4(_Add_7bc501e7b51c415caef9a001d811c6df_Out_2_Vector4, (_Multiply_f3882cee4ac545c4aa8f7eb352afd853_Out_2_Float.xxxx), _Add_156fd364558e4068ba15a0f640bc2bfc_Out_2_Vector4);
            float3 _SceneColor_be2bb10b74d74724b26430d8097c9132_Out_1_Vector3;
            Unity_SceneColor_float(_Add_156fd364558e4068ba15a0f640bc2bfc_Out_2_Vector4, _SceneColor_be2bb10b74d74724b26430d8097c9132_Out_1_Vector3);
            float4 _Property_96426d6d56a54e1ca5d788c45c5e9a17_Out_0_Vector4 = _Color;
            float4 _Property_c180718d1cf84124bf9c17ca4739f3b9_Out_0_Vector4 = _Color2;
            float4 _Lerp_bcd4cf1dfeb74a6f9858eaee379c7d36_Out_3_Vector4;
            Unity_Lerp_float4(_Property_96426d6d56a54e1ca5d788c45c5e9a17_Out_0_Vector4, _Property_c180718d1cf84124bf9c17ca4739f3b9_Out_0_Vector4, (_Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float.xxxx), _Lerp_bcd4cf1dfeb74a6f9858eaee379c7d36_Out_3_Vector4);
            float4 _Property_f191c9709ecc402cb9ec1863c504e750_Out_0_Vector4 = _SecondaryCellsHightlightColor;
            float _Property_33ac0341637943b79dc4220e90bc6cb3_Out_0_Float = _SecondaryCellsLightenAmount;
            float _Multiply_0c6f8dc9b9624ddd83466012c1f0473e_Out_2_Float;
            Unity_Multiply_float_float(_SampleTexture2D_2d280125d80e4736968e48ac98141acf_R_4_Float, _Property_33ac0341637943b79dc4220e90bc6cb3_Out_0_Float, _Multiply_0c6f8dc9b9624ddd83466012c1f0473e_Out_2_Float);
            float4 _Lerp_70729269dac64cb8bd68f49a994a0c87_Out_3_Vector4;
            Unity_Lerp_float4(_Lerp_bcd4cf1dfeb74a6f9858eaee379c7d36_Out_3_Vector4, _Property_f191c9709ecc402cb9ec1863c504e750_Out_0_Vector4, (_Multiply_0c6f8dc9b9624ddd83466012c1f0473e_Out_2_Float.xxxx), _Lerp_70729269dac64cb8bd68f49a994a0c87_Out_3_Vector4);
            float2 _Property_7ecb7f3889da4edd8d256eeb7dc379c0_Out_0_Vector2 = _SceneColorLevelsCorrection;
            float _Split_1b1faf2fc072490089f587c7a8dd0365_R_1_Float = _Property_7ecb7f3889da4edd8d256eeb7dc379c0_Out_0_Vector2[0];
            float _Split_1b1faf2fc072490089f587c7a8dd0365_G_2_Float = _Property_7ecb7f3889da4edd8d256eeb7dc379c0_Out_0_Vector2[1];
            float _Split_1b1faf2fc072490089f587c7a8dd0365_B_3_Float = 0;
            float _Split_1b1faf2fc072490089f587c7a8dd0365_A_4_Float = 0;
            float3 _Saturation_62cdd009cbfc43b5a6909a6ed8d85c62_Out_2_Vector3;
            Unity_Saturation_float(_SceneColor_be2bb10b74d74724b26430d8097c9132_Out_1_Vector3, float(0), _Saturation_62cdd009cbfc43b5a6909a6ed8d85c62_Out_2_Vector3);
            float3 _Smoothstep_2ad597db2b9d456398efcdde08dca730_Out_3_Vector3;
            Unity_Smoothstep_float3((_Split_1b1faf2fc072490089f587c7a8dd0365_R_1_Float.xxx), (_Split_1b1faf2fc072490089f587c7a8dd0365_G_2_Float.xxx), _Saturation_62cdd009cbfc43b5a6909a6ed8d85c62_Out_2_Vector3, _Smoothstep_2ad597db2b9d456398efcdde08dca730_Out_3_Vector3);
            float3 _Lerp_b0fa91facd6a432ab71c6b036f65e3a3_Out_3_Vector3;
            Unity_Lerp_float3(_SceneColor_be2bb10b74d74724b26430d8097c9132_Out_1_Vector3, (_Lerp_70729269dac64cb8bd68f49a994a0c87_Out_3_Vector4.xyz), _Smoothstep_2ad597db2b9d456398efcdde08dca730_Out_3_Vector3, _Lerp_b0fa91facd6a432ab71c6b036f65e3a3_Out_3_Vector3);
            float3 _Property_aa59c625101940d5a091f21db9362c1f_Out_0_Vector3 = _ChromaticAberrationColorMax;
            float _Property_0c3440cb57bf462abacb04bf3eda2ee2_Out_0_Float = _ChromaticAberrationOpacity;
            float2 _Property_c658b9f8acd14a91ae75168205cee28c_Out_0_Vector2 = _ChromaticAberrationLevelsCorrection;
            float _Split_be90da803c834a459aa11bd685c539c1_R_1_Float = _Property_c658b9f8acd14a91ae75168205cee28c_Out_0_Vector2[0];
            float _Split_be90da803c834a459aa11bd685c539c1_G_2_Float = _Property_c658b9f8acd14a91ae75168205cee28c_Out_0_Vector2[1];
            float _Split_be90da803c834a459aa11bd685c539c1_B_3_Float = 0;
            float _Split_be90da803c834a459aa11bd685c539c1_A_4_Float = 0;
            float _Property_3d28fedd9c1b4ae8925c13ed8822d26c_Out_0_Float = _ChromaticAberration;
            float _Multiply_66933dbf527542539db7923a55361940_Out_2_Float;
            Unity_Multiply_float_float(_Property_3d28fedd9c1b4ae8925c13ed8822d26c_Out_0_Float, 0.001, _Multiply_66933dbf527542539db7923a55361940_Out_2_Float);
            float _Negate_4f6a77e415ec48d0905951f646fd06d0_Out_1_Float;
            Unity_Negate_float(_Multiply_66933dbf527542539db7923a55361940_Out_2_Float, _Negate_4f6a77e415ec48d0905951f646fd06d0_Out_1_Float);
            float2 _Vector2_60e5e05cb9c94b44b8ef356c8a0cf51a_Out_0_Vector2 = float2(_Negate_4f6a77e415ec48d0905951f646fd06d0_Out_1_Float, _Multiply_66933dbf527542539db7923a55361940_Out_2_Float);
            float2 _Add_ae2604b2df614aa3b9dcb1ecec9f4486_Out_2_Vector2;
            Unity_Add_float2((_Add_156fd364558e4068ba15a0f640bc2bfc_Out_2_Vector4.xy), _Vector2_60e5e05cb9c94b44b8ef356c8a0cf51a_Out_0_Vector2, _Add_ae2604b2df614aa3b9dcb1ecec9f4486_Out_2_Vector2);
            float3 _SceneColor_6efd0253931e4e5883787f89929021e1_Out_1_Vector3;
            Unity_SceneColor_float((float4(_Add_ae2604b2df614aa3b9dcb1ecec9f4486_Out_2_Vector2, 0.0, 1.0)), _SceneColor_6efd0253931e4e5883787f89929021e1_Out_1_Vector3);
            float3 _Saturation_57b2d1dc792d4b9faf785251dadc5558_Out_2_Vector3;
            Unity_Saturation_float(_SceneColor_6efd0253931e4e5883787f89929021e1_Out_1_Vector3, float(0), _Saturation_57b2d1dc792d4b9faf785251dadc5558_Out_2_Vector3);
            float3 _Smoothstep_37ecb3e11f0a4fff9ebd9656d6cecf39_Out_3_Vector3;
            Unity_Smoothstep_float3((_Split_be90da803c834a459aa11bd685c539c1_R_1_Float.xxx), (_Split_be90da803c834a459aa11bd685c539c1_G_2_Float.xxx), _Saturation_57b2d1dc792d4b9faf785251dadc5558_Out_2_Vector3, _Smoothstep_37ecb3e11f0a4fff9ebd9656d6cecf39_Out_3_Vector3);
            float2 _Vector2_6765997c92d54a82a7e0955e0fc51e93_Out_0_Vector2 = float2(_Negate_4f6a77e415ec48d0905951f646fd06d0_Out_1_Float, _Negate_4f6a77e415ec48d0905951f646fd06d0_Out_1_Float);
            float2 _Add_72ed283444024fd8ab4da82b3a998f33_Out_2_Vector2;
            Unity_Add_float2((_Add_156fd364558e4068ba15a0f640bc2bfc_Out_2_Vector4.xy), _Vector2_6765997c92d54a82a7e0955e0fc51e93_Out_0_Vector2, _Add_72ed283444024fd8ab4da82b3a998f33_Out_2_Vector2);
            float3 _SceneColor_e0be9321e1dd40d1bb12f0a0bc1519c1_Out_1_Vector3;
            Unity_SceneColor_float((float4(_Add_72ed283444024fd8ab4da82b3a998f33_Out_2_Vector2, 0.0, 1.0)), _SceneColor_e0be9321e1dd40d1bb12f0a0bc1519c1_Out_1_Vector3);
            float3 _Saturation_ee467f8815fd444391dd4277dbf70b91_Out_2_Vector3;
            Unity_Saturation_float(_SceneColor_e0be9321e1dd40d1bb12f0a0bc1519c1_Out_1_Vector3, float(0), _Saturation_ee467f8815fd444391dd4277dbf70b91_Out_2_Vector3);
            float3 _Smoothstep_c946ec1206c54154af27837cf3914d85_Out_3_Vector3;
            Unity_Smoothstep_float3((_Split_be90da803c834a459aa11bd685c539c1_R_1_Float.xxx), (_Split_be90da803c834a459aa11bd685c539c1_G_2_Float.xxx), _Saturation_ee467f8815fd444391dd4277dbf70b91_Out_2_Vector3, _Smoothstep_c946ec1206c54154af27837cf3914d85_Out_3_Vector3);
            float2 _Vector2_4c1b45d58a7c4335a13602bcb14e057d_Out_0_Vector2 = float2(_Multiply_66933dbf527542539db7923a55361940_Out_2_Float, _Negate_4f6a77e415ec48d0905951f646fd06d0_Out_1_Float);
            float2 _Add_a18e51b946e9474aa6c509838493bcf0_Out_2_Vector2;
            Unity_Add_float2((_Add_156fd364558e4068ba15a0f640bc2bfc_Out_2_Vector4.xy), _Vector2_4c1b45d58a7c4335a13602bcb14e057d_Out_0_Vector2, _Add_a18e51b946e9474aa6c509838493bcf0_Out_2_Vector2);
            float3 _SceneColor_bbadb668faae4fee86cd3d7901ca0b69_Out_1_Vector3;
            Unity_SceneColor_float((float4(_Add_a18e51b946e9474aa6c509838493bcf0_Out_2_Vector2, 0.0, 1.0)), _SceneColor_bbadb668faae4fee86cd3d7901ca0b69_Out_1_Vector3);
            float3 _Saturation_806176d4b1ee4b73a59c78bd57963c23_Out_2_Vector3;
            Unity_Saturation_float(_SceneColor_bbadb668faae4fee86cd3d7901ca0b69_Out_1_Vector3, float(0), _Saturation_806176d4b1ee4b73a59c78bd57963c23_Out_2_Vector3);
            float3 _Smoothstep_f0a69a29cd3b4b43b5dcfa9febb15269_Out_3_Vector3;
            Unity_Smoothstep_float3((_Split_be90da803c834a459aa11bd685c539c1_R_1_Float.xxx), (_Split_be90da803c834a459aa11bd685c539c1_G_2_Float.xxx), _Saturation_806176d4b1ee4b73a59c78bd57963c23_Out_2_Vector3, _Smoothstep_f0a69a29cd3b4b43b5dcfa9febb15269_Out_3_Vector3);
            float4 _Combine_d72d384256874619b4a04639e94f206d_RGBA_4_Vector4;
            float3 _Combine_d72d384256874619b4a04639e94f206d_RGB_5_Vector3;
            float2 _Combine_d72d384256874619b4a04639e94f206d_RG_6_Vector2;
            Unity_Combine_float((_Smoothstep_37ecb3e11f0a4fff9ebd9656d6cecf39_Out_3_Vector3).x, (_Smoothstep_c946ec1206c54154af27837cf3914d85_Out_3_Vector3).x, (_Smoothstep_f0a69a29cd3b4b43b5dcfa9febb15269_Out_3_Vector3).x, float(0), _Combine_d72d384256874619b4a04639e94f206d_RGBA_4_Vector4, _Combine_d72d384256874619b4a04639e94f206d_RGB_5_Vector3, _Combine_d72d384256874619b4a04639e94f206d_RG_6_Vector2);
            float3 _Multiply_266dacc6e1054dd885156572661c7eb1_Out_2_Vector3;
            Unity_Multiply_float3_float3((_Property_0c3440cb57bf462abacb04bf3eda2ee2_Out_0_Float.xxx), _Combine_d72d384256874619b4a04639e94f206d_RGB_5_Vector3, _Multiply_266dacc6e1054dd885156572661c7eb1_Out_2_Vector3);
            float3 _Lerp_3dd9073d874244ae88c8688335cc44a3_Out_3_Vector3;
            Unity_Lerp_float3(_Lerp_b0fa91facd6a432ab71c6b036f65e3a3_Out_3_Vector3, _Property_aa59c625101940d5a091f21db9362c1f_Out_0_Vector3, _Multiply_266dacc6e1054dd885156572661c7eb1_Out_2_Vector3, _Lerp_3dd9073d874244ae88c8688335cc44a3_Out_3_Vector3);
            float4 _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4 = IN.uv0;
            float _Split_e649f2f75c6e474a848600755e877ab3_R_1_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[0];
            float _Split_e649f2f75c6e474a848600755e877ab3_G_2_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[1];
            float _Split_e649f2f75c6e474a848600755e877ab3_B_3_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[2];
            float _Split_e649f2f75c6e474a848600755e877ab3_A_4_Float = _UV_acef68798c674e10877ff9a9418b145c_Out_0_Vector4[3];
            float _OneMinus_96ffa84f34534286bacbeac79c1e6a04_Out_1_Float;
            Unity_OneMinus_float(_Split_e649f2f75c6e474a848600755e877ab3_R_1_Float, _OneMinus_96ffa84f34534286bacbeac79c1e6a04_Out_1_Float);
            float _Multiply_55d6e7f4b6d246399079ceee11e52645_Out_2_Float;
            Unity_Multiply_float_float(_Split_e649f2f75c6e474a848600755e877ab3_R_1_Float, _OneMinus_96ffa84f34534286bacbeac79c1e6a04_Out_1_Float, _Multiply_55d6e7f4b6d246399079ceee11e52645_Out_2_Float);
            float _OneMinus_22f755e38c114b9ca586c68d75f661d2_Out_1_Float;
            Unity_OneMinus_float(_Split_e649f2f75c6e474a848600755e877ab3_G_2_Float, _OneMinus_22f755e38c114b9ca586c68d75f661d2_Out_1_Float);
            float _Multiply_df72511ff7b44e8983661009c4ed7d02_Out_2_Float;
            Unity_Multiply_float_float(_Split_e649f2f75c6e474a848600755e877ab3_G_2_Float, _OneMinus_22f755e38c114b9ca586c68d75f661d2_Out_1_Float, _Multiply_df72511ff7b44e8983661009c4ed7d02_Out_2_Float);
            float _Multiply_9d0107fe0fc041d483af0038e916149b_Out_2_Float;
            Unity_Multiply_float_float(_Multiply_55d6e7f4b6d246399079ceee11e52645_Out_2_Float, _Multiply_df72511ff7b44e8983661009c4ed7d02_Out_2_Float, _Multiply_9d0107fe0fc041d483af0038e916149b_Out_2_Float);
            float _Smoothstep_abb9639c55aa4810bae13fb0b4949e74_Out_3_Float;
            Unity_Smoothstep_float(float(0), float(0.05), _Multiply_9d0107fe0fc041d483af0038e916149b_Out_2_Float, _Smoothstep_abb9639c55aa4810bae13fb0b4949e74_Out_3_Float);
            float2 _Property_182212378d374ce8bfa20f4f52d6532d_Out_0_Vector2 = _CircleSmoothstep;
            float _Split_075223437c63458bb5e99e2413cc2604_R_1_Float = _Property_182212378d374ce8bfa20f4f52d6532d_Out_0_Vector2[0];
            float _Split_075223437c63458bb5e99e2413cc2604_G_2_Float = _Property_182212378d374ce8bfa20f4f52d6532d_Out_0_Vector2[1];
            float _Split_075223437c63458bb5e99e2413cc2604_B_3_Float = 0;
            float _Split_075223437c63458bb5e99e2413cc2604_A_4_Float = 0;
            float4 _UV_a8efe86069ac4781a2dc587a5eb92bf9_Out_0_Vector4 = IN.uv0;
            float2 _Vector2_494e0112c5b5405991a62fa7d5437cf9_Out_0_Vector2 = float2(float(0.5), float(0.5));
            float _Distance_b68a33dde5ea4dbb995d9993afa14d01_Out_2_Float;
            Unity_Distance_float2((_UV_a8efe86069ac4781a2dc587a5eb92bf9_Out_0_Vector4.xy), _Vector2_494e0112c5b5405991a62fa7d5437cf9_Out_0_Vector2, _Distance_b68a33dde5ea4dbb995d9993afa14d01_Out_2_Float);
            float _Smoothstep_82f21a94208f4efba46de32f75274968_Out_3_Float;
            Unity_Smoothstep_float(_Split_075223437c63458bb5e99e2413cc2604_G_2_Float, _Split_075223437c63458bb5e99e2413cc2604_R_1_Float, _Distance_b68a33dde5ea4dbb995d9993afa14d01_Out_2_Float, _Smoothstep_82f21a94208f4efba46de32f75274968_Out_3_Float);
            float _Property_5bfabba54df4446ca8c86d35440d67c1_Out_0_Float = _CirclePower;
            float _Power_7c70f6a6e559406e8ae2294ab1eebe04_Out_2_Float;
            Unity_Power_float(_Maximum_199f4e57b51a4422ab69b12cf0473c2c_Out_2_Float, _Property_5bfabba54df4446ca8c86d35440d67c1_Out_0_Float, _Power_7c70f6a6e559406e8ae2294ab1eebe04_Out_2_Float);
            float _Add_b72754a7e33c41faa48a4ea1fa2edfb3_Out_2_Float;
            Unity_Add_float(_Smoothstep_82f21a94208f4efba46de32f75274968_Out_3_Float, _Power_7c70f6a6e559406e8ae2294ab1eebe04_Out_2_Float, _Add_b72754a7e33c41faa48a4ea1fa2edfb3_Out_2_Float);
            float _Multiply_374c554884fb4f6b9ab82528b2a73e85_Out_2_Float;
            Unity_Multiply_float_float(_Smoothstep_abb9639c55aa4810bae13fb0b4949e74_Out_3_Float, _Add_b72754a7e33c41faa48a4ea1fa2edfb3_Out_2_Float, _Multiply_374c554884fb4f6b9ab82528b2a73e85_Out_2_Float);
            float _Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float;
            Unity_Saturate_float(_Multiply_374c554884fb4f6b9ab82528b2a73e85_Out_2_Float, _Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float);
            float3 _Lerp_180be64a42cb4958819f855681034de2_Out_3_Vector3;
            Unity_Lerp_float3(_SceneColor_797c5e0605664f268a243b5925dbab4e_Out_1_Vector3, _Lerp_3dd9073d874244ae88c8688335cc44a3_Out_3_Vector3, (_Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float.xxx), _Lerp_180be64a42cb4958819f855681034de2_Out_3_Vector3);
            Bindings_SoftParticles_d0f40856fc355224fab6453746e0507a_float _SoftParticles_2d24478f7f3246149eb145045221b789;
            _SoftParticles_2d24478f7f3246149eb145045221b789.ScreenPosition = IN.ScreenPosition;
            _SoftParticles_2d24478f7f3246149eb145045221b789.NDCPosition = IN.NDCPosition;
            float _SoftParticles_2d24478f7f3246149eb145045221b789_Output1_1_Float;
            SG_SoftParticles_d0f40856fc355224fab6453746e0507a_float(float2 (0, 0.5), _SoftParticles_2d24478f7f3246149eb145045221b789, _SoftParticles_2d24478f7f3246149eb145045221b789_Output1_1_Float);
            float _Multiply_de2953022bc948e69f7268a693c8f3e3_Out_2_Float;
            Unity_Multiply_float_float(_Saturate_016bf538941e4f7087d316c8415b14a3_Out_1_Float, _SoftParticles_2d24478f7f3246149eb145045221b789_Output1_1_Float, _Multiply_de2953022bc948e69f7268a693c8f3e3_Out_2_Float);
            surface.BaseColor = _Lerp_180be64a42cb4958819f855681034de2_Out_3_Vector3;
            surface.Alpha = _Multiply_de2953022bc948e69f7268a693c8f3e3_Out_2_Float;
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
        
            
        
        
        
        
        
            output.WorldSpacePosition = input.positionWS;
            output.ScreenPosition = ComputeScreenPos(TransformWorldToHClip(input.positionWS), _ProjectionParams.x);
        
            #if UNITY_UV_STARTS_AT_TOP
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x < 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #else
            output.PixelPosition = float2(input.positionCS.x, (_ProjectionParams.x > 0) ? (_ScaledScreenParams.y - input.positionCS.y) : input.positionCS.y);
            #endif
        
            output.NDCPosition = output.PixelPosition.xy / _ScaledScreenParams.xy;
            output.NDCPosition.y = 1.0f - output.NDCPosition.y;
        
            output.uv0 = input.texCoord0;
        #if UNITY_ANY_INSTANCING_ENABLED
        #else // TODO: XR support for procedural instancing because in this case UNITY_ANY_INSTANCING_ENABLED is not defined and instanceID is incorrect.
        #endif
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
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
    }
    CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
    CustomEditorForRenderPipeline "UnityEditor.ShaderGraphUnlitGUI" "UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset"
    FallBack "Hidden/Shader Graph/FallbackError"
}
Shader "Lightweight Render Pipeline/2D/Sprite-Lit-Dither"
{
    Properties
    {
        _MainTex("Diffuse", 2D) = "white" {}
        _MaskTex("Mask", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "bump" {}
        _DitherTex("Dither Texture", 2D) = "white" {}
		_DitherScale("Scale", Float)  = 0.5
		_DitherFactor("Dither Power", Float)  = 0
		_AccentCutout("Accent Cutout", Float)  = 0.5

    }

    HLSLINCLUDE
    #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
    ENDHLSL

    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType" = "Transparent" "RenderPipeline" = "LightweightPipeline" }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            Tags { "LightMode" = "Lightweight2D" }
            HLSLPROGRAM
            #pragma prefer_hlslcc gles
            #pragma vertex CombinedShapeLightVertex
            #pragma fragment CombinedShapeLightFragment
            #pragma multi_compile USE_SHAPE_LIGHT_TYPE_0 __
            #pragma multi_compile USE_SHAPE_LIGHT_TYPE_1 __
            #pragma multi_compile USE_SHAPE_LIGHT_TYPE_2 __
            #pragma multi_compile USE_SHAPE_LIGHT_TYPE_3 __

            struct Attributes
            {
                float3 positionOS   : POSITION;
                float4 color        : COLOR;
                float2  uv           : TEXCOORD0;
            };

            struct Varyings
            {
                float4  positionCS  : SV_POSITION;
                float4  color       : COLOR;
                float2	uv          : TEXCOORD0;
                float2	lightingUV  : TEXCOORD1;
            };

            #include "Packages/com.unity.render-pipelines.lightweight/Shaders/2D/Include/LightingUtility.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(_MaskTex);
            SAMPLER(sampler_MaskTex);
            TEXTURE2D(_NormalMap);
            SAMPLER(sampler_NormalMap);
            TEXTURE2D(_DitherTex);
            SAMPLER(sampler_DitherTex);
			half4 _DitherTex_TexelSize;
            half4 _MainTex_ST;
            half4 _NormalMap_ST;
			float _DitherScale;
			float _DitherFactor;
			float _AccentCutout;

            #if USE_SHAPE_LIGHT_TYPE_0
            SHAPE_LIGHT(0)
            #endif

            #if USE_SHAPE_LIGHT_TYPE_1
            SHAPE_LIGHT(1)
            #endif

            #if USE_SHAPE_LIGHT_TYPE_2
            SHAPE_LIGHT(2)
            #endif

            #if USE_SHAPE_LIGHT_TYPE_3
            SHAPE_LIGHT(3)
            #endif

            Varyings CombinedShapeLightVertex(Attributes v)
            {
                Varyings o = (Varyings)0;

                o.positionCS = TransformObjectToHClip(v.positionOS);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                float4 clipVertex = o.positionCS / o.positionCS.w;
                o.lightingUV = ComputeScreenPos(clipVertex).xy;
                o.color = v.color; 
                return o;
            }

            //#include "Assets/Include/CombinedAccentShapeLightShared.hlsl"
			#include "Packages/com.unity.render-pipelines.lightweight/Shaders/2D/Include/CombinedShapeLightShared.hlsl"

            half4 CombinedShapeLightFragment(Varyings i) : SV_Target
            {
                half4 main = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                half4 mask = SAMPLE_TEXTURE2D(_MaskTex, sampler_MaskTex, i.uv);
                //half4 color = CombinedAccentShapeLightShared(main, mask, i.lightingUV);
				half4 color = CombinedShapeLightShared(main, mask, i.lightingUV);
				
				float2 screenRatio = float2(_ScreenParams.x * (_ScreenParams.w-1), 1);
				float2 textureScale = _DitherTex_TexelSize.zw * screenRatio / _DitherScale;
				float2 screenCoords = i.lightingUV * textureScale;

				half4 dither = SAMPLE_TEXTURE2D(_DitherTex, sampler_DitherTex, screenCoords) + 0.01;
				
				half step = dot(color.xyz, color.xyz) > ( _DitherFactor * (dither * dot(main.xyz, main.xyz)));
				half4 result = step * main;


				half4 red = half4(color.r,0,0, 0);
				half4 green = half4(0,color.g,0, 0);
				half4 blue =  half4(0,0,color.b, 0);
				half4 accent = red;
				const half eqThreshold = 0.1;

				half grtr = dot(accent, accent) > dot(green, green);
				accent = accent * grtr + green * (1-grtr);
				grtr = dot(accent, accent) > dot(blue, blue);
				accent = accent * grtr + blue * (1-grtr); 

				half noAccent = abs(max(color.r, color.g) - max(color.g, color.b)) < eqThreshold
							&& abs(max(color.r, color.b) - max(color.g, color.b)) < eqThreshold;
				accent = (noAccent) * half4(1,1,1, 1) + (1 - noAccent) * accent; 
				
				half factor = dot(accent, accent) / dot(color, color);
				
				accent = accent * factor;

				accent.a = 1;
				
				half4 accentColor = half4(0,0,0,1);
				
				accentColor += half4(1,0,0,0) * (accent.r > eqThreshold);
				accentColor += half4(0,1,0,0) * (accent.g > eqThreshold);
				accentColor += half4(0,0,1,0) * (accent.b > eqThreshold);
				accentColor += (dot(accentColor.xyz, accentColor.xyz) == 0) * half4(1,1,1,1);
				
				half accentStep = dot(accent, accent)/ _AccentCutout > ((dither * dot(main.xyz, main.xyz)));

				accentColor = (1 - accentStep) * half4(1, 1, 1, 1) + accentStep * accentColor;
				result = result * accentColor;
				
				result.w = color.w;
				return result;
				return half4(screenCoords.x, screenCoords.y, 0, 1);
            }
            ENDHLSL
        }

        Pass
        {
            Tags { "LightMode" = "NormalsRendering"}
            HLSLPROGRAM
            #pragma prefer_hlslcc gles
            #pragma vertex NormalsRenderingVertex
            #pragma fragment NormalsRenderingFragment

            struct Attributes
            {
                float3 positionOS   : POSITION;
                float4 color		: COLOR;
                float2 uv			: TEXCOORD0;
            };

            struct Varyings
            {
                float4  positionCS		: SV_POSITION;
                float4  color			: COLOR;
                float2	uv				: TEXCOORD0;
                float3  normalWS		: TEXCOORD1;
                float3  tangentWS		: TEXCOORD2;
                float3  bitangentWS		: TEXCOORD3;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(_NormalMap);
            SAMPLER(sampler_NormalMap);
            float4 _NormalMap_ST;  // Is this the right way to do this?

            Varyings NormalsRenderingVertex(Attributes attributes)
            {
                Varyings o = (Varyings)0;

                o.positionCS = TransformObjectToHClip(attributes.positionOS);
                o.uv = TRANSFORM_TEX(attributes.uv, _NormalMap);
                o.uv = attributes.uv;
                o.color = attributes.color;
                o.normalWS = TransformObjectToWorldDir(float3(0, 0, 1));
                o.tangentWS = TransformObjectToWorldDir(float3(1, 0, 0));
                o.bitangentWS = TransformObjectToWorldDir(float3(0, 1, 0));
                return o;
            }

            #include "Packages/com.unity.render-pipelines.lightweight/Shaders/2D/Include/NormalsRenderingShared.hlsl"

            float4 NormalsRenderingFragment(Varyings i) : SV_Target
            {
                float4 mainTex = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                float3 normalTS = UnpackNormal(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, i.uv));
                return NormalsRenderingShared(mainTex, normalTS, i.tangentWS.xyz, i.bitangentWS.xyz, -i.normalWS.xyz);
            }
            ENDHLSL
        }
        Pass
        {
            Tags { "LightMode" = "LightweightForward" "Queue"="Transparent" "RenderType"="Transparent"}

            HLSLPROGRAM
            #pragma prefer_hlslcc gles
            #pragma vertex UnlitVertex
            #pragma fragment UnlitFragment

            struct Attributes
            {
                float3 positionOS   : POSITION;
                float4 color		: COLOR;
                float2 uv			: TEXCOORD0;
            };

            struct Varyings
            {
                float4  positionCS		: SV_POSITION;
                float4  color			: COLOR;
                float2	uv				: TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _MainTex_ST;

            Varyings UnlitVertex(Attributes attributes)
            {
                Varyings o = (Varyings)0;

                o.positionCS = TransformObjectToHClip(attributes.positionOS);
                o.uv = TRANSFORM_TEX(attributes.uv, _MainTex);
                o.uv = attributes.uv;
                o.color = attributes.color;
                return o;
            }

            float4 UnlitFragment(Varyings i) : SV_Target
            {
                float4 mainTex = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                return mainTex;
            }
            ENDHLSL
        }
    }
    Fallback "Hidden/Sprite-Fallback"
}

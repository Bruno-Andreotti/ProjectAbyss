Shader "Unlit/shadertoyport"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            // "Vortex Street" by dr2 - 2015
// License: Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License

// Motivated by implementation of van Wijk's IBFV by eiffie (lllGDl) and andregc (4llGWl) 

#define mod(x, y) (x-y*floor(x/y))


static const float4 cHashA4 = float4 (0., 1., 57., 58.);
static const float3 cHashA3 = float3 (1., 57., 113.);
static const float cHashM = 43758.54;

float4 Hashv4f (float p)
{
  return frac (sin (p + cHashA4) * cHashM);
}

float Noisefv2 (float2 p)
{
  float2 i = floor (p);
  float2 f = frac (p);
  f = f * f * (3. - 2. * f);
  float4 t = Hashv4f (dot (i, cHashA3.xy));
  return lerp (lerp (t.x, t.y, f.x), lerp (t.z, t.w, f.x), f.y);
}

float Fbm2 (float2 p)
{
  float s = 0.;
  float a = 1.;
  for (int i = 0; i < 6; i ++) {
    s += a * Noisefv2 (p);
    a *= 0.5;
    p *= 2.;
  }
  return s;
}

float tCur;

float2 VortF (float2 q, float2 c)
{
  float2 d = q - c;
  return 0.25 * float2 (d.y, - d.x) / (dot (d, d) + 0.05);
}

float2 FlowField (float2 q)
{
  float2 vr, c;
  float dir = 1.;
  c = float2 (mod (tCur, 10.) - 20., 0.6 * dir);
  vr = float2 (0., 0);
  for (int k = 0; k < 30; k ++) {
    vr += dir * VortF (4. * q, c);
    c = float2 (c.x + 1., - c.y);
    dir = - dir;
  }
  return vr;
}
fixed4 frag (v2f i) : SV_Target
            {
               
  float2 uv = i.uv - 0.5;
  
  tCur = _Time.y;
  float2 p = uv;
  for (int i = 0; i < 10; i ++) p -= FlowField (p) * 0.03;
  float3 col = Fbm2 (5. * p + float2 (-0.1 * tCur, 0.)) *
     float3 (0.5, 0.5, 1.);
  return float4 (col, 1.);
}

            
            ENDCG
        }
    }
}

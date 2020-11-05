Shader "lit/MyLit"

 {

 Properties

 {

 _MainTex ("Texture", 2D) = "white" {}

 _FakeLight("FakeLight",Vector)=(0,1,0,0)

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
#include "UnityLightingCommon.cginc"


struct appdata

 {

 float4 vertex : POSITION;

 float4 norm: NORMAL;

 float2 uv : TEXCOORD0;

 };



struct v2f

 {

 float2 uv : TEXCOORD0;

 UNITY_FOG_COORDS(1)

 float4 vertex : SV_POSITION;

 float3 normal : TEXCOORD2;

 };



sampler2D _MainTex;

 float4 _MainTex_ST;

 float4 _FakeLight;

 v2f vert (appdata v)

 {

 v2f o;

 o.normal = UnityObjectToWorldDir(v.norm);

 o.vertex = UnityObjectToClipPos(v.vertex);

 o.uv = TRANSFORM_TEX(v.uv, _MainTex);

 UNITY_TRANSFER_FOG(o,o.vertex);

 return o;

 }



fixed4 frag(v2f i) : SV_Target

 {

 // sample the texture

 fixed4 col = tex2D(_MainTex, i.uv);

 //fixed4 col = fixed4(i.normal,1);


 float ilumin = dot(i.normal, _WorldSpaceLightPos0);


 // apply fog

 UNITY_APPLY_FOG(i.fogCoord, col);

 return col*ilumin;

 }

 ENDCG

 }

 }

 }

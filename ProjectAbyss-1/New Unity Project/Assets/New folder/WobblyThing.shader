Shader "Unlit/NewUnlitShader"
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

#define FARCLIP    35.0

#define MARCHSTEPS 60
#define AOSTEPS    8
#define SHSTEPS    10
#define SHPOWER    3.0

#define PI         3.14
#define PI2        PI*0.5    

#define AMBCOL     float3(1.0,1.0,1.0)
#define BACCOL     float3(1.0,1.0,1.0)
#define DIFCOL     float3(1.0,1.0,1.0)

#define float1x1       1.0

#define FOV 1.0

#define mod(x,y) (x-y*floor(x/y))


			/***********************************************/
			static float rbox(float3 p, float3 s, float r) {
				return length(max(abs(p) - s + r, 0.0)) - r;
			}
			static float torus(float3 p, float2 t) {
				float2 q = float2(length(p.xz) - t.x, p.y);
				return length(q) - t.y;
			}
			static float cylinder(float3 p, float2 h) {
				return max(length(p.xz) - h.x, abs(p.y) - h.y);
			}

			/***********************************************/
			void oprep2(inout float2 p, float l, float s, float k) {
				float r = 1. / l;
				float ofs = s + s / (r*2.0);
				float a = mod(atan2(p.y, p.x) + PI2 * r*k, PI*r) - PI2 * r;
				p.xy = float2(sin(a), cos(a))*length(p.xy) - ofs;
				p.x += ofs;
			}

			float hash(float n) {
				return frac(sin(n)*43758.5453123);
			}

			float noise3(float3 x) {
				float3 p = floor(x);
				float3 f = frac(x);
				f = f * f*(3.0 - 2.0*f);
				float n = p.x + p.y*57.0 + p.z*113.0;
				float res = lerp(lerp(lerp(hash(n + 0.0), hash(n + 1.0), f.x),
					lerp(hash(n + 57.0), hash(n + 58.0), f.x), f.y),
					lerp(lerp(hash(n + 113.0), hash(n + 114.0), f.x),
						lerp(hash(n + 170.0), hash(n + 171.0), f.x), f.y), f.z);
				return res;
			}

			float sminp(float a, float b) {
				const float k = 0.1;
				float h = clamp(0.5 + 0.5*(b - a) / k, 0.0, 1.0);
				return lerp(b, a, h) - k * h*(1.0 - h);
			}


			/***********************************************/

			float2 DE(float3 p) {

				//distortion
				float d3 = noise3(p*2.0 + _Time.y)*0.18;
				//shape
				float h = torus(p, float2(3.0, 1.5)) - d3;
				float h2 = torus(p, float2(3.0, 1.45)) - d3;
				float3 q = p.yzx; p.yz = q.yx;
				oprep2(p.xy, 32.0, 0.15, 0.0);
				oprep2(p.yz, 14.0, 0.15, 0.0);
				float flag = p.z;
				float k = rbox(p, float3(0.05, 0.05, 1.0), 0.0);
				if (flag > 0.1) k -= flag * 0.18; else k -= 0.01;

				//pipes
				p = q.zyx;

				oprep2(p.xy, 3.0, 8.5, 3.0);
				oprep2(p.xz, 12.0, 0.25, 0.0);

				p.y = mod(p.y, 0.3) - 0.5*0.3;
				float k2 = rbox(p, float3(0.12, 0.12, 1.0), 0.05) - 0.01;

				p = q.xzy;
				float r = p.y*0.02 + sin(_Time.y)*0.05;
				oprep2(p.zy, 3.0, 8.5, 0.0);
				float g = cylinder(p, float2(1.15 + r, 17.0)) - sin(p.y*1.3 - _Time.y * 4.0)*0.1 - d3;
				float g2 = cylinder(p, float2(1.05 + r, 18.0)) - sin(p.y*1.3 - _Time.y * 4.0)*0.1 - d3;

				float tot = max(h, -h2);
				float sub = max(g, -g2);
				float o = max(tot, -g);
				float i = max(sub, -h);

				o = max(o, -k);
				i = max(i, -k2);

				tot = sminp(o, i);

				return float2(tot*0.9, float1x1);
			}
			/***********************************************/
			float3 normal(float3 p) {
				float3 e = float3(0.01, -0.01, 0.0);
				return normalize(float3(e.xyy*DE(p + e.xyy).x + e.yyx*DE(p + e.yyx).x + e.yxy*DE(p + e.yxy).x + e.xxx*DE(p + e.xxx).x));
			}
			/***********************************************/
			float calcAO(float3 p, float3 n) {
				float ao = 0.0;
				float sca = 1.0;
				for (int i = 0; i < AOSTEPS; i++) {
					float h = 0.01 + 1.2*pow(float(i) / float(AOSTEPS), 1.5);
					float dd = DE(p + n * h).x;
					ao += -(dd - h)*sca;
					sca *= 0.65;
				}
				return clamp(1.0 - 1.0*ao, 0.0, 1.0);
				//  return clamp(ao,0.0,1.0);
			}
			/***********************************************/
			float calcSh(float3 ro, float3 rd, float s, float e, float k) {
				float res = 1.0;
				for (int i = 0; i < SHSTEPS; i++) {
					if (s > e) break;
					float h = DE(ro + rd * s).x;
					res = min(res, k*h / s);
					s += 0.02*SHPOWER;
				}
				return clamp(res, 0.0, 1.0);
			}
			/***********************************************/
			void rot(inout float3 p, float3 r) {
				float sa = sin(r.y); float sb = sin(r.x); float sc = sin(r.z);
				float ca = cos(r.y); float cb = cos(r.x); float cc = cos(r.z);
				p = mul(float3x3(cb*cc, cc*sa*sb - ca * sc, ca*cc*sb + sa * sc, cb*sc, ca*cc + sa * sb*sc, -cc * sa + ca * sb*sc, -sb, cb*sa, ca*cb), p);
			}
			/***********************************************/
			fixed4 frag(v2f i) : SV_Target
			{
				float2 p = -1.0 + 2.0 * i.uv;
				//p.x *= iResolution.x / iResolution.y;
				float3 ta = float3(0.0, 0.0, 0.0);
				float3 ro = float3(0.0, 0.0, -15.0);
				float3 lig = normalize(float3(2.3, 3.0, 0.0));

				//	float2 mp=iMouse.xy/iResolution.xy;
				//	rot(ro,float3(mp.x,mp.y,0.0));
				//	rot(lig,float3(mp.x,mp.y,0.0));

				float a = _Time.y * 0.5;
				float b = sin(_Time.y*0.25)*0.75;
				rot(ro, float3(a, b, 0.0));
				rot(lig, float3(a, b, 0.0));

				float3 cf = normalize(ta - ro);
				float3 cr = normalize(cross(cf, float3(0.0, 1.0, 0.0)));
				float3 cu = normalize(cross(cr, cf));
				float3 rd = normalize(p.x*cr + p.y*cu + 2.5*cf);

				float3 col = 0.0;
				/* trace */
				float2 r = 0.0;
				float d = 0.0;
				float3 ww;
				for (int i = 0; i < MARCHSTEPS; i++) {
					ww = ro + rd * d;
					r = DE(ww);
					if (abs(r.x) < 0.00 || r.x > FARCLIP) break;
					d += r.x;
				}
				r.x = d;
				/* draw */
				if (r.x < FARCLIP) {
					float2 rs = float2(0.2, 1.0);  //rim and spec
					if (r.y == float1x1) { col = float3(0.29, 0.53, 0.91); }

					float3 nor = normal(ww);

					float amb = 1.0;
					float dif = clamp(dot(nor, lig), 0.0, 1.0);
					float bac = clamp(dot(nor, -lig), 0.0, 1.0);
					float rim = pow(1. + dot(nor, rd), 3.0);
					float spe = pow(clamp(dot(lig, reflect(rd, nor)), 0.5, 1.0), 16.0);
					float ao = calcAO(ww, nor);
					float sh = calcSh(ww, lig, 0.01, 2.0, 4.0);

					col *= 0.5*amb*AMBCOL*ao + 0.4*dif*DIFCOL*sh + 0.05*bac*BACCOL*ao;
					col += 0.3*rim*amb * rs.x;
					col += 0.5*pow(spe, 1.0)*sh * rs.y;

				}

				col *= exp(.08*-r.x); col *= 2.0;

				return float4(col, 1.0);
			}


           
            ENDCG
        }
    }
}

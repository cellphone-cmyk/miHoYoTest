// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SyntyStudios/CustomCharacter"
{
	Properties
	{
		_Color_Primary("Color_Primary", Color) = (0.2431373,0.4196079,0.6196079,0)
		_Color_Secondary("Color_Secondary", Color) = (0.8196079,0.6431373,0.2980392,0)
		_Color_Leather_Primary("Color_Leather_Primary", Color) = (0.282353,0.2078432,0.1647059,0)
		_Color_Metal_Primary("Color_Metal_Primary", Color) = (0.5960785,0.6117647,0.627451,0)
		_Color_Leather_Secondary("Color_Leather_Secondary", Color) = (0.372549,0.3294118,0.2784314,0)
		_Color_Metal_Dark("Color_Metal_Dark", Color) = (0.1764706,0.1960784,0.2156863,0)
		_Color_Metal_Secondary("Color_Metal_Secondary", Color) = (0.345098,0.3764706,0.3960785,0)
		_Color_Hair("Color_Hair", Color) = (0.2627451,0.2117647,0.1333333,0)
		_Color_Skin("Color_Skin", Color) = (1,0.8000001,0.682353,1)
		_Color_Stubble("Color_Stubble", Color) = (0.8039216,0.7019608,0.6313726,1)
		_Color_Scar("Color_Scar", Color) = (0.9294118,0.6862745,0.5921569,1)
		_Color_BodyArt("Color_BodyArt", Color) = (0.2283196,0.5822246,0.7573529,1)
		[HideInInspector]_Texture_Color_Metal_Primary("Texture_Color_Metal_Primary", 2D) = "white" {}
		_Texture("Texture", 2D) = "white" {}
		[HideInInspector]_Texture_Base_Secondary("Texture_Base_Secondary", 2D) = "white" {}
		[HideInInspector]_Texture_Metal_Secondary("Texture_Metal_Secondary", 2D) = "white" {}
		[HideInInspector]_Texture_Color_Metal_Dark("Texture_Color_Metal_Dark", 2D) = "white" {}
		[HideInInspector]_Texture_BodyArt("Texture_BodyArt", 2D) = "white" {}
		[HideInInspector]_Mask_Primary("Mask_Primary", 2D) = "white" {}
		[HideInInspector]_Mask_Secondary("Mask_Secondary", 2D) = "white" {}
		[HideInInspector]_Texture_Base_Primary("Texture_Base_Primary", 2D) = "white" {}
		[HideInInspector]_Texture_Hair("Texture_Hair", 2D) = "white" {}
		[HideInInspector]_Texture_Skin("Texture_Skin", 2D) = "white" {}
		[HideInInspector]_Texture_Stubble("Texture_Stubble", 2D) = "white" {}
		[HideInInspector]_Texture_Scar("Texture_Scar", 2D) = "white" {}
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_BodyArt_Amount("BodyArt_Amount", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color_BodyArt;
		uniform sampler2D _Texture;
		uniform float4 _Texture_ST;
		uniform float4 _Color_Primary;
		uniform sampler2D _Mask_Primary;
		uniform float4 _Mask_Primary_ST;
		uniform float4 _Color_Secondary;
		uniform sampler2D _Mask_Secondary;
		uniform float4 _Mask_Secondary_ST;
		uniform float4 _Color_Leather_Primary;
		uniform sampler2D _Texture_Base_Primary;
		uniform float4 _Texture_Base_Primary_ST;
		uniform float4 _Color_Leather_Secondary;
		uniform sampler2D _Texture_Base_Secondary;
		uniform float4 _Texture_Base_Secondary_ST;
		uniform float4 _Color_Metal_Primary;
		uniform sampler2D _Texture_Color_Metal_Primary;
		uniform float4 _Texture_Color_Metal_Primary_ST;
		uniform float4 _Color_Metal_Secondary;
		uniform sampler2D _Texture_Metal_Secondary;
		uniform float4 _Texture_Metal_Secondary_ST;
		uniform float4 _Color_Metal_Dark;
		uniform sampler2D _Texture_Color_Metal_Dark;
		uniform float4 _Texture_Color_Metal_Dark_ST;
		uniform float4 _Color_Hair;
		uniform sampler2D _Texture_Hair;
		uniform float4 _Texture_Hair_ST;
		uniform float4 _Color_Skin;
		uniform sampler2D _Texture_Skin;
		uniform float4 _Texture_Skin_ST;
		uniform float4 _Color_Stubble;
		uniform sampler2D _Texture_Stubble;
		uniform float4 _Texture_Stubble_ST;
		uniform float4 _Color_Scar;
		uniform sampler2D _Texture_Scar;
		uniform float4 _Texture_Scar_ST;
		uniform sampler2D _Texture_BodyArt;
		uniform float4 _Texture_BodyArt_ST;
		uniform float _BodyArt_Amount;
		uniform float _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Texture = i.uv_texcoord * _Texture_ST.xy + _Texture_ST.zw;
			float2 uv_Mask_Primary = i.uv_texcoord * _Mask_Primary_ST.xy + _Mask_Primary_ST.zw;
			float temp_output_25_0_g2 = 0.5;
			float temp_output_22_0_g2 = step( tex2D( _Mask_Primary, uv_Mask_Primary, float2( 0,0 ), float2( 0,0 ) ).r , temp_output_25_0_g2 );
			float4 lerpResult35 = lerp( tex2D( _Texture, uv_Texture, float2( 0,0 ), float2( 0,0 ) ) , _Color_Primary , temp_output_22_0_g2);
			float2 uv_Mask_Secondary = i.uv_texcoord * _Mask_Secondary_ST.xy + _Mask_Secondary_ST.zw;
			float temp_output_25_0_g3 = 0.5;
			float temp_output_22_0_g3 = step( tex2D( _Mask_Secondary, uv_Mask_Secondary, float2( 0,0 ), float2( 0,0 ) ).r , temp_output_25_0_g3 );
			float4 lerpResult41 = lerp( lerpResult35 , _Color_Secondary , temp_output_22_0_g3);
			float2 uv_Texture_Base_Primary = i.uv_texcoord * _Texture_Base_Primary_ST.xy + _Texture_Base_Primary_ST.zw;
			float temp_output_25_0_g4 = 0.5;
			float temp_output_22_0_g4 = step( tex2D( _Texture_Base_Primary, uv_Texture_Base_Primary, float2( 0,0 ), float2( 0,0 ) ).r , temp_output_25_0_g4 );
			float4 lerpResult45 = lerp( lerpResult41 , _Color_Leather_Primary , temp_output_22_0_g4);
			float2 uv_Texture_Base_Secondary = i.uv_texcoord * _Texture_Base_Secondary_ST.xy + _Texture_Base_Secondary_ST.zw;
			float temp_output_25_0_g9 = 0.5;
			float temp_output_22_0_g9 = step( tex2D( _Texture_Base_Secondary, uv_Texture_Base_Secondary, float2( 0,0 ), float2( 0,0 ) ).r , temp_output_25_0_g9 );
			float4 lerpResult65 = lerp( lerpResult45 , _Color_Leather_Secondary , temp_output_22_0_g9);
			float2 uv_Texture_Color_Metal_Primary = i.uv_texcoord * _Texture_Color_Metal_Primary_ST.xy + _Texture_Color_Metal_Primary_ST.zw;
			float temp_output_25_0_g10 = 0.5;
			float temp_output_22_0_g10 = step( tex2D( _Texture_Color_Metal_Primary, uv_Texture_Color_Metal_Primary, float2( 0,0 ), float2( 0,0 ) ).r , temp_output_25_0_g10 );
			float4 lerpResult124 = lerp( lerpResult65 , _Color_Metal_Primary , temp_output_22_0_g10);
			float2 uv_Texture_Metal_Secondary = i.uv_texcoord * _Texture_Metal_Secondary_ST.xy + _Texture_Metal_Secondary_ST.zw;
			float temp_output_25_0_g11 = 0.5;
			float temp_output_22_0_g11 = step( tex2D( _Texture_Metal_Secondary, uv_Texture_Metal_Secondary, float2( 0,0 ), float2( 0,0 ) ).r , temp_output_25_0_g11 );
			float4 lerpResult132 = lerp( lerpResult124 , _Color_Metal_Secondary , temp_output_22_0_g11);
			float2 uv_Texture_Color_Metal_Dark = i.uv_texcoord * _Texture_Color_Metal_Dark_ST.xy + _Texture_Color_Metal_Dark_ST.zw;
			float temp_output_25_0_g12 = 0.5;
			float temp_output_22_0_g12 = step( tex2D( _Texture_Color_Metal_Dark, uv_Texture_Color_Metal_Dark, float2( 0,0 ), float2( 0,0 ) ).r , temp_output_25_0_g12 );
			float4 lerpResult140 = lerp( lerpResult132 , _Color_Metal_Dark , temp_output_22_0_g12);
			float2 uv_Texture_Hair = i.uv_texcoord * _Texture_Hair_ST.xy + _Texture_Hair_ST.zw;
			float temp_output_25_0_g14 = 0.5;
			float temp_output_22_0_g14 = step( tex2D( _Texture_Hair, uv_Texture_Hair, float2( 0,0 ), float2( 0,0 ) ).r , temp_output_25_0_g14 );
			float4 lerpResult49 = lerp( lerpResult140 , _Color_Hair , temp_output_22_0_g14);
			float2 uv_Texture_Skin = i.uv_texcoord * _Texture_Skin_ST.xy + _Texture_Skin_ST.zw;
			float temp_output_25_0_g15 = 0.5;
			float temp_output_22_0_g15 = step( tex2D( _Texture_Skin, uv_Texture_Skin, float2( 0,0 ), float2( 0,0 ) ).r , temp_output_25_0_g15 );
			float4 lerpResult53 = lerp( lerpResult49 , _Color_Skin , temp_output_22_0_g15);
			float2 uv_Texture_Stubble = i.uv_texcoord * _Texture_Stubble_ST.xy + _Texture_Stubble_ST.zw;
			float temp_output_25_0_g16 = 0.5;
			float temp_output_22_0_g16 = step( tex2D( _Texture_Stubble, uv_Texture_Stubble, float2( 0,0 ), float2( 0,0 ) ).r , temp_output_25_0_g16 );
			float4 lerpResult57 = lerp( lerpResult53 , _Color_Stubble , temp_output_22_0_g16);
			float2 uv_Texture_Scar = i.uv_texcoord * _Texture_Scar_ST.xy + _Texture_Scar_ST.zw;
			float temp_output_25_0_g18 = 0.5;
			float temp_output_22_0_g18 = step( tex2D( _Texture_Scar, uv_Texture_Scar, float2( 0,0 ), float2( 0,0 ) ).r , temp_output_25_0_g18 );
			float4 lerpResult61 = lerp( lerpResult57 , _Color_Scar , temp_output_22_0_g18);
			float2 uv_Texture_BodyArt = i.uv_texcoord * _Texture_BodyArt_ST.xy + _Texture_BodyArt_ST.zw;
			float4 color151 = IsGammaSpace() ? float4(1,1,1,0) : float4(1,1,1,0);
			float4 lerpResult152 = lerp( tex2D( _Texture_BodyArt, uv_Texture_BodyArt, float2( 0,0 ), float2( 0,0 ) ) , color151 , _BodyArt_Amount);
			float4 lerpResult69 = lerp( _Color_BodyArt , lerpResult61 , lerpResult152);
			o.Albedo = lerpResult69.rgb;
			float temp_output_154_0 = _Smoothness;
			o.Metallic = temp_output_154_0;
			o.Smoothness = temp_output_154_0;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiply : MonoBehaviour {

    public Shader curShader;
    public Texture2D blendTexture;
    public float blendOpacity = 1.0f;

    public Texture2D noiseTex;
    public Texture2D maskTex;
    public Color color_A=Color.blue;
    public Color color_B=Color.red;
    [Range(0,1)]
    public float _FactorA=0.73f;
    [Range(0,1)]
    public float _FactorB=0.793f;

    [Range(0,1)]
    public float clipFactor = 0;

    private Material curMaterial;
    private Material CurMaterial
    {
        get
        {
            if (curMaterial == null)
            {
                curMaterial = new Material(curShader);
            }
            return curMaterial;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(curShader!= null)
        {
            //CurMaterial.SetTexture("_NoiseTex",noiseTex);
            //CurMaterial.SetTexture("_Mask",maskTex);
            //CurMaterial.SetFloat("_FactorA",_FactorA);
            //CurMaterial.SetFloat("_FactorB",_FactorB);
            //CurMaterial.SetColor("_ColorA", color_A);
            //CurMaterial.SetColor("_ColorB", color_B);
            //CurMaterial.SetFloat("_ClipFactor", clipFactor);
            CurMaterial.SetTexture("_BlendTex", blendTexture);
            CurMaterial.SetFloat("_opacity", blendOpacity);
            Graphics.Blit(source,destination,CurMaterial);
        }
        else
        {
            Graphics.Blit(source,destination);
        }
    }

    private void OnDisable()
    {
        Destroy(curMaterial);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upravlenie_aeronaveom : MonoBehaviour
{
    private GameObject aeronave;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed=100;
    public GameObject explosao_abstr;
    public GameObject explosao_fogo_abstr;
    //-------------------------------------------+
    private float Gasolina=400f;
    //--------------------------------------------+
    private float cortar_para_o_lado=0f;
    private float max_cortar_para_o_lado=4f;
    private float alterações_deltas_cortar_para_o_lado=15f;
    private int direção_anterior_para=0;
    private float coeficiente_para_inclinação=9f;
    private const float max_velocidade_da_aeronave=10f;
    private const float min_velocidade_da_aeronave=1.5f;
    private float velocidade_da_aeronave=3.2f;
    private float alterações_deltas_velocidade_aeronave=6f; 
    private float angulo_inclinacao_aeronave=0;
    public AudioSource audioSource_aeronave;

    private bool Mais_Esquerda=false;
    private bool Mais_Direita=false;
    private bool Mais_Velocidade=false;
    private bool Mais_Frenagem=false;

    void Start(){
        aeronave=GameObject.Find("___aeronave");
    }

    void Update(){
        Gasolina-=0.5f;
        if(Gasolina <= 0){
            Destroy(aeronave);
            Application.Quit();
        }
        Rastreamento_do_tiro();
        Mudar_velocidade_aeronave();
        Mudar_velocidade_para_lado();
        Definicao_mudanca_direcao_para_lado();
        Inclinar_aeronave();
        Mover_aeronave_para_lado();
        Voo_aeronave();
        
    }

    private void Voo_aeronave(){
        aeronave.transform.Translate(0,0,velocidade_da_aeronave * Time.deltaTime,Space.World);
    }


    private void Rastreamento_do_tiro(){
        if(Input.GetKey(KeyCode.W) & !Input.GetKey(KeyCode.S)){
            Mais_Velocidade=true;
        }else{
            Mais_Velocidade=false;
        }

        if(Input.GetKey(KeyCode.S) & !Input.GetKey(KeyCode.W)){
            Mais_Frenagem=true;
        }else{
            Mais_Frenagem=false;
        }

        if(Input.GetKey(KeyCode.A) & !Input.GetKey(KeyCode.D)){
            Mais_Esquerda=true;
        }else{
            Mais_Esquerda=false;
        }

        if(Input.GetKey(KeyCode.D) & !Input.GetKey(KeyCode.A)){
            Mais_Direita=true;
        }else{
            Mais_Direita=false;
        }
        if(Input.GetKey(KeyCode.Space)){
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position,bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward*(bulletSpeed+velocidade_da_aeronave);
        }
    }

    private void Mudar_velocidade_aeronave(){
        if(Mais_Velocidade & !Mais_Frenagem){
            if(velocidade_da_aeronave<max_velocidade_da_aeronave){
                velocidade_da_aeronave += Time.deltaTime * alterações_deltas_velocidade_aeronave;
            }   
        }
        if(!Mais_Velocidade & Mais_Frenagem){
            if(velocidade_da_aeronave > min_velocidade_da_aeronave){
                velocidade_da_aeronave -= Time.deltaTime * alterações_deltas_velocidade_aeronave;
            }
        }
        audioSource_aeronave.volume=velocidade_da_aeronave / max_velocidade_da_aeronave;
    }

    private void Mudar_velocidade_para_lado(){
        if(Mais_Esquerda | Mais_Direita){
            if(cortar_para_o_lado<max_cortar_para_o_lado){
                cortar_para_o_lado +=Time.deltaTime*alterações_deltas_cortar_para_o_lado;
            }
        }
        if((!Mais_Esquerda & !Mais_Direita) | (Mais_Esquerda & Mais_Direita)){
            cortar_para_o_lado=0;
        }
    }

    private void Mover_aeronave_para_lado(){
        if(Mais_Esquerda){
            aeronave.transform.Translate(-cortar_para_o_lado * Time.deltaTime,0,0,Space.World);
        }
        if(Mais_Direita){
            aeronave.transform.Translate(cortar_para_o_lado * Time.deltaTime,0,0,Space.World);
        }
    }

    private void Definicao_mudanca_direcao_para_lado(){
        int direcao_atual_para_lado;
        if(Mais_Esquerda){
            direcao_atual_para_lado=-1;
        }else if(Mais_Direita){
            direcao_atual_para_lado=1;
        }else{
            direcao_atual_para_lado=0;
        }

        if(direcao_atual_para_lado != direção_anterior_para){
            cortar_para_o_lado=0;
            direção_anterior_para=direcao_atual_para_lado;
        }

    }

    private void Inclinar_aeronave(){
        angulo_inclinacao_aeronave = cortar_para_o_lado * coeficiente_para_inclinação;
        aeronave.transform.rotation=Quaternion.Euler(0,0,0);
        aeronave.transform.Rotate(0,0,cortar_para_o_lado * coeficiente_para_inclinação * - direção_anterior_para);
    }

     private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Mishen")){
            Destroy(aeronave);
            Application.Quit();
        }
        if(other.gameObject.CompareTag("Gasolina")){
            Gasolina+=20;
            Destroy(other.gameObject);
        }

    }

}

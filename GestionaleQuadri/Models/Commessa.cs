namespace GestionaleQuadri.Models
{
    public class Commessa
    {
    }

    public class AnnoAzienda
    {
        public int anno { get; set; }
        public string nome_azienda { get; set; }
    }

    public class ViewCommessa
    {
        public List<AnnoAzienda> aa { get; set; }
        public List<int> anno { get; set; }
    }



    public class Storico
    {
        public string CKY_CNT_AGENTE { get; set; }
        public string NomeAgente { get; set; }
        public string NPC_PROVV { get; set; }
        public string NMP_VALPRO_UM1 { get; set; } //provvigione
        public string NMP_VALMOV_UM1 { get; set; } //venduto
        public string CDS_CNT_RAGSOC { get; set; } // nome
        public string CKY_CNT { get; set; } // id cliente
        public string ANNO { get; set; } // anno

        public string DTT_DOC { get; set; } //data
        public string NGL_DOC { get; set; }

        public string CDS_CAT_STAT_ART { get; set; }
        public string CSG_DOC { get; set; }

        public string CKY_MERC { get; set; }

        public string CDS_MERC { get; set; }

    }

    public class Agente
    {
        public string CKY_CNT { get; set; } = string.Empty;
        public string CDS_CNT_RAGSOC { get; set; } = string.Empty;
        public string CDS_INDIR { get; set; } = string.Empty;
        public string CDS_CAP { get; set; } = string.Empty;
        public string CDS_LOC { get; set; } = string.Empty;
        public string CDS_PROV { get; set; } = string.Empty;
        public string CDS_TEL_TELEX { get; set; } = string.Empty;
        public string CDS_FAX { get; set; } = string.Empty;
        public string CDS_EMAIL { get; set; } = string.Empty;
        public string CDS_ZONA_CLFR { get; set; } = string.Empty;
    }

    public class Azienda
    {
        public string azienda { get; set; } = string.Empty;
        public string nome_azienda { get; set; } = string.Empty;
    }

    public class Commitente
    {
        public string nome_commessa {  get; set; } = string.Empty;
        public string commessa {  get; set; } = string.Empty;

        public int indice_commessa { get; set; } = 0;

        public int anno {  get; set; } = 0; 

        public int num_quadri { get; set; }
        public string data_ciclo_lavoro { get; set; }
    }


    public class Quadro
    {
        public string nome_quadro { get; set; } = string.Empty; 
        public string odl { get; set; }
        public string data_ciclo_lavoro { get; set; }
        public string nome_commessa { get; set; }
    }
}
